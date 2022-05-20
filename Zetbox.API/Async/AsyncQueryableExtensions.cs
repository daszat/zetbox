using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace Zetbox.API.Async
{
    public static class AsyncQueryableExtensions
    {
        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> qry, Expression<Func<T, bool>> predicate)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<T>(Expression.Call(typeof(Queryable), "FirstOrDefault", new Type[] { typeof(T) }, new Expression[] { qry.Expression, Expression.Quote(predicate) }));
            }
            else
            {
                return Task.FromResult(qry.FirstOrDefault(predicate));
            }
        }

        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> qry)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<T>(Expression.Call(typeof(Queryable), "FirstOrDefault", new Type[] { typeof(T) }, new Expression[] { qry.Expression }));
            }
            else
            {
                return Task.FromResult(qry.FirstOrDefault());
            }
        }

        public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> qry, Expression<Func<T, bool>> predicate)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<T>(Expression.Call(typeof(Queryable), "SingleOrDefault", new Type[] { typeof(T) }, new Expression[] { qry.Expression, Expression.Quote(predicate) }));
            }
            else
            {
                return Task.FromResult(qry.SingleOrDefault(predicate));
            }
        }

        public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> qry)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<T>(Expression.Call(typeof(Queryable), "SingleOrDefault", new Type[] { typeof(T) }, new Expression[] { qry.Expression }));
            }
            else
            {
                return Task.FromResult(qry.SingleOrDefault());
            }
        }

        public static Task<bool> AnyAsync<T>(this IQueryable<T> qry, Expression<Func<T, bool>> predicate)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<bool>(Expression.Call(typeof(Queryable), "Any", new Type[] { typeof(T) }, new Expression[] { qry.Expression, Expression.Quote(predicate) }));
            }
            else
            {
                return Task.FromResult(qry.Any(predicate));
            }
        }

        public static Task<bool> AnyAsync<T>(this IQueryable<T> qry)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<bool>(Expression.Call(typeof(Queryable), "Any", new Type[] { typeof(T) }, new Expression[] { qry.Expression }));
            }
            else
            {
                return Task.FromResult(qry.Any());
            }
        }

        public static Task<int> CountAsync<T>(this IQueryable<T> qry, Expression<Func<T, bool>> predicate)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<int>(Expression.Call(typeof(Queryable), "Any", new Type[] { typeof(T) }, new Expression[] { qry.Expression, Expression.Quote(predicate) }));
            }
            else
            {
                return Task.FromResult(qry.Count(predicate));
            }
        }

        public static Task<int> CountAsync<T>(this IQueryable<T> qry)
        {
            if (qry.Provider is IAsyncQueryProvider)
            {
                return ((IAsyncQueryProvider)qry.Provider).ExecuteAsync<int>(Expression.Call(typeof(Queryable), "Any", new Type[] { typeof(T) }, new Expression[] { qry.Expression }));
            }
            else
            {
                return Task.FromResult(qry.Count());
            }
        }

        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> qry)
        {
            if (qry is IAsyncQueryable<T>)
            {
                var asyncQry = (IAsyncQueryable<T>)qry;
                var fetchTask = await asyncQry.GetEnumeratorAsync();
                var lst = new List<T>();
                while (fetchTask.MoveNext())
                {
                    lst.Add(fetchTask.Current);
                }
                return lst;
            }
            else
            {
                return qry.ToList<T>();
            }
        }

        public static async Task<IEnumerable> ToListAsync(this IQueryable qry)
        {
            if (qry is IAsyncQueryable)
            {
                var asyncQry = (IAsyncQueryable)qry;
                var fetchTask = await asyncQry.GetEnumeratorAsync();
                var lst = new List<object>();
                while (fetchTask.MoveNext())
                {
                    lst.Add(fetchTask.Current);
                }
                return lst;
            }
            else
            {
                var lst = new List<object>();
                foreach (var obj in qry)
                {
                    lst.Add(obj);
                }
                return lst;
            }
        }
    }
}
