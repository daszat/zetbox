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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="GroupMembership")]
    [System.Diagnostics.DebuggerDisplay("GroupMembership")]
    public class GroupMembershipEfImpl : Zetbox.App.Base.AccessControlEfImpl, GroupMembership
    {
        private static readonly Guid _objectClassID = new Guid("acf18a64-5fc0-4610-b083-9893eea0776c");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public GroupMembershipEfImpl()
            : base(null)
        {
        }

        public GroupMembershipEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_GroupMembership_has_Group
    A: ZeroOrMore GroupMembership as GroupMembership
    B: One Group as Group
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Group
        // fkBackingName=_fk_Group; fkGuidBackingName=_fk_guid_Group;
        // referencedInterface=Zetbox.App.Base.Group; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.Group Group
        {
            get { return GroupImpl; }
            set { GroupImpl = (Zetbox.App.Base.GroupEfImpl)value; }
        }

        private int? _fk_Group;

        private Guid? _fk_guid_Group = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_GroupMembership_has_Group", "Group")]
        public Zetbox.App.Base.GroupEfImpl GroupImpl
        {
            get
            {
                Zetbox.App.Base.GroupEfImpl __value;
                EntityReference<Zetbox.App.Base.GroupEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.GroupEfImpl>(
                        "Model.FK_GroupMembership_has_Group",
                        "Group");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnGroup_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Group>(__value);
                    OnGroup_Getter(this, e);
                    __value = (Zetbox.App.Base.GroupEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Base.GroupEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.GroupEfImpl>(
                        "Model.FK_GroupMembership_has_Group",
                        "Group");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Base.GroupEfImpl __oldValue = (Zetbox.App.Base.GroupEfImpl)r.Value;
                Zetbox.App.Base.GroupEfImpl __newValue = (Zetbox.App.Base.GroupEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Group", __oldValue, __newValue);

                if (OnGroup_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Group>(__oldValue, __newValue);
                    OnGroup_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.GroupEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Base.GroupEfImpl)__newValue;

                if (OnGroup_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Group>(__oldValue, __newValue);
                    OnGroup_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Group", __oldValue, __newValue);
                UpdateChangedInfo = true;
            }
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Group
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
            var otherImpl = (GroupMembershipEfImpl)obj;
            var me = (GroupMembership)this;

            this._fk_Group = otherImpl._fk_Group;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

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
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_Group.HasValue)
                GroupImpl = (Zetbox.App.Base.GroupEfImpl)Context.FindPersistenceObject<Zetbox.App.Base.Group>(_fk_guid_Group.Value);
            else
            if (_fk_Group.HasValue)
                GroupImpl = (Zetbox.App.Base.GroupEfImpl)Context.Find<Zetbox.App.Base.Group>(_fk_Group.Value);
            else
                GroupImpl = null;
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
                    new PropertyDescriptorEfImpl<GroupMembership, Zetbox.App.Base.Group>(
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
            e.IsValid = b.IsValid;
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
        }
        public static event ObjectEventHandler<GroupMembership> OnNotifyDeleting_GroupMembership;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Zetbox.App.Base.GroupEfImpl>("Model.FK_GroupMembership_has_Group", "Group").EntityKey;
                binStream.Write(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_Group);
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