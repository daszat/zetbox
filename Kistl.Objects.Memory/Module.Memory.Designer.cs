// <autogenerated/>


namespace Kistl.App.Base
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
    /// Metadefinition Object for Modules.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Module")]
    public class Module__Implementation__Memory : BaseMemoryDataObject, Kistl.API.IExportableInternal, Module
    {
        [Obsolete]
        public Module__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public Module__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// Assemblies des Moduls
        /// </summary>
        // object list property
        // ApplyObjectListPropertyTemplate
		// Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Assembly> Assemblies
        {
            get
            {
                if (_AssembliesWrapper == null)
                {
                    List<Kistl.App.Base.Assembly> serverList;
                    if (Helper.IsPersistedObject(this))
                    {
						serverList = Context.GetListOf<Kistl.App.Base.Assembly>(this, "Assemblies");
					}
                    else
                    {
                        serverList = new List<Kistl.App.Base.Assembly>();
                    }
                        
                    _AssembliesWrapper = new OneNRelationList<Kistl.App.Base.Assembly>(
                        "Module",
                        null,
                        this,
                        () => this.NotifyPropertyChanged("Assemblies", null, null),
                        serverList);
                }
                return _AssembliesWrapper;
            }
        }
        
        private OneNRelationList<Kistl.App.Base.Assembly> _AssembliesWrapper;


        /// <summary>
        /// Identity which changed this object
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ChangedBy
		// rel(A): Module was ChangedBy
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
        
        // normalize namespace for Templates
        private Kistl.App.Base.Identity ChangedBy__Implementation__
        {
			get
			{
				return ChangedBy;
			}
			set
			{
				ChangedBy = value;
			}
		}
        
        private int? _fk_ChangedBy;
        private Guid? _fk_guid_ChangedBy = null;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ChangedBy
		public static event PropertyGetterHandler<Kistl.App.Base.Module, Kistl.App.Base.Identity> OnChangedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, Kistl.App.Base.Identity> OnChangedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, Kistl.App.Base.Identity> OnChangedBy_PostSetter;

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
		public static event PropertyGetterHandler<Kistl.App.Base.Module, DateTime?> OnChangedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, DateTime?> OnChangedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, DateTime?> OnChangedOn_PostSetter;

        /// <summary>
        /// Identity which created this object
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for CreatedBy
		// rel(A): Module was CreatedBy
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
        
        // normalize namespace for Templates
        private Kistl.App.Base.Identity CreatedBy__Implementation__
        {
			get
			{
				return CreatedBy;
			}
			set
			{
				CreatedBy = value;
			}
		}
        
        private int? _fk_CreatedBy;
        private Guid? _fk_guid_CreatedBy = null;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for CreatedBy
		public static event PropertyGetterHandler<Kistl.App.Base.Module, Kistl.App.Base.Identity> OnCreatedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, Kistl.App.Base.Identity> OnCreatedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, Kistl.App.Base.Identity> OnCreatedBy_PostSetter;

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
		public static event PropertyGetterHandler<Kistl.App.Base.Module, DateTime?> OnCreatedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, DateTime?> OnCreatedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, DateTime?> OnCreatedOn_PostSetter;

        /// <summary>
        /// Datentypendes Modules
        /// </summary>
        // object list property
        // ApplyObjectListPropertyTemplate
		// Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.DataType> DataTypes
        {
            get
            {
                if (_DataTypesWrapper == null)
                {
                    List<Kistl.App.Base.DataType> serverList;
                    if (Helper.IsPersistedObject(this))
                    {
						serverList = Context.GetListOf<Kistl.App.Base.DataType>(this, "DataTypes");
					}
                    else
                    {
                        serverList = new List<Kistl.App.Base.DataType>();
                    }
                        
                    _DataTypesWrapper = new OneNRelationList<Kistl.App.Base.DataType>(
                        "Module",
                        null,
                        this,
                        () => this.NotifyPropertyChanged("DataTypes", null, null),
                        serverList);
                }
                return _DataTypesWrapper;
            }
        }
        
        private OneNRelationList<Kistl.App.Base.DataType> _DataTypesWrapper;


        /// <summary>
        /// Description of this Module
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Description
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Description;
                if (OnDescription_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDescription_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
                    var __oldValue = _Description;
                    var __newValue = value;
                    if(OnDescription_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    _Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);
                    if(OnDescription_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Description;
		public static event PropertyGetterHandler<Kistl.App.Base.Module, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, string> OnDescription_PostSetter;

        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        private bool _isExportGuidSet = false;
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual Guid ExportGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ExportGuid;
                if (!_isExportGuidSet) {
                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("75e3db82-220c-474e-973a-ceb65fd8386d"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        __result = this._ExportGuid = (Guid)__p.DefaultValue.GetDefaultValue();
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Module.ExportGuid'");
                    }
                }
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isExportGuidSet = true;
                if (_ExportGuid != value)
                {
                    var __oldValue = _ExportGuid;
                    var __newValue = value;
                    if(OnExportGuid_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    _ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if(OnExportGuid_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
            }
        }
        private Guid _ExportGuid;
		public static event PropertyGetterHandler<Kistl.App.Base.Module, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// Name des Moduls
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
		public static event PropertyGetterHandler<Kistl.App.Base.Module, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, string> OnName_PostSetter;

        /// <summary>
        /// CLR Namespace des Moduls
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Namespace
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Namespace;
                if (OnNamespace_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnNamespace_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Namespace != value)
                {
                    var __oldValue = _Namespace;
                    var __newValue = value;
                    if(OnNamespace_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnNamespace_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Namespace", __oldValue, __newValue);
                    _Namespace = __newValue;
                    NotifyPropertyChanged("Namespace", __oldValue, __newValue);
                    if(OnNamespace_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnNamespace_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Namespace;
		public static event PropertyGetterHandler<Kistl.App.Base.Module, string> OnNamespace_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Module, string> OnNamespace_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Module, string> OnNamespace_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(Module);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Module)obj;
			var otherImpl = (Module__Implementation__Memory)obj;
			var me = (Module)this;

			me.ChangedOn = other.ChangedOn;
			me.CreatedOn = other.CreatedOn;
			me.Description = other.Description;
			me.ExportGuid = other.ExportGuid;
			me.Name = other.Name;
			me.Namespace = other.Namespace;
			this._fk_ChangedBy = otherImpl._fk_ChangedBy;
			this._fk_CreatedBy = otherImpl._fk_CreatedBy;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references

			if (_fk_guid_ChangedBy.HasValue)
				ChangedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__Memory)Context.FindPersistenceObject<Kistl.App.Base.Identity>(_fk_guid_ChangedBy.Value);
			else if (_fk_ChangedBy.HasValue)
				ChangedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__Memory)Context.Find<Kistl.App.Base.Identity>(_fk_ChangedBy.Value);
			else
				ChangedBy__Implementation__ = null;

			if (_fk_guid_CreatedBy.HasValue)
				CreatedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__Memory)Context.FindPersistenceObject<Kistl.App.Base.Identity>(_fk_guid_CreatedBy.Value);
			else if (_fk_CreatedBy.HasValue)
				CreatedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__Memory)Context.Find<Kistl.App.Base.Identity>(_fk_CreatedBy.Value);
			else
				CreatedBy__Implementation__ = null;
		}
        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Module")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Module != null)
            {
                OnToString_Module(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Module> OnToString_Module;

        [EventBasedMethod("OnPreSave_Module")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Module != null) OnPreSave_Module(this);
        }
        public static event ObjectEventHandler<Module> OnPreSave_Module;

        [EventBasedMethod("OnPostSave_Module")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Module != null) OnPostSave_Module(this);
        }
        public static event ObjectEventHandler<Module> OnPostSave_Module;

        [EventBasedMethod("OnCreated_Module")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Module != null) OnCreated_Module(this);
        }
        public static event ObjectEventHandler<Module> OnCreated_Module;

        [EventBasedMethod("OnDeleting_Module")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Module != null) OnDeleting_Module(this);
        }
        public static event ObjectEventHandler<Module> OnDeleting_Module;


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
					new CustomPropertyDescriptor<Module__Implementation__Memory, ICollection<Kistl.App.Base.Assembly>>(
						lazyCtx,
						new Guid("cab23a85-a179-475c-a70f-77789e2a2907"),
						"Assemblies",
						null,
						obj => obj.Assemblies,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, Kistl.App.Base.Identity>(
						lazyCtx,
						new Guid("d1cad06b-040e-417c-8e43-67fa2e861649"),
						"ChangedBy",
						null,
						obj => obj.ChangedBy,
						(obj, val) => obj.ChangedBy = val),
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, DateTime?>(
						lazyCtx,
						new Guid("75aedd67-e42d-461d-9263-c301d15b54f0"),
						"ChangedOn",
						null,
						obj => obj.ChangedOn,
						(obj, val) => obj.ChangedOn = val),
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, Kistl.App.Base.Identity>(
						lazyCtx,
						new Guid("7b76322d-c8cd-4845-9cb4-b77f572692be"),
						"CreatedBy",
						null,
						obj => obj.CreatedBy,
						(obj, val) => obj.CreatedBy = val),
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, DateTime?>(
						lazyCtx,
						new Guid("c6370ff5-115a-441d-a688-28297c9e46f8"),
						"CreatedOn",
						null,
						obj => obj.CreatedOn,
						(obj, val) => obj.CreatedOn = val),
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<Module__Implementation__Memory, ICollection<Kistl.App.Base.DataType>>(
						lazyCtx,
						new Guid("a1711984-5263-4407-ac67-6e4123954976"),
						"DataTypes",
						null,
						obj => obj.DataTypes,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, string>(
						lazyCtx,
						new Guid("79408b86-1731-42ad-89b2-ed5c567fbf8a"),
						"Description",
						null,
						obj => obj.Description,
						(obj, val) => obj.Description = val),
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, Guid>(
						lazyCtx,
						new Guid("75e3db82-220c-474e-973a-ceb65fd8386d"),
						"ExportGuid",
						null,
						obj => obj.ExportGuid,
						(obj, val) => obj.ExportGuid = val),
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, string>(
						lazyCtx,
						new Guid("63facb30-d8f7-42f6-8c14-85933d5f94b8"),
						"Name",
						null,
						obj => obj.Name,
						(obj, val) => obj.Name = val),
					// else
					new CustomPropertyDescriptor<Module__Implementation__Memory, string>(
						lazyCtx,
						new Guid("36d2b9e7-d6b9-4a9c-a363-7e059a637919"),
						"Namespace",
						null,
						obj => obj.Namespace,
						(obj, val) => obj.Namespace = val),
					// rel: AccessControl has Module (89b9c0a5-3b5a-4e04-83cf-0e8de37abcf1)
					// rel: ControlKind has Module (6d5e026a-2605-4630-88ed-9da0b0e14c95)
					// rel: FilterConfiguration has Module (a4f7a1d1-bedb-4708-96ff-cd5033c6c03e)
					// rel: Group has Module (8d76b5ef-a7ae-4d4b-a92e-747fe972acfb)
					// rel: Icon has Module (a1360ce2-ecd5-4660-9b4d-3a2dc3919344)
					// rel: Method has Module (b251ee8c-2821-441e-b631-d215c006f1c8)
					// rel: MethodInvocation has Module (379b7181-a832-431f-a48d-ef1dd1996414)
					// rel: MigrationProject migrates_to Module (d2972651-cf22-4dcd-8cdf-4aa0aba7ce1f)
					// rel: Module contains Assemblies (a10474db-85df-4731-a86c-124e54f3d146)
					// rel: Module contains DataTypes (52c4ab07-f341-4eb3-86e2-05f27c8af2f7)
					// rel: Module has Relation (1c91bee2-397b-44f3-8346-313a8e2ba127)
					// rel: Module was ChangedBy (9d108dc6-7caa-4597-95d4-82cf52da5638)
					// rel: Module was CreatedBy (5c8dd58e-cf1d-484f-af64-ff84ea4c3ee9)
					// rel: BaseProperty has Module (bffae7c3-c5f3-4139-ae96-577f4c9fed8f)
					// rel: ViewDescriptor has Module (51b089fa-edd9-4a1b-9f4c-ccfdaad76856)
					// rel: ViewModelDescriptor has Module (557dbc1c-2a38-4c77-8544-264a95307980)
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
            BinarySerializer.ToStream(this._fk_ChangedBy, binStream);
            BinarySerializer.ToStream(this._ChangedOn, binStream);
            BinarySerializer.ToStream(this._fk_CreatedBy, binStream);
            BinarySerializer.ToStream(this._CreatedOn, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._Namespace, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ChangedBy, binStream);
            BinarySerializer.FromStream(out this._ChangedOn, binStream);
            BinarySerializer.FromStream(out this._fk_CreatedBy, binStream);
            BinarySerializer.FromStream(out this._CreatedOn, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._Namespace, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._fk_ChangedBy, xml, "ChangedBy", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_CreatedBy, xml, "CreatedBy", "Kistl.App.Base");
            XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_ChangedBy, xml, "ChangedBy", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_CreatedBy, xml, "CreatedBy", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            this._isExportGuidSet = true;
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

#endregion

    }


}