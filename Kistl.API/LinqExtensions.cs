using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Kistl.API
{
    /// <summary>
    /// Linq Extensions. Aus dem Indischen Netz gefladert. 
    /// Könnte als Demo für Parameter Suchen herhalten.
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

        public static IQueryable<T> AddFilter<T>(this IQueryable<T> queryable, Expression filter)
        {
            List<IllegalExpression> illegal;
            if (!filter.IsLegal(out illegal))
            {
                throw new System.Security.SecurityException("Illegal LINQ Expression found in filter\n" +
                    string.Join("\n", illegal.Select(i => i.ToString()).ToArray()));
            }

            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "Where",
                new Type[] { queryable.ElementType }, queryable.Expression, filter));
        }

        public static TYPE GetExpressionValue<TYPE>(this Expression e)
        {
            if (e is ConstantExpression)
            {
                return (TYPE)(e as ConstantExpression).Value;
            }
            else if (e is MemberExpression)
            {
                MemberExpression me = e as MemberExpression;

                if (me.Member is PropertyInfo)
                    return (TYPE)(me.Member as PropertyInfo).GetValue((me.Expression as ConstantExpression).Value, null);
                else if (me.Member is FieldInfo)
                    return (TYPE)(me.Member as FieldInfo).GetValue((me.Expression as ConstantExpression).Value);
                else
                    throw new NotSupportedException(string.Format("Member of MemberExpression is not supported: {0}", e.ToString()));
            }
            else
            {
                throw new NotSupportedException(string.Format("Unable to get Value, Expression is not supported: {0}", e.ToString()));
            }
        }
    }
}
