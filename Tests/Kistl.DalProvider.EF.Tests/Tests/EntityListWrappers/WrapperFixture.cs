
namespace Kistl.DalProvider.EF.Tests.EntityListWrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    using NUnit.Framework;

    public abstract class WrapperFixture
    {
        protected List<Property__Implementation__> initialItems;
        protected List<Property__Implementation__> underlyingCollection;
        protected EntityListWrapper<Property, Property__Implementation__> wrapper;

        /// <summary>
        /// Is called before each test to initialize the new collection before passing it to the wrapper
        /// </summary>
        protected abstract List<Property__Implementation__> InitialItems();

        /// <summary>
        /// Is called before each test to create the new wrapper fixture from the <see cref="underlyingCollection"/>
        /// </summary>
        protected virtual EntityListWrapper<Property, Property__Implementation__> CreateWrapper()
        {
            return new EntityListWrapper<Property, Property__Implementation__>(null, underlyingCollection, "ObjectClass");
        }

        [SetUp]
        public void Setup()
        {
            initialItems = InitialItems();
            underlyingCollection = new List<Property__Implementation__>(initialItems);
            wrapper = CreateWrapper();
        }

        [Test]
        public void indexer_should_check_index_range_on_get()
        {
            Assert.That(
                () => wrapper[-1],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(
                () => wrapper[initialItems.Count],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(
                () => wrapper[initialItems.Count + 1],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(
                () => wrapper[initialItems.Count + 99],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void indexer_should_check_index_range_on_set(
            [Range(-2, 30)]int i)
        {
            Assume.That(i, Is.GreaterThan(-3));
            Assume.That(i, Is.LessThan(initialItems.Count + 10));

            var newItem = new Property__Implementation__() { Description = "newItem#" + i };
            TestDelegate setter = () => wrapper[i] = newItem;
            var msg = String.Format("i={0}, count={1}", i, initialItems.Count);

            if (i < 0 || i >= initialItems.Count)
            {
                Assert.That(setter, Throws.InstanceOf<ArgumentOutOfRangeException>(), msg);
            }
            else
            {
                Assert.That(setter, Throws.Nothing, msg);
                Assert.That(wrapper[i], Is.SameAs(newItem));
                Assert.That(underlyingCollection, Has.Member(newItem));
            }
        }

        [Test]
        public void indexer_should_get_items()
        {
            Assume.That(initialItems.Count, Is.GreaterThan(0));
            for (int i = 0; i < initialItems.Count; i++)
            {
                Assert.That(wrapper[i], Is.SameAs(initialItems[i]));
            }
        }

        [Test]
        public void indexer_should_replace_items()
        {
            Assume.That(initialItems.Count, Is.GreaterThan(0));
            for (int i = 0; i < initialItems.Count; i++)
            {
                var newItem = new Property__Implementation__() { Description = "newItem#" + i };
                var oldItem = wrapper[i];
                wrapper[i] = newItem;
                Assert.That(wrapper[i], Is.SameAs(newItem));
                Assert.That(underlyingCollection, Has.Member(newItem));
                Assert.That(underlyingCollection, Has.No.Member(oldItem));
            }
        }

        [Test]
        public void indexOf_should_return_index()
        {
            for (int i = 0; i < initialItems.Count; i++)
            {
                Assert.That(wrapper.IndexOf(initialItems[i]), Is.EqualTo(i));
            }
        }

        [Test]
        public void indexOf_should_return_minus_one_for_otherItem()
        {
            var otherItem = new Property__Implementation__() { Description = "otherItem" };
            Assert.That(wrapper.IndexOf(otherItem), Is.EqualTo(-1));
        }

        [Test]
        public void insert_should_insert_at_the_right_place(
            [Range(-2, 30)]int i)
        {
            Assume.That(i, Is.GreaterThan(-3));
            Assume.That(i, Is.LessThan(initialItems.Count + 10));

            // little reminder; insert(0, X) on empty list works:
            var lst = new List<object>();
            Assert.That(() => lst.Insert(0, new object()), Throws.Nothing);
            // end reminder

            var newItem = new Property__Implementation__() { Description = "newItem#" + i };
            TestDelegate setter = () => wrapper.Insert(i, newItem);
            var msg = String.Format("i={0}, count={1}", i, initialItems.Count);

            if (i < 0 || i > initialItems.Count)
            {
                Assert.That(setter, Throws.InstanceOf<ArgumentOutOfRangeException>(), msg);
            }
            else
            {
                Assert.That(setter, Throws.Nothing, msg);
                Assert.That(wrapper[i], Is.SameAs(newItem));
                Assert.That(underlyingCollection, Has.Member(newItem));
            }
        }
    }
}
