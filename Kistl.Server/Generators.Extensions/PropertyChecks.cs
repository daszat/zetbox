using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API;

namespace Kistl.Server.Generators.Extensions
{
    public static class PropertyChecks
    {
        public static bool IsAssociation(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return prop.IsObjectReferencePropertyList() || prop.IsObjectReferencePropertySingle() || prop.IsValueTypePropertyList();
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

        public static bool IsEnumerationPropertyPropertyList(this Property prop)
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

        public static bool IsCompoundObjectPropertyPropertyList(this Property prop)
        {
            return prop is CompoundObjectProperty && ((CompoundObjectProperty)prop).IsList;
        }

        public static string GetCollectionTypeString(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            bool isIndexed = false;

            if (prop is ObjectReferenceProperty)
            {
                var p = (ObjectReferenceProperty)prop;
                var rel = RelationExtensions.Lookup(p.Context, p);
                var relEnd = rel.GetEnd(p);
                var otherEnd = rel.GetOtherEnd(relEnd);
                if (rel.NeedsPositionStorage(otherEnd.GetRole()))
                {
                    isIndexed = true;
                }
            }
            else if (prop is ValueTypeProperty)
            {
                isIndexed = ((ValueTypeProperty)prop).HasPersistentOrder;
            }
            else if (prop is CompoundObjectProperty)
            {
                isIndexed = ((CompoundObjectProperty)prop).HasPersistentOrder;
            }

            if (isIndexed)
            {
                return string.Format("IList<{0}>", prop.GetPropertyTypeString());
            }
            else
            {
                return string.Format("ICollection<{0}>", prop.GetPropertyTypeString());
            }
        }

        public static bool IsList(this ObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            RelationEnd relEnd = prop.RelationEnd;
            Relation rel = relEnd.GetParent();
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            return otherEnd.Multiplicity.UpperBound() > 1;
        }
    }
}
