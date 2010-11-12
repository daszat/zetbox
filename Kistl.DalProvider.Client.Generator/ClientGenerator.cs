
namespace Kistl.DalProvider.Client.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.Generator;

    public class ClientGenerator
        : AbstractBaseGenerator
    {
        public ClientGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string ExtraSuffix { get { return "Client"; } }
        public override string Description { get { return "Client"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Client"; } }
        public override string BaseName { get { return "Client"; } }
        public override string ProjectGuid { get { return "{80F37FB5-66C6-45F2-9E2A-F787B141D66C}"; } }
        public override IEnumerable<string> RequiredNamespaces
        {
            get { return new[] { "Kistl.API.Client", "Kistl.DalProvider.Client" }; }
        }
    }
}
