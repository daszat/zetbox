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


namespace Zetbox.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.API;

    public class FilterValueSource : IFilterValueSource
    {
        private FilterValueSource(string expression)
        {
            this.Expression = expression;
        }

        #region IFilterValueSource Members

        public string Expression
        {
            get;
            private set;
        }

        #endregion

        public static IFilterValueSource FromProperty(Property p)
        {
            if (p == null) throw new ArgumentNullException("p");
            return new FilterValueSource(p.Name);
        }

        public static IFilterValueSource FromProperty(IEnumerable<Property> p)
        {
            if (p == null) throw new ArgumentNullException("p");
            return new FilterValueSource(string.Join(".", p.Select(i => i.Name).ToArray()));
        }

        public static IFilterValueSource FromExpression(string exp)
        {
            return new FilterValueSource(exp);
        }
    }
}
