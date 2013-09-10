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
    [EdmEntityType(NamespaceName="Model", Name="SequenceDataEfImpl")]
    [System.Diagnostics.DebuggerDisplay("SequenceData")]
    public class SequenceDataEfImpl : BaseServerDataObject_EntityFramework, SequenceData
    {
        private static readonly Guid _objectClassID = new Guid("6efc1387-cffc-4cff-9af3-19365d888f4b");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public SequenceDataEfImpl()
            : base(null)
        {
        }

        public SequenceDataEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int CurrentNumber
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _CurrentNumber;
                if (OnCurrentNumber_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnCurrentNumber_Getter(this, __e);
                    __result = _CurrentNumber = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_CurrentNumber != value)
                {
                    var __oldValue = _CurrentNumber;
                    var __newValue = value;
                    if (OnCurrentNumber_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnCurrentNumber_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("CurrentNumber", __oldValue, __newValue);
                    _CurrentNumber = __newValue;
                    NotifyPropertyChanged("CurrentNumber", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnCurrentNumber_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnCurrentNumber_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("CurrentNumber");
                }
            }
        }
        private int _CurrentNumber_store;
        private int _CurrentNumber {
            get { return _CurrentNumber_store; }
            set {
                ReportEfPropertyChanging("CurrentNumber");
                _CurrentNumber_store = value;
                ReportEfPropertyChanged("CurrentNumber");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.SequenceData, int> OnCurrentNumber_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.SequenceData, int> OnCurrentNumber_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.SequenceData, int> OnCurrentNumber_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.SequenceData> OnCurrentNumber_IsValid;

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Sequence_has_Data
    A: One Sequence as Sequence
    B: ZeroOrOne SequenceData as Data
    Preferred Storage: MergeIntoB
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Sequence
        // fkBackingName=_fk_Sequence; fkGuidBackingName=_fk_guid_Sequence;
        // referencedInterface=Zetbox.App.Base.Sequence; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=Data; is reference;
        // PositionStorage=none;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.Sequence Sequence
        {
            get { return SequenceImpl; }
            set { SequenceImpl = (Zetbox.App.Base.SequenceEfImpl)value; }
        }

        private int? _fk_Sequence;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Sequence_has_Data", "Sequence")]
        public Zetbox.App.Base.SequenceEfImpl SequenceImpl
        {
            get
            {
                Zetbox.App.Base.SequenceEfImpl __value;
                EntityReference<Zetbox.App.Base.SequenceEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.SequenceEfImpl>(
                        "Model.FK_Sequence_has_Data",
                        "Sequence");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                __value = r.Value;
                if (OnSequence_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Sequence>(__value);
                    OnSequence_Getter(this, e);
                    __value = (Zetbox.App.Base.SequenceEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Base.SequenceEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.SequenceEfImpl>(
                        "Model.FK_Sequence_has_Data",
                        "Sequence");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Base.SequenceEfImpl __oldValue = (Zetbox.App.Base.SequenceEfImpl)r.Value;
                Zetbox.App.Base.SequenceEfImpl __newValue = (Zetbox.App.Base.SequenceEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Sequence", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Data", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Data", null, null);
                }

                if (OnSequence_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Sequence>(__oldValue, __newValue);
                    OnSequence_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.SequenceEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Base.SequenceEfImpl)__newValue;

                if (OnSequence_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Sequence>(__oldValue, __newValue);
                    OnSequence_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Sequence", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("Data", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("Data", null, null);
                }
                if(IsAttached) UpdateChangedInfo = true;
            }
        }

        public Zetbox.API.Async.ZbTask TriggerFetchSequenceAsync()
        {
            return new Zetbox.API.Async.ZbTask<Zetbox.App.Base.Sequence>(this.Sequence);
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Sequence
		public static event PropertyGetterHandler<Zetbox.App.Base.SequenceData, Zetbox.App.Base.Sequence> OnSequence_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.SequenceData, Zetbox.App.Base.Sequence> OnSequence_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.SequenceData, Zetbox.App.Base.Sequence> OnSequence_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.SequenceData> OnSequence_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(SequenceData);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (SequenceData)obj;
            var otherImpl = (SequenceDataEfImpl)obj;
            var me = (SequenceData)this;

            me.CurrentNumber = other.CurrentNumber;
            this._fk_Sequence = otherImpl._fk_Sequence;
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
                case "CurrentNumber":
                case "Sequence":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Sequence":
                return TriggerFetchSequenceAsync();
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

            if (_fk_Sequence.HasValue)
                SequenceImpl = (Zetbox.App.Base.SequenceEfImpl)Context.Find<Zetbox.App.Base.Sequence>(_fk_Sequence.Value);
            else
                SequenceImpl = null;
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
                    new PropertyDescriptorEfImpl<SequenceData, int>(
                        lazyCtx,
                        new Guid("e557569b-1ed8-49a6-959e-0a6bc3ffa591"),
                        "CurrentNumber",
                        null,
                        obj => obj.CurrentNumber,
                        (obj, val) => obj.CurrentNumber = val,
						obj => OnCurrentNumber_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<SequenceData, Zetbox.App.Base.Sequence>(
                        lazyCtx,
                        new Guid("98a20549-d4ff-4caf-bae2-10951b04c6f1"),
                        "Sequence",
                        null,
                        obj => obj.Sequence,
                        (obj, val) => obj.Sequence = val,
						obj => OnSequence_IsValid), 
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
        [EventBasedMethod("OnToString_SequenceData")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_SequenceData != null)
            {
                OnToString_SequenceData(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<SequenceData> OnToString_SequenceData;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_SequenceData")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_SequenceData != null)
            {
                OnObjectIsValid_SequenceData(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<SequenceData> OnObjectIsValid_SequenceData;

        [EventBasedMethod("OnNotifyPreSave_SequenceData")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_SequenceData != null) OnNotifyPreSave_SequenceData(this);
        }
        public static event ObjectEventHandler<SequenceData> OnNotifyPreSave_SequenceData;

        [EventBasedMethod("OnNotifyPostSave_SequenceData")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_SequenceData != null) OnNotifyPostSave_SequenceData(this);
        }
        public static event ObjectEventHandler<SequenceData> OnNotifyPostSave_SequenceData;

        [EventBasedMethod("OnNotifyCreated_SequenceData")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("CurrentNumber");
            SetNotInitializedProperty("Sequence");
            base.NotifyCreated();
            if (OnNotifyCreated_SequenceData != null) OnNotifyCreated_SequenceData(this);
        }
        public static event ObjectEventHandler<SequenceData> OnNotifyCreated_SequenceData;

        [EventBasedMethod("OnNotifyDeleting_SequenceData")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_SequenceData != null) OnNotifyDeleting_SequenceData(this);
            Sequence = null;
        }
        public static event ObjectEventHandler<SequenceData> OnNotifyDeleting_SequenceData;

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
            binStream.Write(this._CurrentNumber);
            {
                var r = this.RelationshipManager.GetRelatedReference<Zetbox.App.Base.SequenceEfImpl>("Model.FK_Sequence_has_Data", "Sequence");
                var key = r.EntityKey;
                binStream.Write(r.Value != null ? r.Value.ID : (key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null));
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._CurrentNumber = binStream.ReadInt32();
            binStream.Read(out this._fk_Sequence);
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