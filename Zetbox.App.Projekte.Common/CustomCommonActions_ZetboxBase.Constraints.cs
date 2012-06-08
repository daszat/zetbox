
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Extensions;

    public static partial class CustomCommonActions_ZetboxBase
    {
        #region Relation_Storage Constraint

        public static bool OnIsValid_Relation_Storage(object constrainedObject, object constrainedValue)
        {
            if (constrainedObject == null) { throw new ArgumentNullException("constrainedObject"); }
            var rel = (Relation)constrainedObject;
            if (rel.A != null && rel.B != null)
            {
                if (rel.A.Multiplicity == 0 || rel.B.Multiplicity == 0) return false;
                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        return rel.B.Multiplicity.UpperBound() <= 1;
                    case StorageType.MergeIntoB:
                        return rel.A.Multiplicity.UpperBound() <= 1;
                    case StorageType.Separate:
                        return rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() > 1;
                    case StorageType.Replicate:
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string OnGetErrorText_Relation_Storage(object constrainedObject, object constrainedValue)
        {
            if (constrainedObject == null) { throw new ArgumentNullException("constrainedObject"); }
            var rel = (Relation)constrainedObject;
            if (rel.A != null && rel.B != null)
            {
                if (rel.A.Multiplicity == 0 || rel.B.Multiplicity == 0) return "Incomplete Relation (Multiplicity is missing)";
                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        return rel.B.Multiplicity.UpperBound() <= 1 ? String.Empty : "B side could be more than one. Not able to merge foreign key into A";
                    case StorageType.MergeIntoB:
                        return rel.A.Multiplicity.UpperBound() <= 1 ? String.Empty : "A side could be more than one. Not able to merge foreign key into B";
                    case StorageType.Separate:
                        if (rel.A.Multiplicity.UpperBound() <= 1 && rel.B.Multiplicity.UpperBound() <= 1)
                        {
                            return "A side is only one-ary. Please use MergeIntoA or MergeIntoB";
                        }
                        else if (rel.A.Multiplicity.UpperBound() <= 1)
                        {
                            return "A side is only one-ary. Please use MergeIntoB";
                        }
                        else if (rel.B.Multiplicity.UpperBound() <= 1)
                        {
                            return "B side is only one-ary. Please use MergeIntoA";
                        }
                        else
                        {
                            return String.Empty;
                        }
                    case StorageType.Replicate:
                    default:
                        return String.Format("StorageType.{0} not implemented.", rel.Storage);
                }
            }
            else
            {
                return "Incomplete Relation (A or B missing)";
            }
        }

        #endregion

        #region Relation_Containment Constraint

        public static bool OnIsValid_Relation_Containment(object constrainedObject, object constrainedValue)
        {
            if (constrainedObject == null) { throw new ArgumentNullException("constrainedObject"); }
            var rel = (Relation)constrainedObject;
            if (rel.A != null && rel.B != null)
            {
                if (rel.A.Multiplicity == 0 || rel.B.Multiplicity == 0) return false;

                var relType = rel.GetRelationType();

                switch (rel.Containment)
                {
                    case ContainmentSpecification.AContainsB:
                        if (relType == RelationType.n_m) return false;
                        else if (relType == RelationType.one_n) return rel.B.Multiplicity.UpperBound() > 1;
                        else if (relType == RelationType.one_one) return true;
                        else return false;
                    case ContainmentSpecification.BContainsA:
                        if (relType == RelationType.n_m) return false;
                        else if (relType == RelationType.one_n) return rel.A.Multiplicity.UpperBound() > 1;
                        else if (relType == RelationType.one_one) return true;
                        else return false;
                    case ContainmentSpecification.Independent:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string OnGetErrorText_Relation_Containment(object constrainedObject, object constrainedValue)
        {
            if (constrainedObject == null) { throw new ArgumentNullException("constrainedObject"); }
            var rel = (Relation)constrainedObject;
            if (rel.A != null && rel.B != null)
            {
                if (rel.A.Multiplicity == 0 || rel.B.Multiplicity == 0)
                {
                    return "Incomplete Relation (A.Multiplicity or B.Multiplicity missing)";
                }

                var relType = rel.GetRelationType();

                switch (rel.Containment)
                {
                    case ContainmentSpecification.AContainsB:
                        if (relType == RelationType.n_m) return "N:M relations cannot be containment relations";
                        else if (relType == RelationType.one_n) return rel.B.Multiplicity.UpperBound() > 1 ? String.Empty : "Can only contain the N-side of a 1:N relationship (which is A).";
                        else if (relType == RelationType.one_one) return String.Empty;
                        else return String.Format("RelationType.{0} not implemented.", relType);
                    case ContainmentSpecification.BContainsA:
                        if (relType == RelationType.n_m) return "N:M relations cannot be containment relations";
                        else if (relType == RelationType.one_n) return rel.A.Multiplicity.UpperBound() > 1 ? String.Empty : "Can only contain the N-side of a 1:N relationship (which is B).";
                        else if (relType == RelationType.one_one) return String.Empty;
                        else return String.Format("RelationType.{0} not implemented.", relType);
                    case ContainmentSpecification.Independent:
                        return String.Empty;
                    default:
                        return String.Format("ContainmentType.{0} not implemented.", rel.Containment);
                }
            }
            else
            {
                return "Incomplete Relation (A or B missing)";
            }
        }

        #endregion

        #region RelationEnd_Navigator Constraint

        public static bool OnIsValid_RelationEnd_Navigator(object constrainedObject, object constrainedValue)
        {
            var relEnd = (RelationEnd)constrainedObject;
            var rel = relEnd.GetParent();
            if (rel == null) return false;
            var otherEnd = rel.GetOtherEnd(relEnd);
            var orp = (ObjectReferenceProperty)constrainedValue;

            if (orp != null)
            {
                if (orp.ObjectClass == null)
                {
                    return false;
                }
                if (orp.ObjectClass != relEnd.Type)
                {
                    return false;
                }

                switch (otherEnd.Multiplicity)
                {
                    case Multiplicity.One:
                        return orp.Constraints.OfType<NotNullableConstraint>().Count() > 0;
                    case Multiplicity.ZeroOrMore:
                        return orp.Constraints.OfType<NotNullableConstraint>().Count() == 0;
                    case Multiplicity.ZeroOrOne:
                        return orp.Constraints.OfType<NotNullableConstraint>().Count() == 0;
                    default:
                        return false;
                }
            }
            return true;
        }

        public static string OnGetErrorText_RelationEnd_Navigator(object constrainedObject, object constrainedValue)
        {
            var relEnd = (RelationEnd)constrainedObject;
            var rel = relEnd.GetParent();
            if (rel == null) return "No Relation assigned to Relation end";
            var otherEnd = rel.GetOtherEnd(relEnd);
            var orp = (ObjectReferenceProperty)constrainedValue;

            var result = new List<string>();

            if (orp != null)
            {
                switch (otherEnd.Multiplicity)
                {
                    case Multiplicity.One:
                        if (orp.Constraints.OfType<NotNullableConstraint>().Count() == 0)
                        {
                            result.Add("Navigator should have NotNullableConstraint because Multiplicity of opposite RelationEnd is One");
                        }
                        break;
                    case Multiplicity.ZeroOrMore:
                        if (orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                        {
                            result.Add("Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrMore");
                        }
                        break;
                    case Multiplicity.ZeroOrOne:
                        if (orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                        {
                            result.Add("Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrOne");
                        }
                        break;
                }

                if (relEnd.Type == null)
                {
                    result.Add("RelationEnd has no Type defined yet.");
                }
                else if (orp.ObjectClass == null)
                {
                    result.Add(String.Format("Navigator should be attached to {0}",
                        relEnd.Type));
                }
                else if (relEnd.Type != orp.ObjectClass)
                {
                    result.Add(String.Format("Navigator is attached to {0} but should be attached to {1}",
                        orp.ObjectClass,
                        relEnd.Type));
                }
            }

            return String.Join("\n", result.ToArray());
        }

        #endregion

        #region RelationEnd_HasPersistentOrder Constraint

        public static bool OnIsValid_RelationEnd_HasPersistentOrder(object constrainedObject, object constrainedValue)
        {
            var relEnd = (RelationEnd)constrainedObject;
            var hasPersistentOrder = (bool)constrainedValue;

            if (hasPersistentOrder && relEnd.Multiplicity != Multiplicity.ZeroOrMore)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string OnGetErrorText_RelationEnd_HasPersistentOrder(object constrainedObject, object constrainedValue)
        {
            var relEnd = (RelationEnd)constrainedObject;
            var hasPersistentOrder = (bool)constrainedValue;

            if (hasPersistentOrder && relEnd.Multiplicity != Multiplicity.ZeroOrMore)
            {
                return String.Format("Can only require persistent order when multiplicity is ZeroOrMore, but multiplicity is {0}", relEnd.Multiplicity);
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region Property_Name_Unique Constraint

        public static bool OnIsValid_Property_Name_Unique(object constrainedObject, object constrainedValue)
        {
            var prop = (Property)constrainedObject;
            var name = (string)constrainedValue;

            var dataType = prop.ObjectClass;
            while (dataType != null)
            {
                if (dataType.Properties.Where(p => p != prop && p.Name == name).Count() > 0)
                {
                    return false;
                }

                var klass = dataType as ObjectClass;
                if (klass != null)
                {
                    dataType = klass.BaseObjectClass;
                }
            }
            return true;
        }

        public static string OnGetErrorText_Property_Name_Unique(object constrainedObject, object constrainedValue)
        {
            var prop = (Property)constrainedObject;
            var name = (string)constrainedValue;

            var dataType = prop.ObjectClass;
            while (dataType != null)
            {
                var collidingProperties = dataType.Properties.Where(p => p != prop && p.Name == name).ToList();

                if (collidingProperties.Count > 0)
                {
                    return String.Format("Property name '{0}' collides with '{1}.{2}'", name, dataType, collidingProperties.First());
                }

                var klass = dataType as ObjectClass;
                if (klass != null)
                {
                    dataType = klass.BaseObjectClass;
                }
            }
            return String.Empty;
        }

        #endregion
    }
}
