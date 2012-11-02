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
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.API.Async;

    /// <summary>
    /// Provider for Zetbox Linq Provider. See http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx for details.
    /// </summary>
    public class ZetboxContextProvider : ExpressionTreeVisitor, IZetboxQueryProvider, IAsyncQueryProvider
    {
        /// <summary>
        /// The result type of this provider
        /// </summary>
        private InterfaceType _type;
        /// <summary>
        /// 
        /// </summary>
        private ZetboxContextImpl _context;

        /// <summary>
        /// 
        /// </summary>
        private int _maxListCount = API.Helper.MAXLISTCOUNT;

        /// <summary>
        /// 
        /// </summary>
        private bool? _eagerLoadLists = null;

        /// <summary>
        /// Filter Expression for GetList SearchType.
        /// </summary>
        private LinkedList<Expression> _filter = null;
        /// <summary>
        /// OrderBy Expression for GetList SearchType.
        /// </summary>
        private LinkedList<OrderBy> _orderBy = null;

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

        private void ResetState()
        {
            _maxListCount = API.Helper.MAXLISTCOUNT;
            _eagerLoadLists = null;
            _filter = null;
            _orderBy = null;
        }

        #region CallService

        private Expression TransformExpression(Expression e)
        {
            e = QueryTranslator.Translate(e);
            // TODO: Maybe merge constant evaluator into QueryTranslator
            e = ConstantEvaluator.PartialEval(e);
            return e;
        }
        #endregion

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
        /// <param name="e"></param>
        /// <returns></returns>
        internal ZbTask<List<T>> GetListCallAsync<T>(Expression e)
        {
            int objectCount = 0;
            var ticks = perfCounter.IncrementQuery(_type);

            ResetState();

            if (Logging.Linq.IsInfoEnabled)
            {
                Logging.Linq.Info(e.ToString());
            }

            e = TransformExpression(e);
            Visit(e);

            var getListTask = new ZbTask<Tuple<List<IDataObject>, List<IStreamable>>>(() =>
                {
                    List<IStreamable> auxObjects;
                    var list = _proxy.GetList(_type, _maxListCount, _eagerLoadLists ?? _maxListCount == 1, _filter, _orderBy, out auxObjects).ToList();
                    return new Tuple<List<IDataObject>, List<IStreamable>>(list, auxObjects);
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
                        var result = _context.IsModified ? QueryFromLocalObjectsHack(_type).Cast<IDataObject>().ToList() : serviceResult;

                        _context.PlaybackNotifications();

                        // Projection
                        if (e.IsMethodCallExpression("Select"))
                        {
                            // Get Selector and SourceType
                            // Sourcetype should be of type IDataObject
                            MethodCallExpression me = e as MethodCallExpression;
                            LambdaExpression selector = (LambdaExpression)me.Arguments[1].StripQuotes();
                            Type sourceType = selector.Parameters[0].Type;

                            // AddSelector needs a list of the correct type, so we add a cast before selecting
                            // TODO: revisit with covariant structures
                            IQueryable selectResult = result.AsQueryable().AddCast(sourceType).AddSelector(selector, sourceType, selector.Body.Type); // typeof(T).FindElementTypes().First());
                            t.Result = selectResult.Cast<T>().ToList();
                        }
                        else
                        {
                            t.Result = result.Cast<T>().ToList();
                        }
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
        /// <param name="e"></param>
        /// <returns>A Object an Expeption, if the Object was not found.</returns>
        private ZbTask<T> GetObjectCallAsync<T>(Expression e)
        {
            var ticks = perfCounter.IncrementQuery(_type);
            ResetState();

            if (Logging.Linq.IsInfoEnabled)
            {
                Logging.Linq.Info(e.ToString());
            }

            // Visit
            e = TransformExpression(e);
            Visit(e);

            // Try to find a local object first
            var result = QueryFromLocalObjects<T>();

            ZbTask<T> task;
            // If nothing found local -> goto Server
            if (result.Count == 0)
            {
                ZbTask<Tuple<List<IDataObject>, List<IStreamable>>> getListTask = new ZbTask<Tuple<List<IDataObject>, List<IStreamable>>>(() =>
                {
                    List<IStreamable> auxObjects;
                    List<IDataObject> serviceResult = _proxy.GetList(_type, _maxListCount, _eagerLoadLists ?? _maxListCount == 1, _filter, _orderBy, out auxObjects).ToList();
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
                        result.Add((T)_context.AttachRespectingIsolationLevel(obj));
                    }

                    _context.PlaybackNotifications();
                });
                task = new ZbTask<T>(getListTask);
            }
            else
            {
                task = new ZbTask<T>(ZbTask.Synchron, () => default(T));
            }

            return task.OnResult(t =>
            {
                try
                {
                    if (e.IsMethodCallExpression("First"))
                    {
                        t.Result = result.First();
                    }
                    else if (e.IsMethodCallExpression("FirstOrDefault"))
                    {
                        t.Result = result.FirstOrDefault();
                    }
                    else if (e.IsMethodCallExpression("Single"))
                    {
                        t.Result = result.Single();
                    }
                    else if (e.IsMethodCallExpression("SingleOrDefault"))
                    {
                        t.Result = result.SingleOrDefault();
                    }
                    else
                    {
                        throw new NotSupportedException("Expression is not supported");
                    }
                }
                finally
                {
                    perfCounter.DecrementQuery(_type, result.Count, ticks);
                }
            });
        }

        #region Local Object handling
        private IList QueryFromLocalObjectsHack(InterfaceType ifType)
        {
            MethodInfo mi = typeof(ZetboxContextProvider).GetMethod("QueryFromLocalObjects", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(ifType.Type);
            return (IList)mi.Invoke(this, new object[] { });
        }

        private List<T> QueryFromLocalObjects<T>()
        {
            List<T> result = new List<T>();
            var list = _context.AttachedObjects.AsQueryable().Where(o => o != null && o.ObjectState != DataObjectState.Deleted).OfType<T>();
            if (_filter != null) _filter.ForEach(f => list = list.AddFilter(f));
            list.ForEach<T>(result.Add);
            return result;
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

        #endregion

        #region Visits
        protected override void VisitMethodCall(MethodCallExpression m)
        {
            if (m.IsMethodCallExpression("Where"))
            {
                if (_filter == null) _filter = new LinkedList<Expression>();
                _filter.AddFirst(m.Arguments[1]);
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("ThenBy"))
            {
                if (_orderBy == null) _orderBy = new LinkedList<OrderBy>();
                _orderBy.AddFirst(new OrderBy(OrderByType.ASC, m.Arguments[1]));
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("OrderByDescending") || m.IsMethodCallExpression("ThenByDescending"))
            {
                if (_orderBy == null) _orderBy = new LinkedList<OrderBy>();
                _orderBy.AddFirst(new OrderBy(OrderByType.DESC, m.Arguments[1]));
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("Select"))
            {
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("Take"))
            {
                _maxListCount = m.Arguments[1].GetExpressionValue<int>();
                base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("First") ||
                        m.IsMethodCallExpression("FirstOrDefault") ||
                        m.IsMethodCallExpression("Single") ||
                        m.IsMethodCallExpression("SingleOrDefault")
                )
            {
                _maxListCount = 1;
                if (m.Arguments.Count == 2)
                {
                    if (_filter == null) _filter = new LinkedList<Expression>();
                    _filter.AddFirst(m.Arguments[1]);
                }
                else
                    base.Visit(m.Arguments[0]);
            }
            else if (m.IsMethodCallExpression("WithEagerLoading", typeof(ZetboxContextQueryableExtensions)))
            {
                _eagerLoadLists = true;
            }
            else if (m.IsMethodCallExpression(typeof(IQueryable)))
            {
                // Lets serialize, server has to ensure security
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else if (m.IsMethodCallExpression(typeof(Queryable)))
            {
                // Lets serialize, server has to ensure security
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else if (m.IsMethodCallExpression(typeof(IEnumerable)))
            {
                // Lets serialize, server has to ensure security
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else if (m.IsMethodCallExpression(typeof(string)))
            {
                m.Arguments.ForEach(a => base.Visit(a));
            }
            else
            {
                throw new NotSupportedException(string.Format("Method Call '{0}' is not allowed", m.Method.Name));
            }

            // Do not call base - only first expression is important
        }
        #endregion

        public ZbTask<object> ExecuteAsync(Expression expression)
        {
            var task = GetObjectCallAsync<IDataObject>(expression);
            return new ZbTask<object>(task).OnResult(t => t.Result = task.Result);
        }

        public ZbTask<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return GetObjectCallAsync<TResult>(expression);
        }
    }
}
