
namespace Kistl.API.Client.Tests.OneNRelationLists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client.Mocks.OneNLists;

    using NUnit.Framework;

    [TestFixture]
    public class when_having_a_item
        : WrapperFixture
    {

        private NSide containedItem;

        internal override List<NSide> InitialItems()
        {
            containedItem = new NSide() { Description = "containedItem" };
            return new List<NSide>() { new NSide() { Description = "Single" } };
        }

        #region ICollection tests

        [Test]
        public void should_have_the_item()
        {
            Assert.That(wrapper, Is.EquivalentTo(new[] { containedItem }));
        }

        [Test]
        public void should_yield_only_the_item()
        {
            bool first = true;
            foreach (var i in wrapper)
            {
                Assert.That(first, Is.True, "wrapper yielded second object [{0}]", i);
                Assert.That(i, Is.SameAs(containedItem));
                first = false;
            }
        }

        [Test]
        public void should_have_count_one()
        {
            Assert.That(wrapper.Count, Is.EqualTo(1));
        }

        [Test]
        public void should_return_true_on_contains_containedItem()
        {
            Assert.That(wrapper.Contains(containedItem), Is.True);
        }

        [Test]
        public void should_copyTo_any_length_array()
        {
            foreach (var length in new[] { 1, 10, 100 })
            {
                foreach (var arrayIndex in new[] { 0, 1, 2, 9, 10, 11, 50, 99, 100, 101 })
                {
                    if (arrayIndex + 1 < length) // only legal calls
                    {
                        var array = new NSide[length];
                        Assert.That(() => wrapper.CopyTo(array, arrayIndex), Throws.Nothing);
                        Assert.That(array[arrayIndex], Is.SameAs(containedItem));
                    }
                }
            }
        }

        #endregion
    }
}
