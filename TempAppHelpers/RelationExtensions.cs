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
            return ctx.GetQuery<Relation>().ToList().Where(rel => rel.A.Navigator == prop || rel.B.Navigator == prop).FirstOrDefault();
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
            return ((rel.Storage == StorageType.MergeIntoA && RelationEndRole.A == endRole)
                || (rel.Storage == StorageType.MergeIntoB && RelationEndRole.B == endRole)
                || (rel.Storage == StorageType.Replicate));
        }
    }

}
