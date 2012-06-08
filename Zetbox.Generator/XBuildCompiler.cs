
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Microsoft.Build.BuildEngine;
    using Microsoft.Build.Framework;
    
    public class XBuildCompiler : Compiler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Generator.Compiler.XBuild");

        public XBuildCompiler(ILifetimeScope container, IEnumerable<AbstractBaseGenerator> generatorProviders)
            : base(container, generatorProviders)
        {
        }

        protected override void RegisterConsoleLogger(Engine engine, string workingPath)
        {
            engine.RegisterLogger(new ConsoleLogger(LoggerVerbosity.Normal));
        }

        protected override bool CompileSingle(Engine engine, AbstractBaseGenerator gen, string workingPath, string target)
        {
            try
            {
                using (log4net.NDC.Push("Compiling " + gen.Description))
                {
                    var props = String.Join(";", engine.GlobalProperties.OfType<BuildProperty>().Select(prop => String.Format("{0}={1}", prop.Name, prop.Value)).ToArray());
                    var args = String.Format("\"/p:{0}\" {1}", props, Helper.PathCombine(workingPath, gen.TargetNameSpace, gen.ProjectFileName));

                    var pi = new ProcessStartInfo("xbuild", args);
                    pi.UseShellExecute = false;
                    pi.RedirectStandardOutput = true;
                    pi.RedirectStandardError = true;
                    pi.ErrorDialog = false;
                    pi.CreateNoWindow = true;

                    Log.InfoFormat("Calling xbuild with arguments [{0}]", args);
                    var p = Process.Start(pi);
                    p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                            Log.Error(e.Data);
                    };
                    p.BeginErrorReadLine();

                    p.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                            Log.Info(e.Data);
                    };
                    p.BeginOutputReadLine();

                    if (!p.WaitForExit(100 * 1000))
                    {
                        p.Kill();
                        throw new InvalidOperationException(String.Format("xbuild did not complete within 100 seconds"));
                    }

                    return p.ExitCode == 0;
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
