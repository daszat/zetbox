
namespace Kistl.Server.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Arebis.CodeGeneration;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators.Extensions;

    using Microsoft.Build.BuildEngine;
    using Microsoft.Build.Framework;
    using System.Globalization;

    public static class Generator
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Generator");

        public static void GenerateCode()
        {
            using (Log.DebugTraceMethodCall())
            {
                var workingPath = ApplicationContext.Current.Configuration.Server.CodeGenWorkingPath;
                if (String.IsNullOrEmpty(workingPath))
                {
                    throw new ConfigurationException("CodeGenWorkingPath is not defined in the current configuration file.");
                }

                if (!Directory.Exists(workingPath))
                {
                    Log.InfoFormat("Creating destination directory: [{0}]", workingPath);
                    Directory.CreateDirectory(workingPath);
                }

                Log.InfoFormat("Generating Code to [{0}]", workingPath);
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    string serverReferencePath = Path.GetDirectoryName(typeof(Generator).Assembly.Location);
                    string clientReferencePath = Path.GetFullPath(Path.Combine(serverReferencePath, @"..\Client"));

                    Log.DebugFormat("serverReferencePath = [{0}]", serverReferencePath);
                    Log.DebugFormat("clientReferencePath = [{0}]", clientReferencePath);

                    var generators = new[]{
                        new { Caption = "Interface Source Files", Generator = DataObjectGeneratorFactory.GetInterfaceGenerator(), ReferencePath = serverReferencePath },
                        new { Caption = "Server Source Files", Generator = DataObjectGeneratorFactory.GetServerGenerator(), ReferencePath = serverReferencePath },
                        new { Caption = "Client Source Files", Generator = DataObjectGeneratorFactory.GetClientGenerator(), ReferencePath = clientReferencePath },
                        new { Caption = "Generating Frozen Source Files", Generator = DataObjectGeneratorFactory.GetFreezingGenerator(), ReferencePath = serverReferencePath },
                    };

                    // TODO: move MsBuild logging to log4net
                    if (File.Exists("TemplateCodegenLog.txt"))
                        File.Delete("TemplateCodegenLog.txt");

                    string binPath = Path.Combine(workingPath, @"bin\Debug");
                    binPath = Path.GetFullPath(binPath); // Ensure that path is an absolute path

                    Log.DebugFormat("binPath = [{0}]", binPath);

                    Directory.CreateDirectory(binPath);

                    var engine = new Engine(ToolsetDefinitionLocations.Registry);

                    engine.RegisterLogger(new ConsoleLogger(LoggerVerbosity.Minimal));

                    var logger = new FileLogger();
                    logger.Parameters = String.Format(@"logfile={0}", Path.Combine(workingPath, "compile.log"));
                    engine.RegisterLogger(logger);

                    try
                    {

                        foreach (var gen in generators)
                        {
                            using (log4net.NDC.Push(gen.Caption))
                            {
                                Log.InfoFormat("Generating");
                                string projectFileName = gen.Generator.Generate(ctx, workingPath);

                                Log.DebugFormat("Loading MsBuild Project");
                                var proj = new Project(engine);
                                proj.Load(projectFileName);
                                var defaultPropertyGroup = proj.AddNewPropertyGroup(false);
                                defaultPropertyGroup.AddNewProperty("OutputPath", binPath, true);
                                // Fix XML Path
                                defaultPropertyGroup.AddNewProperty("DocumentationFile", "$(OutputPath)\\$(AssemblyName).xml", false);
                                defaultPropertyGroup.AddNewProperty("KistlAPIPath", gen.ReferencePath, true);

                                Log.DebugFormat("Compiling");
                                if (!engine.BuildProject(proj))
                                {
                                    // TODO: fix dll name here
                                    //File.Delete(Path.Combine(binPath, "Kistl.Objects.dll"));
                                    //File.Delete(Path.Combine(binPath, "Kistl.Objects.pdb"));
                                    throw new ApplicationException(String.Format("Failed to compile {0}", gen.Caption));
                                }
                            }
                        }
                    }
                    finally
                    {
                        // close all logfiles
                        engine.UnregisterAllLoggers();
                    }
                }
                ArchiveOldOutput();
                PublishOutput();
                Log.Info("Finished generating Code");
            }
        }

        private static void PublishOutput()
        {
            var outputPath = ApplicationContext.Current.Configuration.Server.CodeGenOutputPath;
            if (!String.IsNullOrEmpty(outputPath))
            {
                Log.InfoFormat("Publishing results to [{0}]", outputPath);
                if (Directory.Exists(outputPath))
                {
                    Directory.Delete(outputPath, true);
                }
                Directory.Move(ApplicationContext.Current.Configuration.Server.CodeGenWorkingPath, outputPath);
            }
        }

        private static void ArchiveOldOutput()
        {
            var outputPath = ApplicationContext.Current.Configuration.Server.CodeGenOutputPath;
            if (String.IsNullOrEmpty(outputPath))
            {
                throw new ConfigurationException("CodeGenOutputPath is not defined in the current configuration file, but archival was requested: don't know what to archive.");
            }
            var archivePath = ApplicationContext.Current.Configuration.Server.CodeGenArchivePath;
            if (!String.IsNullOrEmpty(archivePath))
            {
                var destDir = Path.Combine(archivePath, DateTime.Now.ToString("'CodeGen'yyyyMMdd'_'HHmmss"));
                if (Directory.Exists(outputPath))
                {
                    Log.InfoFormat("Archiving old results to [{0}]", destDir);
                    Directory.Move(outputPath, destDir);
                }
                else
                {
                    Log.InfoFormat("No results found in [{0}]", outputPath);
                }
            }
        }

        internal static TemplateGenerator GetTemplateGenerator(string providerTemplatePath,
            string template, string output, string targetdir, params object[] templateParameter)
        {
            var gen = new TemplateGenerator();

            gen.Settings.Add("basetemplatepath", "Kistl.Server.Generators.Templates");
            gen.Settings.Add("providertemplatepath", providerTemplatePath);

            gen.Settings.Add("template", template);

            gen.Settings.Add("targetdir", targetdir);
            gen.Settings.Add("output", output);
            gen.Settings.Add("logfile", "TemplateCodegenLog.txt");

            gen.TemplateParameters = templateParameter;

            return gen;
        }

        #region GetLists

        public static IQueryable<ObjectClass> GetObjectClassList(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return from c in ctx.GetQuery<ObjectClass>()
                   select c;
        }

        public static IQueryable<Interface> GetInterfaceList(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return from i in ctx.GetQuery<Interface>()
                   select i;
        }

        public static IQueryable<Enumeration> GetEnumList(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return from e in ctx.GetQuery<Enumeration>()
                   select e;
        }

        public static IQueryable<Struct> GetStructList(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return from s in ctx.GetQuery<Struct>()
                   select s;
        }

        #endregion
    }

    public class KistlCodeTemplate : Arebis.CodeGeneration.CodeTemplate
    {
        public KistlCodeTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }

        protected string ResolveResourceUrl(string template)
        {
            return "res://kistl.server/Kistl.Server.Generators.Templates." + template;
        }
    }
}
