
namespace Kistl.API.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Kistl.App.Base;
    
    // http://msdn.microsoft.com/en-us/library/bb549414.aspx
    // The Execute method executes queries that return a single value 
    // (instead of an enumerable sequence of values). Expression trees that represent queries 
    // that return enumerable results are executed when the IQueryable<(Of <(T>)>) object that 
    // contains the expression tree is enumerated. 
    // The Queryable standard query operator methods that return singleton results call Execute. 
    // They pass it a MethodCallExpression that represents a LINQ query. 
    // http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx

    public sealed class QueryTranslator<T> : IOrderedQueryable<T>
    {
        private readonly Expression _expression = null;
        private readonly QueryTranslatorProvider<T> _provider = null;

        public QueryTranslator(QueryTranslatorProvider<T> provider)
        {
            _expression = Expression.Constant(this);
            _provider = provider;
        }

        public QueryTranslator(QueryTranslatorProvider<T> provider, Expression e)
        {
            if (e == null) throw new ArgumentNullException("e");
            _expression = e;
            _provider = provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_provider.ExecuteEnumerable(this._expression)).GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _provider.ExecuteEnumerable(this._expression).GetEnumerator();
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
}