using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Client.Tests
{
    [TestFixture]
    public class BackReferenceCollectionTests
    {

        public class TestObject : IDataObject
        {
            public TestObject Parent { get; set; }
            public BackReferenceCollection<TestObject> Children { get; private set; }

            public TestObject()
            {
                Children = new BackReferenceCollection<TestObject>("Parent", this);
            }

            #region IDataObject Member

            public void NotifyChange()
            {
                throw new NotImplementedException();
            }

            public void NotifyPreSave()
            {
                throw new NotImplementedException();
            }

            public void NotifyPostSave()
            {
                throw new NotImplementedException();
            }

            public void CopyTo(IDataObject obj)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IPersistenceObject Member

            public int ID { get; set; }

            public DataObjectState ObjectState
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public void ToStream(System.IO.BinaryWriter sw)
            {
                throw new NotImplementedException();
            }

            public void FromStream(System.IO.BinaryReader sr)
            {
                throw new NotImplementedException();
            }

            public void NotifyPropertyChanging(string property)
            {
                throw new NotImplementedException();
            }

            public void NotifyPropertyChanged(string property)
            {
                throw new NotImplementedException();
            }

            public IKistlContext Context
            {
                get { throw new NotImplementedException(); }
            }

            public void AttachToContext(IKistlContext ctx)
            {
                throw new NotImplementedException();
            }

            public void DetachFromContext(IKistlContext ctx)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region INotifyPropertyChanged Member

            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

            #endregion

            #region ICloneable Member

            public object Clone()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        [Test]
        public void AddRemoveTest() {
            TestObject obj = new TestObject();
            TestNewTestObject(obj);

            TestObject child1 = new TestObject();
            TestNewTestObject(child1);

            obj.Children.Add(child1);

            Assert.AreSame(obj, child1.Parent, "Adding a child should set Parent to the owner of the Children Collection");
            Assert.AreEqual(1, obj.Children.Count, "After adding one child, the collection should contain only one element");
            Assert.IsTrue(obj.Children.Contains(child1), "The child should be contained in the Collection");

            obj.Children.Remove(child1);

            Assert.IsNull(child1.Parent, "Removing the child should unset Parent Property");
            Assert.AreEqual(0, obj.Children.Count, "After Removing the only child, the collection should be empty");
            Assert.IsFalse(obj.Children.Contains(child1), "After Removing the only child, the collection should be empty");

        }

        private static void TestNewTestObject(TestObject obj)
        {
            Assert.IsNull(obj.Parent, "new Object should have no parent");
            Assert.IsNotNull(obj.Children, "Should create Children Collection");
            Assert.AreEqual(0, obj.Children.Count, "Collection should be empty");
        }

    }
}
