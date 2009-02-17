using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Data.Objects.DataClasses;
using Kistl.API.Server;
using System.Xml.Serialization;
using Kistl.DALProvider.EF;

namespace Kistl.API.Server.Tests
{
    [EdmEntityTypeAttribute(NamespaceName = "Model", Name = "TestObjClass_TestNameCollectionEntry")]
    public class TestObjClass_TestNameCollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, INewCollectionEntry<TestObjClass, string>
    {

        private int _ID;

        private string _Value;

        private int _fk_Parent;

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
        public string B
        {
            get
            {
                return _Value;
            }
            set
            {
                base.NotifyPropertyChanging("B");
                _Value = value;
                base.NotifyPropertyChanged("B"); ;
            }
        }

        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass")]
        public TestObjClass__Implementation__ Parent__Implementation__
        {
            get
            {
                EntityReference<TestObjClass__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass__Implementation__>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                return r.Value;
            }
            set
            {
                EntityReference<TestObjClass__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass__Implementation__>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                r.Value = value;
            }
        }

        public TestObjClass A { get { return Parent__Implementation__; } set { Parent__Implementation__ = (TestObjClass__Implementation__)value; } }

        public int fk_Parent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && A != null)
                {
                    _fk_Parent = A.ID;
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
            BinarySerializer.ToStream(this.B, sw);
            BinarySerializer.ToStream(this.fk_Parent, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStream(out this._Value, sr);
            BinarySerializer.FromStream(out this._fk_Parent, sr);
        }

        /// <summary>
        /// returns the most specific implemented data object interface
        /// </summary>
        /// <returns></returns>
        public override Type GetInterfaceType()
        {
            return typeof(INewCollectionEntry<TestObjClass, string>);
        }
    }
}
