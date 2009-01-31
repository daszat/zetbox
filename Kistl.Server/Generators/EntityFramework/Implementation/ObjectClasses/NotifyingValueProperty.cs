using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class NotifyingValueProperty : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingValueProperty
    {
        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string type, string name)
            : base(_host, ctx, type, name)
        {

        }
        protected override void ApplyRequisitesTemplate()
        {
            base.ApplyRequisitesTemplate();
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
