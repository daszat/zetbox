
namespace Kistl.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    public abstract class IListTests<TCollection, TItem>
        : ICollectionTests<TCollection, TItem>
        where TCollection : IList
    {
        protected IListTests(int items)
            : base(items) { }

        protected override void AssertInvariants(List<TItem> expectedItems)
        {
            Assert.That(collection, Is.EquivalentTo(expectedItems));
            base.AssertInvariants(expectedItems);
        }

        #region IList tests

        [Test]
        public void isfixedsize_should_not_be_set()
        {
            Assert.That(collection.IsFixedSize, Is.False);
            AssertInvariants(initialItems);
        }

        [Test]
        public void isreadonly_should_not_be_set()
        {
            Assert.That(collection.IsReadOnly, Is.False);
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_negative_index_on_get(
            [Range(-5, -1)] int index)
        {
            Assert.That(
                () => collection[index],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_too_big_index_on_get(
            [Range(0, 5)] int offset)
        {
            Assert.That(
                () => collection[initialItems.Count + offset],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_get_items()
        {
            for (int i = 0; i < initialItems.Count; i++)
            {
                Assert.That(collection[i], Is.SameAs(initialItems[i]));
            }
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_negative_index_on_set(
            [Range(-5, -1)] int index)
        {
            Assert.That(
                () => collection[index] = NewItem(),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_too_big_index_on_set(
            [Range(0, 5)] int offset)
        {
            Assert.That(
                () => collection[initialItems.Count + offset] = NewItem(),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_set_items_beginning(
            [Range(0, 5)] int index)
        {
            Assume.That(index, Is.GreaterThan(0));
            Assume.That(index, Is.LessThan(collection.Count));

            var newItem = NewItem();
            var oldItem = collection[index];
            Assert.That(() => { collection[index] = newItem; }, Throws.Nothing);
            Assert.That(collection, Has.Member(newItem));
            Assert.That(collection, Has.No.Member(oldItem));

            var expectedItems = new List<TItem>(initialItems);
            expectedItems[index] = newItem;
            AssertInvariants(expectedItems);
        }

        [Test]
        public void indexer_should_set_items_end(
            [Range(0, 5)] int offset)
        {
            var index = collection.Count - offset;
            Assume.That(index, Is.GreaterThan(0));
            Assume.That(index, Is.LessThan(collection.Count));

            indexer_should_set_items_beginning(index);
        }

        [Test]
        public void add_should_insert_items_at_the_end([Range(1, 100, 10)]int numItems)
        {
            var expectedItems = new List<TItem>(initialItems);
            for (int i = 0; i < numItems; i++)
            {
                var newItem = NewItem();
                
                var index = collection.Add(newItem);
                expectedItems.Add(newItem);

                Assert.That(index, Is.EqualTo(initialItems.Count + i));
                Assert.That(collection, Has.Member(newItem));
            }
            AssertInvariants(expectedItems);
        }

        [Test]
        public void clear_should_remove_all_items()
        {
            collection.Clear();
            Assert.That(collection, Is.Empty);
            AssertInvariants(new List<TItem>(0));
        }

        [Test]
        public void indexOf_should_return_index()
        {
            for (int i = 0; i < initialItems.Count; i++)
            {
                Assert.That(collection.IndexOf(initialItems[i]), Is.EqualTo(i));
            }
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexOf_should_return_minus_one_for_otherItem()
        {
            var otherItem = NewItem();
            Assert.That(collection.IndexOf(otherItem), Is.EqualTo(-1));
            AssertInvariants(initialItems);
        }

        [Test]
        public void insert_should_insert_at_the_beginning(
            [Range(0, 5)]int index)
        {
            Assume.That(index, Is.GreaterThanOrEqualTo(0));
            Assume.That(index, Is.LessThan(initialItems.Count));

            var newItem = NewItem();
            TestDelegate setter = () => collection.Insert(index, newItem);
            Assert.That(setter, Throws.Nothing, String.Format("i={0}, count={1}", index, initialItems.Count));
            Assert.That(collection, Has.Count.EqualTo(initialItems.Count + 1));
            Assert.That(collection, Has.Member(newItem));
            Assert.That(collection[index], Is.SameAs(newItem));

            var expectedItems = new List<TItem>(initialItems);
            expectedItems.Insert(index, newItem);
            AssertInvariants(expectedItems);
        }

        [Test]
        public void insert_should_insert_at_the_end(
            [Range(0, 5)]int offset)
        {
            var index = collection.Count - offset;
            Assume.That(index, Is.GreaterThan(0));
            Assume.That(index, Is.LessThan(collection.Count));

            insert_should_insert_at_the_beginning(index);
        }

        #endregion
    }
}
