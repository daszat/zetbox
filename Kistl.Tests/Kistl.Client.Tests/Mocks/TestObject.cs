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
        #endregion

        #region DateTime Properties

        public DateTime? TestDateTime { get; set; }
        public readonly static DateTimeProperty TestDateTimeProperty
            = new DateTimeProperty()
            {
                PropertyName = "TestDateTime",
                IsNullable = true
            };
        #endregion

        #region Double Properties

        public double? TestDouble { get; set; }
        public readonly static DoubleProperty TestDoubleProperty
            = new DoubleProperty()
            {
                PropertyName = "TestDouble",
                IsNullable = true
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

        #endregion

        #region String Properties

        public string TestString { get; set; }
        public readonly static StringProperty TestStringProperty
            = new StringProperty()
            {
                PropertyName = "TestString",
                IsNullable = true
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
