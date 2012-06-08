

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
