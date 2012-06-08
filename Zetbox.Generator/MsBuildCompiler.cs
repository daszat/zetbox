
namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Microsoft.Build.BuildEngine;
    using Microsoft.Build.Framework;

    public class MsBuildCompiler : Compiler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Generator.Compiler.MsBuild");

        public MsBuildCompiler(ILifetimeScope container, IEnumerable<AbstractBaseGenerator> generatorProviders)
            : base(container, generatorProviders)
        {
        }

        protected override void RegisterConsoleLogger(Engine engine, string workingPath)
        {
            engine.RegisterLogger(new ConsoleLogger(LoggerVerbosity.Minimal));
            // TODO: implement FileLogger in mono, reenable this
            var logger = new FileLogger();
            logger.Parameters = String.Format(@"logfile={0}", Path.Combine(workingPath, "compile.log"));
            logger.Verbosity = LoggerVerbosity.Minimal; // Normal, Detailed;
            engine.RegisterLogger(logger);
        }

        protected override bool CompileSingle(Engine engine, AbstractBaseGenerator gen, string workingPath, string target)
        {
            try
            {
                using (log4net.NDC.Push("Compiling " + gen.Description))
                {
                    Log.DebugFormat("Loading MsBuild Project");
                    var proj = new Project(engine);
                    proj.Load(Helper.PathCombine(workingPath, gen.TargetNameSpace, gen.ProjectFileName));

                    Log.DebugFormat("Compiling");
                    if (engine.BuildProject(proj, target))
                    {
                        return true;
                    }
                    else
                    {
                        Log.ErrorFormat("Failed to compile {0}", gen.Description);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed compiling " + gen.Description, ex);
                return false;
            }
        }
    }
}
