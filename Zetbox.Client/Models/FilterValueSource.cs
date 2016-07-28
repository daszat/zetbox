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
    using Zetbox.App.Extensions;

    public class FilterValueSource : IFilterValueSource
    {
        private FilterValueSource(string expression, string lastInnerExpression)
        {
            this.Expression = expression;
            this.LastInnerExpression = lastInnerExpression;
            this.OuterExpression = expression.Replace(lastInnerExpression + FilterModel.PREDICATE_PLACEHOLDER, FilterModel.PREDICATE_PLACEHOLDER);
        }

        #region IFilterValueSource Members

        public string Expression
        {
            get;
            private set;
        }

        public string LastInnerExpression
        {
            get;
            private set;
        }

        public string OuterExpression 
        { 
            get; 
            private set; 
        }

        #endregion

        public static IFilterValueSource FromProperty(Property p)
        {
            if (p == null) throw new ArgumentNullException("p");
            return new FilterValueSource(p.Name + FilterModel.PREDICATE_PLACEHOLDER, p.Name);
        }

        public static IFilterValueSource FromProperty(IEnumerable<Property> properties)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            if (!properties.Any()) throw new ArgumentOutOfRangeException("properties", "At least one property is requiered");

            var result = FilterModel.PREDICATE_PLACEHOLDER;
            var isInList = false;

            foreach (var p in properties)
            {
                if (!isInList)
                {
                    result = result.Replace(FilterModel.PREDICATE_PLACEHOLDER, string.Format(".{0}{1}", p.Name, FilterModel.PREDICATE_PLACEHOLDER));
                }
                else
                {
                    result = result.Replace(FilterModel.PREDICATE_PLACEHOLDER, string.Format(".Any({0}{1})", p.Name, FilterModel.PREDICATE_PLACEHOLDER));
                }

                isInList = p.GetIsList();
            }

            result = result.Trim('.');

            return new FilterValueSource(result, string.Join(".", properties.Reverse().TakeWhile(i => !i.GetIsList()).Reverse().Select(i => i.Name)));
        }

        public static IFilterValueSource FromExpression(string exp)
        {
            return new FilterValueSource(exp + FilterModel.PREDICATE_PLACEHOLDER, exp);
        }
    }
}
