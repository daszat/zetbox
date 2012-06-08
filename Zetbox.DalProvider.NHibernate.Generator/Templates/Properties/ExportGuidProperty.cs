
namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public class ExportGuidProperty
        : ProxyProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string interfaceName)
        {
            if (_host == null) { throw new ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ExportGuidProperty", ctx, serializationList, moduleNamespace, interfaceName);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string interfaceName)
            : base(_host, ctx, serializationList, moduleNamespace, "Guid", "ExportGuid", false, false, false, interfaceName, null, false, null, Guid.Empty, "Guid", "ExportGuid", false)
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
