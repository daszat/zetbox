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
    [EdmEntityType(NamespaceName="Model", Name="Muhblah")]
    [System.Diagnostics.DebuggerDisplay("Muhblah")]
    public class Muhblah__Implementation__ : BaseServerDataObject_EntityFramework, Muhblah
    {
    
		public Muhblah__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
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
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual bool? TestBool
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestBool;
                if (OnTestBool_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool?>(__result);
                    OnTestBool_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestBool != value)
                {
                    var __oldValue = _TestBool;
                    var __newValue = value;
                    if(OnTestBool_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnTestBool_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestBool", __oldValue, __newValue);
                    _TestBool = __newValue;
                    NotifyPropertyChanged("TestBool", __oldValue, __newValue);
                    if(OnTestBool_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnTestBool_PostSetter(this, __e);
                    }
                }
            }
        }
        private bool? _TestBool;
		public event PropertyGetterHandler<Kistl.App.Test.Muhblah, bool?> OnTestBool_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.Muhblah, bool?> OnTestBool_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.Muhblah, bool?> OnTestBool_PostSetter;
        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? TestDateTime
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestDateTime;
                if (OnTestDateTime_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnTestDateTime_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestDateTime != value)
                {
                    var __oldValue = _TestDateTime;
                    var __newValue = value;
                    if(OnTestDateTime_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnTestDateTime_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestDateTime", __oldValue, __newValue);
                    _TestDateTime = __newValue;
                    NotifyPropertyChanged("TestDateTime", __oldValue, __newValue);
                    if(OnTestDateTime_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnTestDateTime_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _TestDateTime;
		public event PropertyGetterHandler<Kistl.App.Test.Muhblah, DateTime?> OnTestDateTime_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.Muhblah, DateTime?> OnTestDateTime_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.Muhblah, DateTime?> OnTestDateTime_PostSetter;
        /// <summary>
        /// 
        /// </summary>
        // enumeration property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.EnumerationPropertyTemplate
        // implement the user-visible interface
        public Kistl.App.Test.TestEnum TestEnum
        {
            get
            {
				var __value = _TestEnum;
				if(OnTestEnum_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Test.TestEnum>(__value);
					OnTestEnum_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (_TestEnum != value)
                {
					var __oldValue = _TestEnum;
					var __newValue = value;
                    if(OnTestEnum_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<Kistl.App.Test.TestEnum>(__oldValue, __newValue);
						OnTestEnum_PreSetter(this, e);
						__newValue = e.Result;
                    }
					
                    NotifyPropertyChanging("TestEnum", "TestEnum__Implementation__", __oldValue, __newValue);
                    _TestEnum = value;
                    NotifyPropertyChanged("TestEnum", "TestEnum__Implementation__", __oldValue, __newValue);
                    if(OnTestEnum_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<Kistl.App.Test.TestEnum>(__oldValue, __newValue);
						OnTestEnum_PostSetter(this, e);
                    }
                    
                }
            }
        }
        
        /// <summary>backing store for TestEnum</summary>
        private Kistl.App.Test.TestEnum _TestEnum;
        
        /// <summary>EF sees only this property, for TestEnum</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int TestEnum__Implementation__
        {
            get
            {
                return (int)this.TestEnum;
            }
            set
            {
                this.TestEnum = (Kistl.App.Test.TestEnum)value;
            }
        }
        
		public event PropertyGetterHandler<Kistl.App.Test.Muhblah, Kistl.App.Test.TestEnum> OnTestEnum_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.Muhblah, Kistl.App.Test.TestEnum> OnTestEnum_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.Muhblah, Kistl.App.Test.TestEnum> OnTestEnum_PostSetter;
        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string TestString
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestString;
                if (OnTestString_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnTestString_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestString != value)
                {
                    var __oldValue = _TestString;
                    var __newValue = value;
                    if(OnTestString_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnTestString_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestString", __oldValue, __newValue);
                    _TestString = __newValue;
                    NotifyPropertyChanged("TestString", __oldValue, __newValue);
                    if(OnTestString_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnTestString_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _TestString;
		public event PropertyGetterHandler<Kistl.App.Test.Muhblah, string> OnTestString_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.Muhblah, string> OnTestString_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.Muhblah, string> OnTestString_PostSetter;
		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Muhblah));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Muhblah)obj;
			var otherImpl = (Muhblah__Implementation__)obj;
			var me = (Muhblah)this;

			me.TestBool = other.TestBool;
			me.TestDateTime = other.TestDateTime;
			me.TestEnum = other.TestEnum;
			me.TestString = other.TestString;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Muhblah")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Muhblah != null)
            {
                OnToString_Muhblah(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Muhblah> OnToString_Muhblah;

        [EventBasedMethod("OnPreSave_Muhblah")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Muhblah != null) OnPreSave_Muhblah(this);
        }
        public event ObjectEventHandler<Muhblah> OnPreSave_Muhblah;

        [EventBasedMethod("OnPostSave_Muhblah")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Muhblah != null) OnPostSave_Muhblah(this);
        }
        public event ObjectEventHandler<Muhblah> OnPostSave_Muhblah;

        [EventBasedMethod("OnCreated_Muhblah")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Muhblah != null) OnCreated_Muhblah(this);
        }
        public event ObjectEventHandler<Muhblah> OnCreated_Muhblah;

        [EventBasedMethod("OnDeleting_Muhblah")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Muhblah != null) OnDeleting_Muhblah(this);
        }
        public event ObjectEventHandler<Muhblah> OnDeleting_Muhblah;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "TestBool":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("9206e71e-85ea-4d74-85ea-59ee2484ed2a")).Constraints
						.Where(c => !c.IsValid(this, this.TestBool))
						.Select(c => c.GetErrorText(this, this.TestBool))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "TestDateTime":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("c5a66e0b-1fdb-45e4-b9e4-2ae4ee35a201")).Constraints
						.Where(c => !c.IsValid(this, this.TestDateTime))
						.Select(c => c.GetErrorText(this, this.TestDateTime))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "TestEnum":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("1a5484e4-4be0-4641-9c25-1aa30d1c0e7a")).Constraints
						.Where(c => !c.IsValid(this, this.TestEnum))
						.Select(c => c.GetErrorText(this, this.TestEnum))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "TestString":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("e9516350-fa66-426b-808a-bd8a5f432427")).Constraints
						.Where(c => !c.IsValid(this, this.TestString))
						.Select(c => c.GetErrorText(this, this.TestString))
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
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
            
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._TestBool, binStream);
            BinarySerializer.ToStream(this._TestDateTime, binStream);
            BinarySerializer.ToStream((int)((Muhblah)this).TestEnum, binStream);
            BinarySerializer.ToStream(this._TestString, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._TestBool, binStream);
            BinarySerializer.FromStream(out this._TestDateTime, binStream);
            BinarySerializer.FromStreamConverter(v => ((Muhblah)this).TestEnum = (Kistl.App.Test.TestEnum)v, binStream);
            BinarySerializer.FromStream(out this._TestString, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._TestBool, xml, "TestBool", "Kistl.App.Test");
            XmlStreamer.ToStream(this._TestDateTime, xml, "TestDateTime", "Kistl.App.Test");
            XmlStreamer.ToStream((int)this.TestEnum, xml, "TestEnum", "Kistl.App.Test");
            XmlStreamer.ToStream(this._TestString, xml, "TestString", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._TestBool, xml, "TestBool", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._TestDateTime, xml, "TestDateTime", "Kistl.App.Test");
            XmlStreamer.FromStreamConverter(v => ((Muhblah)this).TestEnum = (Kistl.App.Test.TestEnum)v, xml, "TestEnum", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._TestString, xml, "TestString", "Kistl.App.Test");
        }

#endregion

    }


}