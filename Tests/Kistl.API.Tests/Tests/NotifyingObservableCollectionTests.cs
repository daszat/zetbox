using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.IO;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.ComponentModel;


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
            parent = new TestDataObject();
            list = new NotifyingObservableCollection<TestDataObject>(parent, "");

            a = new TestDataObject();
            b = new TestDataObject();

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
            list[0] = new TestDataObject();

            Assert.That(hasChanged, Is.True);
        }
    }
}
