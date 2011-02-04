
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public class ExportGuidProperty
        : Templates.Properties.ExportGuidProperty
    {
        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Serialization.SerializationMembersList list, string backingName)
            : base(_host, ctx, list, backingName)
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
