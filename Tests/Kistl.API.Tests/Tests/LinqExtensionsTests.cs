using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.Linq.Expressions;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class LinqExtensionsTests
    {
        List<TestDataObject> ctx;
        IQueryable<TestDataObject> list;

        [SetUp]
        public void SetUp()
        {
            ctx = new TestQuery<TestDataObject>().ToList();
            list = (from o in ctx
                   select o).AsQueryable<TestDataObject>();
        }

        [Test]
        public void AddEqualityCondition()
        {
            list = list.AddEqualityCondition("BoolProperty", true);
            Assert.That(list.ToList().Count, Is.EqualTo(5));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddEqualityCondition_InvalidProperty()
        {
            list = list.AddEqualityCondition("InvalidProperty", true);
            Assert.That(list.ToList().Count, Is.EqualTo(5));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEqualityCondition_InvalidType()
        {
            list = list.AddEqualityCondition("BoolProperty", "Hello");
            Assert.That(list.ToList().Count, Is.EqualTo(5));
        }

        [Test]
        public void AddFilter()
        {
            Expression<Func<TestDataObject, bool>> e = (o) => o.BoolProperty == true;
            list = list.AddFilter(e);
            Assert.That(list.ToList().Count, Is.EqualTo(5));
        }

        [Test]
        [ExpectedException(typeof(System.Security.SecurityException))]
        public void AddFilter_IllegalExpression()
        {
            Expression<Func<TestDataObject, bool>> e = (o) => o.GetType().ToString() == "";
            list = list.AddFilter(e);
        }

        [Test]
        public void AddOrderBy()
        {
            Expression<Func<TestDataObject, string>> e = (o) => o.StringProperty;
            list = list.AddOrderBy(e);
            Assert.That(list.ToList().Count, Is.EqualTo(9));
            Assert.That(list.ToList()[0].ID, Is.EqualTo(8)); // Eighth
        }

        [Test]
        public void AddOrderBy_WithQuote()
        {
            Expression<Func<TestDataObject, string>> e = (o) => o.StringProperty;
            list = list.AddOrderBy(Expression.Quote(e));
            Assert.That(list.ToList().Count, Is.EqualTo(9));
            Assert.That(list.ToList()[0].ID, Is.EqualTo(8)); // Eighth
        }

        [Test]
        public void GetExpressionValue_Constant()
        {
            Expression e = Expression.Constant("Hello World");
            Assert.That(e.GetExpressionValue<string>(), Is.EqualTo("Hello World"));
        }

        [Test]
        public void GetExpressionValue_MemberExpression_Property()
        {
            TestDataObject obj = new TestDataObject() { StringProperty = "Hello World" };
            Expression e = Expression.PropertyOrField(Expression.Constant(obj), "StringProperty");
            Assert.That(e.GetExpressionValue<string>(), Is.EqualTo("Hello World"));
        }

        [Test]
        public void GetExpressionValue_MemberExpression_Field()
        {
            TestDataObject obj = new TestDataObject() { TestField = "Hello World" };
            Expression e = Expression.PropertyOrField(Expression.Constant(obj), "TestField");
            Assert.That(e.GetExpressionValue<string>(), Is.EqualTo("Hello World"));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetExpressionValue_LambdaExpression()
        {
            TestDataObject obj = new TestDataObject() { TestField = "Hello World" };
            Expression<Func<TestDataObject, string>> e = (o) => o.StringProperty;
            Assert.That(e.GetExpressionValue<string>(), Is.EqualTo("Hello World"));
        }
    }
}
