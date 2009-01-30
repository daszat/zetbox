using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{
    public class ProjectFile
        : Kistl.Server.Generators.Templates.Implementation.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames)
            : base(_host, ctx, projectGuid, fileNames)
        {
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

            // used for XMLSerialization annotations
            this.WriteLine(@"    <Reference Include=""System.Xml"" />");

            // EF Provider infrastructure
            this.WriteLine(@"    <ProjectReference Include=""..\Kistl.DALProvider.EF\Kistl.DALProvider.EF.csproj"">");
            this.WriteLine(@"      <Project>{52EC8DFB-9C75-4FDB-9EE1-E78847F7F711}</Project>");
            this.WriteLine(@"      <Name>Kistl.DALProvider.EF</Name>");
            this.WriteLine(@"    </ProjectReference>");

            // Server API
            this.WriteLine(@"    <ProjectReference Include=""..\Kistl.API.Server\Kistl.API.Server.csproj"">");
            this.WriteLine(@"      <Project>{08902397-B9CA-43DA-8C8D-27DCEC097611}</Project>");
            this.WriteLine(@"      <Name>Kistl.API.Server</Name>");
            this.WriteLine(@"    </ProjectReference>");
        }

        protected override void ApplyAdditionalItemGroups()
        {
            base.ApplyAdditionalItemGroups();

            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.csdl"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.ssdl"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""Model.msl"" />");
            this.WriteLine(@"  </ItemGroup>");

            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <Compile Include=""FrozenContext.cs"" />");
            this.WriteLine(@"    <Compile Include=""CollectionEntries.cs"" />");
            this.WriteLine(@"    <Compile Include=""AssociationSetAttributes.cs"" />");
            this.WriteLine(@"  </ItemGroup>");

        }

    }
}
