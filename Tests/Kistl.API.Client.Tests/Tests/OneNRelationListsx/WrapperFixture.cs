
namespace Kistl.API.Client.Tests.OneNRelationLists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client.Mocks.OneNLists;

    using NUnit.Framework;

    [Ignore]
    public abstract class WrapperFixture
    {
        internal List<NSide> initialItems;
        internal OneSide obj;
        internal OneNRelationList<INSide> wrapper;

        /// <summary>
        /// Is called before each test to initialize the new collection before passing it to the object
        /// </summary>
        internal abstract List<NSide> InitialItems();

        /// <summary>
        /// Is called before each test to create the new wrapper fixture from the <see cref="initialItems"/>
        /// </summary>
        internal virtual OneSide CreateFixture()
        {
            return new OneSide(initialItems.Cast<INSide>().ToList());
        }

        [SetUp]
        public void Setup()
        {
            initialItems = InitialItems();
            obj = CreateFixture();
            wrapper = obj.List;
        }

        #region ICollection<> tests

        [Test]
        public void should_not_be_readonly()
        {
            Assert.That(wrapper.IsReadOnly, Is.False);
        }

        [Test]
        public void should_be_equivalent_to_initialItems()
        {
            Assert.That(wrapper.OrderBy(o => o.GetHashCode()), Is.EquivalentTo(initialItems.OrderBy(o => o.GetHashCode())));
        }

        [Test]
        public void should_add_item()
        {
            var newItem = new NSide() { Description = "newItem" };

            wrapper.Add(newItem);
            initialItems.Add(newItem);

            Assert.That(newItem.OneSide, Is.SameAs(obj));
            Assert.That(newItem.OneSide_pos, Is.Not.Null);

            // check both that the list is in the correct order (item added at the end)
            // as well as that the index was set to maintain the order when going to the server
            Assert.That(wrapper, Is.EquivalentTo(initialItems));
            Assert.That(wrapper, Is.EquivalentTo(initialItems.OrderBy(ns => ns.OneSide_pos.Value)));
        }

        [Test]
        public void should_clear()
        {
            Assert.That(() => wrapper.Clear(), Throws.Nothing);
            Assert.That(wrapper, Is.Empty);
            foreach (var item in initialItems)
            {
                Assert.That(item.OneSide, Is.Null);
                Assert.That(item.OneSide_pos, Is.Null);
            }
        }

        [Test]
        public void should_return_false_on_contains_otherItem()
        {
            var otherItem = new NSide() { Description = "otherItem" };
            Assert.That(wrapper.Contains(otherItem), Is.False);
        }

        [Test]
        public void copyTo_should_check_for_null_array()
        {
            Assert.That(() => wrapper.CopyTo(null, 1), Throws.InstanceOf(typeof(ArgumentNullException)));
        }

        [Test]
        public void copyTo_should_check_for_negative_index()
        {
            Assert.That(() => wrapper.CopyTo(new NSide[10], -11), Throws.InstanceOf(typeof(ArgumentOutOfRangeException)));
        }

        [Test]
        public void copyTo_should_check_for_overflow()
        {
            var array = new NSide[10];
            Assert.That(() => wrapper.CopyTo(array, array.Length), Throws.InstanceOf(typeof(ArgumentException)));
            Assert.That(() => wrapper.CopyTo(array, array.Length + 10), Throws.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void should_return_false_on_remove_other_item()
        {
            var otherItem = new NSide() { Description = "otherItem" };
            Assert.That(wrapper.Remove(otherItem), Is.False);
        }
        #endregion

        #region IList<> tests

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

            var newItem = new NSide() { Description = "newItem#" + i };
            TestDelegate setter = () => wrapper[i] = newItem;
            var msg = String.Format("i={0}, count={1}", i, initialItems.Count);

            if (i < 0 || i >= initialItems.Count)
            {
                Assert.That(setter, Throws.InstanceOf<ArgumentOutOfRangeException>(), msg);
            }
            else
            {
                initialItems[i] = newItem;

                Assert.That(setter, Throws.Nothing, msg);
                Assert.That(wrapper[i], Is.SameAs(newItem));

                Assert.That(newItem.OneSide, Is.SameAs(obj));
                Assert.That(newItem.OneSide_pos, Is.Not.Null);

                // check both that the list is in the correct order (item added at the end)
                // as well as that the index was set to maintain the order when going to the server
                Assert.That(wrapper, Is.EquivalentTo(initialItems));
                Assert.That(wrapper, Is.EquivalentTo(initialItems.OrderBy(ns => ns.OneSide_pos.Value)));
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
                var newItem = new NSide() { Description = "newItem#" + i };
                var oldItem = (NSide)wrapper[i];
                
                wrapper[i] = newItem;
                initialItems[i] = newItem;

                Assert.That(wrapper[i], Is.SameAs(newItem));

                Assert.That(oldItem.OneSide, Is.Null);
                Assert.That(oldItem.OneSide_pos, Is.Null);

                Assert.That(newItem.OneSide, Is.SameAs(obj));
                Assert.That(newItem.OneSide_pos, Is.Not.Null);

                // check both that the list is in the correct order (item added at the end)
                // as well as that the index was set to maintain the order when going to the server
                Assert.That(wrapper, Is.EquivalentTo(initialItems));
                Assert.That(wrapper, Is.EquivalentTo(initialItems.OrderBy(ns => ns.OneSide_pos.Value)));
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
            var otherItem = new NSide() { Description = "otherItem" };
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

            var newItem = new NSide() { Description = "newItem#" + i };
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

                Assert.That(newItem.OneSide, Is.SameAs(obj));
                Assert.That(newItem.OneSide_pos, Is.Not.Null);

                // check both that the list is in the correct order (item added at the end)
                // as well as that the index was set to maintain the order when going to the server
                Assert.That(wrapper, Is.EquivalentTo(initialItems));
                Assert.That(wrapper, Is.EquivalentTo(initialItems.OrderBy(ns => ns.OneSide_pos.Value)));
            }
        }
        #endregion
    }
}
