using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.Extensions
{
    public static class PropertyChecks
    {
        public static bool IsListProperty(this BaseProperty prop)
        {
            return prop is Property && ((Property)prop).IsList;
        }

        public static bool IsValueTypePropertySingle(this BaseProperty prop)
        {
            return prop is ValueTypeProperty && !((Property)prop).IsList;
        }

        public static bool IsValueTypePropertyList(this BaseProperty prop)
        {
            return prop is ValueTypeProperty && ((Property)prop).IsList;
        }

        public static bool IsEnumerationPropertySingle(this BaseProperty prop)
        {
            return prop is EnumerationProperty && !((Property)prop).IsList;
        }

        public static bool IsEnumerationPropertyPropertyList(this BaseProperty prop)
        {
            return prop is EnumerationProperty && ((Property)prop).IsList;
        }

        public static bool IsObjectReferencePropertySingle(this BaseProperty prop)
        {
            return prop is ObjectReferenceProperty && !((Property)prop).IsList;
        }

        public static bool IsObjectReferencePropertyList(this BaseProperty prop)
        {
            return prop is ObjectReferenceProperty && ((Property)prop).IsList;
        }

        public static bool IsStructPropertySingle(this BaseProperty prop)
        {
            return prop is StructProperty && !((Property)prop).IsList;
        }

        public static bool IsStructPropertyPropertyList(this BaseProperty prop)
        {
            return prop is StructProperty && ((Property)prop).IsList;
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
