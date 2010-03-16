
namespace Kistl.DalProvider.Frozen.Generator.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public class ProjectFile
        : Kistl.Server.Generators.Templates.Implementation.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
        }

        protected override string GetAssemblyName()
        {
            return "Kistl.Objects.Frozen";
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // used for indexing 
            this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // log4net
            this.WriteLine(@"    <Reference Include=""log4net, Version=1.2.10.0, Culture=neutral, PublicKeyToken=1b44e1d426115821, processorArchitecture=MSIL"">");
            this.WriteLine(@"        <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"        <HintPath>$(KistlAPIPath)\log4net.dll</HintPath>");
            this.WriteLine(@"    </Reference>");

            // Frozen Provider infrastructure
            this.WriteLine(@"    <Reference Include=""Kistl.DalProvider.Frozen"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Kistl.DalProvider.Frozen.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }
    }
}
