// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public static class MiscExtensions
    {
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

        public static string SerializerReadMethod(this string typeName)
        {
            var nullable = typeName.EndsWith("?");
            if (nullable)
                typeName = typeName.Substring(0, typeName.Length - 1);

            string suffix;
            switch (typeName)
            {
                case "bool":
                    suffix = "Boolean";
                    break;
                case "int":
                    suffix = "Int32";
                    break;
                default:
                    suffix = typeName.Substring(0, 1).ToUpperInvariant() + typeName.Substring(1);
                    break;
            }

            return "Read" + (nullable ? "Nullable" : string.Empty) + suffix;
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
        public static string GetCollectionEntryNamespace(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            var orp = prop as ObjectReferenceProperty;
            if (orp != null)
            {
                return orp.RelationEnd.Parent.Module.Namespace;
            }
            else
            {
                return prop.Module.Namespace;
            }
        }

        public static string GetCollectionEntryClassName(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return String.Format("{0}_{1}_CollectionEntry", prop.ObjectClass.Name, prop.Name);
        }

        public static string GetCollectionEntryTable(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            var cls = prop.ObjectClass as ObjectClass;
            return String.Format("{0}_{1}Collection", cls != null ? cls.TableName : prop.ObjectClass.Name, prop.Name);
        }

        public static string GetCollectionEntryFullName(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            return String.Format("{0}.{1}", prop.GetCollectionEntryNamespace(), prop.GetCollectionEntryClassName());
        }

        public static string GetCollectionEntryReverseKeyColumnName(this Property prop)
        {
            return "fk_" + prop.ObjectClass.Name;
        }

        #endregion
    }
}
