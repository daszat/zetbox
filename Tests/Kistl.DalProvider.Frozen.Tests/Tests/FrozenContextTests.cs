using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

using NUnit.Framework;

namespace Kistl.DalProvider.Frozen.Tests
{
    [TestFixture]
    public class FrozenContextTests
    {

        [Test]
        public void should_have_IsReadonly_set()
        {
            Assert.That(FrozenContext.Single.IsReadonly, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyContextException))]
        public void should_forbid_Create()
        {
            var obj = FrozenContext.Single.Create<ObjectClass>();
        }


        [Test]
        public void should_only_hand_out_objects_with_IsReadonly_set()
        {
            foreach (var obj in FrozenContext.Single.GetQuery<ObjectClass>())
            {
                Assert.That(obj.IsReadonly, Is.True);
            }
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyObjectException))]
        public void should_forbid_setting_simple_properties()
        {
            var obj = FrozenContext.Single.GetQuery<ObjectClass>().First();
            obj.ClassName = "test";
        }

        [Test]
        [ExpectedException(typeof(ReadOnlyObjectException))]
        public void should_forbid_setting_reference_properties()
        {
            var obj = FrozenContext.Single.GetQuery<ObjectClass>().First();
            var baseobj = FrozenContext.Single.GetQuery<ObjectClass>().Skip(2).First();
            obj.BaseObjectClass = baseobj;
        }

    }
}
