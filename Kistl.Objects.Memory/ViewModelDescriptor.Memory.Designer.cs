// <autogenerated/>


namespace Kistl.App.GUI
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
    [System.Diagnostics.DebuggerDisplay("ViewModelDescriptor")]
    public class ViewModelDescriptor__Implementation__Memory : BaseMemoryDataObject, Kistl.API.IExportableInternal, ViewModelDescriptor
    {
        [Obsolete]
        public ViewModelDescriptor__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public ViewModelDescriptor__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// The default ControlKind for displaying this model in a GridCell
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for DefaultGridCellKind
		// rel(A): ViewModelDescriptor displayedInGridBy DefaultGridCellKind
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.ControlKind DefaultGridCellKind
        {
            get
            {
				Kistl.App.GUI.ControlKind __value;
                if (_fk_DefaultGridCellKind.HasValue)
                    __value = Context.Find<Kistl.App.GUI.ControlKind>(_fk_DefaultGridCellKind.Value);
                else
                    __value = null;

				if(OnDefaultGridCellKind_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.GUI.ControlKind>(__value);
					OnDefaultGridCellKind_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_DefaultGridCellKind == null)
					return;
                else if (value != null && value.ID == _fk_DefaultGridCellKind)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = DefaultGridCellKind;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DefaultGridCellKind", __oldValue, __newValue);
				
                if(OnDefaultGridCellKind_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultGridCellKind_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_DefaultGridCellKind = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DefaultGridCellKind", __oldValue, __newValue);

                if(OnDefaultGridCellKind_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultGridCellKind_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_DefaultGridCellKind;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for DefaultGridCellKind
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultGridCellKind_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultGridCellKind_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultGridCellKind_PostSetter;

        /// <summary>
        /// The default ControlKind to use for this Presentable.
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for DefaultKind
		// rel(A): Presentable has DefaultKind
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.ControlKind DefaultKind
        {
            get
            {
				Kistl.App.GUI.ControlKind __value;
                if (_fk_DefaultKind.HasValue)
                    __value = Context.Find<Kistl.App.GUI.ControlKind>(_fk_DefaultKind.Value);
                else
                    __value = null;

				if(OnDefaultKind_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.GUI.ControlKind>(__value);
					OnDefaultKind_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_DefaultKind == null)
					return;
                else if (value != null && value.ID == _fk_DefaultKind)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = DefaultKind;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DefaultKind", __oldValue, __newValue);
				
                if(OnDefaultKind_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultKind_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_DefaultKind = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DefaultKind", __oldValue, __newValue);

                if(OnDefaultKind_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultKind_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_DefaultKind;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for DefaultKind
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_PostSetter;

        /// <summary>
        /// describe this ViewModel
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
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, string> OnDescription_PostSetter;

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
                    var __p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("77ce1e5b-f244-4279-af13-b3e75b55f933"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        __result = this._ExportGuid = (Guid)__p.DefaultValue.GetDefaultValue();
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'ViewModelDescriptor.ExportGuid'");
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
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Module
		// rel(A): ViewModelDescriptor has Module
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
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.Module> OnModule_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.Module> OnModule_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.Module> OnModule_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // collection reference property
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.ControlKind> SecondaryControlKinds
		{
			get
			{
				if (_SecondaryControlKinds == null)
				{
					Context.FetchRelation<ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__Memory>(new Guid("5404456a-4527-4e40-a660-b4a5e96e4a47"), RelationEndRole.A, this);
					_SecondaryControlKinds 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind, ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__Memory>(
							this, 
							new RelationshipFilterASideCollection<ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__Memory>(this.Context, this));
				}
				return _SecondaryControlKinds;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind, ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__Memory> _SecondaryControlKinds;

        /// <summary>
        /// The described CLR class&apos; reference
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ViewModelRef
		// rel(A): Descriptor has ViewModelRef
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef ViewModelRef
        {
            get
            {
				Kistl.App.Base.TypeRef __value;
                if (_fk_ViewModelRef.HasValue)
                    __value = Context.Find<Kistl.App.Base.TypeRef>(_fk_ViewModelRef.Value);
                else
                    __value = null;

				if(OnViewModelRef_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.TypeRef>(__value);
					OnViewModelRef_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_ViewModelRef == null)
					return;
                else if (value != null && value.ID == _fk_ViewModelRef)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = ViewModelRef;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ViewModelRef", __oldValue, __newValue);
				
                if(OnViewModelRef_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnViewModelRef_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_ViewModelRef = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ViewModelRef", __oldValue, __newValue);

                if(OnViewModelRef_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnViewModelRef_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_ViewModelRef;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ViewModelRef
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.TypeRef> OnViewModelRef_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.TypeRef> OnViewModelRef_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.TypeRef> OnViewModelRef_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(ViewModelDescriptor);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ViewModelDescriptor)obj;
			var otherImpl = (ViewModelDescriptor__Implementation__Memory)obj;
			var me = (ViewModelDescriptor)this;

			me.Description = other.Description;
			me.ExportGuid = other.ExportGuid;
			this._fk_DefaultGridCellKind = otherImpl._fk_DefaultGridCellKind;
			this._fk_DefaultKind = otherImpl._fk_DefaultKind;
			this._fk_Module = otherImpl._fk_Module;
			this._fk_ViewModelRef = otherImpl._fk_ViewModelRef;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ViewModelDescriptor")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ViewModelDescriptor != null)
            {
                OnToString_ViewModelDescriptor(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ViewModelDescriptor> OnToString_ViewModelDescriptor;

        [EventBasedMethod("OnPreSave_ViewModelDescriptor")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ViewModelDescriptor != null) OnPreSave_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnPreSave_ViewModelDescriptor;

        [EventBasedMethod("OnPostSave_ViewModelDescriptor")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ViewModelDescriptor != null) OnPostSave_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnPostSave_ViewModelDescriptor;

        [EventBasedMethod("OnCreated_ViewModelDescriptor")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_ViewModelDescriptor != null) OnCreated_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnCreated_ViewModelDescriptor;

        [EventBasedMethod("OnDeleting_ViewModelDescriptor")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_ViewModelDescriptor != null) OnDeleting_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnDeleting_ViewModelDescriptor;


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
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, Kistl.App.GUI.ControlKind>(
						lazyCtx,
						new Guid("6c744476-35e0-4cef-a221-f02abc81566c"),
						"DefaultGridCellKind",
						null,
						obj => obj.DefaultGridCellKind,
						(obj, val) => obj.DefaultGridCellKind = val),
					// else
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, Kistl.App.GUI.ControlKind>(
						lazyCtx,
						new Guid("b535115c-b847-479d-bdea-a7994ae6eeca"),
						"DefaultKind",
						null,
						obj => obj.DefaultKind,
						(obj, val) => obj.DefaultKind = val),
					// else
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, string>(
						lazyCtx,
						new Guid("93e25648-50f9-40d8-8753-e5dadab68e1d"),
						"Description",
						null,
						obj => obj.Description,
						(obj, val) => obj.Description = val),
					// else
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, Guid>(
						lazyCtx,
						new Guid("77ce1e5b-f244-4279-af13-b3e75b55f933"),
						"ExportGuid",
						null,
						obj => obj.ExportGuid,
						(obj, val) => obj.ExportGuid = val),
					// else
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, Kistl.App.Base.Module>(
						lazyCtx,
						new Guid("0b7135d3-dedc-4091-a0c4-690c1b4a2b6d"),
						"Module",
						null,
						obj => obj.Module,
						(obj, val) => obj.Module = val),
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, ICollection<Kistl.App.GUI.ControlKind>>(
						lazyCtx,
						new Guid("5e2e007c-2e90-4ba6-9c9d-46e62b662ff9"),
						"SecondaryControlKinds",
						null,
						obj => obj.SecondaryControlKinds,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__Memory, Kistl.App.Base.TypeRef>(
						lazyCtx,
						new Guid("554288d1-f5f4-4b22-908b-01525a1d0f9b"),
						"ViewModelRef",
						null,
						obj => obj.ViewModelRef,
						(obj, val) => obj.ViewModelRef = val),
					// rel: FilterConfiguration has ViewModelDescriptor (2d856368-2ffc-42de-83ef-5389cc57308a)
					// rel: Presentable has DefaultViewModelDescriptor (1ae94c81-3359-45e8-b97a-b61add91abba)
					// rel: Property has ValueModelDescriptor (3437ea5d-d926-4a0b-a848-9dafedf7ad6a)
					// rel: ViewModelDescriptor displayedInGridBy DefaultGridCellKind (0a03215f-1c1a-4a44-892d-86642eefe9f1)
					// rel: Presentable has DefaultKind (cc835258-6ada-4f3f-839e-7f276ded995a)
					// rel: ViewModelDescriptor has Module (557dbc1c-2a38-4c77-8544-264a95307980)
					// rel: Descriptor has ViewModelRef (9d771d87-3b28-4e5e-be33-ea71028e1720)
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
                case "DefaultGridCellKind":
                    __oldValue = _fk_DefaultGridCellKind;
                    NotifyPropertyChanging("DefaultGridCellKind", __oldValue, __newValue);
                    _fk_DefaultGridCellKind = __newValue;
                    NotifyPropertyChanged("DefaultGridCellKind", __oldValue, __newValue);
                    break;
                case "DefaultKind":
                    __oldValue = _fk_DefaultKind;
                    NotifyPropertyChanging("DefaultKind", __oldValue, __newValue);
                    _fk_DefaultKind = __newValue;
                    NotifyPropertyChanged("DefaultKind", __oldValue, __newValue);
                    break;
                case "Module":
                    __oldValue = _fk_Module;
                    NotifyPropertyChanging("Module", __oldValue, __newValue);
                    _fk_Module = __newValue;
                    NotifyPropertyChanged("Module", __oldValue, __newValue);
                    break;
                case "ViewModelRef":
                    __oldValue = _fk_ViewModelRef;
                    NotifyPropertyChanging("ViewModelRef", __oldValue, __newValue);
                    _fk_ViewModelRef = __newValue;
                    NotifyPropertyChanged("ViewModelRef", __oldValue, __newValue);
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
            BinarySerializer.ToStream(this._fk_DefaultGridCellKind, binStream);
            BinarySerializer.ToStream(this._fk_DefaultKind, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._fk_ViewModelRef, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_DefaultGridCellKind, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultKind, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_ViewModelRef, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._fk_DefaultGridCellKind, xml, "DefaultGridCellKind", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_DefaultKind, xml, "DefaultKind", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.GUI");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            }
            XmlStreamer.ToStream(this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_ViewModelRef, xml, "ViewModelRef", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_DefaultGridCellKind, xml, "DefaultGridCellKind", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._fk_DefaultKind, xml, "DefaultKind", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.GUI");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            }
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._fk_ViewModelRef, xml, "ViewModelRef", "http://dasz.at/Kistl");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            this._isExportGuidSet = true;
        }

#endregion

    }


}