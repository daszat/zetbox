using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class ListProperty : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.ListProperty
    {
        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, DataType containingType, Type type, String name, Property property)
            : base(_host, ctx, containingType, type, name, property)
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
                var rel = FullRelation.Lookup(ctx, orp);
                var relEnd = rel.GetEnd(orp);

                CallTemplate("Implementation.ObjectClasses.EfListWrapper", ctx,
                    name + Kistl.API.Helper.ImplementationSuffix,
                    rel.GetAssociationName(), relEnd.RoleName, relEnd.Referenced.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix);
            }
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();

            // duplicate code from Model.csdl.EntityTypeFields.cst
        }
    }
}
