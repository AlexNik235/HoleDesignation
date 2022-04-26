﻿namespace Builders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Extensions;
    using Generators;
    using Models;
    using Nuke.Common.ProjectModel;

    /// <summary>
    /// The Wix package builder.
    /// </summary>
    public class WixBuilder<T>
        where T : PackageContentsGenerator, new()
    {
        private Options? _options;

        /// <summary>
        /// Builds MSI.
        /// </summary>
        /// <param name="project">Selected project.</param>
        /// <param name="configuration">Selected configuration.</param>
        /// <param name="outputDir">Output directory path.</param>
        /// <param name="outputBinDir">Output assemblies directory path.</param>
        public void BuildMsi(
            Project project,
            string configuration,
            string outputDir,
            string outputBinDir,
            string version)
        {
            if (!Directory.Exists(outputBinDir))
                return;

            var options = GetBuildMsiOptions(project, outputDir, configuration, version);
            const string toolPath =
                @"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild"; // "rxbim.msi.builder";

            project.BuildMsiWithTool(toolPath, options);
        }

        /// <summary>
        /// Gets build MSI options.
        /// </summary>
        /// <param name="project">Selected Project.</param>
        /// <param name="outputDir">Output directory path.</param>
        /// <param name="configuration">Selected configuration.</param>
        public Options GetBuildMsiOptions(
            Project project,
            string outputDir,
            string configuration,
            string version)
        {
            var installDir = GetInstallDir(project, configuration, version);
            return _options ??= project.GetSetupOptions(
                installDir, outputDir, configuration);
        }

        /// <summary>
        /// Generates additional files.
        /// </summary>
        /// <param name="rootProjectName">Root project name.</param>
        /// <param name="allProject">All projects.</param>
        /// <param name="addInTypes">Assembly types.</param>
        /// <param name="outputDir">Output directory path.</param>
        public virtual void GenerateAdditionalFiles(
            string? rootProjectName,
            IEnumerable<Project> allProject,
            IEnumerable<AssemblyType> addInTypes,
            string outputDir)
        {
        }

        /// <summary>
        /// Generates package contents file.
        /// </summary>
        /// <param name="project">Selected project.</param>
        /// <param name="configuration">Selected configuration.</param>
        /// <param name="outputDir">Output directory path.</param>
        public void GeneratePackageContentsFile(
            Project project,
            string configuration,
            string outputDir)
        {
            if (!NeedGeneratePackageContents(configuration))
                return;

            var packageContentsGenerator = new T();
            packageContentsGenerator.Generate(project, outputDir);
        }

        /// <summary>
        /// Returns True, if need generate PackageContents.
        /// </summary>
        /// <param name="configuration">Selected configuration.</param>
        protected virtual bool NeedGeneratePackageContents(string configuration) => true;

        /// <summary>
        /// Gets Debug configuration install directory.
        /// </summary>
        /// <param name="project">Selected project.</param>
        protected virtual string GetDebugInstallDir(Project project, string version)
        {
            return GetReleaseInstallDir(project, version);
        }

        /// <summary>
        /// Gets installation directory.
        /// </summary>
        /// <param name="project">Selected project.</param>
        /// <param name="configuration">Selected configuration.</param>
        private string GetInstallDir(
            Project project,
            string configuration,
            string version)
        {
            return configuration switch
            {
                "Debug" => GetDebugInstallDir(project, version),
                "Release" => GetReleaseInstallDir(project, version),
                _ => throw new ArgumentException("Configuration not set!")
            };
        }

        /// <summary>
        /// Gets Release configuration install directory path.
        /// </summary>
        /// <param name="project">Selected project.</param>
        private string GetReleaseInstallDir(Project project, string version)
        {
            return $"%AppDataFolder%/Autodesk/ApplicationPlugins/{project.Name}.bundle";
        }
    }
}