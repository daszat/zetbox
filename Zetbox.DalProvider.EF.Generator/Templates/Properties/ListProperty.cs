
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class ListProperty
        : Templates.Properties.ListProperty
    {
        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, Templates.Serialization.SerializationMembersList list, DataType containingType, String name, Property property)
            : base(_host, ctx, list, containingType, name, property)
        {
        }

        protected override void ApplyRequisitesTemplate()
        {
            base.ApplyRequisitesTemplate();

            if (this.property is ObjectReferenceProperty)
            {
                // here we're doing direct references, without any CollectionEntries
                // this is 1:N stuff

                var orp = (ObjectReferenceProperty)this.property;
                var rel = Zetbox.App.Extensions.RelationExtensions.Lookup(ctx, orp);
                var relEnd = rel.GetEnd(orp);
                var assocName = rel.GetAssociationName();

                Properties.EfListWrapper.Call(Host, ctx,
                    name + ImplementationPropertySuffix,
                    assocName, relEnd.RoleName, relEnd.Type.GetDataTypeString() + ImplementationSuffix);
            }
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();

            // duplicate code from Model.csdl.EntityTypeFields.cst
        }
    }
}
