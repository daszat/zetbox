using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class NotifyingValueProperty : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingValueProperty
    {
        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, Type type, string name)
            : base(_host, type, name)
        {

        }
        protected override void ApplyRequisitesTemplate()
        {
            base.ApplyRequisitesTemplate();
            //CallTemplate("Implementation.ObjectClasses.NotifyingValuePropertyRequisites", 
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();
            this.WriteLine("        [XmlIgnore()]");
            this.WriteLine("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]");
        }

    }
}
