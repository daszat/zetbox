using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using Autofac;

namespace Kistl.API.Tests.Serializables
{
    [TestFixture]
    public class SerializableTypeTests : AbstractApiTestFixture
    {
        SerializableType t;

        public override void SetUp()
        {
            base.SetUp();
            t = typeTrans.AsInterfaceType(typeof(TestDataObject)).ToSerializableType();
        }

        [Test]
        public void GetHashCode_returns_right_value()
        {
            // TODO: better GetHashCode() testing
            Assert.That(t.GetHashCode(), Is.EqualTo(t.TypeName.GetHashCode() ^ t.AssemblyQualifiedName.GetHashCode()));
        }

        [Test]
        public void GetSystemType_returns_right_type()
        {
            Type result = t.GetSystemType();
            Assert.That(result, Is.EqualTo(typeof(TestDataObject)));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSystemType_fails_on_invalid_AssemblyQualifiedName()
        {
            t.AssemblyQualifiedName = "Test";
            Type result = t.GetSystemType();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetSystemType_fails_on_invalid_TypeName()
        {
            t.TypeName = "Invalid Test Class Name";
            Type result = t.GetSystemType();
        }

    }
}
