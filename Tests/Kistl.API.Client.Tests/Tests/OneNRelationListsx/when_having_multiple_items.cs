
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
    public class when_having_multiple_items
        : WrapperFixture
    {
        internal override List<NSide> InitialItems()
        {
            return new List<NSide>() 
            { 
                new NSide() { Description="First" },
                new NSide() { Description="Second" },
                new NSide() { Description="Third" },
                new NSide() { Description="Fourth" },
                new NSide() { Description="Fifth" },
            };
        }

        #region ICollection Tests


        [Test]
        public void should_have_the_items()
        {
            Assert.That(wrapper, Is.EquivalentTo(initialItems));
        }

        [Test]
        public void should_have_correct_count()
        {
            Assert.That(wrapper.Count, Is.EqualTo(initialItems.Count));
        }

        [Test]
        public void should_return_true_on_contains_containedItem()
        {
            foreach (var i in initialItems)
            {
                Assert.That(wrapper.Contains(i), Is.True);
            }
        }

        [Test]
        public void should_copyTo_any_length_array()
        {
            foreach (var length in new[] { 0, 1, 10, 100 })
            {
                foreach (var arrayIndex in new[] { 0, 1, 2, 9, 10, 11, 50, 99, 100, 101 })
                {
                    if (arrayIndex + initialItems.Count < length) // only legal calls
                    {
                        var array = new NSide[length];
                        Assert.That(() => wrapper.CopyTo(array, arrayIndex), Throws.Nothing);
                        var results = new NSide[initialItems.Count];
                        Array.Copy(array, arrayIndex, results, 0, results.Length);
                        Assert.That(results, Is.EquivalentTo(initialItems));
                    }
                }
            }
        }

        #endregion
    }
}