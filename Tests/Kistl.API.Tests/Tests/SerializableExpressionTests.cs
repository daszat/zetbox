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
            SerializableExpression.FromExpression(null, SerializableType.SerializeDirection.ClientToServer);
        }

        [Test]
        public void SerializableType_Valid()
        {
            SerializableType t = new SerializableType(typeof(TestDataObject), SerializableType.SerializeDirection.ClientToServer);
            Type result = t.GetSerializedType();
            Assert.That(result, Is.EqualTo(typeof(TestDataObject)));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SerializableType_InvalidType()
        {
            SerializableType t = new SerializableType(typeof(TestDataObject), SerializableType.SerializeDirection.ClientToServer);
            t.AssemblyQualifiedName = "TestType";
            Type result = t.GetSerializedType();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SerializableType_InvalidType_Generic()
        {
            SerializableType t = new SerializableType(typeof(List<int>), SerializableType.SerializeDirection.ClientToServer);
            t.AssemblyQualifiedName = "TestType";
            Type result = t.GetSerializedType();
        }
    }
}
