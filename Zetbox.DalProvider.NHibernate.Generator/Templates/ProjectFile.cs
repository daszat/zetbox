
namespace Zetbox.DalProvider.NHibernate.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Templates = Zetbox.Generator.Templates;

    public class ProjectFile
        : Templates.ProjectFile
    {
        public ProjectFile(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, string projectGuid, List<string> fileNames, IEnumerable<ISchemaProvider> schemaProviders)
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

            // Server API
            this.WriteLine(@"    <Reference Include=""Zetbox.API.Server"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Server\Core\Zetbox.API.Server.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // DAL Provider
            this.WriteLine(@"    <Reference Include=""Zetbox.DalProvider.NHibernate"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Server\NH\Zetbox.DalProvider.NHibernate.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // NHibernate
            this.WriteLine(@"    <Reference Include=""NHibernate"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Server\NH\NHibernate.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");

            // Base Provider
            this.WriteLine(@"    <Reference Include=""Zetbox.DalProvider.Base"">");
            this.WriteLine(@"      <SpecificVersion>False</SpecificVersion>");
            this.WriteLine(@"      <HintPath>$(ZetboxAPIPath)\Common\Core\Zetbox.DalProvider.Base.dll</HintPath>");
            this.WriteLine(@"      <Private>False</Private>");
            this.WriteLine(@"    </Reference>");
        }

        protected override void ApplyAdditionalItemGroups()
        {
            base.ApplyAdditionalItemGroups();
            this.WriteLine(@"  <ItemGroup>");
            this.WriteLine(@"    <EmbeddedResource Include=""*.hbm.xml"" />");
            this.WriteLine(@"    <EmbeddedResource Include=""*/*.hbm.xml"" />");
            this.WriteLine(@"  </ItemGroup>");
        }

        protected override string GetRelativeOutputPath()
        {
            return @"Server\NH";
        }
    }
}
