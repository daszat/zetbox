using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Mocks
{
    public class TestObject : IDataObject
    {
        public TestObject()
        {
            TestBackReference = new List<IDataObject>();
        }

        #region BackReference Properties

        public List<IDataObject> TestBackReference { get; set; }
        public readonly static BackReferenceProperty TestBackReferenceProperty
            = new BackReferenceProperty()
            {
                PropertyName = "TestBackReference",
            };

        #endregion

        #region Bool Properties

        public bool? TestBool { get; set; }
        public readonly static BoolProperty TestBoolProperty
            = new BoolProperty()
            {
                PropertyName = "TestBool",
                IsNullable = true
            };

        public bool TestBoolNotNull { get; set; }
        public readonly static BoolProperty TestBoolNotNullProperty
            = new BoolProperty()
            {
                PropertyName = "TestBoolNotNull",
                IsNullable = false
            };
        #endregion

        #region DateTime Properties

        public DateTime? TestDateTime { get; set; }
        public readonly static DateTimeProperty TestDateTimeProperty
            = new DateTimeProperty()
            {
                PropertyName = "TestDateTime",
                IsNullable = true
            };

        private DateTime _TestDateTimeNotNull = DateTime.Now;
        public DateTime TestDateTimeNotNull { get; set; }
        public readonly static DateTimeProperty TestDateTimeNotNullProperty
            = new DateTimeProperty()
            {
                PropertyName = "TestDateTimeNotNull",
                IsNullable = false
            };
        #endregion

        #region Int Properties

        public int? TestInt { get; set; }
        public readonly static IntProperty TestIntProperty
            = new IntProperty()
            {
                PropertyName = "TestInt",
                IsNullable = true
            };

        public int TestIntNotNull { get; set; }
        public readonly static IntProperty TestIntNotNullProperty
            = new IntProperty()
            {
                PropertyName = "TestIntNotNull",
                IsNullable = false
            };
        #endregion

        #region String Properties

        public string TestString { get; set; }
        public readonly static StringProperty TestStringProperty
            = new StringProperty()
            {
                PropertyName = "TestString",
                IsNullable = true
            };

        private string _TestStringNotNull = "";
        public string TestStringNotNull
        {
            get { return _TestStringNotNull; }
            set
            {
                // Actually, validation should be done by generated class
                if (value == null)
                    throw new NullReferenceException("TestStringNotNull may not be null");
                _TestStringNotNull = value;
            }
        }
        public readonly static StringProperty TestStringNotNullProperty
            = new StringProperty()
            {
                PropertyName = "TestStringNotNull",
                IsNullable = false
            };
        #endregion

        #region IDataObject Members

        public void AttachToContext(IKistlContext ctx)
        {
        }

        public IKistlContext Context { get; set; }

        public void CopyTo(IDataObject obj)
        {
            throw new NotImplementedException();
        }

        public void DetachFromContext(IKistlContext ctx)
        {
        }

        public void FromStream(IKistlContext ctx, System.IO.BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public int ID { get; set; }


        public void NotifyChange()
        {
            throw new NotImplementedException();
        }

        public void NotifyPostSave()
        {
            throw new NotImplementedException();
        }

        public void NotifyPreSave()
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanged(string property)
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanging(string property)
        {
            throw new NotImplementedException();
        }

        public DataObjectState ObjectState { get; set; }

        public void ToStream(System.IO.BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public ObjectType Type
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
