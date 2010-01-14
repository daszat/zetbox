using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class NotifyingValueProperty
        : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingValueProperty
    {
        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, string type, string name, string moduleNamespace)
            : base(_host, ctx, list, type, name, moduleNamespace)
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
