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

namespace Zetbox.DalProvider.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using NUnit.Framework;

    public struct CETData
    {
        public Expression Expression;
        public object ExpectedValue;
        public string Description;

        public override string ToString()
        {
            return string.Format("{0}: {1} = {2}", Description, ExpectedValue, Expression.ToString());
        }
    }

    class TestClass
    {
        public string field;
        public string property { get; set; }
    }

    enum TestEnum
    {
        Foo = 3
    }

    public class ConstantEvaluatorTests
    {
        public static IEnumerable<object> GetTestCases()
        {
            yield return new CETData() { Description = "box null to int?", Expression = Expression.Convert(Expression.Constant(null), typeof(int?)), ExpectedValue = null };
            yield return new CETData() { Description = "box enum to int?", Expression = Expression.Convert(Expression.Constant(TestEnum.Foo), typeof(int?)), ExpectedValue = 3 };
            yield return new CETData() { Description = "box null to enum? to int?", Expression = Expression.Convert(Expression.Convert(Expression.Constant(null), typeof(TestEnum?)), typeof(int?)), ExpectedValue = null };
            yield return new CETData() { Description = "convert enum? null to int?", Expression = Expression.Convert(Expression.Constant((TestEnum?)null), typeof(int?)), ExpectedValue = null };

            yield return new CETData() { Description = "add int", Expression = Expression.Add(Expression.Constant(1), Expression.Constant(2)), ExpectedValue = 3 };
            yield return new CETData() { Description = "add int to null", Expression = Expression.Add(Expression.Constant(1, typeof(int?)), Expression.Constant(null, typeof(int?))), ExpectedValue = null };
            yield return new CETData() { Description = "add int? to int?", Expression = Expression.Add(Expression.Constant(1, typeof(int?)), Expression.Constant(2, typeof(int?))), ExpectedValue = 3 };
            yield return new CETData() { Description = "bitwise and", Expression = Expression.And(Expression.Constant(1, typeof(int?)), Expression.Constant(3, typeof(int?))), ExpectedValue = 0x1 };
            yield return new CETData() { Description = "logical and", Expression = Expression.AndAlso(Expression.Constant(true), Expression.Constant(true)), ExpectedValue = true };
            yield return new CETData() { Description = "logical and", Expression = Expression.AndAlso(Expression.Constant(false), Expression.Constant(true)), ExpectedValue = false };
            yield return new CETData() { Description = "subtract int", Expression = Expression.Subtract(Expression.Constant(1), Expression.Constant(2)), ExpectedValue = -1 };
            yield return new CETData() { Description = "equality int==int", Expression = Expression.Equal(Expression.Constant(1), Expression.Constant(2)), ExpectedValue = false };
            yield return new CETData() { Description = "equality int!=int", Expression = Expression.NotEqual(Expression.Constant(1), Expression.Constant(2)), ExpectedValue = true };

            yield return new CETData() { Description = "equality int==int?", Expression = Expression.Equal(Expression.Convert(Expression.Constant(1), typeof(int?)), Expression.Convert(Expression.Constant(2), typeof(int?))), ExpectedValue = false };
            yield return new CETData() { Description = "equality int!=int?", Expression = Expression.NotEqual(Expression.Convert(Expression.Constant(1), typeof(int?)), Expression.Convert(Expression.Constant(2), typeof(int?))), ExpectedValue = true };

            yield return new CETData() { Description = "equality int==int?null", Expression = Expression.Equal(Expression.Convert(Expression.Constant(1), typeof(int?)), Expression.Convert(Expression.Constant(null), typeof(int?))), ExpectedValue = false };
            yield return new CETData() { Description = "equality int!=int?null", Expression = Expression.NotEqual(Expression.Convert(Expression.Constant(1), typeof(int?)), Expression.Convert(Expression.Constant(null), typeof(int?))), ExpectedValue = true };

            var fieldtest = new TestClass() { field = "foo" };
            yield return new CETData() { Description = "field access", Expression = ((Expression<Func<string>>)(() => fieldtest.field)).Body, ExpectedValue = "foo" };

            var propertytest = new TestClass() { property = "foo" };
            yield return new CETData() { Description = "property access", Expression = ((Expression<Func<string>>)(() => propertytest.property)).Body, ExpectedValue = "foo" };

            yield return new CETData() { Description = "decrement int", Expression = Expression.Decrement(Expression.Constant(1)), ExpectedValue = 0 };
            yield return new CETData() { Description = "increment int", Expression = Expression.Increment(Expression.Constant(1)), ExpectedValue = 2 };
            yield return new CETData() { Description = "one's complement int", Expression = Expression.OnesComplement(Expression.Constant(1)), ExpectedValue = ~1 };
            yield return new CETData() { Description = "one's complement int", Expression = Expression.OnesComplement(Expression.Constant(-1)), ExpectedValue = ~-1 };
        }

        [SetUp]
        public void SetUp()
        {
            ConstantEvaluator.FailOnLambdaCompile = true;
        }

        [TearDown]
        public void TearDown()
        {
            ConstantEvaluator.FailOnLambdaCompile = false;
        }

        [Test]
        [TestCaseSource("GetTestCases")]
        public void Test(CETData data)
        {
            Assert.That(((ConstantExpression)ConstantEvaluator.PartialEval(data.Expression)).Value, Is.EqualTo(data.ExpectedValue), "PartialEval failed");
            Assert.That(Expression.Lambda(data.Expression).Compile().DynamicInvoke(null), Is.EqualTo(data.ExpectedValue), "Compilation failed");
        }
    }
}
