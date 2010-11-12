
namespace Kistl.DalProvider.NHibernate.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.Generator;

    public class NhGenerator
        : AbstractBaseGenerator
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Generator.NHibernate");

        public NhGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        [Obsolete]
        public static string Suffix { get { return "NHibernate"; } }

        // TODO: #1569 Why not using const Suffix?
        public override string ExtraSuffix { get { return "NHibernate"; } }
        public override string Description { get { return ExtraSuffix; } }
        public override string TargetNameSpace { get { return "Kistl.Objects." + ExtraSuffix; } }
        public override string BaseName { get { return ExtraSuffix; } }
        public override string ProjectGuid { get { return "{5514C9AF-6C2E-4713-8EAC-FAAADFFDB029}"; } }
        public override IEnumerable<string> RequiredNamespaces
        {
            get
            {
                return new string[] {
                   "Kistl.DalProvider.NHibernate",
                };
            }
        }
    }
}
