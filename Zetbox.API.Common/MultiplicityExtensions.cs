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
