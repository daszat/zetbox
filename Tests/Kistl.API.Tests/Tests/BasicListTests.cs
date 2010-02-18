
namespace Kistl.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    public abstract class BasicListTests<TCollection, TItem>
        : BasicCollectionTests<TCollection, TItem>
        where TCollection : IList
    {
        protected BasicListTests(int items)
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
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        [Test]
        public void isreadonly_should_not_be_set()
        {
            Assert.That(collection.IsReadOnly, Is.False);
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_negative_index_on_get(
            [Range(-5, -1)] int index)
        {
            Assert.That(
                () => collection[index],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_too_big_index_on_get(
            [Range(0, 5)] int offset)
        {
            Assert.That(
                () => collection[initialItems.Count + offset],
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_get_items()
        {
            for (int i = 0; i < initialItems.Count; i++)
            {
                Assert.That(collection[i], Is.SameAs(initialItems[i]), String.Format("i={0}", i));
                AssertCollectionIsUnchanged();
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
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexer_should_check_too_big_index_on_set(
            [Range(0, 5)] int offset)
        {
            Assert.That(
                () => collection[initialItems.Count + offset] = NewItem(),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertCollectionIsUnchanged();
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

            AssertCollectionIsChanged();

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
                AssertCollectionIsChanged();
            }
            AssertInvariants(expectedItems);
        }

        [Test]
        public void clear_should_remove_all_items()
        {
            var count = collection.Count;
            collection.Clear();
            Assert.That(collection, Is.Empty);
            // The ObservableCollection does fire a notification 
            // even if the collection is empty; Since we don't 
            // actually want this, but need to properly test it,
            // we special case it here:
            if (count == 0 && !IsObservableCollection(typeof(TCollection)))
            {
                AssertCollectionIsUnchanged();
            }
            else
            {
                AssertCollectionIsChanged();
            }
            AssertInvariants(new List<TItem>(0));
        }

        private static bool IsObservableCollection(Type t)
        {
            while (t != null)
            {
                if (t.IsGenericType && typeof(ObservableCollection<>).IsAssignableFrom(t.GetGenericTypeDefinition()))
                {
                    return true;
                }
                t = t.BaseType;
            }
            return false;
        }

        [Test]
        public void indexOf_should_return_index()
        {
            for (int i = 0; i < initialItems.Count; i++)
            {
                Assert.That(collection.IndexOf(initialItems[i]), Is.EqualTo(i));
                AssertCollectionIsUnchanged();
            }
            AssertInvariants(initialItems);
        }

        [Test]
        public void indexOf_should_return_minus_one_for_otherItem()
        {
            var otherItem = NewItem();
            Assert.That(collection.IndexOf(otherItem), Is.EqualTo(-1));
            AssertCollectionIsUnchanged();
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
            AssertCollectionIsChanged();

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

        [Test]
        public void remove_should_remove_contained_object_from_beginning(
             [Range(0, 5)]int index)
        {
            Assume.That(index, Is.GreaterThanOrEqualTo(0));
            Assume.That(index, Is.LessThan(initialItems.Count));

            var item = initialItems[index];
            TestDelegate remover = () => collection.Remove(item);
            Assert.That(remover, Throws.Nothing, String.Format("i={0}, count={1}", index, initialItems.Count));
            Assert.That(collection, Has.Count.EqualTo(initialItems.Count - 1));
            Assert.That(collection, Has.No.Member(item));

            AssertCollectionIsChanged();

            var expectedItems = new List<TItem>(initialItems);
            expectedItems.Remove(item);
            AssertInvariants(expectedItems);
        }

        [Test]
        public void remove_should_remove_contained_object_from_end(
             [Range(0, 5)]int offset)
        {
            remove_should_remove_contained_object_from_beginning(initialItems.Count - offset);
        }

        [Test]
        public void remove_should_ignore_other_item()
        {
            var otherItem = NewItem();
            TestDelegate remover = () => collection.Remove(otherItem);
            Assert.That(remover, Throws.Nothing);
            Assert.That(collection, Has.Count.EqualTo(initialItems.Count));
            Assert.That(collection, Has.No.Member(otherItem));

            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        #endregion
    }
}
