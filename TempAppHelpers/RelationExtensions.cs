using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
 
    public static class RelationExtensions
    {
        public static RelationEnd GetOtherEnd(this Relation rel, RelationEnd relEnd)
        {
            if (rel.A == relEnd)
                return rel.B;
            else if (rel.B == relEnd)
                return rel.A;
            else
                return null;
        }

        public static RelationEnd GetEnd(this Relation rel, RelationEndRole role)
        {
            switch (role)
            {
                case RelationEndRole.A:
                    return rel.A;
                case RelationEndRole.B:
                    return rel.B;
                default:
                    throw new ArgumentOutOfRangeException("role");
            }
        }

        public static RelationEnd GetEnd(this Relation rel, ObjectReferenceProperty prop)
        {
            if (rel.A.Navigator == prop)
                return rel.A;
            else if (rel.B.Navigator == prop)
                return rel.B;
            else
                return null;
        }

        public static Relation Lookup(IKistlContext ctx, ObjectReferenceProperty prop)
        {
            if(prop.RelationEnd == null) return null;
            return prop.RelationEnd.AParent ?? prop.RelationEnd.BParent;
        }

        public static RelationType GetRelationType(this Relation rel)
        {
            if (rel.A.Multiplicity.UpperBound() == 1 && rel.B.Multiplicity.UpperBound() > 1) return RelationType.one_n;
            if (rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() == 1) return RelationType.one_n;
            if (rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() > 1) return RelationType.n_m;
            if (rel.A.Multiplicity.UpperBound() == 1 && rel.B.Multiplicity.UpperBound() == 1) return RelationType.one_one;

            throw new InvalidOperationException("Unable to find out RelationType");
        }

        public static bool NeedsPositionStorage(this Relation rel, RelationEndRole endRole)
        {
            return ((rel.Storage == StorageType.MergeIntoA && RelationEndRole.A == endRole && rel.A.HasPersistentOrder)
                || (rel.Storage == StorageType.MergeIntoB && RelationEndRole.B == endRole && rel.B.HasPersistentOrder)
                || (rel.Storage == StorageType.Replicate 
                    && (
                        (rel.A.HasPersistentOrder && RelationEndRole.A == endRole)
                        || (rel.B.HasPersistentOrder && RelationEndRole.B == endRole))
                    )
                || (rel.Storage == StorageType.Separate && (rel.A.HasPersistentOrder || rel.B.HasPersistentOrder))
                );
        }


        /// <summary>
        /// Returns the association name for the association from the given end to the CollectionEntry
        /// </summary>
        public static string GetRelationAssociationName(this Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);

            return String.Format("FK_{0}_{1}_{2}_{3}", rel.A.Type.ClassName, rel.Verb, rel.B.Type.ClassName, relEnd.RoleName);
        }
    }

}
