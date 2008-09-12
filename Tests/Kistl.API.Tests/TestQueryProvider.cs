using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;

namespace Kistl.API.Tests
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
            if (provider == null) throw new ArgumentNullException("provider");
            if (e == null) throw new ArgumentNullException("e");
            _expression = e;
            _provider = provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> result = (IEnumerable<T>)_provider.Execute<List<T>>(this._expression);
            if (result != null)
                return result.GetEnumerator();
            else
                return null;
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            IEnumerable result = ((IEnumerable)_provider.Execute(this._expression));
            if (result != null)
                return result.GetEnumerator();
            else
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
    public class TestQueryProvider<T> : Kistl.API.ExpressionTreeVisitor, IQueryProvider
    {
        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return new TestQuery<TElement>(new TestQueryProvider<TElement>(), expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return new TestQuery<T>(this, expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            try
            {
                return (TResult)(this as IQueryProvider).Execute(expression);
            }
            catch
            {
                return default(TResult);
            }
        }

        public object Execute(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            this.Visit(expression);

            List<TestDataObject> result = new List<TestDataObject>();
            result.Add(new TestDataObject__Implementation__() { ID = 1, StringProperty = "First", IntProperty = 10, BoolProperty = true, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 2, StringProperty = "Second", IntProperty = 20, BoolProperty = false, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 3, StringProperty = "Third", IntProperty = 30, BoolProperty = true, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 4, StringProperty = "Fourth", IntProperty = 40, BoolProperty = false, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 5, StringProperty = "Fith", IntProperty = 50, BoolProperty = true, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 6, StringProperty = "Sixth", IntProperty = 60, BoolProperty = false, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 7, StringProperty = "Seventh", IntProperty = 70, BoolProperty = true, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 8, StringProperty = "Eightth", IntProperty = 80, BoolProperty = false, TestField = "test" });
            result.Add(new TestDataObject__Implementation__() { ID = 9, StringProperty = "Nineth", IntProperty = 90, BoolProperty = true, TestField = "test" });
            return result;
        }

        #endregion
    }
}
