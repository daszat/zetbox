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
            return prop.IsObjectReferencePropertyList() || prop.IsObjectReferencePropertySingle() || prop.IsValueTypePropertyList();
        }

        public static bool IsListProperty(this Property prop)
        {
            return prop.IsList;
        }

        public static bool IsValueTypePropertySingle(this Property prop)
        {
            return prop is ValueTypeProperty && !prop.IsList;
        }

        public static bool IsValueTypePropertyList(this Property prop)
        {
            return prop is ValueTypeProperty && prop.IsList;
        }

        public static bool IsEnumerationPropertySingle(this Property prop)
        {
            return prop is EnumerationProperty && !prop.IsList;
        }

        public static bool IsEnumerationPropertyPropertyList(this Property prop)
        {
            return prop is EnumerationProperty && prop.IsList;
        }

        public static bool IsObjectReferencePropertySingle(this Property prop)
        {
            return prop is ObjectReferenceProperty && !prop.IsList;
        }

        public static bool IsObjectReferencePropertyList(this Property prop)
        {
            return prop is ObjectReferenceProperty && prop.IsList;
        }

        public static bool IsStructPropertySingle(this Property prop)
        {
            return prop is StructProperty && !prop.IsList;
        }

        public static bool IsStructPropertyPropertyList(this Property prop)
        {
            return prop is StructProperty && prop.IsList;
        }

        public static bool NeedsPositionColumn(this Property prop)
        {
            bool result = false;

            var p = prop as ObjectReferenceProperty;
            if (p != null)
            {
                var rel = RelationExtensions.Lookup(p.Context, p);
                var relEnd = rel.GetEnd(p);
                result = rel.NeedsPositionStorage((RelationEndRole)relEnd.Role);
            }
            return result;
        }

        public static string GetCollectionTypeString(this Property prop)
        {
            bool isIndexed = false;

            if (prop is ObjectReferenceProperty)
            {
                var p = (ObjectReferenceProperty)prop;
                var rel = RelationExtensions.Lookup(p.Context, p);
                var relEnd = rel.GetEnd(p);
                var relOtherEnd = rel.GetOtherEnd(relEnd);
                if (rel.NeedsPositionStorage((RelationEndRole)relOtherEnd.Role))
                {
                    isIndexed = true;
                }
            }
            else
            {
                isIndexed = prop.IsIndexed;
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

    }
}
