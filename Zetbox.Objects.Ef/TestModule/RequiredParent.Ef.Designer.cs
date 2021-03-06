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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// Testclass for the required_parent tests: parent
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="RequiredParentEfImpl")]
    [System.Diagnostics.DebuggerDisplay("RequiredParent")]
    public class RequiredParentEfImpl : BaseServerDataObject_EntityFramework, RequiredParent
    {
        private static readonly Guid _objectClassID = new Guid("0d753d7d-b023-43ce-9189-2ea6d03b70a1");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public RequiredParentEfImpl()
            : base(null)
        {
        }

        public RequiredParentEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Parent_of_Children
    A: One RequiredParent as Parent
    B: ZeroOrMore RequiredParentChild as Children
    Preferred Storage: MergeIntoB
    */
        // object list property
        // object list property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Zetbox.App.Test.RequiredParentChild> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = new EntityCollectionWrapper<Zetbox.App.Test.RequiredParentChild, Zetbox.App.Test.RequiredParentChildEfImpl>(
                            this.Context, ChildrenImpl,
                            () => this.NotifyPropertyChanging("Children", null, null),
                            null, // see GetChildrenImplCollection()
                            (item) => item.NotifyPropertyChanging("Parent", null, null),
                            (item) => item.NotifyPropertyChanged("Parent", null, null));
                }
                return _Children;
            }
        }
    
        [EdmRelationshipNavigationProperty("Model", "FK_Parent_of_Children", "Children")]
        public EntityCollection<Zetbox.App.Test.RequiredParentChildEfImpl> ChildrenImpl
        {
            get
            {
                return GetChildrenImplCollection();
            }
        }
        private EntityCollectionWrapper<Zetbox.App.Test.RequiredParentChild, Zetbox.App.Test.RequiredParentChildEfImpl> _Children;

        private EntityCollection<Zetbox.App.Test.RequiredParentChildEfImpl> _ChildrenImplEntityCollection;
        internal EntityCollection<Zetbox.App.Test.RequiredParentChildEfImpl> GetChildrenImplCollection()
        {
            if (_ChildrenImplEntityCollection == null)
            {
                _ChildrenImplEntityCollection = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Zetbox.App.Test.RequiredParentChildEfImpl>(
                        "Model.FK_Parent_of_Children",
                        "Children");
                // the EntityCollection has to be loaded before attaching the AssociationChanged event
                // because the event is triggered while relation entries are loaded from the database
                // although that does not require notification of the business logic.
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !_ChildrenImplEntityCollection.IsLoaded)
                {
                    _ChildrenImplEntityCollection.Load();
                }
                _ChildrenImplEntityCollection.AssociationChanged += (s, e) => { this.NotifyPropertyChanged("Children", null, null); if (OnChildren_PostSetter != null && IsAttached) OnChildren_PostSetter(this); };
            }
            return _ChildrenImplEntityCollection;
        }

        public Zetbox.API.Async.ZbTask TriggerFetchChildrenAsync()
        {
            return new Zetbox.API.Async.ZbTask<ICollection<Zetbox.App.Test.RequiredParentChild>>(this.Children);
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectListProperty
public static event PropertyListChangedHandler<Zetbox.App.Test.RequiredParent> OnChildren_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.RequiredParent> OnChildren_IsValid;

        /// <summary>
        /// dummy property
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Name
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
                    __result = _Name = __e.Result;
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
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
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
        private string _Name_store;
        private string _Name {
            get { return _Name_store; }
            set {
                ReportEfPropertyChanging("Name");
                _Name_store = value;
                ReportEfPropertyChanged("Name");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.RequiredParent, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.RequiredParent, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.RequiredParent, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.RequiredParent> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(RequiredParent);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (RequiredParent)obj;
            var otherImpl = (RequiredParentEfImpl)obj;
            var me = (RequiredParent)this;

            me.Name = other.Name;
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
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Children":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Children":
                return TriggerFetchChildrenAsync();
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

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
                    new PropertyDescriptorEfImpl<RequiredParent, ICollection<Zetbox.App.Test.RequiredParentChild>>(
                        lazyCtx,
                        new Guid("e452deb2-1f35-4b7c-9adc-1f904dfbfc6d"),
                        "Children",
                        null,
                        obj => obj.Children,
                        null, // lists are read-only properties
                        obj => OnChildren_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<RequiredParent, string>(
                        lazyCtx,
                        new Guid("22abc57e-581f-49f1-8eff-747e126a6480"),
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
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_RequiredParent")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RequiredParent != null)
            {
                OnToString_RequiredParent(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RequiredParent> OnToString_RequiredParent;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_RequiredParent")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_RequiredParent != null)
            {
                OnObjectIsValid_RequiredParent(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<RequiredParent> OnObjectIsValid_RequiredParent;

        [EventBasedMethod("OnNotifyPreSave_RequiredParent")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_RequiredParent != null) OnNotifyPreSave_RequiredParent(this);
        }
        public static event ObjectEventHandler<RequiredParent> OnNotifyPreSave_RequiredParent;

        [EventBasedMethod("OnNotifyPostSave_RequiredParent")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_RequiredParent != null) OnNotifyPostSave_RequiredParent(this);
        }
        public static event ObjectEventHandler<RequiredParent> OnNotifyPostSave_RequiredParent;

        [EventBasedMethod("OnNotifyCreated_RequiredParent")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_RequiredParent != null) OnNotifyCreated_RequiredParent(this);
        }
        public static event ObjectEventHandler<RequiredParent> OnNotifyCreated_RequiredParent;

        [EventBasedMethod("OnNotifyDeleting_RequiredParent")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_RequiredParent != null) OnNotifyDeleting_RequiredParent(this);
            Children.Clear();
        }
        public static event ObjectEventHandler<RequiredParent> OnNotifyDeleting_RequiredParent;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var __oldValue = _ID;
                    var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                }
                else
                {
                    SetInitializedProperty("ID");
                }
            }
        }
        private int _ID;
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Name = binStream.ReadString();
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