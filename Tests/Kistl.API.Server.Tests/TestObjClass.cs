using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kistl.API;
using Kistl.API.Server;
using System.Data.Objects.DataClasses;
using System.Xml.Serialization;

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.API.Server.Tests.TestObjClass), "B_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.API.Server.Tests.TestObjClass))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.API.Server.Tests.TestObjClass), "B_TestObjClass_TestNameCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.API.Server.Tests.TestObjClass_TestNameCollectionEntry))]

namespace Kistl.API.Server.Tests
{
    [EdmEntityTypeAttribute(NamespaceName = "Model", Name = "TestObjClass")]
    public class TestObjClass : BaseServerDataObject
    {

        private int _ID;

        private string _StringProp;

        private int _TestEnumProp;

        private int? _fk_BaseTestObjClass;

        public TestObjClass()
        {
        }

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
        public string StringProp
        {
            get
            {
                return _StringProp;
            }
            set
            {
                NotifyPropertyChanging("StringProp");
                _StringProp = value;
                NotifyPropertyChanged("StringProp"); ;
            }
        }

        [EdmScalarPropertyAttribute()]
        public int TestEnumProp
        {
            get
            {
                return _TestEnumProp;
            }
            set
            {
                NotifyPropertyChanging("TestEnumProp");
                _TestEnumProp = value;
                NotifyPropertyChanged("TestEnumProp"); ;
            }
        }

        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "B_TestObjClass_TestNameCollectionEntry")]
        public EntityCollection<TestObjClass_TestNameCollectionEntry> TestNames
        {
            get
            {
                EntityCollection<TestObjClass_TestNameCollectionEntry> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<TestObjClass_TestNameCollectionEntry>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "B_TestObjClass_TestNameCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load();
                return c;
            }
        }

        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestObjClass", "A_TestObjClass")]
        public TestObjClass BaseTestObjClass
        {
            get
            {
                EntityReference<TestObjClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass>("Model.FK_TestObjClass_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                return r.Value;
            }
            set
            {
                EntityReference<TestObjClass> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass>("Model.FK_TestObjClass_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                r.Value = value;
            }
        }

        public int? fk_BaseTestObjClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && BaseTestObjClass != null)
                {
                    _fk_BaseTestObjClass = BaseTestObjClass.ID;
                }
                return _fk_BaseTestObjClass;
            }
            set
            {
                _fk_BaseTestObjClass = value;
            }
        }

        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestObjClass", "B_TestObjClass")]
        public EntityCollection<TestObjClass> SubClasses
        {
            get
            {
                EntityCollection<TestObjClass> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<TestObjClass>("Model.FK_TestObjClass_TestObjClass", "B_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load();
                return c;
            }
        }

        public event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;

        public event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestObjClass != null)
            {
                OnToString_TestObjClass(this, e);
            }
            return e.Result;
        }

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }

        //public override object Clone()
        //{
        //    TestObjClass obj = new TestObjClass();
        //    CopyTo(obj);
        //    return obj;
        //}

        //public override void ApplyChanges(Kistl.API.IDataObject obj)
        //{
        //    base.ApplyChanges(obj);
        //    ((TestObjClass)obj)._StringProp = this._StringProp;
        //    ((TestObjClass)obj)._TestEnumProp = this._TestEnumProp;
        //}

        public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod)
        {
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            };
        }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._StringProp, sw);
            BinarySerializer.ToBinary(this._TestEnumProp, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._StringProp, sr);
            BinarySerializer.FromBinary(out this._TestEnumProp, sr);
        }

        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
    }

}
