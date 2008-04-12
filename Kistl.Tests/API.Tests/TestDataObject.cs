using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kistl.API;

namespace API.Tests
{
    [Serializable]
    public class TestDataObject : IDataObject, ICloneable, INotifyPropertyChanged
    {
        private int _ID;
        private string _StringProperty;
        private int _IntProperty;
        private bool _BoolProperty;
        private ObjectType _Type = new ObjectType(typeof(TestDataObject));

        public int ID { get { return _ID; } set { _ID = value; } }
        public string StringProperty { get { return _StringProperty; } set { _StringProperty = value; } }
        public int IntProperty { get { return _IntProperty; } set { _IntProperty = value; } }
        public bool BoolProperty { get { return _BoolProperty; } set { _BoolProperty = value; } }
        public ObjectType Type { get { return _Type; } }
        public DataObjectState ObjectState { get; set; }

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
            BinarySerializer.ToBinary(Type, sw);
            BinarySerializer.ToBinary(ID, sw);
            BinarySerializer.ToBinary(StringProperty, sw);
            BinarySerializer.ToBinary(IntProperty, sw);
            BinarySerializer.ToBinary(BoolProperty, sw);
        }

        public void FromStream(IKistlContext ctx, System.IO.BinaryReader sr)
        {
            BinarySerializer.FromBinary(out _Type, sr);
            BinarySerializer.FromBinary(out _ID, sr);
            BinarySerializer.FromBinary(out _StringProperty, sr);
            BinarySerializer.FromBinary(out _IntProperty, sr);
            BinarySerializer.FromBinary(out _BoolProperty, sr);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyTo(IDataObject obj)
        {
            ((TestDataObject)obj).ID = this.ID;
            ((TestDataObject)obj)._Type = this.Type;
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
    }
}
