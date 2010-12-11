using System.Collections.Generic;
using Kistl.API.Server;


namespace Kistl.Generator.ClickOnce.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst")]
    public partial class ProjectFile : Kistl.Generator.ResourceTemplate
    {
		protected Kistl.API.IKistlContext ctx;
		protected string projectGuid;
		protected List<string> fileNames;
		protected IEnumerable<ISchemaProvider> schemaProviders;


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
#line 11 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
this.WriteObjects("<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\r\n");
this.WriteObjects("    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\r\n");
this.WriteObjects("    <ProductVersion>9.0.21022</ProductVersion>\r\n");
this.WriteObjects("    <SchemaVersion>2.0</SchemaVersion>\r\n");
this.WriteObjects("    <ProjectGuid>",  projectGuid , "</ProjectGuid>\r\n");
this.WriteObjects("    <OutputType>WinExe</OutputType>\r\n");
this.WriteObjects("    <RootNamespace>",  GetAssemblyName() , "</RootNamespace>\r\n");
this.WriteObjects("    <AssemblyName>",  GetAssemblyName() , "</AssemblyName>\r\n");
this.WriteObjects("    <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>\r\n");
this.WriteObjects("    <FileAlignment>512</FileAlignment>\r\n");
this.WriteObjects("    <SignAssembly>true</SignAssembly>\r\n");
this.WriteObjects("    <AssemblyOriginatorKeyFile>Kistl.Objects.snk</AssemblyOriginatorKeyFile>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("	<IsWebBootstrapper>true</IsWebBootstrapper>\r\n");
this.WriteObjects("    <PublishUrl>",  GetPublishUrl() , "</PublishUrl>\r\n");
this.WriteObjects("    <Install>true</Install>\r\n");
this.WriteObjects("    <InstallFrom>Web</InstallFrom>\r\n");
this.WriteObjects("    <UpdateEnabled>true</UpdateEnabled>\r\n");
this.WriteObjects("    <UpdateMode>Foreground</UpdateMode>\r\n");
this.WriteObjects("    <UpdateInterval>7</UpdateInterval>\r\n");
this.WriteObjects("    <UpdateIntervalUnits>Days</UpdateIntervalUnits>\r\n");
this.WriteObjects("    <UpdatePeriodically>false</UpdatePeriodically>\r\n");
this.WriteObjects("    <UpdateRequired>false</UpdateRequired>\r\n");
this.WriteObjects("    <MapFileExtensions>true</MapFileExtensions>\r\n");
this.WriteObjects("    <InstallUrl>",  GetInstallUrl() , "</InstallUrl>\r\n");
this.WriteObjects("    <ApplicationRevision>17</ApplicationRevision>\r\n");
this.WriteObjects("    <ApplicationVersion>1.0.0.%2a</ApplicationVersion>\r\n");
this.WriteObjects("    <UseApplicationTrust>false</UseApplicationTrust>\r\n");
this.WriteObjects("    <PublishWizardCompleted>true</PublishWizardCompleted>\r\n");
this.WriteObjects("    <BootstrapperEnabled>true</BootstrapperEnabled>\r\n");
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
this.WriteObjects("    <OutputPath></OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath></KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n");
this.WriteObjects("    <DebugType>pdbonly</DebugType>\r\n");
this.WriteObjects("    <Optimize>true</Optimize>\r\n");
this.WriteObjects("    <DefineConstants>TRACE</DefineConstants>\r\n");
this.WriteObjects("    <ErrorReport>prompt</ErrorReport>\r\n");
this.WriteObjects("    <WarningLevel>4</WarningLevel>\r\n");
this.WriteObjects("    <NoWarn>1591</NoWarn>\r\n");
this.WriteObjects("    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>\r\n");
this.WriteObjects("    <OutputPath></OutputPath>\r\n");
this.WriteObjects("    <KistlAPIPath></KistlAPIPath>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <!-- Has to come in a separate group to receive correct $(OutputPath) -->\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- Signing -->\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <ManifestCertificateThumbprint>C42C4602D97EFDCFFAB7AF870C9DBE5FE87A5ECF</ManifestCertificateThumbprint>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <ManifestKeyFile>ClickOnceTest_TemporaryKey.pfx</ManifestKeyFile>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <GenerateManifests>true</GenerateManifests>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
this.WriteObjects("    <SignManifests>true</SignManifests>\r\n");
this.WriteObjects("  </PropertyGroup>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <PropertyGroup>\r\n");
#line 86 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
ApplyAdditionalProperties();

#line 88 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
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
this.WriteObjects("	<Reference Include=\"WindowsBase\">\r\n");
this.WriteObjects("      <RequiredTargetFramework>3.5</RequiredTargetFramework>\r\n");
this.WriteObjects("    </Reference>\r\n");
this.WriteObjects("    <Reference Include=\"PresentationCore\" />\r\n");
this.WriteObjects("    <Reference Include=\"PresentationFramework\" />\r\n");
#line 107 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
ApplyAdditionalReferences();

#line 109 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
#line 112 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
foreach (var name in fileNames)
	{

#line 115 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
this.WriteObjects("    <Compile Include=\"",  name , "\" />\r\n");
#line 117 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
}

#line 119 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <None Include=\"Kistl.Objects.snk\" />\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("  <!-- The Application --> \r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <Compile Include=\"Program.cs\" />\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <Content Include=\"DefaultConfig.xml\">\r\n");
this.WriteObjects("      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n");
this.WriteObjects("      <SubType>Designer</SubType>\r\n");
this.WriteObjects("    </Content>\r\n");
this.WriteObjects("    <Content Include=\"ZBox\\Client\\Kistl.Client.WPF.exe.config\">\r\n");
this.WriteObjects("      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n");
this.WriteObjects("    </Content>\r\n");
this.WriteObjects("    <None Include=\"ClickOnceTest_TemporaryKey.pfx\" />\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("  <ItemGroup>\r\n");
this.WriteObjects("    <BootstrapperPackage Include=\"Microsoft.Net.Client.3.5\">\r\n");
this.WriteObjects("      <Visible>False</Visible>\r\n");
this.WriteObjects("      <ProductName>.NET Framework 3.5 SP1 Client Profile</ProductName>\r\n");
this.WriteObjects("      <Install>false</Install>\r\n");
this.WriteObjects("    </BootstrapperPackage>\r\n");
this.WriteObjects("    <BootstrapperPackage Include=\"Microsoft.Net.Framework.3.5.SP1\">\r\n");
this.WriteObjects("      <Visible>False</Visible>\r\n");
this.WriteObjects("      <ProductName>.NET Framework 3.5 SP1</ProductName>\r\n");
this.WriteObjects("      <Install>false</Install>\r\n");
this.WriteObjects("    </BootstrapperPackage>\r\n");
this.WriteObjects("    <BootstrapperPackage Include=\"Microsoft.Windows.Installer.3.1\">\r\n");
this.WriteObjects("      <Visible>False</Visible>\r\n");
this.WriteObjects("      <ProductName>Windows Installer 3.1</ProductName>\r\n");
this.WriteObjects("      <Install>true</Install>\r\n");
this.WriteObjects("    </BootstrapperPackage>\r\n");
this.WriteObjects("  </ItemGroup>\r\n");
this.WriteObjects("\r\n");
#line 157 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
ApplyAdditionalItemGroups();

#line 159 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
this.WriteObjects("  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\r\n");
this.WriteObjects("  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. \r\n");
this.WriteObjects("       Other similar extension points exist, see Microsoft.Common.targets.\r\n");
this.WriteObjects("  <Target Name=\"BeforeBuild\">\r\n");
this.WriteObjects("  </Target>\r\n");
this.WriteObjects("  -->\r\n");
this.WriteObjects("  <Target Name=\"AfterBuild\">\r\n");
this.WriteObjects("  </Target>\r\n");
this.WriteObjects("  ");
#line 168 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
ApplyAdditionalPropertyGroups();
  
#line 170 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\ProjectFile.cst"
this.WriteObjects("</Project>\r\n");

        }



    }
}