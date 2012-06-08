// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

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
