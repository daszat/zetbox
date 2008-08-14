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
using System.Collections;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class HelperTest
    {
        TestDataObject obj;

        [SetUp]
        public void SetUp()
        {
            obj = new TestDataObject() { BoolProperty = true, IntProperty = 1, StringProperty = "test" };
        }

        [Test]
        public void IsFloatingObjectTest()
        {
            Assert.That(Helper.IsFloatingObject(obj), Is.EqualTo(true));
            obj.ID = 1;
            obj.AttachToContext(new TestKistlContext());
            Assert.That(Helper.IsFloatingObject(obj), Is.EqualTo(false));
        }

        [Test]
        public void IsPersistedObject()
        {
            Assert.That(Helper.IsPersistedObject(obj), Is.EqualTo(false));
            obj.ID = 1;
            obj.AttachToContext(new TestKistlContext());
            Assert.That(Helper.IsPersistedObject(obj), Is.EqualTo(true));
        }
    }
}
