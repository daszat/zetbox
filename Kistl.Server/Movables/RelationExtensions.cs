using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.API;

namespace Kistl.Server.Movables
{
    public static class RelationExtensions
    {
        public static NewRelation ToNewRelation(this Relation rel)
        {
            return new NewRelation(
                rel.LeftPart.ToRelationEnd("B_" + rel.LeftPart.ObjectClass.ClassName, "Left, from relation ID = " + rel.ID),
                rel.RightPart.ToRelationEnd("A_" + rel.RightPart.ObjectClass.ClassName, "Right, from relation ID = " + rel.ID)
                );
        }

        public static RelationEnd ToRelationEnd(this ObjectReferenceProperty prop, string rolename, string site)
        {
            return new RelationEnd()
            {
                Navigator = prop,
                Referenced = ((ObjectClass)prop.ObjectClass).GetTypeMoniker(),
                Multiplicity = prop.ToMultiplicity(),
                RoleName = rolename,
                DebugCreationSite = site
            };
        }

        public static RelationEnd ToRelationEnd(this ValueTypeProperty prop, string rolename, string site)
        {
            return new RelationEnd()
            {
                Navigator = prop,
                Referenced = ((ObjectClass)prop.ObjectClass).GetTypeMoniker(),
                Multiplicity = prop.ToMultiplicity(),
                RoleName = rolename,
                DebugCreationSite = site
            };
        }

        /// <summary>
        /// Whether or not the given relation will result in two association 
        /// sets. Note that this is true exactly if this is a N:M relation 
        /// between two Classes
        /// </summary>
        public static bool IsTwoProngedAssociation(this NewRelation rel, IKistlContext ctx)
        {
            return rel.Left.Referenced.ToObjectClass(ctx) != null;
        }

    }
}
