using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Kistl.API
{
    public enum OrderByType
    {
        ASC = 1,
        DESC = 2,
    }

    public class OrderBy
    {
        public OrderBy()
        {
        }

        public OrderBy(OrderByType type, Expression expression)
        {
            this.Type = type;
            this.Expression = expression;
        }

        public OrderByType Type { get; set; }
        public Expression Expression { get; set; }
    }
}
