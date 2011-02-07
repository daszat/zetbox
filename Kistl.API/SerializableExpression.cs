using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Kistl.API
{
    [DataContract(Namespace = "http://dasz.at/ZBox/")]
    [Serializable]
    public class SerializableMemberInfo
    {
        public SerializableMemberInfo()
        {
        }

        public SerializableMemberInfo(MemberInfo mi, InterfaceType.Factory iftFactory)
        {
            if (mi == null) throw new ArgumentNullException("mi");
            if (iftFactory == null) throw new ArgumentNullException("iftFactory");

            this.Type = new SerializableType(iftFactory(mi.DeclaringType), iftFactory);
            this.Name = mi.Name;
        }

        [DataMember]
        public SerializableType Type { get; set; }
        [DataMember]
        public string Name { get; set; }

        public MemberInfo GetMemberInfo()
        {
            return this.Type.GetSystemType().GetMember(Name).SingleOrDefault();
        }
    }

    [DataContract(Namespace = "http://dasz.at/ZBox/")]
    [Serializable]
    public class SerializableConstructorInfo : SerializableMemberInfo
    {
        [DataMember]
        public List<SerializableType> ParameterTypes { get; set; }

        public SerializableConstructorInfo()
        {
            this.ParameterTypes = new List<SerializableType>();
        }

        public SerializableConstructorInfo(ConstructorInfo ci, InterfaceType.Factory iftFactory)
            : base(ci, iftFactory)
        {
            if (ci == null) throw new ArgumentNullException("ci");
            if (iftFactory == null) throw new ArgumentNullException("iftFactory");

            this.ParameterTypes = new List<SerializableType>(ci.GetParameters().Select(i => new SerializableType(iftFactory(i.ParameterType), iftFactory)).ToArray());
        }

        public ConstructorInfo GetConstructorInfo()
        {
            return this.Type.GetSystemType().GetConstructor(this.ParameterTypes.Select(i => i.GetSystemType()).ToArray());
        }
    }

    /// <summary>
    /// Abstract Base Class for a serializable Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/")]
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
        /// <param name="iftFactory"></param>
        /// <returns>serializable Expression</returns>
        public static SerializableExpression FromExpression(Expression e, InterfaceType.Factory iftFactory)
        {
            SerializationContext ctx = new SerializationContext();
            return FromExpression(e, ctx, iftFactory);
        }

        /// <summary>
        /// Creates a SerializableExpression from an Expression
        /// </summary>
        /// <param name="e">Linq Expression</param>
        /// <param name="ctx">Serialization Context</param>
        /// <param name="iftFactory"></param>
        // TODO: use ExpressionTreeVisitor/Translator
        internal static SerializableExpression FromExpression(Expression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
        {
            if (e == null) throw new ArgumentNullException("e");

            if (e is BinaryExpression)
                return new SerializableBinaryExpression((BinaryExpression)e, ctx, iftFactory);

            if (e is UnaryExpression)
                return new SerializableUnaryExpression((UnaryExpression)e, ctx, iftFactory);

            if (e is ConstantExpression)
                return new SerializableConstantExpression((ConstantExpression)e, ctx, iftFactory);

            if (e is MemberExpression)
            {
                MemberExpression exp = (MemberExpression)e;
                if (exp.Expression is ConstantExpression)
                {
                    ConstantExpression left = (ConstantExpression)exp.Expression;
                    if (exp.Member is PropertyInfo)
                    {
                        return new SerializableConstantExpression(Expression.Constant(((PropertyInfo)exp.Member).GetValue(left.Value, null)), ctx, iftFactory);
                    }
                    else if (exp.Member is FieldInfo)
                    {
                        return new SerializableConstantExpression(Expression.Constant(((FieldInfo)exp.Member).GetValue(left.Value)), ctx, iftFactory);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                return new SerializableMemberExpression((MemberExpression)e, ctx, iftFactory);
            }

            if (e is LambdaExpression)
                return new SerializableLambdaExpression((LambdaExpression)e, ctx, iftFactory);

            if (e is MethodCallExpression)
                return new SerializableMethodCallExpression((MethodCallExpression)e, ctx, iftFactory);

            if (e is ParameterExpression)
                return new SerializableParameterExpression((ParameterExpression)e, ctx, iftFactory);

            if (e is NewExpression)
                return new SerializableNewExpression((NewExpression)e, ctx, iftFactory);

            if (e is ConditionalExpression)
                return new SerializableConditionalExpression((ConditionalExpression)e, ctx, iftFactory);

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
            private Dictionary<Guid, Expression> _Parameter = new Dictionary<Guid, Expression>();
            /// <summary>
            /// Collection of LINQ Parameter
            /// </summary>
            public Dictionary<Guid, Expression> Parameter
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
        /// <param name="iftFactory"></param>
        internal SerializableExpression(Expression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
        {
            this.iftFactory = iftFactory;
            SerializableType = iftFactory(e.Type).ToSerializableType();
            NodeType = (int)e.NodeType;
        }

        /// <summary>
        /// Expression Node Type
        /// </summary>
        [DataMember]
        public int NodeType { get; set; }

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

        [NonSerialized]
        protected readonly InterfaceType.Factory iftFactory;

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
        internal SerializableCompoundExpression(Expression e, SerializableExpression.SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
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
        internal SerializableBinaryExpression(BinaryExpression e, SerializableExpression.SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            Children.Add(SerializableExpression.FromExpression(e.Left, ctx, iftFactory));
            Children.Add(SerializableExpression.FromExpression(e.Right, ctx, iftFactory));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeBinary((ExpressionType)NodeType, Children[0].ToExpressionInternal(ctx), Children[1].ToExpressionInternal(ctx));
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
        internal SerializableUnaryExpression(UnaryExpression e, SerializableExpression.SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            Children.Add(SerializableExpression.FromExpression(e.Operand, ctx, iftFactory));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeUnary((ExpressionType)NodeType, Children[0].ToExpressionInternal(ctx), Type);
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
        internal SerializableConstantExpression(ConstantExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            // Handle Enums
            if (e.Value != null && e.Value.GetType().IsEnum)
            {
                Value = (int)e.Value;
            }
            else
            {
                Value = e.Value;
            }
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            // Handle Enums
            if (Value != null && Type.IsEnum)
            {
                return Expression.Constant(Enum.GetValues(Type).AsQueryable().OfType<object>().FirstOrDefault(i => (int)i == (int)Value), Type);
            }
            else if (Value != null && Type.IsGenericType && Type.GetGenericTypeDefinition() == typeof(Nullable<>) && Type.GetGenericArguments().Single().IsEnum)
            {
                return Expression.Constant(Enum.GetValues(Type.GetGenericArguments()[0]).AsQueryable().OfType<object>().FirstOrDefault(i => (int)i == (int)Value), Type);
            }
            else
            {
                return Expression.Constant(Value, Type);
            }
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
        internal SerializableMemberExpression(MemberExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            MemberName = e.Member.Name;
            Children.Add(SerializableExpression.FromExpression(e.Expression, ctx, iftFactory));
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
        internal SerializableMethodCallExpression(MethodCallExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            if (e.Object != null) ObjectExpression = SerializableExpression.FromExpression(e.Object, ctx, iftFactory);

            MethodName = e.Method.Name;
            SerializableMethodType = iftFactory(e.Method.DeclaringType).ToSerializableType();
            ParameterTypes = e.Method.GetParameters().Select(p => iftFactory(p.ParameterType).ToSerializableType()).ToList();
            GenericArguments = e.Method.GetGenericArguments().Select(p => iftFactory(p).ToSerializableType()).ToList();

            if (e.Arguments != null)
            {
                Children = e.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx, iftFactory)).ToList();
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
        [IgnoreDataMember]
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
                    if (method.IsGenericMethod && method.Name == methodName)
                    {
                        if (method.GetGenericArguments().Length != typeArguments.Length) continue;
                        if (method.GetParameters().Length != parameterTypes.Length) continue;

                        MethodInfo mi = method.MakeGenericMethod(typeArguments);
                        ParameterInfo[] parameters = mi.GetParameters();

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
        internal SerializableLambdaExpression(LambdaExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            Children.Add(SerializableExpression.FromExpression(e.Body, ctx, iftFactory));
            Children.AddRange(e.Parameters.Select(p => SerializableExpression.FromExpression(p, ctx, iftFactory)));
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
        internal SerializableParameterExpression(ParameterExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            this.Name = e.Name;
            if (!ctx.Parameter.ContainsValue(e))
            {
                this.Guid = Guid.NewGuid();
                ctx.Parameter[this.Guid] = e;
            }
            else
            {
                this.Guid = ctx.Parameter.Single(i => i.Value == e).Key;
            }
        }

        /// <summary>
        /// Parameter Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Guid to find parameter instance
        /// </summary>
        [DataMember]
        public Guid Guid { get; set; }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            if (!ctx.Parameter.ContainsKey(Guid))
            {
                ctx.Parameter[Guid] = Expression.Parameter(Type, Name);
            }
            return ctx.Parameter[Guid];
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
        public SerializableConstructorInfo Constructor { get; set; }

        [DataMember]
        public List<SerializableMemberInfo> Members;

        internal SerializableNewExpression(NewExpression source, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(source, ctx, iftFactory)
        {
            Constructor = new SerializableConstructorInfo(source.Constructor, iftFactory);
            if (source.Members != null)
            {
                Members = source.Members.Select(i => new SerializableMemberInfo(i, iftFactory)).ToList();
            }

            Children = source.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx, iftFactory)).ToList();
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            List<Expression> arguments = Children.Select(a => a.ToExpressionInternal(ctx)).ToList();

            if (Members != null)
            {
                return Expression.New(Constructor.GetConstructorInfo(), arguments, Members.Select(i => i.GetMemberInfo()).ToArray());
            }
            else
            {
                return Expression.New(Constructor.GetConstructorInfo(), arguments);
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

        internal SerializableConditionalExpression(ConditionalExpression source, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(source, ctx, iftFactory)
        {
            Test = SerializableExpression.FromExpression(source.Test, ctx, iftFactory);
            IfTrue = SerializableExpression.FromExpression(source.IfTrue, ctx, iftFactory);
            IfFalse = SerializableExpression.FromExpression(source.IfFalse, ctx, iftFactory);
        }

        internal override Expression ToExpressionInternal(SerializableExpression.SerializationContext ctx)
        {
            return Expression.Condition(Test.ToExpressionInternal(ctx),
                IfTrue.ToExpressionInternal(ctx),
                IfFalse.ToExpressionInternal(ctx));
        }
    }

}
