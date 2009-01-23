using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Movables;
using System.Data.Metadata.Edm;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{

    public static class RelationExtensions
    {
        public static StorageHint GetPreferredStorage(this FullRelation rel)
        {
            if (rel.Right.Multiplicity.UpperBound() == 1 && rel.Left.Multiplicity.UpperBound() == 1)
            {
                // arbitrary 1:1 relations default 
                return StorageHint.MergeRight;
            }
            else if (rel.Right.Multiplicity.UpperBound() > 1 && rel.Left.Multiplicity.UpperBound() == 1)
            {
                return StorageHint.MergeRight;
            }
            else if (rel.Right.Multiplicity.UpperBound() == 1 && rel.Left.Multiplicity.UpperBound() > 1)
            {
                return StorageHint.MergeLeft;
            }
            else if (rel.Right.Multiplicity.UpperBound() > 1 && rel.Left.Multiplicity.UpperBound() > 1)
            {
                return StorageHint.Separate;
            }

            // this means that UpperBound() < 1 for some end
            throw new NotImplementedException();
        }

        public static string GetAssociationName(this FullRelation rel)
        {
            return String.Format("FK_{0}_{1}_{2}_{3}", rel.Right.Referenced.ClassName, rel.Left.Referenced.ClassName, rel.Left.RoleName, rel.ID);
        }

        /// <summary>
        /// Returns the association name for the association from the given end to the CollectionEntry
        /// </summary>
        public static string GetCollectionEntryAssociationName(this FullRelation rel, ObjectRelationEnd end)
        {
            return String.Format("FK_{0}_{1}_{2}_{3}", rel.Right.Referenced.ClassName, rel.Left.Referenced.ClassName, end.RoleName, rel.ID);
        }

        /// <summary>
        /// Returns the association name for the association from the Right end to the CollectionEntry
        /// </summary>
        public static string GetRightToCollectionEntryAssociationName(this FullRelation rel)
        {
            return rel.GetCollectionEntryAssociationName(rel.Right);
        }

        /// <summary>
        /// Returns the association name for the association from the Left end to the CollectionEntry
        /// </summary>
        public static string GetLeftToCollectionEntryAssociationName(this FullRelation rel)
        {
            return rel.GetCollectionEntryAssociationName(rel.Left);
        }

        public static string GetCollectionEntryClassName(this FullRelation rel)
        {
            return String.Format("{0}_{1}{2}CollectionEntry", rel.Right.Referenced.ClassName, rel.Right.Navigator.PropertyName, rel.ID);
        }

        public static string GetCollectionEntryFullName(this FullRelation rel)
        {
            return String.Format("{0}.{1}", rel.Right.Referenced.Module.Namespace, rel.GetCollectionEntryClassName());
        }

        public static RelationshipMultiplicity ToCsdlRelationshipMultiplicity(this Multiplicity m)
        {
            switch (m)
            {
                case Multiplicity.One:
                case Multiplicity.ZeroOrOne:
                    // ObjectReferences in C# are always nullable
                    return RelationshipMultiplicity.ZeroOrOne;
                case Multiplicity.ZeroOrMore:
                    return RelationshipMultiplicity.Many;
                default:
                    throw new ArgumentOutOfRangeException("m");
            }
        }
    }
}
