
namespace Kistl.DalProvider.NHibernate.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.Generator;
    using System.Collections.Specialized;

    public class NhGenerator
        : AbstractBaseGenerator
    {
        public NhGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        // TODO: #1569 Why not using const Suffix?
        public override string ExtraSuffix { get { return "NHibernate"; } }
        public override string Description { get { return "NHibernate"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.NHibernate"; } }
        public override string BaseName { get { return "NHibernate"; } }
        public override string ProjectGuid { get { return "{5514C9AF-6C2E-4713-8EAC-FAAADFFDB029}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        public override IEnumerable<string> RequiredNamespaces
        {
            get
            {
                return new string[] {
                   "Kistl.API.Utils", "Kistl.DalProvider.Base", "Kistl.DalProvider.NHibernate",
                };
            }
        }

        protected override IEnumerable<string> Generate_Other(Kistl.API.IKistlContext ctx)
        {
            using (log4net.NDC.Push("NhGenerateOther"))
            {
                var otherFileNames = new List<string>();

                // Mapping files are picked up automatically by the ProjectFile
                // so we don't need to keep track of them.
                foreach (var oc in ctx.GetQuery<ObjectClass>().Where(i => i.BaseObjectClass == null))
                {
                    this.RunTemplateWithExtension(ctx, "Mappings.ObjectClassHbm", oc.Name, "hbm.xml",
                        Templates.Mappings.ObjectClassHbm.MakeArgs(ctx, oc, new NameValueCollection() { { "extrasuffix", ExtraSuffix } }));
                }

                return base.Generate_Other(ctx).Concat(otherFileNames);
            }
        }
    }
}
