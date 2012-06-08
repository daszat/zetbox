// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Zetbox.API.Mocks;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

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
            obj.AttachToContext(scope.Resolve<IZetboxContext>());
            Assert.That(Helper.IsFloatingObject(obj), Is.EqualTo(false));
        }

        [Test]
        public void IsPersistedObject()
        {
            Assert.That(Helper.IsPersistedObject(obj), Is.EqualTo(false));
            ((TestDataObjectImpl)obj).ID = 1;
            obj.AttachToContext(scope.Resolve<IZetboxContext>());
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
        public void FindSequences_should_not_invent_stuff(Type value)
        {
            Assert.That(value.FindSequences().ToArray(), Is.EquivalentTo(new Type[] { }));
        }

        [TestCase(typeof(object[]), typeof(IEnumerable))]
        [TestCase(typeof(string[]), typeof(IEnumerable))]
        [TestCase(typeof(IEnumerable), typeof(IEnumerable))]
        public void FindSequences_should_find_single_IEnumerable(Type value, Type expected)
        {
            Assert.That(value.FindSequences().ToArray(), Is.EquivalentTo(new Type[] { expected }));
        }

        [TestCase(null)]
        [TestCase(typeof(object))]
        [TestCase(typeof(string))]
        public void FindElementTypes_should_not_invent_stuff(Type value)
        {
            Assert.That(value.FindElementTypes().ToArray(), Is.EquivalentTo(new Type[] { }));
        }

        [TestCase(typeof(object[]), typeof(object))]
        [TestCase(typeof(string[]), typeof(object), Description = "this might run against common expectations, but string[] really doesn't implement IEnumerable<string>")]
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

        // see https://bugzilla.novell.com/show_bug.cgi?id=670331
#if !MONO
        [TestCase(typeof(IEnumerable<string>), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable) })]
        [TestCase(typeof(IList<string>), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable) })]
        [TestCase(typeof(TestSequence), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable<int>), typeof(IEnumerable) })]
        [TestCase(typeof(TestSequenceInheritance), new Type[] { typeof(IEnumerable<string>), typeof(IEnumerable<int>), typeof(IEnumerable) })]
        [TestCase(typeof(TestSequenceGeneric), new Type[] { typeof(IEnumerable<TestSequence>), typeof(IEnumerable) })]
        public void FindSequences_should_find_multiple_IEnumerable(Type value, Type[] expected)
        {
            Assert.That(value.FindSequences().OrderBy(t => t.ToString()).ToArray(),
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

        [TestCase("OneParameter", new[] { typeof(string) })]
        [TestCase("TwoParameters", new[] { typeof(string), typeof(string) })]
        [TestCase("OneParameterReturns", new[] { typeof(string) })]
        [TestCase("TwoParametersReturns", new[] { typeof(string), typeof(string) })]
        public void FindGenericMethod_Public(string methodName, Type[] typeArguments)
        {
            var result = typeof(FgmTestClass).FindGenericMethod(methodName, typeArguments, null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(methodName));
        }

        [TestCase("OneParameterPrivate", new[] { typeof(string) })]
        [TestCase("TwoParametersPrivate", new[] { typeof(string), typeof(string) })]
        [TestCase("OneParameterReturnsPrivate", new[] { typeof(string) })]
        [TestCase("TwoParametersReturnsPrivate", new[] { typeof(string), typeof(string) })]
        public void FindGenericMethod_Private(string methodName, Type[] typeArguments)
        {
            var result = typeof(FgmTestClass).FindGenericMethod(true, methodName, typeArguments, null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(methodName));
        }

        [TestCase("OneParameterExtension", new[] { typeof(string) })]
        public void FindGenericMethod_Extensions(string methodName, Type[] typeArguments)
        {
            var result = typeof(FgmTestClass).FindGenericMethod(methodName, typeArguments, null);
            Assert.That(result, Is.Null); // TODO: implement extension lookup in FindGenericMethod
        }
#endif
    }

    public class FgmTestClass
    {
        public void OneParameter<T>() { }
        public void TwoParameters<T1, T2>() { }

        public int OneParameterReturns<T>() { return 0; }
        public int TwoParametersReturns<T1, T2>() { return 0; }

        private void OneParameterPrivate<T>() { }
        private void TwoParametersPrivate<T1, T2>() { }

        private int OneParameterReturnsPrivate<T>() { return 0; }
        private int TwoParametersReturnsPrivate<T1, T2>() { return 0; }
    }

    public static class FgmExtensions
    {
        public static void OneParameterExtension<T>(this FgmTestClass self) { }
    }
}
