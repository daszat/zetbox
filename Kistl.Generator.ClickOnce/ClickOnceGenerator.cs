using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.Generator.ClickOnce
{
    public class ClickOnceGenerator : AbstractBaseGenerator
    {
        public ClickOnceGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string Description { get { return "ClickOnce"; } }
        public override string ExtraSuffix { get { return "ClickOnce"; } }
        public override string TargetNameSpace { get { return "Kistl.Client.ClickOnce"; } }
        public override string BaseName { get { return "ClickOnce"; } }
        public override string ProjectGuid { get { return "{D057DB2C-1916-45CF-B8D8-2ACAAC5D47F9}"; } }
        public override IEnumerable<string> RequiredNamespaces { get { return new string[] { }; } }

        protected override List<string> Generate_Objects(API.IKistlContext ctx)
        {
            // Do nothing!
            return new List<string>();
        }

        protected override IEnumerable<string> Generate_Other(API.IKistlContext ctx)
        {
            // Added directly in project file
            this.RunTemplate(ctx, "Program", "Program.cs");
            this.RunTemplate(ctx, "DefaultConfig", "DefaultConfig.xml");
            this.RunTemplate(ctx, "AppConfig", "ZBox\\Client\\Kistl.Client.WPF.exe.config");

            // No Module for bootstrapper needed
            return new List<string>();
        }
    }
}
