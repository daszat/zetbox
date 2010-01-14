using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Generators;

namespace Kistl.DalProvider.EF.Generator.Implementation
{
    public class ProjectFile
        : Kistl.Server.Generators.Templates.Implementation.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string projectGuid, List<string> fileNames)
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
            this.WriteLine(@"    <Reference Include=""Kistl.DalProvider.EF"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Kistl.DalProvider.EF.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");


            // Server API
            this.WriteLine(@"    <Reference Include=""Kistl.API.Server"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Kistl.API.Server.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
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
