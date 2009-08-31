using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.CollectionEntries
{
    public class ExportGuidProperty
        : Templates.Implementation.CollectionEntries.ExportGuidProperty
    {
        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList list)
            : base(_host, ctx, list)
        {

        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();
            this.WriteLine("        [XmlIgnore()]");
            this.WriteLine("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]");
            this.WriteLine("        [EdmScalarProperty()]");
        }
    }
}
