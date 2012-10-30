// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class VisualMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, Visual
    {
        private static readonly Guid _objectClassID = new Guid("98790e5d-808f-4e0b-8a1a-b304839f07ab");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public VisualMemoryImpl()
            : base(null)
        {
        }

        public VisualMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        // collection entry list property
   		// Zetbox.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.GUI.Visual> Children
		{
			get
			{
				if (_Children == null)
				{
                    TriggerFetchChildrenAsync().Wait();
				}
				return (ICollection<Zetbox.App.GUI.Visual>)_Children;
			}
		}
        
        Zetbox.API.Async.ZbTask _triggerFetchChildrenTask;
        public Zetbox.API.Async.ZbTask TriggerFetchChildrenAsync()
        {
            if (_triggerFetchChildrenTask != null) return _triggerFetchChildrenTask;
			_triggerFetchChildrenTask = Context.FetchRelationAsync<Zetbox.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>(new Guid("4d4e1ffd-f362-40e2-9fe1-0711ded83241"), RelationEndRole.A, this);
			_triggerFetchChildrenTask.OnResult(r => 
            {
                _Children 
				= new ObservableBSideCollectionWrapper<Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl, ICollection<Zetbox.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>>(
					this, 
					new RelationshipFilterASideCollection<Zetbox.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>(this.Context, this));
            });
            return _triggerFetchChildrenTask;
        }

		private ObservableBSideCollectionWrapper<Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl, ICollection<Zetbox.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>> _Children;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.Visual> OnChildren_IsValid;

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
        // collection entry list property
   		// Zetbox.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.GUI.Visual> ContextMenu
		{
			get
			{
				if (_ContextMenu == null)
				{
                    TriggerFetchContextMenuAsync().Wait();
				}
				return (ICollection<Zetbox.App.GUI.Visual>)_ContextMenu;
			}
		}
        
        Zetbox.API.Async.ZbTask _triggerFetchContextMenuTask;
        public Zetbox.API.Async.ZbTask TriggerFetchContextMenuAsync()
        {
            if (_triggerFetchContextMenuTask != null) return _triggerFetchContextMenuTask;
			_triggerFetchContextMenuTask = Context.FetchRelationAsync<Zetbox.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>(new Guid("358c14b9-fef5-495d-8d44-04e84186830e"), RelationEndRole.A, this);
			_triggerFetchContextMenuTask.OnResult(r => 
            {
                _ContextMenu 
				= new ObservableBSideCollectionWrapper<Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl, ICollection<Zetbox.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>>(
					this, 
					new RelationshipFilterASideCollection<Zetbox.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>(this.Context, this));
            });
            return _triggerFetchContextMenuTask;
        }

		private ObservableBSideCollectionWrapper<Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual, Zetbox.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl, ICollection<Zetbox.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>> _ContextMenu;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.Visual> OnContextMenu_IsValid;

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Description
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
                    __result = _Description = __e.Result;
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
                    if (OnDescription_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    _Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDescription_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Description");
				}
            }
        }
        private string _Description;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.GUI.Visual, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.Visual, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.Visual, string> OnDescription_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.Visual> OnDescription_IsValid;

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
	        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Method
        // fkBackingName=_fk_Method; fkGuidBackingName=_fk_guid_Method;
        // referencedInterface=Zetbox.App.Base.Method; moduleNamespace=Zetbox.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.Method Method
        {
            get { return MethodImpl; }
            set { MethodImpl = (Zetbox.App.Base.MethodMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_Method;


        Zetbox.API.Async.ZbTask<Zetbox.App.Base.Method> _triggerFetchMethodTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.Base.Method> TriggerFetchMethodAsync()
        {
            if (_triggerFetchMethodTask != null) return _triggerFetchMethodTask;

            if (_fk_Method.HasValue)
                _triggerFetchMethodTask = Context.FindAsync<Zetbox.App.Base.Method>(_fk_Method.Value);
            else
                _triggerFetchMethodTask = new Zetbox.API.Async.ZbTask<Zetbox.App.Base.Method>(null, () => null);

            _triggerFetchMethodTask.OnResult(t =>
            {
                if (OnMethod_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Method>(t.Result);
                    OnMethod_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchMethodTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Base.MethodMemoryImpl MethodImpl
        {
            get
            {
                var t = TriggerFetchMethodAsync();
                t.Wait();
                return (Zetbox.App.Base.MethodMemoryImpl)t.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Method == null) || (value != null && value.ID == _fk_Method))
				{
					SetInitializedProperty("Method");
                    return;
				}

                // cache old value to remove inverse references later
                var __oldValue = MethodImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Method", __oldValue, __newValue);

                if (OnMethod_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Method>(__oldValue, __newValue);
                    OnMethod_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.MethodMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Method = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Method", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnMethod_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Method>(__oldValue, __newValue);
                    OnMethod_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Method
		public static event PropertyGetterHandler<Zetbox.App.GUI.Visual, Zetbox.App.Base.Method> OnMethod_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.Visual, Zetbox.App.Base.Method> OnMethod_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.Visual, Zetbox.App.Base.Method> OnMethod_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.Visual> OnMethod_IsValid;

        /// <summary>
        /// The Property to display
        /// </summary>
	        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
        // fkBackingName=_fk_Property; fkGuidBackingName=_fk_guid_Property;
        // referencedInterface=Zetbox.App.Base.Property; moduleNamespace=Zetbox.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.Property Property
        {
            get { return PropertyImpl; }
            set { PropertyImpl = (Zetbox.App.Base.PropertyMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_Property;


        Zetbox.API.Async.ZbTask<Zetbox.App.Base.Property> _triggerFetchPropertyTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.Base.Property> TriggerFetchPropertyAsync()
        {
            if (_triggerFetchPropertyTask != null) return _triggerFetchPropertyTask;

            if (_fk_Property.HasValue)
                _triggerFetchPropertyTask = Context.FindAsync<Zetbox.App.Base.Property>(_fk_Property.Value);
            else
                _triggerFetchPropertyTask = new Zetbox.API.Async.ZbTask<Zetbox.App.Base.Property>(null, () => null);

            _triggerFetchPropertyTask.OnResult(t =>
            {
                if (OnProperty_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Property>(t.Result);
                    OnProperty_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchPropertyTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Base.PropertyMemoryImpl PropertyImpl
        {
            get
            {
                var t = TriggerFetchPropertyAsync();
                t.Wait();
                return (Zetbox.App.Base.PropertyMemoryImpl)t.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Property == null) || (value != null && value.ID == _fk_Property))
				{
					SetInitializedProperty("Property");
                    return;
				}

                // cache old value to remove inverse references later
                var __oldValue = PropertyImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Property", __oldValue, __newValue);

                if (OnProperty_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.PropertyMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Property = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Property", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnProperty_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
		public static event PropertyGetterHandler<Zetbox.App.GUI.Visual, Zetbox.App.Base.Property> OnProperty_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.Visual, Zetbox.App.Base.Property> OnProperty_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.Visual, Zetbox.App.Base.Property> OnProperty_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.Visual> OnProperty_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Visual);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Visual)obj;
            var otherImpl = (VisualMemoryImpl)obj;
            var me = (Visual)this;

            me.Description = other.Description;
            this._fk_Method = otherImpl._fk_Method;
            this._fk_Property = otherImpl._fk_Property;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Method":
                    {
                        var __oldValue = _fk_Method;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Method", __oldValue, __newValue);
                        _fk_Method = __newValue;
                        NotifyPropertyChanged("Method", __oldValue, __newValue);
                    }
                    break;
                case "Property":
                    {
                        var __oldValue = _fk_Property;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Property", __oldValue, __newValue);
                        _fk_Property = __newValue;
                        NotifyPropertyChanged("Property", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Description":
                case "Method":
                case "Property":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Children":
                case "ContextMenu":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Method.HasValue)
                MethodImpl = (Zetbox.App.Base.MethodMemoryImpl)Context.Find<Zetbox.App.Base.Method>(_fk_Method.Value);
            else
                MethodImpl = null;

            if (_fk_Property.HasValue)
                PropertyImpl = (Zetbox.App.Base.PropertyMemoryImpl)Context.Find<Zetbox.App.Base.Property>(_fk_Property.Value);
            else
                PropertyImpl = null;
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Visual, ICollection<Zetbox.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("9f69c3bd-e274-4639-b30c-8d2a9599917b"),
                        "Children",
                        null,
                        obj => obj.Children,
                        null, // lists are read-only properties
                        obj => OnChildren_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Visual, ICollection<Zetbox.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("7b18f26e-0f3f-4554-b469-1029bd4ca10b"),
                        "ContextMenu",
                        null,
                        obj => obj.ContextMenu,
                        null, // lists are read-only properties
                        obj => OnContextMenu_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Visual, string>(
                        lazyCtx,
                        new Guid("8d3b7c91-2bbf-4dcf-bc37-318dc0fda92d"),
                        "Description",
                        null,
                        obj => obj.Description,
                        (obj, val) => obj.Description = val,
						obj => OnDescription_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Visual, Zetbox.App.Base.Method>(
                        lazyCtx,
                        new Guid("0b55b2ba-3ac0-4631-8a73-1e8846c8e9b1"),
                        "Method",
                        null,
                        obj => obj.Method,
                        (obj, val) => obj.Method = val,
						obj => OnMethod_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Visual, Zetbox.App.Base.Property>(
                        lazyCtx,
                        new Guid("a432e3ff-61ed-4726-8559-f34516181065"),
                        "Property",
                        null,
                        obj => obj.Property,
                        (obj, val) => obj.Property = val,
						obj => OnProperty_IsValid), 
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
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Visual")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Visual != null)
            {
                OnToString_Visual(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Visual> OnToString_Visual;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Visual")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Visual != null)
            {
                OnObjectIsValid_Visual(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Visual> OnObjectIsValid_Visual;

        [EventBasedMethod("OnNotifyPreSave_Visual")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Visual != null) OnNotifyPreSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyPreSave_Visual;

        [EventBasedMethod("OnNotifyPostSave_Visual")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Visual != null) OnNotifyPostSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyPostSave_Visual;

        [EventBasedMethod("OnNotifyCreated_Visual")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Description");
            SetNotInitializedProperty("Method");
            SetNotInitializedProperty("Property");
            base.NotifyCreated();
            if (OnNotifyCreated_Visual != null) OnNotifyCreated_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyCreated_Visual;

        [EventBasedMethod("OnNotifyDeleting_Visual")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Visual != null) OnNotifyDeleting_Visual(this);
            Children.Clear();
            ContextMenu.Clear();
            Method = null;
            Property = null;
        }
        public static event ObjectEventHandler<Visual> OnNotifyDeleting_Visual;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Description);
            binStream.Write(Method != null ? Method.ID : (int?)null);
            binStream.Write(Property != null ? Property.ID : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Description = binStream.ReadString();
            this._fk_Method = binStream.ReadNullableInt32();
            this._fk_Property = binStream.ReadNullableInt32();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}