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
    using System.Threading.Tasks;
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

        private async Task<Expression> TransformExpression(Expression e)
        {
            e = await QueryTranslator.Translate(e);
            // TODO: Maybe merge constant evaluator into QueryTranslator
            e = await ConstantEvaluator.PartialEval(e);
            return e;
        }

        #region Operations GetListOf/GetList/GetObject/InvokeServerMethod

        internal async Task<List<IDataObject>> GetListOfCallAsync(int ID, string propertyName)
        {
            // ResetState();
            var serviceTask = await _proxy.GetListOf(_type, ID, propertyName);

            if (_context.IsDisposed) return new List<IDataObject>();

            _context.RecordNotifications();
            try
            {
                serviceTask.Item2.Cast<IPersistenceObject>().ForEach(obj => _context.AttachRespectingIsolationLevel(obj));
                return serviceTask.Item1.Select(obj => (IDataObject)_context.AttachRespectingIsolationLevel(obj)).ToList();
            }
            finally
            {
                _context.PlaybackNotifications();
            }
        }

        /// <summary>
        /// Performs a GetListCallAsync
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal async Task<List<T>> GetListCallAsync<T>(Expression query)
        {
            if (!typeof(T).IsIPersistenceObject()) throw new NotSupportedException("Server-side projections are not supported. Use a server-side method or a client-side projection");

            int objectCount = 0;
            var ticks = perfCounter.IncrementQuery(_type);
            try
            {
                if (Logging.Linq.IsInfoEnabled)
                {
                    Logging.Linq.Info(query.ToString());
                }

                query = await TransformExpression(query);

                ValidateServerExpression.CheckValid(query);

                var ftDetector = new FulltextDetector();
                ftDetector.Visit(query);

                var getListTask = await _proxy.GetObjects(_context, _type, query);

                if (_context.IsDisposed) return new List<T>();

                _context.RecordNotifications();

                // prepare caches
                getListTask.Item2.Cast<IPersistenceObject>().ForEach(obj => _context.AttachRespectingIsolationLevel(obj));
                var serviceResult = getListTask.Item1.Select(obj => (IDataObject)_context.AttachRespectingIsolationLevel(obj)).ToList();
                objectCount = serviceResult.Count;

                // in the face of local changes, we have to re-query against local objects, to provide a consistent view of the objects
                var result = _context.IsModified && !ftDetector.IsFulltext
                           ? QueryFromLocalObjectsHack(_type, query).Cast<IDataObject>().ToList()
                           : serviceResult;

                return result.Cast<T>().ToList();
            }
            finally
            {
                _context.PlaybackNotifications();
                perfCounter.DecrementQuery(_type, objectCount, ticks);
            }
        }

        /// <summary>
        /// Performs a GetListCallAsync but returns a single Object
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        private async Task<T> GetObjectCallAsync<T>(Expression query)
        {
            if (!typeof(T).IsIPersistenceObject()) throw new NotSupportedException("Server-side aggregations are not supported. Use a server-side method or a client-side aggregation");

            var ticks = perfCounter.IncrementQuery(_type);
            T result = default(T);
            try
            {
                if (Logging.Linq.IsInfoEnabled)
                {
                    Logging.Linq.Info(query.ToString());
                }

                // Visit
                query = await TransformExpression(query);

                ValidateServerExpression.CheckValid(query);

                // Try to find a local object first
                result = await ExecuteFromLocalObjects<T>(query);

                // If nothing found local -> goto Server
                if (result == null)
                {
                    var serviceResult = await _proxy.GetObjects(_context, _type, query);
                    if (_context.IsDisposed) return default(T);

                    _context.RecordNotifications();
                    try
                    {
                        serviceResult.Item2.Cast<IPersistenceObject>().ForEach(obj => _context.AttachRespectingIsolationLevel(obj));
                        result = (T)serviceResult.Item1.Select(obj => (IDataObject)_context.AttachRespectingIsolationLevel(obj)).ToList().FirstOrDefault();
                    }
                    finally
                    {
                        _context.PlaybackNotifications();
                    }
                }
                else
                {
                    result = default(T);
                }

                if (result == null
                    && (query.IsMethodCallExpression("First")
                        || query.IsMethodCallExpression("Single")))
                {
                    throw new InvalidOperationException("No element found");
                }
                else
                {
                    return result;
                }
            }
            finally
            {
                perfCounter.DecrementQuery(_type, result == null ? 0 : 1, ticks);
            }
        }

        #region Local Object handling
        private IList QueryFromLocalObjectsHack(InterfaceType ifType, Expression query)
        {
            MethodInfo mi = typeof(ZetboxContextProvider).GetMethod("QueryFromLocalObjects", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(ifType.Type);
            return (IList)mi.Invoke(this, new object[] { query });
        }

        private async Task<List<T>> QueryFromLocalObjects<T>(Expression query)
        {
            var localObjects = _context.AttachedObjects.AsQueryable().Where(o => o != null && o.ObjectState != DataObjectState.Deleted).OfType<T>();
            var replacedQuery = await new SourceReplacer<T>(localObjects).Visit(query);
            return localObjects.Provider.CreateQuery<T>(replacedQuery).ToList();
        }

        /// <summary>
        /// Always returns a value of T. May be null if nothing was found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<T> ExecuteFromLocalObjects<T>(Expression query)
        {
            var localObjects = _context.AttachedObjects.AsQueryable().Where(o => o != null && o.ObjectState != DataObjectState.Deleted).OfType<T>();
            var replacedQuery = (await new SourceReplacer<T>(localObjects).Visit(query)) as MethodCallExpression;
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

            protected override async Task<Expression> VisitConstant(ConstantExpression c)
            {
                if (c.Type.IsGenericType && typeof(ZetboxContextQuery<>).IsAssignableFrom(c.Type.GetGenericTypeDefinition()))
                    return _newSource;
                else
                    return await base.VisitConstant(c);
            }

            protected override async Task<Expression> VisitMethodCall(MethodCallExpression m)
            {
                return await base.VisitMethodCall(m);
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
            var task = Task.Run(async () => await this.ExecuteAsync<TResult>(expression));
            task.Wait();
            return task.Result;
        }

        public object Execute(Expression expression)
        {
            var task = Task.Run(async () => await this.ExecuteAsync(expression));
            task.Wait();
            return task.Result;
        }

        public async Task<object> ExecuteAsync(Expression expression)
        {
            return await GetObjectCallAsync<IDataObject>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression)
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
                else if (m.IsMethodCallExpression(typeof(IQueryable))
                      || m.IsMethodCallExpression(typeof(Queryable))
                      || m.IsMethodCallExpression(typeof(IEnumerable))
                      || m.IsMethodCallExpression(typeof(Enumerable))
                      || m.IsMethodCallExpression(typeof(string)))
                {
                    // Lets serialize, server has to ensure security
                    m.Arguments.ForEach(a => Visit(a));
                }
                else
                {
                    // Fail fast, if we know that the expression is invalid
                    throw new NotSupportedException(string.Format("Method Call '{0}.{1}' is not allowed", m.Method.DeclaringType.FullName, m.Method.Name));
                }

                // Do not call base - only first expression is important
            }
        }
    }
}
