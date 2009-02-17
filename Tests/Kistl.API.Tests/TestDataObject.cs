using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kistl.API;

namespace Kistl.API.Tests
{
    [Serializable]
    public class TestObj
    {
        public string TestField;
    }

    public interface TestDataObject : IDataObject
    {
        bool BoolProperty { get; set; }
        int IntProperty { get; set; }
        string StringProperty { get; set; }
    }

    [Serializable]
    public class TestDataObject__Implementation__ : IDataObject, ICloneable, INotifyPropertyChanged, TestDataObject
    {
        private int _ID;
        private string _StringProperty;
        private int _IntProperty;
        private bool _BoolProperty;

        public int ID { get { return _ID; } set { _ID = value; } }
        public string StringProperty { get { return _StringProperty; } set { _StringProperty = value; } }
        public int IntProperty { get { return _IntProperty; } set { _IntProperty = value; } }
        public bool BoolProperty { get { return _BoolProperty; } set { _BoolProperty = value; } }
        public DataObjectState ObjectState { get; set; }

        private int PrivateIntProperty { get; set; }

        public string TestField;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj is TestDataObject)
            {
                TestDataObject x = (TestDataObject)obj;
                return
                       this.BoolProperty == x.BoolProperty
                    && this.IntProperty == x.IntProperty
                    && this.StringProperty == x.StringProperty
                    && this.ID == x.ID;
            }
            else
            {
                return false;
            }
        }

        public void ToStream(System.IO.BinaryWriter sw)
        {
            BinarySerializer.ToStream(new SerializableType(this.GetType()), sw);
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream(StringProperty, sw);
            BinarySerializer.ToStream(IntProperty, sw);
            BinarySerializer.ToStream(BoolProperty, sw);
        }

        public void FromStream(System.IO.BinaryReader sr)
        {
            SerializableType type;
            BinarySerializer.FromStream(out type, sr);
            BinarySerializer.FromStream(out _ID, sr);
            BinarySerializer.FromStream(out _StringProperty, sr);
            BinarySerializer.FromStream(out _IntProperty, sr);
            BinarySerializer.FromStream(out _BoolProperty, sr);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public void CopyTo(IDataObject obj)
        {
            ((TestDataObject__Implementation__)obj).ID = this.ID;
            ((TestDataObject)obj).IntProperty = this.IntProperty;
            ((TestDataObject)obj).StringProperty = this.StringProperty;
            ((TestDataObject)obj).BoolProperty = this.BoolProperty;
        }

        public void NotifyChange()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }

        public void NotifyPostSave()
        {
        }

        public void NotifyPreSave()
        {
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void NotifyPropertyChanging(string property)
        {
        }

        private IKistlContext _context = null;
        public IKistlContext Context
        {
            get
            {
                return _context;
            }
        }
        public void AttachToContext(IKistlContext ctx)
        {
            _context = ctx;
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }


        public bool IsAttached
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadonly { get; private set; }

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public Type GetInterfaceType()
        {
            return typeof(TestDataObject);
        }
    }
}
