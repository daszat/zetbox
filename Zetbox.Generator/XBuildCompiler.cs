// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    
    public class XBuildCompiler : Compiler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(XBuildCompiler));

        public XBuildCompiler(ILifetimeScope container, IEnumerable<AbstractBaseGenerator> generatorProviders)
            : base(container, generatorProviders)
        {
        }

        protected override bool CompileSingle(AbstractBaseGenerator gen, Dictionary<string, string> buildProps, string workingPath, string target)
        {
            try
            {
                using (log4net.NDC.Push("Compiling " + gen.Description))
                {
                    var props = String.Join(";", buildProps.Select(prop => String.Format("{0}={1}", prop.Key, prop.Value)).ToArray());
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
