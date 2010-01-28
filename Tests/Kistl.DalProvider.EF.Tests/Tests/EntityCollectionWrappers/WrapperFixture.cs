
namespace Kistl.DalProvider.EF.Tests.EntityCollectionWrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    using NUnit.Framework;

    public abstract class WrapperFixture<TWrapper>
        where TWrapper : EntityCollectionWrapper<Property, Property__Implementation__>
    {
        protected List<Property__Implementation__> underlyingCollection;
        protected TWrapper wrapper;

        /// <summary>
        /// Is called before each test to initialize the new collection before passing it to the wrapper
        /// </summary>
        protected abstract void InitItems();

        /// <summary>
        /// Is called before each test to create the new wrapper fixture from the <see cref="underlyingCollection"/>
        /// </summary>
        protected virtual TWrapper CreateWrapper()
        {
            return (TWrapper)new EntityCollectionWrapper<Property, Property__Implementation__>(null, underlyingCollection);
        }

        [SetUp]
        public void Setup()
        {
            underlyingCollection = new List<Property__Implementation__>();
            InitItems();
            wrapper = CreateWrapper();
        }

        [Test]
        public void should_not_be_readonly()
        {
            Assert.That(wrapper.IsReadOnly, Is.False);
        }

        [Test]
        public void should_be_equivalent_to_underlyingCollection()
        {
            Assert.That(wrapper.OrderBy(o => o.GetHashCode()), Is.EquivalentTo(underlyingCollection.OrderBy(o => o.GetHashCode())));
        }

        [Test]
        public void should_add_item()
        {
            var originalList = underlyingCollection.ToList();
            var item = new Property__Implementation__();

            wrapper.Add(item);
            originalList.Add(item);

            Assert.That(wrapper, Is.EquivalentTo(originalList));
            Assert.That(underlyingCollection, Is.EquivalentTo(originalList));
        }

        [Test]
        public void should_clear()
        {
            Assert.That(() => wrapper.Clear(), Throws.Nothing);
            Assert.That(wrapper, Is.Empty);
            Assert.That(underlyingCollection, Is.Empty);
        }

        [Test]
        public void should_return_false_on_contains_otherItem()
        {
            var item = new Property__Implementation__();
            Assert.That(wrapper.Contains(item), Is.False);
        }

        [Test]
        public void copyTo_should_check_for_null_array()
        {
            Assert.That(() => wrapper.CopyTo(null, 1), Throws.InstanceOf(typeof(ArgumentNullException)));
        }

        [Test]
        public void copyTo_should_check_for_negative_index()
        {
            Assert.That(() => wrapper.CopyTo(new Property[10], -11), Throws.InstanceOf(typeof(ArgumentOutOfRangeException)));
        }

        [Test]
        public void copyTo_should_check_for_overflow()
        {
            var array = new Property[10];
            Assert.That(() => wrapper.CopyTo(array, array.Length), Throws.InstanceOf(typeof(ArgumentException)));
            Assert.That(() => wrapper.CopyTo(array, array.Length + 10), Throws.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void should_return_false_on_remove_other_item()
        {
            var item = new Property__Implementation__();
            Assert.That(wrapper.Remove(item), Is.False);
        }
    }
}
