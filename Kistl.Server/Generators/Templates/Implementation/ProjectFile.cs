using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation
{
    public class ProjectFile
        : Templates.Interface.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // local project references
            this.WriteLine(@"    <Reference Include=""Kistl.Objects, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(OutputPath)\Kistl.Objects.dll</HintPath>");
            this.WriteLine(@"    </Reference>");

        }

    }
}
