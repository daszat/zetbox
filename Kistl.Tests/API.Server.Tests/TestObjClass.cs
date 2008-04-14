using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kistl.API;
using Kistl.API.Server;
using System.Data.Objects.DataClasses;

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(API.Server.Tests.TestObjClass), "B_TestObjClass_TestNameCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(API.Server.Tests.TestObjClass_TestNameCollectionEntry))]

namespace API.Server.Tests
{
    [EdmEntityTypeAttribute(NamespaceName = "Model", Name = "TestObjClass")]
    public class TestObjClass : BaseServerDataObject, ICloneable
    {

        private int _ID = Helper.INVALIDID;

        private string _StringProp;

        private int _TestEnumProp;

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

        public override object Clone()
        {
            TestObjClass obj = new TestObjClass();
            CopyTo(obj);
            return obj;
        }

        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((TestObjClass)obj)._StringProp = this._StringProp;
            ((TestObjClass)obj)._TestEnumProp = this._TestEnumProp;
        }

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

        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
            BinarySerializer.FromBinary(out this._StringProp, sr);
            BinarySerializer.FromBinary(out this._TestEnumProp, sr);
        }

        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
    }

}
