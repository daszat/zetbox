using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.GeneratorsOld;

using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{

    internal static class Construct
    {

        #region Association Names

        private static string AssociationName(string parentClass, string childClass, string propertyName)
        {
            return "FK_" + childClass + "_" + parentClass + "_" + propertyName;
        }

        /// <summary>
        /// TODO: This is only needed for ID Relations
        /// </summary>
        /// <param name="parentClass"></param>
        /// <param name="childClass"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string AssociationName(TypeMoniker parentClass, TypeMoniker childClass, string propertyName)
        {
            return AssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        }

        //public static string AssociationName(ObjectClass parentClass, ObjectClass childClass, string propertyName)
        //{
        //    return AssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        //}

        //public static string AssociationName(ObjectClass parentClass, Property listProperty)
        //{
        //    return AssociationName(
        //        parentClass.ClassName,
        //        Construct.PropertyCollectionEntryType(listProperty).ClassName,
        //        listProperty.PropertyName);
        //}

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

        //public static string AssociationChildEntitySetName(Property prop)
        //{
        //    TypeMoniker childType = Construct.AssociationChildType(prop);
        //    if (!prop.IsList)
        //    {
        //        return prop.Context.GetQuery<ObjectClass>().First(c => childType.ClassName == c.ClassName).GetRootClass().ClassName;
        //    }
        //    else
        //    {
        //        return childType.ClassName;
        //    }
        //}

        //#region AssociationChildType

        //public static TypeMoniker AssociationChildType(Property prop)
        //{
        //    if (prop.HasStorage())
        //    {
        //        if (!prop.IsList)
        //        {
        //            return prop.ObjectClass.GetTypeMoniker();
        //        }
        //        else
        //        {
        //            return Construct.PropertyCollectionEntryType(prop);
        //        }
        //    }
        //    else if (prop is ObjectReferenceProperty)
        //    {
        //        return AssociationChildType((ObjectReferenceProperty)prop);
        //    }

        //    throw new InvalidOperationException("Unable to find out AssociationChildType");
        //}

        //public static TypeMoniker AssociationChildType(ObjectReferenceProperty prop)
        //{
        //    Relation rel = RelationExtensions.Lookup(prop.Context, prop);
        //    RelationEnd relEnd = rel.GetEnd(prop);
        //    RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

        //    if (!opposite.IsList)
        //    {
        //        return new TypeMoniker(prop.GetPropertyTypeString());
        //    }
        //    else
        //    {
        //        return Construct.PropertyCollectionEntryType(opposite);
        //    }
        //}


        //#endregion

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
