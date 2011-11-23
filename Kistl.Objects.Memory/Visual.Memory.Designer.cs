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
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class VisualMemoryImpl : Kistl.DalProvider.Memory.DataObjectMemoryImpl, Visual
    {
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
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> Children
		{
			get
			{
				if (_Children == null)
				{
					Context.FetchRelation<Kistl.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>(new Guid("4d4e1ffd-f362-40e2-9fe1-0711ded83241"), RelationEndRole.A, this);
					_Children 
						= new ObservableBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl, ICollection<Kistl.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<Kistl.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (ICollection<Kistl.App.GUI.Visual>)_Children;
			}
		}

		private ObservableBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl, ICollection<Kistl.App.GUI.Visual_contains_Visual_RelationEntryMemoryImpl>> _Children;

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
        // collection entry list property
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> ContextMenu
		{
			get
			{
				if (_ContextMenu == null)
				{
					Context.FetchRelation<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>(new Guid("358c14b9-fef5-495d-8d44-04e84186830e"), RelationEndRole.A, this);
					_ContextMenu 
						= new ObservableBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl, ICollection<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (ICollection<Kistl.App.GUI.Visual>)_ContextMenu;
			}
		}

		private ObservableBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl, ICollection<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryMemoryImpl>> _ContextMenu;

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string Description
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
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
                    if (OnDescription_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    _Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);
                    if (OnDescription_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Description;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, string> OnDescription_PostSetter;

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Method
        // fkBackingName=_fk_Method; fkGuidBackingName=_fk_guid_Method;
        // referencedInterface=Kistl.App.Base.Method; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.Base.Method Method
        {
            get { return MethodImpl; }
            set { MethodImpl = (Kistl.App.Base.MethodMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_Method;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.Base.MethodMemoryImpl MethodImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.MethodMemoryImpl __value;
                if (_fk_Method.HasValue)
                    __value = (Kistl.App.Base.MethodMemoryImpl)Context.Find<Kistl.App.Base.Method>(_fk_Method.Value);
                else
                    __value = null;

                if (OnMethod_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Method>(__value);
                    OnMethod_Getter(this, e);
                    __value = (Kistl.App.Base.MethodMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_Method == null)
                    return;
                else if (value != null && value.ID == _fk_Method)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = MethodImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Method", __oldValue, __newValue);

                if (OnMethod_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Method>(__oldValue, __newValue);
                    OnMethod_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.MethodMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Method = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Method", __oldValue, __newValue);

                if (OnMethod_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Method>(__oldValue, __newValue);
                    OnMethod_PostSetter(this, e);
                }
            }
        }
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Method
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_PostSetter;

        /// <summary>
        /// The Property to display
        /// </summary>
        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
        // fkBackingName=_fk_Property; fkGuidBackingName=_fk_guid_Property;
        // referencedInterface=Kistl.App.Base.Property; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.Base.Property Property
        {
            get { return PropertyImpl; }
            set { PropertyImpl = (Kistl.App.Base.PropertyMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_Property;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.Base.PropertyMemoryImpl PropertyImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.PropertyMemoryImpl __value;
                if (_fk_Property.HasValue)
                    __value = (Kistl.App.Base.PropertyMemoryImpl)Context.Find<Kistl.App.Base.Property>(_fk_Property.Value);
                else
                    __value = null;

                if (OnProperty_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Property>(__value);
                    OnProperty_Getter(this, e);
                    __value = (Kistl.App.Base.PropertyMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_Property == null)
                    return;
                else if (value != null && value.ID == _fk_Property)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = PropertyImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Property", __oldValue, __newValue);

                if (OnProperty_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.PropertyMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Property = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Property", __oldValue, __newValue);

                if (OnProperty_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PostSetter(this, e);
                }
            }
        }
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_PostSetter;

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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Method.HasValue)
                MethodImpl = (Kistl.App.Base.MethodMemoryImpl)Context.Find<Kistl.App.Base.Method>(_fk_Method.Value);
            else
                MethodImpl = null;

            if (_fk_Property.HasValue)
                PropertyImpl = (Kistl.App.Base.PropertyMemoryImpl)Context.Find<Kistl.App.Base.Property>(_fk_Property.Value);
            else
                PropertyImpl = null;
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<VisualMemoryImpl, ICollection<Kistl.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("9f69c3bd-e274-4639-b30c-8d2a9599917b"),
                        "Children",
                        null,
                        obj => obj.Children,
                        null), // lists are read-only properties
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<VisualMemoryImpl, ICollection<Kistl.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("7b18f26e-0f3f-4554-b469-1029bd4ca10b"),
                        "ContextMenu",
                        null,
                        obj => obj.ContextMenu,
                        null), // lists are read-only properties
                    // else
                    new PropertyDescriptorMemoryImpl<VisualMemoryImpl, string>(
                        lazyCtx,
                        new Guid("8d3b7c91-2bbf-4dcf-bc37-318dc0fda92d"),
                        "Description",
                        null,
                        obj => obj.Description,
                        (obj, val) => obj.Description = val),
                    // else
                    new PropertyDescriptorMemoryImpl<VisualMemoryImpl, Kistl.App.Base.Method>(
                        lazyCtx,
                        new Guid("0b55b2ba-3ac0-4631-8a73-1e8846c8e9b1"),
                        "Method",
                        null,
                        obj => obj.Method,
                        (obj, val) => obj.Method = val),
                    // else
                    new PropertyDescriptorMemoryImpl<VisualMemoryImpl, Kistl.App.Base.Property>(
                        lazyCtx,
                        new Guid("a432e3ff-61ed-4726-8559-f34516181065"),
                        "Property",
                        null,
                        obj => obj.Property,
                        (obj, val) => obj.Property = val),
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

        [EventBasedMethod("OnPreSave_Visual")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Visual != null) OnPreSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnPreSave_Visual;

        [EventBasedMethod("OnPostSave_Visual")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Visual != null) OnPostSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnPostSave_Visual;

        [EventBasedMethod("OnCreated_Visual")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Visual != null) OnCreated_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnCreated_Visual;

        [EventBasedMethod("OnDeleting_Visual")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Visual != null) OnDeleting_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnDeleting_Visual;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(Method != null ? Method.ID : (int?)null, binStream);
            BinarySerializer.ToStream(Property != null ? Property.ID : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._fk_Property, binStream);
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
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(Method != null ? Method.ID : (int?)null, xml, "Method", "Kistl.App.GUI");
            XmlStreamer.ToStream(Property != null ? Property.ID : (int?)null, xml, "Property", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_Method, xml, "Method", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_Property, xml, "Property", "Kistl.App.GUI");
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