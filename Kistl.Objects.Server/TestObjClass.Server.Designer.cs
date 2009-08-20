// <autogenerated/>


namespace Kistl.App.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="TestObjClass")]
    [System.Diagnostics.DebuggerDisplay("TestObjClass")]
    public class TestObjClass__Implementation__ : BaseServerDataObject_EntityFramework, TestObjClass
    {
    
		public TestObjClass__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
				return _ID;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
					var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);

                }
            }
        }
        private int _ID;

        /// <summary>
        /// test
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual int? MyIntProperty
        {
            get
            {
				var __value = _MyIntProperty;
				if(OnMyIntProperty_Getter != null)
				{
					var e = new PropertyGetterEventArgs<int?>(__value);
					OnMyIntProperty_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MyIntProperty != value)
                {
					var __oldValue = _MyIntProperty;
					var __newValue = value;
                    if(OnMyIntProperty_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
						OnMyIntProperty_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("MyIntProperty", __oldValue, __newValue);
                    _MyIntProperty = __newValue;
                    NotifyPropertyChanged("MyIntProperty", __oldValue, __newValue);

                    if(OnMyIntProperty_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
						OnMyIntProperty_PostSetter(this, e);
                    }
                }
            }
        }
        private int? _MyIntProperty;
		public event PropertyGetterHandler<Kistl.App.Test.TestObjClass, int?> OnMyIntProperty_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, int?> OnMyIntProperty_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, int?> OnMyIntProperty_PostSetter;
        /// <summary>
        /// testtest
        /// </summary>
    /*
    Relation: FK_TestObjClass_has_Kunde
    A: ZeroOrMore TestObjClass as TestObjClass
    B: ZeroOrOne Kunde as ObjectProp
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Kunde ObjectProp
        {
            get
            {
                return ObjectProp__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                ObjectProp__Implementation__ = (Kistl.App.Projekte.Kunde__Implementation__)value;
            }
        }
        
        private int? _fk_ObjectProp;
        private Guid? _fk_guid_ObjectProp = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TestObjClass_has_Kunde", "ObjectProp")]
        public Kistl.App.Projekte.Kunde__Implementation__ ObjectProp__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Kunde__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Kunde__Implementation__>(
                        "Model.FK_TestObjClass_has_Kunde",
                        "ObjectProp");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnObjectProp_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Projekte.Kunde>(__value);
					OnObjectProp_Getter(this, e);
					__value = (Kistl.App.Projekte.Kunde__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Kunde__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Kunde__Implementation__>(
                        "Model.FK_TestObjClass_has_Kunde",
                        "ObjectProp");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Projekte.Kunde __oldValue = (Kistl.App.Projekte.Kunde)r.Value;
                Kistl.App.Projekte.Kunde __newValue = (Kistl.App.Projekte.Kunde)value;

                if(OnObjectProp_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Projekte.Kunde>(__oldValue, __newValue);
					OnObjectProp_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Projekte.Kunde__Implementation__)__newValue;
                if(OnObjectProp_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Projekte.Kunde>(__oldValue, __newValue);
					OnObjectProp_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Projekte.Kunde> OnObjectProp_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Projekte.Kunde> OnObjectProp_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Projekte.Kunde> OnObjectProp_PostSetter;
        /// <summary>
        /// String Property
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string StringProp
        {
            get
            {
				var __value = _StringProp;
				if(OnStringProp_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnStringProp_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_StringProp != value)
                {
					var __oldValue = _StringProp;
					var __newValue = value;
                    if(OnStringProp_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnStringProp_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("StringProp", __oldValue, __newValue);
                    _StringProp = __newValue;
                    NotifyPropertyChanged("StringProp", __oldValue, __newValue);

                    if(OnStringProp_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnStringProp_PostSetter(this, e);
                    }
                }
            }
        }
        private string _StringProp;
		public event PropertyGetterHandler<Kistl.App.Test.TestObjClass, string> OnStringProp_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, string> OnStringProp_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, string> OnStringProp_PostSetter;
        /// <summary>
        /// Test Enumeration Property
        /// </summary>
        // enumeration property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.EnumerationPropertyTemplate
        // implement the user-visible interface
        public Kistl.App.Test.TestEnum TestEnumProp
        {
            get
            {
				var __value = _TestEnumProp;
				if(OnTestEnumProp_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Test.TestEnum>(__value);
					OnTestEnumProp_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (_TestEnumProp != value)
                {
					var __oldValue = _TestEnumProp;
					var __newValue = value;
                    if(OnTestEnumProp_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<Kistl.App.Test.TestEnum>(__oldValue, __newValue);
						OnTestEnumProp_PreSetter(this, e);
						__newValue = e.Result;
                    }
					
                    NotifyPropertyChanging("TestEnumProp", "TestEnumProp__Implementation__", __oldValue, __newValue);
                    _TestEnumProp = value;
                    NotifyPropertyChanged("TestEnumProp", "TestEnumProp__Implementation__", __oldValue, __newValue);
                    if(OnTestEnumProp_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<Kistl.App.Test.TestEnum>(__oldValue, __newValue);
						OnTestEnumProp_PostSetter(this, e);
                    }
                    
                }
            }
        }
        
        /// <summary>backing store for TestEnumProp</summary>
        private Kistl.App.Test.TestEnum _TestEnumProp;
        
        /// <summary>EF sees only this property, for TestEnumProp</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int TestEnumProp__Implementation__
        {
            get
            {
                return (int)this.TestEnumProp;
            }
            set
            {
                this.TestEnumProp = (Kistl.App.Test.TestEnum)value;
            }
        }
        
		public event PropertyGetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Test.TestEnum> OnTestEnumProp_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Test.TestEnum> OnTestEnumProp_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Test.TestEnum> OnTestEnumProp_PostSetter;
        /// <summary>
        /// testmethod
        /// </summary>
		[EventBasedMethod("OnTestMethod_TestObjClass")]
		public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod) 
		{
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
				OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
			}
			else
			{
                throw new NotImplementedException("No handler registered on TestObjClass.TestMethod");
			}
        }
		public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
		public event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TestObjClass));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (TestObjClass)obj;
			var otherImpl = (TestObjClass__Implementation__)obj;
			var me = (TestObjClass)this;

			me.MyIntProperty = other.MyIntProperty;
			me.StringProp = other.StringProp;
			me.TestEnumProp = other.TestEnumProp;
			this._fk_ObjectProp = otherImpl._fk_ObjectProp;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_TestObjClass")]
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
        public event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        [EventBasedMethod("OnPreSave_TestObjClass")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        [EventBasedMethod("OnPostSave_TestObjClass")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;

        [EventBasedMethod("OnCreated_TestObjClass")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_TestObjClass != null) OnCreated_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnCreated_TestObjClass;

        [EventBasedMethod("OnDeleting_TestObjClass")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_TestObjClass != null) OnDeleting_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnDeleting_TestObjClass;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "MyIntProperty":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("29c0242b-cd1c-42b4-8ca0-be0a209afcbf")).Constraints
						.Where(c => !c.IsValid(this, this.MyIntProperty))
						.Select(c => c.GetErrorText(this, this.MyIntProperty))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ObjectProp":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("e93b3fc2-2fc9-4577-9a93-a51ed2a4190f")).Constraints
						.Where(c => !c.IsValid(this, this.ObjectProp))
						.Select(c => c.GetErrorText(this, this.ObjectProp))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "StringProp":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("c9a3769e-7a53-4e1d-b894-72dc1b4e9aea")).Constraints
						.Where(c => !c.IsValid(this, this.StringProp))
						.Select(c => c.GetErrorText(this, this.StringProp))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "TestEnumProp":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("89470dda-4ac6-4bb4-9221-d16f80f8d95a")).Constraints
						.Where(c => !c.IsValid(this, this.TestEnumProp))
						.Select(c => c.GetErrorText(this, this.TestEnumProp))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references

			if (_fk_guid_ObjectProp.HasValue)
				ObjectProp__Implementation__ = (Kistl.App.Projekte.Kunde__Implementation__)Context.FindPersistenceObject<Kistl.App.Projekte.Kunde>(_fk_guid_ObjectProp.Value);
			else if (_fk_ObjectProp.HasValue)
				ObjectProp__Implementation__ = (Kistl.App.Projekte.Kunde__Implementation__)Context.Find<Kistl.App.Projekte.Kunde>(_fk_ObjectProp.Value);
			else
				ObjectProp__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._MyIntProperty, binStream);
            BinarySerializer.ToStream(ObjectProp != null ? ObjectProp.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._StringProp, binStream);
            BinarySerializer.ToStream((int)((TestObjClass)this).TestEnumProp, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._MyIntProperty, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectProp, binStream);
            BinarySerializer.FromStream(out this._StringProp, binStream);
            BinarySerializer.FromStreamConverter(v => ((TestObjClass)this).TestEnumProp = (Kistl.App.Test.TestEnum)v, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._MyIntProperty, xml, "MyIntProperty", "Kistl.App.Test");
            XmlStreamer.ToStream(ObjectProp != null ? ObjectProp.ID : (int?)null, xml, "ObjectProp", "Kistl.App.Test");
            XmlStreamer.ToStream(this._StringProp, xml, "StringProp", "Kistl.App.Test");
            XmlStreamer.ToStream((int)this.TestEnumProp, xml, "TestEnumProp", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._MyIntProperty, xml, "MyIntProperty", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._fk_ObjectProp, xml, "ObjectProp", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._StringProp, xml, "StringProp", "Kistl.App.Test");
            XmlStreamer.FromStreamConverter(v => ((TestObjClass)this).TestEnumProp = (Kistl.App.Test.TestEnum)v, xml, "TestEnumProp", "Kistl.App.Test");
        }

#endregion

    }


}