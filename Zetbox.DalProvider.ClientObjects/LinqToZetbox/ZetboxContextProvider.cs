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

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Utils;

    /// <summary>
    /// Provider for Zetbox Linq Provider. See http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx for details.
    /// </summary>
    internal class ZetboxContextProvider : IZetboxQueryProvider, IAsyncQueryProvider
    {
        /// <summary>
        /// The result type of this provider
        /// </summary>
        private InterfaceType _type;
        /// <summary>
        /// 
        /// </summary>
        private ZetboxContextImpl _context;

        private IProxy _proxy;
        private readonly IPerfCounter perfCounter;

        internal ZetboxContextProvider(ZetboxContextImpl ctx, InterfaceType ifType, IProxy proxy, IPerfCounter perfCounter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            _context = ctx;
            _type = ifType;
            _proxy = proxy;
            this.perfCounter = perfCounter;
        }

        private Expression TransformExpression(Expression e)
        {
            e = QueryTranslator.Translate(e);
            // TODO: Maybe merge constant evaluator into QueryTranslator
            e = ConstantEvaluator.PartialEval(e);
            return e;
        }

        #region Operations GetListOf/GetList/GetObject/InvokeServerMethod

        internal ZbTask<List<IDataObject>> GetListOfCallAsync(int ID, string propertyName)
        {
            // ResetState();
            var serviceTask = new ZbTask<Tuple<List<IDataObject>, List<IStreamable>>>(() =>
            {
                List<IStreamable> auxObjects;
                var result = _proxy.GetListOf(_type, ID, propertyName, out auxObjects).ToList();
                return new Tuple<List<IDataObject>, List<IStreamable>>(result, auxObjects);
            });

            return new ZbTask<List<IDataObject>>(serviceTask)
                .OnResult(t =>
                {
                    t.Result = new List<IDataObject>();

                    foreach (IDataObject obj in serviceTask.Result.Item1)
                    {
                        t.Result.Add((IDataObject)_context.AttachRespectingIsolationLevel(obj));
                    }

                    foreach (IPersistenceObject obj in serviceTask.Result.Item2)
                    {
                        _context.AttachRespectingIsolationLevel(obj);
                    }

                    _context.PlaybackNotifications();
                });
        }

        /// <summary>
        /// Performs a GetListCallAsync
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal ZbTask<List<T>> GetListCallAsync<T>(Expression query)
        {
            int objectCount = 0;
            var ticks = perfCounter.IncrementQuery(_type);

            if (Logging.Linq.IsInfoEnabled)
            {
                Logging.Linq.Info(query.ToString());
            }

            query = TransformExpression(query);

            ValidateServerExpression.CheckValid(query);

            var getListTask = new ZbTask<Tuple<List<IDataObject>, List<IStreamable>>>(() =>
                {
                    List<IStreamable> auxObjectsObjects;
                    var objects = _proxy.GetObjects(_context, _type, query, out auxObjectsObjects).ToList();
                    return new Tuple<List<IDataObject>, List<IStreamable>>(objects, auxObjectsObjects);
                });

            return new ZbTask<List<T>>(getListTask)
                .OnResult(t =>
                {
                    try
                    {
                        // prepare caches
                        foreach (IPersistenceObject obj in getListTask.Result.Item2)
                        {
                            _context.AttachRespectingIsolationLevel(obj);
                        }

                        var serviceResult = getListTask.Result.Item1.Select(obj => (IDataObject)_context.AttachRespectingIsolationLevel(obj)).ToList();
                        objectCount = serviceResult.Count;

                        // in the face of local changes, we have to re-query against local objects, to provide a consistent view of the objects
                        var result = _context.IsModified ? QueryFromLocalObjectsHack(_type, query).Cast<IDataObject>().ToList() : serviceResult;

                        _context.PlaybackNotifications();

                        t.Result = result.Cast<T>().ToList();
                    }
                    finally
                    {
                        perfCounter.DecrementQuery(_type, objectCount, ticks);
                    }
                });

        }

        /// <summary>
        /// Performs a GetListCallAsync but returns a single Object
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        private ZbTask<T> GetObjectCallAsync<T>(Expression query)
        {
            var ticks = perfCounter.IncrementQuery(_type);

            if (Logging.Linq.IsInfoEnabled)
            {
                Logging.Linq.Info(query.ToString());
            }

            // Visit
            query = TransformExpression(query);

            ValidateServerExpression.CheckValid(query);

            // Try to find a local object first
            var result = ExecuteFromLocalObjects<T>(query);

            ZbTask<T> task;
            // If nothing found local -> goto Server
            if (result == null)
            {
                ZbTask<Tuple<List<IDataObject>, List<IStreamable>>> getObjectTask = new ZbTask<Tuple<List<IDataObject>, List<IStreamable>>>(() =>
                {
                    List<IStreamable> auxObjects;
                    List<IDataObject> serviceResult = _proxy.GetObjects(_context, _type, query, out auxObjects).ToList();
                    return new Tuple<List<IDataObject>, List<IStreamable>>(serviceResult, auxObjects);
                })
                .OnResult(t =>
                {
                    // prepare caches
                    foreach (IPersistenceObject obj in t.Result.Item2)
                    {
                        _context.AttachRespectingIsolationLevel(obj);
                    }

                    foreach (IDataObject obj in t.Result.Item1)
                    {
                        result = (T)_context.AttachRespectingIsolationLevel(obj);
                    }

                    _context.PlaybackNotifications();
                });
                task = new ZbTask<T>(getObjectTask);
            }
            else
            {
                task = new ZbTask<T>(ZbTask.Synchron, () => default(T));
            }

            return task.OnResult(t =>
            {
                try
                {
                    if (result == null
                        && (query.IsMethodCallExpression("First")
                            || query.IsMethodCallExpression("Single")))
                    {
                        throw new InvalidOperationException("No element found");
                    }
                    else
                    {
                        t.Result = result;
                    }
                }
                finally
                {
                    perfCounter.DecrementQuery(_type, result == null ? 0 : 1, ticks);
                }
            });
        }

        #region Local Object handling
        private IList QueryFromLocalObjectsHack(InterfaceType ifType, Expression query)
        {
            MethodInfo mi = typeof(ZetboxContextProvider).GetMethod("QueryFromLocalObjects", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(ifType.Type);
            return (IList)mi.Invoke(this, new object[] { query });
        }

        private List<T> QueryFromLocalObjects<T>(Expression query)
        {
            var localObjects = _context.AttachedObjects.AsQueryable().Where(o => o != null && o.ObjectState != DataObjectState.Deleted).OfType<T>();
            var replacedQuery = new SourceReplacer<T>(localObjects).Visit(query);
            return localObjects.Provider.CreateQuery<T>(replacedQuery).ToList();
        }

        /// <summary>
        /// Always returns a value of T. May be null if nothing was found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        private T ExecuteFromLocalObjects<T>(Expression query)
        {
            var localObjects = _context.AttachedObjects.AsQueryable().Where(o => o != null && o.ObjectState != DataObjectState.Deleted).OfType<T>();
            var replacedQuery = new SourceReplacer<T>(localObjects).Visit(query) as MethodCallExpression;
            Expression resultQuery = replacedQuery;

            if (replacedQuery.IsMethodCallExpression("First"))
                resultQuery = Expression.Call(typeof(Queryable), "FirstOrDefault",
                    new Type[] { replacedQuery.Type }, replacedQuery.Arguments.ToArray());

            if (replacedQuery.IsMethodCallExpression("Single"))
                resultQuery = Expression.Call(typeof(Queryable), "SingleOrDefault",
                    new Type[] { replacedQuery.Type }, replacedQuery.Arguments.ToArray());

            return (T)localObjects.Provider.Execute<T>(resultQuery);
        }

        private class SourceReplacer<T> : ExpressionTreeTranslator
        {
            private readonly Expression _newSource;

            public SourceReplacer(IQueryable<T> newSource)
            {
                _newSource = newSource.Expression;
            }

            protected override Expression VisitConstant(ConstantExpression c)
            {
                if (c.Type.IsGenericType && typeof(ZetboxContextQuery<>).IsAssignableFrom(c.Type.GetGenericTypeDefinition()))
                    return _newSource;
                else
                    return base.VisitConstant(c);
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                return base.VisitMethodCall(m);
            }
        }
        #endregion

        #endregion

        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>)new ZetboxContextQuery<TElement>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null) { throw new ArgumentNullException("expression"); }

            Type elementType = expression.Type.FindElementTypes().Single(t => t != typeof(object));
            return (IQueryable)Activator.CreateInstance(typeof(ZetboxContextQuery<>)
                .MakeGenericType(elementType), new object[] { this, expression });
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return this.ExecuteAsync<TResult>(expression).Result;
        }

        public object Execute(Expression expression)
        {
            return this.ExecuteAsync(expression).Result;
        }

        public ZbTask<object> ExecuteAsync(Expression expression)
        {
            var task = GetObjectCallAsync<IDataObject>(expression);
            return new ZbTask<object>(task).OnResult(t => t.Result = task.Result);
        }

        public ZbTask<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return GetObjectCallAsync<TResult>(expression);
        }
        #endregion

        private class ValidateServerExpression : ExpressionTreeVisitor
        {
            public static void CheckValid(Expression e)
            {
                new ValidateServerExpression().Visit(e);
            }

            protected override void VisitMethodCall(MethodCallExpression m)
            {
                if (m.IsMethodCallExpression("Select") || m.IsMethodCallExpression("GroupBy"))
                {
                    throw new NotSupportedException("Projections and groupings cannot be transported to the server. Please use ToList().");
                }
                else if (m.IsMethodCallExpression(typeof(ZetboxContextQueryableExtensions)))
                {
                    // Lets serialize, server has to ensure security
                    m.Arguments.ForEach(a => Visit(a));
                }
                else if (m.IsMethodCallExpression(typeof(IQueryable)) || m.IsMethodCallExpression(typeof(Queryable)) || m.IsMethodCallExpression(typeof(IEnumerable)) || m.IsMethodCallExpression(typeof(string)))
                {
                    // Lets serialize, server has to ensure security
                    m.Arguments.ForEach(a => Visit(a));
                }
                else
                {
                    // Fail fast, if we know that the expression is invalid
                    throw new NotSupportedException(string.Format("Method Call '{0}' is not allowed", m.Method.Name));
                }

                // Do not call base - only first expression is important
            }
        }
    }
}
