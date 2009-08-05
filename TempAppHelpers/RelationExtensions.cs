
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kistl.API;
    using Kistl.App.Base;

    public static class RelationExtensions
    {
        public static Relation Lookup(IKistlContext ctx, ObjectReferenceProperty prop)
        {
            if(prop.RelationEnd == null) return null;
            return prop.RelationEnd.AParent ?? prop.RelationEnd.BParent;
        }

        /// <summary>
        /// Returns the association name for the association from the given end to the CollectionEntry
        /// </summary>
        public static string GetRelationAssociationName(this Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEndFromRole(endRole);

            return String.Format("FK_{0}_{1}_{2}_{3}", rel.A.Type.ClassName, rel.Verb, rel.B.Type.ClassName, relEnd.RoleName);
        }
    }
}
