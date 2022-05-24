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

namespace Zetbox.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public static class PropertyChecks
    {
        public static async Task<bool> IsAssociation(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return await prop.IsObjectReferencePropertyList()
                || await prop.IsObjectReferencePropertySingle()
                || await prop.IsValueTypePropertyList()
                || await prop.IsCompoundObjectPropertyList();
        }

        public static Task<bool> IsValueTypePropertySingle(this Property prop)
        {
            return Task.FromResult(prop is ValueTypeProperty && !((ValueTypeProperty)prop).IsList);
        }

        public static Task<bool> IsValueTypePropertyList(this Property prop)
        {
            return Task.FromResult(prop is ValueTypeProperty && ((ValueTypeProperty)prop).IsList);
        }

        public static Task<bool> IsEnumerationPropertySingle(this Property prop)
        {
            return Task.FromResult(prop is EnumerationProperty && !((ValueTypeProperty)prop).IsList);
        }

        public static Task<bool> IsEnumerationPropertyList(this Property prop)
        {
            return Task.FromResult(prop is EnumerationProperty && ((ValueTypeProperty)prop).IsList);
        }

        public static async Task<bool> IsObjectReferencePropertySingle(this Property prop)
        {
            return prop is ObjectReferenceProperty && !await ((ObjectReferenceProperty)prop).IsList();
        }

        public static async Task<bool> IsObjectReferencePropertyList(this Property prop)
        {
            return prop is ObjectReferenceProperty && await ((ObjectReferenceProperty)prop).IsList();
        }

        public static Task<bool> IsCompoundObjectPropertySingle(this Property prop)
        {
            return Task.FromResult(prop is CompoundObjectProperty && !((CompoundObjectProperty)prop).IsList);
        }

        public static Task<bool> IsCompoundObjectPropertyList(this Property prop)
        {
            return Task.FromResult(prop is CompoundObjectProperty && ((CompoundObjectProperty)prop).IsList);
        }

        public static async Task<bool> HasPersistentOrder(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            bool result = false;

            if (prop is ObjectReferenceProperty)
            {
                var p = (ObjectReferenceProperty)prop;
                var rel = RelationExtensions.Lookup(p.Context, p);
                var relEnd = await rel.GetEnd(p);
                var otherEnd = await rel.GetOtherEnd(relEnd);

                if (await rel.NeedsPositionStorage(otherEnd.GetRole()))
                {
                    result = true;
                }
            }
            else if (prop is ValueTypeProperty)
            {
                result = ((ValueTypeProperty)prop).HasPersistentOrder;
            }
            else if (prop is CompoundObjectProperty)
            {
                result = ((CompoundObjectProperty)prop).HasPersistentOrder;
            }

            return result;
        }

        public static async Task<bool> IsList(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop is ObjectReferenceProperty)
            {
                return await IsList((ObjectReferenceProperty)prop);
            }
            else if (prop is ValueTypeProperty)
            {
                return ((ValueTypeProperty)prop).IsList;
            }
            else if (prop is CompoundObjectProperty)
            {
                return ((CompoundObjectProperty)prop).IsList;
            }
            return false;
        }

        public static async Task<bool> IsList(this ObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop.RelationEnd == null) throw new InvalidOperationException(string.Format("Error: object reference property {0} on ObjectClass {1} has no relation end", prop.Name, prop.ObjectClass));

            RelationEnd relEnd = prop.RelationEnd;
            Relation rel = relEnd.GetParent();
            RelationEnd otherEnd = await rel.GetOtherEnd(relEnd);

            return otherEnd.Multiplicity.UpperBound() > 1;
        }

        public static async Task<string> GetCSharpTypeDef(this Property prop)
        {
            return await prop.GetPropertyTypeString();
        }
    }
}
