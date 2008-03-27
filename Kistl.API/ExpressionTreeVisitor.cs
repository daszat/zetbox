using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace Kistl.API
{
    public class ExpressionTreeVisitor
    {
        public virtual void Visit(Expression e)
        {
            if (e == null) return;

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
                        VisitUnary((UnaryExpression)e);
                        break;
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
                        VisitBinary((BinaryExpression)e);
                        break;
                    }
                case ExpressionType.TypeIs:
                    {
                        VisitTypeIs((TypeBinaryExpression)e);
                        break;
                    }
                case ExpressionType.Conditional:
                    {
                        VisitConditional((ConditionalExpression)e);
                        break;
                    }
                case ExpressionType.Constant:
                    {
                        VisitConstant((ConstantExpression)e);
                        break;
                    }
                case ExpressionType.Parameter:
                    {
                        VisitParameter((ParameterExpression)e);
                        break;
                    }
                case ExpressionType.MemberAccess:
                    {
                        VisitMemberAccess((MemberExpression)e);
                        break;
                    }
                case ExpressionType.Call:
                    {
                        VisitMethodCall((MethodCallExpression)e);
                        break;
                    }
                case ExpressionType.Lambda:
                    {
                        VisitLambda((LambdaExpression)e);
                        break;
                    }
                case ExpressionType.New:
                    {
                        VisitNew((NewExpression)e);
                        break;
                    }
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    {
                        VisitNewArray((NewArrayExpression)e);
                        break;
                    }
                case ExpressionType.Invoke:
                    {
                        VisitInvocation((InvocationExpression)e);
                        break;
                    }
                case ExpressionType.MemberInit:
                    {
                        VisitMemberInit((MemberInitExpression)e);
                        break;
                    }
                case ExpressionType.ListInit:
                    {
                        VisitListInit((ListInitExpression)e);
                        break;
                    }
                default:
                    {
                        throw new NotSupportedException(string.Format("Unknown expression type: '{0}'", e.NodeType));
                    }
            }
        }

        protected virtual void VisitBinding(MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    VisitMemberAssignment((MemberAssignment)binding);
                    break;
                case MemberBindingType.MemberBinding:
                    VisitMemberMemberBinding((MemberMemberBinding)binding);
                    break;
                case MemberBindingType.ListBinding:
                    VisitMemberListBinding((MemberListBinding)binding);
                    break;
                default:
                    throw new NotSupportedException(string.Format("Unknown binding type '{0}'", binding.BindingType));
            }
        }

        protected virtual void VisitElementInitializer(ElementInit initializer)
        {
            this.VisitExpressionList(initializer.Arguments);
        }

        protected virtual void VisitUnary(UnaryExpression u)
        {
            Visit(u.Operand);
        }

        protected virtual void VisitBinary(BinaryExpression b)
        {
            Visit(b.Left);
            Visit(b.Right);
            Visit(b.Conversion);
        }

        protected virtual void VisitTypeIs(TypeBinaryExpression b)
        {
            Visit(b.Expression);
        }

        protected virtual void VisitConstant(ConstantExpression c)
        {
        }

        protected virtual void VisitConditional(ConditionalExpression c)
        {
            Visit(c.Test);
            Visit(c.IfTrue);
            Visit(c.IfFalse);
        }

        protected virtual void VisitParameter(ParameterExpression p)
        {
        }

        protected virtual void VisitMemberAccess(MemberExpression m)
        {
            Visit(m.Expression);
        }

        protected virtual void VisitMethodCall(MethodCallExpression m)
        {
            this.Visit(m.Object);
            VisitExpressionList(m.Arguments);
        }

        protected virtual void VisitExpressionList(ReadOnlyCollection<Expression> list)
        {
            list.ForEach<Expression>(e => Visit(e));
        }

        protected virtual void VisitMemberAssignment(MemberAssignment assignment)
        {
            Visit(assignment.Expression);
        }

        protected virtual void VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            VisitBindingList(binding.Bindings);
        }

        protected virtual void VisitMemberListBinding(MemberListBinding binding)
        {
            VisitElementInitializerList(binding.Initializers);
        }

        protected virtual void VisitBindingList(ReadOnlyCollection<MemberBinding> list)
        {
            list.ForEach<MemberBinding>(e => VisitBinding(e));
        }

        protected virtual void VisitElementInitializerList(ReadOnlyCollection<ElementInit> list)
        {
            list.ForEach<ElementInit>(e => VisitElementInitializer(e));
        }

        protected virtual void VisitLambda(LambdaExpression lambda)
        {
            Visit(lambda.Body);
        }

        protected virtual void VisitNew(NewExpression newExpression)
        {
            VisitExpressionList(newExpression.Arguments);
        }

        protected virtual void VisitMemberInit(MemberInitExpression init)
        {
            VisitNew(init.NewExpression);
            VisitBindingList(init.Bindings);
        }

        protected virtual void VisitListInit(ListInitExpression init)
        {
            VisitNew(init.NewExpression);
            VisitElementInitializerList(init.Initializers);
        }

        protected virtual void VisitNewArray(NewArrayExpression na)
        {
            VisitExpressionList(na.Expressions);
        }

        protected virtual void VisitInvocation(InvocationExpression iv)
        {
            VisitExpressionList(iv.Arguments);
            Visit(iv.Expression);
        }
    }
}
