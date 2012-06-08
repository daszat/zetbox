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
    using System.Text;

    using Zetbox.API;

    internal static class ConstantEvaluator
    {
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
                if (e.NodeType == ExpressionType.Constant)
                {
                    return e;
                }
                LambdaExpression lambda = Expression.Lambda(e);
                // TODO: The following line is _the_only_ (85% of ZetboxContext.Find()) performance Hotspot for Linq2Zetbox
                Delegate fn = lambda.Compile();
                return Expression.Constant(fn.DynamicInvoke(null), e.Type);
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
