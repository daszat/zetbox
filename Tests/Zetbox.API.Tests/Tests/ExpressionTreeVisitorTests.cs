using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Zetbox.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Zetbox.API.Tests
{
    [Serializable]
    public class TestObj
    {
        public string TestField;
    }

    [TestFixture]
    public class ExpressionTreeVisitorTests : AbstractApiTestFixture
    {
        private int MethodCallTest(int a)
        {
            return a * 3;
        }

        [Test]
        public void Visit()
        {
            System.Linq.Expressions.Expression<Func<int, int, bool>> largeSumTestExpression = (num1, num2) => (num1 + num2) > 1000;
            Func<int, int, bool> largeSumTest = largeSumTestExpression.Compile();

            TestDataObject obj = new TestDataObjectImpl() { StringProperty = "test", TestField = "test2" };
            TestObj obj2 = new TestObj() { TestField = "Test2" };
            TestQuery<TestDataObject> ctx = new TestQuery<TestDataObject>();
            var list = from o in ctx
                       where o.IntProperty == 1
                           && o.IntProperty != 2
                           && o.IntProperty > 3
                           && o.IntProperty == obj.ID
                           && o.StringProperty == obj2.TestField
                           && o.StringProperty == obj.StringProperty
                           && o.StringProperty.StartsWith(obj2.TestField)
                           && (o.StringProperty.StartsWith("test") || o.StringProperty == "test")
                           && (o.StringProperty is string)
                           && !o.BoolProperty
                           && o.BoolProperty ? false : true
                           && o.StringProperty == new DateTime().ToShortDateString()
                           && new int[] { 1, obj.IntProperty }.Length == 2
                           && (obj.IntProperty + o.StringProperty.Length) == 4
                           && largeSumTest(o.IntProperty, o.IntProperty)
                       select new
                       {
                           o.IntProperty,
                           o.BoolProperty,
                           Test = o.IntProperty * 2,
                           Test2 = MethodCallTest(o.StringProperty.Length * 2),
                           TestList = new int[] { o.IntProperty, o.ID, o.StringProperty.Length },
                           TestObj = new { o.BoolProperty, o.ID },
                           Date = new DateTime(1000),
                           Test3 = obj.StringProperty,
                           Test4 = new TestDataObjectImpl() { StringProperty = o.StringProperty, ID = MethodCallTest(o.IntProperty) },
                           Test5 = new List<TestDataObject> {
                                new TestDataObjectImpl() { StringProperty = obj.StringProperty, ID = o.IntProperty }, 
                                new TestDataObjectImpl() { StringProperty = o.StringProperty, ID = MethodCallTest(o.IntProperty) } 
                           },
                           Test6 = MethodCallTest(2),
                           Test7 = new List<TestDataObject>() { new TestDataObjectImpl(), obj }.Max(x => x.ID),
                       };

            // The TestProvider does not implement Projections
            // But in that case we just whant to test the visitor
            var result = list.GetEnumerator();
            Assert.That(result, Is.Null);
        }
    }
}
