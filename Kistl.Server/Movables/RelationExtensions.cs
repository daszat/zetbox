using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.Server.Movables
{
    public static class RelationExtensions
    {
        public static FullRelation ToFullRelation(this Relation rel)
        {
            return new FullRelation(
                rel.LeftPart.ToRelationEnd("B_" + rel.LeftPart.ObjectClass.ClassName, "Left, from relation ID = " + rel.ID),
                rel.RightPart.ToRelationEnd("A_" + rel.RightPart.ObjectClass.ClassName, "Right, from relation ID = " + rel.ID)
                );
        }

        public static ObjectRelationEnd ToRelationEnd(this ObjectReferenceProperty prop, string rolename, string site)
        {
            return new ObjectRelationEnd()
            {
                Navigator = prop,
                Referenced = (ObjectClass)prop.ObjectClass,
                Multiplicity = prop.ToMultiplicity(),
                RoleName = rolename,
                DebugCreationSite = site
            };
        }
    }
}
