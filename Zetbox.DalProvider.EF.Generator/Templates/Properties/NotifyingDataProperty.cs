
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public class NotifyingDataProperty
        : Kistl.Generator.Templates.Properties.NotifyingDataProperty
    {
        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Serialization.SerializationMembersList list, Property prop)
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
