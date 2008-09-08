using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using System.Reflection;
using System.Collections.ObjectModel;
using Kistl.API;

namespace Kistl.API.Server
{
    #region QueryTranslator
    public class QueryTranslator<T> : IQueryable<T>
    {
        IQueryable _source;
        private Expression _expression = null;
        private QueryTranslatorProvider<T> _provider = null;

        public QueryTranslator(IQueryable source)
        {
            _source = source;
            _expression = Expression.Constant(this);
            _provider = new QueryTranslatorProvider<T>(_source);
        }

        public QueryTranslator(IQueryable source, Expression e)
        {
            if (e == null) throw new ArgumentNullException("e");
            _expression = e;
            _source = source;
            _provider = new QueryTranslatorProvider<T>(_source);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_provider.Execute<IEnumerable<T>>(this._expression)).GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_provider.Execute(this._expression)).GetEnumerator();
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
    #endregion

    #region QueryTranslatorProvider
    public class QueryTranslatorProvider<T> : ExpressionTreeTranslator, IQueryProvider
    {
        #region Configuration
        private const string ImplementationSuffix = "Impl";
        private const string DestinationAssembly = "Kistl.Objects.Server";
        #endregion

        IQueryable _source;
        Expression _filter = null;

        public QueryTranslatorProvider(IQueryable source)
        {
            if (source == null) throw new ArgumentNullException("source");
            _source = source;
        }

