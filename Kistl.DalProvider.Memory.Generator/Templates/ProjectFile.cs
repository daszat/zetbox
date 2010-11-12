
namespace Kistl.DalProvider.Memory.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Templates = Kistl.Generator.Templates;

    public class ProjectFile
        : Templates.ProjectFile
    {

        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
            : base(_host, ctx, projectGuid, fileNames, schemaProviders)
        {
        }

        protected override void ApplyAdditionalReferences()
        {
            base.ApplyAdditionalReferences();

            // used all over the place
            this.WriteLine(@"    <Reference Include=""System.Data.Linq"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // used for INotifyCollectionChanged
            this.WriteLine(@"    <Reference Include=""WindowsBase"">");
            this.WriteLine(@"      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
            this.WriteLine(@"    </Reference>");

            // DAL Provider
            this.WriteLine(@"    <Reference Include=""Kistl.DalProvider.Memory"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(KistlAPIPath)\Common\Kistl.DalProvider.Memory.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }

        protected override void ApplyAdditionalItemGroups()
        {
            base.ApplyAdditionalItemGroups();
            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <EmbeddedResource Include=""FrozenObjects.xml""/>");
            this.WriteLine(@"  </ItemGroup>");
        }
    }
}
