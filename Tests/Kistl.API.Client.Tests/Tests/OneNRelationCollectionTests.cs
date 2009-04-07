using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Client.Tests
{
    [TestFixture]
    public class OneNRelationCollectionTests
    {

        public class TestObject : BaseClientDataObject, IDataObject
        {
            public static Dictionary<int, TestObject> Instances = new Dictionary<int, TestObject>();
            private static int MaxId = Helper.INVALIDID;


            public TestObject Parent { get; set; }
            public int? fk_Parent
            {
                get { return Parent == null ? (int?)null : Parent.ID; }
                private set
                {
                    if (value.HasValue)
                        Parent = TestObject.Instances[value.Value];
                    else
                        Parent = null;
                }
            }

            public OneNRelationCollection<TestObject> Children { get; private set; }

            public TestObject()
            {
                this.ID = ++MaxId;
                TestObject.Instances[this.ID] = this;
                Children = new OneNRelationCollection<TestObject>("Parent", this);
            }

            public override InterfaceType GetInterfaceType()
            {
                throw new NotImplementedException();
            }

            public override void UpdateParent(string propertyName, int? id)
            {
                switch (propertyName)
                {
                    case "Parent":
                        fk_Parent = id;
                        break;
                    default:
                        base.UpdateParent(propertyName, id);
                        break;
                }
            }
        }

        [Test]
        public void AddRemoveTest()
        {
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
