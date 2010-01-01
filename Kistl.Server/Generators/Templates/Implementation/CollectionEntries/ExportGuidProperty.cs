using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public class ExportGuidProperty
        : Templates.Implementation.ObjectClasses.NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
          IKistlContext ctx, SerializationMembersList serializationList)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.CollectionEntries.ExportGuidProperty",
                ctx, serializationList);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, SerializationMembersList serializationList)
            : base(_host, ctx, serializationList, "Guid", "ExportGuid", "") // TODO: use proper namespace
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
