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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Assembly")]
    public class Assembly__Implementation__Memory : BaseMemoryDataObject, Assembly, Kistl.API.IExportableInternal
    {
        [Obsolete]
        public Assembly__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public Assembly__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// Identity which changed this object
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ChangedBy
		// rel(A): Assembly was ChangedBy
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
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Identity> OnChangedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Identity> OnChangedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Identity> OnChangedBy_PostSetter;

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
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, DateTime?> OnChangedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, DateTime?> OnChangedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, DateTime?> OnChangedOn_PostSetter;

        /// <summary>
        /// Identity which created this object
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for CreatedBy
		// rel(A): Assembly was CreatedBy
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
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Identity> OnCreatedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Identity> OnCreatedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Identity> OnCreatedBy_PostSetter;

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
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, DateTime?> OnCreatedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, DateTime?> OnCreatedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, DateTime?> OnCreatedOn_PostSetter;

        /// <summary>
        /// Deployment restrictions for this assembly
        /// </summary>
        // enumeration property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual Kistl.App.Base.DeploymentRestriction DeploymentRestrictions
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DeploymentRestrictions;
                if (OnDeploymentRestrictions_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Kistl.App.Base.DeploymentRestriction>(__result);
                    OnDeploymentRestrictions_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DeploymentRestrictions != value)
                {
                    var __oldValue = _DeploymentRestrictions;
                    var __newValue = value;
                    if(OnDeploymentRestrictions_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Kistl.App.Base.DeploymentRestriction>(__oldValue, __newValue);
                        OnDeploymentRestrictions_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DeploymentRestrictions", __oldValue, __newValue);
                    _DeploymentRestrictions = __newValue;
                    NotifyPropertyChanged("DeploymentRestrictions", __oldValue, __newValue);
                    if(OnDeploymentRestrictions_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Kistl.App.Base.DeploymentRestriction>(__oldValue, __newValue);
                        OnDeploymentRestrictions_PostSetter(this, __e);
                    }
                }
            }
        }
        private Kistl.App.Base.DeploymentRestriction _DeploymentRestrictions;
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.DeploymentRestriction> OnDeploymentRestrictions_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.DeploymentRestriction> OnDeploymentRestrictions_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.DeploymentRestriction> OnDeploymentRestrictions_PostSetter;

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
                    var __p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("9c1ddbcf-24b9-47cb-a27d-043fc47e4002"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        __result = this._ExportGuid = (Guid)__p.DefaultValue.GetDefaultValue();
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Assembly.ExportGuid'");
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
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// Module
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Module
		// rel(B): Module contains Assemblies
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
				Kistl.App.Base.Module __value;
                if (_fk_Module.HasValue)
                    __value = Context.Find<Kistl.App.Base.Module>(_fk_Module.Value);
                else
                    __value = null;

				if(OnModule_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Module>(__value);
					OnModule_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_Module == null)
					return;
                else if (value != null && value.ID == _fk_Module)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = Module;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Module", __oldValue, __newValue);
				
                if(OnModule_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Module>(__oldValue, __newValue);
					OnModule_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_Module = __newValue == null ? (int?)null : __newValue.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (__oldValue != null)
				{
					// remove from old list
					(__oldValue.Assemblies as OneNRelationList<Kistl.App.Base.Assembly>).RemoveWithoutClearParent(this);
				}

                if (__newValue != null)
                {
					// add to new list
					(__newValue.Assemblies as OneNRelationList<Kistl.App.Base.Assembly>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Module", __oldValue, __newValue);

                if(OnModule_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Module>(__oldValue, __newValue);
					OnModule_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_Module;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Module
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Module> OnModule_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Module> OnModule_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, Kistl.App.Base.Module> OnModule_PostSetter;

        /// <summary>
        /// Full Assemblyname eg. MyActions, Version=1.0.0.0
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
		public static event PropertyGetterHandler<Kistl.App.Base.Assembly, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Assembly, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Assembly, string> OnName_PostSetter;

        /// <summary>
        /// Regenerates the stored list of TypeRefs from the loaded assembly
        /// </summary>
		[EventBasedMethod("OnRegenerateTypeRefs_Assembly")]
		public virtual void RegenerateTypeRefs() 
		{
            // base.RegenerateTypeRefs();
            if (OnRegenerateTypeRefs_Assembly != null)
            {
				OnRegenerateTypeRefs_Assembly(this);
			}
			else
			{
                throw new NotImplementedException("No handler registered on Assembly.RegenerateTypeRefs");
			}
        }
		public delegate void RegenerateTypeRefs_Handler<T>(T obj);
		public static event RegenerateTypeRefs_Handler<Assembly> OnRegenerateTypeRefs_Assembly;



        public override Type GetImplementedInterface()
        {
            return typeof(Assembly);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Assembly)obj;
			var otherImpl = (Assembly__Implementation__Memory)obj;
			var me = (Assembly)this;

			me.ChangedOn = other.ChangedOn;
			me.CreatedOn = other.CreatedOn;
			me.DeploymentRestrictions = other.DeploymentRestrictions;
			me.ExportGuid = other.ExportGuid;
			me.Name = other.Name;
			this._fk_ChangedBy = otherImpl._fk_ChangedBy;
			this._fk_CreatedBy = otherImpl._fk_CreatedBy;
			this._fk_Module = otherImpl._fk_Module;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Assembly")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Assembly != null)
            {
                OnToString_Assembly(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Assembly> OnToString_Assembly;

        [EventBasedMethod("OnPreSave_Assembly")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Assembly != null) OnPreSave_Assembly(this);
        }
        public static event ObjectEventHandler<Assembly> OnPreSave_Assembly;

        [EventBasedMethod("OnPostSave_Assembly")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Assembly != null) OnPostSave_Assembly(this);
        }
        public static event ObjectEventHandler<Assembly> OnPostSave_Assembly;

        [EventBasedMethod("OnCreated_Assembly")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Assembly != null) OnCreated_Assembly(this);
        }
        public static event ObjectEventHandler<Assembly> OnCreated_Assembly;

        [EventBasedMethod("OnDeleting_Assembly")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Assembly != null) OnDeleting_Assembly(this);
        }
        public static event ObjectEventHandler<Assembly> OnDeleting_Assembly;


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
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, Kistl.App.Base.Identity>(
						lazyCtx,
						new Guid("cb31759a-597a-4277-a963-c914a07312e7"),
						"ChangedBy",
						null,
						obj => obj.ChangedBy,
						(obj, val) => obj.ChangedBy = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, DateTime?>(
						lazyCtx,
						new Guid("5e74f538-9961-4b5c-a770-b92b75fb898a"),
						"ChangedOn",
						null,
						obj => obj.ChangedOn,
						(obj, val) => obj.ChangedOn = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, Kistl.App.Base.Identity>(
						lazyCtx,
						new Guid("819fd217-def6-49d1-8239-bbc7451e95f6"),
						"CreatedBy",
						null,
						obj => obj.CreatedBy,
						(obj, val) => obj.CreatedBy = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, DateTime?>(
						lazyCtx,
						new Guid("c6350562-d385-41b5-afc9-89024a38ceba"),
						"CreatedOn",
						null,
						obj => obj.CreatedOn,
						(obj, val) => obj.CreatedOn = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, Kistl.App.Base.DeploymentRestriction>(
						lazyCtx,
						new Guid("8458ea0d-04ca-48db-88ed-7d36e7e93b58"),
						"DeploymentRestrictions",
						null,
						obj => obj.DeploymentRestrictions,
						(obj, val) => obj.DeploymentRestrictions = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, Guid>(
						lazyCtx,
						new Guid("9c1ddbcf-24b9-47cb-a27d-043fc47e4002"),
						"ExportGuid",
						null,
						obj => obj.ExportGuid,
						(obj, val) => obj.ExportGuid = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, Kistl.App.Base.Module>(
						lazyCtx,
						new Guid("8d579192-717e-4f2c-90ed-1c066255e270"),
						"Module",
						null,
						obj => obj.Module,
						(obj, val) => obj.Module = val),
					// else
					new CustomPropertyDescriptor<Assembly__Implementation__Memory, string>(
						lazyCtx,
						new Guid("9a9dbd59-6816-4d25-9ef2-da84b96bf454"),
						"Name",
						null,
						obj => obj.Name,
						(obj, val) => obj.Name = val),
					// rel: Assembly was CreatedBy (56b41a57-e179-4cd3-8706-8c6871f0fc58)
					// rel: Assembly was ChangedBy (98612719-3882-45a2-ae30-b5329524e744)
					// rel: Module contains Assemblies (a10474db-85df-4731-a86c-124e54f3d146)
					// rel: Template has DisplayedTypeAssembly (0e64ccd9-2f72-489a-83a4-095f949fdee3)
					// rel: TypeRef has Assembly (c10b1abc-3786-40f6-8c8c-dccdd8dc03ef)
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
                case "Module":
                    __oldValue = _fk_Module;
                    NotifyPropertyChanging("Module", __oldValue, __newValue);
                    _fk_Module = __newValue;
                    NotifyPropertyChanged("Module", __oldValue, __newValue);
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
            BinarySerializer.ToStream((int?)((Assembly)this).DeploymentRestrictions, binStream);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ChangedBy, binStream);
            BinarySerializer.FromStream(out this._ChangedOn, binStream);
            BinarySerializer.FromStream(out this._fk_CreatedBy, binStream);
            BinarySerializer.FromStream(out this._CreatedOn, binStream);
			{
				int? baseValue;
				BinarySerializer.FromStream(out baseValue, binStream);
				((Assembly)this).DeploymentRestrictions = (Kistl.App.Base.DeploymentRestriction)baseValue;
			}
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._fk_ChangedBy, xml, "ChangedBy", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_CreatedBy, xml, "CreatedBy", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.ToStream((int?)this.DeploymentRestrictions, xml, "DeploymentRestrictions", "Kistl.App.Base");
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_ChangedBy, xml, "ChangedBy", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_CreatedBy, xml, "CreatedBy", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.FromStreamConverter(v => ((Assembly)this).DeploymentRestrictions = (Kistl.App.Base.DeploymentRestriction)v, xml, "DeploymentRestrictions", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream((int?)this.DeploymentRestrictions, xml, "DeploymentRestrictions", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.FromStreamConverter(v => ((Assembly)this).DeploymentRestrictions = (Kistl.App.Base.DeploymentRestriction)v, xml, "DeploymentRestrictions", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            this._isExportGuidSet = true;
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Base");
        }

#endregion

    }


}