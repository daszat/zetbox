using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Tests
{
	[TestFixture]
    public class LinqExtensionsTests : AbstractApiTestFixture
	{
		List<TestDataObject> ctx;
		IQueryable<TestDataObject> list;

		public override void SetUp()
		{
            base.SetUp();
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
		public void AddEqualityCondition_InvalidProperty()
		{
			Assert.That(() => list.AddEqualityCondition("InvalidProperty", true), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void AddEqualityCondition_InvalidType()
		{
			Assert.That(() => list.AddEqualityCondition("BoolProperty", "Hello"), Throws.InstanceOf<InvalidOperationException>());
		}

		[Test]
		public void AddFilter()
		{
			Expression<Func<TestDataObject, bool>> e = (o) => o.BoolProperty == true;
			list = list.AddFilter(e);
			Assert.That(list.ToList().Count, Is.EqualTo(5));
		}

		[Test]
        [Ignore("Illegal Expression checking disabled for now")]
        public void AddFilter_IllegalExpression()
		{
			Expression<Func<TestDataObject, bool>> e = (o) => o.GetType().ToString() == "";
			Assert.That(() => list.AddFilter(e), Throws.InstanceOf<System.Security.SecurityException>());
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
		public void AddSelector()
		{
			Expression<Func<TestDataObject, bool>> e = (o) => o.BoolProperty;
			var slist = (IEnumerable<bool>)list.AddSelector(e, typeof(TestDataObject), typeof(bool));
			Assert.That(slist.Count(), Is.EqualTo(9));
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
			TestDataObject obj = new TestDataObjectImpl() { StringProperty = "Hello World" };
			Expression e = Expression.PropertyOrField(Expression.Constant(obj), "StringProperty");
			Assert.That(e.GetExpressionValue<string>(), Is.EqualTo("Hello World"));
		}

		[Test]
		public void GetExpressionValue_MemberExpression_Field()
		{
			TestDataObject obj = new TestDataObjectImpl() { TestField = "Hello World" };
			Expression e = Expression.PropertyOrField(Expression.Constant(obj), "TestField");
			Assert.That(e.GetExpressionValue<string>(), Is.EqualTo("Hello World"));
		}

		[Test]
		public void GetExpressionValue_LambdaExpression()
		{
			Expression<Func<TestDataObject, string>> e = (o => o.StringProperty);
			Assert.That(() => e.GetExpressionValue<string>(), Throws.InstanceOf<NotSupportedException>());
		}
	}
}
