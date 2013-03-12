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

namespace Zetbox.Server.Tests.SchemaTests.SchemaProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.Server.SchemaManagement;
    using Zetbox.Server.SchemaManagement.SqlProvider;

    public class CheckExpressionVisitorTests
    {
        private CheckExpressionVisitor _visitor;

        [SetUp]
        public void SetUp()
        {
            _visitor = new CheckExpressionVisitor();
        }

        private string WrapAndTranslate(Expression expression)
        {
            return _visitor.TranslateCheckExpression(Expression.Lambda<Func<string, bool>>(expression, Expression.Parameter(typeof(string), "s")));
        }

        [TestCase(true, "(0=0)")]
        [TestCase(false, "(0=1)")]
        public void Constant(object value, string expected)
        {
            Assert.That(WrapAndTranslate(Expression.Constant(value)), Is.EqualTo(expected));
        }

        [Test]
        public void ParameterAndString()
        {
            Assert.That(WrapAndTranslate(Expression.MakeBinary(ExpressionType.Equal, Expression.Parameter(typeof(string), "s"), Expression.Constant("b'ar"))), Is.EqualTo("{0}='b''ar'"), "param,string,quoting");
        }

        [TestCase(ExpressionType.NotEqual, 1, 2, "1<>2")]
        [TestCase(ExpressionType.And, true, true, "(0=0) AND (0=0)")]
        [TestCase(ExpressionType.Or, true, true, "(0=0) OR (0=0)")]
        public void Binary(ExpressionType comp, object first, object second, string expected)
        {
            Assert.That(WrapAndTranslate(Expression.MakeBinary(comp, Expression.Constant(first), Expression.Constant(second))), Is.EqualTo(expected));
        }

        [Test]
        public void Coalesce()
        {
            Assert.That(WrapAndTranslate(Expression.MakeBinary(ExpressionType.Coalesce, Expression.Constant(null, typeof(bool?)), Expression.Constant(true))), Is.EqualTo("COALESCE(NULL, (0=0))"));
        }

        [Test]
        public void IsNull()
        {
            Assert.That(WrapAndTranslate(Expression.MakeBinary(ExpressionType.Equal, Expression.Constant(null, typeof(bool?)), Expression.Constant(true, typeof(bool?)))), Is.EqualTo("(0=0) IS NULL"));
        }

        [Test]
        public void IsNotNull()
        {
            Assert.That(WrapAndTranslate(Expression.MakeBinary(ExpressionType.NotEqual, Expression.Constant(null, typeof(bool?)), Expression.Constant(true, typeof(bool?)))), Is.EqualTo("(0=0) IS NOT NULL"));
        }

        [TestCase(ExpressionType.GreaterThan, ExpressionType.Add, 1, 1, 2, "1+1>2")]
        [TestCase(ExpressionType.LessThan, ExpressionType.Subtract, 1, 1, 2, "1-1<2")]
        [TestCase(ExpressionType.LessThanOrEqual, ExpressionType.Multiply, 1, 1, 2, "1*1<=2")]
        [TestCase(ExpressionType.GreaterThanOrEqual, ExpressionType.Divide, 1, 1, 2, "1/1>=2")]
        [TestCase(ExpressionType.Equal, ExpressionType.Modulo, 1, 1, 2, "1%1=2")]
        public void Ternary(ExpressionType comp, ExpressionType op, int first, int second, int third, string expected)
        {
            Assert.That(WrapAndTranslate(Expression.MakeBinary(comp, Expression.MakeBinary(op, Expression.Constant(first), Expression.Constant(second)), Expression.Constant(third))), Is.EqualTo(expected));
        }

        [Test]
        public void Negate()
        {
            Assert.That(WrapAndTranslate(
                Expression.MakeBinary(ExpressionType.LessThan,
                    Expression.MakeUnary(ExpressionType.Negate, Expression.Constant(1), typeof(int)),
                    Expression.Constant(2))
                    ),
                Is.EqualTo("-1<2"), "negate,lt");
        }

        [Test]
        public void Not()
        {
            Assert.That(WrapAndTranslate(Expression.MakeUnary(ExpressionType.Not, Expression.Constant(true), typeof(bool))),
                Is.EqualTo("NOT (0=0)"), "not");
        }
    }
}
