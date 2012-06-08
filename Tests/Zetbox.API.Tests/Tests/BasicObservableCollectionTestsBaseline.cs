
namespace Zetbox.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public sealed class BasicObservableCollectionTestsBaseline
        : BasicListTests<ObservableCollection<Item>, Item>
    {
        private bool _hasChanged;

        public BasicObservableCollectionTestsBaseline(int items)
            : base(items) { }

        protected override Item NewItem()
        {
            return new Item() { Description = "item#" + NewItemNumber() };
        }

        protected override ObservableCollection<Item> CreateCollection(List<Item> items)
        {
            var result = new ObservableCollection<Item>();
            foreach (var i in items)
            {
                result.Add(i);
            }
            _hasChanged = false;
            result.CollectionChanged += (sender, args) => { _hasChanged = true; };
            return result;
        }

        protected override void AssertCollectionIsChanged()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasChanged);
            _hasChanged = false;
        }

        protected override void AssertCollectionIsUnchanged()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasChanged);
            _hasChanged = false;
        }
    }
}
