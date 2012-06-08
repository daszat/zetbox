// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    public abstract class GenericListTests<TList, TItem>
        : GenericCollectionTests<TList, TItem>
        where TList : IList<TItem>
    {
        protected GenericListTests(int items)
            : base(items) { }

        protected override void AssertInvariants(List<TItem> expectedItems)
        {
            Assert.That(collection, Is.EquivalentTo(expectedItems));
            base.AssertInvariants(expectedItems);
        }

        #region IList tests

        [Test]
        public void indexer_should_check_negative_index_on_get(
            [Range(-5, -1)] int index)
        {
            TItem ignored = default(TItem);
            Assert.That(
                () => { ignored = collection[index]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(ignored, Is.EqualTo(default(TItem)));
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
                Assert.That(collection[i], Is.SameAs(initialItems[i]));
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
        public void removeat_should_remove_item([Range(0, 5)]int index)
        {
            Assume.That(index, Is.GreaterThanOrEqualTo(0));
            Assume.That(index, Is.LessThan(initialItems.Count));

            collection.RemoveAt(index);

            AssertCollectionIsChanged();

            var expectedItems = new List<TItem>(initialItems);
            expectedItems.RemoveAt(index);
            AssertInvariants(expectedItems);
        }

        [Test]
        public void removeat_should_remove_item_at_end([Range(0, 5)]int offset)
        {
            removeat_should_remove_item(initialItems.Count - offset);
        }

        [Test]
        public void removeat_should_throw_on_negative_index([Range(-5, -1)]int index)
        {
            TestDelegate remover = () => collection.RemoveAt(index);
            Assert.That(remover, Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        [Test]
        public void removeat_should_throw_on_index_greater_or_equal_count([Range(0, 5)]int offset)
        {
            TestDelegate remover = () => collection.RemoveAt(initialItems.Count + offset);
            Assert.That(remover, Throws.InstanceOf<ArgumentOutOfRangeException>());
            AssertCollectionIsUnchanged();
            AssertInvariants(initialItems);
        }

        #endregion
    }
}
