

namespace Kistl.Server.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators.Extensions;
    using Kistl.API;

    /// <summary>
    /// TODO: Rename and move to Kistl.API.Server
    /// When Provider are separated it will help
    /// </summary>
    public static class Construct
    {
        #region Association Names

        private static string InheritanceAssociationName(string parentClass, string childClass)
        {
            return "FK_" + childClass + "_" + parentClass + "_ID";
        }

        public static string InheritanceAssociationName(InterfaceType parentClass, InterfaceType childClass)
        {
            if (parentClass == null) { throw new ArgumentNullException("parentClass"); }
            if (childClass == null) { throw new ArgumentNullException("childClass"); }

            return InheritanceAssociationName(parentClass.Type.Name, childClass.Type.Name);
        }

        public static string InheritanceAssociationName(TypeMoniker parentClass, TypeMoniker childClass)
        {
            if (parentClass == null) { throw new ArgumentNullException("parentClass"); }
            if (childClass == null) { throw new ArgumentNullException("childClass"); }

            return InheritanceAssociationName(parentClass.ClassName, childClass.ClassName);
        }

        public static string InheritanceAssociationName(ObjectClass parentClass, ObjectClass childClass)
        {
            if (parentClass == null) { throw new ArgumentNullException("parentClass"); }
            if (childClass == null) { throw new ArgumentNullException("childClass"); }

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
            if (obj == null) { throw new ArgumentNullException("obj"); }

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
            if (obj == null) { throw new ArgumentNullException("obj"); }

            return AssociationChildRoleName(obj.ClassName);
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
            return NestedColumnName(prop.PropertyName, parentPropName);
        }

        public static string ListPositionColumnName(RelationEnd otherEnd)
        {
            return ListPositionColumnName(otherEnd, string.Empty);
        }

        public static string ListPositionColumnName(RelationEnd otherEnd, string parentPropName)
        {
            return ForeignKeyColumnName(Construct.NestedColumnName(otherEnd.RoleName, parentPropName)) + "_pos";
        }
        #endregion

        #region SecurityRules
        public static string SecurityRulesTableName(ObjectClass objClass)
        {
            return objClass.TableName + "_Rights";
        }

        public static string SecurityRulesWithRightsViewName(ObjectClass objClass)
        {
            return objClass.TableName + "_with_Rights";
        }

        public static string SecurityRulesWithRightsViewTriggerName(ObjectClass objClass)
        {
            return objClass.TableName + "_with_Rights_Trigger";
        }

        public static string SecurityRulesInsertRightsTriggerName(ObjectClass objClass)
        {
            return objClass.TableName + "_Insert_Rights_Trigger";
        }
        public static string SecurityRulesUpdateRightsTriggerName(ObjectClass objClass)
        {
            return objClass.TableName + "_Update_Rights_Trigger";
        }
        public static string SecurityRulesRightsViewUnmaterializedName(ObjectClass objClass)
        {
            return objClass.TableName + "_Rights_unmaterialized";
        }
        public static string SecurityRulesRefreshRightsOnProcedureName(ObjectClass objClass)
        {
            return "RefreshRightsOn_" + objClass.TableName;
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
    }
}
