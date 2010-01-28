
namespace Kistl.DalProvider.EF.Tests.EntityCollectionWrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    using NUnit.Framework;

    [TestFixture(TypeArgs = new[] { typeof(EntityCollectionWrapper<Property, Property__Implementation__>) })]
    public class when_having_a_item<TWrapper>
        : WrapperFixture<TWrapper>
        where TWrapper : EntityCollectionWrapper<Property, Property__Implementation__>
    {
        private Property__Implementation__ containedItem;

        protected override void InitItems()
        {
            containedItem = new Property__Implementation__();
            underlyingCollection.Add(containedItem);
        }

        [Test]
        public void should_have_the_item()
        {
            Assert.That(wrapper, Is.EquivalentTo(new[] { containedItem }));
        }

        [Test]
        public void should_leave_underlyingCollection_in_correct_state()
        {
            Assert.That(underlyingCollection, Is.EquivalentTo(new[] { containedItem }));
        }

        [Test]
        public void should_yield_only_the_item()
        {
            bool first = true;
            foreach (var i in wrapper)
            {
                Assert.That(first, Is.True, "wrapper yielded second object [{0}]", i);
                Assert.That(i, Is.SameAs(containedItem));
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
                        var array = new Property[length];
                        Assert.That(() => wrapper.CopyTo(array, arrayIndex), Throws.Nothing);
                        Assert.That(array[arrayIndex], Is.SameAs(containedItem));
                    }
                }
            }
        }
    }
}
