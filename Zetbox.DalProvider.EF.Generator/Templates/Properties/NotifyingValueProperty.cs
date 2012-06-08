
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public class NotifyingValueProperty
        : Templates.Properties.NotifyingValueProperty
    {
        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializationMembersList list, string type, string name, string moduleNamespace, string backingName, bool isCalculated)
            : base(_host, ctx, list, type, name, moduleNamespace, backingName, isCalculated)
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
