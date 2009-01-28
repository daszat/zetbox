using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Movables
{
    public static class RelationExtensions
    {
        public static NewRelation ToNewRelation(this Relation rel)
        {
            return new NewRelation(
                ToRelationEnd(RelationEndRole.A, rel.LeftPart, rel.RightPart, "A, from relation ID = " + rel.ID),
                ToRelationEnd(RelationEndRole.B, rel.RightPart, rel.LeftPart, "B, from relation ID = " + rel.ID)
                );
        }

        // for test purposes only
        public static ObjectReferenceProperty ToProperty(this RelationEnd end, IKistlContext ctx)
        {
            var result = ctx.Create<ObjectReferenceProperty>();
            result.ObjectClass = end.Other.Type.ToObjectClass(ctx);
            // result.Module = missing;
            result.IsIndexed = end.HasPersistentOrder;
            result.IsList = end.Multiplicity.UpperBound() > 1;
            result.IsNullable = true; // always true for objects
            result.PropertyName = end.Other.RoleName;
            result.ReferenceObjectClass = end.Type.ToObjectClass(ctx);
            return result;
        }

        public static RelationEnd ToRelationEnd(RelationEndRole role, ObjectReferenceProperty prop, ObjectReferenceProperty otherProp, string site)
        {
            if (otherProp == null)
                throw new ArgumentNullException("otherProp");

            // assert that the two references are symmetrical
            Debug.Assert(prop.ObjectClass.GetTypeMoniker().Equals(otherProp.ReferenceObjectClass.GetTypeMoniker()));
            Debug.Assert(otherProp.ObjectClass.GetTypeMoniker().Equals(prop.ReferenceObjectClass.GetTypeMoniker()));

            return new RelationEnd(role)
            {
                Navigator = prop,
                Type = ((ObjectClass)prop.ObjectClass).GetTypeMoniker(),
                Multiplicity = otherProp.ToMultiplicity(),
                RoleName = otherProp.PropertyName,
                HasPersistentOrder = otherProp.IsIndexed,
                DebugCreationSite = site
            };
        }

        //public static RelationEnd ToRelationEnd(this ValueTypeProperty prop, string rolename, string site)
        //{
        //    return new RelationEnd()
        //    {
        //        Navigator = prop,
        //        Referenced = ((ObjectClass)prop.ObjectClass).GetTypeMoniker(),
        //        Multiplicity = prop.ToMultiplicity(),
        //        RoleName = rolename,
        //        DebugCreationSite = site
        //    };
        //}

    }
}
