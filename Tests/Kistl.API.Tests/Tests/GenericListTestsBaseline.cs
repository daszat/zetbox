
namespace Kistl.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    // see https://bugzilla.novell.com/show_bug.cgi?id=670331
#if !MONO
    [TestFixture(0, TypeArgs = new[] { typeof(List<Item>) })]
    [TestFixture(1, TypeArgs = new[] { typeof(List<Item>) })]
    [TestFixture(10, TypeArgs = new[] { typeof(List<Item>) })]
    [TestFixture(50, TypeArgs = new[] { typeof(List<Item>) })]
    public class GenericListTestsBaseline<TCollection>
        : GenericListTests<TCollection, Item>
        where TCollection : IList<Item>, new()
    {
        public GenericListTestsBaseline(int items)
            : base(items) { }

        protected override Item NewItem()
        {
            return new Item() { Description = "item#" + NewItemNumber() };
        }

        protected override TCollection CreateCollection(List<Item> items)
        {
            var result = new TCollection();
            foreach (var i in items)
            {
                result.Add(i);
            }
            return result;
        }
    }
#endif
}
