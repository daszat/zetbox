using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;


namespace Kistl.API.Tests.Serializables
{
    [TestFixture]
    public class SerializableTypeTests
    {
        [Test]
        public void GetHashCode_returns_right_value()
        {
            // TODO: better GetHashCode() testing
            SerializableType t = new SerializableType(typeof(TestDataObject));
            Assert.That(t.GetHashCode(), Is.EqualTo(t.TypeName.GetHashCode() ^ t.AssemblyQualifiedName.GetHashCode()));
        }

        [Test]
        public void GetSystemType_returns_right_type()
        {
            SerializableType t = new SerializableType(typeof(TestDataObject));
            Type result = t.GetSystemType();
            Assert.That(result, Is.EqualTo(typeof(TestDataObject)));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSystemType_fails_on_invalid_AssemblyQualifiedName()
        {
            SerializableType t = new SerializableType(typeof(TestDataObject));
            t.AssemblyQualifiedName = "Test";
            Type result = t.GetSystemType();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSystemType_fails_on_invalid_TypeName()
        {
            SerializableType t = new SerializableType(typeof(TestDataObject));
            t.TypeName = "Invalid Test Class Name";
            Type result = t.GetSystemType();
        }

    }
}
