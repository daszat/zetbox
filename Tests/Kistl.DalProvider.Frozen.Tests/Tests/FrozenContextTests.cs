
namespace Kistl.DalProvider.Frozen.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    using NUnit.Framework;
    
    [TestFixture]
    public class FrozenContextTests
    {
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

        [Test]
        public void should_provide_each_AttachedObject_only_once()
        {
            var numberOfAttachedObjects
                = FrozenContext.Single.AttachedObjects.Count();
            var numberOfDistinctAttachedObjects
                = FrozenContext.Single.AttachedObjects.Distinct().Count();

            Assert.That(numberOfAttachedObjects, Is.EqualTo(numberOfDistinctAttachedObjects));
        }
    }
}
