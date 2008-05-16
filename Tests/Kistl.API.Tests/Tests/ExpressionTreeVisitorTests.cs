using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ExpressionTreeVisitorTests
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

            TestDataObject obj = new TestDataObject() { StringProperty = "test", TestField = "test2" };
            TestQuery<TestDataObject> ctx = new TestQuery<TestDataObject>();
            var list = from o in ctx
                       where o.IntProperty == 1
                           && o.IntProperty != 2
                           && o.IntProperty > 3
                           && o.IntProperty == obj.ID
                           && o.StringProperty == obj.TestField
                           && o.StringProperty == obj.StringProperty
                           && o.StringProperty.StartsWith(obj.TestField)
                           && (o.StringProperty.StartsWith("test") || o.StringProperty == "test")
                           && !o.BoolProperty
                           && (o.StringProperty is string)
                           && o.BoolProperty ? true : false
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
                           TestObj = new { o.ObjectState, o.ID },
                           Date = new DateTime(1000),
                           Test3 = obj.StringProperty,
                           Test4 = new TestDataObject() { StringProperty = o.StringProperty, ID = MethodCallTest(o.IntProperty) },
                           Test5 = new List<TestDataObject> {
                                new TestDataObject() { StringProperty = obj.StringProperty, ID = o.IntProperty }, 
                                new TestDataObject() { StringProperty = o.StringProperty, ID = MethodCallTest(o.IntProperty) } 
                           },
                           Test6 = MethodCallTest(2),
                           Test7 = new List<TestDataObject>() { new TestDataObject(), obj }.Max(x => x.ID),
                       };

            // The TestProvider does not implement Projections
            // But in that case we just whant to test the visitor
            var result = list.GetEnumerator();
            Assert.That(result, Is.Null);
        }
    }
}
