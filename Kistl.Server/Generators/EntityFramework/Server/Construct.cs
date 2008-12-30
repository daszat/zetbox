using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.GeneratorsOld;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Server
{

    internal static class Construct
    {

        #region Association Names

        private static string AssociationName(string parentClass, string childClass, string propertyName)
        {
            return "FK_" + childClass + "_" + parentClass + "_" + propertyName;
        }

        public static string AssociationName(TypeMoniker parentClass, TypeMoniker childClass, string propertyName)
        {
            return AssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        }

        public static string AssociationName(ObjectClass parentClass, ObjectClass childClass, string propertyName)
        {
            return AssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        }

        public static string AssociationName(ObjectClass parentClass, Property listProperty)
        {
            return AssociationName(
                parentClass.ClassName,
                Construct.PropertyCollectionEntryType(listProperty).ClassName,
                listProperty.PropertyName);
        }

        //public static string AssociationName(ObjectClass parentClass, Property listProperty, string propertyName)
        //{
        //    return AssociationName(
        //        parentClass.ClassName,
        //        Construct.PropertyCollectionEntryType(listProperty).Classname,
        //        propertyName);
        //}

        #endregion

        #region AssociationParentRoleName

        private static string AssociationParentRoleName(string obj)
        {
            return "A_" + obj;
        }

        public static string AssociationParentRoleName(TypeMoniker obj)
        {
            return AssociationParentRoleName(obj.ClassName);
        }

        public static string AssociationParentRoleName(DataType obj)
        {
            return AssociationParentRoleName(obj.ClassName);
        }

        #endregion

        #region AssociationChildRoleName

        private static string AssociationChildRoleName(string obj)
        {
            return "B_" + obj;
        }

        public static string AssociationChildRoleName(TypeMoniker obj)
        {
            return AssociationChildRoleName(obj.ClassName);
        }

        public static string AssociationChildRoleName(ObjectClass obj)
        {
            return AssociationChildRoleName(obj.ClassName);
        }

        public static string AssociationChildRoleName(Property listProperty)
        {
            return AssociationChildRoleName(listProperty.PropertyName);
        }

        #endregion

        #region AssociationParentType

        public static TypeMoniker AssociationParentType(Property prop)
        {
            return new TypeMoniker(prop.GetPropertyTypeString());
        }

        #endregion

        #region AssociationChildType

        public static TypeMoniker AssociationChildType(Property prop)
        {
            if (prop.HasStorage())
            {
                if (!prop.IsList)
                {
                    return prop.ObjectClass.GetTypeMoniker();
                }
                else
                {
                    return Construct.PropertyCollectionEntryType(prop);
                }
            }
            else if (prop is ObjectReferenceProperty)
            {
                if (!((ObjectReferenceProperty)prop).GetOpposite().IsList)
                {
                    return new TypeMoniker(prop.GetPropertyTypeString());
                }
                else
                {
                    return Construct.PropertyCollectionEntryType(((ObjectReferenceProperty)prop).GetOpposite());
                }
            }

            throw new InvalidOperationException("Unable to find out AssociationChildType");
        }

        #endregion

        #region a collection entry TypeMoniker

        public static TypeMoniker PropertyCollectionEntryType(Property prop)
        {
            return new TypeMoniker(prop.ObjectClass.Module.Namespace,
                        prop.ObjectClass.ClassName + "_" + prop.PropertyName + "CollectionEntry");
        }

        #endregion

        #region Column Names

        public static string NestedColumnName(Property prop, string parentPropName)
        {
            if (String.IsNullOrEmpty(parentPropName))
                return prop.PropertyName;

            return parentPropName + "_" + prop.PropertyName;
        }

        public static string ForeignKeyColumnName(Property prop, string parentPropName)
        {
            return "fk_" + Construct.NestedColumnName(prop, parentPropName);
        }

        public static string ListPositionColumnName(Property prop, string parentPropName)
        {
            return ForeignKeyColumnName(prop, parentPropName) + "_pos";
        }

        #endregion
    }
}
