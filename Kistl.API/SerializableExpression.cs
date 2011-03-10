
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract(Namespace = "http://dasz.at/ZBox/")]
    [KnownType(typeof(SerializableConstructorInfo))]
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

        [DataMember(Name = "Type")]
        public SerializableType Type { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        public MemberInfo GetMemberInfo()
        {
            return this.Type.GetSystemType().GetMember(Name).SingleOrDefault();
        }

        public virtual void ToStream(BinaryWriter binStream)
        {
            if (binStream == null) throw new ArgumentNullException("binStream");

            binStream.Write((byte)0);
            binStream.Write(Name);
            Type.ToStream(binStream);
        }

        internal static SerializableMemberInfo FromStream(BinaryReader binReader)
        {
            var type = binReader.ReadByte();
            switch (type)
            {
                case 0:
                    return new SerializableMemberInfo()
                    {
                        Name = binReader.ReadString(),
                        Type = SerializableType.FromStream(binReader),
                    };
                case 1:
                    return new SerializableConstructorInfo()
                    {
                        Name = binReader.ReadString(),
                        Type = SerializableType.FromStream(binReader),
                        ParameterTypes = SerializableExpression.ReadTypeArray(binReader)
                    };
                default:
                    throw new NotImplementedException(String.Format("unrecognized SerializableMemberInfoType [{0}]", type));
            }
        }
    }

    [DataContract(Namespace = "http://dasz.at/ZBox/")]
    [Serializable]
    public class SerializableConstructorInfo : SerializableMemberInfo
    {
        [DataMember(Name = "ParameterTypes")]
        public SerializableType[] ParameterTypes { get; set; }

        public SerializableConstructorInfo()
        {
            this.ParameterTypes = new SerializableType[] { };
        }

        public SerializableConstructorInfo(ConstructorInfo ci, InterfaceType.Factory iftFactory)
            : base(ci, iftFactory)
        {
            if (ci == null) throw new ArgumentNullException("ci");
            if (iftFactory == null) throw new ArgumentNullException("iftFactory");

            this.ParameterTypes = ci.GetParameters().Select(i => new SerializableType(iftFactory(i.ParameterType), iftFactory)).ToArray();
        }

        public ConstructorInfo GetConstructorInfo()
        {
            return this.Type.GetSystemType().GetConstructor(this.ParameterTypes.Select(i => i.GetSystemType()).ToArray());
        }

        public override void ToStream(BinaryWriter binStream)
        {
            if (binStream == null) throw new ArgumentNullException("binStream");

            binStream.Write((byte)1);
            binStream.Write(Name);
            Type.ToStream(binStream);
            SerializableExpression.WriteTypeArray(binStream, ParameterTypes);
        }
    }

    internal enum SerializableExpressionType
    {
        Binary,
        Unary,
        Constant,
        Member,
        MethodCall,
        Lambda,
        Parameter,
        New,
        Conditional,
    }

    /// <summary>
    /// Abstract Base Class for a serializable Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "Expression")]
    [KnownType(typeof(SerializableType))]
    [KnownType(typeof(SerializableBinaryExpression))]
    [KnownType(typeof(SerializableConditionalExpression))]
    [KnownType(typeof(SerializableConstantExpression))]
    [KnownType(typeof(SerializableCompoundExpression))]
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
        /// Serialization Context for Expressions
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
        /// Serialization Context for streaming
        /// </summary>
        internal class StreamSerializationContext
        {
            private Dictionary<Guid, SerializableParameterExpression> _Parameter = new Dictionary<Guid, SerializableParameterExpression>();
            /// <summary>
            /// Collection of LINQ Parameter
            /// </summary>
            public Dictionary<Guid, SerializableParameterExpression> Parameter
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
        [DataMember(Name = "NodeType")]
        public int NodeType { get; set; }

        /// <summary>
        /// SerializableType of this Expression
        /// </summary>
        [DataMember(Name = "Type")]
        public SerializableType SerializableType { get; set; }

        /// <summary>
        /// CLR Type of this Expression
        /// </summary>
        [IgnoreDataMember]
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

        /// <summary>
        /// Writes this SerializableExpression to the specified stream.
        /// </summary>
        /// <param name="binStream"></param>
        public void ToStream(BinaryWriter binStream)
        {
            if (binStream == null) throw new ArgumentNullException("binStream");

            var ctx = new StreamSerializationContext();

            ToStream(binStream, ctx);
        }

        internal SerializableExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
        {
            this.iftFactory = iftFactory;
            SerializableType = SerializableType.FromStream(binReader);
            NodeType = binReader.ReadInt32();
        }

        /// <remarks>
        /// Inheriting classes need to first write their SerializableExpressionType as byte to the stream, then call this method to write out basic infromation. Afterwards they are free to implement their own members.
        /// </remarks>
        internal virtual void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            this.SerializableType.ToStream(binStream);
            binStream.Write(this.NodeType);
        }

        public static SerializableExpression FromStream(BinaryReader binStream, InterfaceType.Factory iftFactory)
        {
            StreamSerializationContext ctx = new StreamSerializationContext();
            return FromStream(binStream, ctx, iftFactory);
        }

        internal static SerializableExpression FromStream(BinaryReader binStream, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
        {
            var type = (SerializableExpressionType)binStream.ReadByte();
            switch (type)
            {
                case SerializableExpressionType.Binary:
                    return new SerializableBinaryExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.Unary:
                    return new SerializableUnaryExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.Constant:
                    return new SerializableConstantExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.Member:
                    return new SerializableMemberExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.MethodCall:
                    return new SerializableMethodCallExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.Lambda:
                    return new SerializableLambdaExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.Parameter:
                    var parameterGuid = new Guid(binStream.ReadString());
                    if (ctx.Parameter.ContainsKey(parameterGuid))
                    {
                        return ctx.Parameter[parameterGuid];
                    }
                    else
                    {
                        return ctx.Parameter[parameterGuid] = new SerializableParameterExpression(binStream, ctx, iftFactory, parameterGuid);
                    }
                case SerializableExpressionType.New:
                    return new SerializableNewExpression(binStream, ctx, iftFactory);
                case SerializableExpressionType.Conditional:
                    return new SerializableConditionalExpression(binStream, ctx, iftFactory);
                default:
                    throw new NotImplementedException(string.Format("Unknown SerializableExpressionType encountered: [{0}]", type));
            }
        }

        internal static SerializableType[] ReadTypeArray(BinaryReader binReader)
        {
            var types = new List<SerializableType>();
            while (binReader.ReadBoolean())
            {
                types.Add(SerializableType.FromStream(binReader));
            }
            return types.ToArray();
        }

        internal static void WriteTypeArray(BinaryWriter binStream, SerializableType[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                binStream.Write(true);
                types[i].ToStream(binStream);
            }
            binStream.Write(false);
        }
    }

    #region CompoundExpression
    /// <summary>
    /// Serializable Compound Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "CompoundExpression")]
    public abstract class SerializableCompoundExpression : SerializableExpression
    {
        internal SerializableCompoundExpression(Expression e, SerializableExpression.SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            this.Children = new SerializableExpression[] { };
        }

        internal SerializableCompoundExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
            var children = new List<SerializableExpression>();
            while (binReader.ReadBoolean())
            {
                children.Add(SerializableExpression.FromStream(binReader, ctx, iftFactory));
            }
            this.Children = children.ToArray();
        }

        /// <summary>
        /// Child Expressions
        /// </summary>
        [DataMember(Name = "Children")]
        public SerializableExpression[] Children { get; set; }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            // do net render SerializableExpressionType for abstract, intermediate class
            base.ToStream(binStream, ctx);
            for (int i = 0; i < Children.Length; i++)
            {
                binStream.Write(true);
                Children[i].ToStream(binStream, ctx);
            }
            binStream.Write(false);
        }
    }
    #endregion

    #region BinaryExpression
    /// <summary>
    /// Serializable Binary Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "BinaryExpression")]
    public class SerializableBinaryExpression : SerializableCompoundExpression
    {
        internal SerializableBinaryExpression(BinaryExpression e, SerializableExpression.SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            Children = new[] { SerializableExpression.FromExpression(e.Left, ctx, iftFactory), SerializableExpression.FromExpression(e.Right, ctx, iftFactory) };
        }

        internal SerializableBinaryExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeBinary((ExpressionType)NodeType, Children[0].ToExpressionInternal(ctx), Children[1].ToExpressionInternal(ctx));
        }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Binary);
            base.ToStream(binStream, ctx);
        }
    }
    #endregion

    #region UnaryExpression
    /// <summary>
    /// Serializable Unary Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "UnaryExpression")]
    public class SerializableUnaryExpression : SerializableCompoundExpression
    {
        internal SerializableUnaryExpression(UnaryExpression e, SerializableExpression.SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            Children = new[] { SerializableExpression.FromExpression(e.Operand, ctx, iftFactory) };
        }

        internal SerializableUnaryExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.MakeUnary((ExpressionType)NodeType, Children[0].ToExpressionInternal(ctx), Type);
        }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Unary);
            base.ToStream(binStream, ctx);
        }
    }
    #endregion

    #region ConstantExpression
    /// <summary>
    /// Serializable Constant Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "ConstantExpression")]
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

        internal SerializableConstantExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
            var isNull = binReader.ReadBoolean();
            if (isNull)
            {
                Value = null;
            }
            else
            {
                // Deserialize only basic types
                if (Type.IsAssignableFrom(typeof(int)) || Type.IsEnum)
                {
                    Value = binReader.ReadInt32();
                }
                else if (Type.IsAssignableFrom(typeof(bool)))
                {
                    Value = binReader.ReadBoolean();
                }
                else if (Type.IsAssignableFrom(typeof(double)))
                {
                    Value = binReader.ReadDouble();
                }
                else if (Type.IsAssignableFrom(typeof(float)))
                {
                    Value = binReader.ReadSingle();
                }
                else if (Type == typeof(string))
                {
                    Value = binReader.ReadString();
                }
                else if (Type.IsAssignableFrom(typeof(decimal)))
                {
                    Value = binReader.ReadDecimal();
                }
                else if (Type.IsAssignableFrom(typeof(DateTime)))
                {
                    DateTime val;
                    BinarySerializer.FromStream(out val, binReader);
                    Value = val;
                }
                else if (Type.IsAssignableFrom(typeof(Guid)))
                {
                    Guid val;
                    BinarySerializer.FromStream(out val, binReader);
                    Value = val;
                }
                else
                {
                    throw new NotSupportedException(string.Format("Can't deserialize Value of type '{0}'.", Type));
                }
            }
            //var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //// let's hope that mono has at least the basic types covered here!
            //Value = bf.Deserialize(binReader.BaseStream);
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
        [DataMember(Name = "Value")]
        public object Value { get; set; }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Constant);
            base.ToStream(binStream, ctx);

            if (Value == null)
            {
                // IsNull
                binStream.Write(true);
            }
            else
            {
                // IsNull
                binStream.Write(false);
                // Serialize only basic types
                if (Type.IsAssignableFrom(typeof(int)) || Type.IsEnum)
                {
                    binStream.Write((int)Value);
                }
                else if (Type.IsAssignableFrom(typeof(bool)))
                {
                    binStream.Write((bool)Value);
                }
                else if (Type.IsAssignableFrom(typeof(double)))
                {
                    binStream.Write((double)Value);
                }
                else if (Type.IsAssignableFrom(typeof(float)))
                {
                    binStream.Write((float)Value);
                }
                else if (Type == typeof(string))
                {
                    binStream.Write((string)Value);
                }
                else if (Type.IsAssignableFrom(typeof(decimal)))
                {
                    binStream.Write((decimal)Value);
                }
                else if (Type.IsAssignableFrom(typeof(DateTime)))
                {
                    BinarySerializer.ToStream((DateTime)Value, binStream);
                }
                else if (Type.IsAssignableFrom(typeof(Guid)))
                {
                    BinarySerializer.ToStream((Guid)Value, binStream);
                }
                else
                {
                    throw new NotSupportedException(string.Format("Can't serialize Value '{0}' of type '{1}'.", Value, Type));
                }
            }
        }
    }
    #endregion

    #region MemberExpression
    /// <summary>
    /// Serializable Member Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "MemberExpression")]
    public class SerializableMemberExpression : SerializableCompoundExpression
    {
        internal SerializableMemberExpression(MemberExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            MemberName = e.Member.Name;
            Children = new[] { SerializableExpression.FromExpression(e.Expression, ctx, iftFactory) };
        }

        internal SerializableMemberExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
            MemberName = binReader.ReadString();
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
        [DataMember(Name = "MemberName")]
        public string MemberName { get; set; }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Member);
            base.ToStream(binStream, ctx);
            binStream.Write(this.MemberName);
        }
    }
    #endregion

    #region MethodCallExpression
    /// <summary>
    /// Serializable MethodCall Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "MethodCallExpression")]
    public class SerializableMethodCallExpression : SerializableCompoundExpression
    {
        internal SerializableMethodCallExpression(MethodCallExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            if (e.Object != null) ObjectExpression = SerializableExpression.FromExpression(e.Object, ctx, iftFactory);

            MethodName = e.Method.Name;
            SerializableMethodType = iftFactory(e.Method.DeclaringType).ToSerializableType();
            ParameterTypes = e.Method.GetParameters().Select(p => iftFactory(p.ParameterType).ToSerializableType()).ToArray();
            GenericArguments = e.Method.GetGenericArguments().Select(p => iftFactory(p).ToSerializableType()).ToArray();

            if (e.Arguments != null)
            {
                Children = e.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx, iftFactory)).ToArray();
            }
        }

        internal SerializableMethodCallExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
            var hasObject = binReader.ReadBoolean();
            if (hasObject) ObjectExpression = SerializableExpression.FromStream(binReader, ctx, iftFactory);

            MethodName = binReader.ReadString();
            SerializableMethodType = SerializableType.FromStream(binReader);
            ParameterTypes = ReadTypeArray(binReader);
            GenericArguments = ReadTypeArray(binReader);
        }

        /// <summary>
        /// Method Name
        /// </summary>
        [DataMember(Name = "MethodName")]
        public string MethodName { get; set; }

        /// <summary>
        /// Parameter Types
        /// </summary>
        [DataMember(Name = "ParameterTypes")]
        public SerializableType[] ParameterTypes { get; set; }

        /// <summary>
        /// Generic Arguments
        /// </summary>
        [DataMember(Name = "GenericArguments")]
        public SerializableType[] GenericArguments { get; set; }

        [DataMember(Name = "SerializableMethodType")]
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
            if (GenericArguments != null && GenericArguments.Length > 0)
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
        [DataMember(Name = "ObjectExpression")]
        public SerializableExpression ObjectExpression { get; set; }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.Call(
                ObjectExpression == null ? null : ObjectExpression.ToExpressionInternal(ctx),
                GetMethodInfo(),
                Children.Select(e => e.ToExpressionInternal(ctx)));
        }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.MethodCall);
            base.ToStream(binStream, ctx);
            if (this.ObjectExpression != null)
            {
                binStream.Write(true);
                this.ObjectExpression.ToStream(binStream, ctx);
            }
            else
            {
                binStream.Write(false);
            }
            binStream.Write(MethodName);
            this.SerializableMethodType.ToStream(binStream);
            WriteTypeArray(binStream, ParameterTypes);
            WriteTypeArray(binStream, GenericArguments);
        }
    }
    #endregion

    #region LambdaExpression
    /// <summary>
    /// Serializable Lambda Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "LambdaExpression")]
    public class SerializableLambdaExpression : SerializableCompoundExpression
    {
        internal SerializableLambdaExpression(LambdaExpression e, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(e, ctx, iftFactory)
        {
            Children = new[] { SerializableExpression.FromExpression(e.Body, ctx, iftFactory) }
                .Union(e.Parameters.Select(p => SerializableExpression.FromExpression(p, ctx, iftFactory))).ToArray();
        }

        internal SerializableLambdaExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            List<ParameterExpression> parameters = new List<ParameterExpression>();
            foreach (SerializableExpression p in Children.Skip(1))
                parameters.Add((ParameterExpression)p.ToExpressionInternal(ctx));

            return Expression.Lambda(Children[0].ToExpressionInternal(ctx), parameters.ToArray());
        }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Lambda);
            base.ToStream(binStream, ctx);
        }
    }
    #endregion

    #region ParameterExpression
    /// <summary>
    /// Serializable Parameter Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "ParameterExpression")]
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

        internal SerializableParameterExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory, Guid parameterGuid)
            : base(binReader, ctx, iftFactory)
        {
            this.Name = binReader.ReadString();
            this.Guid = parameterGuid;
        }

        /// <summary>
        /// Parameter Name
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Guid to find parameter instance
        /// </summary>
        [DataMember(Name = "Guid")]
        public Guid Guid { get; set; }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            if (!ctx.Parameter.ContainsKey(Guid))
            {
                ctx.Parameter[Guid] = Expression.Parameter(Type, Name);
            }
            return ctx.Parameter[Guid];
        }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Parameter);
            binStream.Write(this.Guid.ToString());

            // shortcut serialization if we already went into the stream at an earlier position
            if (ctx.Parameter.ContainsKey(this.Guid))
                return;
            else
                ctx.Parameter[this.Guid] = this;

            base.ToStream(binStream, ctx);
            binStream.Write(this.Name);
        }
    }
    #endregion

    #region NewExpression
    /// <summary>
    /// Serializable New Expression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "NewExpression")]
    public class SerializableNewExpression : SerializableCompoundExpression
    {
        [DataMember(Name = "Constructor")]
        public SerializableConstructorInfo Constructor { get; set; }

        [DataMember(Name = "Members")]
        public SerializableMemberInfo[] Members;

        internal SerializableNewExpression(NewExpression source, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(source, ctx, iftFactory)
        {
            Constructor = new SerializableConstructorInfo(source.Constructor, iftFactory);
            if (source.Members != null)
            {
                Members = source.Members.Select(i => new SerializableMemberInfo(i, iftFactory)).ToArray();
            }

            Children = source.Arguments.Select(a => SerializableExpression.FromExpression(a, ctx, iftFactory)).ToArray();
        }

        internal SerializableNewExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
            Constructor = (SerializableConstructorInfo)SerializableMemberInfo.FromStream(binReader);

            var members = new List<SerializableMemberInfo>();
            while (binReader.ReadBoolean())
            {
                members.Add(SerializableMemberInfo.FromStream(binReader));
            }
            this.Members = members.ToArray();
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

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.New);
            base.ToStream(binStream, ctx);
            Constructor.ToStream(binStream);
            for (int i = 0; i < Members.Length; i++)
            {
                binStream.Write(true);
                Members[i].ToStream(binStream);
            }
            binStream.Write(false);
        }
    }
    #endregion

    #region ConditionalExpression
    /// <summary>
    /// Serializable ConditionalExpression
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/ZBox/", Name = "ConditionalExpression")]
    public class SerializableConditionalExpression : SerializableExpression
    {
        [DataMember(Name = "Test")]
        public SerializableExpression Test { get; set; }
        [DataMember(Name = "IfTrue")]
        public SerializableExpression IfTrue { get; set; }
        [DataMember(Name = "IfFalse")]
        public SerializableExpression IfFalse { get; set; }

        internal SerializableConditionalExpression(ConditionalExpression source, SerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(source, ctx, iftFactory)
        {
            Test = SerializableExpression.FromExpression(source.Test, ctx, iftFactory);
            IfTrue = SerializableExpression.FromExpression(source.IfTrue, ctx, iftFactory);
            IfFalse = SerializableExpression.FromExpression(source.IfFalse, ctx, iftFactory);
        }

        internal SerializableConditionalExpression(BinaryReader binReader, StreamSerializationContext ctx, InterfaceType.Factory iftFactory)
            : base(binReader, ctx, iftFactory)
        {
            Test = SerializableExpression.FromStream(binReader, ctx, iftFactory);
            IfTrue = SerializableExpression.FromStream(binReader, ctx, iftFactory);
            IfFalse = SerializableExpression.FromStream(binReader, ctx, iftFactory);
        }

        internal override Expression ToExpressionInternal(SerializableExpression.SerializationContext ctx)
        {
            return Expression.Condition(Test.ToExpressionInternal(ctx),
                IfTrue.ToExpressionInternal(ctx),
                IfFalse.ToExpressionInternal(ctx));
        }

        internal override void ToStream(BinaryWriter binStream, StreamSerializationContext ctx)
        {
            binStream.Write((byte)SerializableExpressionType.Conditional);
            base.ToStream(binStream, ctx);
            Test.ToStream(binStream, ctx);
            IfTrue.ToStream(binStream, ctx);
            IfFalse.ToStream(binStream, ctx);
        }
    }
    #endregion

}
