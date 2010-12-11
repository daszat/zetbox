
namespace Kistl.Generator
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

    public class Compiler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Generator");

        private readonly ILifetimeScope _container;
        private readonly IEnumerable<AbstractBaseGenerator> _generatorProviders;
        private readonly KistlConfig _config;

        public Compiler(ILifetimeScope container, IEnumerable<AbstractBaseGenerator> generatorProviders)
        {
            _container = container;
            _generatorProviders = generatorProviders;
            _config = _container.Resolve<KistlConfig>();
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
                var workingPath = _config.Server.CodeGenWorkingPath;
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
                CompileCodeOnStaThread(workingPath);
                ArchiveOldOutput();
                PublishOutput();
                Log.Info("Finished generating Code");
            }
        }

        private void CompileCodeOnStaThread(string workingPath)
        {
            Exception failed = null;
            bool success = false;
            var staThread = new Thread(() => { try { success = CompileCode(workingPath); } catch (Exception ex) { failed = ex; } });
            if (staThread.TrySetApartmentState(ApartmentState.STA))
            {
                Log.Info("Successfully set STA on compile thread");
            }
            else
            {
                Log.Warn("STA not set on compile thread");
            }
            staThread.Name = "Compile";
            staThread.Start();
            staThread.Join();

            if (failed != null)
            {
                throw new ApplicationException("Compilation failed", failed);
            }

            if (!success)
            {
                throw new ApplicationException("Compilation failed");
            }
        }

        private void GenerateTo(string workingPath)
        {
            Log.InfoFormat("Generating Code to [{0}]", workingPath);
            List<Exception> failed = new List<Exception>();
            // TODO: use TaskExecutor to optimally use multicores
            var threads = new List<Thread>();
            foreach (var gen in _generatorProviders)
            {
                // decouple from loop variable
                var generator = gen;
                var genThread = new Thread(() =>
                {
                    try
                    {
                        using (var innerContainer = _container.BeginLifetimeScope())
                        {
                            generator.Generate(innerContainer.Resolve<IKistlContext>(), workingPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(String.Format("Error generating [{0}]:", generator.Description), ex);
                        lock (failed)
                        {
                            failed.Add(ex);
                        }
                    }
                });
                genThread.Name = generator.BaseName;
                genThread.Start();
                threads.Add(genThread);

#if SERIALIZE_GENERATION_THREADS
                // serialize execution
                Log.Warn("Serializing generation threads.");
                genThread.Join();
#endif
            }

            foreach (var t in threads)
            {
                t.Join();
            }

            if (failed.Count > 0)
            {
                // TODO: Introduce own exception
                throw new ApplicationException(String.Format("Failed code generation in {0} threads", failed.Count), failed.First());
            }
        }

        private bool CompileCode(string workingPath)
        {
            string referencePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(Compiler).Assembly.Location), @".."));

            Log.DebugFormat("referencePath = [{0}]", referencePath);

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
                var result = true;
                foreach (var gens in _generatorProviders.GroupBy(k => k.CompileOrder).OrderBy(i => i.Key))
                {
                    foreach (var gen in gens)
                    {
                        result &= CompileSingle(referencePath, binPath, engine, gen);
                    }
                }

                return result;
            }
            finally
            {
                // close all logfiles
                engine.UnregisterAllLoggers();
            }
        }

        private static bool CompileSingle(string apiPath, string binPath, Engine engine, AbstractBaseGenerator gen)
        {
            try
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
                    defaultPropertyGroup.AddNewProperty("KistlAPIPath", apiPath, true);

                    Log.DebugFormat("Compiling");
                    if (!engine.BuildProject(proj))
                    {
                        throw new ApplicationException(String.Format("Failed to compile {0}", gen.Description));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Failed compiling " + gen.Description, ex);
                return false;
            }
        }

        private void PublishOutput()
        {
            var outputPath = _config.Server.CodeGenOutputPath;
            if (!String.IsNullOrEmpty(outputPath))
            {
                Log.InfoFormat("Publishing results to [{0}]", outputPath);
                if (Directory.Exists(outputPath))
                {
                    Directory.Delete(outputPath, true);
                }
                try
                {
                    Directory.Move(_config.Server.CodeGenWorkingPath, outputPath);
                }
                catch (IOException)
                {
                    Thread.Sleep(1000);
                    // try again, might be locked by something
                    Directory.Move(_config.Server.CodeGenWorkingPath, outputPath);
                }

                // Case #1382: Recompile to regenerate PDB's
                // CompileCode(outputPath);
            }
        }

        private void ArchiveOldOutput()
        {
            var outputPath = _config.Server.CodeGenOutputPath;
            if (String.IsNullOrEmpty(outputPath))
            {
                throw new ConfigurationException("CodeGenOutputPath is not defined in the current configuration file, but archival was requested: don't know what to archive.");
            }
            var archivePath = _config.Server.CodeGenArchivePath;
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
