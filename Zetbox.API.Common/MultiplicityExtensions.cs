using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.App.Base;

namespace Zetbox.App.Extensions
{
    public static class MultiplicityExtensions
    {
        public static int LowerBound(this Multiplicity m)
        {
            switch (m)
            {
                case Multiplicity.One:
                    return 1;
                case Multiplicity.ZeroOrOne:
                case Multiplicity.ZeroOrMore:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException("m", "unknown value");
            }
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
