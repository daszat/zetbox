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
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Template")]
    public class TemplateMemoryImpl : Kistl.DalProvider.Memory.DataObjectMemoryImpl, Template
    {
        private static readonly Guid _objectClassID = new Guid("c677d3fe-7dfe-4ea5-91e0-d1b0df9118be");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public TemplateMemoryImpl()
            : base(null)
        {
        }

        public TemplateMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
	        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DisplayedTypeAssembly
        // fkBackingName=_fk_DisplayedTypeAssembly; fkGuidBackingName=_fk_guid_DisplayedTypeAssembly;
        // referencedInterface=Kistl.App.Base.Assembly; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.Base.Assembly DisplayedTypeAssembly
        {
            get { return DisplayedTypeAssemblyImpl; }
            set { DisplayedTypeAssemblyImpl = (Kistl.App.Base.AssemblyMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_DisplayedTypeAssembly;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.Base.AssemblyMemoryImpl DisplayedTypeAssemblyImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.AssemblyMemoryImpl __value;
                if (_fk_DisplayedTypeAssembly.HasValue)
                    __value = (Kistl.App.Base.AssemblyMemoryImpl)Context.Find<Kistl.App.Base.Assembly>(_fk_DisplayedTypeAssembly.Value);
                else
                    __value = null;

                if (OnDisplayedTypeAssembly_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Assembly>(__value);
                    OnDisplayedTypeAssembly_Getter(this, e);
                    __value = (Kistl.App.Base.AssemblyMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_DisplayedTypeAssembly == null)
                    return;
                else if (value != null && value.ID == _fk_DisplayedTypeAssembly)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = DisplayedTypeAssemblyImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("DisplayedTypeAssembly", __oldValue, __newValue);

                if (OnDisplayedTypeAssembly_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Assembly>(__oldValue, __newValue);
                    OnDisplayedTypeAssembly_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.AssemblyMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_DisplayedTypeAssembly = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("DisplayedTypeAssembly", __oldValue, __newValue);

                if (OnDisplayedTypeAssembly_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Assembly>(__oldValue, __newValue);
                    OnDisplayedTypeAssembly_PostSetter(this, e);
                }
            }
        }
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DisplayedTypeAssembly
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, Kistl.App.Base.Assembly> OnDisplayedTypeAssembly_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, Kistl.App.Base.Assembly> OnDisplayedTypeAssembly_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, Kistl.App.Base.Assembly> OnDisplayedTypeAssembly_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Template> OnDisplayedTypeAssembly_IsValid;

        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string DisplayedTypeFullName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DisplayedTypeFullName;
                if (OnDisplayedTypeFullName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDisplayedTypeFullName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayedTypeFullName != value)
                {
                    var __oldValue = _DisplayedTypeFullName;
                    var __newValue = value;
                    if (OnDisplayedTypeFullName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayedTypeFullName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DisplayedTypeFullName", __oldValue, __newValue);
                    _DisplayedTypeFullName = __newValue;
                    NotifyPropertyChanged("DisplayedTypeFullName", __oldValue, __newValue);

                    if (OnDisplayedTypeFullName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayedTypeFullName_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("DisplayedTypeFullName");
				}
            }
        }
        private string _DisplayedTypeFullName;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, string> OnDisplayedTypeFullName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, string> OnDisplayedTypeFullName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, string> OnDisplayedTypeFullName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Template> OnDisplayedTypeFullName_IsValid;

        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string DisplayName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DisplayName;
                if (OnDisplayName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDisplayName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayName != value)
                {
                    var __oldValue = _DisplayName;
                    var __newValue = value;
                    if (OnDisplayName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DisplayName", __oldValue, __newValue);
                    _DisplayName = __newValue;
                    NotifyPropertyChanged("DisplayName", __oldValue, __newValue);

                    if (OnDisplayName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("DisplayName");
				}
            }
        }
        private string _DisplayName;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, string> OnDisplayName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, string> OnDisplayName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, string> OnDisplayName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Template> OnDisplayName_IsValid;

        /// <summary>
        /// The main menu for this Template
        /// </summary>
        // collection entry list property
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> Menu
		{
			get
			{
				if (_Menu == null)
				{
					Context.FetchRelation<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryMemoryImpl>(new Guid("81ff3089-57da-478c-8be5-fd23abc222a2"), RelationEndRole.A, this);
					_Menu 
						= new ObservableBSideCollectionWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryMemoryImpl, ICollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (ICollection<Kistl.App.GUI.Visual>)_Menu;
			}
		}

		private ObservableBSideCollectionWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryMemoryImpl, ICollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryMemoryImpl>> _Menu;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Template> OnMenu_IsValid;

        /// <summary>
        /// The visual representation of this Template
        /// </summary>
	        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for VisualTree
        // fkBackingName=_fk_VisualTree; fkGuidBackingName=_fk_guid_VisualTree;
        // referencedInterface=Kistl.App.GUI.Visual; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.GUI.Visual VisualTree
        {
            get { return VisualTreeImpl; }
            set { VisualTreeImpl = (Kistl.App.GUI.VisualMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_VisualTree;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.GUI.VisualMemoryImpl VisualTreeImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.GUI.VisualMemoryImpl __value;
                if (_fk_VisualTree.HasValue)
                    __value = (Kistl.App.GUI.VisualMemoryImpl)Context.Find<Kistl.App.GUI.Visual>(_fk_VisualTree.Value);
                else
                    __value = null;

                if (OnVisualTree_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.GUI.Visual>(__value);
                    OnVisualTree_Getter(this, e);
                    __value = (Kistl.App.GUI.VisualMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_VisualTree == null)
                    return;
                else if (value != null && value.ID == _fk_VisualTree)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = VisualTreeImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("VisualTree", __oldValue, __newValue);

                if (OnVisualTree_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.Visual>(__oldValue, __newValue);
                    OnVisualTree_PreSetter(this, e);
                    __newValue = (Kistl.App.GUI.VisualMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_VisualTree = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("VisualTree", __oldValue, __newValue);

                if (OnVisualTree_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.Visual>(__oldValue, __newValue);
                    OnVisualTree_PostSetter(this, e);
                }
            }
        }
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for VisualTree
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, Kistl.App.GUI.Visual> OnVisualTree_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, Kistl.App.GUI.Visual> OnVisualTree_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, Kistl.App.GUI.Visual> OnVisualTree_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Template> OnVisualTree_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnPrepareDefault_Template")]
        public virtual void PrepareDefault(Kistl.App.Base.ObjectClass cls)
        {
            // base.PrepareDefault();
            if (OnPrepareDefault_Template != null)
            {
                OnPrepareDefault_Template(this, cls);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method Template.PrepareDefault");
            }
        }
        public delegate void PrepareDefault_Handler<T>(T obj, Kistl.App.Base.ObjectClass cls);
        public static event PrepareDefault_Handler<Template> OnPrepareDefault_Template;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Template> OnPrepareDefault_Template_CanExec;

        [EventBasedMethod("OnPrepareDefault_Template_CanExec")]
        public virtual bool PrepareDefaultCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnPrepareDefault_Template_CanExec != null)
				{
					OnPrepareDefault_Template_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Template> OnPrepareDefault_Template_CanExecReason;

        [EventBasedMethod("OnPrepareDefault_Template_CanExecReason")]
        public virtual string PrepareDefaultCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnPrepareDefault_Template_CanExecReason != null)
				{
					OnPrepareDefault_Template_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(Template);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Template)obj;
            var otherImpl = (TemplateMemoryImpl)obj;
            var me = (Template)this;

            me.DisplayedTypeFullName = other.DisplayedTypeFullName;
            me.DisplayName = other.DisplayName;
            this._fk_DisplayedTypeAssembly = otherImpl._fk_DisplayedTypeAssembly;
            this._fk_VisualTree = otherImpl._fk_VisualTree;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "DisplayedTypeAssembly":
                    {
                        var __oldValue = _fk_DisplayedTypeAssembly;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("DisplayedTypeAssembly", __oldValue, __newValue);
                        _fk_DisplayedTypeAssembly = __newValue;
                        NotifyPropertyChanged("DisplayedTypeAssembly", __oldValue, __newValue);
                    }
                    break;
                case "VisualTree":
                    {
                        var __oldValue = _fk_VisualTree;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("VisualTree", __oldValue, __newValue);
                        _fk_VisualTree = __newValue;
                        NotifyPropertyChanged("VisualTree", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "DisplayedTypeAssembly":
                case "DisplayedTypeFullName":
                case "DisplayName":
                case "VisualTree":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_DisplayedTypeAssembly.HasValue)
                DisplayedTypeAssemblyImpl = (Kistl.App.Base.AssemblyMemoryImpl)Context.Find<Kistl.App.Base.Assembly>(_fk_DisplayedTypeAssembly.Value);
            else
                DisplayedTypeAssemblyImpl = null;

            if (_fk_VisualTree.HasValue)
                VisualTreeImpl = (Kistl.App.GUI.VisualMemoryImpl)Context.Find<Kistl.App.GUI.Visual>(_fk_VisualTree.Value);
            else
                VisualTreeImpl = null;
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // else
                    new PropertyDescriptorMemoryImpl<Template, Kistl.App.Base.Assembly>(
                        lazyCtx,
                        new Guid("c81105da-97e4-4685-af88-792c68e55a17"),
                        "DisplayedTypeAssembly",
                        null,
                        obj => obj.DisplayedTypeAssembly,
                        (obj, val) => obj.DisplayedTypeAssembly = val,
						obj => OnDisplayedTypeAssembly_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Template, string>(
                        lazyCtx,
                        new Guid("4b683aa1-45a9-4c5e-80e7-0ff30f5b798c"),
                        "DisplayedTypeFullName",
                        null,
                        obj => obj.DisplayedTypeFullName,
                        (obj, val) => obj.DisplayedTypeFullName = val,
						obj => OnDisplayedTypeFullName_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Template, string>(
                        lazyCtx,
                        new Guid("4fc51781-b0fe-495c-91a1-90e484345515"),
                        "DisplayName",
                        null,
                        obj => obj.DisplayName,
                        (obj, val) => obj.DisplayName = val,
						obj => OnDisplayName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Template, ICollection<Kistl.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("5e9612d5-019a-416b-a2e2-dfc9674a50f6"),
                        "Menu",
                        null,
                        obj => obj.Menu,
                        null, // lists are read-only properties
                        obj => OnMenu_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Template, Kistl.App.GUI.Visual>(
                        lazyCtx,
                        new Guid("5d2880a4-716a-4bdc-aaa9-379c006e7ed4"),
                        "VisualTree",
                        null,
                        obj => obj.VisualTree,
                        (obj, val) => obj.VisualTree = val,
						obj => OnVisualTree_IsValid), 
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Template")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Template != null)
            {
                OnToString_Template(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Template> OnToString_Template;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Template")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Template != null)
            {
                OnObjectIsValid_Template(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Template> OnObjectIsValid_Template;

        [EventBasedMethod("OnNotifyPreSave_Template")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Template != null) OnNotifyPreSave_Template(this);
        }
        public static event ObjectEventHandler<Template> OnNotifyPreSave_Template;

        [EventBasedMethod("OnNotifyPostSave_Template")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Template != null) OnNotifyPostSave_Template(this);
        }
        public static event ObjectEventHandler<Template> OnNotifyPostSave_Template;

        [EventBasedMethod("OnNotifyCreated_Template")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DisplayedTypeAssembly");
            SetNotInitializedProperty("DisplayedTypeFullName");
            SetNotInitializedProperty("DisplayName");
            SetNotInitializedProperty("VisualTree");
            base.NotifyCreated();
            if (OnNotifyCreated_Template != null) OnNotifyCreated_Template(this);
        }
        public static event ObjectEventHandler<Template> OnNotifyCreated_Template;

        [EventBasedMethod("OnNotifyDeleting_Template")]
        public override void NotifyDeleting()
        {
            Menu.Clear();
            DisplayedTypeAssembly = null;
            VisualTree = null;
            base.NotifyDeleting();
            if (OnNotifyDeleting_Template != null) OnNotifyDeleting_Template(this);
        }
        public static event ObjectEventHandler<Template> OnNotifyDeleting_Template;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(DisplayedTypeAssembly != null ? DisplayedTypeAssembly.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._DisplayedTypeFullName, binStream);
            BinarySerializer.ToStream(this._DisplayName, binStream);
            BinarySerializer.ToStream(VisualTree != null ? VisualTree.ID : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.FromStream(out this._DisplayedTypeFullName, binStream);
            BinarySerializer.FromStream(out this._DisplayName, binStream);
            BinarySerializer.FromStream(out this._fk_VisualTree, binStream);
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(DisplayedTypeAssembly != null ? DisplayedTypeAssembly.ID : (int?)null, xml, "DisplayedTypeAssembly", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._DisplayedTypeFullName, xml, "DisplayedTypeFullName", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._DisplayName, xml, "DisplayName", "Kistl.App.GUI");
            XmlStreamer.ToStream(VisualTree != null ? VisualTree.ID : (int?)null, xml, "VisualTree", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_DisplayedTypeAssembly, xml, "DisplayedTypeAssembly", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._DisplayedTypeFullName, xml, "DisplayedTypeFullName", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._DisplayName, xml, "DisplayName", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_VisualTree, xml, "VisualTree", "Kistl.App.GUI");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}