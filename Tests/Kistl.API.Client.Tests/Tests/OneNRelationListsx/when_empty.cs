
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
    public class when_empty
        : WrapperFixture
    {
        internal override List<NSide> InitialItems()
        {
            return new List<NSide>();
        }

        #region ICollection tests

        [Test]
        public void should_be_empty()
        {
            Assert.That(wrapper, Is.Empty);
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
                        var array = new NSide[length];
                        Assert.That(() => wrapper.CopyTo(array, arrayIndex), Throws.Nothing);
                        for (int i = 0; i < length; i++)
                        {
                            Assert.That(array[i], Is.Null);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
