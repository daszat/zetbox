using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Zetbox.API.Async
{
    public static class AsyncQueryableExtensions
    {
        public static ZbTask<T> SingleOrDefaultAsync<T>(this IQueryable<T> qry)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<T>(Expression.Call(typeof(Queryable), "SingleOrDefault", new Type[] { typeof(T) }, new Expression[] { qry.Expression }));
            }
            else
            {
                return new ZbTask<T>(ZbTask.Synchron, () => qry.SingleOrDefault());
            }
        }

        public static ZbTask<T> SingleOrDefaultAsync<T>(this IQueryable<T> qry, Expression<Func<T, bool>> predicate)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<T>(Expression.Call(typeof(Queryable), "SingleOrDefault", new Type[] { typeof(T) }, new Expression[] { qry.Expression, Expression.Quote(predicate) }));
            }
            else
            {
                return new ZbTask<T>(ZbTask.Synchron, () => qry.SingleOrDefault(predicate));
            }
        }
    }
}
