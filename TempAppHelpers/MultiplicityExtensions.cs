using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    public static class MultiplicityExtensions
    {
        public static Multiplicity ToMultiplicity(this Property prop)
        {
            if (prop.IsList && prop.IsNullable)
            {
                return Multiplicity.ZeroOrMore;
            }
            else if (prop.IsList && !prop.IsNullable)
            {
                throw new InvalidOperationException();
            }
            else if (!prop.IsList && prop.IsNullable)
            {
                return Multiplicity.ZeroOrOne;
            }
            else if (!prop.IsList && !prop.IsNullable)
            {
                return Multiplicity.One;
            }
            throw new InvalidOperationException();
        }

        public static int UpperBound(this Multiplicity m)
        {
            switch (m)
            {
                case Multiplicity.One:
                case Multiplicity.ZeroOrOne:
                    return 1;
                case Multiplicity.ZeroOrMore:
                    return Int32.MaxValue;
                default:
                    throw new ArgumentOutOfRangeException("m", "unknown value");
            }
        }
    }
}
