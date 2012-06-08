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

using NUnit.Framework;

namespace Zetbox.API.Tests.Serializables
{
    /// <summary>
    /// Helper function to check Expressions
    /// </summary>
    public class AssertExpressions : AbstractApiTestFixture
    {

        /// <summary>
        /// Compares two Linq Expressions.
        /// </summary>
        public static void AreEqual(Expression result, Expression expected)
        {
            var xType = result == null ? null : result.GetType();
            var yType = expected == null ? null : expected.GetType();

            if (xType == null && yType == null)
            {
                return;
            }
            else if (xType == yType)
            {
                var assertingVisitor = new AssertingVisitor(expected);
                assertingVisitor.Visit(result);
            }
            else
            {
                Assert.That(xType, Is.EqualTo(yType));
            }
        }


        /// <summary>
        /// Compares Linq Expressions to a given Expression
        /// </summary>
        private class AssertingVisitor
            : ExpressionVisitor<int>
        {
            private Expression second;

            public AssertingVisitor(Expression second)
            {
                this.second = second;
            }

            private static void CollectionsAreEqual<T>(ReadOnlyCollection<T> x, ReadOnlyCollection<T> y)
                where T : Expression
            {
                Assert.That(x.Count, Is.EqualTo(y.Count));
                for (int i = 0; i < x.Count; i++)
                {
                    AssertExpressions.AreEqual(x[i], y[i]);
                }
            }


            private void CompareBasicProperties(Expression x, Expression y)
            {
                Assert.AreEqual(x.NodeType, y.NodeType);
                Assert.AreEqual(x.Type, y.Type);
            }

            protected override int VisitExpression(BinaryExpression expr)
            {
                var other = (BinaryExpression)second;
                CompareBasicProperties(expr, other);
                AssertExpressions.AreEqual(expr.Conversion, other.Conversion);
                Assert.AreEqual(expr.IsLifted, other.IsLifted);
                Assert.AreEqual(expr.IsLiftedToNull, other.IsLiftedToNull);
                AssertExpressions.AreEqual(expr.Left, other.Left);
                Assert.AreEqual(expr.Method, other.Method);
                AssertExpressions.AreEqual(expr.Right, other.Right);
                return 0;
            }

            protected override int VisitExpression(ConditionalExpression expr)
            {
                var other = (ConditionalExpression)second;
                CompareBasicProperties(expr, other);
                AssertExpressions.AreEqual(expr.IfFalse, other.IfFalse);
                AssertExpressions.AreEqual(expr.IfTrue, other.IfTrue);
                AssertExpressions.AreEqual(expr.Test, other.Test);
                return 0;
            }

            protected override int VisitExpression(ConstantExpression expr)
            {
                var other = (ConstantExpression)second;
                CompareBasicProperties(expr, other);
                Assert.AreEqual((expr.Value ?? "").ToString(), (other.Value ?? "").ToString());
                return 0;
            }

            protected override int VisitExpression(LambdaExpression expr)
            {
                var other = (LambdaExpression)second;
                CompareBasicProperties(expr, other);
                AssertExpressions.AreEqual(expr.Body, other.Body);
                CollectionsAreEqual(expr.Parameters, other.Parameters);
                return 0;
            }

            protected override int VisitExpression(MemberExpression expr)
            {
                var other = (MemberExpression)second;
                CompareBasicProperties(expr, other);
                AssertExpressions.AreEqual(expr.Expression, other.Expression);
                Assert.AreEqual(expr.Member, other.Member);
                return 0;
            }

            protected override int VisitExpression(MethodCallExpression expr)
            {
                var other = (MethodCallExpression)second;
                CompareBasicProperties(expr, other);
                CollectionsAreEqual(expr.Arguments, other.Arguments);
                Assert.AreEqual(expr.Method, other.Method);
                AssertExpressions.AreEqual(expr.Object, other.Object);
                return 0;
            }

            protected override int VisitExpression(NewExpression expr)
            {
                var other = (NewExpression)second;
                CompareBasicProperties(expr, other);
                CollectionsAreEqual(expr.Arguments, other.Arguments);
                Assert.AreEqual(expr.Constructor, other.Constructor);
                Assert.AreEqual(expr.Members, other.Members);
                return 0;
            }

            protected override int VisitExpression(ParameterExpression expr)
            {
                var other = (ParameterExpression)second;
                CompareBasicProperties(expr, other);
                Assert.AreEqual(expr.Name, other.Name);
                return 0;
            }

            protected override int VisitExpression(UnaryExpression expr)
            {
                var other = (UnaryExpression)second;
                CompareBasicProperties(expr, other);
                Assert.AreEqual(expr.IsLifted, other.IsLifted);
                Assert.AreEqual(expr.IsLiftedToNull, other.IsLiftedToNull);
                Assert.AreEqual(expr.Method, other.Method);
                AssertExpressions.AreEqual(expr.Operand, other.Operand);
                return 0;
            }
        }
    }
}
