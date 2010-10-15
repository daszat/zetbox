

namespace Kistl.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using Kistl.API;
    
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

        public static IFilterValueSource FromExpression(string exp)
        {
            return new FilterValueSource(exp);
        }
    }
}
