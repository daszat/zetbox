using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.DalProvider.EF.Generator.Implementation
{

    public static class RelationExtensions
    {
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
    }
}
