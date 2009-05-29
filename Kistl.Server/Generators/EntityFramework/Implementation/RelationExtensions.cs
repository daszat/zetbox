using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{

    public static class RelationExtensions
    {

        /// <summary>
        /// Returns the association name for the given relation
        /// </summary>
        public static string GetAssociationName(this Relation rel)
        {
            return String.Format("FK_{0}_{1}_{2}", rel.A.Type.ClassName, rel.Verb, rel.B.Type.ClassName);
        }

        /// <summary>
        /// Returns the association name for the given ValueTypeProperty
        /// </summary>
        public static string GetAssociationName(this ValueTypeProperty prop)
        {
            return String.Format("FK_{0}_{1}_{2}", prop.ObjectClass.ClassName, prop.GetPropertyTypeString().Split('.').Last(), prop.PropertyName);
        }

        /// <summary>
        /// maps from a RelationEnd.Multiplicity to EF's RelationshipMultiplicity as used in the CSDL part of EDMX
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
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

        /// <summary>
        /// maps from a RelationEnd.Multiplicity to EF's RelationshipMultiplicity as used in the SSDL part of EDMX
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static RelationshipMultiplicity ToSsdlMultiplicity(this Multiplicity m)
        {
            switch (m)
            {
                case Multiplicity.One:
                case Multiplicity.ZeroOrOne:
                    return RelationshipMultiplicity.ZeroOrOne;
                case Multiplicity.ZeroOrMore:
                    return RelationshipMultiplicity.Many;
                default:
                    throw new ArgumentOutOfRangeException("m");
            }
        }
        /// <summary>
        /// Calculate how EF's RelationshipMultiplicity is written as EDMX attribute value
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string ToXmlValue(this RelationshipMultiplicity m)
        {
            switch (m)
            {
                case RelationshipMultiplicity.One:
                    return "1";
                case RelationshipMultiplicity.ZeroOrOne:
                    return "0..1";
                case RelationshipMultiplicity.Many:
                    return "*";
                default:
                    throw new ArgumentOutOfRangeException("m");
            }
        }

        internal static Relation Lookup(IKistlContext ctx, ObjectReferenceProperty prop)
        {
            return Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
        }
    }
}
