
namespace Kistl.API.Tests.InterfaceTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    public interface BaseClass : IDataObject { }
    public interface ChildClass : BaseClass { }

    public interface ValueCollectionEntry : IValueCollectionEntry<BaseClass, ChildClass> { }

    [TestFixture]
    public class when_looking_for_root_type : AbstractApiTestFixture
    {
        [Test]
        public void should_recognize_data_objects()
        {
            var baseInterfaceType = iftFactory(typeof(BaseClass));
            Assert.That(baseInterfaceType.GetRootType(), Is.EqualTo(baseInterfaceType));

            var childInterfaceType = iftFactory(typeof(ChildClass));
            Assert.That(childInterfaceType.GetRootType(), Is.EqualTo(baseInterfaceType));
        }

        [Test]
        public void should_recognize_valueCollectionEntries()
        {
            var ceInterfaceType = iftFactory(typeof(ValueCollectionEntry));
            Assert.That(ceInterfaceType.GetRootType(), Is.EqualTo(ceInterfaceType));
        }
    }
}
