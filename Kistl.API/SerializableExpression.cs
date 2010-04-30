using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Kistl.API
{
    /// <summary>
    /// Abstract Base Class for a serializable Expression
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(SerializableBinaryExpression))]
    [KnownType(typeof(SerializableConditionalExpression))]
    [KnownType(typeof(SerializableConstantExpression))]
    [KnownType(typeof(SerializableLambdaExpression))]
    [KnownType(typeof(SerializableMemberExpression))]
    [KnownType(typeof(SerializableMethodCallExpression))]
    [KnownType(typeof(SerializableNewExpression))]
    [KnownType(typeof(SerializableParameterExpression))]
    [KnownType(typeof(SerializableUnaryExpression))]
    public abstract partial class SerializableExpression
    {

        /// <summary>
        /// Creates a serializable Expression from a Expression
        /// </summary>
        /// <param name="e">Linq Expression</param>
        /// <param name="typeTrans"></param>
        /// <returns>serializable Expression</returns>
        public static SerializableExpression FromExpression(Expression e, ITypeTransformations typeTrans)
        {
            SerializationContext ctx = new SerializationContext();
            return FromExpression(e, ctx, typeTrans);
        }

        /// <summary>
        /// Creates a SerializableExpression from an Expression
        /// </summary>
        /// <param name="e">Linq Expression</param>
        /// <param name="ctx">Serialization Context</param>
        /// <param name="typeTrans"></param>
        // TODO: use ExpressionTreeVisitor/Translator
        internal static SerializableExpression FromExpression(Expression e, SerializationContext ctx, ITypeTransformations typeTrans)
        {
            if (e == null) throw new ArgumentNullException("e");

            if (e is BinaryExpression)
                return new SerializableBinaryExpression((BinaryExpression)e, ctx, typeTrans);

            if (e is UnaryExpression)
                return new SerializableUnaryExpression((UnaryExpression)e, ctx, typeTrans);

            if (e is ConstantExpression)
                return new SerializableConstantExpression((ConstantExpression)e, ctx, typeTrans);

            if (e is MemberExpression)
            {
                MemberExpression exp = (MemberExpression)e;
                if (exp.Expression is ConstantExpression)
                {
                    ConstantExpression left = (ConstantExpression)exp.Expression;
                    if (exp.Member is PropertyInfo)
                    {
                        return new SerializableConstantExpression(Expression.Constant(((PropertyInfo)exp.Member).GetValue(left.Value, null)), ctx, typeTrans);
                    }
                    else if (exp.Member is FieldInfo)
                    {
                        return new SerializableConstantExpression(Expression.Constant(((FieldInfo)exp.Member).GetValue(left.Value)), ctx, typeTrans);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                return new SerializableMemberExpression((MemberExpression)e, ctx, typeTrans);
            }

            if (e is LambdaExpression)
                return new SerializableLambdaExpression((LambdaExpression)e, ctx, typeTrans);

            if (e is MethodCallExpression)
                return new SerializableMethodCallExpression((MethodCallExpression)e, ctx, typeTrans);

            if (e is ParameterExpression)
                return new SerializableParameterExpression((ParameterExpression)e, ctx, typeTrans);

            if (e is NewExpression)
                return new SerializableNewExpression((NewExpression)e, ctx, typeTrans);

            if (e is ConditionalExpression)
                return new SerializableConditionalExpression((ConditionalExpression)e, ctx, typeTrans);

            throw new NotSupportedException(string.Format("Nodetype {0} is not supported: {1}", e.NodeType, e.ToString()));
        }

        /// <summary>
        /// Converts a SerializableExpression to a Linq Expression
        /// </summary>
        /// <returns>Linq Expression</returns>
        public static Expression ToExpression(SerializableExpression e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            SerializationContext ctx = new SerializationContext();
            return e.ToExpressionInternal(ctx);
        }

        /// <summary>
        /// Serialization Context
        /// </summary>
        internal class SerializationContext
        {
            private Dictionary<string, Expression> _Parameter = new Dictionary<string, Expression>();
            /// <summary>
            /// Collection of LINQ Parameter
            /// </summary>
            public Dictionary<string, Expression> Parameter
            {
                get
                {
                    return _Parameter;
                }
            }
        }

        /// <summary>
        /// Creates a SerializableExpression from an Expression
        /// </summary>
        /// <param name="e">Linq Expression</param>
        /// <param name="ctx">Serialization Context</param>
        /// <param name="typeTrans"></param>
        internal SerializableExpression(Expression e, SerializationContext ctx, ITypeTransformations typeTrans)
        {
            this.typeTrans = typeTrans;
            SerializableType = typeTrans.AsInterfaceType(e.Type.ToInterfaceType(typeTrans.AssemblyConfiguration)).ToSerializableType();
            NodeType = e.NodeType;
        }

        /// <summary>
        /// Expression Node Type
        /// </summary>
        [DataMember]
        public ExpressionType NodeType { get; set; }

        /// <summary>
        /// SerializableType of this Expression
        /// </summary>
        [DataMember]
        public SerializableType SerializableType { get; set; }

        /// <summary>
        /// CLR Type of this Expression
        /// </summary>
        public Type Type
        {
            get
            {
                return SerializableType.GetSystemType();
            }
        }

        protected ITypeTransformations typeTrans { get; private set; }

        /// <summary>
        /// Converts a SerializableExpression to a Linq Expression
        /// </summary>
        /// <param name="ctx">serialization Context</param>
        /// <returns>Linq Expression</returns>
        internal abstract Expression ToExpressionInternal(SerializationContext ctx);
    }

    #region CompoundExpression
    /// <summary>
    /// Serializable Compound Expression
    /// </summary>
    [Serializable]
    public abstract class SerializableCompoundExpression : SerializableExpression
    {
        internal SerializableCompoundExpression(Expression e, SerializableExpression.SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            this.Children = new List<SerializableExpression>();
        }


        /// <summary>
        /// Child Expressions
        /// </summary>
        [DataMember]
        public List<SerializableExpression> Children { get; set; }
    }
    #endregion

    #region BinaryExpression
    /// <summary>
    /// Serializable Binary Expression
    /// </summary>
    [Serializable]
    public class SerializableBinaryExpression : SerializableCompoundExpression
    {
        internal SerializableBinaryExpression(BinaryExpression e, SerializableExpression.SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            Children.Add(SerializableExpression.FromExpression(e.Left, ctx, typeTrans));
            Children.Add(SerializableExpression.FromExpression(e.Right, ctx, typeTrans));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeBinary(NodeType, Children[0].ToExpressionInternal(ctx), Children[1].ToExpressionInternal(ctx));
        }
    }
    #endregion

    #region UnaryExpression
    /// <summary>
    /// Serializable Unary Expression
    /// </summary>
    [Serializable]
    public class SerializableUnaryExpression : SerializableCompoundExpression
    {
        internal SerializableUnaryExpression(UnaryExpression e, SerializableExpression.SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            Children.Add(SerializableExpression.FromExpression(e.Operand, ctx, typeTrans));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeUnary(NodeType, Children[0].ToExpressionInternal(ctx), Type);
        }
    }
    #endregion

    #region ConstantExpression
    /// <summary>
    /// Serializable Constant Expression
    /// </summary>
    [Serializable]
    public class SerializableConstantExpression : SerializableExpression
    {
        internal SerializableConstantExpression(ConstantExpression e, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            Value = e.Value;
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.Constant(Value, Type);
        }

        /// <summary>
        /// Value of this Constant
        /// </summary>
        [DataMember]
        public object Value { get; set; }
    }
    #endregion

    #region MemberExpression
    /// <summary>
    /// Serializable Member Expression
    /// </summary>
    [Serializable]
    public class SerializableMemberExpression : SerializableCompoundExpression
    {
        internal SerializableMemberExpression(MemberExpression e, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            MemberName = e.Member.Name;
            Children.Add(SerializableExpression.FromExpression(e.Expression, ctx, typeTrans));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            Expression e = Children[0].ToExpressionInternal(ctx);
            MemberInfo mi = e.Type.FindFirstOrDefaultMember(MemberName);
            return Expression.MakeMemberAccess(e, mi);
        }

        /// <summary>
        /// Member Name
        /// </summary>
        [DataMember]
        public string MemberName { get; set; }
    }
    #endregion

    #region MethodCallExpression
    /// <summary>
    /// Serializable MethodCall Expression
    /// </summary>
    [Serializable]
    public class SerializableMethodCallExpression : SerializableCompoundExpression
    {
        internal SerializableMethodCallExpression(MethodCallExpression e, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            if (e.Object != null) ObjectExpression = SerializableExpression.FromExpression(e.Object, ctx, typeTrans);

            MethodName = e.Method.Name;
            SerializableMethodType = typeTrans.AsInterfaceType(e.Method.DeclaringType).ToSerializableType();
            ParameterTypes = e.Method.GetParameters().Select(p => typeTrans.AsInterfaceType(p.ParameterType).ToSerializableType()).ToList();
            GenericArguments = e.Method.GetGenericArguments().Select(p => typeTrans.AsInterfaceType(p).ToSerializableType()).ToList();

            if (e.Arguments != null)
            {
                Children = e.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx, typeTrans)).ToList();
            }
        }

        /// <summary>
        /// Method Name
        /// </summary>
        [DataMember]
        public string MethodName { get; set; }

        /// <summary>
        /// Parameter Types
        /// </summary>
        [DataMember]
        public List<SerializableType> ParameterTypes { get; set; }

        /// <summary>
        /// Generic Arguments
        /// </summary>
        [DataMember]
        public List<SerializableType> GenericArguments { get; set; }

        [DataMember]
        public SerializableType SerializableMethodType { get; set; }
        /// <summary>
        /// Type where this Method is implemented
        /// </summary>
        public Type MethodType
        {
            get
            {
                return SerializableMethodType.GetSystemType();
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

                            if (paramSame) return mi;
                        }
                    }
                }
            }

            return null;
        }
        #endregion

        #region GetMethodInfo
        private MethodInfo GetMethodInfo()
        {
            MethodInfo mi;
            if (GenericArguments != null && GenericArguments.Count > 0)
            {
                mi = FindGenericMethod(MethodType, MethodName,
                        GenericArguments.Select(p => p.GetSystemType()).ToArray(),
                        ParameterTypes.Select(p => p.GetSystemType()).ToArray());
            }
            else
            {
                mi = MethodType.GetMethod(MethodName, ParameterTypes.Select(p => p.GetSystemType()).ToArray());
            }

            if (mi == null)
            {
                throw new MissingMethodException(MethodType.FullName, MethodName);
            }
            return mi;
        }
        #endregion

        /// <summary>
        /// Object Expression
        /// </summary>
        [DataMember]
        public SerializableExpression ObjectExpression { get; set; }

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
    /// <summary>
    /// Serializable Lambda Expression
    /// </summary>
    [Serializable]
    public class SerializableLambdaExpression : SerializableCompoundExpression
    {
        internal SerializableLambdaExpression(LambdaExpression e, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            Children.Add(SerializableExpression.FromExpression(e.Body, ctx, typeTrans));
            Children.AddRange(e.Parameters.Select(p => SerializableExpression.FromExpression(p, ctx, typeTrans)));
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
    /// <summary>
    /// Serializable Parameter Expression
    /// </summary>
    [Serializable]
    public class SerializableParameterExpression : SerializableExpression
    {
        internal SerializableParameterExpression(ParameterExpression e, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(e, ctx, typeTrans)
        {
            this.Name = e.Name;
        }

        /// <summary>
        /// Parameter Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            lock (ctx)
            {
                if (!ctx.Parameter.ContainsKey(Name))
                {
                    ctx.Parameter[Name] = Expression.Parameter(Type, Name);
                }
                return ctx.Parameter[Name];
            }
        }
    }
    #endregion

    #region NewExpression
    /// <summary>
    /// Serializable New Expression
    /// </summary>
    [Serializable]
    public class SerializableNewExpression : SerializableCompoundExpression
    {
        [DataMember]
        public ConstructorInfo Constructor { get; set; }

        [DataMember]
        public List<MemberInfo> Members;

        internal SerializableNewExpression(NewExpression source, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(source, ctx, typeTrans)
        {
            Constructor = source.Constructor;
            if (source.Members != null)
            {
                Members = source.Members.ToList();
            }

            Children = source.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx, typeTrans)).ToList();
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            List<Expression> arguments = Children.Select(a => a.ToExpressionInternal(ctx)).ToList();

            if (Members != null)
            {
                return Expression.New(Constructor, arguments, Members.ToArray());
            }
            else
            {
                return Expression.New(Constructor, arguments);
            }
        }
    }
    #endregion

    /// <summary>
    /// Serializable ConditionalExpression
    /// </summary>
    [Serializable]
    public class SerializableConditionalExpression : SerializableExpression
    {
        [DataMember]
        public SerializableExpression Test { get; set; }
        [DataMember]
        public SerializableExpression IfTrue { get; set; }
        [DataMember]
        public SerializableExpression IfFalse { get; set; }

        internal SerializableConditionalExpression(ConditionalExpression source, SerializationContext ctx, ITypeTransformations typeTrans)
            : base(source, ctx, typeTrans)
        {
            Test = SerializableExpression.FromExpression(source.Test, ctx, typeTrans);
            IfTrue = SerializableExpression.FromExpression(source.IfTrue, ctx, typeTrans);
            IfFalse = SerializableExpression.FromExpression(source.IfFalse, ctx, typeTrans);
        }

        internal override Expression ToExpressionInternal(SerializableExpression.SerializationContext ctx)
        {
            return Expression.Condition(Test.ToExpressionInternal(ctx),
                IfTrue.ToExpressionInternal(ctx),
                IfFalse.ToExpressionInternal(ctx));
        }
    }

}
