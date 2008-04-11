using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;

namespace API.Tests
{
    [Serializable]
    public class TestQuery<T> : IOrderedQueryable<T>
    {
        [NonSerialized]
        private Expression _expression = null;
        private TestQueryProvider<T> _provider = null;

        public TestQuery()
        {
            _expression = System.Linq.Expressions.Expression.Constant(this);
            _provider = new TestQueryProvider<T>();
        }

        public TestQuery(TestQueryProvider<T> provider, Expression e)
        {
            _expression = e;
            _provider = provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return null;
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return null;
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression
        {
            get { return _expression; }
        }

        public IQueryProvider Provider
        {
            get { return _provider; }
        }
    }

    [Serializable]
    public class TestQueryProvider<T> : IQueryProvider
    {
        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestQuery<TElement>(this as TestQueryProvider<TElement>, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestQuery<T>(this, expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return default(TResult);
        }

        public object Execute(Expression expression)
        {
            return null;
        }

        #endregion
    }
}
