
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public class ExportGuidProperty
        : Templates.Properties.ExportGuidProperty
    {
        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList list, string backingName)
            : base(_host, ctx, list, backingName)
        {
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();
            EfScalarPropHelper.ApplyAttributesTemplate(this);
        }

        protected override void ApplyBackingStoreDefinition()
        {
            EfScalarPropHelper.ApplyBackingStoreDefinition(this, type, backingName, name);
        }
    }
}
