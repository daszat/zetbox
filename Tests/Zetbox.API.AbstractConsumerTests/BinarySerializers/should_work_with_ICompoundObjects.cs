using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Zetbox.API.AbstractConsumerTests.BinarySerializers
{
    class MinimalCompoundObjectTest
        : ICompoundObject
    {
        #region ICompoundObject Members

        public void AttachToObject(IPersistenceObject obj, string property)
        {
            throw new NotImplementedException();
        }

        public void DetachFromObject(IPersistenceObject obj, string property)
        {
            throw new NotImplementedException();
        }

        public bool IsReadonly
        {
            get { throw new NotImplementedException(); }
        }
        public AccessRights CurrentAccessRights
        {
            get { throw new NotImplementedException(); }
        }
        public virtual Guid CompoundObjectID { get { return Guid.Empty; } }
        #endregion

        #region IStreamable Members

        public void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {

        }

        public IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            return null;
        }

        public virtual void ToStream(System.Xml.XmlWriter xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
        }

        public IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            if (xml == null) throw new ArgumentNullException("xml");
            return null;
        }

        public void ReloadReferences()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Notify
        public virtual void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(property));

            if (PropertyChangingWithValue != null)
                PropertyChangingWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        public virtual void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            if (PropertyChangedWithValue != null)
                PropertyChangedWithValue(this, new PropertyChangeWithValueEventArgs(property, oldValue, newValue));
        }

        #endregion

        #region INotifyPropertyChang* Members

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangeWithValueEventHandler PropertyChangedWithValue;
        public event PropertyChangeWithValueEventHandler PropertyChangingWithValue;

        #endregion

        public void ApplyChangesFrom(ICompoundObject other)
        {
            throw new NotImplementedException();
        }

        #region IComparable
        int System.IComparable.CompareTo(object other)
        {
            if (other == null) return 1;
            var aStr = this.ToString();
            var bStr = other.ToString();
            if (aStr == null && bStr == null) return 0;
            if (aStr == null) return -1;
            return aStr.CompareTo(bStr);
        }
        #endregion
    }

    [TestFixture(typeof(MinimalCompoundObjectTest))]
    public abstract class should_work_with_ICompoundObjects<T> : SerializerTestFixture
        where T : class, ICompoundObject, new()
    {
        T test;

        public override void SetUp()
        {
            base.SetUp();
            test = new T();
        }

        /// <summary>
        /// Rewinds all streams to their start
        /// </summary>
        private void RewindStreams()
        {
            ms.Seek(0, SeekOrigin.Begin);
        }

        [Test]
        public void when_serializing()
        {
            sw.Write(test);
        }

        [Test]
        public void when_deserializing()
        {
            sw.Write(test);

            RewindStreams();

            Assert.DoesNotThrow(() =>
            {
                test = sr.ReadCompoundObject<T>();
            });
        }
    }
}