using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API;

namespace Kistl.App.Extensions
{
    public static class PropertyExtensions
    {
        [Obsolete("Storage of a Property is defined by the containing Relation")]
        public static bool HasStorage(this Property bp)
        {
            if (bp is ObjectReferenceProperty)
            {
                return HasStorage((ObjectReferenceProperty)bp);
            }
            else
            {
                return true;
            }
        }

        private static bool HasStorage(this ObjectReferenceProperty p)
        {
            Relation rel = RelationExtensions.Lookup(p.Context, p);
            if (rel == null) return true;

            if (rel.Storage == StorageType.Replicate)
                throw new NotImplementedException();

            RelationType type = rel.GetRelationType();
            return
                (type == RelationType.one_n && p.IsList == false)
                || (type == RelationType.one_one && rel.Storage == StorageType.Replicate)
                || (type == RelationType.one_one && rel.Storage == StorageType.MergeIntoA && rel.A.Navigator == p)
                || (type == RelationType.one_one && rel.Storage == StorageType.MergeIntoB && rel.B.Navigator == p)
                // TODO: n:m darf nicht an eine Seite gebunden sein
                || (type == RelationType.n_m && rel.A.Navigator == p);
        }
        public static RelationType GetRelationType(this ObjectReferenceProperty p)
        {
            Relation rel = RelationExtensions.Lookup(p.Context, p);
            if (rel == null) return p.IsList ? RelationType.n_m : RelationType.one_n;
            return rel.GetRelationType();
        }
    }
}
