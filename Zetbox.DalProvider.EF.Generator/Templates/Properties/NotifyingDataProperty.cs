
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public class NotifyingDataProperty
        : Zetbox.Generator.Templates.Properties.NotifyingDataProperty
    {
        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList list, Property prop)
            : base(_host, ctx, list, prop)
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
