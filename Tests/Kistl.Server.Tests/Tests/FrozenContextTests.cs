using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.Server.Tests
{
    [TestFixture]
    public class FrozenContextTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void IsReadonlyFlag()
        {
            Assert.That(FrozenContext.Single.IsReadonly, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyContextException))]
        public void IsReadonly_Create()
        {
            var obj = FrozenContext.Single.Create<ObjectClass>();
        }


        [Test]
        public void IsReadonlyObject()
        {
            var obj = FrozenContext.Single.GetQuery<ObjectClass>().First();
            Assert.That(obj.IsReadonly, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyObjectException))]
        public void IsReadonlyObject_String()
        {
            var obj = FrozenContext.Single.GetQuery<ObjectClass>().First();
            obj.ClassName = "test";
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyObjectException))]
        public void IsReadonlyObject_Reference()
        {
            var obj = FrozenContext.Single.GetQuery<ObjectClass>().First();
            var baseobj = FrozenContext.Single.GetQuery<ObjectClass>().Skip(2).First();
            obj.BaseObjectClass = baseobj;
        }
    }
}
