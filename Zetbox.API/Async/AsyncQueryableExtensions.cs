using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

        public static ZbTask<List<T>> ToListAsync<T>(this IQueryable<T> qry)
        {
            if (qry is IAsyncQueryable<T>)
            {
                var asyncQry = (IAsyncQueryable<T>)qry;
                var fetchTask = asyncQry.GetEnumeratorAsync();
                return new ZbTask<List<T>>(fetchTask)
                    .OnResult(t =>
                    {
                        t.Result = new List<T>();
                        while (fetchTask.Result.MoveNext()) {
                            t.Result.Add(fetchTask.Result.Current);
                        }
                    });
            }
            else
            {
                return new ZbTask<List<T>>(ZbTask.Synchron, () => qry.ToList());
            }
        }
    }
}
