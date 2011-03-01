using System.Collections.Generic;
using Kistl.API.Server;


namespace Kistl.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst")]
    public partial class ProjectFile : Kistl.Generator.ResourceTemplate
    {
		protected Kistl.API.IKistlContext ctx;
		protected string projectGuid;
		protected List<string> fileNames;
		protected IEnumerable<ISchemaProvider> schemaProviders;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ProjectFile", ctx, projectGuid, fileNames, schemaProviders);
        }

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host)
        {
			this.ctx = ctx;
			this.projectGuid = projectGuid;
			this.fileNames = fileNames;
			this.schemaProviders = schemaProviders;

        }

        public override void Generate()
        {
#line 11 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\r\n");
this.WriteObjects("    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\r\n");
this.WriteObjects("    <ProductVersion>9.0.21022</ProductVersion>\r\n");
this.WriteObjects("    <SchemaVersion>2.0</SchemaVersion>\r\n");
this.WriteObjects("    <ProjectGuid>",  projectGuid , "</ProjectGuid>\r\n");
this.WriteObjects("    <OutputType>Library</OutputType>\r\n");
this.WriteObjects("    <RootNamespace>",  GetAssemblyName() , "</RootNamespace>\r\n");
this.WriteObjects("    <AssemblyName>",  GetAssemblyName() , "</AssemblyName>\r\n");
this.WriteObjects("    <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>\r\n");
this.WriteObjects("    <FileAlignment>512</FileAlignment>\r\n");
this.WriteObjects("    <SignAssembly>true</SignAssembly>\r\n");
this.WriteObjects("    <AssemblyOriginatorKeyFile>Kistl.Objects.snk</AssemblyOriginatorKeyFile>\r\n");
this.WriteObjects("    <!-- this is referenced by the generator to put the results in their right place -->\r\n");
this.WriteObjects("    <RelativeOutputPath>",  GetRelativeOutputPath() , "</RelativeOutputPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugSymbols>true</DebugSymbols>\r\n");
this.WriteObjects("    <DebugType>full</DebugType>\r\n");
this.WriteObjects("    <Optimize>false</Optimize>\r\n");
this.WriteObjects("    <DefineConstants>DEBUG;TRACE</DefineConstants>\r\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\r\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\r\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\Debug\\",  GetRelativeOutputPath() , ".Fallback\\</OutputPath>\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath() , ".Generated\\</OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath Condition=\" '$(KistlAPIPathOverride)' == '' \">..\\bin\\Debug\\</KistlAPIPath>\r\n");
this.WriteObjects("    <KistlAPIPath Condition=\" '$(KistlAPIPathOverride)' != '' \">$(KistlAPIPathOverride)</KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugType>pdbonly</DebugType>\r\n");
this.WriteObjects("    <Optimize>true</Optimize>\r\n");
this.WriteObjects("    <DefineConstants>TRACE</DefineConstants>\r\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\r\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\r\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\Release\\",  GetRelativeOutputPath() , ".Fallback\\</OutputPath>\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath() , ".Generated\\</OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath Condition=\" '$(KistlAPIPathOverride)' == '' \">..\\bin\\Release\\</KistlAPIPath>\r\n");
this.WriteObjects("    <KistlAPIPath Condition=\" '$(KistlAPIPathOverride)' != '' \">$(KistlAPIPathOverride)</KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Linux.Debug|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugSymbols>true</DebugSymbols>\r\n");
this.WriteObjects("    <DebugType>full</DebugType>\r\n");
this.WriteObjects("    <Optimize>false</Optimize>\r\n");
this.WriteObjects("    <DefineConstants>DEBUG;TRACE;MONO</DefineConstants>\r\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\r\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\r\n");
this.WriteObjects("    <!-- mono is quite more pedantic; ignore superfluous local vars and fields for now -->\r\n");
this.WriteObjects("    <NoWarn>1591,0168,0414,0219</NoWarn>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("    <!-- hardcode output path for external builds; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' == '' \">..\\bin\\Debug\\",  GetRelativeOutputPath() , ".Fallback\\</OutputPath>\r\n");
this.WriteObjects("    <OutputPath Condition=\" '$(OutputPathOverride)' != '' \">$(OutputPathOverride)\\",  GetRelativeOutputPath() , ".Generated\\</OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath Condition=\" '$(KistlAPIPathOverride)' == '' \">..\\bin\\Debug\\</KistlAPIPath>\r\n");
this.WriteObjects("    <KistlAPIPath Condition=\" '$(KistlAPIPathOverride)' != '' \">$(KistlAPIPathOverride)</KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <!-- additional properties have to come in a separate group to receive correct $(OutputPath) -->\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
#line 76 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalProperties(); 
#line 77 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <AdditionalReferencePath Include=\"$(OutputPath)\" />\r\n");
this.WriteObjects("    <Reference Include=\"System\" />\r\n");
this.WriteObjects("    <Reference Include=\"System.Core\">\r\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"System.Xml\" />\r\n");
this.WriteObjects("    <Reference Include=\"log4net\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(KistlAPIPath)\\Common\\Core\\log4net.dll</HintPath>\r\n");
this.WriteObjects("      <Private>False</Private>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"Kistl.API\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(KistlAPIPath)\\Common\\Core\\Kistl.API.dll</HintPath>\r\n");
this.WriteObjects("      <Private>False</Private>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"Autofac\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(KistlAPIPath)\\Common\\Core\\Autofac.dll</HintPath>\r\n");
this.WriteObjects("      <Private>False</Private>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"WindowsBase\">\r\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\r\n");
this.WriteObjects("    </Reference>\r\n");
#line 103 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalReferences(); 
#line 104 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
#line 106 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
foreach (var name in fileNames) { 
#line 107 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("    <Compile Include=\"",  name , "\" />\r\n");
#line 108 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
} 
#line 109 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <None Include=\"Kistl.Objects.snk\" />\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
#line 113 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalItemGroups(); 
#line 114 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\r\n");
this.WriteObjects("  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. \r\n");
this.WriteObjects("       Other similar extension points exist, see Microsoft.Common.targets.\r\n");
this.WriteObjects("  <Target Name=\"BeforeBuild\">\r\n");
this.WriteObjects("  </Target>\r\n");
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <Target Name=\"AfterBuild\">\r\n");
this.WriteObjects("  </Target>\r\n");
#line 122 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalPropertyGroups(); 
#line 123 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("</Project>\r\n");

        }

    }
}