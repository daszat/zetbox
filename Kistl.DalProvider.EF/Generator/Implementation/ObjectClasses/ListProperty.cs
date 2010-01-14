using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class ListProperty 
        : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.ListProperty
    {
        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, DataType containingType, String name, Property property)
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
                var rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, orp);
                var relEnd = rel.GetEnd(orp);

                Implementation.ObjectClasses.EfListWrapper.Call(Host, ctx,
                    name + Kistl.API.Helper.ImplementationSuffix,
                    rel.GetAssociationName(), relEnd.RoleName, relEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix);
            }
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();

            // duplicate code from Model.csdl.EntityTypeFields.cst
        }
    }
}
