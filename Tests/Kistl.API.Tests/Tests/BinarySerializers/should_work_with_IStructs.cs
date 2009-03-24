using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;

namespace Kistl.API.Tests.BinarySerializers
{
    class MinimalStructTest : IStruct
    {
        #region IStruct Members

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

        #endregion

        #region IStreamable Members

        public void ToStream(BinaryWriter sw)
        {

        }

        public void FromStream(BinaryReader sr)
        {
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

        #region INotifyPropertyChang* Members

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion
    }

    [TestFixture(typeof(MinimalStructTest))]
    [TestFixture(typeof(TestStruct__Implementation__))]
    public class should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
        MemoryStream ms;
        BinaryWriter sw;
        BinaryReader sr;

        T test;

        [SetUp]
        public void SetUp()
        {
            ms = new MemoryStream();
            sw = new BinaryWriter(ms);
            sr = new BinaryReader(ms);
            var testCtx = new TestApplicationContext();

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
            BinarySerializer.ToStream(test, sw);
        }

        [Test]
        public void when_deserializing()
        {
            BinarySerializer.ToStream(test, sw);

            RewindStreams();

            Assert.DoesNotThrow(() =>
            {
                BinarySerializer.FromStream(out test, sr);
            });
        }

    }
}
