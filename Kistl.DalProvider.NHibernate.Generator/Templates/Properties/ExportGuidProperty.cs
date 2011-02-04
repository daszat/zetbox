
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public class ExportGuidProperty
        : ProxyProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace)
        {
            if (_host == null) { throw new ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ExportGuidProperty", ctx, serializationList, moduleNamespace);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace)
            : base(_host, ctx, serializationList, moduleNamespace, "Guid", "ExportGuid", false, false, false, null, false, null, Guid.Empty, "Guid", "ExportGuid")
        { 
        }

        protected override void ApplyOnGetTemplate()
        {
            this.WriteObjects("                if (this.Proxy.ExportGuid == Guid.Empty) {");
            this.WriteLine();
            this.WriteObjects("                    __result = this.Proxy.ExportGuid = Guid.NewGuid();");
            this.WriteLine();
            this.WriteObjects("                }");
            this.WriteLine();
        }
    }
}
