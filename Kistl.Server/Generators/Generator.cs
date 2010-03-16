
namespace Kistl.Server.Generators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    using Microsoft.Build.BuildEngine;
    using Microsoft.Build.Framework;

    public class Generator
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Generator");
        private readonly IContainer _container;
        private readonly IEnumerable<BaseDataObjectGenerator> _generatorProviders;

        public Generator(IContainer container, IEnumerable<BaseDataObjectGenerator> generatorProviders)
        {
            _container = container;
            _generatorProviders = generatorProviders;
        }

        public void GenerateCode()
        {
            if (_generatorProviders.Count() == 0)
            {
                Log.Warn("No BaseDataObjectGenerators found. Exiting the Generator.");
                return;
            }

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

                GenerateTo(workingPath);
                GenerateCodeOnStaThread(workingPath);
                ArchiveOldOutput();
                PublishOutput();
                Log.Info("Finished generating Code");
            }
        }

        private void GenerateCodeOnStaThread(string workingPath)
        {
            var staThread = new Thread(() => CompileCode(workingPath));
            if (staThread.TrySetApartmentState(ApartmentState.STA))
            {
                Log.Info("Successfully set STA on compile thread");
            }
            else
            {
                Log.Info("Successfully set STA on compile thread");
            }
            staThread.Name = "Compile";
            staThread.Start();
            staThread.Join();
        }

        private void GenerateTo(string workingPath)
        {
            Log.InfoFormat("Generating Code to [{0}]", workingPath);
            var threads = new List<Thread>();
            foreach (var gen in _generatorProviders)
            {
                // decouple from loop variable
                var generator = gen;
                var genThread = new Thread(() =>
                {
                    using (var innerContainer = _container.CreateInnerContainer())
                    {
                        generator.Generate(innerContainer.Resolve<IKistlContext>(), workingPath);
                    }
                });
                genThread.Name = gen.BaseName;
                genThread.Start();
                threads.Add(genThread);
            }
            foreach (var t in threads)
            {
                t.Join();
            }
        }

        private void CompileCode(string workingPath)
        {
            string serverReferencePath = Path.GetDirectoryName(typeof(Generator).Assembly.Location);
            string clientReferencePath = Path.GetFullPath(Path.Combine(serverReferencePath, @"..\Client"));

            Log.DebugFormat("serverReferencePath = [{0}]", serverReferencePath);
            Log.DebugFormat("clientReferencePath = [{0}]", clientReferencePath);

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
                foreach (var gen in _generatorProviders)
                {
                    using (log4net.NDC.Push("Compiling " + gen.Description))
                    {
                        Log.DebugFormat("Loading MsBuild Project");
                        var proj = new Project(engine);
                        proj.Load(gen.ProjectFileName);
                        var defaultPropertyGroup = proj.AddNewPropertyGroup(false);
                        defaultPropertyGroup.AddNewProperty("OutputPath", binPath, true);
#if DEBUG
                        defaultPropertyGroup.AddNewProperty("Configuration", "Debug", true);
#else
                        defaultPropertyGroup.AddNewProperty("Configuration", "Release", true);
#endif
                        // Fix XML Path
                        defaultPropertyGroup.AddNewProperty("DocumentationFile", "$(OutputPath)\\$(AssemblyName).xml", false);
                        if (gen.BaseName == "Client")
                        {
                            defaultPropertyGroup.AddNewProperty("KistlAPIPath", clientReferencePath, true);
                        }
                        else
                        {
                            defaultPropertyGroup.AddNewProperty("KistlAPIPath", serverReferencePath, true);
                        }

                        Log.DebugFormat("Compiling");
                        if (!engine.BuildProject(proj))
                        {
                            throw new ApplicationException(String.Format("Failed to compile {0}", gen.Description));
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

        private void PublishOutput()
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
                // Case #1382: Recompile to regenerate PDB's
                // CompileCode(outputPath);
            }
        }

        private void ArchiveOldOutput()
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
                    Log.InfoFormat("No old results found in [{0}]", outputPath);
                }
            }
        }

        internal static TemplateGenerator GetTemplateGenerator(
            string providerTemplateNamespace,
            string providerTemplateAssembly,
            string template, string output, string targetdir, params object[] templateParameter)
        {
            var gen = new TemplateGenerator();

            gen.Settings.Add("basetemplatepath", "Kistl.Server.Generators.Templates");
            gen.Settings.Add("providertemplatenamespace", providerTemplateNamespace);
            gen.Settings.Add("providertemplateassembly", providerTemplateAssembly);

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

        public static IQueryable<CompoundObject> GetCompoundObjectList(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return from s in ctx.GetQuery<CompoundObject>()
                   select s;
        }

        #endregion
    }


}
