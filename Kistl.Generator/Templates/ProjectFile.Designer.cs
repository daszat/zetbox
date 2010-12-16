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
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugSymbols>true</DebugSymbols>\r\n");
this.WriteObjects("    <DebugType>full</DebugType>\r\n");
this.WriteObjects("    <Optimize>false</Optimize>\r\n");
this.WriteObjects("    <DefineConstants>DEBUG;TRACE</DefineConstants>\r\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\r\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\r\n");
this.WriteObjects("    <NoWarn>1591</NoWarn>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("	<!-- hardcode output path for the visual studio build; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath>..\\bin\\Debug\\bin\\",  GetRelativeOutputPath() , "</OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath>..\\bin\\Debug\\bin\\</KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugType>pdbonly</DebugType>\r\n");
this.WriteObjects("    <Optimize>true</Optimize>\r\n");
this.WriteObjects("    <DefineConstants>TRACE</DefineConstants>\r\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\r\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\r\n");
this.WriteObjects("    <NoWarn>1591</NoWarn>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("	<!-- hardcode output path for the visual studio build; the generator replaces this value from configuration -->\r\n");
this.WriteObjects("    <OutputPath>..\\bin\\Release\\bin\\",  GetRelativeOutputPath() , "</OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath>..\\bin\\Release\\bin\\</KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <!-- Has to come in a separate group to receive correct $(OutputPath) -->\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
#line 55 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalProperties();

#line 57 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <AdditionalReferencePath Include=\"$(OutputPath)\" />\r\n");
this.WriteObjects("    <Reference Include=\"System\" />\r\n");
this.WriteObjects("    <Reference Include=\"System.Core\">\r\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"System.Xml\" />\r\n");
this.WriteObjects("    <Reference Include=\"log4net, Version=1.2.10.0, Culture=neutral, PublicKeyToken=1b44e1d426115821, processorArchitecture=MSIL\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(KistlAPIPath)\\Common\\log4net.dll</HintPath>\r\n");
this.WriteObjects("      <Private>False</Private>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"Kistl.API, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(KistlAPIPath)\\Common\\Kistl.API.dll</HintPath>\r\n");
this.WriteObjects("      <Private>False</Private>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"Autofac\">\r\n");
this.WriteObjects("      <SpecificVersion>False</SpecificVersion>\r\n");
this.WriteObjects("      <HintPath>$(KistlAPIPath)\\Common\\Autofac.dll</HintPath>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("	<Reference Include=\"WindowsBase\">\r\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\r\n");
this.WriteObjects("    </Reference>\r\n");
#line 83 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalReferences();

#line 85 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
#line 88 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
foreach (var name in fileNames)
	{

#line 91 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("    <Compile Include=\"",  name , "\" />\r\n");
#line 93 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
}

#line 95 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <None Include=\"Kistl.Objects.snk\" />\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
#line 100 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalItemGroups();

#line 102 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\r\n");
this.WriteObjects("  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. \r\n");
this.WriteObjects("       Other similar extension points exist, see Microsoft.Common.targets.\r\n");
this.WriteObjects("  <Target Name=\"BeforeBuild\">\r\n");
this.WriteObjects("  </Target>\r\n");
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <Target Name=\"AfterBuild\">\r\n");
this.WriteObjects("  </Target>\r\n");
this.WriteObjects("  ");
#line 111 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
ApplyAdditionalPropertyGroups();
  
#line 113 "P:\Kistl\Kistl.Generator\Templates\ProjectFile.cst"
this.WriteObjects("</Project>\r\n");

        }

    }
}