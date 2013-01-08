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

namespace Zetbox.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Zetbox.API;
    using Zetbox.App.Base;

    public static class RelationExtensions
    {
        public static Relation Lookup(IZetboxContext ctx, ObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (prop.RelationEnd == null) { return null; }

            return prop.RelationEnd.AParent ?? prop.RelationEnd.BParent;
        }

        /// <summary>
        /// Returns the association name for the association from the given end to the CollectionEntry
        /// </summary>
        public static string GetRelationAssociationName(this Relation rel, RelationEndRole endRole)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            RelationEnd relEnd = rel.GetEndFromRole(endRole);

            return String.Format("FK_{0}_{1}_{2}_{3}", rel.A.RoleName, rel.Verb, rel.B.RoleName, relEnd.GetRole());
        }

        /// <summary>
        /// Returns the association name for the given relation
        /// </summary>
        public static string GetAssociationName(this Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return String.Format("FK_{0}_{1}_{2}", rel.A.RoleName, rel.Verb, rel.B.RoleName);
        }

        public static RelationEndRole GetRole(this RelationEnd relEnd)
        {
            if (relEnd == null) { throw new ArgumentNullException("relEnd"); }
            if (relEnd.AParent == null && relEnd.BParent == null)
            {
                throw new ArgumentOutOfRangeException("relEnd", "RelationEnd not connected to any parent");
            }
            return relEnd.AParent != null ? RelationEndRole.A : RelationEndRole.B;
        }

        public static Relation GetParent(this RelationEnd relEnd)
        {
            // no relEnd, no parent.
            if (relEnd == null) { return null; }

            return relEnd.AParent ?? relEnd.BParent;
        }

        public static ObjectClass GetReferencedObjectClass(this ObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            var rel = prop.RelationEnd.GetParent();
            if (rel == null) { return null; }

            var relEnd = prop.RelationEnd;
            if (relEnd == null) { return null; }

            var otherEnd = rel.GetOtherEnd(relEnd);
            if (otherEnd == null) { return null; }

            return otherEnd.Type;
        }

        public static bool IsNullable(this RelationEnd relEnd)
        {
            if (relEnd == null) { throw new ArgumentNullException("relEnd"); }

            return relEnd.Multiplicity.LowerBound() == 0;
        }

        public static RelationEnd GetOtherEndFromRole(this Relation rel, RelationEndRole role)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
     
            switch (role)
            {
                case RelationEndRole.A:
                    return rel.B;
                case RelationEndRole.B:
                    return rel.A;
                default:
                    throw new ArgumentOutOfRangeException("role", String.Format("unknown RelationEndRole '{0}'", role));
            }
        }

        public static bool HasStorage(this Relation rel, RelationEndRole role)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }

            var storage = rel.Storage;

            if (storage == StorageType.Replicate)
                throw new NotImplementedException();

            var type = rel.GetRelationType();
            // n:m has no storage on A or B
            return
                   (type == RelationType.one_n && storage == StorageType.MergeIntoA && role == RelationEndRole.A)
                || (type == RelationType.one_n && storage == StorageType.MergeIntoB && role == RelationEndRole.B)
                || (type == RelationType.one_one && storage == StorageType.Replicate)
                || (type == RelationType.one_one && storage == StorageType.MergeIntoA && role == RelationEndRole.A)
                || (type == RelationType.one_one && storage == StorageType.MergeIntoB && role == RelationEndRole.B);
        }
    }
}
