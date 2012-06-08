
namespace Kistl.DalProvider.Ef.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.Generator;

    public class ProjectFile
        : Kistl.Generator.Templates.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host, ctx, projectGuid, fileNames, schemaProviders)
        {
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // Entity Framework assemblies
            this.WriteLine(@"    <Reference Include=""System.Data"" />");
            this.WriteLine(@"    <Reference Include=""System.Data.Entity"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            //// used all over the place
            //this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            //this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            //this.WriteLine(@"    </Reference>");

            //// used for ObservableCollection?
            //this.WriteLine(@"    <Reference Include=""WindowsBase"">");
            //this.WriteLine(@"      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
            //this.WriteLine(@"    </Reference>");

            // EF Provider infrastructure
            this.WriteLine(@"    <Reference Include=""Kistl.DalProvider.Ef"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Server\EF\Kistl.DalProvider.Ef.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // Server API
            this.WriteLine(@"    <Reference Include=""Kistl.API.Server"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Server\Core\Kistl.API.Server.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }

        protected override void ApplyAdditionalItemGroups()
        {
            base.ApplyAdditionalItemGroups();

            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.csdl"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.msl"" />");
            foreach (var provider in schemaProviders.Where(sp => sp.IsStorageProvider))
            {
                this.WriteLine(@"    <EmbeddedResource Include=""Model.{0}.ssdl"" />", provider.ConfigName);
            }
            // hardcoded views for mssql
            //this.WriteLine(@"    <Compile Include=""Model.MSSQLViews.cs"" />");
            this.WriteLine(@"  </ItemGroup>");

        }

        protected override void ApplyAdditionalPropertyGroups()
        {
            base.ApplyAdditionalPropertyGroups();

            this.WriteLine(@"  <PropertyGroup>");
            // can only generate views for (globally-)registered data providers
            //this.WriteLine(@"    <PreBuildEvent>""%25windir%25\Microsoft.NET\Framework\v3.5\EdmGen.exe"" /nologo /language:CSharp /mode:ViewGeneration ""/inssdl:$(ProjectDir)Model.MSSQL.ssdl"" ""/incsdl:$(ProjectDir)Model.csdl"" ""/inmsl:$(ProjectDir)Model.msl"" ""/outviews:$(ProjectDir)Model.MSSQLViews.cs""</PreBuildEvent>");
            this.WriteLine(@"  </PropertyGroup>");
        }

        protected override string GetRelativeOutputPath()
        {
            return @"Server\EF";
        }
    }
}
