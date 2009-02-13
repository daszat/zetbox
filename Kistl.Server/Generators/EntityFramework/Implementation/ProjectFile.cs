using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{
    public class ProjectFile
        : Templates.Implementation.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
        }

        protected override string GetAssemblyName()
        {
            return "Kistl.Objects." + TaskEnum.Server;
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // Entity Framework assemblies
            this.WriteLine(@"    <Reference Include=""System.Data"" />");
            this.WriteLine(@"    <Reference Include=""System.Data.DataSetExtensions"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");
            this.WriteLine(@"    <Reference Include=""System.Data.Entity"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // used all over the place
            this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            //// used for ObservableCollection?
            //this.WriteLine(@"    <Reference Include=""WindowsBase"">");
            //this.WriteLine(@"      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
            //this.WriteLine(@"    </Reference>");

            // EF Provider infrastructure
            this.WriteLine(@"    <Reference Include=""Kistl.DALProvider.EF, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(OutputPath)\Kistl.DALProvider.EF.dll</HintPath>");
            this.WriteLine(@"    </Reference>");

            // Server API
            this.WriteLine(@"    <Reference Include=""Kistl.API.Server, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(OutputPath)\Kistl.API.Server.dll</HintPath>");
            this.WriteLine(@"    </Reference>");
        }

        protected override void ApplyAdditionalItemGroups()
        {
            base.ApplyAdditionalItemGroups();

            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.csdl"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.ssdl"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.msl"" />");
            this.WriteLine(@"  </ItemGroup>");

        }

    }
}
