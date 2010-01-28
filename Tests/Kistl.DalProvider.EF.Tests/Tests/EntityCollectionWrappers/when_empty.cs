
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
    public class when_empty<TWrapper>
        : WrapperFixture<TWrapper>
        where TWrapper : EntityCollectionWrapper<Property, Property__Implementation__>
    {
        protected override void InitItems() { }

        [Test]
        public void should_be_empty()
        {
            Assert.That(wrapper, Is.Empty);
        }

        [Test]
        public void should_leave_underlyingCollection_empty()
        {
            Assert.That(underlyingCollection, Is.Empty);
        }

        [Test]
        public void should_yield_no_items()
        {
            foreach (var i in wrapper)
            {
                Assert.Fail("empty wrapper yielded object [{0}]", i);
            }
        }

        [Test]
        public void should_have_zero_count()
        {
            Assert.That(wrapper.Count, Is.EqualTo(0));
        }

        [Test]
        public void should_copyTo_any_length_array()
        {
            foreach (var length in new[] { 0, 1, 10, 100 })
            {
                foreach (var arrayIndex in new[] { 0, 1, 2, 9, 10, 11, 50, 99, 100, 101 })
                {
                    if (arrayIndex < length) // only legal calls
                    {
                        var array = new Property[length];
                        Assert.That(() => wrapper.CopyTo(array, arrayIndex), Throws.Nothing);
                        for (int i = 0; i < length; i++)
                        {
                            Assert.That(array[i], Is.Null);
                        }
                    }
                }
            }
        }
    }
}
