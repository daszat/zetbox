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

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;

    internal static class ConstantEvaluator
    {
        /// <summary>
        /// Internal flag to force test failures when we fall back to compiling lambdas
        /// </summary>
        internal static bool FailOnLambdaCompile = false;

        public static Expression PartialEval(Expression expression)
        {
            Nominator nominator = new Nominator();
            SubtreeEvaluator evaluator = new SubtreeEvaluator(nominator.Nominate(expression));
            return evaluator.Eval(expression);
        }

        class SubtreeEvaluator : ExpressionTreeTranslator
        {
            HashSet<Expression> candidates;

            internal SubtreeEvaluator(HashSet<Expression> candidates)
            {
                this.candidates = candidates;
            }

            internal Expression Eval(Expression exp)
            {
                return this.Visit(exp);
            }

            public override Expression Visit(Expression exp)
            {
                if (exp == null)
                {
                    return null;
                }

                if (this.candidates.Contains(exp))
                {
                    return this.Evaluate(exp);
                }
                return base.Visit(exp);
            }

            private Expression Evaluate(Expression e)
            {
                return Expression.Constant(EvaluateToValue(e), e.Type);
            }

            private object EvaluateToValue(Expression e)
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Constant:
                        return ((ConstantExpression)e).Value;
                    case ExpressionType.IsFalse:
                        return false;
                    case ExpressionType.IsTrue:
                        return true;
                    case ExpressionType.Convert:
                    case ExpressionType.Decrement:
                    case ExpressionType.Increment:
                    case ExpressionType.Negate:
                    case ExpressionType.Not:
                    case ExpressionType.OnesComplement:
                    case ExpressionType.UnaryPlus:
                    case ExpressionType.Unbox:
                        return EvaluateUnary((UnaryExpression)e);
                    case ExpressionType.Add:
                    case ExpressionType.And:
                    case ExpressionType.AndAlso:
                    case ExpressionType.Divide:
                    case ExpressionType.Equal:
                    case ExpressionType.ExclusiveOr:
                    case ExpressionType.GreaterThan:
                    case ExpressionType.GreaterThanOrEqual:
                    case ExpressionType.LeftShift:
                    case ExpressionType.LessThan:
                    case ExpressionType.LessThanOrEqual:
                    case ExpressionType.Modulo:
                    case ExpressionType.Multiply:
                    case ExpressionType.NotEqual:
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                    case ExpressionType.Power:
                    case ExpressionType.RightShift:
                    case ExpressionType.Subtract:
                        return EvaluateBinary((BinaryExpression)e);
                    case ExpressionType.MemberAccess:
                        return EvaluateMemberAccess((MemberExpression)e);

                    // other ExpressionTypes that would be nice to have:
                    case ExpressionType.ArrayIndex:
                    case ExpressionType.ArrayLength:
                    case ExpressionType.Conditional:
                    case ExpressionType.Default:
                    case ExpressionType.Index:
                    case ExpressionType.ListInit:
                    case ExpressionType.New:
                    case ExpressionType.NewArrayBounds:
                    case ExpressionType.NewArrayInit:
                    case ExpressionType.Switch:
                    case ExpressionType.TypeAs:
                    case ExpressionType.TypeEqual:
                    case ExpressionType.TypeIs:
                    case ExpressionType.Call:
                    case ExpressionType.Invoke:
                    default:
                        return EvaluateLambda(Expression.Lambda(e));
                }
            }

            private object EvaluateUnary(UnaryExpression e)
            {
                if (e.Method != null)
                    return e.Method.Invoke(null, new[] { EvaluateToValue(e.Operand) });
                else
                {
                    switch (e.NodeType)
                    {
                        case ExpressionType.Convert:
                            var result = EvaluateToValue(e.Operand);
                            // sometimes we manually need to convert, as boxed value types don't convert voluntarily
                            var t = e.Type;
                            if (t == typeof(bool)) return (bool)result;
                            else if (t == typeof(bool?)) return (bool?)result;
                            else if (t == typeof(byte)) return (byte)result;
                            else if (t == typeof(byte?)) return (byte?)result;
                            else if (t == typeof(char)) return (char)result;
                            else if (t == typeof(char?)) return (char?)result;
                            else if (t == typeof(decimal)) return (decimal)result;
                            else if (t == typeof(decimal?)) return (decimal?)result;
                            else if (t == typeof(double)) return (double)result;
                            else if (t == typeof(double?)) return (double?)result;
                            // enum  t
                            else if (t == typeof(float)) return (float)result;
                            else if (t == typeof(float?)) return (float?)result;
                            else if (t == typeof(int)) return (int)result;
                            else if (t == typeof(int?)) return (int?)result;
                            else if (t == typeof(long)) return (long)result;
                            else if (t == typeof(long?)) return (long?)result;
                            else if (t == typeof(sbyte)) return (sbyte)result;
                            else if (t == typeof(sbyte?)) return (sbyte?)result;
                            else if (t == typeof(short)) return (short)result;
                            else if (t == typeof(short?)) return (short?)result;
                            // structt
                            else if (t == typeof(uint)) return (uint)result;
                            else if (t == typeof(uint?)) return (uint?)result;
                            else if (t == typeof(ulong)) return (ulong)result;
                            else if (t == typeof(ulong?)) return (ulong?)result;
                            else if (t == typeof(ushort)) return (ushort)result;
                            else if (t == typeof(ushort?)) return (ushort?)result;
                            else return result;
                        case ExpressionType.Decrement:
                            return ((dynamic)(EvaluateToValue(e.Operand))) - 1;
                        case ExpressionType.Increment:
                            return ((dynamic)(EvaluateToValue(e.Operand))) + 1;
                        case ExpressionType.Negate:
                            return -((dynamic)(EvaluateToValue(e.Operand)));
                        case ExpressionType.Not:
                            return !((bool)(EvaluateToValue(e.Operand)));
                        case ExpressionType.OnesComplement:
                            return ~((dynamic)(EvaluateToValue(e.Operand)));
                        case ExpressionType.UnaryPlus:
                            return +((dynamic)(EvaluateToValue(e.Operand)));
                        case ExpressionType.Unbox:
                            return EvaluateToValue(e.Operand);
                        default:
                            return EvaluateLambda(Expression.Lambda(e));
                    }
                }
            }

            private object EvaluateBinary(BinaryExpression e)
            {
                if (e.Conversion == null && e.Method != null)
                {
                    return e.Method.Invoke(null, new[] { EvaluateToValue(e.Left), EvaluateToValue(e.Right) });
                }
                else if (e.Conversion == null && e.NodeType == ExpressionType.AndAlso)
                {
                    // short cut operator has to cut short
                    var left = (bool)EvaluateToValue(e.Left);
                    return left ? EvaluateToValue(e.Right) : false;
                }
                else if (e.Conversion == null && e.NodeType == ExpressionType.Coalesce)
                {
                    // short cut operator has to cut short
                    var left = EvaluateToValue(e.Left);
                    return left == null ? EvaluateToValue(e.Right) : null;
                }
                else if (e.Conversion == null && e.NodeType == ExpressionType.OrElse)
                {
                    // short cut operator has to cut short
                    var left = (bool)EvaluateToValue(e.Left);
                    return left ? true : EvaluateToValue(e.Right);
                }
                else if (e.Conversion == null)
                {
                    // Benchmark results comparing various kinds of implementing this
                    // ============ Add ============
                    // Cast and add         0:30.011
                    // dynamic add          6:55.875
                    // add with lambda   7569:07.257

                    dynamic left = EvaluateToValue(e.Left);
                    dynamic right = EvaluateToValue(e.Right);

                    switch (e.NodeType)
                    {
                        case ExpressionType.Add:
                            return left + right;
                        case ExpressionType.And:
                            return left & right;
                        case ExpressionType.Divide:
                            return left / right;
                        case ExpressionType.Equal:
                            return left == right;
                        case ExpressionType.ExclusiveOr:
                            return left ^ right;
                        case ExpressionType.GreaterThan:
                            return left > right;
                        case ExpressionType.GreaterThanOrEqual:
                            return left >= right;
                        case ExpressionType.LeftShift:
                            return left << right;
                        case ExpressionType.LessThan:
                            return left < right;
                        case ExpressionType.LessThanOrEqual:
                            return left <= right;
                        case ExpressionType.Modulo:
                            return left % right;
                        case ExpressionType.Multiply:
                            return left * right;
                        case ExpressionType.NotEqual:
                            return left != right;
                        case ExpressionType.Or:
                            return left | right;
                        case ExpressionType.Power:
                            return Math.Pow(left, right);
                        case ExpressionType.RightShift:
                            return left >> right;
                        case ExpressionType.Subtract:
                            return left - right;
                    }
                }

                // fallthrough
                return EvaluateLambda(Expression.Lambda(e));
            }

            /// <summary>
            /// Fallback to generate the evaluator dynamically. Hitting this is a massive performance problem.
            /// </summary>
            private static object EvaluateLambda(LambdaExpression lambda)
            {
                if (FailOnLambdaCompile) throw new InvalidOperationException(string.Format("Tried to compile an expression while running tests: {0}: {1}\n{2}", lambda.Body.NodeType, lambda.Body.GetType().FullName, lambda.Body));

                Delegate fn = lambda.Compile();
                return fn.DynamicInvoke(null);
            }

            private object EvaluateMemberAccess(MemberExpression me)
            {
                var obj = me.Expression == null
                    ? null
                    : EvaluateToValue(me.Expression);
                var propertyInfo = me.Member as PropertyInfo;
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(obj, null);
                }

                var fieldInfo = me.Member as FieldInfo;
                if (fieldInfo != null)
                {
                    return fieldInfo.GetValue(obj);
                }

                throw new InvalidOperationException();
            }
        }

        class Nominator : ExpressionTreeVisitor
        {
            HashSet<Expression> candidates;
            bool cannotBeEvaluated;

            internal Nominator()
            {
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                this.candidates = new HashSet<Expression>();
                this.Visit(expression);
                return this.candidates;
            }

            public override void Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                    this.cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!this.cannotBeEvaluated)
                    {
                        if (CanBeEvaluatedLocally(expression))
                        {
                            this.candidates.Add(expression);
                        }
                        else
                        {
                            this.cannotBeEvaluated = true;
                        }
                    }
                    this.cannotBeEvaluated |= saveCannotBeEvaluated;
                }
            }

            private bool CanBeEvaluatedLocally(Expression expression)
            {
                if (expression.NodeType == ExpressionType.Parameter) return false;
                if (expression.Type.IsGenericType && expression.Type.GetGenericTypeDefinition() == typeof(ZetboxContextQuery<>)) return false;
                return true;
            }
        }
    }
}
