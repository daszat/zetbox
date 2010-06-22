// <autogenerated/>


namespace Kistl.App.TimeRecords
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
    using Kistl.DalProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="PresenceRecord")]
    [System.Diagnostics.DebuggerDisplay("PresenceRecord")]
    public class PresenceRecord__Implementation__ : BaseServerDataObject_EntityFramework, PresenceRecord
    {
        [Obsolete]
        public PresenceRecord__Implementation__()
            : base(null)
        {
        }

        public PresenceRecord__Implementation__(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.IdProperty
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
        /// Point in time when the presence started.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime From
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _From;
                if (OnFrom_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime>(__result);
                    OnFrom_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_From != value)
                {
                    var __oldValue = _From;
                    var __newValue = value;
                    if(OnFrom_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime>(__oldValue, __newValue);
                        OnFrom_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("From", __oldValue, __newValue);
                    _From = __newValue;
                    NotifyPropertyChanged("From", __oldValue, __newValue);
                    if(OnFrom_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime>(__oldValue, __newValue);
                        OnFrom_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime _From;
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.PresenceRecord, DateTime> OnFrom_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.PresenceRecord, DateTime> OnFrom_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.PresenceRecord, DateTime> OnFrom_PostSetter;

        /// <summary>
        /// Which employee was present.
        /// </summary>
    /*
    Relation: FK_PresenceRecord_has_Mitarbeiter
    A: ZeroOrMore PresenceRecord as PresenceRecord
    B: One Mitarbeiter as Mitarbeiter
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Mitarbeiter Mitarbeiter
        {
            get
            {
                return Mitarbeiter__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        private int? _fk_Mitarbeiter;
        private Guid? _fk_guid_Mitarbeiter = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresenceRecord_has_Mitarbeiter", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ Mitarbeiter__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_PresenceRecord_has_Mitarbeiter",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnMitarbeiter_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Projekte.Mitarbeiter>(__value);
					OnMitarbeiter_Getter(this, e);
					__value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_PresenceRecord_has_Mitarbeiter",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Projekte.Mitarbeiter __oldValue = (Kistl.App.Projekte.Mitarbeiter)r.Value;
                Kistl.App.Projekte.Mitarbeiter __newValue = (Kistl.App.Projekte.Mitarbeiter)value;

                if(OnMitarbeiter_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Projekte.Mitarbeiter>(__oldValue, __newValue);
					OnMitarbeiter_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)__newValue;
                if(OnMitarbeiter_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Projekte.Mitarbeiter>(__oldValue, __newValue);
					OnMitarbeiter_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.PresenceRecord, Kistl.App.Projekte.Mitarbeiter> OnMitarbeiter_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.PresenceRecord, Kistl.App.Projekte.Mitarbeiter> OnMitarbeiter_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.PresenceRecord, Kistl.App.Projekte.Mitarbeiter> OnMitarbeiter_PostSetter;

        /// <summary>
        /// Point in time (inclusive) when the presence ended.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? Thru
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Thru;
                if (OnThru_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnThru_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Thru != value)
                {
                    var __oldValue = _Thru;
                    var __newValue = value;
                    if(OnThru_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnThru_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Thru", __oldValue, __newValue);
                    _Thru = __newValue;
                    NotifyPropertyChanged("Thru", __oldValue, __newValue);
                    if(OnThru_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnThru_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _Thru;
		public static event PropertyGetterHandler<Kistl.App.TimeRecords.PresenceRecord, DateTime?> OnThru_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.TimeRecords.PresenceRecord, DateTime?> OnThru_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.TimeRecords.PresenceRecord, DateTime?> OnThru_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(PresenceRecord);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PresenceRecord)obj;
			var otherImpl = (PresenceRecord__Implementation__)obj;
			var me = (PresenceRecord)this;

			me.From = other.From;
			me.Thru = other.Thru;
			this._fk_Mitarbeiter = otherImpl._fk_Mitarbeiter;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_PresenceRecord")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PresenceRecord != null)
            {
                OnToString_PresenceRecord(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<PresenceRecord> OnToString_PresenceRecord;

        [EventBasedMethod("OnPreSave_PresenceRecord")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresenceRecord != null) OnPreSave_PresenceRecord(this);
        }
        public static event ObjectEventHandler<PresenceRecord> OnPreSave_PresenceRecord;

        [EventBasedMethod("OnPostSave_PresenceRecord")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresenceRecord != null) OnPostSave_PresenceRecord(this);
        }
        public static event ObjectEventHandler<PresenceRecord> OnPostSave_PresenceRecord;

        [EventBasedMethod("OnCreated_PresenceRecord")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_PresenceRecord != null) OnCreated_PresenceRecord(this);
        }
        public static event ObjectEventHandler<PresenceRecord> OnCreated_PresenceRecord;

        [EventBasedMethod("OnDeleting_PresenceRecord")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_PresenceRecord != null) OnDeleting_PresenceRecord(this);
        }
        public static event ObjectEventHandler<PresenceRecord> OnDeleting_PresenceRecord;


		private static readonly object _propertiesLock = new object();
		private static System.ComponentModel.PropertyDescriptor[] _properties;
		
		private void _InitializePropertyDescriptors(Func<IReadOnlyKistlContext> lazyCtx)
		{
			if (_properties != null) return;
			lock (_propertiesLock)
			{
				// recheck for a lost race after aquiring the lock
				if (_properties != null) return;
				
				_properties = new System.ComponentModel.PropertyDescriptor[] {
					// else
					new CustomPropertyDescriptor<PresenceRecord__Implementation__, DateTime>(
						lazyCtx,
						new Guid("3833e790-e2f2-43c6-b9c2-79dd4a03c8c6"),
						"From",
						null,
						obj => obj.From,
						(obj, val) => obj.From = val),
					// else
					new CustomPropertyDescriptor<PresenceRecord__Implementation__, Kistl.App.Projekte.Mitarbeiter>(
						lazyCtx,
						new Guid("b67880d2-37b0-436f-8628-6637fbe19e31"),
						"Mitarbeiter",
						null,
						obj => obj.Mitarbeiter,
						(obj, val) => obj.Mitarbeiter = val),
					// else
					new CustomPropertyDescriptor<PresenceRecord__Implementation__, DateTime?>(
						lazyCtx,
						new Guid("17dabad9-a47e-46b8-a72e-b7616af0ceae"),
						"Thru",
						null,
						obj => obj.Thru,
						(obj, val) => obj.Thru = val),
					// rel: PresenceRecord has Mitarbeiter (f6d98929-883a-4457-a49f-157324bd5ae3)
				};
			}
		}
		
		protected override void CollectProperties(Func<IReadOnlyKistlContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			_InitializePropertyDescriptors(lazyCtx);
			props.AddRange(_properties);
		}
	

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references

			if (_fk_guid_Mitarbeiter.HasValue)
				Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)Context.FindPersistenceObject<Kistl.App.Projekte.Mitarbeiter>(_fk_guid_Mitarbeiter.Value);
			else if (_fk_Mitarbeiter.HasValue)
				Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)Context.Find<Kistl.App.Projekte.Mitarbeiter>(_fk_Mitarbeiter.Value);
			else
				Mitarbeiter__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(this._From, binStream);
            BinarySerializer.ToStream(Mitarbeiter != null ? Mitarbeiter.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._Thru, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._From, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._Thru, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._From, xml, "From", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(Mitarbeiter != null ? Mitarbeiter.ID : (int?)null, xml, "Mitarbeiter", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Thru, xml, "Thru", "Kistl.App.TimeRecords");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._From, xml, "From", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._fk_Mitarbeiter, xml, "Mitarbeiter", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Thru, xml, "Thru", "Kistl.App.TimeRecords");
        }

#endregion

    }


}