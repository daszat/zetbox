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

namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;

    /// <summary>
    /// A collection of naming policies.
    /// </summary>
    /// TODO: respect maximal name length restrictions. PostgreSQL has 63 chars max, for example
    public static class Construct
    {
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

        #region IndexNames
        public static string IndexName(string tblName, params string[] colNames)
        {
            return "IDX_" + tblName + "_" + string.Join("_", colNames);
        }
        #endregion

        #region Column Names

        // internal use only. Implement and use a proper overload in/from this region.
        private static string ForeignKeyColumnName(string colName)
        {
            return "fk_" + colName;
        }

        public static string ForeignKeyColumnName(RelationEnd otherEnd)
        {
            return ForeignKeyColumnName(otherEnd, string.Empty);
        }

        public static string ForeignKeyColumnName(RelationEnd otherEnd, string prefix)
        {
            if (otherEnd == null) { throw new ArgumentNullException("otherEnd"); }

            return ForeignKeyColumnName(NestedColumnName(otherEnd.RoleName, prefix));
        }

        public static string NestedColumnName(string prop, string parentPropName)
        {
            if (String.IsNullOrEmpty(parentPropName))
                return prop;

            return parentPropName + "_" + prop;
        }

        public static string NestedColumnName(Property prop, string parentPropName)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            return NestedColumnName(prop.Name, parentPropName);
        }

        public static string ListPositionColumnName(ValueTypeProperty prop)
        {
            return prop.Name + Zetbox.API.Helper.PositionSuffix;
        }

        public static string ListPositionColumnName(CompoundObjectProperty prop)
        {
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
            return rel.GetRelationTableName() + "_Update_Rights_Trigger";
        }
        public static string SecurityRulesRightsViewUnmaterializedName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return objClass.TableName + "_Rights_unmaterialized";
        }
        public static string SecurityRulesRefreshRightsOnProcedureName(ObjectClass objClass)
        {
            if (objClass == null) { throw new ArgumentNullException("objClass"); }
            return "RefreshRightsOn_" + objClass.TableName;
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

        public static string ValueListCollectionTableName(ValueTypeProperty prop)
        {
            throw new NotImplementedException();
        }
    }
}
