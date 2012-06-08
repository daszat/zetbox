
namespace Zetbox.API
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Linq Extensions.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Adds a EqualityCondition to a Linq Expression Tree.
        /// </summary>
        /// <typeparam name="T">Typeparameter for IQueryable</typeparam>
        /// <typeparam name="V">Typeparameter for Equal Value</typeparam>
        /// <param name="queryable">IQueryable (Expression Tree)</param>
        /// <param name="propertyName">Propertyname to check</param>
        /// <param name="propertyValue">Value to check</param>
        /// <returns>IQueryable with this EqualityCondition</returns>
        public static IQueryable<T> AddEqualityCondition<T, V>(this IQueryable<T> queryable,
            string propertyName, V propertyValue)
        {
            PropertyInfo pi = typeof(T).GetProperty(propertyName);
            if (pi == null) throw new ArgumentOutOfRangeException("propertyName", "Property not found");
            ParameterExpression pe = Expression.Parameter(typeof(T), "p");

            IQueryable<T> x = queryable.Where<T>(
              Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(pe, pi),
                    Expression.Constant(propertyValue, typeof(V)),
                    false,
                    typeof(T).GetMethod("op_Equality")
                ),
              new ParameterExpression[] { pe }));

            return (x);
        }

        /// <summary>
        /// Appends a Expression Tree to a Linq Expression
        /// </summary>
        /// <param name="queryable">IQueryable (Expression Tree) to add filter</param>
        /// <param name="filter">Expression Tree</param>
        /// <returns>IQueryable with this Filter</returns>
        public static IQueryable AddFilter(this IQueryable queryable, Expression filter)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (filter == null) throw new ArgumentNullException("filter");

            List<IllegalExpression> illegal;
            if (!filter.IsLegal(out illegal))
            {
                throw new System.Security.SecurityException("Illegal LINQ Expression found in filter\n" +
                    string.Join("\n", illegal.Select(i => i.ToString()).ToArray()));
            }

            return queryable.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "Where",
                new Type[] { queryable.ElementType }, queryable.Expression, filter));
        }

        /// <summary>
        /// Appends a Expression Tree to a Linq Expression
        /// </summary>
        /// <typeparam name="T">Typeparameter for IQueryable</typeparam>
        /// <param name="queryable">IQueryable (Expression Tree) to add filter</param>
        /// <param name="filter">Expression Tree</param>
        /// <returns>IQueryable with this Filter</returns>
        public static IQueryable<T> AddFilter<T>(this IQueryable<T> queryable, Expression filter)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (filter == null) throw new ArgumentNullException("filter");

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

        public static IQueryable AddSelector(this IQueryable queryable, LambdaExpression selector, Type sourceType, Type resultType)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (selector == null) throw new ArgumentNullException("selector");
            if (sourceType == null) throw new ArgumentNullException("sourceType");
            if (resultType == null) throw new ArgumentNullException("resultType");

            return queryable.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "Select",
                    new Type[] { sourceType, resultType }, queryable.Expression, selector));
        }

        public static IQueryable AddOfType(this IQueryable queryable, Type t)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (t == null) throw new ArgumentNullException("t");

            return queryable.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "OfType",
                new Type[] { t }, queryable.Expression));
        }

        public static IQueryable<T> AddOfType<T>(this IQueryable queryable, Type t)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (t == null) throw new ArgumentNullException("t");

            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "OfType",
                new Type[] { t }, queryable.Expression));
        }

        public static IQueryable AddCast(this IQueryable queryable, Type t)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (t == null) throw new ArgumentNullException("t");

            return queryable.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "Cast",
                new Type[] { t }, queryable.Expression));
        }

        public static IQueryable<T> AddCast<T>(this IQueryable queryable, Type t)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (t == null) throw new ArgumentNullException("t");

            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "Cast",
                new Type[] { t }, queryable.Expression));
        }

        /// <summary>
        /// Appends a expression tree OrderBy to a linq expression
        /// </summary>
        /// <typeparam name="T">Typeparameter for IQueryable</typeparam>
        /// <param name="queryable">IQueryable (Expression Tree) to add filter</param>
        /// <param name="orderBy">OrderBy expression tree</param>
        /// <returns>IQueryable with this OrderBy expression</returns>
        public static IQueryable<T> AddOrderBy<T>(this IQueryable<T> queryable, Expression orderBy)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (orderBy == null) throw new ArgumentNullException("orderBy");

            Type type = null;

            if (orderBy.NodeType != ExpressionType.Quote)
            {
                orderBy = Expression.Quote(orderBy);
            }

            type = orderBy.Type.GetGenericArguments()[0].GetGenericArguments()[1];
            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "OrderBy",
                new Type[] { queryable.ElementType, type },
                queryable.Expression, orderBy));
        }

        /// <summary>
        /// Appends a expression tree OrderBy to a linq expression
        /// </summary>
        /// <typeparam name="T">Typeparameter for IQueryable</typeparam>
        /// <param name="queryable">IQueryable (Expression Tree) to add filter</param>
        /// <param name="orderBy">OrderBy expression tree</param>
        /// <returns>IQueryable with this OrderBy expression</returns>
        public static IQueryable<T> AddThenBy<T>(this IQueryable<T> queryable, Expression orderBy)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (orderBy == null) throw new ArgumentNullException("orderBy");

            Type type = null;

            if (orderBy.NodeType != ExpressionType.Quote)
            {
                orderBy = Expression.Quote(orderBy);
            }

            type = orderBy.Type.GetGenericArguments()[0].GetGenericArguments()[1];
            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "ThenBy",
                new Type[] { queryable.ElementType, type },
                queryable.Expression, orderBy));
        }

        /// <summary>
        /// Appends a expression tree OrderByDescending to a linq expression
        /// </summary>
        /// <typeparam name="T">Typeparameter for IQueryable</typeparam>
        /// <param name="queryable">IQueryable (Expression Tree) to add filter</param>
        /// <param name="orderBy">OrderByDescending expression Tree</param>
        /// <returns>IQueryable with this OrderByDescending expression</returns>
        public static IQueryable<T> AddOrderByDescending<T>(this IQueryable<T> queryable, Expression orderBy)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (orderBy == null) throw new ArgumentNullException("orderBy");

            Type type = null;

            if (orderBy.NodeType != ExpressionType.Quote)
            {
                orderBy = Expression.Quote(orderBy);
            }

            type = orderBy.Type.GetGenericArguments()[0].GetGenericArguments()[1];
            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "OrderByDescending",
                new Type[] { queryable.ElementType, type },
                queryable.Expression, orderBy));
        }

        /// <summary>
        /// Appends a expression tree OrderBy Descending to a Linq Expression
        /// </summary>
        /// <typeparam name="T">Typeparameter for IQueryable</typeparam>
        /// <param name="queryable">IQueryable (Expression Tree) to add filter</param>
        /// <param name="orderBy">OrderByDescending expression tree</param>
        /// <returns>IQueryable with this OrderByDescending expression</returns>
        public static IQueryable<T> AddThenByDescending<T>(this IQueryable<T> queryable, Expression orderBy)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (orderBy == null) throw new ArgumentNullException("orderBy");

            Type type = null;

            if (orderBy.NodeType != ExpressionType.Quote)
            {
                orderBy = Expression.Quote(orderBy);
            }

            type = orderBy.Type.GetGenericArguments()[0].GetGenericArguments()[1];
            return queryable.Provider.CreateQuery<T>(
                Expression.Call(typeof(Queryable), "ThenByDescending",
                new Type[] { queryable.ElementType, type },
                queryable.Expression, orderBy));
        }

        #region // http://stackoverflow.com/questions/110314/linq-to-entities-building-where-clauses-to-test-collections-within-a-many-to-ma#131551
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public class ParameterRebinder : ExpressionTreeTranslator
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;
            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }
            protected override ParameterExpression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (merge == null) throw new ArgumentNullException("merge");

            // build parameter map (from parameters of second to parameters of first)                
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with parameters from the first                
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression                 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static LambdaExpression Compose(this LambdaExpression first, LambdaExpression second, Func<Expression, Expression, Expression> merge)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (merge == null) throw new ArgumentNullException("merge");

            // build parameter map (from parameters of second to parameters of first)                
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with parameters from the first                
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression                 
            return Expression.Lambda(first.Type, merge(first.Body, secondBody), first.Parameters);
        }
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            return first.Compose(second, Expression.AndAlso);
        }
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            return first.Compose(second, Expression.OrElse);
        }

        public static LambdaExpression OrElse(this LambdaExpression first, LambdaExpression second)
        {
            if (first == null) throw new ArgumentNullException("first");
            return first.Compose(second, Expression.OrElse);
        }
        #endregion

        /// <summary>
        /// Returns the Value of a Constant or Member Expression
        /// TODO: Wer benutzt das? Ich glaub nur der ClientContext beim Auswerten von Take. Das könnte man vielleicht mit dem Constant Evaluatoer machen
        /// </summary>
        /// <typeparam name="TYPE">Values type</typeparam>
        /// <param name="e">Expression to get Value</param>
        /// <returns>Value of the given Expression</returns>
        public static TYPE GetExpressionValue<TYPE>(this Expression e)
        {
            if (e == null) { throw new ArgumentNullException("e"); }

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

        public static Expression StripQuotes(this Expression e)
        {
            if (e == null) { throw new ArgumentNullException("e"); }

            while (e.NodeType == ExpressionType.Quote)
                e = ((UnaryExpression)e).Operand;
            return e;
        }

        public static bool IsMethodCallExpression(this Expression e, string methodName)
        {
            if (e == null) { throw new ArgumentNullException("e"); }

            return e.NodeType == ExpressionType.Call &&
                ((MethodCallExpression)e).Method.Name == methodName &&
                ((MethodCallExpression)e).Method.DeclaringType == typeof(Queryable);
        }

        public static bool IsMethodCallExpression(this Expression e, string methodName, Type type)
        {
            if (e == null) { throw new ArgumentNullException("e"); }

            return e.NodeType == ExpressionType.Call &&
                ((MethodCallExpression)e).Method.Name == methodName &&
                ((MethodCallExpression)e).Method.DeclaringType == type;
        }

        public static bool IsMethodCallExpression(this Expression e, Type type)
        {
            if (e == null) { throw new ArgumentNullException("e"); }

            return e.NodeType == ExpressionType.Call &&
                ((MethodCallExpression)e).Method.DeclaringType == type;
        }

        public static StringBuilder Trace(this Expression e)
        {
            StringBuilder sb = new StringBuilder();
            TraceExpression(e, sb, 0);
            return sb;
        }

        public static void TraceExpression(Expression e, StringBuilder sb, int indent)
        {
            if (e == null) { throw new ArgumentNullException("e"); }
            if (sb == null) { throw new ArgumentNullException("sb"); }

            Type t = e.GetType();
            string indentString = Zetbox.API.Helper.Indent(indent);
            sb.Append(indentString);
            sb.AppendLine(t.FullName);

            PropertyInfo[] propertyInfos = null;
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Expression<>))
            {
                propertyInfos = t.BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            else
            {
                propertyInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }

            sb.AppendLine(indentString + "{");
            foreach (PropertyInfo pInfo in propertyInfos)
            {
                TraceExpressionAttributes(e, pInfo, sb, indent + 2);
            }
            sb.AppendLine(indentString + "}");
        }

        private static void TraceExpressionAttributes(object obj, PropertyInfo pInfo, StringBuilder sb, int indent)
        {
            string indentString = Zetbox.API.Helper.Indent(indent);
            sb.Append(indentString);
            sb.AppendFormat("[{0}] {1}: ", pInfo.PropertyType.Name, pInfo.Name);

            object value = pInfo.GetValue(obj, null);
            if (value != null)
            {
                if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>))
                {
                    int count = (int)value.GetType().InvokeMember("get_Count", BindingFlags.InvokeMethod, null, value, null, CultureInfo.InvariantCulture);
                    if (count == 0)
                    {
                        sb.AppendLine("empty");
                    }
                    else
                    {
                        sb.AppendLine(count.ToString() + " elements");
                        sb.AppendLine(indentString + "(");
                        foreach (object e in (IEnumerable)value)
                        {
                            if (e is Expression)
                            {
                                TraceExpression((Expression)e, sb, indent + 2);
                            }
                            else if (e is MemberAssignment)
                            {
                                TraceExpression(((MemberAssignment)e).Expression, sb, indent + 2);
                            }
                        }
                        sb.AppendLine(indentString + ")");
                    }
                }
                else if (value is Expression)
                {
                    sb.AppendLine(((Expression)value).NodeType.ToString());
                    sb.AppendLine(indentString + "{");
                    TraceExpression((Expression)value, sb, indent + 2);
                    sb.AppendLine(indentString + "}");
                }
                else if (value is MethodInfo)
                {
                    MethodInfo minfo = value as MethodInfo;
                    sb.AppendLine("\"" + minfo.Name + "\"");
                }
                else if (value is Type)
                {
                    Type t = value as Type;
                    sb.AppendLine("\"" + t.FullName + "\"");
                }
                else
                {
                    sb.AppendLine("\"" + value.ToString() + "\"");
                }
            }
            else
            {
                sb.AppendLine("null");
            }
        }

        public static IQueryable AsQueryable(this IEnumerable lst, Type t)
        {
            MethodInfo mi = typeof(LinqExtensions).FindGenericMethod("AsTypedQueryable", new Type[] { t }, new Type[] { typeof(IEnumerable) });
            return (IQueryable)mi.Invoke(null, new object[] { lst });
        }

        public static IQueryable AsTypedQueryable<T>(this IEnumerable lst)
        {
            return lst.Cast<T>().AsQueryable();
        }
    }
}
