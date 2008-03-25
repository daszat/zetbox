using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Kistl.API
{
    #region SerializableType
    [Serializable]
    public class SerializableType
    {
        public enum SerializeDirection
        {
            None,
            ClientToServer,
            ServerToClient,
        }

        public SerializableType(Type type, SerializeDirection direction)
        {
            TypeName = type.FullName;
            AssemblyQualifiedName = type.AssemblyQualifiedName;

            switch (direction)
            {
                case SerializeDirection.ClientToServer:
                    AssemblyQualifiedName = AssemblyQualifiedName.Replace(".Client", ".Server");
                    break;
                case SerializeDirection.ServerToClient:
                    AssemblyQualifiedName = AssemblyQualifiedName.Replace(".Server", ".Client");
                    break;
            }

            _GenericTypeParameter = new List<SerializableType>();
            type.GetGenericArguments().ForEach<Type>(t => _GenericTypeParameter.Add(new SerializableType(t, direction)));
        }

        public string TypeName { get; set; }
        public string AssemblyQualifiedName { get; set; }

        private List<SerializableType> _GenericTypeParameter;
        public List<SerializableType> GenericTypeParameter
        {
            get
            {
                return _GenericTypeParameter;
            }
        }

        public Type Type
        {
            get
            {
                Type result;
                if (_GenericTypeParameter.Count > 0)
                {
                    Type[] g = new Type[_GenericTypeParameter.Count + 1];
                    g[0] = Type.GetType(AssemblyQualifiedName);
                    int i = 0;
                    _GenericTypeParameter.ForEach(t => g[++i] = t.Type);
                    result = Type.MakeGenericType(g);
                }
                else
                {
                    result = Type.GetType(AssemblyQualifiedName, true);
                }

                if (result == null)
                {
                    throw new InvalidOperationException(string.Format("Unable to create Type {0}{1}",
                        TypeName,
                        _GenericTypeParameter.Count > 0 ? "<Generic>" : ""));
                }
                return result;
            }
        }
    }
    #endregion

    [Serializable]
    public abstract class SerializableExpression
    {
        internal class SerializationContext
        {
            public SerializableType.SerializeDirection Direction { get; set; }
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
            _Type = new SerializableType(e.Type, ctx.Direction);
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
                return _Type.Type;
            }
        }

        public static SerializableExpression FromExpression(Expression e, SerializableType.SerializeDirection direction)
        {
            SerializationContext ctx = new SerializationContext();
            ctx.Direction = direction;

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

            throw new NotSupportedException(string.Format("Nodetype {0} is not supported: {1}", e.NodeType, e.ToString()));
        }

        public virtual Expression ToExpression()
        {
            SerializationContext ctx = new SerializationContext();
            ctx.Direction = SerializableType.SerializeDirection.None;

            return ToExpressionInternal(ctx);
        }

        internal abstract Expression ToExpressionInternal(SerializationContext ctx);
    }

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

    [Serializable]
    public class SerializableConstantExpression : SerializableExpression
    {
        internal SerializableConstantExpression(ConstantExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            _value = e.Value;
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.Constant(_value, Type);
        }

        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = value;
            }
        }
    }

    [Serializable]
    public class SerializableMemberExpression : SerializableCompoundExpression
    {
        internal SerializableMemberExpression(MemberExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            _memberName = e.Member.Name;
            Children.Add(SerializableExpression.FromExpression(e.Expression, ctx));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return MemberExpression.PropertyOrField(Children[0].ToExpressionInternal(ctx), _memberName);
        }

        private string _memberName;
        public string MemberName
        {
            get
            {
                return _memberName;
            }
        }
    }

    [Serializable]
    public class SerializableMethodCallExpression : SerializableCompoundExpression
    {
        internal SerializableMethodCallExpression(MethodCallExpression e, SerializationContext ctx)
            : base(e, ctx)
        {            
            if(e.Object != null) _objectExpression = SerializableExpression.FromExpression(e.Object, ctx);
            _method = e.Method;

            if (e.Arguments != null)
            {
                foreach (Expression ce in e.Arguments)
                {
                    Children.Add(SerializableExpression.FromExpression(ce, ctx));
                }
            }
        }

        // TODO: MethodInfo muss selbst serialisiert werden, 
        // da es leider, leider Methoden gibt, die KistlObjekte als GenericsArguments verwenden...
        private MethodInfo _method;
        public MethodInfo Method
        {
            get
            {
                return _method;
            }
        }

        private SerializableExpression _objectExpression;
        public SerializableExpression ObjectExpression
        {
            get
            {
                return _objectExpression;
            }
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            return Expression.Call(
                _objectExpression == null ? null : _objectExpression.ToExpressionInternal(ctx), _method,
                Children.Select(e => e.ToExpressionInternal(ctx)));
        }
    }

    [Serializable]
    public class SerializableLambdaExpression : SerializableCompoundExpression
    {
        internal SerializableLambdaExpression(LambdaExpression e, SerializationContext ctx)
            : base(e, ctx)
        {
            Children.Add(SerializableExpression.FromExpression(e.Body, ctx));

            foreach (Expression p in e.Parameters)
                Children.Add(SerializableExpression.FromExpression(p, ctx));
        }

        internal override Expression ToExpressionInternal(SerializationContext ctx)
        {
            List<ParameterExpression> parameters = new List<ParameterExpression>();
            foreach (SerializableExpression p in Children.Skip(1))
                parameters.Add((ParameterExpression)p.ToExpressionInternal(ctx));

            return Expression.Lambda(Children[0].ToExpressionInternal(ctx), parameters.ToArray());
        }
    }

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
}
