using System.Collections.Generic;
using Zetbox.API.Server;


namespace Zetbox.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst")]
    public partial class ProjectFile : Zetbox.Generator.ResourceTemplate
    {
		protected Zetbox.API.IZetboxContext ctx;
		protected string projectGuid;
		protected List<string> fileNames;
		protected IEnumerable<ISchemaProvider> schemaProviders;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ProjectFile", ctx, projectGuid, fileNames, schemaProviders);
        }

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host)
        {
			this.ctx = ctx;
			this.projectGuid = projectGuid;
			this.fileNames = fileNames;
			this.schemaProviders = schemaProviders;

        }

        public override void Generate()
        {
#line 27 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("<Project Sdk=\"Microsoft.NET.Sdk\">\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <TargetFramework>net5.0</TargetFramework>\r\n");
this.WriteObjects("    <RootNamespace>",  GetAssemblyName() , "</RootNamespace>\r\n");
this.WriteObjects("    <AssemblyName>",  GetAssemblyName() , "</AssemblyName>\r\n");
this.WriteObjects("	<AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>\r\n");
this.WriteObjects("    <AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>\r\n");
this.WriteObjects("    <SignAssembly>true</SignAssembly>\r\n");
this.WriteObjects("    <AssemblyOriginatorKeyFile>Zetbox.Objects.snk</AssemblyOriginatorKeyFile>\r\n");
this.WriteObjects("    <!-- this is referenced by the generator to put the results in their right place -->\r\n");
this.WriteObjects("    <RelativeOutputPath>",  GetRelativeOutputPath() , "</RelativeOutputPath>\r\n");
this.WriteObjects("    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\r\n");
this.WriteObjects("    <BuildDependsOn>$(BuildDependsOn);AfterBuildMigrated</BuildDependsOn>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugType>full</DebugType>\r\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\$(Configuration)\\",  GetRelativeOutputPath().Replace('/', '\\') , "\\",  RelativeExternBuildOutputSubDirectory , "\\</OutputPath>\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath().Replace('/', '\\') , "\\Generated\\</OutputPath>\r\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' == '' \">..\\bin\\$(Configuration)\\</ZetboxAPIPath>\r\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' != '' \">$(ZetboxAPIPathOverride)</ZetboxAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugType>pdbonly</DebugType>\r\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\$(Configuration)\\",  GetRelativeOutputPath().Replace('/', '\\') , "\\",  RelativeExternBuildOutputSubDirectory , "\\</OutputPath>\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath().Replace('/', '\\') , "\\Generated\\</OutputPath>\r\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' == '' \">..\\bin\\$(Configuration)\\</ZetboxAPIPath>\r\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' != '' \">$(ZetboxAPIPathOverride)</ZetboxAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <!-- additional properties have to come in a separate group to receive correct $(OutputPath) -->\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
#line 61 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalProperties(); 
#line 62 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <PackageReference Include=\"log4net\" Version=\"2.0.10\" />\r\n");
this.WriteObjects("    <PackageReference Include=\"Autofac\" Version=\"5.2.0\" />\r\n");
this.WriteObjects("    <Reference Include=\"Zetbox.API\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(ZetboxAPIPath)\\Common\\Zetbox.API.dll</HintPath>\r\n");
this.WriteObjects("      <Private>False</Private>\r\n");
this.WriteObjects("    </Reference>  \r\n");
#line 71 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalReferences(); 
#line 72 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <None Include=\"Zetbox.Objects.snk\" />\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
#line 76 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalItemGroups(); 
#line 77 "D:\Projects\zetbox.core\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("</Project>");

        }

    }
}