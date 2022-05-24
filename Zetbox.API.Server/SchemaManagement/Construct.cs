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

namespace Zetbox.API.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    /// <summary>
    /// A collection of naming policies.
    /// </summary>
    /// TODO: respect maximal name length restrictions. PostgreSQL has 63 chars max, for example
    public static class Construct
    {
        #region Tablenames

        public static string RelationTableName(Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return String.Format("{0}_{1}_{2}", rel.A.RoleName, rel.Verb, rel.B.RoleName);
        }

        #endregion

        #region Association Names

        private static string InheritanceAssociationName(string parentClass, string childClass)
        {
            return "FK_" + childClass + "_" + parentClass + "_ID";
        }

        public static string InheritanceAssociationName(InterfaceType parentClass, InterfaceType childClass)
        {
            //if (parentClass == null) { throw new ArgumentNullException("parentClass"); }
            //if (childClass == null) { throw new ArgumentNullException("childClass"); }

            return InheritanceAssociationName(parentClass.Type.Name, childClass.Type.Name);
        }

        public static string InheritanceAssociationName(ObjectClass parentClass, ObjectClass childClass)
        {
            if (parentClass == null) { throw new ArgumentNullException("parentClass"); }
            if (childClass == null) { throw new ArgumentNullException("childClass"); }

            return InheritanceAssociationName(parentClass.Name, childClass.Name);
        }

        #endregion

        #region AssociationParentRoleName

        private static string AssociationParentRoleName(string obj)
        {
            return "A_" + obj;
        }

        public static string AssociationParentRoleName(ObjectClass obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            return AssociationParentRoleName(obj.Name);
        }

        #endregion

        #region AssociationChildRoleName

        private static string AssociationChildRoleName(string obj)
        {
            return "B_" + obj;
        }

        public static string AssociationChildRoleName(ObjectClass obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            return AssociationChildRoleName(obj.Name);
        }

        #endregion

        #region Index and Constraint Names
        public static string IndexName(string tblName, params string[] colNames)
        {
            return "IDX_" + tblName + "_" + string.Join("_", colNames);
        }

        public static string CheckConstraintName(string tblName, string colName)
        {
            return "CK_" + tblName + "_" + colName;
        }

        public static async Task<string[]> GetUCColNames(IndexConstraint uc)
        {
            if (uc == null) throw new ArgumentNullException("uc");

            var vt_columns = uc.Properties.OfType<ValueTypeProperty>().Select(p => Construct.ColumnName(p, null)).ToArray();
            var objref_columns = await uc.Properties.OfType<ObjectReferenceProperty>().Select(async p => await Construct.ForeignKeyColumnName(await p.RelationEnd.Parent.GetOtherEnd(p.RelationEnd))).ToList().WhenAll();
            var columns = vt_columns.Union(objref_columns).OrderBy(n => n).ToArray();
            return columns;
        }
        #endregion

        #region Column Names

        // internal use only. Implement and use a proper overload in/from this region.
        private static Task<string> ForeignKeyColumnName(string colName)
        {
            return Task.FromResult("fk_" + colName);
        }

        public static async Task<string> ForeignKeyColumnName(ValueTypeProperty listProp)
        {
            if (listProp == null) { throw new ArgumentNullException("listProp"); }

            return await ForeignKeyColumnName(listProp.ObjectClass.Name);
        }

        public static async Task<string> ForeignKeyColumnName(CompoundObjectProperty listProp)
        {
            if (listProp == null) { throw new ArgumentNullException("listProp"); }

            return await ForeignKeyColumnName(listProp.ObjectClass.Name);
        }

        /// <summary>Returns the name for a fk column pointing to otherEnd</summary>
        /// <returns>The name for a fk column pointing to otherEnd</returns>
        public static async Task<string> ForeignKeyColumnName(RelationEnd otherEnd)
        {
            return await ForeignKeyColumnName(otherEnd, string.Empty);
        }

        /// <summary>Returns the name for a fk column pointing to otherEnd</summary>
        /// <returns>The prefixed name for a fk column pointing to otherEnd</returns>
        public static async Task<string> ForeignKeyColumnName(RelationEnd otherEnd, string prefix)
        {
            if (otherEnd == null) { throw new ArgumentNullException("otherEnd"); }

            var rel = otherEnd.GetParent();
            var relEnd = await rel.GetOtherEnd(otherEnd);

            if (relEnd.Type.GetTableMapping() == TableMapping.TPH
                && relEnd.Type.BaseObjectClass != null
                && (await rel.HasStorage(relEnd.GetRole())))
            {
                return NestedColumnName(await ForeignKeyColumnName(NestedColumnName(otherEnd.RoleName, prefix)), relEnd.Type.TableName);
            }
            else
            {
                return await ForeignKeyColumnName(NestedColumnName(otherEnd.RoleName, prefix));
            }
        }

        public static string NestedColumnName(string prop, string parentPropName)
        {
            if (String.IsNullOrEmpty(parentPropName))
                return prop;

            return parentPropName + "_" + prop;
        }

        public static string ColumnName(Property prop, string parentPropName)
        {
            if (prop == null) throw new ArgumentNullException("prop");

            var cls = prop.ObjectClass as ObjectClass;

            if (cls != null && cls.GetTableMapping() == TableMapping.TPH && cls.BaseObjectClass != null)
            {
                return NestedColumnName(NestedColumnName(prop.Name, parentPropName), cls.TableName);
            }
            else
            {
                return NestedColumnName(prop.Name, parentPropName);
            }
        }

        public static string ListPositionColumnName(ValueTypeProperty prop)
        {
            if (prop == null) throw new ArgumentNullException("prop");

            return prop.Name + Zetbox.API.Helper.PositionSuffix;
        }

        public static string ListPositionColumnName(CompoundObjectProperty prop)
        {
            if (prop == null) throw new ArgumentNullException("prop");

            return prop.Name + Zetbox.API.Helper.PositionSuffix;
        }

        public static string ListPositionColumnName(RelationEnd otherEnd)
        {
            return ListPositionColumnName(otherEnd, string.Empty);
        }

        public static string ListPositionColumnName(RelationEnd otherEnd, string parentPropName)
        {
            if (otherEnd == null) { throw new ArgumentNullException("otherEnd"); }
            return ForeignKeyColumnName(Construct.NestedColumnName(otherEnd.RoleName, parentPropName)) + Zetbox.API.Helper.PositionSuffix;
        }

        public static string ListPositionPropertyName(RelationEnd relEnd)
        {
            if (relEnd == null) { throw new ArgumentNullException("relEnd"); }
            return relEnd.RoleName + Zetbox.API.Helper.PositionSuffix;
        }
        #endregion

        #region SecurityRules
        public static string SecurityRulesTableName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return objClass.TableName + "_Rights";
        }

        public static string SecurityRulesClassName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return objClass.Name + "_Rights";
        }

        public static string SecurityRulesUpdateRightsTriggerName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return objClass.TableName + "_Update_Rights_Trigger";
        }
        public static string SecurityRulesUpdateRightsTriggerName(Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return RelationTableName(rel) + "_Update_Rights_Trigger";
        }
        public static string SecurityRulesRightsViewUnmaterializedName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return objClass.TableName + "_Rights_unmaterialized";
        }

        public static string SecurityRulesRefreshRightsOnProcedurePrefix()
        {
            return "RefreshRightsOn_";
        }

        public static string SecurityRulesRefreshRightsOnProcedureName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return SecurityRulesRefreshRightsOnProcedurePrefix() + objClass.TableName;
        }

        public static string SecurityRulesRefreshAllRightsProcedureName()
        {
            return "RefreshAllRights";
        }

        public static string SecurityRulesIndexName(ObjectClass objClass)
        {
            return "IDX_" + SecurityRulesTableName(objClass);
        }

        public static string SecurityRulesFKName(ObjectClass objClass)
        {
            return "FK_" + SecurityRulesTableName(objClass);
        }

        #endregion

        public static string DiscriminatorValue(ObjectClass cls)
        {
            if (cls == null) throw new ArgumentNullException("cls");

            return string.Format("{0}.{1}", cls.Module.SchemaName, cls.TableName);
        }

        public static string ValueListCollectionTableName(ValueTypeProperty prop)
        {
            throw new NotImplementedException();
        }
    }
}
