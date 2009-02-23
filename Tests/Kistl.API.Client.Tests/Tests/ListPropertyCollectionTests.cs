using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using Kistl.API.Client;
using System.Reflection;
using System.IO;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Kistl.API.Client.Tests
{
    [TestFixture]
    public class ListPropertyCollectionTests
    {
        NewListPropertyCollection<TestObjClass, string, TestObjClass_TestNameCollectionEntry> list;
        TestObjClass parent;

        string a;
        string b;

        [SetUp]
        public void SetUp()
        {
            parent = new TestObjClass__Implementation__();
            list = new NewListPropertyCollection<TestObjClass, string, TestObjClass_TestNameCollectionEntry>(parent, "TestNames");

            a = "A-String";
            b = "B-String";

            list.UnderlyingCollection.Add(new TestObjClass_TestNameCollectionEntry() { /*ID = 1,*/ B = a });
            list.UnderlyingCollection.Add(new TestObjClass_TestNameCollectionEntry() { /*ID = 2,*/ B = b });

            list.UnderlyingCollection[0].SetPrivatePropertyValue<int>("ID", 1);
            list.UnderlyingCollection[1].SetPrivatePropertyValue<int>("ID", 2);
        }

        [Test]
        public void List()
        {
            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddItem()
        {
            string n = "N-String";
            list.Add(n);
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list[2], Is.Not.Null);
            Assert.That(list[2], Is.EqualTo(n));
        }

        [Test]
        public void SetItem()
        {
            string n = "N-String";
            list[0] = n;
            Assert.That(list[0], Is.Not.Null);
            Assert.That(list[0], Is.EqualTo(n));
            Assert.That(list[0], Is.Not.EqualTo(a));
        }

        [Test]
        public void RemoveItem()
        {
            Assert.That(list.DeletedCollection.Count, Is.EqualTo(0));
            list.Remove(a);
            Assert.That(list.DeletedCollection.Count, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0], Is.Not.Null);
            Assert.That(list[0], Is.Not.EqualTo(a));
            Assert.That(list[0], Is.EqualTo(b));
        }

        [Test]
        public void RemoveItemAt()
        {
            Assert.That(list.DeletedCollection.Count, Is.EqualTo(0));
            list.RemoveAt(0);
            Assert.That(list.DeletedCollection.Count, Is.EqualTo(1));
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0], Is.Not.Null);
            Assert.That(list[0], Is.Not.EqualTo(a));
            Assert.That(list[0], Is.EqualTo(b));
        }

        [Test]
        public void RemoveItem_New()
        {
            string n = "N-String";
            list.Add(n);
            Assert.That(list.Count, Is.EqualTo(3));

            Assert.That(list.DeletedCollection.Count, Is.EqualTo(0));
            list.Remove(n);
            Assert.That(list.DeletedCollection.Count, Is.EqualTo(0));
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0], Is.Not.Null);
            Assert.That(list[0], Is.Not.EqualTo(n));
            Assert.That(list[0], Is.EqualTo(a));
        }

        [Test]
        public void RemoveItemAt_New()
        {
            string n = "N-String";
            list.Add(n);
            Assert.That(list.Count, Is.EqualTo(3));

            Assert.That(list.DeletedCollection.Count, Is.EqualTo(0));
            list.RemoveAt(2);
            Assert.That(list.DeletedCollection.Count, Is.EqualTo(0));
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0], Is.Not.Null);
            Assert.That(list[0], Is.Not.EqualTo(n));
            Assert.That(list[0], Is.EqualTo(a));
        }
    }
}