        #region TypeSystem
        private static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;

            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }

            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }

            return null;
        }
        #endregion

        #region TranslateType
        public static Type TranslateType(Type input)
        {
            if (input.IsGenericType)
            {
                Type genericType = input.GetGenericTypeDefinition();
                List<Type> genericArguments = new List<Type>();
                genericArguments.AddRange(input.GetGenericArguments().Select(t => TranslateType(t)));

                return genericType.MakeGenericType(genericArguments.ToArray());
            }
            else
            {
                if (input == typeof(IDataObject))
                {
                    return typeof(BaseServerDataObject);
                }
                else if (input == typeof(IPersistenceObject))
                {
                    return typeof(BaseServerPersistenceObject);
                }
                else if (input == typeof(IStruct))
                {
                    return typeof(BaseServerStructObject);
                }
                else if (input == typeof(ICollectionEntry))
                {
                    return typeof(BaseServerCollectionEntry);
                }
                else if (typeof(IDataObject).IsAssignableFrom(input) && !typeof(BaseServerDataObject).IsAssignableFrom(input))
                {
                    // add "Impl"
                    string newType = input.Namespace + "."
                        + input.Name
                        + ImplementationSuffix
                        + ", "
                        + DestinationAssembly;
                    return Type.GetType(newType, true);
                }
                else
                {
                    return input;
                }
            }
        }
        #endregion

        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return new QueryTranslator<TElement>(_source, expression) as IQueryable<TElement>;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            Type elementType = GetElementType(expression.Type);
            IQueryable result = (IQueryable)Activator.CreateInstance(typeof(QueryTranslator<>).MakeGenericType(elementType),
                new object[] { _source, expression });
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

            Expression translated = this.Visit(expression);

            if (expression.IsMethodCallExpression("First") || expression.IsMethodCallExpression("FirstOrDefault") ||
                expression.IsMethodCallExpression("Single") || expression.IsMethodCallExpression("SingleOrDefault"))
            {
                return _source.Provider.Execute(translated);
            }
            else
            {
                IQueryable newQuery = _source.Provider.CreateQuery(translated);
                List<T> result = new List<T>();
                foreach (T item in newQuery)
                {
                    result.Add(item);
                }
                return result;
            }
        }

        #endregion

        #region MethodHelper
        private static MethodInfo FindGenericMethod(Type type, string methodName, Type[] typeArguments, Type[] parameterTypes)
        {
            if (parameterTypes == null)
            {
                MethodInfo mi = type.GetMethod(methodName);
                if (mi == null) return null;
                return mi.MakeGenericMethod(typeArguments);
            }
            else
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == methodName && method.GetGenericArguments().Length == typeArguments.Length)
                    {
                        MethodInfo mi = method.MakeGenericMethod(typeArguments);
                        ParameterInfo[] parameters = mi.GetParameters();

                        if (parameters.Length == parameterTypes.Length)
                        {
                            bool paramSame = true;
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                if (parameters[i].ParameterType != parameterTypes[i])
                                {
                                    paramSame = false;
                                    break;
                                }
                            }

                            if (paramSame) return mi;
                        }
                    }
                }
            }

            return null;
        }

        public MethodInfo GetMethodInfo(MethodInfo input)
        {
            Type MethodType = TranslateType(input.DeclaringType);
            string MethodName = input.Name;

            List<Type> ParameterTypes = new List<Type>();
            ParameterTypes.AddRange(input.GetParameters().Select(p => TranslateType(p.ParameterType)));

            List<Type> GenericArguments = new List<Type>();
            GenericArguments.AddRange(input.GetGenericArguments().Select(p => TranslateType(p)));


            MethodInfo mi;
            if (GenericArguments != null && GenericArguments.Count > 0)
            {
                mi = FindGenericMethod(MethodType, MethodName,
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
        #endregion

        #region Visits
        protected override MethodCallExpression VisitMethodCall(MethodCallExpression m)
        {
            Expression objExp = base.Visit(m.Object);
            MethodInfo newMethod = GetMethodInfo(m.Method);
            ReadOnlyCollection<Expression> args = base.VisitExpressionList(m.Arguments);
            m = Expression.Call(
                objExp,
                newMethod,
                args);

            return m;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            if (u.NodeType == ExpressionType.Convert)
            {
                return base.Visit(u.Operand);
            }
            else
            {
                return Expression.MakeUnary(u.NodeType, base.Visit(u.Operand), TranslateType(u.Type), u.Method);
            }
        }

        protected override LambdaExpression VisitLambda(LambdaExpression lambda)
        {
            try
            {
                Type t = TranslateType(lambda.Type);
                Expression body = base.Visit(lambda.Body);
                var parameters = base.VisitParameterList(lambda.Parameters);
                return Expression.Lambda(t, body, parameters);
            }
            catch
            {
                throw;
            }
        }

        protected override TypeBinaryExpression VisitTypeIs(TypeBinaryExpression b)
        {
            b = base.VisitTypeIs(b);
            return Expression.TypeIs(b.Expression, TranslateType(b.TypeOperand));
        }

        protected override ConstantExpression VisitConstant(ConstantExpression c)
        {
            c = base.VisitConstant(c);
            if (c.Type.IsGenericType && c.Type.GetGenericTypeDefinition() == typeof(QueryTranslator<>))
            {
                ConstantExpression s = (ConstantExpression)_source.Expression;
                return Expression.Constant(s.Value, s.Type);
            }
            else
            {
                return Expression.Constant(c.Value);
            }
        }

        private Dictionary<string, ParameterExpression> _Parameter = new Dictionary<string, ParameterExpression>();
        protected override ParameterExpression VisitParameter(ParameterExpression p)
        {
            if (!_Parameter.ContainsKey(p.Name))
            {
                _Parameter[p.Name] = Expression.Parameter(TranslateType(p.Type), p.Name);
            }
            return _Parameter[p.Name];
        }

        protected override MemberExpression VisitMemberAccess(MemberExpression m)
        {
            Expression e = base.Visit(m.Expression);
            string memberName = m.Member.Name;
            Type declaringType = TranslateType(m.Member.DeclaringType);
            MemberExpression result;
            if (declaringType.GetMember(memberName).Length > 0 && declaringType.GetMember(memberName + ImplementationSuffix).Length > 0)
            {
                result = Expression.PropertyOrField(e, memberName + ImplementationSuffix);
            }
            else
            {
                result = Expression.PropertyOrField(e, memberName);
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
                foreach(MemberInfo mi in newExpression.Members)
                {
                    declaringType = TranslateType(mi.DeclaringType);
                    if (declaringType.GetMember(mi.Name).Length > 0 && declaringType.GetMember(mi.Name + ImplementationSuffix).Length > 0)
                    {
                        members.Add(declaringType.GetMember(mi.Name + ImplementationSuffix)[0]);
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
        #endregion
    }
    #endregion
}
