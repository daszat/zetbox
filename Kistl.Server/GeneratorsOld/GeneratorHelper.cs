using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.GeneratorsOld.Helper
{
    public static class GeneratorHelper
    {
        #region Misc Helper
        public static string CalcColumnName(this string propName, string parentPropName)
        {
            if (string.IsNullOrEmpty(parentPropName)) return propName;
            return parentPropName + "_" + propName;
        }

        public static string CalcForeignKeyColumnName(this string propName, string parentPropName)
        {
            return "fk_" + propName.CalcColumnName(parentPropName);
        }

        public static string CalcListPositionColumnName(this string propName, string parentPropName)
        {
            return propName.CalcForeignKeyColumnName(parentPropName) + "_pos";
        }

        #endregion

        #region GetAssociationName
        //private static string GetAssociationName(string parentClass, string childClass, string propertyName)
        //{
        //    return "FK_" + childClass + "_" + parentClass + "_" + propertyName;
        //}

        //public static string GetAssociationName(TypeMoniker parentClass, TypeMoniker childClass, string propertyName)
        //{
        //    return GetAssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        //}

        //public static string GetAssociationName(ObjectClass parentClass, ObjectClass childClass, string propertyName)
        //{
        //    return GetAssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        //}

        //public static string GetAssociationName(ObjectClass parentClass, Property listProperty)
        //{
        //    return GetAssociationName(
        //        parentClass.ClassName,
        //        GeneratorHelper.GetPropertyCollectionObjectType(listProperty).ClassName,
        //        listProperty.PropertyName);
        //}
        //public static string GetAssociationName(ObjectClass parentClass, Property listProperty, string propertyName)
        //{
        //    return GetAssociationName(
        //        parentClass.ClassName,
        //        GeneratorHelper.GetPropertyCollectionObjectType(listProperty).ClassName,
        //        propertyName);
        //}
        #endregion

        #region GetAssociationParentRoleName
        private static string GetAssociationParentRoleName(string obj)
        {
            return "A_" + obj;
        }

        public static string GetAssociationParentRoleName(TypeMoniker obj)
        {
            return GetAssociationParentRoleName(obj.ClassName);
        }

        public static string GetAssociationParentRoleName(DataType obj)
        {
            return GetAssociationParentRoleName(obj.ClassName);
        }
        #endregion

        #region GetAssociationChildRoleName
        private static string GetAssociationChildRoleName(string obj)
        {
            return "B_" + obj;
        }

        public static string GetAssociationChildRoleName(TypeMoniker obj)
        {
            return GetAssociationChildRoleName(obj.ClassName);
        }

        public static string GetAssociationChildRoleName(ObjectClass obj)
        {
            return GetAssociationChildRoleName(obj.ClassName);
        }
        #endregion

        #region GetPropertyCollectionObjectType
        // ==>> Construct.PropertyCollectionEntryType
        public static TypeMoniker GetPropertyCollectionObjectType(Property prop)
        {
            return new TypeMoniker(prop.ObjectClass.Module.Namespace,
                        prop.ObjectClass.ClassName + "_" + prop.PropertyName + "CollectionEntry");
        }
        #endregion

        #region GetDatabaseTableName
        public static string GetDatabaseTableName(ObjectClass objClass)
        {
            return objClass.TableName;
        }

        public static string GetDatabaseTableName(Property prop)
        {
            if (!prop.IsList) throw new ArgumentException("Property must be a List Property", "prop");
            return ((ObjectClass)prop.ObjectClass).TableName + "_" + prop.PropertyName + "Collection";
        }
        #endregion

        #region GetLists
        public static IQueryable<ObjectClass> GetObjectClassList(Kistl.API.IKistlContext ctx)
        {
            return from c in ctx.GetQuery<ObjectClass>()
                   select c;
        }

        public static IQueryable<Interface> GetInterfaceList(Kistl.API.IKistlContext ctx)
        {
            return from i in ctx.GetQuery<Interface>()
                   select i;
        }

        public static IQueryable<Enumeration> GetEnumList(Kistl.API.IKistlContext ctx)
        {
            return from e in ctx.GetQuery<Enumeration>()
                   select e;
        }

        public static IQueryable<Struct> GetStructList(Kistl.API.IKistlContext ctx)
        {
            return from s in ctx.GetQuery<Struct>()
                   select s;
        }

        // -> GetObjectListPropertiesWithStorage(IKistlContext)
        public static IEnumerable<Property> GetCollectionProperties(Kistl.API.IKistlContext ctx)
        {
            return (from p in ctx.GetQuery<Property>()
                    where p.ObjectClass is ObjectClass && p.IsList
                    select p).ToList().Where(p => p.HasStorage());
        }

        // -> GetObjectReferencePropertiesWithStorage(IKistlContext)
        public static IEnumerable<ObjectReferenceProperty> GetObjectReferenceProperties(Kistl.API.IKistlContext ctx)
        {
            return (from p in ctx.GetQuery<ObjectReferenceProperty>()
                    where p.ObjectClass is ObjectClass
                    select p).ToList().Where(p => p.HasStorage());
        }
        #endregion

        #region GetAssociationChildType
        public static TypeMoniker GetAssociationChildType(Property prop)
        {
            if (prop.HasStorage())
            {
                if (!prop.IsList)
                {
                    return prop.ObjectClass.GetTypeMoniker();
                }
                else
                {
                    return GeneratorHelper.GetPropertyCollectionObjectType(prop);
                }
            }
            else if (prop is ObjectReferenceProperty)
            {
                var orp = (ObjectReferenceProperty)prop;
                var rel = RelationExtensions.Lookup(prop.Context, orp);
                RelationEnd relEnd = rel.GetEnd(orp);
                RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

 
                if (relEnd.Multiplicity.UpperBound() == 1)
                {
                    return new TypeMoniker(prop.GetPropertyTypeString());
                }
                else
                {
                    return GeneratorHelper.GetPropertyCollectionObjectType(otherEnd.Navigator);
                }
            }

            throw new InvalidOperationException("Unable to find out AssociationChildType");
        }

        public static TypeMoniker GetAssociationChildTypeImplementation(Property prop)
        {
            if (!prop.IsList)
            {
                return prop.ObjectClass.GetTypeMonikerImplementation();
            }
            else
            {
                return GeneratorHelper.GetPropertyCollectionObjectType(prop);
            }
        }
        #endregion
    }
}
