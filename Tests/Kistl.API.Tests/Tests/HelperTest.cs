using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Autofac;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class HelperTest : AbstractApiTestFixture
    {
        TestDataObject obj;

        public override void SetUp()
        {
            base.SetUp();
            obj = new TestDataObjectImpl() { BoolProperty = true, IntProperty = 1, StringProperty = "test" };
        }

        [Test]
        public void IsFloatingObjectTest()
        {
            Assert.That(Helper.IsFloatingObject(obj), Is.EqualTo(true));
            ((TestDataObjectImpl)obj).ID = 1;
            obj.AttachToContext(scope.Resolve<IKistlContext>());
            Assert.That(Helper.IsFloatingObject(obj), Is.EqualTo(false));
        }

        [Test]
        public void IsPersistedObject()
        {
            Assert.That(Helper.IsPersistedObject(obj), Is.EqualTo(false));
            ((TestDataObjectImpl)obj).ID = 1;
            obj.AttachToContext(scope.Resolve<IKistlContext>());
            Assert.That(Helper.IsPersistedObject(obj), Is.EqualTo(true));
        }

        //[TestCase(null, null)]
        //[TestCase(typeof(object), null)]
        //[TestCase(typeof(string), null)]
        //[TestCase(typeof(object[]), typeof(IEnumerable))]
        //[TestCase(typeof(string[]), typeof(IEnumerable))]
        //[TestCase(typeof(IEnumerable), typeof(IEnumerable))]
        //[TestCase(typeof(IEnumerable<string>), typeof(IEnumerable<string>))]
        //[TestCase(typeof(IList<string>), typeof(IEnumerable<string>))]
        //public void FindIEnumerable_should_find_IEnumerable(Type value, Type expected)
        //{
        //    Assert.That(value.FindIEnumerable(), Is.EqualTo(expected));
        //}




        [TestCase(null)]
        [TestCase(typeof(object))]
        [TestCase(typeof(string))]
        public void FindIEnumerables_should_not_invent_stuff(Type value)
        {
            Assert.That(value.FindIEnumerables().ToArray(), Is.EquivalentTo(new Type[] { }));
        }

        [TestCase(typeof(object[]), typeof(IEnumerable))]
        [TestCase(typeof(string[]), typeof(IEnumerable))]
        [TestCase(typeof(IEnumerable), typeof(IEnumerable))]
        public void FindIEnumerables_should_find_single_IEnumerable(Type value, Type expected)
        {
            Assert.That(value.FindIEnumerables().ToArray(), Is.EquivalentTo(new Type[] { expected }));
        }

        [TestCase(null)]
        [TestCase(typeof(object))]
        [TestCase(typeof(string))]
        public void FindElementTypes_should_not_invent_stuff(Type value)
        {
            Assert.That(value.FindElementTypes().ToArray(), Is.EquivalentTo(new Type[] { }));
        }

        [TestCase(typeof(object[]), typeof(object))]
        [TestCase(typeof(string[]), typeof(object), Description="this might run against common expectations, but string[] really doesn't implement IEnumerable<string>")]
        [TestCase(typeof(IEnumerable), typeof(object))]
        public void FindElementTypes_should_find_single_IEnumerable(Type value, Type expected)
        {
            Assert.That(value.FindElementTypes().ToArray(), Is.EquivalentTo(new Type[] { expected }));
        }

        #region TestSequences

        public abstract class TestSequence : IList<string>, IList<int>
        {
            #region IList<string> Members

            public int IndexOf(string item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, string item)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            public string this[int index]
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

            #endregion

            #region ICollection<string> Members

            public void Add(string item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(string item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(string[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsReadOnly
            {
                get { throw new NotImplementedException(); }
            }

            public bool Remove(string item)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable<string> Members

            public IEnumerator<string> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IList<int> Members

            public int IndexOf(int item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, int item)
            {
                throw new NotImplementedException();
            }

            int IList<int>.this[int index]
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

            #endregion

            #region ICollection<int> Members

            public void Add(int item)
            {
                throw new NotImplementedException();
            }

            public bool Contains(int item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(int[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public bool Remove(int item)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable<int> Members

            IEnumerator<int> IEnumerable<int>.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public abstract class TestSequenceInheritance : TestSequence { }

        public abstract class TestSequenceGeneric : IList<TestSequence>
        {
            #region IList<TestSequence> Members

            public int IndexOf(TestSequence item)
            {
                throw new NotImplementedException();
            }

            public void Insert(int index, TestSequence item)
            {
                throw new NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotImplementedException();
            }

            public TestSequence this[int index]
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

            #endregion

            #region ICollection<TestSequence> Members

            public void Add(TestSequence item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(TestSequence item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(TestSequence[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public int Count
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsReadOnly
            {
                get { throw new NotImplementedException(); }
            }

            public bool Remove(TestSequence item)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable<TestSequence> Members

            public IEnumerator<TestSequence> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion

        [TestCase(typeof(IEnumerable<string>), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable) })]
        [TestCase(typeof(IList<string>), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable) })]
        [TestCase(typeof(TestSequence), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable<int>), typeof(IEnumerable) })]
        [TestCase(typeof(TestSequenceInheritance), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable<int>), typeof(IEnumerable) })]
        [TestCase(typeof(TestSequenceGeneric), new Type[] { typeof(IEnumerable<TestSequence>), typeof(IEnumerable) })]
        public void FindIEnumerables_should_find_multiple_IEnumerable(Type value, Type[] expected)
        {
            Assert.That(value.FindIEnumerables().OrderBy(t => t.ToString()).ToArray(),
                Is.EquivalentTo(expected.OrderBy(t => t.ToString()).ToArray()));
        }

        [TestCase(typeof(IEnumerable<string>), new Type[] { typeof(string), typeof(object) })]
        [TestCase(typeof(IList<string>), new Type[] { typeof(string), typeof(object) })]
        [TestCase(typeof(TestSequence), new Type[] { typeof(string), typeof(int), typeof(object) })]
        [TestCase(typeof(TestSequenceInheritance), new Type[] { typeof(string), typeof(int), typeof(object) })]
        [TestCase(typeof(TestSequenceGeneric), new Type[] { typeof(TestSequence), typeof(object) })]
        public void FindElementTypes_should_find_multiple_IEnumerable(Type value, Type[] expected)
        {
            Assert.That(value.FindElementTypes().OrderBy(t => t.ToString()).ToArray(),
                Is.EquivalentTo(expected.OrderBy(t => t.ToString()).ToArray()));
        }


    }
}
