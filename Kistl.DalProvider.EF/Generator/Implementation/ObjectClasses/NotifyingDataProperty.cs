using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class NotifyingDataProperty
        : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
    {
        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, Property prop)
            : base(_host, ctx, list, prop)
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
