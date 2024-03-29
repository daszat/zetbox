// <autogenerated/>

namespace Zetbox.App.Base
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
    [System.Diagnostics.DebuggerDisplay("Group")]
    public class GroupNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, Group, Zetbox.API.IExportableInternal
    {
        private static readonly Guid _objectClassID = new Guid("01a06aef-8fe4-4cb6-b348-ee4bcd11f5af");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public GroupNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public GroupNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new GroupProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public GroupNHibernateImpl(Func<IFrozenContext> lazyCtx, GroupProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
            _isExportGuidSet = Proxy.ID > 0;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly GroupProxy Proxy;

        /// <summary>
        /// Export Guid
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid ExportGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchExportGuidOrDefault();
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
                if (Proxy.ExportGuid != value)
                {
                    var __oldValue = Proxy.ExportGuid;
                    var __newValue = value;
                    if (OnExportGuid_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    Proxy.ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnExportGuid_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("ExportGuid");
                }
            }
        }


        private Guid FetchExportGuidOrDefault()
        {
            var __result = Proxy.ExportGuid;
                if (!_isExportGuidSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("c776e87f-2b95-466e-848e-0ce195f4bd73"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this.Proxy.ExportGuid = (Guid)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Zetbox.App.Base.Group.ExportGuid'");
                    }
                }
            return __result;
        }

        private bool _isExportGuidSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.Group, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.Group, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.Group, Guid> OnExportGuid_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.Group> OnExportGuid_IsValid;

        /// <summary>
        /// Identities are member of this group
        /// </summary>
        // collection entry list property
   		// Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.Base.Identity> Member
		{
			get
			{
				if (_Member == null)
				{
					_Member 
						= new NHibernateASideCollectionWrapper<Zetbox.App.Base.Identity, Zetbox.App.Base.Group, Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl.Identities_memberOf_Groups_RelationEntryProxy, Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl>(
                                () => this.Proxy.Member,
                                p => (Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl.Identities_memberOf_Groups_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy));
                    _Member.CollectionChanged += (s, e) => { this.NotifyPropertyChanged("Member", null, null); if(OnMember_PostSetter != null && IsAttached) OnMember_PostSetter(this); };
                    if (Member_was_eagerLoaded) { Member_was_eagerLoaded = false; }
				}
				return (ICollection<Zetbox.App.Base.Identity>)_Member;
			}
		}

        public async System.Threading.Tasks.Task<ICollection<Zetbox.App.Base.Identity>> GetProp_Member()
        {
            await TriggerFetchMemberAsync();
            return _Member;
        }

		private NHibernateASideCollectionWrapper<Zetbox.App.Base.Identity, Zetbox.App.Base.Group, Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl> _Member;
		// ignored, but required for Serialization
        private bool Member_was_eagerLoaded = false;

        public System.Threading.Tasks.Task TriggerFetchMemberAsync()
        {
            return System.Threading.Tasks.Task.FromResult<ICollection<Zetbox.App.Base.Identity>>(this.Member);
        }

public static event PropertyListChangedHandler<Zetbox.App.Base.Group> OnMember_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.Group> OnMember_IsValid;

        /// <summary>
        /// Module is optional. Use it only when you need a Group to be exportable
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Module
        // fkBackingName=this.Proxy.Module; fkGuidBackingName=_fk_guid_Module;
        // referencedInterface=Zetbox.App.Base.Module; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable; does call events
        
        public System.Threading.Tasks.Task<Zetbox.App.Base.Module> GetProp_Module()
        {
            return System.Threading.Tasks.Task.FromResult(Module);
        }

        public async System.Threading.Tasks.Task SetProp_Module(Zetbox.App.Base.Module newValue)
        {
            await TriggerFetchModuleAsync();
            Module = newValue;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public Zetbox.App.Base.Module Module
        {
            get
            {
                Zetbox.App.Base.ModuleNHibernateImpl __value = (Zetbox.App.Base.ModuleNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Module);

                if (OnModule_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Module>(__value);
                    OnModule_Getter(this, e);
                    __value = (Zetbox.App.Base.ModuleNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Module == null)
                {
                    SetInitializedProperty("Module");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Base.ModuleNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Module);
                var __newValue = (Zetbox.App.Base.ModuleNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("Module");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Module", __oldValue, __newValue);

                if (OnModule_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Module>(__oldValue, __newValue);
                    OnModule_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.ModuleNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Module = null;
                }
                else
                {
                    this.Proxy.Module = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Module", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnModule_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Module>(__oldValue, __newValue);
                    OnModule_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Module's id, used on dehydration only</summary>
        private int? _fk_Module = null;

        /// <summary>ForeignKey Property for Module's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Module
		{
			get { return Module != null ? Module.ID : (int?)null; }
			set { _fk_Module = value; }
		}

        /// <summary>Backing store for Module's guid, used on import only</summary>
        private Guid? _fk_guid_Module = null;

    public System.Threading.Tasks.Task TriggerFetchModuleAsync()
    {
        return System.Threading.Tasks.Task.FromResult<Zetbox.App.Base.Module>(this.Module);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Module
		public static event PropertyGetterHandler<Zetbox.App.Base.Group, Zetbox.App.Base.Module> OnModule_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.Group, Zetbox.App.Base.Module> OnModule_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.Group, Zetbox.App.Base.Module> OnModule_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.Group> OnModule_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Name
        {
            get
            {
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
                    if(IsAttached) UpdateChangedInfo = true;

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
		public static event PropertyGetterHandler<Zetbox.App.Base.Group, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.Group, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.Group, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.Group> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_Group")]
        public virtual async System.Threading.Tasks.Task<string> GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_Group != null)
            {
                await OnGetName_Group(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Group.GetName");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetName_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
        public static event GetName_Handler<Group> OnGetName_Group;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Group> OnGetName_Group_CanExec;

        [EventBasedMethod("OnGetName_Group_CanExec")]
        public virtual bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_Group_CanExec != null)
				{
					OnGetName_Group_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Group> OnGetName_Group_CanExecReason;

        [EventBasedMethod("OnGetName_Group_CanExecReason")]
        public virtual string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_Group_CanExecReason != null)
				{
					OnGetName_Group_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(Group);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Group)obj;
            var otherImpl = (GroupNHibernateImpl)obj;
            var me = (Group)this;

            me.ExportGuid = other.ExportGuid;
            me.Name = other.Name;
            this._fk_Module = otherImpl._fk_Module;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Module":
                    {
                        var __oldValue = (Zetbox.App.Base.ModuleNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Module);
                        var __newValue = (Zetbox.App.Base.ModuleNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Module", __oldValue, __newValue);
                        this.Proxy.Module = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Module", __oldValue, __newValue);
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
                case "ExportGuid":
                case "Module":
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Member":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Member":
                return TriggerFetchMemberAsync();
            case "Module":
                return TriggerFetchModuleAsync();
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override async System.Threading.Tasks.Task ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            await base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_Module.HasValue)
                this.Module = ((Zetbox.App.Base.ModuleNHibernateImpl)(await OurContext.FindPersistenceObjectAsync<Zetbox.App.Base.Module>(_fk_guid_Module.Value)));
            else
            if (_fk_Module.HasValue)
                this.Module = ((Zetbox.App.Base.ModuleNHibernateImpl)(await OurContext.FindPersistenceObjectAsync<Zetbox.App.Base.Module>(_fk_Module.Value)));
            else
                this.Module = null;
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
                    new PropertyDescriptorNHibernateImpl<Group, Guid>(
                        lazyCtx,
                        new Guid("c776e87f-2b95-466e-848e-0ce195f4bd73"),
                        "ExportGuid",
                        null,
                        obj => obj.ExportGuid,
                        (obj, val) => obj.ExportGuid = val,
						obj => OnExportGuid_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<Group, ICollection<Zetbox.App.Base.Identity>>(
                        lazyCtx,
                        new Guid("f60308a5-a502-4641-aa19-f895e701778c"),
                        "Member",
                        null,
                        obj => obj.Member,
                        null, // lists are read-only properties
                        obj => OnMember_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Group, Zetbox.App.Base.Module>(
                        lazyCtx,
                        new Guid("8da2dab4-81c9-4659-9f25-c6b5177ba26d"),
                        "Module",
                        null,
                        obj => obj.Module,
                        (obj, val) => obj.Module = val,
						obj => OnModule_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Group, string>(
                        lazyCtx,
                        new Guid("99c320b1-0003-4e2d-aa98-9a215d80988b"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
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
        [EventBasedMethod("OnToString_Group")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Group != null)
            {
                OnToString_Group(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Group> OnToString_Group;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Group")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Group != null)
            {
                OnObjectIsValid_Group(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Group> OnObjectIsValid_Group;

        [EventBasedMethod("OnNotifyPreSave_Group")]
        public override void NotifyPreSave()
        {
            FetchExportGuidOrDefault();
            base.NotifyPreSave();
            if (OnNotifyPreSave_Group != null) OnNotifyPreSave_Group(this);
        }
        public static event ObjectEventHandler<Group> OnNotifyPreSave_Group;

        [EventBasedMethod("OnNotifyPostSave_Group")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Group != null) OnNotifyPostSave_Group(this);
        }
        public static event ObjectEventHandler<Group> OnNotifyPostSave_Group;

        [EventBasedMethod("OnNotifyCreated_Group")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Module");
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_Group != null) OnNotifyCreated_Group(this);
        }
        public static event ObjectEventHandler<Group> OnNotifyCreated_Group;

        [EventBasedMethod("OnNotifyDeleting_Group")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Group != null) OnNotifyDeleting_Group(this);

            // FK_Group_has_Module
            if (Module != null) {
                ((NHibernatePersistenceObject)Module).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)Module);
            }
            // should fetch && remember parent for GroupMembership_has_Group_RelationEntry

            Member.Clear();
            Module = null;
        }
        public static event ObjectEventHandler<Group> OnNotifyDeleting_Group;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class GroupProxy
            : IProxyObject, ISortKey<int>
        {
            public GroupProxy()
            {
                Member = new Collection<Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl.Identities_memberOf_Groups_RelationEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(GroupNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(GroupProxy); } }

            public virtual Guid ExportGuid { get; set; }

            public virtual ICollection<Zetbox.App.Base.Identities_memberOf_Groups_RelationEntryNHibernateImpl.Identities_memberOf_Groups_RelationEntryProxy> Member { get; set; }

            public virtual Zetbox.App.Base.ModuleNHibernateImpl.ModuleProxy Module { get; set; }

            public virtual string Name { get; set; }


			[System.Runtime.Serialization.IgnoreDataMember]
			int ISortKey<int>.InternalSortKey { get { return ID; } }
        }

        // make proxy available for the provider
        [System.Runtime.Serialization.IgnoreDataMember]
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._isExportGuidSet);
            if (this._isExportGuidSet) {
                binStream.Write(this.Proxy.ExportGuid);
            }
            binStream.Write(this.Proxy.Module != null ? OurContext.GetIdFromProxy(this.Proxy.Module) : (int?)null);
            binStream.Write(this.Proxy.Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._isExportGuidSet = binStream.ReadBoolean();
            if (this._isExportGuidSet) {
                this.Proxy.ExportGuid = binStream.ReadGuid();
            }
            binStream.Read(out this._fk_Module);
            this.Proxy.Name = binStream.ReadString();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            xml.WriteAttributeString("ExportGuid", this.Proxy.ExportGuid.ToString());
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this.Proxy.Module != null ? this.Proxy.Module.ExportGuid : (Guid?)null, xml, "Module", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this.Proxy.Name, xml, "Name", "Zetbox.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|ExportGuid":
                // Import must have default value set
                this.Proxy.ExportGuid = XmlStreamer.ReadGuid(xml);
                this._isExportGuidSet = true;
                break;
            case "Zetbox.App.Base|Module":
                this._fk_guid_Module = XmlStreamer.ReadNullableGuid(xml);
                break;
            case "Zetbox.App.Base|Name":
                this.Proxy.Name = XmlStreamer.ReadString(xml);
                break;
            }
        }

        #endregion

    }
}