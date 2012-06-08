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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Zetbox.API.Utils
{
    /// <summary>
    /// implements IComparer%lt;Expression>
    /// </summary>
    /// 
    /// thread safe
    public class ExpressionComparer
        : Comparer<Expression>
    {
        #region IComparer<Expression> Members

        /// <summary>
        /// Compares two Linq Expressions. Expressions of different type are 
        /// ordered by the Type, expressions of the same type are ordered in 
        /// a type-specific way.
        /// 
        /// Probably mostly interesting for comparing for equality
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(Expression x, Expression y)
        {
            int result;
            var xType = x == null ? null : x.GetType();
            var yType = y == null ? null : y.GetType();

            if (xType == null && yType == null)
            {
                result = 0;
            }
            else if (xType == yType)
            {
                var comparingVisitor = new ComparingVisitor(y);
                result = comparingVisitor.Visit(x);
            }
            else
            {
                result = Comparer<Type>.Default.Compare(xType, yType);
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Compares Linq Expressions to a given Expression
        /// </summary>
        private class ComparingVisitor
            : ExpressionVisitor<int>
        {
            private Expression second;

            public ComparingVisitor(Expression second)
            {
                this.second = second;
            }

            /// <summary>
            /// Compares two Expressions if all values until now where equal.
            /// </summary>
            private static int CompareIfUndecided(int intermediateResult, Expression x, Expression y)
            {
                if (intermediateResult != 0)
                    return intermediateResult;

                return ExpressionComparer.Default.Compare(x, y);
            }

            /// <summary>
            /// Compares two ReadOnlyCollections of Expressions, if all values until now where equal.
            /// </summary>
            private static int CompareCollectionIfUndecided<T>(int intermediateResult, ReadOnlyCollection<T> x, ReadOnlyCollection<T> y)
                where T : Expression
            {
                if (intermediateResult != 0)
                    return intermediateResult;

                intermediateResult = CompareIfUndecidedDefault(intermediateResult, x.Count, y.Count);
                if (intermediateResult == 0)
                {
                    for (int i = 0; i < x.Count && intermediateResult == 0; i++)
                    {
                        intermediateResult = CompareIfUndecided(intermediateResult, x[i], y[i]);
                    }
                }
                return intermediateResult;
            }


            /// <summary>
            /// Helper function to compare two values with their default comparer, if all values until now where equal.
            /// </summary>
            private static int CompareIfUndecidedDefault<T>(int intermediateResult, T x, T y)
            {
                if (intermediateResult != 0)
                    return intermediateResult;

                return Comparer<T>.Default.Compare(x, y);
            }

            private int CompareBasicProperties(Expression x, Expression y)
            {
                int result = 0;
                result = CompareIfUndecidedDefault(result, x.NodeType, y.NodeType);
                result = CompareIfUndecidedDefault(result, x.Type, y.Type);
                return result;
            }

            protected override int VisitExpression(BinaryExpression expr)
            {
                var other = (BinaryExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecided(result, expr.Conversion, other.Conversion);
                result = CompareIfUndecidedDefault(result, expr.IsLifted, other.IsLifted);
                result = CompareIfUndecidedDefault(result, expr.IsLiftedToNull, other.IsLiftedToNull);
                result = CompareIfUndecided(result, expr.Left, other.Left);
                result = CompareIfUndecidedDefault(result, expr.Method, other.Method);
                result = CompareIfUndecided(result, expr.Right, other.Right);
                return result;
            }

            protected override int VisitExpression(ConditionalExpression expr)
            {
                var other = (ConditionalExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecided(result, expr.IfFalse, other.IfFalse);
                result = CompareIfUndecided(result, expr.IfTrue, other.IfTrue);
                result = CompareIfUndecided(result, expr.Test, other.Test);
                return result;
            }

            protected override int VisitExpression(ConstantExpression expr)
            {
                var other = (ConstantExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecidedDefault(result, (expr.Value ?? String.Empty).ToString(), (other.Value ?? String.Empty).ToString());
                return result;
            }

            protected override int VisitExpression(LambdaExpression expr)
            {
                var other = (LambdaExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecided(result, expr.Body, other.Body);
                result = CompareCollectionIfUndecided(result, expr.Parameters, other.Parameters);
                return result;
            }

            protected override int VisitExpression(MemberExpression expr)
            {
                var other = (MemberExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecided(result, expr.Expression, other.Expression);
                result = CompareIfUndecidedDefault(result, expr.Member, other.Member);
                return result;
            }

            protected override int VisitExpression(MethodCallExpression expr)
            {
                var other = (MethodCallExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareCollectionIfUndecided(result, expr.Arguments, other.Arguments);
                result = CompareIfUndecidedDefault(result, expr.Method, other.Method);
                result = CompareIfUndecided(result, expr.Object, other.Object);
                return result;
            }

            protected override int VisitExpression(NewExpression expr)
            {
                var other = (NewExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareCollectionIfUndecided(result, expr.Arguments, other.Arguments);
                result = CompareIfUndecidedDefault(result, expr.Constructor, other.Constructor);
                result = CompareIfUndecidedDefault(result, expr.Members, other.Members);
                return result;
            }

            protected override int VisitExpression(ParameterExpression expr)
            {
                var other = (ParameterExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecidedDefault(result, expr.Name, other.Name);
                return result;
            }

            protected override int VisitExpression(UnaryExpression expr)
            {
                var other = (UnaryExpression)second;
                int result = CompareBasicProperties(expr, other);
                result = CompareIfUndecidedDefault(result, expr.IsLifted, other.IsLifted);
                result = CompareIfUndecidedDefault(result, expr.IsLiftedToNull, other.IsLiftedToNull);
                result = CompareIfUndecidedDefault(result, expr.Method, other.Method);
                result = CompareIfUndecided(result, expr.Operand, other.Operand);
                return result;
            }
        }
    }
}
