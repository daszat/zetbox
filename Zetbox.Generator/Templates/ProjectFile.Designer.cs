using System.Collections.Generic;
using Zetbox.API.Server;


namespace Zetbox.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst")]
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("\n");
#line 28 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
this.WriteObjects("<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n");
this.WriteObjects("  <PropertyGroup>\n");
this.WriteObjects("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\n");
this.WriteObjects("    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\n");
this.WriteObjects("    <ProductVersion>9.0.21022</ProductVersion>\n");
this.WriteObjects("    <SchemaVersion>2.0</SchemaVersion>\n");
this.WriteObjects("    <ProjectGuid>",  projectGuid , "</ProjectGuid>\n");
this.WriteObjects("    <OutputType>Library</OutputType>\n");
this.WriteObjects("    <RootNamespace>",  GetAssemblyName() , "</RootNamespace>\n");
this.WriteObjects("    <AssemblyName>",  GetAssemblyName() , "</AssemblyName>\n");
this.WriteObjects("    <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>\n");
this.WriteObjects("    <FileAlignment>512</FileAlignment>\n");
this.WriteObjects("    <SignAssembly>true</SignAssembly>\n");
this.WriteObjects("    <AssemblyOriginatorKeyFile>Zetbox.Objects.snk</AssemblyOriginatorKeyFile>\n");
this.WriteObjects("    <!-- this is referenced by the generator to put the results in their right place -->\n");
this.WriteObjects("    <RelativeOutputPath>",  GetRelativeOutputPath() , "</RelativeOutputPath>\n");
this.WriteObjects("  </PropertyGroup>\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\n");
this.WriteObjects("    <DebugSymbols>true</DebugSymbols>\n");
this.WriteObjects("    <DebugType>full</DebugType>\n");
this.WriteObjects("    <Optimize>false</Optimize>\n");
this.WriteObjects("    <DefineConstants>DEBUG;TRACE</DefineConstants>\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\Debug\\",  GetRelativeOutputPath() , ".Fallback\\</OutputPath>\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath() , ".Generated\\</OutputPath>\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' == '' \">..\\bin\\Debug\\</ZetboxAPIPath>\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' != '' \">$(ZetboxAPIPathOverride)</ZetboxAPIPath>\n");
this.WriteObjects("  </PropertyGroup>\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\n");
this.WriteObjects("    <DebugType>pdbonly</DebugType>\n");
this.WriteObjects("    <Optimize>true</Optimize>\n");
this.WriteObjects("    <DefineConstants>TRACE</DefineConstants>\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\Release\\",  GetRelativeOutputPath() , ".Fallback\\</OutputPath>\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath() , ".Generated\\</OutputPath>\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' == '' \">..\\bin\\Release\\</ZetboxAPIPath>\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' != '' \">$(ZetboxAPIPathOverride)</ZetboxAPIPath>\n");
this.WriteObjects("  </PropertyGroup>\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Linux.Debug|AnyCPU' \">\n");
this.WriteObjects("    <DebugSymbols>true</DebugSymbols>\n");
this.WriteObjects("    <DebugType>full</DebugType>\n");
this.WriteObjects("    <Optimize>false</Optimize>\n");
this.WriteObjects("    <DefineConstants>DEBUG;TRACE;MONO</DefineConstants>\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\n");
this.WriteObjects("    <!-- mono is quite more pedantic; ignore superfluous local vars and fields for now -->\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\Debug\\",  GetRelativeOutputPath() , ".Fallback\\</OutputPath>\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath() , ".Generated\\</OutputPath>\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' == '' \">..\\bin\\Debug\\</ZetboxAPIPath>\n");
this.WriteObjects("    <ZetboxAPIPath Condition=\" '$(ZetboxAPIPathOverride)' != '' \">$(ZetboxAPIPathOverride)</ZetboxAPIPath>\n");
this.WriteObjects("  </PropertyGroup>\n");
this.WriteObjects("  <!-- additional properties have to come in a separate group to receive correct $(OutputPath) -->\n");
this.WriteObjects("  <PropertyGroup>\n");
#line 93 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalProperties(); 
#line 94 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </PropertyGroup>\n");
this.WriteObjects("  <ItemGroup>\n");
this.WriteObjects("    <AdditionalReferencePath Include=\"$(OutputPath)\" />\n");
this.WriteObjects("    <Reference Include=\"System\" />\n");
this.WriteObjects("    <Reference Include=\"System.Core\">\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\n");
this.WriteObjects("    </Reference>\n");
this.WriteObjects("    <Reference Include=\"System.Xml\" />\n");
this.WriteObjects("    <Reference Include=\"log4net\">\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\n");
this.WriteObjects("      <HintPath>$(ZetboxAPIPath)\\Common\\Core\\log4net.dll</HintPath>\n");
this.WriteObjects("      <Private>False</Private>\n");
this.WriteObjects("    </Reference>\n");
this.WriteObjects("    <Reference Include=\"Zetbox.API\">\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\n");
this.WriteObjects("      <HintPath>$(ZetboxAPIPath)\\Common\\Core\\Zetbox.API.dll</HintPath>\n");
this.WriteObjects("      <Private>False</Private>\n");
this.WriteObjects("    </Reference>\n");
this.WriteObjects("    <Reference Include=\"Autofac\">\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\n");
this.WriteObjects("      <HintPath>$(ZetboxAPIPath)\\Common\\Core\\Autofac.dll</HintPath>\n");
this.WriteObjects("      <Private>False</Private>\n");
this.WriteObjects("    </Reference>\n");
this.WriteObjects("    <Reference Include=\"WindowsBase\">\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\n");
this.WriteObjects("    </Reference>\n");
#line 120 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalReferences(); 
#line 121 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\n");
this.WriteObjects("  <ItemGroup>\n");
#line 123 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
foreach (var name in fileNames) { 
#line 124 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("    <Compile Include=\"",  name , "\" />\n");
#line 125 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
} 
#line 126 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\n");
this.WriteObjects("  <ItemGroup>\n");
this.WriteObjects("    <None Include=\"Zetbox.Objects.snk\" />\n");
this.WriteObjects("  </ItemGroup>\n");
#line 130 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalItemGroups(); 
#line 131 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\n");
this.WriteObjects("  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. \n");
this.WriteObjects("       Other similar extension points exist, see Microsoft.Common.targets.\n");
this.WriteObjects("  <Target Name=\"BeforeBuild\">\n");
this.WriteObjects("  </Target>\n");
this.WriteObjects("  -->\n");
this.WriteObjects("  <Target Name=\"AfterBuild\">\n");
this.WriteObjects("  </Target>\n");
#line 139 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
ApplyAdditionalPropertyGroups(); 
#line 140 "P:\zetbox\Zetbox.Generator\Templates\ProjectFile.cst"
this.WriteObjects("</Project>\n");

        }

    }
}