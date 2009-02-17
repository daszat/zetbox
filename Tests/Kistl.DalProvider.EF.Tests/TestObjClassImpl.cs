using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.API.Server;
using Kistl.DALProvider.EF;

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.API.Server.Tests.TestObjClass__Implementation__), "B_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.API.Server.Tests.TestObjClass__Implementation__))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "A_TestObjClass", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.API.Server.Tests.TestObjClass__Implementation__), "B_TestObjClass_TestNameCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.API.Server.Tests.TestObjClass_TestNameCollectionEntry__Implementation__))]

namespace Kistl.API.Server.Tests
{
    [EdmEntityTypeAttribute(NamespaceName = "Model", Name = "TestObjClass")]
    public class TestObjClass__Implementation__ : BaseServerDataObject_EntityFramework, Kistl.API.Server.Tests.TestObjClass
    {

        private int _ID;

        private string _StringProp;

        private int _TestEnumProp;

        private int? _fk_BaseTestObjClass;

        public TestObjClass__Implementation__()
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
        public EntityCollection<TestObjClass_TestNameCollectionEntry__Implementation__> TestNames__Implementation__
        {
            get
            {
                EntityCollection<TestObjClass_TestNameCollectionEntry__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<TestObjClass_TestNameCollectionEntry__Implementation__>("Model.FK_TestObjClass_TestNameCollectionEntry_TestObjClass", "B_TestObjClass_TestNameCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load();
                return c;
            }
        }

        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_TestObjClass_TestObjClass", "A_TestObjClass")]
        public TestObjClass__Implementation__ BaseTestObjClass__Implementation__
        {
            get
            {
                EntityReference<TestObjClass__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass__Implementation__>("Model.FK_TestObjClass_TestObjClass", "A_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load();
                return r.Value;
            }
            set
            {
                EntityReference<TestObjClass__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<TestObjClass__Implementation__>("Model.FK_TestObjClass_TestObjClass", "A_TestObjClass");
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
        public EntityCollection<TestObjClass__Implementation__> SubClasses__Implementation__
        {
            get
            {
                EntityCollection<TestObjClass__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<TestObjClass__Implementation__>("Model.FK_TestObjClass_TestObjClass", "B_TestObjClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load();
                return c;
            }
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            TestNames__Implementation__.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i));
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
            BinarySerializer.ToStream(this.fk_BaseTestObjClass, sw);
            BinarySerializer.ToStream(this._StringProp, sw);
            BinarySerializer.ToStream(this._TestEnumProp, sw);
            BinarySerializer.ToStreamCollectionEntries(this.TestNames__Implementation__, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStream(out this._fk_BaseTestObjClass, sr);
            BinarySerializer.FromStream(out this._StringProp, sr);
            BinarySerializer.FromStream(out this._TestEnumProp, sr);
            BinarySerializer.FromStreamCollectionEntries(this.TestNames__Implementation__, sr);
        }

        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);

        #region TestObjClass Members

        public TestObjClass BaseTestObjClass
        {
            get
            {
                return BaseTestObjClass__Implementation__;
            }
            set
            {
                BaseTestObjClass__Implementation__ = (TestObjClass__Implementation__)value;
                if (value == null)
                {
                    _fk_BaseTestObjClass = null;
                }
                else
                {
                    _fk_BaseTestObjClass = value.ID;
                }
            }
        }

        private EntityCollectionWrapper<TestObjClass, TestObjClass__Implementation__> _SubClasses;
        public ICollection<TestObjClass> SubClasses
        {
            get 
            {
                if (_SubClasses == null) _SubClasses = new EntityCollectionWrapper<TestObjClass, TestObjClass__Implementation__>(SubClasses__Implementation__);
                return _SubClasses;
            }
        }

        private EntityCollectionBSideWrapper<TestObjClass, string, TestObjClass_TestNameCollectionEntry__Implementation__> _TestNames;
        public ICollection<string> TestNames
        {
            get 
            {
                if (_TestNames == null) _TestNames = new EntityCollectionBSideWrapper<TestObjClass, string, TestObjClass_TestNameCollectionEntry__Implementation__>(this, TestNames__Implementation__);
                return _TestNames;
            }
        }

        #endregion

        public override Type GetInterfaceType()
        {
            return typeof(Kistl.API.Server.Tests.TestObjClass);
        }
    }

}
