// <autogenerated/>

namespace Zetbox.App.Test
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

    using Zetbox.API.Utils;
    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.NHibernate;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SecurityTestChild")]
    public class SecurityTestChildNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, SecurityTestChild
    {
        private static readonly Guid _objectClassID = new Guid("09dfa3cf-4a15-48ed-b76a-c330ac7379e0");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public SecurityTestChildNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public SecurityTestChildNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new SecurityTestChildProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public SecurityTestChildNHibernateImpl(Func<IFrozenContext> lazyCtx, SecurityTestChildProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly SecurityTestChildProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Identity
        // fkBackingName=this.Proxy.Identity; fkGuidBackingName=_fk_guid_Identity;
        // referencedInterface=Zetbox.App.Base.Identity; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.Identity Identity
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Zetbox.App.Base.IdentityNHibernateImpl __value = (Zetbox.App.Base.IdentityNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Identity);

                if (OnIdentity_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Identity>(__value);
                    OnIdentity_Getter(this, e);
                    __value = (Zetbox.App.Base.IdentityNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Identity == null)
				{
					SetInitializedProperty("Identity");
                    return;
				}

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Base.IdentityNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Identity);
                var __newValue = (Zetbox.App.Base.IdentityNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
				{
					SetInitializedProperty("Identity");
                    return;
				}

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Identity", __oldValue, __newValue);

                if (OnIdentity_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Identity>(__oldValue, __newValue);
                    OnIdentity_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.IdentityNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Identity = null;
                }
                else
                {
                    this.Proxy.Identity = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Identity", __oldValue, __newValue);

                if (OnIdentity_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Identity>(__oldValue, __newValue);
                    OnIdentity_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Identity's id, used on dehydration only</summary>
        private int? _fk_Identity = null;


        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Identity
		public static event PropertyGetterHandler<Zetbox.App.Test.SecurityTestChild, Zetbox.App.Base.Identity> OnIdentity_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.SecurityTestChild, Zetbox.App.Base.Identity> OnIdentity_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.SecurityTestChild, Zetbox.App.Base.Identity> OnIdentity_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.SecurityTestChild> OnIdentity_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Name
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Name;
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
                if (Proxy.Name != value)
                {
                    var __oldValue = Proxy.Name;
                    var __newValue = value;
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    Proxy.Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);

                    if (OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Name");
				}
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.SecurityTestChild, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.SecurityTestChild, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.SecurityTestChild, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.SecurityTestChild> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Parent
        // fkBackingName=this.Proxy.Parent; fkGuidBackingName=_fk_guid_Parent;
        // referencedInterface=Zetbox.App.Test.SecurityTestParent; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=Children; is list;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Test.SecurityTestParent Parent
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Zetbox.App.Test.SecurityTestParentNHibernateImpl __value = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Parent);

                if (OnParent_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Test.SecurityTestParent>(__value);
                    OnParent_Getter(this, e);
                    __value = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Parent == null)
				{
					SetInitializedProperty("Parent");
                    return;
				}

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Parent);
                var __newValue = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
				{
					SetInitializedProperty("Parent");
                    return;
				}

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Parent", __oldValue, __newValue);

                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Children", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Children", null, null);
                }

                if (OnParent_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Test.SecurityTestParent>(__oldValue, __newValue);
                    OnParent_PreSetter(this, e);
                    __newValue = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Parent = null;
                }
                else
                {
                    this.Proxy.Parent = __newValue.Proxy;
                }

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.Children as IRelationListSync<Zetbox.App.Test.SecurityTestChild>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.Children as IRelationListSync<Zetbox.App.Test.SecurityTestChild>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Parent", __oldValue, __newValue);

                if (OnParent_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Test.SecurityTestParent>(__oldValue, __newValue);
                    OnParent_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Parent's id, used on dehydration only</summary>
        private int? _fk_Parent = null;


        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Parent
		public static event PropertyGetterHandler<Zetbox.App.Test.SecurityTestChild, Zetbox.App.Test.SecurityTestParent> OnParent_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.SecurityTestChild, Zetbox.App.Test.SecurityTestParent> OnParent_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.SecurityTestChild, Zetbox.App.Test.SecurityTestParent> OnParent_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.SecurityTestChild> OnParent_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string ParentName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchParentNameOrDefault();
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.ParentName != value)
                {
                    var __oldValue = Proxy.ParentName;
                    var __newValue = value;
                    NotifyPropertyChanging("ParentName", __oldValue, __newValue);
                    Proxy.ParentName = __newValue;
                    NotifyPropertyChanged("ParentName", __oldValue, __newValue);
			        _ParentName_IsDirty = false;

                }
				else 
				{
					SetInitializedProperty("ParentName");
				}
            }
        }
		private bool _ParentName_IsDirty = false;


        private string FetchParentNameOrDefault()
        {
           var __result = Proxy.ParentName;
            if (_ParentName_IsDirty && OnParentName_Getter != null)
            {
                var __e = new PropertyGetterEventArgs<string>(__result);
                OnParentName_Getter(this, __e);
                _ParentName_IsDirty = false;
                __result = Proxy.ParentName = __e.Result;
            }
            return __result;
        }

        private bool _isParentNameSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.SecurityTestChild, string> OnParentName_Getter;

        public override Type GetImplementedInterface()
        {
            return typeof(SecurityTestChild);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (SecurityTestChild)obj;
            var otherImpl = (SecurityTestChildNHibernateImpl)obj;
            var me = (SecurityTestChild)this;

            me.Name = other.Name;
            this._fk_Identity = otherImpl._fk_Identity;
            this._fk_Parent = otherImpl._fk_Parent;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }
        public override void SetNew()
        {
            base.SetNew();
            _ParentName_IsDirty = true;
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Identity":
                    {
                        var __oldValue = (Zetbox.App.Base.IdentityNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Identity);
                        var __newValue = (Zetbox.App.Base.IdentityNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Identity", __oldValue, __newValue);
                        this.Proxy.Identity = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Identity", __oldValue, __newValue);
                    }
                    break;
                case "Parent":
                    {
                        var __oldValue = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Parent);
                        var __newValue = (Zetbox.App.Test.SecurityTestParentNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Parent", __oldValue, __newValue);
                        this.Proxy.Parent = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Parent", __oldValue, __newValue);
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
                case "Identity":
                case "Name":
                case "Parent":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        public override void Recalculate(string property)
        {
            switch (property)
            {
                case "ParentName":
                    NotifyPropertyChanging(property, null, null);
                    _ParentName_IsDirty = true;
                    NotifyPropertyChanged(property, null, null);
                    return;
            }

            base.Recalculate(property);
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Identity.HasValue)
                this.Identity = ((Zetbox.App.Base.IdentityNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Base.Identity>(_fk_Identity.Value));
            else
                this.Identity = null;

            if (_fk_Parent.HasValue)
                this.Parent = ((Zetbox.App.Test.SecurityTestParentNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Test.SecurityTestParent>(_fk_Parent.Value));
            else
                this.Parent = null;
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
                    // else
                    new PropertyDescriptorNHibernateImpl<SecurityTestChild, Zetbox.App.Base.Identity>(
                        lazyCtx,
                        new Guid("9119f41f-0767-47c0-ae71-5cd3f897b477"),
                        "Identity",
                        null,
                        obj => obj.Identity,
                        (obj, val) => obj.Identity = val,
						obj => OnIdentity_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<SecurityTestChild, string>(
                        lazyCtx,
                        new Guid("4716ffd4-5aa2-466d-abfe-1bcb20a090fc"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<SecurityTestChild, Zetbox.App.Test.SecurityTestParent>(
                        lazyCtx,
                        new Guid("9bdf1c7f-16d1-4e92-9024-7447d3b2c9ec"),
                        "Parent",
                        null,
                        obj => obj.Parent,
                        (obj, val) => obj.Parent = val,
						obj => OnParent_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<SecurityTestChild, string>(
                        lazyCtx,
                        new Guid("ae267bd6-e435-4186-b374-ab19db6488b3"),
                        "ParentName",
                        null,
                        obj => obj.ParentName,
                        null, // calculated property
						null), // no constraints on calculated properties
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
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_SecurityTestChild")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_SecurityTestChild != null)
            {
                OnToString_SecurityTestChild(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<SecurityTestChild> OnToString_SecurityTestChild;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_SecurityTestChild")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_SecurityTestChild != null)
            {
                OnObjectIsValid_SecurityTestChild(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<SecurityTestChild> OnObjectIsValid_SecurityTestChild;

        [EventBasedMethod("OnNotifyPreSave_SecurityTestChild")]
        public override void NotifyPreSave()
        {
            FetchParentNameOrDefault();
            base.NotifyPreSave();
            if (OnNotifyPreSave_SecurityTestChild != null) OnNotifyPreSave_SecurityTestChild(this);
        }
        public static event ObjectEventHandler<SecurityTestChild> OnNotifyPreSave_SecurityTestChild;

        [EventBasedMethod("OnNotifyPostSave_SecurityTestChild")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_SecurityTestChild != null) OnNotifyPostSave_SecurityTestChild(this);
        }
        public static event ObjectEventHandler<SecurityTestChild> OnNotifyPostSave_SecurityTestChild;

        [EventBasedMethod("OnNotifyCreated_SecurityTestChild")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Identity");
            SetNotInitializedProperty("Name");
            SetNotInitializedProperty("Parent");
            _ParentName_IsDirty = true;
            base.NotifyCreated();
            if (OnNotifyCreated_SecurityTestChild != null) OnNotifyCreated_SecurityTestChild(this);
        }
        public static event ObjectEventHandler<SecurityTestChild> OnNotifyCreated_SecurityTestChild;

        [EventBasedMethod("OnNotifyDeleting_SecurityTestChild")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_SecurityTestChild != null) OnNotifyDeleting_SecurityTestChild(this);

            // FK_Child_allow_Identity
            if (Identity != null) {
                ((NHibernatePersistenceObject)Identity).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)Identity);
            }
            // FK_Parent_has_Children
            if (Parent != null) {
                ((NHibernatePersistenceObject)Parent).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)Parent);
            }

            Parent = null;
        }
        public static event ObjectEventHandler<SecurityTestChild> OnNotifyDeleting_SecurityTestChild;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class SecurityTestChildProxy
            : IProxyObject, ISortKey<int>
        {
            public SecurityTestChildProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(SecurityTestChildNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(SecurityTestChildProxy); } }

            public virtual Zetbox.App.Base.IdentityNHibernateImpl.IdentityProxy Identity { get; set; }

            public virtual string Name { get; set; }

            public virtual Zetbox.App.Test.SecurityTestParentNHibernateImpl.SecurityTestParentProxy Parent { get; set; }

            public virtual string ParentName { get; set; }

            public virtual ICollection<SecurityTestChild_RightsNHibernateImpl> SecurityRightsCollectionImpl { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        private Zetbox.API.AccessRights? __currentAccessRights;
        public override Zetbox.API.AccessRights CurrentAccessRights
        {
           get { 
             if(Context == null) return Zetbox.API.AccessRights.Full;
             if(__currentAccessRights == null) { 
                 __currentAccessRights = base.CurrentAccessRights; 
                 var secRight = this.Proxy.SecurityRightsCollectionImpl != null ? this.Proxy.SecurityRightsCollectionImpl.SingleOrDefault(i => i.Identity == Context.Internals().IdentityID) : null;
                 __currentAccessRights |= secRight != null ? (Zetbox.API.AccessRights)secRight.Right : Zetbox.API.AccessRights.None; 
             } 
             return __currentAccessRights.Value; }
        }


        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.Identity != null ? OurContext.GetIdFromProxy(this.Proxy.Identity) : (int?)null);
            binStream.Write(this.Proxy.Name);
            binStream.Write(this.Proxy.Parent != null ? OurContext.GetIdFromProxy(this.Proxy.Parent) : (int?)null);
            binStream.Write(this.Proxy.ParentName);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_Identity);
            this.Proxy.Name = binStream.ReadString();
            binStream.Read(out this._fk_Parent);
            this.Proxy.ParentName = binStream.ReadString();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }

        public class SecurityTestChild_RightsNHibernateImpl
        {
            public SecurityTestChild_RightsNHibernateImpl()
            {
            }

            public virtual int ID { get; set; }
            public virtual int Identity { get; set; }
            public virtual int Right { get; set; }
        }
}