using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests.Serializables
{

    [TestFixture]
    public class ConstantExpressions
    {

        public interface ISomething { }
        public class Something : ISomething { }

        [Test]
        public void roundtrip_Constant_string()
        {
            ConstantExpression expr = Expression.Constant("a");
            var result = (ConstantExpression)SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr));

            AssertExpressions.AreEqual(result, expr);
        }

        [Test]
        public void roundtrip_Constant_object()
        {
            ISomething value = new Something();
            ConstantExpression expr = Expression.Constant(value);
            var result = (ConstantExpression)SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr));

            AssertExpressions.AreEqual(result, expr);
        }

        [Test]
        public void roundtrip_Constant_interface()
        {
            ISomething value = new Something();
            ConstantExpression expr = Expression.Constant(value, typeof(ISomething));
            var result = (ConstantExpression)SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr));

            AssertExpressions.AreEqual(result, expr);
            Assert.That(result.Type, Is.EqualTo(typeof(ISomething)));
        }
    }
}
