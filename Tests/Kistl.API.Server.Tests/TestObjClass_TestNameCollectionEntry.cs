using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Data.Objects.DataClasses;
using Kistl.API.Server;
using System.Xml.Serialization;

namespace API.Server.Tests
{
    [EdmEntityTypeAttribute(NamespaceName = "Model", Name = "TestObjClass_TestNameCollectionEntry")]
    public class TestObjClass_TestNameCollectionEntry : BaseServerCollectionEntry
    {

        private int _ID = Helper.INVALIDID;

        private string _Value;

        private int _fk_Parent = Helper.INVALIDID;

        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        [EdmScalarPropertyAttribute()]
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
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass")]
        public TestObjClass Parent
        {
            get
            {
                EntityReference<TestObjClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                return r.Value;
            }
            set
            {
                EntityReference<TestObjClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                r.Value = value;
            }
        }

        public int fk_Parent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Parent == Helper.INVALIDID && Parent != null)
                {
                    _fk_Parent = Parent.ID;
                }
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
            BinarySerializer.ToBinary(this.Value, sw);
            BinarySerializer.ToBinary(this.fk_Parent, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Value, sr);
            BinarySerializer.FromBinary(out this._fk_Parent, sr);
        }

        public override void CopyTo(Kistl.API.ICollectionEntry obj)
        {
            base.CopyTo(obj);
            ((TestObjClass_TestNameCollectionEntry)obj)._Value = this._Value;
            ((TestObjClass_TestNameCollectionEntry)obj)._fk_Parent = this._fk_Parent;
        }
    }
}
