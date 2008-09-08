using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kistl.API;
using Kistl.API.Server;
using System.Data.Objects.DataClasses;
using System.Xml.Serialization;

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.API.Server.Tests.TestObjClassImpl), "B_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.API.Server.Tests.TestObjClassImpl))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.API.Server.Tests.TestObjClassImpl), "B_TestObjClass_TestNameCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.API.Server.Tests.TestObjClass_TestNameCollectionEntryImpl))]

namespace Kistl.API.Server.Tests
{
    [EdmEntityTypeAttribute(NamespaceName = "Model", Name = "TestObjClass")]
    public class TestObjClassImpl : BaseServerDataObject, Kistl.API.Server.Tests.TestObjClass
    {

        private int _ID;

        private string _StringProp;

        private int _TestEnumProp;

        private int? _fk_BaseTestObjClass;

        public TestObjClassImpl()
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
        public EntityCollection<TestObjClass_TestNameCollectionEntryImpl> TestNamesImpl
        {
            get
            {
                EntityCollection<TestObjClass_TestNameCollectionEntryImpl> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<TestObjClass_TestNameCollectionEntryImpl>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "B_TestObjClass_TestNameCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load();
                return c;
            }
        }

        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestObjClass", "A_TestObjClass")]
        public TestObjClassImpl BaseTestObjClassImpl
        {
            get
            {
                EntityReference<TestObjClassImpl> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClassImpl>("Model.FK_TestObjClass_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                return r.Value;
            }
            set
            {
                EntityReference<TestObjClassImpl> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClassImpl>("Model.FK_TestObjClass_TestObjClass", "A_TestObjClass");
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
        public EntityCollection<TestObjClassImpl> SubClassesImpl
        {
            get
            {
                EntityCollection<TestObjClassImpl> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<TestObjClassImpl>("Model.FK_TestObjClass_TestObjClass", "B_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load();
                return c;
            }
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            TestNamesImpl.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i));
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

        #region TestObjClass Members

        public TestObjClass BaseTestObjClass
        {
            get
            {
                return BaseTestObjClassImpl;
            }
            set
            {
                BaseTestObjClassImpl = (TestObjClassImpl)value;
            }
        }

        private EntityCollectionWrapper<TestObjClass, TestObjClassImpl> _SubClasses;
        public ICollection<TestObjClass> SubClasses
        {
            get 
            {
                if (_SubClasses == null) _SubClasses = new EntityCollectionWrapper<TestObjClass, TestObjClassImpl>(SubClassesImpl);
                return _SubClasses;
            }
        }

        private EntityCollectionEntryValueWrapper<TestObjClass, string, TestObjClass_TestNameCollectionEntryImpl> _TestNames;
        public IList<string> TestNames
        {
            get 
            {
                if (_TestNames == null) _TestNames = new EntityCollectionEntryValueWrapper<TestObjClass, string, TestObjClass_TestNameCollectionEntryImpl>(this, TestNamesImpl);
                return _TestNames;
            }
        }

        #endregion
    }

}
