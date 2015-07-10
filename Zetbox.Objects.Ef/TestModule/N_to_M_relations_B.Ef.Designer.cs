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
    /// The B-Side class for the N_to_M_relations Tests
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="N_to_M_relations_BEfImpl")]
    [System.Diagnostics.DebuggerDisplay("N_to_M_relations_B")]
    public class N_to_M_relations_BEfImpl : BaseServerDataObject_EntityFramework, N_to_M_relations_B
    {
        private static readonly Guid _objectClassID = new Guid("c2af3719-d63a-4a94-9cc8-b4f94bb253ff");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public N_to_M_relations_BEfImpl()
            : base(null)
        {
        }

        public N_to_M_relations_BEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_ASide_connectsTo_BSide
    A: ZeroOrMore N_to_M_relations_A as ASide
    B: ZeroOrMore N_to_M_relations_B as BSide
    Preferred Storage: Separate
    */
        // collection reference property
        // Zetbox.DalProvider.Ef.Generator.Templates.Properties.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Zetbox.App.Test.N_to_M_relations_A> ASide
        {
            get
            {
                if (_ASide == null)
                {
                    _ASide = new ASideCollectionWrapper<Zetbox.App.Test.N_to_M_relations_A, Zetbox.App.Test.N_to_M_relations_B, Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl, EntityCollection<Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl>>(
                            this,
                            ASideImpl);
                }
                return _ASide;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_ASide_connectsTo_BSide_B", "CollectionEntry")]
        public EntityCollection<Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl> ASideImpl
        {
            get
            {
                return GetASideImplCollection();
            }
        }

        private EntityCollection<Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl> _ASideImplEntityCollection;
        internal EntityCollection<Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl> GetASideImplCollection()
        {
            if (_ASideImplEntityCollection == null)
            {
                _ASideImplEntityCollection
                    = ((IEntityWithRelationships)(this)).RelationshipManager
                        .GetRelatedCollection<Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl>(
                            "Model.FK_ASide_connectsTo_BSide_B",
                            "CollectionEntry");
                // the EntityCollection has to be loaded before attaching the AssociationChanged event
                // because the event is triggered while relation entries are loaded from the database
                // although that does not require notification of the business logic.
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !_ASideImplEntityCollection.IsLoaded)
                {
                    _ASideImplEntityCollection.Load();
                }
                _ASideImplEntityCollection.AssociationChanged += (s, e) => { this.NotifyPropertyChanged("ASide", null, null); if(OnASide_PostSetter != null && IsAttached) OnASide_PostSetter(this); };
            }
            return _ASideImplEntityCollection;
        }
        private ASideCollectionWrapper<Zetbox.App.Test.N_to_M_relations_A, Zetbox.App.Test.N_to_M_relations_B, Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl, EntityCollection<Zetbox.App.Test.ASide_connectsTo_BSide_RelationEntryEfImpl>> _ASide;

        public Zetbox.API.Async.ZbTask TriggerFetchASideAsync()
        {
            return new Zetbox.API.Async.ZbTask<ICollection<Zetbox.App.Test.N_to_M_relations_A>>(this.ASide);
        }

public static event PropertyListChangedHandler<Zetbox.App.Test.N_to_M_relations_B> OnASide_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.N_to_M_relations_B> OnASide_IsValid;

        /// <summary>
        /// 
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
		public static event PropertyGetterHandler<Zetbox.App.Test.N_to_M_relations_B, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.N_to_M_relations_B, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.N_to_M_relations_B, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.N_to_M_relations_B> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(N_to_M_relations_B);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (N_to_M_relations_B)obj;
            var otherImpl = (N_to_M_relations_BEfImpl)obj;
            var me = (N_to_M_relations_B)this;

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
                case "ASide":
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
            case "ASide":
                return TriggerFetchASideAsync();
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
                    new PropertyDescriptorEfImpl<N_to_M_relations_B, ICollection<Zetbox.App.Test.N_to_M_relations_A>>(
                        lazyCtx,
                        new Guid("a741d6bd-8a87-44c2-83b3-69225661f958"),
                        "ASide",
                        null,
                        obj => obj.ASide,
                        null, // lists are read-only properties
                        obj => OnASide_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<N_to_M_relations_B, string>(
                        lazyCtx,
                        new Guid("80ec9efe-c73b-4554-a145-064a32f225b8"),
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
        [EventBasedMethod("OnToString_N_to_M_relations_B")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_N_to_M_relations_B != null)
            {
                OnToString_N_to_M_relations_B(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<N_to_M_relations_B> OnToString_N_to_M_relations_B;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_N_to_M_relations_B")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_N_to_M_relations_B != null)
            {
                OnObjectIsValid_N_to_M_relations_B(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<N_to_M_relations_B> OnObjectIsValid_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyPreSave_N_to_M_relations_B")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_N_to_M_relations_B != null) OnNotifyPreSave_N_to_M_relations_B(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyPreSave_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyPostSave_N_to_M_relations_B")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_N_to_M_relations_B != null) OnNotifyPostSave_N_to_M_relations_B(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyPostSave_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyCreated_N_to_M_relations_B")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_N_to_M_relations_B != null) OnNotifyCreated_N_to_M_relations_B(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyCreated_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyDeleting_N_to_M_relations_B")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_N_to_M_relations_B != null) OnNotifyDeleting_N_to_M_relations_B(this);
            ASide.Clear();
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyDeleting_N_to_M_relations_B;

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