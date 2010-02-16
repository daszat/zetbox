
namespace Kistl.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API.Mocks;

    using NUnit.Framework;

    [TestFixture(0, false)]
    [TestFixture(1, false)]
    [TestFixture(10, false)]
    [TestFixture(50, false)]
    [TestFixture(0, true)]
    [TestFixture(1, true)]
    [TestFixture(10, true)]
    [TestFixture(50, true)]
    public class BasicNotifyingObservableCollectionTests
        : BasicListTests<NotifyingObservableCollection<TestDataObject>, TestDataObject>
    {
        private readonly bool _withBeginUpdate;

        private TestDataObject parent;
        private bool _hasCollectionChanged;
        private bool _hasParentChanged;
        private bool _expectChanges;

        public BasicNotifyingObservableCollectionTests(int items, bool withBeginUpdate)
            : base(items)
        {
            _withBeginUpdate = withBeginUpdate;
        }

        protected override TestDataObject NewItem()
        {
            int id = NewItemNumber();
            return new TestDataObject__Implementation__() { ID = id, StringProperty = "item#" + id };
        }

        protected override NotifyingObservableCollection<TestDataObject> CreateCollection(List<TestDataObject> items)
        {
            parent = new TestDataObject__Implementation__();
            var result = new NotifyingObservableCollection<TestDataObject>(parent, "ParentProperty");
            foreach (var i in items)
            {
                result.Add(i);
            }

            _hasCollectionChanged = false;
            result.CollectionChanged += (sender, args) => { _hasCollectionChanged = true; };

            _hasParentChanged = false;
            parent.PropertyChanged += (sender, args) => { if (args.PropertyName == "ParentProperty") { _hasParentChanged = true; } };

            if (_withBeginUpdate)
            {
                result.BeginUpdate();
                _expectChanges = false;
            }

            return result;
        }

        protected override void AssertCollectionIsChanged()
        {
            if (_withBeginUpdate)
            {
                _expectChanges = true;
            }
            else
            {
                AssertCollectionIsChangedCore();
            }
        }

        protected override void AssertCollectionIsUnchanged()
        {
            if (!_withBeginUpdate)
            {
                AssertCollectionIsUnchangedCore();
            }
        }

        private void AssertCollectionIsChangedCore()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasCollectionChanged, "Collection was not notified");
            _hasCollectionChanged = false;

            Assert.That(_hasParentChanged, "Parent was not notified");
            _hasParentChanged = false;
        }

        private void AssertCollectionIsUnchangedCore()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasCollectionChanged, "Collection was notified falsely");
            _hasCollectionChanged = false;

            Assert.That(!_hasParentChanged, "Parent was notified falsely");
            _hasParentChanged = false;
        }

        protected override void AssertInvariants(List<TestDataObject> expectedItems)
        {
            Assert.That(collection.PropertyName, Is.EqualTo("ParentProperty"));
            if (_withBeginUpdate)
            {
                collection.EndUpdate();
                if (_expectChanges)
                {
                    AssertCollectionIsChangedCore();
                }
                else
                {
                    AssertCollectionIsUnchangedCore();
                }
                // reset beginUpdate
                collection.BeginUpdate();
                _expectChanges = false;
            }
            base.AssertInvariants(expectedItems);
        }
    }

    [TestFixture(0, false)]
    [TestFixture(1, false)]
    [TestFixture(10, false)]
    [TestFixture(50, false)]
    [TestFixture(0, true)]
    [TestFixture(1, true)]
    [TestFixture(10, true)]
    [TestFixture(50, true)]
    public class GenericNotifyingObservableCollectionTests
        : GenericListTests<NotifyingObservableCollection<TestDataObject>, TestDataObject>
    {
        private readonly bool _withBeginUpdate;

        private TestDataObject parent;
        private bool _hasCollectionChanged;
        private bool _hasParentChanged;
        private bool _expectChanges;

        public GenericNotifyingObservableCollectionTests(int items, bool withBeginUpdate)
            : base(items)
        {
            _withBeginUpdate = withBeginUpdate;
        }

        protected override TestDataObject NewItem()
        {
            int id = NewItemNumber();
            return new TestDataObject__Implementation__() { ID = id, StringProperty = "item#" + id };
        }

        protected override NotifyingObservableCollection<TestDataObject> CreateCollection(List<TestDataObject> items)
        {
            parent = new TestDataObject__Implementation__();
            var result = new NotifyingObservableCollection<TestDataObject>(parent, "ParentProperty");
            foreach (var i in items)
            {
                result.Add(i);
            }

            _hasCollectionChanged = false;
            result.CollectionChanged += (sender, args) => { _hasCollectionChanged = true; };

            _hasParentChanged = false;
            parent.PropertyChanged += (sender, args) => { if (args.PropertyName == "ParentProperty") { _hasParentChanged = true; } };

            if (_withBeginUpdate)
            {
                result.BeginUpdate();
                _expectChanges = false;
            }

            return result;
        }

        protected override void AssertCollectionIsChanged()
        {
            if (_withBeginUpdate)
            {
                _expectChanges = true;
            }
            else
            {
                AssertCollectionIsChangedCore();
            }
        }

        protected override void AssertCollectionIsUnchanged()
        {
            if (!_withBeginUpdate)
            {
                AssertCollectionIsUnchangedCore();
            }
        }

        private void AssertCollectionIsChangedCore()
        {
            base.AssertCollectionIsChanged();
            Assert.That(_hasCollectionChanged, "Collection was not notified");
            _hasCollectionChanged = false;

            Assert.That(_hasParentChanged, "Parent was not notified");
            _hasParentChanged = false;
        }

        private void AssertCollectionIsUnchangedCore()
        {
            base.AssertCollectionIsUnchanged();
            Assert.That(!_hasCollectionChanged, "Collection was notified falsely");
            _hasCollectionChanged = false;

            Assert.That(!_hasParentChanged, "Parent was notified falsely");
            _hasParentChanged = false;
        }

        protected override void AssertInvariants(List<TestDataObject> expectedItems)
        {
            Assert.That(collection.PropertyName, Is.EqualTo("ParentProperty"));
            if (_withBeginUpdate)
            {
                collection.EndUpdate();
                if (_expectChanges)
                {
                    AssertCollectionIsChangedCore();
                }
                else
                {
                    AssertCollectionIsUnchangedCore();
                }
                // reset beginUpdate
                collection.BeginUpdate();
                _expectChanges = false;
            }
            base.AssertInvariants(expectedItems);
        }
    }
}
