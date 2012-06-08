
namespace Zetbox.DalProvider.Client.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.Generator;

    public class ClientGenerator
        : AbstractBaseGenerator
    {
        public ClientGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string ExtraSuffix { get { return "Client"; } }
        public override string Description { get { return "Client"; } }
        public override string TargetNameSpace { get { return "Zetbox.Objects.Client"; } }
        public override string BaseName { get { return "Client"; } }
        public override string ProjectGuid { get { return "{80F37FB5-66C6-45F2-9E2A-F787B141D66C}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        public override IEnumerable<string> RequiredNamespaces
        {
            get { return new[] { "Zetbox.API.Client", "Zetbox.DalProvider.Base", "Zetbox.DalProvider.Client" }; }
        }
    }
}
