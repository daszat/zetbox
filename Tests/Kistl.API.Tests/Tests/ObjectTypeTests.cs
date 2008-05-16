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
    public class ObjectTypeTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            ObjectType.Init("Kistl.API.Tests");
        }

        [Test]
        public void InitString()
        {
            ObjectType.Init("Test");
            Assert.That(ObjectType.AssemblyName, Is.EqualTo("Test"));
        }

        [Test]
        public void InitAssembly()
        {
            ObjectType.Init(this.GetType().Assembly);
            Assert.That(ObjectType.AssemblyName, Is.EqualTo(this.GetType().Assembly.FullName));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InitFail()
        {
            ObjectType.Init("");
        }

        [Test]
        public void Constructor_Empty()
        {
            ObjectType t = new ObjectType();
            Assert.That(t.NameDataObject, Is.Empty);
            Assert.That(t.FullNameDataObject, Is.Empty);
        }

        private void AssertExpectedType(ObjectType t)
        {
            Assert.That(t.NameDataObject, Is.EqualTo("Kistl.API.Tests.TestDataObject"));
            Assert.That(t.FullNameDataObject, Is.EqualTo("Kistl.API.Tests.TestDataObject, Kistl.API.Tests"));
        }

        [Test]
        public void Constructor_IDataObject()
        {
            ObjectType t = new ObjectType(new TestDataObject());
        }

        [Test]
        public void Constructor_Type()
        {
            ObjectType t = new ObjectType(typeof(TestDataObject));
            AssertExpectedType(t);
        }

        [Test]
        public void Constructor_String()
        {
            ObjectType t = new ObjectType("Kistl.API.Tests.TestDataObject");
            AssertExpectedType(t);
        }

        [Test]
        public void Constructor_Namespace_Classname()
        {
            ObjectType t = new ObjectType("Kistl.API.Tests", "TestDataObject");
            AssertExpectedType(t);
        }

        [Test]
        public void NewDataObject()
        {
            ObjectType t = new ObjectType("Kistl.API.Tests", "TestDataObject");
            IDataObject result = t.NewDataObject();
            Assert.That(result.GetType(), Is.EqualTo(typeof(TestDataObject)));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NewDataObject_Empty()
        {
            ObjectType t = new ObjectType();
            IDataObject result = t.NewDataObject();
        }

        [Test]
        [ExpectedException(typeof(TypeLoadException))]
        public void NewDataObject_TypeNotFound()
        {
            ObjectType t = new ObjectType("NotANamespace.Test.NotAType");
            IDataObject result = t.NewDataObject();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NewDataObject_TypeNotIDataObject()
        {
            ObjectType t = new ObjectType("Kistl.API.Tests", "TestCollectionEntry");
            IDataObject result = t.NewDataObject();
        }

        [Test]
        public void HashCode()
        {
            ObjectType t = new ObjectType(typeof(TestDataObject));
            int a = t.GetHashCode();
            int b = t.GetHashCode();
            Assert.That(a, Is.EqualTo(b));
        }
    }
}
