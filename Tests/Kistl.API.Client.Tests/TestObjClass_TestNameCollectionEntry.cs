using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kistl.API.Client.Tests
{
    public class TestObjClass_TestNameCollectionEntry : BaseClientCollectionEntry, INewCollectionEntry<TestObjClass, string>
    {

        private int _ID;

        private string _Value;

        private int _fk_Parent;

        public override int RelationID { get { return -1; } }

       

        public string B
        {
            get
            {
                return _Value;
            }
            set
            {
                base.NotifyPropertyChanging("B", null, null);
                _Value = value;
                base.NotifyPropertyChanged("B", null, null); 
            }
        }

        [XmlIgnore()]
        public TestObjClass A
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
            BinarySerializer.ToStream(this.B, sw);
            BinarySerializer.ToStream(this.fk_Parent, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStream(out this._Value, sr);
            BinarySerializer.FromStream(out this._fk_Parent, sr);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            this._Value = ((TestObjClass_TestNameCollectionEntry)obj)._Value;
            this._fk_Parent = ((TestObjClass_TestNameCollectionEntry)obj)._fk_Parent;
        }

        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(INewCollectionEntry<string, TestObjClass>));
        }
    }
}
