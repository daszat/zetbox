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

namespace Zetbox.API.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Zetbox.API.Common;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    // http://msdn.microsoft.com/en-us/library/bb549414.aspx
    // The Execute method executes queries that return a single value 
    // (instead of an enumerable sequence of values). Expression trees that represent queries 
    // that return enumerable results are executed when the IQueryable<(Of <(T>)>) object that 
    // contains the expression tree is enumerated. 
    // The Queryable standard query operator methods that return singleton results call Execute. 
    // They pass it a MethodCallExpression that represents a LINQ query. 
    // http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx
    public abstract class QueryTranslatorProvider<T>
        : ExpressionTreeTranslator, IZetboxQueryProvider
    {
        protected readonly IMetaDataResolver MetaDataResolver;
        protected readonly Identity Identity;
        protected readonly IQueryable Source;
        protected readonly IZetboxContext Ctx;
        protected readonly InterfaceType.Factory IftFactory;
        protected readonly IPerfCounter perfCounter;

        /// <summary>
        /// Get a sub-provider using the same components as this provider but working on a different type.
        /// </summary>
        /// <typeparam name="TElement">the sub-provider's element type</typeparam>
        /// <returns></returns>
        protected abstract QueryTranslatorProvider<TElement> GetSubProvider<TElement>();

        /// <summary>
        /// Initializes a new instances of the QueryTranslatorProvider class 
        /// with the specified components. If no Identity is passed, 
        /// unrestricted queries are executed.
        /// </summary>
        /// <param name="metaDataResolver"></param>
        /// <param name="identity">the user who is making the query; if null, a unrestricted query is executed.</param>
        /// <param name="source"></param>
        /// <param name="ctx"></param>
        /// <param name="iftFactory"></param>
        /// <param name="perfCounter"></param>
        protected QueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, IZetboxContext ctx, InterfaceType.Factory iftFactory, IPerfCounter perfCounter)
        {
            if (metaDataResolver == null) { throw new ArgumentNullException("metaDataResolver"); }
            if (source == null) { throw new ArgumentNullException("source"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");

            this.MetaDataResolver = metaDataResolver;
            this.Identity = identity;
            this.Source = source;
            this.Ctx = ctx;
            this.IftFactory = iftFactory;
            this.perfCounter = perfCounter;
        }

        protected abstract string ImplementationSuffix { get; }

        public bool WithDeactivated { get; private set; }


        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return new QueryTranslator<TElement>(GetSubProvider<TElement>(), expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            Type elementType = expression.Type.FindElementTypes().Single(t => t != typeof(object));
            MethodInfo getSubProvider = typeof(QueryTranslatorProvider<T>).GetMethod("GetSubProvider", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(elementType);
            // new' up a generic class with the result of a generic method call, yay!
            IQueryable result = (IQueryable)Activator.CreateInstance(typeof(QueryTranslator<>).MakeGenericType(elementType),
                new object[] { getSubProvider.Invoke(this, new object[] { }) });
            return result;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            object result = (this as IQueryProvider).Execute(expression);
            return (TResult)result;
        }

        public object Execute(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            var ifType = IftFactory(typeof(T));
            int objectCount = 0;
            var ticks = perfCounter.IncrementQuery(ifType);
            try
            {
                ResetProvider();

                using (Logging.Linq.DebugTraceMethodCall("Execute"))
                {
                    if (Logging.Linq.IsInfoEnabled)
                    {
                        Logging.Linq.Info(expression.ToString());
                    }

                    Expression translated = this.Visit(expression);

                    if (Logging.LinqQuery.IsDebugEnabled)
                    {
                        Logging.LinqQuery.Debug(translated.Trace());
                    }

                    object result = Source.Provider.Execute(translated);

                    if (result != null)
                    {
                        result = WrapResult(result);
                        objectCount = 1;
                    }

                    return result;
                }
            }
            finally
            {
                perfCounter.DecrementQuery(ifType, objectCount, ticks);
            }
        }

        internal IEnumerable ExecuteEnumerable(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            int objectCount = 0;
            var ifType = IftFactory(typeof(T));
            var ticks = perfCounter.IncrementQuery(ifType);
            try
            {
                ResetProvider();

                // detect projections to (anonymous) generic types referencing business objects
                if (typeof(T).IsGenericType
                    && typeof(T).GetGenericArguments()
                        .SelectMany(argument => argument.AndChildren(t => t.IsGenericType ? t.GetGenericArguments() : new Type[] { }))
                        .Any(t => typeof(IPersistenceObject).IsAssignableFrom(t)))
                {
                    throw new NotImplementedException("Projecting to anonymous type with IPersistenceObject members not yet implemented.");
                }

                using (Logging.Linq.DebugTraceMethodCall("ExecuteEnumerable"))
                {
                    if (Logging.Linq.IsInfoEnabled)
                    {
                        Logging.Linq.Info(expression.ToString());
                    }

                    Expression translated = this.Visit(expression);

                    if (Logging.LinqQuery.IsDebugEnabled)
                    {
                        Logging.LinqQuery.Debug(translated.Trace());
                    }

                    IQueryable newQuery = Source.Provider.CreateQuery(translated);
                    List<T> result = new List<T>();
                    foreach (object item in newQuery)
                    {
                        result.Add((T)WrapResult(item));
                    }
                    objectCount = result.Count;
                    return result;
                }
            }
            finally
            {
                perfCounter.DecrementQuery(ifType, objectCount, ticks);
            }
        }

        protected virtual object WrapResult(object item)
        {
            return item;
        }

        private void ResetProvider()
        {
            WithDeactivated = false;
            _Parameter = new Dictionary<ParameterExpression, ParameterExpression>();
        }

        #endregion

        #region Visits

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.IsMethodCallExpression("Cast"))
            {
                // Throw away casts, either it is redundant, then we don't need it, or it is wrong, then we'll fail in translation or in the database anyways.
                return Visit(m.Arguments.Single());
            }
            else if (m.IsMethodCallExpression("WithEagerLoading", typeof(ZetboxContextQueryableExtensions)))
            {
                // Eager Loading is done automatically on the server - ignore and continue
                return Visit(m.Arguments.Single());
            }
            else if (m.IsMethodCallExpression("WithDeactivated", typeof(ZetboxContextQueryableExtensions)))
            {
                // save for future use
                WithDeactivated = true;
                return Visit(m.Arguments.Single());
            }
            else if (m.IsMethodCallExpression("OfType"))
            {
                var source = Visit(m.Arguments.Single());
                source = Expression.Call(null, GetMethodInfo(m.Method), source);

                var type = source.Type.FindElementTypes().Single(t => t != typeof(object));
                return AddFilter(source, Ctx.GetImplementationType(type).ToInterfaceType());
            }
            // Methods requiring special translation
            else if (m.Method.DeclaringType == typeof(Queryable) && m.Method.GetParameters().Length > 1)
            {
                var source = Visit(m.Arguments[0]);
                // unpack IQueryable<T>
                var sourceType = source.Type.GetGenericArguments().Single();

                switch (m.Method.Name)
                {
                    case "All": // bool All<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
                    case "Any": // bool Any<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "Count": // Count<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "First": // First<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "FirstOrDefault": // FirstOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "Last": // Last<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "LastOrDefault": // LastOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "LongCount": // LongCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "Single": // Single<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    case "SingleOrDefault": // SingleOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);

                    case "SkipWhile": // SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    // case "SkipWhile": // SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate);
                    case "TakeWhile": // TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    // case "TakeWhile": // TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate);
                    case "Where": // Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
                    // case "Where": // Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate);

                    case "Average": // Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, (decimal?|decimal|double?|double|float?|float|int?|int|long?|long)>> selector); 
                    case "Sum": // Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, (decimal?|decimal|double?|double|float?|float|int?|int|long?|long)>> selector);
                        if (m.Arguments.Count > 2)
                        {
                            throw new InvalidOperationException(string.Format("Cannot translate Queryable.{0} call with custom comparer", m.Method.Name));
                        }
                        else
                        {
                            var newPredicate = VisitQueryArgument(m.Arguments[1], sourceType);

                            // the number of generic arguments to the predicate
                            var predicateArgCount = ExtractArgCount(newPredicate.Type);

                            MethodInfo newMethod = typeof(Queryable).GetMethods()
                                   .Single(mi => mi.Name == m.Method.Name
                                       && mi.GetParameters().Length == 2
                                       && ExtractArgCount(mi.GetParameters()[1].ParameterType.GetGenericArguments().Single()) == predicateArgCount)
                                   .MakeGenericMethod(sourceType);

                            return Expression.Call(null, newMethod, new[] { source, newPredicate });
                        }

                    case "Skip": // Skip<TSource>(this IQueryable<TSource> source, int count);
                    case "Take": // Take<TSource>(this IQueryable<TSource> source, int count);
                        {
                            var newCount = Visit(m.Arguments[1]);

                            MethodInfo newMethod = typeof(Queryable).GetMethods()
                                   .Single(mi => mi.Name == m.Method.Name
                                       && mi.GetParameters().Length == 2)
                                   .MakeGenericMethod(sourceType);

                            return Expression.Call(null, newMethod, new[] { source, newCount });
                        }

                    case "Max": // Max<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector);
                    case "Min": // Min<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector);

                    case "Select": // Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector);
                    // case "Select": // Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, TResult>> selector);

                    case "OrderBy": // OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector);
                    case "OrderByDescending": // OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector);
                    case "ThenBy": // ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector);
                    case "ThenByDescending": // ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector);
                        if (m.Arguments.Count > 2)
                        {
                            throw new InvalidOperationException(string.Format("Cannot translate {0} call with custom comparer", m.Method.Name));
                        }
                        else
                        {
                            var newKeySelector = VisitQueryArgument(m.Arguments[1], sourceType);

                            if (newKeySelector.Body.Type.IsICompoundObject() && new[] { "OrderBy", "OrderByDescending", "ThenBy", "ThenByDescending" }.Contains(m.Method.Name))
                            {
                                return CreateCompoundOrderByExpression(m, source, sourceType, newKeySelector);
                            }
                            else
                            {
                                // the number of generic arguments to the predicate
                                var predicateArgCount = ExtractArgCount(newKeySelector.Type);

                                MethodInfo newMethod = typeof(Queryable).GetMethods()
                                    .Single(mi => mi.Name == m.Method.Name
                                        && mi.GetParameters().Length == 2
                                       && ExtractArgCount(mi.GetParameters()[1].ParameterType.GetGenericArguments().Single()) == predicateArgCount)
                                    .MakeGenericMethod(sourceType, newKeySelector.Body.Type);

                                return Expression.Call(null, newMethod, new[] { source, newKeySelector });
                            }
                        }

                    case "GroupBy": // GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector);
                        // + Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector);
                        // + Expression<Func<TSource, TElement>> elementSelector);
                        // + Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector);
                        if (m.Arguments.Count != m.Method.GetGenericArguments().Length)
                        {
                            // all feasible forms of GroupBy have exactly one parameter for each generic parameter:
                            // source => TSource
                            // keySelector => TKey
                            // elementSelector => TElement
                            // resultSelector => TResult
                            throw new InvalidOperationException(string.Format("Cannot translate {0} call with custom comparer", m.Method.Name));
                        }
                        else
                        {
                            var selectors = m.Arguments.Skip(1).Select(a => VisitQueryArgument(a, sourceType)).ToList();

                            if (selectors[0].Body.Type.IsICompoundObject())
                            {
                                throw new NotImplementedException("grouping by compound objects not yet implemented. See CreateCompoundOrderByExpression for a template");
                            }
                            else
                            {
                                var parameterTypes = new[] { sourceType }.Concat(selectors.Select(s => s.Body.Type)).ToArray();

                                MethodInfo newMethod = typeof(Queryable).GetMethods()
                                    .Single(mi => mi.Name == m.Method.Name && mi.GetParameters().Length == parameterTypes.Length)
                                    .MakeGenericMethod(parameterTypes);

                                var arguments = new[] { source }.Concat(selectors).ToArray();
                                return Expression.Call(null, newMethod, arguments);
                            }
                        }

                    default:
                        throw new InvalidOperationException(string.Format("Cannot translate Queryable.{0} call", m.Method.Name));
                }
            }
            else
            {
                Expression objExp = Visit(m.Object);
                MethodInfo newMethod = GetMethodInfo(m.Method);

                return Expression.Call(objExp, newMethod, VisitExpressionList(m.Arguments));
            }
        }

        private static int ExtractArgCount(Type t)
        {
            return t.GetGenericArguments().Length;
        }

        /// <summary>
        /// All Queryable functions take Lamdas whose first argument is of type TSource. This method translates this to use the real underlying source type.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        private LambdaExpression VisitQueryArgument(Expression argument, Type sourceType)
        {
            var lambda = (LambdaExpression)argument.StripQuotes();
            // force the first parameter to underlying source type
            var newParams = new[] { VisitParameter(lambda.Parameters.First(), sourceType) }.Concat(VisitParameterList(lambda.Parameters.Skip(1).ToList().AsReadOnly()));
            var newBody = Visit(lambda.Body);

            return Expression.Lambda(newBody, newParams);
        }

        private Expression CreateCompoundOrderByExpression(MethodCallExpression m, Expression source, Type sourceType, LambdaExpression lambda)
        {
            var cpDef = MetaDataResolver.GetCompoundObject(Ctx.GetImplementationType(lambda.Body.Type).ToInterfaceType());
            if (cpDef == null) throw new InvalidOperationException("cannot resolve CompoundObject type: " + lambda.Body.Type.AssemblyQualifiedName);

            var param = lambda.Parameters.Single();
            bool isOrderBy = m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("OrderByDescending");
            bool isAsc = m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("ThenBy");
            var result = source;
            foreach (var prop in cpDef.Properties)
            {
                var propType = prop.IsNullable() ? typeof(Nullable<>).MakeGenericType(prop.GetPropertyType()) : prop.GetPropertyType();
                var newOrderByLambda = Expression.Quote(
                    Expression.Lambda(
                        Expression.MakeMemberAccess(lambda.Body, lambda.Body.Type.GetProperty(prop.Name)),
                        param)
                );

                var sortExpression = Expression.Call(
                    typeof(Queryable),
                    isOrderBy
                        ? (isAsc ? "OrderBy" : "OrderByDescending")
                        : (isAsc ? "ThenBy" : "ThenByDescending"),
                    new Type[] { sourceType, propType }, // CP-Objects can only contain value properties, until nested CP-Objects are implemented correctly
                    result,
                    newOrderByLambda);

                result = sortExpression;
                isOrderBy = false;
            }
            return result;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            var targetType = u.Type;

            // Ignore Converts for all IExportable and IPersistenceObject casts. This may happen even if we don't yet
            // know what the underlying type is. The database will catch that.
            // Also remove casts to assignment compatible types, except when casting to Nullable<T>. The latter casts
            // are necessary to keep operator methods happy, since they do not accept mixed nullability arguments.
            if (u.NodeType == ExpressionType.Convert)
            {
                var operandType = u.Operand.Type;

                var castToIExportable = typeof(Zetbox.App.Base.IExportable).IsAssignableFrom(targetType) || targetType.IsIExportableInternal();
                var castToIPersistenceObject = targetType.IsIPersistenceObject();
                var upCast = targetType.IsAssignableFrom(operandType);
                var nullableCast = upCast && !operandType.IsGenericType && operandType.IsValueType && targetType == typeof(Nullable<>).MakeGenericType(operandType);
                if (castToIExportable || castToIPersistenceObject || (upCast && !nullableCast))
                {
                    return Visit(u.Operand);
                }
            }

            return Expression.MakeUnary(u.NodeType, Visit(u.Operand), TranslateType(targetType), u.Method);
        }

        protected override Expression VisitLambda(LambdaExpression lambda)
        {
            var body = Visit(lambda.Body);
            var parameters = VisitParameterList(lambda.Parameters);
            return Expression.Lambda(TranslateType(lambda.Type), body, parameters);
        }

        protected override Expression VisitTypeIs(TypeBinaryExpression b)
        {
            var type = TranslateType(b.TypeOperand);
            return Expression.TypeIs(Visit(b.Expression), type);
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Type.IsGenericType && c.Type.GetGenericTypeDefinition() == typeof(QueryTranslator<>))
            {
                var result = Source.Expression;
                var type = c.Type.GetGenericArguments().First();
                return AddFilter(result, IftFactory(type));
            }
            else
            {
                return c;
            }
        }

        private Dictionary<ParameterExpression, ParameterExpression> _Parameter = new Dictionary<ParameterExpression, ParameterExpression>();
        protected override ParameterExpression VisitParameter(ParameterExpression p)
        {
            return VisitParameter(p, null);
        }

        private ParameterExpression VisitParameter(ParameterExpression p, Type targetType)
        {
            if (!_Parameter.ContainsKey(p))
            {
                _Parameter[p] = Expression.Parameter(targetType ?? TranslateType(p.Type), p.Name);
            }
            return _Parameter[p];
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            Expression e = base.Visit(m.Expression);

            // e might be null if m.Member is a static reference
            var type = e == null
                ? TranslateType(m.Type)
                : e.Type;

            string memberName = m.Member.Name;
            MemberExpression result;
            if (type.GetMember(memberName).Length > 0 && type.GetMember(memberName + Zetbox.API.Helper.ImplementationSuffix).Length > 0)
            {
                result = Expression.PropertyOrField(e, memberName + Zetbox.API.Helper.ImplementationSuffix);
            }
            else
            {
                MemberInfo member = m.Member;
                // If this is not a static access AND the member type(!) and expression type do not match, fixup MemberInfo
                if (e != null && !member.DeclaringType.IsAssignableFrom(e.Type))
                {
                    member = e.Type.FindProperty(m.Member.Name).Single();
                }
                result = Expression.MakeMemberAccess(e, member);
            }
            return result;
        }

        protected override NewExpression VisitNew(NewExpression newExpression)
        {
            var args = VisitExpressionList(newExpression.Arguments);
            Type declaringType = TranslateType(newExpression.Constructor.DeclaringType);
            ConstructorInfo c = declaringType.GetConstructor(
                newExpression.Constructor.GetParameters().Select(p => TranslateType(p.ParameterType)).ToArray());

            if (newExpression.Members != null)
            {
                List<MemberInfo> members = new List<MemberInfo>();
                foreach (MemberInfo mi in newExpression.Members)
                {
                    declaringType = TranslateType(mi.DeclaringType);
                    if (declaringType.GetMember(mi.Name).Length > 0 && declaringType.GetMember(mi.Name + Zetbox.API.Helper.ImplementationSuffix).Length > 0)
                    {
                        members.Add(declaringType.GetMember(mi.Name + Zetbox.API.Helper.ImplementationSuffix)[0]);
                    }
                    else
                    {
                        members.Add(declaringType.GetMember(mi.Name)[0]);
                    }
                }
                return Expression.New(c, args, members);
            }
            else
            {
                return Expression.New(c, args);
            }
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Equal && b.Left.Type.IsIDataObject() && b.Right.Type.IsIDataObject())
            {
                var newLeft = Visit(b.Left);
                var newRight = Visit(b.Right);
                return Expression.MakeBinary(b.NodeType,
                    Expression.MakeMemberAccess(newLeft, newLeft.Type.FindFirstOrDefaultMember("ID")),
                    Expression.MakeMemberAccess(newRight, newRight.Type.FindFirstOrDefaultMember("ID")));
            }
            return base.VisitBinary(b);
        }
        #endregion

        #region Filter
        private Expression AddFilter(Expression e, InterfaceType ifType)
        {
            e = AddSecurityFilter(e, ifType);
            e = AddDeactivatableFilter(e, ifType);
            return e;
        }

        private Expression AddDeactivatableFilter(Expression e, InterfaceType ifType)
        {
            if (WithDeactivated) return e;
            if (!ifType.Type.IsIDataObject()) return e;
            if (!typeof(Zetbox.App.Base.IDeactivatable).IsAssignableFrom(ifType.Type)) return e;

            // original expression type
            var type = TranslateType(ifType.Type);

            // .Where(o => o.IsDeactivated == false)
            ParameterExpression pe_o = Expression.Parameter(type, "o");

            // o.IsDeactivated == false
            var eq_isdeactivated = Expression.Equal(
                            Expression.PropertyOrField(pe_o, "IsDeactivated"),
                            Expression.Constant(false),
                            false,
                            typeof(int).GetMethod("op_Equality"));

            // o => o.IsDeactivated == false
            var filter = Expression.Lambda(eq_isdeactivated, pe_o);

            // e.Where(o => o.IsDeactivated == false)
            var result = Expression.Call(typeof(Queryable), "Where", new Type[] { type }, e, filter);
            return result;
        }

        private Expression AddSecurityFilter(Expression e, InterfaceType ifType)
        {
            if (Identity == null || !ifType.Type.IsIDataObject()) return e;

            // Case #1363: May return NULL during initialization
            var objClass = MetaDataResolver.GetObjectClass(ifType);
            if (objClass == null) return e;

            // Only ACL's on Root classes are allowed
            var rootClass = objClass.GetRootClass();

            if (Ctx.GetGroupAccessRights(ifType).HasReadRights())
            {
                return e;
            }
            else if (rootClass.NeedsRightsTable())
            {
                // original expression type
                var type = TranslateType(ifType.Type);

                var baseIfType = rootClass.GetDescribedInterfaceType();
                var rights_type = Type.GetType(baseIfType.Type.FullName + "_Rights" + ImplementationSuffix + ", " + type.Assembly.FullName, true);

                // .Where(o => o.Projekte_Rights.Any(r => r.Identity == 12))
                ParameterExpression pe_o = Expression.Parameter(type, "o");
                ParameterExpression pe_r = Expression.Parameter(rights_type, "r");

                // r.Identity == 12
                var eq_identity = Expression.Equal(
                                Expression.PropertyOrField(pe_r, "Identity"),
                                Expression.Constant(Identity.ID),
                                false,
                                typeof(int).GetMethod("op_Equality"));

                // r => r.Identity == 12
                var eq_identity_lambda = Expression.Lambda(eq_identity, pe_r);

                // o.Projekte_Rights
                var any_src = Expression.PropertyOrField(pe_o, "SecurityRightsCollection" + Zetbox.API.Helper.ImplementationSuffix);

                // o.Projekte_Rights.Any(r => r.Identity == 12)
                var any = Expression.Call(typeof(System.Linq.Enumerable), "Any", new Type[] { rights_type },
                    any_src,
                    eq_identity_lambda);

                // o.Projekte_Rights.Any(r => r.Identity == 12)
                //var eq_count = Expression.Equal(
                //                count,
                //                Expression.Constant(1),
                //                false,
                //                typeof(int).GetMethod("op_Equality"));

                // (o => o.Projekte_Rights.Any(r => r.Identity == 12))
                var filter = Expression.Lambda(any, pe_o);

                // e.Where(o => o.Projekte_Rights.Any(r => r.Identity == 12))
                var result = Expression.Call(typeof(Queryable), "Where", new Type[] { type }, e, filter);
                return result;
            }
            else
            {
                // No Group Membership, no rights table - no rights
                throw new System.Security.SecurityException(string.Format("Identity has no rights to query '{0}'", ifType.Type.FullName));
            }
        }
        #endregion

        #region Utilities

        private MethodInfo GetMethodInfo(MethodInfo input)
        {
            if (input == null) { throw new ArgumentNullException("input"); }

            Type MethodType = TranslateType(input.DeclaringType);
            string MethodName = input.Name;

            List<Type> ParameterTypes = new List<Type>();
            ParameterTypes.AddRange(input.GetParameters().Select(p => TranslateType(p.ParameterType)));

            List<Type> GenericArguments = new List<Type>();
            GenericArguments.AddRange(input.GetGenericArguments().Select(p => TranslateType(p)));

            MethodInfo mi;
            if (GenericArguments != null && GenericArguments.Count > 0)
            {
                mi = MethodType.FindGenericMethod(MethodName,
                        GenericArguments.ToArray(),
                        ParameterTypes.ToArray());
            }
            else
            {
                mi = MethodType.GetMethod(MethodName, ParameterTypes.ToArray());
            }

            if (mi == null)
            {
                throw new MissingMethodException(MethodType.FullName, MethodName);
            }
            return mi;
        }

        /// <summary>
        /// Translates the specified Type to a provider type, if it is a IPersistenceObject; else it is passed through unmodified.
        /// </summary>
        protected virtual Type TranslateType(Type type)
        {
            return ((type.IsIPersistenceObject() && type != typeof(IPersistenceObject) && type != typeof(IDataObject)) || type.IsICompoundObject())
                ? Ctx.ToImplementationType(IftFactory(type)).Type
                : type.IsGenericType
                    ? type.GetGenericTypeDefinition().MakeGenericType(type.GetGenericArguments().Select(arg => TranslateType(arg)).ToArray())
                    : type;
        }

        #endregion
    }
}
