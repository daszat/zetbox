using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using System.Xml.Serialization;

namespace Kistl.API.Client.Tests
{
    public class TestObjClass_TestNameCollectionEntry : BaseClientCollectionEntry, ICollectionEntry<string, TestObjClass>
    {

        private int _ID;

        private string _Value;

        private int _fk_Parent;

       

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                base.NotifyPropertyChanging("Value");
                _Value = value;
                base.NotifyPropertyChanged("Value"); ;
            }
        }

        [XmlIgnore()]
        public TestObjClass Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public int fk_Parent
        {
            get
            {
                return _fk_Parent;
            }
            set
            {
                _fk_Parent = value;
            }
        }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToStream(this.Value, sw);
            BinarySerializer.ToStream(this.fk_Parent, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStream(out this._Value, sr);
            BinarySerializer.FromStream(out this._fk_Parent, sr);
        }

        public override void ApplyChanges(Kistl.API.ICollectionEntry obj)
        {
            base.ApplyChanges(obj);
            ((TestObjClass_TestNameCollectionEntry)obj)._Value = this._Value;
            ((TestObjClass_TestNameCollectionEntry)obj)._fk_Parent = this._fk_Parent;
        }

        public override Type GetInterfaceType()
        {
            return typeof(ICollectionEntry<string, TestObjClass>);
        }
    }
}
