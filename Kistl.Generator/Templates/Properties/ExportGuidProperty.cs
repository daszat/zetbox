
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public class ExportGuidProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
          IKistlContext ctx, Serialization.SerializationMembersList serializationList)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.ExportGuidProperty",
                ctx, serializationList);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx,
            Serialization.SerializationMembersList serializationList)
            : base(_host, ctx, serializationList, "Guid", "ExportGuid", String.Empty) // TODO: use proper namespace
        {
        }

        protected override void ApplyOnGetTemplate()
        {
            base.ApplyOnGetTemplate();

            this.WriteObjects("                if (", BackingMemberFromName(name), " == Guid.Empty) {\r\n");
            this.WriteObjects("                    __result = ", BackingMemberFromName(name), " = Guid.NewGuid();\r\n");
            this.WriteObjects("                }\r\n");
        }
    }
}
