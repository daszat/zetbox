using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.ClientObjects.Implementation
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
            return "Kistl.Objects." + TaskEnum.Client;
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // used all over the place
            this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // used for ObservableCollection?
            this.WriteLine(@"    <Reference Include=""WindowsBase"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // used for XMLSerialization annotations
            this.WriteLine(@"    <Reference Include=""System.Xml"" />");

            // Client API
            this.WriteLine(@"    <ProjectReference Include=""$(SourcePath)\Kistl.API.Client\Kistl.API.Client.csproj"">");
            this.WriteLine(@"      <Project>{08902397-B9CA-43DA-8C8D-27DCEC097611}</Project>");
            this.WriteLine(@"      <Name>Kistl.API.Client</Name>");
            this.WriteLine(@"    </ProjectReference>");
        }

    }
}
