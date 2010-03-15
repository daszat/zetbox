
namespace Kistl.Server.Generators.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public static class MiscExtensions
    {
        public static string ToNameSpace(this TaskEnum task)
        {
            if (task == TaskEnum.Interface)
            {
                return "Kistl.Objects";
            }
            else
            {
                return string.Format(@"Kistl.Objects.{0}", task);
            }
        }

        public static string ToCSharpTypeRef(this Type t)
        {
            if (t == null) { throw new ArgumentNullException("t"); }

            if (t.IsGenericType)
            {
                return String.Format("{0}<{1}>",
                    t.FullName.Split('`')[0], // TODO: hack to get to class name
                    String.Join(", ", t.GetGenericArguments().Select(arg => arg.ToCSharpTypeRef()).ToArray())
                    );
            }
            else
            {
                return t.FullName;
            }
        }

        public static string ToDbType(this ValueTypeProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            if (prop is IntProperty)
                return "int";
            if (prop is StringProperty)
                return "nvarchar";
            if (prop is DoubleProperty)
                return "float";
            if (prop is BoolProperty)
                return "bit";
            if (prop is DateTimeProperty)
                return "datetime";

            throw new ArgumentOutOfRangeException("prop", "unknown ValueTypeProperty type: " + prop.GetType().FullName);
        }

        #region Relation naming standards
        public static string GetRelationClassName(this Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return String.Format("{0}_{1}_{2}_RelationEntry", rel.A.Type.Name, rel.Verb, rel.B.Type.Name);
        }

        public static string GetRelationTableName(this Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return String.Format("{0}_{1}_{2}", rel.A.RoleName, rel.Verb, rel.B.RoleName);
        }

        public static string GetRelationFullName(this Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return String.Format("{0}.{1}", rel.Module.Namespace, rel.GetRelationClassName());
        }

        public static string GetRelationFkColumnName(this Relation rel, RelationEndRole endRole)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            var relEnd = rel.GetEndFromRole(endRole);
            return "fk_" + relEnd.RoleName;
        }
        #endregion

        #region CollectionEntry naming standards
        public static string GetCollectionEntryClassName(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return String.Format("{0}_{1}_CollectionEntry", prop.ObjectClass.Name, prop.PropertyName);
        }

        public static string GetCollectionEntryTable(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return String.Format("{0}_{1}Collection", ((ObjectClass)prop.ObjectClass).TableName, prop.PropertyName);
        }

        public static string GetCollectionEntryFullName(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return String.Format("{0}.{1}", prop.ObjectClass.Module.Namespace, prop.GetCollectionEntryClassName());
        }
        #endregion
    }
}
