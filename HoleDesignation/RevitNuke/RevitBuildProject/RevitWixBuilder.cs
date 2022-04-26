using System;
using System.Collections.Generic;
using System.Linq;
using Builders;
using Models;
using Nuke.Common.ProjectModel;

/// <inheritdoc />
public class RevitWixBuilder : WixBuilder<RevitPackageContentsGenerator>
{
    /// <inheritdoc />
    public override void GenerateAdditionalFiles(
        string? rootProjectName,
        IEnumerable<Project> allProject,
        IEnumerable<AssemblyType> addInTypes,
        string outputDir)
    {
        var addInGenerator = new AddInGenerator();
        var addInTypesPerProjects = addInTypes
            .Select(x => new ProjectWithAssemblyType(
                allProject.First(proj => proj.Name == x.AssemblyName), x))
            .ToList();
        addInGenerator.GenerateAddInFile(rootProjectName, addInTypesPerProjects, outputDir);
    }

    /// <inheritdoc />
    protected override bool NeedGeneratePackageContents(string configuration)
    {
        return configuration == Configuration.Release;
    }

    /// <inheritdoc />
    protected override string GetDebugInstallDir(Project project, string version)
    {
        Console.Write(version);
        return $"%AppDataFolder%/Autodesk/Revit/Addins/{version}";
    }
}