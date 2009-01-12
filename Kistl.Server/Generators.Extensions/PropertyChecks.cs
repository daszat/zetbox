using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.Extensions
{
    public static class PropertyChecks
    {
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
            if (prop == null || !prop.HasStorage()) return false;

            if (prop is ObjectReferenceProperty)
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)prop;
                if (objRefProp.IsList == false &&
                    objRefProp.GetRelation() != null &&
                    objRefProp.GetRelationType() == Kistl.API.RelationType.one_n &&
                    objRefProp.GetOpposite().IsIndexed) return true;
            }
            if (prop.IsList == true &&
                prop.IsIndexed) return true;
            return false;
        }

        public static string GetCollectionTypeString(this Property prop)
        {
            if (prop.IsIndexed)
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
