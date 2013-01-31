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
            _Parameter = new Dictionary<ParameterExpression, ParameterExpression>();
        }
        #endregion

        #region Visits
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            Expression objExp = base.Visit(m.Object);
            MethodInfo newMethod = GetMethodInfo(m.Method);
            ReadOnlyCollection<Expression> args = base.VisitExpressionList(m.Arguments);
            if (m.IsMethodCallExpression("Cast"))
            {
                // Throw away unneeded casts - no database will handle that
                // Note: Cast != OfType
                return args.Single();
            }
            else if (m.IsMethodCallExpression("WithEagerLoading", typeof(ZetboxContextQueryableExtensions)))
            {
                // Eager Loading is done automatically on the server - ignore and continue
                return args.Single();
            }
            else if (m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("OrderByDescending") || m.IsMethodCallExpression("ThenBy") || m.IsMethodCallExpression("ThenByDescending"))
            {
                var lambda = (LambdaExpression)args.Skip(1).Single().StripQuotes();
                if (lambda.Body.Type.IsICompoundObject())
                {
                    var cpDef = MetaDataResolver.GetCompoundObject(Ctx.GetImplementationType(lambda.Body.Type).ToInterfaceType());
                    if (cpDef == null) throw new InvalidOperationException("cannot resolve CompoundObject type: " + lambda.Body.Type.AssemblyQualifiedName);

                    var sourceType = args.First().Type.GetGenericArguments()[0];
                    var param = lambda.Parameters.Single();
                    bool isOrderBy = m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("OrderByDescending");
                    bool isAsc = m.IsMethodCallExpression("OrderBy") || m.IsMethodCallExpression("ThenBy");
                    var result = args.First();
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
                else
                {
                    return Expression.Call(objExp, newMethod, args);
                }
            }
            else
            {
                var result = Expression.Call(objExp, newMethod, args);

                if (result.IsMethodCallExpression("OfType"))
                {
                    var type = result.Type.FindElementTypes().Single(t => t != typeof(object));
                    return AddSecurityFilter(result, Ctx.GetImplementationType(type).ToInterfaceType());
                }
                return result;
            }
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            // ignore Converts for IExportable objects
            if (u.NodeType == ExpressionType.Convert && (typeof(Zetbox.App.Base.IExportable).IsAssignableFrom(u.Type) || u.Type.IsIExportableInternal()))
            {
                return base.Visit(u.Operand);
            }
            // ignore Converts for persistence objects
            else if (u.NodeType == ExpressionType.Convert && u.Type.IsIPersistenceObject())
            {
                return base.Visit(u.Operand);
            }
            else
            {
                return Expression.MakeUnary(u.NodeType, base.Visit(u.Operand), TranslateType(u.Type), u.Method);
            }
        }

        protected override Expression VisitLambda(LambdaExpression lambda)
        {
            Type t = TranslateType(lambda.Type);
            Expression body = base.Visit(lambda.Body);
            var parameters = base.VisitParameterList(lambda.Parameters);
            return Expression.Lambda(t, body, parameters);
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
                return AddSecurityFilter(result, IftFactory(type));
            }
            else
            {
                return c;
            }
        }

        private Dictionary<ParameterExpression, ParameterExpression> _Parameter = new Dictionary<ParameterExpression, ParameterExpression>();
        protected override ParameterExpression VisitParameter(ParameterExpression p)
        {
            if (!_Parameter.ContainsKey(p))
            {
                _Parameter[p] = Expression.Parameter(TranslateType(p.Type), p.Name);
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

        #region SecurityFilter
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
                var filter = Expression.Lambda(any, new ParameterExpression[] { pe_o });

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
