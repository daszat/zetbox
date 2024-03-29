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
    [System.Diagnostics.DebuggerDisplay("RoleMembership")]
    public class RoleMembershipMemoryImpl : Zetbox.App.Base.AccessControlMemoryImpl, RoleMembership
    {
        private static readonly Guid _objectClassID = new Guid("3b79d759-2943-4caa-bf6f-5e89955f7f91");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public RoleMembershipMemoryImpl()
            : base(null)
        {
        }

        public RoleMembershipMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
        // BEGIN Zetbox.Generator.Templates.Properties.CollectionEntryListProperty for Relations
        public IList<Zetbox.App.Base.Relation> Relations
        {
            get
            {
                if (_Relations == null)
                {
                    var task = TriggerFetchRelationsAsync();
                    task.TryRunSynchronously();
                }
                return (IList<Zetbox.App.Base.Relation>)_Relations;
            }
        }

        public async System.Threading.Tasks.Task<IList<Zetbox.App.Base.Relation>> GetProp_Relations()
        {
            await TriggerFetchRelationsAsync();
            return _Relations;
        }

        System.Threading.Tasks.Task _triggerFetchRelationsTask;
        public System.Threading.Tasks.Task TriggerFetchRelationsAsync()
        {
            if (_triggerFetchRelationsTask != null) return _triggerFetchRelationsTask;
            System.Threading.Tasks.Task task;
            task = Context.FetchRelationAsync<Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryMemoryImpl>(new Guid("f74d425f-e733-4cba-baca-f4a05fbc0a80"), RelationEndRole.A, this);
            task = task.OnResult(r =>
            {
                _Relations
                    = new ObservableBSideListWrapper<Zetbox.App.Base.RoleMembership, Zetbox.App.Base.Relation, Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryMemoryImpl, ICollection<Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryMemoryImpl>>(
                        this,
                        new RelationshipFilterASideCollection<Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryMemoryImpl>(this.Context, this));
                        // _Relations.CollectionChanged is managed by OnRelationsCollectionChanged() and called from the RelationEntry
            });
            return _triggerFetchRelationsTask = task;
        }

        internal void OnRelationsCollectionChanged()
        {
            NotifyPropertyChanged("Relations", null, null);
            if (OnRelations_PostSetter != null && IsAttached)
                OnRelations_PostSetter(this);
        }

        private ObservableBSideListWrapper<Zetbox.App.Base.RoleMembership, Zetbox.App.Base.Relation, Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryMemoryImpl, ICollection<Zetbox.App.Base.RoleMembership_resolves_Relations_RelationEntryMemoryImpl>> _Relations;
        // END Zetbox.Generator.Templates.Properties.CollectionEntryListProperty for Relations
public static event PropertyListChangedHandler<Zetbox.App.Base.RoleMembership> OnRelations_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.RoleMembership> OnRelations_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(RoleMembership);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (RoleMembership)obj;
            var otherImpl = (RoleMembershipMemoryImpl)obj;
            var me = (RoleMembership)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange


        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Relations":
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
            case "Relations":
                return TriggerFetchRelationsAsync();
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<RoleMembership, IList<Zetbox.App.Base.Relation>>(
                        lazyCtx,
                        new Guid("fb799900-1a5b-4b62-a445-5dae8febdd28"),
                        "Relations",
                        null,
                        obj => obj.Relations,
                        null, // lists are read-only properties
                        obj => OnRelations_IsValid), 
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
        [EventBasedMethod("OnToString_RoleMembership")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RoleMembership != null)
            {
                OnToString_RoleMembership(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RoleMembership> OnToString_RoleMembership;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_RoleMembership")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_RoleMembership != null)
            {
                OnObjectIsValid_RoleMembership(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<RoleMembership> OnObjectIsValid_RoleMembership;

        [EventBasedMethod("OnNotifyPreSave_RoleMembership")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_RoleMembership != null) OnNotifyPreSave_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnNotifyPreSave_RoleMembership;

        [EventBasedMethod("OnNotifyPostSave_RoleMembership")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_RoleMembership != null) OnNotifyPostSave_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnNotifyPostSave_RoleMembership;

        [EventBasedMethod("OnNotifyCreated_RoleMembership")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_RoleMembership != null) OnNotifyCreated_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnNotifyCreated_RoleMembership;

        [EventBasedMethod("OnNotifyDeleting_RoleMembership")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_RoleMembership != null) OnNotifyDeleting_RoleMembership(this);
            Relations.Clear();
        }
        public static event ObjectEventHandler<RoleMembership> OnNotifyDeleting_RoleMembership;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
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
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}