using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Kistl.API
{
    #region Expression
    [Serializable]
    public abstract class SerializableExpression
    {
        internal class SerializationContext
        {
            private Dictionary<string, Expression> _Parameter = new Dictionary<string, Expression>();
            public Dictionary<string, Expression> Parameter
            {
                get
                {
                    return _Parameter;
                }
            }
        }

        internal SerializableExpression(Expression e, SerializationContext ctx)
        {
            _Type = new SerializableType(e.Type);
            _NodeType = e.NodeType;
        }

        private ExpressionType _NodeType;
        public ExpressionType NodeType
        {
            get
            {
                return _NodeType;
            }
        }

        private SerializableType _Type;
        public Type Type
        {
            get
            {
                return _Type.GetSerializedType();
            }
        }

        public static SerializableExpression FromExpression(Expression e)
        {
            SerializationContext ctx = new SerializationContext();
            return FromExpression(e, ctx);
        }

        internal static SerializableExpression FromExpression(Expression e, SerializationContext ctx)
        {
            if (e == null) throw new ArgumentNullException("e");

            if (e is BinaryExpression)
                return new SerializableBinaryExpression((BinaryExpression)e, ctx);

            if (e is UnaryExpression)
                return new SerializableUnaryExpression((UnaryExpression)e, ctx);

            if (e is ConstantExpression)
                return new SerializableConstantExpression((ConstantExpression)e, ctx);

            if (e is MemberExpression)
            {
                MemberExpression exp = (MemberExpression)e;
                if (exp.Expression is ConstantExpression)
                {
                    ConstantExpression left = (ConstantExpression)exp.Expression;
                    if (exp.Member is PropertyInfo)
                    {
                        return new SerializableConstantExpression(Expression.Constant(((PropertyInfo)exp.Member).GetValue(left.Value, null)), ctx);
                    }
                    else if (exp.Member is FieldInfo)
                    {
                        return new SerializableConstantExpression(Expression.Constant(((FieldInfo)exp.Member).GetValue(left.Value)), ctx);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                return new SerializableMemberExpression((MemberExpression)e, ctx);
            }

            if (e is LambdaExpression)
                return new SerializableLambdaExpression((LambdaExpression)e, ctx);

            if (e is MethodCallExpression)
                return new SerializableMethodCallExpression((MethodCallExpression)e, ctx);

            if (e is ParameterExpression)
                return new SerializableParameterExpression((ParameterExpression)e, ctx);

            if (e is NewExpression)
                return new SerializableNewExpression((NewExpression)e, ctx);

            throw new NotSupportedException(string.Format("Nodetype {0} is not supported: {1}", e.NodeType, e.ToString()));
        }

        public virtual Expression ToExpression()
        {
            SerializationContext ctx = new SerializationContext();
            return ToExpressionInternal(ctx);
        }

        internal abstract Expression ToExpressionInternal(SerializationContext ctx);
    }
    #endregion

    #region CompoundExpression
    [Serializable]
    public abstract class SerializableCompoundExpression : SerializableExpression
    {
        internal SerializableCompoundExpression(Expression e, SerializableExpression.SerializationContext ctx)
            : base(e, ctx)
        {
            _children = new List<SerializableExpression>();
        }


        private List<SerializableExpression> _children;
        public List<SerializableExpression> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }
    }
    #endregion

    #region BinaryExpression
    [Serializable]
    public class SerializableBinaryExpression : SerializableCompoundExpression
    {
        internal SerializableBinaryExpression(BinaryExpression e, SerializableExpression.SerializationContext ctx)
            : base(e, ctx)
        {
            Children.Add(SerializableExpression.FromExpression(e.Left, ctx));
            Children.Add(SerializableExpression.FromExpression(e.Right, ctx));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeBinary(NodeType, Children[0].ToExpressionInternal(ctx), Children[1].ToExpressionInternal(ctx));
        }
    }
    #endregion

    #region UnaryExpression
    [Serializable]
    public class SerializableUnaryExpression : SerializableCompoundExpression
    {
        internal SerializableUnaryExpression(UnaryExpression e, SerializableExpression.SerializationContext ctx)
            : base(e, ctx)
        {
            Children.Add(SerializableExpression.FromExpression(e.Operand, ctx));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeUnary(NodeType, Children[0].ToExpressionInternal(ctx), Type);
        }
    }
    #endregion

    #region ConstantExpression
    [Serializable]
    public class SerializableConstantExpression : SerializableExpression
    {
        internal SerializableConstantExpression(ConstantExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            Value = e.Value;
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.Constant(Value, Type);
        }

        public object Value { get; set; }
    }
    #endregion

    #region MemberExpression
    [Serializable]
    public class SerializableMemberExpression : SerializableCompoundExpression
    {
        internal SerializableMemberExpression(MemberExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            MemberName = e.Member.Name;
            Children.Add(SerializableExpression.FromExpression(e.Expression, ctx));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return MemberExpression.PropertyOrField(Children[0].ToExpressionInternal(ctx), MemberName);
        }

        public string MemberName { get; private set; }
    }
    #endregion

    #region MethodCallExpression
    [Serializable]
    public class SerializableMethodCallExpression : SerializableCompoundExpression
    {
        internal SerializableMethodCallExpression(MethodCallExpression e, SerializationContext ctx)
            : base(e, ctx)
        {            
            if(e.Object != null) ObjectExpression = SerializableExpression.FromExpression(e.Object, ctx);

            MethodName = e.Method.Name;
            _Type = new SerializableType(e.Method.DeclaringType);
            ParameterTypes = e.Method.GetParameters().Select(p => new SerializableType(p.ParameterType)).ToList();
            GenericArguments = e.Method.GetGenericArguments().Select(p => new SerializableType(p)).ToList();

            if (e.Arguments != null)
            {
                Children = e.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx)).ToList();
            }
        }

        public string MethodName {get; private set; }

        public List<SerializableType> ParameterTypes { get; private set; }

        public List<SerializableType> GenericArguments {get; private set; }

        private SerializableType _Type;
        public Type MethodType
        {
            get
            {
                return _Type.GetSerializedType();
            }
        }

        #region FindGenericMethod
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
                    if (method.Name == methodName)
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

                            if(paramSame) return mi;
                        }
                    }
                }
            }

            return null;
        }
        #endregion

        public MethodInfo GetMethodInfo()
        {
            MethodInfo mi;
            if (GenericArguments != null && GenericArguments.Count > 0)
            {
                mi = FindGenericMethod(MethodType, MethodName,
                        GenericArguments.Select(p => p.GetSerializedType()).ToArray(),
                        ParameterTypes.Select(p => p.GetSerializedType()).ToArray());
            }
            else
            {
                mi = MethodType.GetMethod(MethodName, ParameterTypes.Select(p => p.GetSerializedType()).ToArray());
            }

            if (mi == null)
            {
                throw new MissingMethodException(MethodType.FullName, MethodName);
            }
            return mi;
        }

        public SerializableExpression ObjectExpression { get; private set; }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.Call(
                ObjectExpression == null ? null : ObjectExpression.ToExpressionInternal(ctx), 
                GetMethodInfo(),
                Children.Select(e => e.ToExpressionInternal(ctx)));
        }
    }
    #endregion 

    #region LambdaExpression
    [Serializable]
    public class SerializableLambdaExpression : SerializableCompoundExpression
    {
        internal SerializableLambdaExpression(LambdaExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            Children.Add(SerializableExpression.FromExpression(e.Body, ctx));
            Children.AddRange(e.Parameters.Select(p => SerializableExpression.FromExpression(p, ctx)));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            List<ParameterExpression> parameters = new List<ParameterExpression>();
            foreach (SerializableExpression p in Children.Skip(1))
                parameters.Add((ParameterExpression)p.ToExpressionInternal(ctx));

            return Expression.Lambda(Children[0].ToExpressionInternal(ctx), parameters.ToArray());
        }
    }
    #endregion

    #region ParameterExpression
    [Serializable]
    public class SerializableParameterExpression : SerializableExpression
    {
        internal SerializableParameterExpression(ParameterExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            name = e.Name;
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            lock (ctx)
            {
                if (!ctx.Parameter.ContainsKey(Name))
                {
                    ctx.Parameter[Name] = Expression.Parameter(Type, name);
                }
                return ctx.Parameter[Name];
            }
        }
    }
    #endregion

    #region NewExpression
    [Serializable]
    public class SerializableNewExpression : SerializableCompoundExpression
    {
        private ConstructorInfo constructor;
        private List<MemberInfo> members;

        internal SerializableNewExpression(NewExpression source, SerializationContext ctx)
            : base(source, ctx)
        {
            constructor = source.Constructor;
            if(source.Members != null)
            {
                members = source.Members.ToList();
            }

            Children = source.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx)).ToList();
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            List<Expression> arguments = Children.Select(a => a.ToExpressionInternal(ctx)).ToList();

            if (members != null)
            {
                return Expression.New(constructor, arguments, members.ToArray());
            }
            else
            {
                return Expression.New(constructor, arguments);
            }
        }
    }
    #endregion

}
