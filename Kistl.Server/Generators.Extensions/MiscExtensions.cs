using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Server.Generators.Extensions
{
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
            return String.Format("{0}_{1}_{2}_RelationEntry", rel.A.Type.ClassName, rel.Verb, rel.B.Type.ClassName);
        }

        /// <summary>
        /// Support "legacy" non-unique naming scheme
        /// </summary>
        public static string GetRelationTableName(this Relation rel)
        {
            return String.Format("{0}_{1}_{2}", rel.A.Type.TableName, rel.Verb, rel.B.Type.TableName);
        }

        public static string GetRelationFullName(this Relation rel)
        {
            return String.Format("{0}.{1}", rel.A.Type.Module.Namespace, rel.GetRelationClassName());
        }

        public static string GetRelationFkColumnName(this Relation rel, RelationEndRole endRole)
        {
            var relEnd = rel.GetEnd(endRole);
            // legacy condition: if navigator exists, use his objectclass' name
            // See Kistl.Server.GeneratorsOld.Helper.GeneratorHelper.CalcForeignKeyColumnName()
            // TODO: remove this after reworking the SQL Generator to use Relations
            if (relEnd.Navigator != null)
            {
                return "fk_" + relEnd.Navigator.ObjectClass.ClassName;
            }
            else
            {
                return "fk_" + relEnd.RoleName;
            }
        }
        #endregion

        #region CollectionEntry naming standards
        public static string GetCollectionEntryClassName(this ValueTypeProperty prop)
        {
            return String.Format("{0}_{1}_CollectionEntry", prop.ObjectClass.ClassName, prop.PropertyName);
        }

        public static string GetCollectionEntryTable(this ValueTypeProperty prop)
        {
            return String.Format("{0}_{1}Collection", ((ObjectClass)prop.ObjectClass).TableName, prop.PropertyName);
        }

        public static string GetCollectionEntryFullName(this ValueTypeProperty prop)
        {
            return String.Format("{0}.{1}", prop.ObjectClass.Module.Namespace, prop.GetCollectionEntryClassName());
        }
        #endregion
    }
}
