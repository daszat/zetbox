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
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public static class PropertyChecks
    {
        public static bool IsAssociation(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return prop.IsObjectReferencePropertyList()
                || prop.IsObjectReferencePropertySingle()
                || prop.IsValueTypePropertyList()
                || prop.IsCompoundObjectPropertyList();
        }

        public static bool IsValueTypePropertySingle(this Property prop)
        {
            return prop is ValueTypeProperty && !((ValueTypeProperty)prop).IsList;
        }

        public static bool IsValueTypePropertyList(this Property prop)
        {
            return prop is ValueTypeProperty && ((ValueTypeProperty)prop).IsList;
        }

        public static bool IsEnumerationPropertySingle(this Property prop)
        {
            return prop is EnumerationProperty && !((ValueTypeProperty)prop).IsList;
        }

        public static bool IsEnumerationPropertyList(this Property prop)
        {
            return prop is EnumerationProperty && ((ValueTypeProperty)prop).IsList;
        }

        public static bool IsObjectReferencePropertySingle(this Property prop)
        {
            return prop is ObjectReferenceProperty && !((ObjectReferenceProperty)prop).IsList();
        }

        public static bool IsObjectReferencePropertyList(this Property prop)
        {
            return prop is ObjectReferenceProperty && ((ObjectReferenceProperty)prop).IsList();
        }

        public static bool IsCompoundObjectPropertySingle(this Property prop)
        {
            return prop is CompoundObjectProperty && !((CompoundObjectProperty)prop).IsList;
        }

        public static bool IsCompoundObjectPropertyList(this Property prop)
        {
            return prop is CompoundObjectProperty && ((CompoundObjectProperty)prop).IsList;
        }

        public static bool HasPersistentOrder(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            bool result = false;

            if (prop is ObjectReferenceProperty)
            {
                var p = (ObjectReferenceProperty)prop;
                var rel = RelationExtensions.Lookup(p.Context, p);
                var relEnd = rel.GetEnd(p);
                var otherEnd = rel.GetOtherEnd(relEnd);

                if (rel.NeedsPositionStorage(otherEnd.GetRole()))
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

        public static bool IsList(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop is ObjectReferenceProperty)
            {
                return IsList((ObjectReferenceProperty)prop);
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

        public static bool IsList(this ObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop.RelationEnd == null) throw new InvalidOperationException(string.Format("Error: object reference property {0} on ObjectClass {1} has no relation end", prop.Name, prop.ObjectClass));

            RelationEnd relEnd = prop.RelationEnd;
            Relation rel = relEnd.GetParent();
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            return otherEnd.Multiplicity.UpperBound() > 1;
        }

        public static string GetCSharpTypeDef(this Property prop)
        {
            return prop.GetPropertyTypeString();
        }
    }
}
