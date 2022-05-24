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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zetbox.API
{
    [Serializable]
    public class ExpressionTreeTranslator
    {
        public virtual async Task<Expression> Visit(Expression e)
        {
            if (e == null) return null;

            switch (e.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Decrement:
                case ExpressionType.Increment:
                case ExpressionType.OnesComplement:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                case ExpressionType.UnaryPlus:
                case ExpressionType.Unbox:
                    {
                        return await VisitUnary((UnaryExpression)e);
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
                        return await VisitBinary((BinaryExpression)e);
                    }
                case ExpressionType.TypeIs:
                    {
                        return await VisitTypeIs((TypeBinaryExpression)e);
                    }
                case ExpressionType.Conditional:
                    {
                        return await VisitConditional((ConditionalExpression)e);
                    }
                case ExpressionType.Constant:
                    {
                        return await VisitConstant((ConstantExpression)e);
                    }
                case ExpressionType.Parameter:
                    {
                        return await VisitParameter((ParameterExpression)e);
                    }
                case ExpressionType.MemberAccess:
                    {
                        return await VisitMemberAccess((MemberExpression)e);
                    }
                case ExpressionType.Call:
                    {
                        return await VisitMethodCall((MethodCallExpression)e);
                    }
                case ExpressionType.Lambda:
                    {
                        return await VisitLambda((LambdaExpression)e);
                    }
                case ExpressionType.New:
                    {
                        return await VisitNew((NewExpression)e);
                    }
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    {
                        return await VisitNewArray((NewArrayExpression)e);
                    }
                case ExpressionType.Invoke:
                    {
                        return await VisitInvocation((InvocationExpression)e);
                    }
                case ExpressionType.MemberInit:
                    {
                        return await VisitMemberInit((MemberInitExpression)e);
                    }
                case ExpressionType.ListInit:
                    {
                        return await VisitListInit((ListInitExpression)e);
                    }
                default:
                    {
                        throw new NotSupportedException(string.Format("Unknown expression type: '{0}'", e.NodeType));
                    }
            }
        }

        protected virtual async Task<MemberBinding> VisitBinding(MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    return await VisitMemberAssignment((MemberAssignment)binding);
                case MemberBindingType.MemberBinding:
                    return await VisitMemberMemberBinding((MemberMemberBinding)binding);
                case MemberBindingType.ListBinding:
                    return await VisitMemberListBinding((MemberListBinding)binding);
                default:
                    throw new NotSupportedException(string.Format("Unknown binding type '{0}'", binding.BindingType));
            }
        }

        protected virtual async Task<ElementInit> VisitElementInitializer(ElementInit initializer)
        {
            return Expression.ElementInit(initializer.AddMethod,
               await this.VisitExpressionList(initializer.Arguments));
        }

        protected virtual async Task<Expression> VisitUnary(UnaryExpression u)
        {
            return Expression.MakeUnary(u.NodeType, await Visit(u.Operand), u.Type, u.Method);
        }

        protected virtual async Task<Expression> VisitBinary(BinaryExpression b)
        {
            Expression left = await Visit(b.Left);
            Expression right = await Visit(b.Right);
            Expression conv = await Visit(b.Conversion);

            if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
                return Expression.Coalesce(left, right, conv as LambdaExpression);
            else
                return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
        }

        protected virtual async Task<Expression> VisitTypeIs(TypeBinaryExpression b)
        {
            return Expression.TypeIs(await Visit(b.Expression), b.TypeOperand);
        }

        protected virtual Task<Expression> VisitConstant(ConstantExpression c)
        {
            return Task.FromResult((Expression)c);
        }

        protected virtual async Task<Expression> VisitConditional(ConditionalExpression c)
        {
            return Expression.Condition(await Visit(c.Test), await Visit(c.IfTrue), await Visit(c.IfFalse));
        }

        protected virtual Task<ParameterExpression> VisitParameter(ParameterExpression p)
        {
            return Task.FromResult(p);
        }

        protected virtual async Task<Expression> VisitMemberAccess(MemberExpression m)
        {
            return Expression.MakeMemberAccess(await Visit(m.Expression), m.Member);
        }

        protected virtual async Task<Expression> VisitMethodCall(MethodCallExpression m)
        {
            return Expression.Call(await Visit(m.Object), m.Method, await VisitExpressionList(m.Arguments));
        }

        protected virtual async Task<ReadOnlyCollection<Expression>> VisitExpressionList(ReadOnlyCollection<Expression> list)
        {
            List<Expression> result = new List<Expression>();
            foreach (var e in list)
            {
                result.Add(await Visit(e));
            }
            return result.AsReadOnly();
        }

        protected virtual async Task<MemberAssignment> VisitMemberAssignment(MemberAssignment assignment)
        {
            return Expression.Bind(assignment.Member, await Visit(assignment.Expression));
        }

        protected virtual async Task<MemberMemberBinding> VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            return Expression.MemberBind(binding.Member, await VisitBindingList(binding.Bindings));
        }

        protected virtual async Task<MemberListBinding> VisitMemberListBinding(MemberListBinding binding)
        {
            return Expression.ListBind(binding.Member, await VisitElementInitializerList(binding.Initializers));
        }

        protected virtual async Task<ReadOnlyCollection<MemberBinding>> VisitBindingList(ReadOnlyCollection<MemberBinding> list)
        {
            List<MemberBinding> result = new List<MemberBinding>();
            foreach(var e in list)
            {
                result.Add(await VisitBinding(e));
            }
            return result.AsReadOnly();
        }

        protected virtual async Task<ReadOnlyCollection<ElementInit>> VisitElementInitializerList(ReadOnlyCollection<ElementInit> list)
        {
            List<ElementInit> result = new List<ElementInit>();
            foreach (var e in list)
            {
                result.Add(await VisitElementInitializer(e));
            }
            return result.AsReadOnly();
        }

        protected virtual async Task<ReadOnlyCollection<ParameterExpression>> VisitParameterList(ReadOnlyCollection<ParameterExpression> list)
        {
            List<ParameterExpression> result = new List<ParameterExpression>();
            foreach (var e in list)
            {
                result.Add(await VisitParameter(e));
            }
            return result.AsReadOnly();
        }

        protected virtual async Task<Expression> VisitLambda(LambdaExpression lambda)
        {
            return Expression.Lambda(lambda.Type, await Visit(lambda.Body), await VisitParameterList(lambda.Parameters));
        }

        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "correct grammar for this method name")]
        protected virtual async Task<NewExpression> VisitNew(NewExpression newExpression)
        {
            var args = await VisitExpressionList(newExpression.Arguments);
            if (newExpression.Members != null)
                return Expression.New(newExpression.Constructor, args, newExpression.Members);
            else
                return Expression.New(newExpression.Constructor, args);
        }

        protected virtual async Task<MemberInitExpression> VisitMemberInit(MemberInitExpression init)
        {
            return Expression.MemberInit(await VisitNew(init.NewExpression), await VisitBindingList(init.Bindings));
        }

        protected virtual async Task<ListInitExpression> VisitListInit(ListInitExpression init)
        {
            return Expression.ListInit(await VisitNew(init.NewExpression), await VisitElementInitializerList(init.Initializers));
        }

        protected virtual async Task<NewArrayExpression> VisitNewArray(NewArrayExpression na)
        {
            var e = await VisitExpressionList(na.Expressions);
            Type type = na.Type.FindElementTypes().Single(t => t != typeof(object));
            if (na.NodeType == ExpressionType.NewArrayInit)
            {
                return Expression.NewArrayInit(type, e);
            }
            else
            {
                return Expression.NewArrayBounds(type, e);
            }
        }

        protected virtual async Task<Expression> VisitInvocation(InvocationExpression iv)
        {
            return Expression.Invoke(await Visit(iv.Expression), await VisitExpressionList(iv.Arguments));
        }
    }
}
