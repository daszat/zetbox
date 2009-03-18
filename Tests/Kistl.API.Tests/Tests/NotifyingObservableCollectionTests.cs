using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;


namespace Kistl.API.Tests
{
    [TestFixture]
    public class NotifyingObservableCollectionTests
    {
        TestDataObject parent;
        NotifyingObservableCollection<TestDataObject> list;

        TestDataObject a;
        TestDataObject b;

        [SetUp]
        public void SetUp()
        {
            parent = new TestDataObject__Implementation__();
            list = new NotifyingObservableCollection<TestDataObject>(parent, "ParentProperty");

            a = new TestDataObject__Implementation__();
            b = new TestDataObject__Implementation__();

            list.Add(a);
            list.Add(b);
        }

        [Test]
        public void List()
        {
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void Event()
        {
            bool hasChanged = false;
            parent.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) { hasChanged = true; });
            a.NotifyChange();

            Assert.That(hasChanged, Is.True);
        }

        [Test]
        public void SetItem()
        {
            bool hasChanged = false;
            parent.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) { hasChanged = true; });
            list[0] = new TestDataObject__Implementation__();

            Assert.That(hasChanged, Is.True);
        }

        [Test]
        public void Property()
        {
            Assert.That(list.PropertyName, Is.EqualTo("ParentProperty"));
        }

        [Test]
        public void SetItem_WithBeginUpdate()
        {
            bool hasChanged = false;
            parent.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) { hasChanged = true; });

            list.BeginUpdate();
            list[0] = new TestDataObject__Implementation__();
            list.EndUpdate();

            Assert.That(hasChanged, Is.False);

            list[1] = new TestDataObject__Implementation__();

            Assert.That(hasChanged, Is.True);
        }
    }
}
