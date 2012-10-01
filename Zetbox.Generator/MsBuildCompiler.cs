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
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Microsoft.Build.Evaluation;
    using Zetbox.API;
    using Microsoft.Build.Execution;

    public class MsBuildCompiler : Compiler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Generator.Compiler.MsBuild");

        public MsBuildCompiler(ILifetimeScope container, IEnumerable<AbstractBaseGenerator> generatorProviders)
            : base(container, generatorProviders)
        {
        }

        protected override bool CompileSingle(AbstractBaseGenerator gen, Dictionary<string, string> buildProps, string workingPath, string target)
        {
            try
            {
                using (log4net.NDC.Push("Compiling " + gen.Description))
                {
                    Log.DebugFormat("Loading MsBuild Project");
                    var projectFile = Helper.PathCombine(workingPath, gen.TargetNameSpace, gen.ProjectFileName);
                    var req = new BuildRequestData(
                        projectFile,
                        buildProps,
                        null,
                        new[] { target },
                        null);

                    Log.DebugFormat("Compiling");
                    var result = BuildManager.DefaultBuildManager.Build(new BuildParameters(), req);

                    if (result.OverallResult == BuildResultCode.Success)
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
