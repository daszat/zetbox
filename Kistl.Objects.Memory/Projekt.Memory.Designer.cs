// <autogenerated/>


namespace Kistl.App.Projekte
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

    using Kistl.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Projekt")]
    public class Projekt__Implementation__Memory : BaseMemoryDataObject, Projekt
    {
        [Obsolete]
        public Projekt__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public Projekt__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// Aufträge
        /// </summary>
        // object list property
        // ApplyObjectListPropertyTemplate
		// Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Auftrag> Auftraege
        {
            get
            {
                if (_AuftraegeWrapper == null)
                {
                    List<Kistl.App.Projekte.Auftrag> serverList;
                    if (Helper.IsPersistedObject(this))
                    {
						serverList = Context.GetListOf<Kistl.App.Projekte.Auftrag>(this, "Auftraege");
					}
                    else
                    {
                        serverList = new List<Kistl.App.Projekte.Auftrag>();
                    }
                        
                    _AuftraegeWrapper = new OneNRelationList<Kistl.App.Projekte.Auftrag>(
                        "Projekt",
                        null,
                        this,
                        () => this.NotifyPropertyChanged("Auftraege", null, null),
                        serverList);
                }
                return _AuftraegeWrapper;
            }
        }
        
        private OneNRelationList<Kistl.App.Projekte.Auftrag> _AuftraegeWrapper;


        /// <summary>
        /// 
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual double? AufwandGes
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _AufwandGes;
                if (OnAufwandGes_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnAufwandGes_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_AufwandGes != value)
                {
                    var __oldValue = _AufwandGes;
                    var __newValue = value;
                    if(OnAufwandGes_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnAufwandGes_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("AufwandGes", __oldValue, __newValue);
                    _AufwandGes = __newValue;
                    NotifyPropertyChanged("AufwandGes", __oldValue, __newValue);
                    if(OnAufwandGes_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnAufwandGes_PostSetter(this, __e);
                    }
                }
            }
        }
        private double? _AufwandGes;
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, double?> OnAufwandGes_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, double?> OnAufwandGes_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, double?> OnAufwandGes_PostSetter;

        /// <summary>
        /// Identity which changed this object
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ChangedBy
		// rel(A): Projekt was ChangedBy
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Identity ChangedBy
        {
            get
            {
				Kistl.App.Base.Identity __value;
                if (_fk_ChangedBy.HasValue)
                    __value = Context.Find<Kistl.App.Base.Identity>(_fk_ChangedBy.Value);
                else
                    __value = null;

				if(OnChangedBy_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Identity>(__value);
					OnChangedBy_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_ChangedBy == null)
					return;
                else if (value != null && value.ID == _fk_ChangedBy)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = ChangedBy;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ChangedBy", __oldValue, __newValue);
				
                if(OnChangedBy_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnChangedBy_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_ChangedBy = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ChangedBy", __oldValue, __newValue);

                if(OnChangedBy_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnChangedBy_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_ChangedBy;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ChangedBy
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, Kistl.App.Base.Identity> OnChangedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, Kistl.App.Base.Identity> OnChangedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, Kistl.App.Base.Identity> OnChangedBy_PostSetter;

        /// <summary>
        /// Date and time where this object was changed
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? ChangedOn
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ChangedOn;
                if (OnChangedOn_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnChangedOn_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ChangedOn != value)
                {
                    var __oldValue = _ChangedOn;
                    var __newValue = value;
                    if(OnChangedOn_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnChangedOn_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ChangedOn", __oldValue, __newValue);
                    _ChangedOn = __newValue;
                    NotifyPropertyChanged("ChangedOn", __oldValue, __newValue);
                    if(OnChangedOn_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnChangedOn_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _ChangedOn;
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, DateTime?> OnChangedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, DateTime?> OnChangedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, DateTime?> OnChangedOn_PostSetter;

        /// <summary>
        /// Identity which created this object
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for CreatedBy
		// rel(A): Projekt was CreatedBy
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Identity CreatedBy
        {
            get
            {
				Kistl.App.Base.Identity __value;
                if (_fk_CreatedBy.HasValue)
                    __value = Context.Find<Kistl.App.Base.Identity>(_fk_CreatedBy.Value);
                else
                    __value = null;

				if(OnCreatedBy_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Identity>(__value);
					OnCreatedBy_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_CreatedBy == null)
					return;
                else if (value != null && value.ID == _fk_CreatedBy)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = CreatedBy;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("CreatedBy", __oldValue, __newValue);
				
                if(OnCreatedBy_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnCreatedBy_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_CreatedBy = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("CreatedBy", __oldValue, __newValue);

                if(OnCreatedBy_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnCreatedBy_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_CreatedBy;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for CreatedBy
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, Kistl.App.Base.Identity> OnCreatedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, Kistl.App.Base.Identity> OnCreatedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, Kistl.App.Base.Identity> OnCreatedBy_PostSetter;

        /// <summary>
        /// Date and time where this object was created
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? CreatedOn
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _CreatedOn;
                if (OnCreatedOn_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnCreatedOn_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_CreatedOn != value)
                {
                    var __oldValue = _CreatedOn;
                    var __newValue = value;
                    if(OnCreatedOn_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnCreatedOn_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("CreatedOn", __oldValue, __newValue);
                    _CreatedOn = __newValue;
                    NotifyPropertyChanged("CreatedOn", __oldValue, __newValue);
                    if(OnCreatedOn_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnCreatedOn_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _CreatedOn;
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, DateTime?> OnCreatedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, DateTime?> OnCreatedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, DateTime?> OnCreatedOn_PostSetter;

        /// <summary>
        /// Bitte geben Sie den Kundennamen ein
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Kundenname
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Kundenname;
                if (OnKundenname_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnKundenname_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Kundenname != value)
                {
                    var __oldValue = _Kundenname;
                    var __newValue = value;
                    if(OnKundenname_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnKundenname_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Kundenname", __oldValue, __newValue);
                    _Kundenname = __newValue;
                    NotifyPropertyChanged("Kundenname", __oldValue, __newValue);
                    if(OnKundenname_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnKundenname_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Kundenname;
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, string> OnKundenname_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, string> OnKundenname_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, string> OnKundenname_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // collection reference property
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntryListProperty
		public IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
		{
			get
			{
				if (_Mitarbeiter == null)
				{
					Context.FetchRelation<Projekt_haben_Mitarbeiter_RelationEntry__Implementation__Memory>(new Guid("c7b3cf10-cdc8-454c-826c-04a0f7e5ef3e"), RelationEndRole.A, this);
					_Mitarbeiter 
						= new ClientRelationBSideListWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__Memory>(
							this, 
							new RelationshipFilterASideCollection<Projekt_haben_Mitarbeiter_RelationEntry__Implementation__Memory>(this.Context, this));
				}
				return _Mitarbeiter;
			}
		}

		private ClientRelationBSideListWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__Memory> _Mitarbeiter;

        /// <summary>
        /// Projektname
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Name
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    var __oldValue = _Name;
                    var __newValue = value;
                    if(OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    if(OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Name;
		public static event PropertyGetterHandler<Kistl.App.Projekte.Projekt, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Projekte.Projekt, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Projekte.Projekt, string> OnName_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // ApplyObjectListPropertyTemplate
		// Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Task> Tasks
        {
            get
            {
                if (_TasksWrapper == null)
                {
                    List<Kistl.App.Projekte.Task> serverList;
                    if (Helper.IsPersistedObject(this))
                    {
						serverList = Context.GetListOf<Kistl.App.Projekte.Task>(this, "Tasks");
					}
                    else
                    {
                        serverList = new List<Kistl.App.Projekte.Task>();
                    }
                        
                    _TasksWrapper = new OneNRelationList<Kistl.App.Projekte.Task>(
                        "Projekt",
                        null,
                        this,
                        () => this.NotifyPropertyChanged("Tasks", null, null),
                        serverList);
                }
                return _TasksWrapper;
            }
        }
        
        private OneNRelationList<Kistl.App.Projekte.Task> _TasksWrapper;


        public override Type GetImplementedInterface()
        {
            return typeof(Projekt);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Projekt)obj;
			var otherImpl = (Projekt__Implementation__Memory)obj;
			var me = (Projekt)this;

			me.AufwandGes = other.AufwandGes;
			me.ChangedOn = other.ChangedOn;
			me.CreatedOn = other.CreatedOn;
			me.Kundenname = other.Kundenname;
			me.Name = other.Name;
			this._fk_ChangedBy = otherImpl._fk_ChangedBy;
			this._fk_CreatedBy = otherImpl._fk_CreatedBy;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Projekt")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Projekt != null)
            {
                OnToString_Projekt(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Projekt> OnToString_Projekt;

        [EventBasedMethod("OnPreSave_Projekt")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Projekt != null) OnPreSave_Projekt(this);
        }
        public static event ObjectEventHandler<Projekt> OnPreSave_Projekt;

        [EventBasedMethod("OnPostSave_Projekt")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Projekt != null) OnPostSave_Projekt(this);
        }
        public static event ObjectEventHandler<Projekt> OnPostSave_Projekt;

        [EventBasedMethod("OnCreated_Projekt")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Projekt != null) OnCreated_Projekt(this);
        }
        public static event ObjectEventHandler<Projekt> OnCreated_Projekt;

        [EventBasedMethod("OnDeleting_Projekt")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Projekt != null) OnDeleting_Projekt(this);
        }
        public static event ObjectEventHandler<Projekt> OnDeleting_Projekt;


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
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, ICollection<Kistl.App.Projekte.Auftrag>>(
						lazyCtx,
						new Guid("30a1d8b6-4db5-45a0-a9a8-531472a9107e"),
						"Auftraege",
						null,
						obj => obj.Auftraege,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, double?>(
						lazyCtx,
						new Guid("a26cec7d-1e5c-44f5-9c56-92af595739eb"),
						"AufwandGes",
						null,
						obj => obj.AufwandGes,
						(obj, val) => obj.AufwandGes = val),
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, Kistl.App.Base.Identity>(
						lazyCtx,
						new Guid("2fe9d894-c359-412f-b787-d3ed3a26a0a2"),
						"ChangedBy",
						null,
						obj => obj.ChangedBy,
						(obj, val) => obj.ChangedBy = val),
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, DateTime?>(
						lazyCtx,
						new Guid("d1f821b0-5991-44a7-9c4d-8be66834ea9c"),
						"ChangedOn",
						null,
						obj => obj.ChangedOn,
						(obj, val) => obj.ChangedOn = val),
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, Kistl.App.Base.Identity>(
						lazyCtx,
						new Guid("fbe34f93-21ec-470a-b9d4-6e4664729466"),
						"CreatedBy",
						null,
						obj => obj.CreatedBy,
						(obj, val) => obj.CreatedBy = val),
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, DateTime?>(
						lazyCtx,
						new Guid("7119febf-e750-411f-a4f2-5a2181e45dc7"),
						"CreatedOn",
						null,
						obj => obj.CreatedOn,
						(obj, val) => obj.CreatedOn = val),
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, string>(
						lazyCtx,
						new Guid("cd6be045-d1bd-4086-b848-c83249f5ca9b"),
						"Kundenname",
						null,
						obj => obj.Kundenname,
						(obj, val) => obj.Kundenname = val),
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, IList<Kistl.App.Projekte.Mitarbeiter>>(
						lazyCtx,
						new Guid("3e60fe29-ac50-4232-bbeb-af023ede02f6"),
						"Mitarbeiter",
						null,
						obj => obj.Mitarbeiter,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, string>(
						lazyCtx,
						new Guid("b5482479-fd14-4990-86f4-49872e2eeeb8"),
						"Name",
						null,
						obj => obj.Name,
						(obj, val) => obj.Name = val),
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<Projekt__Implementation__Memory, ICollection<Kistl.App.Projekte.Task>>(
						lazyCtx,
						new Guid("f6ff71b0-ccaf-4c7d-8e2b-1210a9df4b0f"),
						"Tasks",
						null,
						obj => obj.Tasks,
						null), // lists are read-only properties
					// rel: Projekt has Auftraege (062fa6cf-bdb1-4994-9e8b-5fe5426c60aa)
					// rel: Projekt has Tasks (434dab4f-0dcd-4724-a62b-730540ce143a)
					// rel: Projekt was ChangedBy (bc2a3fdc-68d7-4ba1-9c16-03fd74c43bb0)
					// rel: Projekt was CreatedBy (035db8da-a9f4-4529-9f50-29afd9e6f043)
				};
			}
		}
		
		protected override void CollectProperties(Func<IReadOnlyKistlContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			_InitializePropertyDescriptors(lazyCtx);
			props.AddRange(_properties);
		}
	

		public override void UpdateParent(string propertyName, int? id)
		{
			int? __oldValue, __newValue = id;
			
			switch(propertyName)
			{
                case "ChangedBy":
                    __oldValue = _fk_ChangedBy;
                    NotifyPropertyChanging("ChangedBy", __oldValue, __newValue);
                    _fk_ChangedBy = __newValue;
                    NotifyPropertyChanged("ChangedBy", __oldValue, __newValue);
                    break;
                case "CreatedBy":
                    __oldValue = _fk_CreatedBy;
                    NotifyPropertyChanging("CreatedBy", __oldValue, __newValue);
                    _fk_CreatedBy = __newValue;
                    NotifyPropertyChanged("CreatedBy", __oldValue, __newValue);
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(this._AufwandGes, binStream);
            BinarySerializer.ToStream(this._fk_ChangedBy, binStream);
            BinarySerializer.ToStream(this._ChangedOn, binStream);
            BinarySerializer.ToStream(this._fk_CreatedBy, binStream);
            BinarySerializer.ToStream(this._CreatedOn, binStream);
            BinarySerializer.ToStream(this._Kundenname, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AufwandGes, binStream);
            BinarySerializer.FromStream(out this._fk_ChangedBy, binStream);
            BinarySerializer.FromStream(out this._ChangedOn, binStream);
            BinarySerializer.FromStream(out this._fk_CreatedBy, binStream);
            BinarySerializer.FromStream(out this._CreatedOn, binStream);
            BinarySerializer.FromStream(out this._Kundenname, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._AufwandGes, xml, "AufwandGes", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._fk_ChangedBy, xml, "ChangedBy", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._fk_CreatedBy, xml, "CreatedBy", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Kundenname, xml, "Kundenname", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Projekte");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._AufwandGes, xml, "AufwandGes", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._fk_ChangedBy, xml, "ChangedBy", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._fk_CreatedBy, xml, "CreatedBy", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Kundenname, xml, "Kundenname", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Projekte");
        }

#endregion

    }


}