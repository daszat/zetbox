using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Zetbox.API
{
    /// <summary>
    /// Baseclass for a Expressiontree Visitor. This Visitor does _not_ translate the Expression Tree.
    /// </summary>
    [Serializable]
    public abstract class ExpressionTreeVisitor
    {
        /// <summary>
        /// Visits the Expression Tree
        /// </summary>
        /// <param name="e">Linq Expression</param>
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

        /// <summary>
        /// Visits a Binding
        /// </summary>
        /// <param name="binding">MemberBinding Expression</param>
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

        /// <summary>
        /// Visits a Element Initializer
        /// </summary>
        /// <param name="initializer">ElementInit Expression</param>
        protected virtual void VisitElementInitializer(ElementInit initializer)
        {
            this.VisitExpressionList(initializer.Arguments);
        }

        /// <summary>
        /// Visits a Unary Expression
        /// </summary>
        /// <param name="u">Unary Expression</param>
        protected virtual void VisitUnary(UnaryExpression u)
        {
            Visit(u.Operand);
        }

        /// <summary>
        /// Visits a Binary Expression
        /// </summary>
        /// <param name="b">Binary Expression</param>
        protected virtual void VisitBinary(BinaryExpression b)
        {
            Visit(b.Left);
            Visit(b.Right);
            Visit(b.Conversion);
        }

        /// <summary>
        /// Visits a TypeIs Expression
        /// </summary>
        /// <param name="b">TypeBinary Expression</param>
        protected virtual void VisitTypeIs(TypeBinaryExpression b)
        {
            Visit(b.Expression);
        }

        /// <summary>
        /// Visits a Constant Expression
        /// </summary>
        /// <param name="c">Constant Expression</param>
        protected virtual void VisitConstant(ConstantExpression c)
        {
        }

        /// <summary>
        /// Visits a Conditional Expression
        /// </summary>
        /// <param name="c">Conditional Expression</param>
        protected virtual void VisitConditional(ConditionalExpression c)
        {
            Visit(c.Test);
            Visit(c.IfTrue);
            Visit(c.IfFalse);
        }

        /// <summary>
        /// Visits a Parameter Expression
        /// </summary>
        /// <param name="p">Parameter Expression</param>
        protected virtual void VisitParameter(ParameterExpression p)
        {
        }

        /// <summary>
        /// Visits a Member Access Expression
        /// </summary>
        /// <param name="m">Member Expression</param>
        protected virtual void VisitMemberAccess(MemberExpression m)
        {
            Visit(m.Expression);
        }

        /// <summary>
        /// Visits a MethodCall Expression
        /// </summary>
        /// <param name="m">MethodCall Expression</param>
        protected virtual void VisitMethodCall(MethodCallExpression m)
        {
            this.Visit(m.Object);
            VisitExpressionList(m.Arguments);
        }

        /// <summary>
        /// Visits a Expression List
        /// </summary>
        /// <param name="list">Expression List</param>
        protected virtual void VisitExpressionList(ReadOnlyCollection<Expression> list)
        {
            list.ForEach<Expression>(e => Visit(e));
        }

        /// <summary>
        /// Visits a Member Assignment
        /// </summary>
        /// <param name="assignment">Member Assignment Expression</param>
        protected virtual void VisitMemberAssignment(MemberAssignment assignment)
        {
            Visit(assignment.Expression);
        }

        /// <summary>
        /// Visits a MemberMemberBinding 
        /// </summary>
        /// <param name="binding">MemberMemberBinding Expression</param>
        protected virtual void VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            VisitBindingList(binding.Bindings);
        }

        /// <summary>
        /// Visits a MemberListBinding
        /// </summary>
        /// <param name="binding">MemberListBinding Expression</param>
        protected virtual void VisitMemberListBinding(MemberListBinding binding)
        {
            VisitElementInitializerList(binding.Initializers);
        }

        /// <summary>
        /// Visits a BindingList
        /// </summary>
        /// <param name="list">MemberBinding List</param>
        protected virtual void VisitBindingList(ReadOnlyCollection<MemberBinding> list)
        {
            list.ForEach<MemberBinding>(e => VisitBinding(e));
        }

        /// <summary>
        /// Visits a ElementInitializer List
        /// </summary>
        /// <param name="list">ElementInit List</param>
        protected virtual void VisitElementInitializerList(ReadOnlyCollection<ElementInit> list)
        {
            list.ForEach<ElementInit>(e => VisitElementInitializer(e));
        }

        /// <summary>
        /// Visits a Lambda Expression
        /// </summary>
        /// <param name="lambda">Lambda Expression</param>
        protected virtual void VisitLambda(LambdaExpression lambda)
        {
            Visit(lambda.Body);
        }

        /// <summary>
        /// Visits a New Expression
        /// </summary>
        /// <param name="newExpression">New Expression</param>
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "correct grammar for this method name")]
        protected virtual void VisitNew(NewExpression newExpression)
        {
            VisitExpressionList(newExpression.Arguments);
        }

        /// <summary>
        /// Visits a MemberInit Expression
        /// </summary>
        /// <param name="init">MemberInit Expression</param>
        protected virtual void VisitMemberInit(MemberInitExpression init)
        {
            VisitNew(init.NewExpression);
            VisitBindingList(init.Bindings);
        }

        /// <summary>
        /// Visits a List Init Expression
        /// </summary>
        /// <param name="init">List Init Expression</param>
        protected virtual void VisitListInit(ListInitExpression init)
        {
            VisitNew(init.NewExpression);
            VisitElementInitializerList(init.Initializers);
        }

        /// <summary>
        /// Visits a new Array Expression
        /// </summary>
        /// <param name="na">new Array Expression</param>
        protected virtual void VisitNewArray(NewArrayExpression na)
        {
            VisitExpressionList(na.Expressions);
        }

        /// <summary>
        /// Visits a Invocation Expression
        /// </summary>
        /// <param name="iv">Invocation Expression</param>
        protected virtual void VisitInvocation(InvocationExpression iv)
        {
            VisitExpressionList(iv.Arguments);
            Visit(iv.Expression);
        }
    }
}
