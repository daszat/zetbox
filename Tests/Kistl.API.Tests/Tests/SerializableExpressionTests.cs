using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.IO;
using System.Linq.Expressions;
using System.Collections.ObjectModel;


namespace Kistl.API.Tests
{
    [TestFixture]
    public class SerializableExpressionTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromExpression_Null()
        {
            SerializableExpression.FromExpression(null);
        }

        [Test]
        public void SerializableType_Valid()
        {
            SerializableType t = new SerializableType(typeof(TestDataObject));
            Type result = t.GetSerializedType();
            Assert.That(result, Is.EqualTo(typeof(TestDataObject)));
        }
    }
}
