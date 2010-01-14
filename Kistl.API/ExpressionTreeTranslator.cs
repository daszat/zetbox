using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Kistl.API
{
    [Serializable]
    public class ExpressionTreeTranslator
    {
        public virtual Expression Visit(Expression e)
        {
            if (e == null) return null;

            switch (e.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    {
                        return VisitUnary((UnaryExpression)e);
                    }
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    {
                        return VisitBinary((BinaryExpression)e);
                    }
                case ExpressionType.TypeIs:
                    {
                        return VisitTypeIs((TypeBinaryExpression)e);
                    }
                case ExpressionType.Conditional:
                    {
                        return VisitConditional((ConditionalExpression)e);
                    }
                case ExpressionType.Constant:
                    {
                        return VisitConstant((ConstantExpression)e);
                    }
                case ExpressionType.Parameter:
                    {
                        return VisitParameter((ParameterExpression)e);
                    }
                case ExpressionType.MemberAccess:
                    {
                        return VisitMemberAccess((MemberExpression)e);
                    }
                case ExpressionType.Call:
                    {
                        return VisitMethodCall((MethodCallExpression)e);
                    }
                case ExpressionType.Lambda:
                    {
                        return VisitLambda((LambdaExpression)e);
                    }
                case ExpressionType.New:
                    {
                        return VisitNew((NewExpression)e);
                    }
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    {
                        return VisitNewArray((NewArrayExpression)e);
                    }
                case ExpressionType.Invoke:
                    {
                        return VisitInvocation((InvocationExpression)e);
                    }
                case ExpressionType.MemberInit:
                    {
                        return VisitMemberInit((MemberInitExpression)e);
                    }
                case ExpressionType.ListInit:
                    {
                        return VisitListInit((ListInitExpression)e);
                    }
                default:
                    {
                        throw new NotSupportedException(string.Format("Unknown expression type: '{0}'", e.NodeType));
                    }
            }
        }

        protected virtual MemberBinding VisitBinding(MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    return VisitMemberAssignment((MemberAssignment)binding);
                case MemberBindingType.MemberBinding:
                    return VisitMemberMemberBinding((MemberMemberBinding)binding);
                case MemberBindingType.ListBinding:
                    return VisitMemberListBinding((MemberListBinding)binding);
                default:
                    throw new NotSupportedException(string.Format("Unknown binding type '{0}'", binding.BindingType));
            }
        }

        protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
        {
            return Expression.ElementInit(initializer.AddMethod, 
                this.VisitExpressionList(initializer.Arguments));
        }

        protected virtual Expression VisitUnary(UnaryExpression u)
        {
            return Expression.MakeUnary(u.NodeType, Visit(u.Operand), u.Type, u.Method);
        }

        protected virtual Expression VisitBinary(BinaryExpression b)
        {
            Expression left = Visit(b.Left);
            Expression right = Visit(b.Right);
            Expression conv = Visit(b.Conversion);

            if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
                return Expression.Coalesce(left, right, conv as LambdaExpression);
            else
                return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
        }

        protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
        {
            return Expression.TypeIs(Visit(b.Expression), b.TypeOperand);
        }

        protected virtual Expression VisitConstant(ConstantExpression c)
        {
            return c;
        }

        protected virtual Expression VisitConditional(ConditionalExpression c)
        {
            return Expression.Condition(Visit(c.Test), Visit(c.IfTrue), Visit(c.IfFalse));
        }

        protected virtual ParameterExpression VisitParameter(ParameterExpression p)
        {
            return p;
        }

        protected virtual Expression VisitMemberAccess(MemberExpression m)
        {
            return Expression.MakeMemberAccess(Visit(m.Expression), m.Member);
        }

        protected virtual Expression VisitMethodCall(MethodCallExpression m)
        {
            return Expression.Call(Visit(m.Object), m.Method, VisitExpressionList(m.Arguments));
        }

        protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> list)
        {
            List<Expression> result = new List<Expression>();
            list.ForEach<Expression>(e => result.Add(Visit(e)));
            return result.AsReadOnly();
        }

        protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            return Expression.Bind(assignment.Member, Visit(assignment.Expression));
        }

        protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            return Expression.MemberBind(binding.Member, VisitBindingList(binding.Bindings));
        }

        protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        {
            return Expression.ListBind(binding.Member, VisitElementInitializerList(binding.Initializers));
        }

        protected virtual ReadOnlyCollection<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> list)
        {
            List<MemberBinding> result = new List<MemberBinding>();
            list.ForEach<MemberBinding>(e => result.Add(VisitBinding(e)));
            return result.AsReadOnly();
        }

        protected virtual ReadOnlyCollection<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> list)
        {
            List<ElementInit> result = new List<ElementInit>();
            list.ForEach<ElementInit>(e => result.Add(VisitElementInitializer(e)));
            return result.AsReadOnly();
        }

        protected virtual ReadOnlyCollection<ParameterExpression> VisitParameterList(ReadOnlyCollection<ParameterExpression> list)
        {
            List<ParameterExpression> result = new List<ParameterExpression>();
            list.ForEach<ParameterExpression>(e => result.Add(VisitParameter(e)));
            return result.AsReadOnly();
        }

        protected virtual Expression VisitLambda(LambdaExpression lambda)
        {
            return Expression.Lambda(lambda.Type, Visit(lambda.Body), VisitParameterList(lambda.Parameters));
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "correct grammar for this method name")]
        protected virtual NewExpression VisitNew(NewExpression newExpression)
        {
            var args = VisitExpressionList(newExpression.Arguments);
            if (newExpression.Members != null)
                return Expression.New(newExpression.Constructor, args, newExpression.Members);
            else
                return Expression.New(newExpression.Constructor, args);
        }

        protected virtual MemberInitExpression VisitMemberInit(MemberInitExpression init)
        {
            return Expression.MemberInit(VisitNew(init.NewExpression), VisitBindingList(init.Bindings));
        }

        protected virtual ListInitExpression VisitListInit(ListInitExpression init)
        {
            return Expression.ListInit(VisitNew(init.NewExpression), VisitElementInitializerList(init.Initializers));
        }

        protected virtual NewArrayExpression VisitNewArray(NewArrayExpression na)
        {
            var e = VisitExpressionList(na.Expressions);
            Type type = na.Type.FindElementTypes().First();
            if (na.NodeType == ExpressionType.NewArrayInit)
            {
                return Expression.NewArrayInit(type, e);
            }
            else
            {
                return Expression.NewArrayBounds(type, e);
            }
        }

        protected virtual Expression VisitInvocation(InvocationExpression iv)
        {
            return Expression.Invoke(Visit(iv.Expression), VisitExpressionList(iv.Arguments));
        }
    }
}
