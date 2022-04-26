using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Builders;
using Extensions;
using Generators;
using Helpers;
using InnoSetup.ScriptBuilder;
using Models;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.InnoSetup;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

/// <summary>
/// Проект для сбоки MSI
/// </summary>
public class RevitBuild : NukeBuild
{
    string _fullVersionsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Autodesk",
        "Revit",
        "Addins");

    string MsBuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe";
    string _version;
    private string? _outputTmpDirBin;
    private string? _outputTmpDir;
    private readonly RevitWixBuilder _wix;
    private string? _project;
    private List<AssemblyType>? _types;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    public RevitBuild()
    {
        _wix = new RevitWixBuilder();
    }

    /// <summary>
    /// Output "bin" temp directory path.
    /// </summary>
    protected string OutputTmpDirBin
        => _outputTmpDirBin ??= Path.Combine(OutputTmpDir, "bin");

    /// <summary>
    /// Output temp directory path.
    /// </summary>
    protected string OutputTmpDir
        => _outputTmpDir ??= Path.Combine(Path.GetTempPath(), $"RxBim_build_{Guid.NewGuid()}");

    private Project ProjectForMsiBuild => Solution.AllProjects.First(x => x.Name == _project);
    
    /// <summary>
    /// Certificate path.
    /// </summary>
    [Parameter("Certificate path")]
    public string? Cert { get; set; }

    /// <summary>
    /// Private key container.
    /// </summary>
    [Parameter("Private key container")]
    public string? PrivateKey { get; set; }

    /// <summary>
    /// CSP containing for Private key.
    /// </summary>
    [Parameter("CSP containing for Private key")]
    public string? Csp { get; set; }

    /// <summary>
    /// Digest algorithm.
    /// </summary>
    [Parameter("Digest algorithm")]
    public string? Algorithm { get; set; }

    /// <summary>
    /// Timestamp server URL.
    /// </summary>
    [Parameter("Timestamp server URL")]
    public string? ServerUrl { get; set; }

    /// <summary>
    /// Проект
    /// </summary>
    [Parameter("Select project")]
    public string Project
    {
        get
        {
            if (_project == null)
            {
                var result = ConsoleUtility.PromptForChoice(
                    "Select project:",
                    Solution.AllProjects
                        .Select(x => (x.Name, x.Name))
                        .ToArray());

                _project = result == nameof(Solution)
                    ? Solution.Name
                    : Solution.AllProjects.FirstOrDefault(x => x.Name == result)?.Name;
            }

            return _project;
        }

        set => _project = value;
    }

    /// <summary>
    /// Версия ревита
    /// </summary>
    [Parameter("Select revit version")]
    public string Version
    {
        get
        {
            if (_version == null)
            {
                var result = ConsoleUtility.PromptForChoice(
                    "Select project:",
                    GetVersions()
                        .Select(x => (x, x))
                        .ToArray());

                _version = result;
            }

            return _version;
        }

        set => _version = value;
    }

    [Solution]
    public Solution Solution { get; set; } = null!;

    public Target Clean => _ => _
        .Description("Clean bin/, obj/")
        .Executes(() =>
        {
            Enumerable.Where<string>(GlobDirectories(
                    Solution.Directory, "*#1#bin", "*#1#obj"),
                    x => !IsDescendantPath(BuildProjectDirectory, x))
                .ForEach(DeleteDirectory);
        });

    public Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s.SetProjectFile<DotNetRestoreSettings>(Solution.Path));
        });

    /// <summary>
    /// Installs WixSharp.
    /// </summary>
    public Target InstallWixTools => _ => _
        .Executes(WixHelper.SetupWixTools);

    /// <summary>
    /// Compiles the project defined in <see cref="Project"/> to temporary path.
    /// </summary>
    public Target CompileToTemp => _ => _
        .Description("Build project to temp output")
        .Requires(() => Project)
        .DependsOn(Restore)
        .Executes(() =>
        {
            Console.Write(Project);
            DotNetBuild(settings => settings.SetProjectFile(GetProjectPath(Project))
                .SetOutputDirectory(OutputTmpDirBin)
                .SetConfiguration(Configuration));
        });

    /// <summary>
    /// Builds an EXE package.
    /// </summary>
    public Target BuildInnoExe => _ => _
        .Description("Build installation EXE from selected project (if Release - sign assemblies)")
        .DependsOn(SignAssemblies)
        .DependsOn(GenerateAdditionalFiles)
        .DependsOn(GeneratePackageContentsFile)
        .Executes(() =>
        {
            CreateOutDirectory();
            BuildInnoInstaller(ProjectForMsiBuild, Configuration);
        });

    /// <summary>
    /// Generates project properties (PackageGuid, UpgradeCode and other).
    /// </summary>
    public Target GenerateProjectProps => _ => _
        .Requires(() => Project)
        .Requires(() => Configuration)
        .Executes(() => new RevitProjectPropertiesGenerator().GenerateProperties(ProjectForMsiBuild, Configuration));

    /// <summary>
    /// Signs assemblies af a given project.
    /// </summary>
    public Target SignAssemblies => _ => _
        .Requires(() => Project)
        .Requires(() => Configuration)
        .DependsOn(CompileToTemp)
        .Executes(() =>
        {
            if (Configuration != Configuration.Release)
                return;

            var types = GetAssemblyTypes(
                ProjectForMsiBuild, OutputTmpDirBin, OutputTmpDir, Configuration);

            types.SignAssemblies(
                (AbsolutePath)OutputTmpDirBin,
                (AbsolutePath)Cert,
                PrivateKey.Ensure(),
                Csp.Ensure(),
                Algorithm.Ensure(),
                ServerUrl.Ensure());
        });

    /// <summary>
    /// Generates a package contents file.
    /// </summary>
    public Target GeneratePackageContentsFile => _ => _
        .Requires(() => Project)
        .Requires(() => Configuration)
        .DependsOn(CompileToTemp)
        .Executes(() =>
        {
            _wix.GeneratePackageContentsFile(ProjectForMsiBuild, Configuration, OutputTmpDir);
        });

    /// <summary>
    /// Generates additional files.
    /// </summary>
    public Target GenerateAdditionalFiles => _ => _
        .Requires(() => Project)
        .Requires(() => Configuration)
        .DependsOn(CompileToTemp)
        .Executes(() =>
        {
            var types = GetAssemblyTypes(
                ProjectForMsiBuild, OutputTmpDirBin, OutputTmpDir, Configuration);

            _wix.GenerateAdditionalFiles(
                ProjectForMsiBuild.Name, Solution.AllProjects, types, OutputTmpDir);
        });

    public Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(settings => settings.SetProjectFile(Solution.Path)
                .SetConfiguration(Configuration));
        });

    private List<string> GetVersions()
    {
        return Directory.GetDirectories(_fullVersionsPath).Select(i => new DirectoryInfo(i).Name).ToList();
    }

    private AbsolutePath GetProjectPath(string? name)
    {
        return Solution.AllProjects.FirstOrDefault(x => x.Name == name)?.Path ?? Solution.Path;
    }

    /// <summary>
    /// Gets assembly types.
    /// </summary>
    /// <param name="project">Selected Project.</param>
    /// <param name="outputBinDir">Output assembly directory.</param>
    /// <param name="outputDir">Output directory.</param>
    /// <param name="configuration">Selected configuration.</param>
    private List<AssemblyType> GetAssemblyTypes(
        Project project,
        string outputBinDir,
        string outputDir,
        string configuration)
    {
        var options = _wix.GetBuildMsiOptions(project, outputDir, configuration, _version);
        return _types ??= project.GetAssemblyTypes(outputBinDir, options);
    }
    
    private void CreateOutDirectory()
    {
        var outDir = Solution.Directory / "out";
        if (!Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }
    }

    private void BuildInnoInstaller(
        Project project,
        string configuration)
    {
        var iss = TemporaryDirectory / "package.iss";
        var options = _wix.GetBuildMsiOptions(project, OutputTmpDir, configuration, _version);
        var setupFileName = $"{options.OutFileName}_{options.Version}";

        InnoBuilder.Create(
                options,
                (AbsolutePath)OutputTmpDir,
                (AbsolutePath)OutputTmpDirBin,
                setupFileName)
            .AddIcons()
            .AddFonts()
            .AddUninstallScript()
            .Build(iss);

        var outDir = project.Solution.Directory / "out";
        InnoSetupTasks.InnoSetup(config => config
            .SetProcessToolPath(ToolPathResolver.GetPackageExecutable("Tools.InnoSetup", "ISCC.exe"))
            .SetScriptFile(iss)
            .SetOutputDir(outDir));

        DeleteDirectory(OutputTmpDir);
        SignSetupFile(outDir / $"{setupFileName}.exe");
    }

    private void SignSetupFile(string filePath)
    {
        if (Configuration != Configuration.Release)
            return;

        filePath.SignFile(
            (AbsolutePath)Cert,
            PrivateKey.Ensure(),
            Csp.Ensure(),
            Algorithm.Ensure(),
            ServerUrl.Ensure());
    }
}