
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
          IKistlContext ctx, Serialization.SerializationMembersList serializationList, string backingName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.ExportGuidProperty",
                ctx, serializationList, backingName);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx,
            Serialization.SerializationMembersList serializationList, string backingName)
            : base(_host, ctx, serializationList, "Guid", "ExportGuid", String.Empty, backingName) // TODO: use proper namespace
        {
        }

        protected override void ApplyOnGetTemplate()
        {
            base.ApplyOnGetTemplate();

            this.WriteObjects("                if (", backingName, " == Guid.Empty) {\r\n");
            this.WriteObjects("                    __result = ", backingName, " = Guid.NewGuid();\r\n");
            this.WriteObjects("                }\r\n");
        }
    }
}
