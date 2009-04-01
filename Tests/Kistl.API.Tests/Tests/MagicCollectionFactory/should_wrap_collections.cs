using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests.MagicCollectionFactory
{
    [TestFixture]
    public class should_wrap_collections
    {
        [Test]
        public void from_string_to_object()
        {
            TestFactory(baseList => Utils.MagicCollectionFactory.WrapAsCollectionHelper<string, object>(baseList));
        }

        [Test]
        public void from_any_string_to_object()
        {
            TestFactory(baseList => Utils.MagicCollectionFactory.WrapAsCollection<object>((object)baseList));
        }

        private static void TestFactory(Func<List<string>, ICollection<object>> factory)
        {
            List<string> baseList = new List<string>() { "a", "b" };

            ICollection<object> wrappedList = null;

            Assert.DoesNotThrow(() => wrappedList = factory(baseList));

            Assert.That(wrappedList, Is.EquivalentTo(baseList));

            Assert.DoesNotThrow(() => wrappedList.Add("c"));
            Assert.Throws<InvalidCastException>(() => wrappedList.Add(new object()));

            Assert.That(wrappedList, Is.EquivalentTo(baseList));
            Assert.That(baseList, Is.EquivalentTo(new[] { "a", "b", "c" }));
        }

    }
}
