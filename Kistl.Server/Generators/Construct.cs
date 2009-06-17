using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators
{
    /// <summary>
    /// TODO: Rename and move to Kistl.API.Server
    /// When Provider are seperated it will help
    /// </summary>
    internal static class Construct
    {

        #region Association Names

        private static string InheritanceAssociationName(string parentClass, string childClass)
        {
            return "FK_" + childClass + "_" + parentClass + "_ID";
        }

        public static string InheritanceAssociationName(TypeMoniker parentClass, TypeMoniker childClass)
        {
            return InheritanceAssociationName(parentClass.ClassName, childClass.ClassName);
        }

        public static string InheritanceAssociationName(ObjectClass parentClass, ObjectClass childClass)
        {
            return InheritanceAssociationName(parentClass.ClassName, childClass.ClassName);
        }

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

        #region a collection entry TypeMoniker

        public static TypeMoniker PropertyCollectionEntryType(Property prop)
        {
            return new TypeMoniker(prop.ObjectClass.Module.Namespace,
                        prop.ObjectClass.ClassName + "_" + prop.PropertyName + "CollectionEntry");
        }

        #endregion

        #region Column Names

        // internal use only. Implement and use a proper overload in/from this region.
        private static string ForeignKeyColumnName(string colName)
        {
            return "fk_" + colName;
        }

        public static string ForeignKeyColumnName(ObjectReferenceProperty prop)
        {
            return ForeignKeyColumnName(prop.PropertyName);
        }

        public static string ForeignKeyColumnName(ObjectReferenceProperty prop, string prefix)
        {
            return ForeignKeyColumnName(NestedColumnName(prop, prefix));
        }

        /// <summary>
        /// Create a name for a foreign key referencing the <see cref="DataType"/> dt.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ForeignKeyColumnNameReferencing(DataType dt)
        {
            return ForeignKeyColumnName(dt.ClassName);
        }

        public static string NestedColumnName(Property prop, string parentPropName)
        {
            if (String.IsNullOrEmpty(parentPropName))
                return prop.PropertyName;

            return parentPropName + "_" + prop.PropertyName;
        }

        public static string ListPositionColumnName(Property prop)
        {
            return ListPositionColumnName(prop, "");
        }

        public static string ListPositionColumnName(Property prop, string parentPropName)
        {
            return ForeignKeyColumnName(Construct.NestedColumnName(prop, parentPropName)) + "_pos";
        }

        /// <summary>
        /// This is used for the ParentIndex in CollectionEntrys
        /// </summary>
        public static string ListPositionColumnName(DataType cls)
        {
            return ForeignKeyColumnName(cls.ClassName) + "_pos";
        }

        /// <summary>
        /// This is used for the ParentIndex in CollectionEntrys
        /// </summary>
        public static string ForeignListPositionColumnName(DataType cls)
        {
            return ForeignKeyColumnNameReferencing(cls) + "_pos";
        }

        #endregion

    }
}
