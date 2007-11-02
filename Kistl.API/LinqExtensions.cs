using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Kistl.API
{
    /// <summary>
    /// Linq Extensions. Aus dem Indischen Netz gefladert. 
    /// Könnte als Demo für Parameter Suchen herhalten.
    /// Und ich hab nüsse Ahnung, wie das funktioniert.
    /// </summary>
    public static class LinqExtensions
    {
        public static IQueryable<T> AddEqualityCondition<T, V>(this IQueryable<T> queryable,
          string propertyName, V propertyValue)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "p");

            IQueryable<T> x = queryable.Where<T>(
              Expression.Lambda<Func<T, bool>>(
                Expression.Equal(Expression.Property(
                  pe,
                  typeof(T).GetProperty(propertyName)),
                  Expression.Constant(propertyValue, typeof(V)),
                  false,
                  typeof(T).GetMethod("op_Equality")),
              new ParameterExpression[] { pe }));

            return (x);
        }
    }
}
