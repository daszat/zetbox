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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("GroupMembership")]
    public class GroupMembershipMemoryImpl : Zetbox.App.Base.AccessControlMemoryImpl, GroupMembership
    {
        private static readonly Guid _objectClassID = new Guid("acf18a64-5fc0-4610-b083-9893eea0776c");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public GroupMembershipMemoryImpl()
            : base(null)
        {
        }

        public GroupMembershipMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Group
        // fkBackingName=_fk_Group; fkGuidBackingName=_fk_guid_Group;
        // referencedInterface=Zetbox.App.Base.Group; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.Group Group
        {
            get { return GroupImpl; }
            set { GroupImpl = (Zetbox.App.Base.GroupMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<Zetbox.App.Base.Group> GetProp_Group()
        {
            return TriggerFetchGroupAsync();
        }

        public async System.Threading.Tasks.Task SetProp_Group(Zetbox.App.Base.Group newValue)
        {
            await TriggerFetchGroupAsync();
            GroupImpl = (Zetbox.App.Base.GroupMemoryImpl)newValue;
        }

        private int? __fk_GroupCache;

        private int? _fk_Group {
            get
            {
                return __fk_GroupCache;
            }
            set
            {
                __fk_GroupCache = value;
                // Recreate task to clear it's cache
                _triggerFetchGroupTask = null;
            }
        }

        /// <summary>ForeignKey Property for Group's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Group
		{
			get { return _fk_Group; }
			set { _fk_Group = value; }
		}

        private Guid? _fk_guid_Group = null;

        System.Threading.Tasks.Task<Zetbox.App.Base.Group> _triggerFetchGroupTask;
        public System.Threading.Tasks.Task<Zetbox.App.Base.Group> TriggerFetchGroupAsync()
        {
            if (_triggerFetchGroupTask != null) return _triggerFetchGroupTask;

            System.Threading.Tasks.Task<Zetbox.App.Base.Group> task;

            if (_fk_Group.HasValue)
                task = Context.FindAsync<Zetbox.App.Base.Group>(_fk_Group.Value);
            else
                task = System.Threading.Tasks.Task.FromResult<Zetbox.App.Base.Group>(null);

            task.OnResult(t =>
            {
                if (OnGroup_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Group>(t.Result);
                    OnGroup_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchGroupTask = task;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Base.GroupMemoryImpl GroupImpl
        {
            get
            {
                var task = TriggerFetchGroupAsync();
                task.TryRunSynchronously();
                return (Zetbox.App.Base.GroupMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Group == null) || (value != null && value.ID == _fk_Group))
                {
                    SetInitializedProperty("Group");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = GroupImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Group", __oldValue, __newValue);

                if (OnGroup_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Group>(__oldValue, __newValue);
                    OnGroup_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.GroupMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Group = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Group", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnGroup_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Group>(__oldValue, __newValue);
                    OnGroup_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Group
		public static event PropertyGetterHandler<Zetbox.App.Base.GroupMembership, Zetbox.App.Base.Group> OnGroup_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.GroupMembership, Zetbox.App.Base.Group> OnGroup_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.GroupMembership, Zetbox.App.Base.Group> OnGroup_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.GroupMembership> OnGroup_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(GroupMembership);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (GroupMembership)obj;
            var otherImpl = (GroupMembershipMemoryImpl)obj;
            var me = (GroupMembership)this;

            this._fk_Group = otherImpl._fk_Group;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Group":
                    {
                        var __oldValue = _fk_Group;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Group", __oldValue, __newValue);
                        _fk_Group = __newValue;
                        NotifyPropertyChanged("Group", __oldValue, __newValue);
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
                case "Group":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Group":
                return TriggerFetchGroupAsync();
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

            if (_fk_guid_Group.HasValue)
                GroupImpl = (Zetbox.App.Base.GroupMemoryImpl)(await Context.FindPersistenceObjectAsync<Zetbox.App.Base.Group>(_fk_guid_Group.Value));
            else
            if (_fk_Group.HasValue)
                GroupImpl = (Zetbox.App.Base.GroupMemoryImpl)(await Context.FindAsync<Zetbox.App.Base.Group>(_fk_Group.Value));
            else
                GroupImpl = null;
            // fix cached lists references
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
                    new PropertyDescriptorMemoryImpl<GroupMembership, Zetbox.App.Base.Group>(
                        lazyCtx,
                        new Guid("da080b07-15d2-4cdf-bc1c-df776e094a75"),
                        "Group",
                        null,
                        obj => obj.Group,
                        (obj, val) => obj.Group = val,
						obj => OnGroup_IsValid), 
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
        [EventBasedMethod("OnToString_GroupMembership")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_GroupMembership != null)
            {
                OnToString_GroupMembership(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<GroupMembership> OnToString_GroupMembership;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_GroupMembership")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_GroupMembership != null)
            {
                OnObjectIsValid_GroupMembership(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<GroupMembership> OnObjectIsValid_GroupMembership;

        [EventBasedMethod("OnNotifyPreSave_GroupMembership")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_GroupMembership != null) OnNotifyPreSave_GroupMembership(this);
        }
        public static event ObjectEventHandler<GroupMembership> OnNotifyPreSave_GroupMembership;

        [EventBasedMethod("OnNotifyPostSave_GroupMembership")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_GroupMembership != null) OnNotifyPostSave_GroupMembership(this);
        }
        public static event ObjectEventHandler<GroupMembership> OnNotifyPostSave_GroupMembership;

        [EventBasedMethod("OnNotifyCreated_GroupMembership")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Group");
            base.NotifyCreated();
            if (OnNotifyCreated_GroupMembership != null) OnNotifyCreated_GroupMembership(this);
        }
        public static event ObjectEventHandler<GroupMembership> OnNotifyCreated_GroupMembership;

        [EventBasedMethod("OnNotifyDeleting_GroupMembership")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_GroupMembership != null) OnNotifyDeleting_GroupMembership(this);
            Group = null;
        }
        public static event ObjectEventHandler<GroupMembership> OnNotifyDeleting_GroupMembership;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(_fk_Group != null ? _fk_Group : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_Group = binStream.ReadNullableInt32();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(Group != null ? Group.ExportGuid : (Guid?)null, xml, "Group", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|Group":
                this._fk_guid_Group = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}