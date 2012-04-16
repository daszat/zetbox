// <autogenerated/>

namespace Kistl.App.Base
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

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="SequenceData")]
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
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int CurrentNumber
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _CurrentNumber;
                if (OnCurrentNumber_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnCurrentNumber_Getter(this, __e);
                    __result = __e.Result;
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
        private int _CurrentNumber;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.SequenceData, int> OnCurrentNumber_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.SequenceData, int> OnCurrentNumber_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.SequenceData, int> OnCurrentNumber_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.SequenceData> OnCurrentNumber_IsValid;

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
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Sequence
        // fkBackingName=_fk_Sequence; fkGuidBackingName=_fk_guid_Sequence;
        // referencedInterface=Kistl.App.Base.Sequence; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=Data; is reference;
        // PositionStorage=none;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Sequence Sequence
        {
            get { return SequenceImpl; }
            set { SequenceImpl = (Kistl.App.Base.SequenceEfImpl)value; }
        }

        private int? _fk_Sequence;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Sequence_has_Data", "Sequence")]
        public Kistl.App.Base.SequenceEfImpl SequenceImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.SequenceEfImpl __value;
                EntityReference<Kistl.App.Base.SequenceEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.SequenceEfImpl>(
                        "Model.FK_Sequence_has_Data",
                        "Sequence");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnSequence_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Sequence>(__value);
                    OnSequence_Getter(this, e);
                    __value = (Kistl.App.Base.SequenceEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Base.SequenceEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.SequenceEfImpl>(
                        "Model.FK_Sequence_has_Data",
                        "Sequence");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Base.SequenceEfImpl __oldValue = (Kistl.App.Base.SequenceEfImpl)r.Value;
                Kistl.App.Base.SequenceEfImpl __newValue = (Kistl.App.Base.SequenceEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("Sequence", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Data", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Data", null, null, null);
                }

                if (OnSequence_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Sequence>(__oldValue, __newValue);
                    OnSequence_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.SequenceEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Base.SequenceEfImpl)__newValue;

                if (OnSequence_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Sequence>(__oldValue, __newValue);
                    OnSequence_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("Sequence", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("Data", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("Data", null, null, null);
                }
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Sequence
		public static event PropertyGetterHandler<Kistl.App.Base.SequenceData, Kistl.App.Base.Sequence> OnSequence_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.SequenceData, Kistl.App.Base.Sequence> OnSequence_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.SequenceData, Kistl.App.Base.Sequence> OnSequence_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.SequenceData> OnSequence_IsValid;

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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

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

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Sequence.HasValue)
                SequenceImpl = (Kistl.App.Base.SequenceEfImpl)Context.Find<Kistl.App.Base.Sequence>(_fk_Sequence.Value);
            else
                SequenceImpl = null;
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
                    new PropertyDescriptorEfImpl<SequenceData, Kistl.App.Base.Sequence>(
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

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
            Sequence = null;
            base.NotifyDeleting();
            if (OnNotifyDeleting_SequenceData != null) OnNotifyDeleting_SequenceData(this);
        }
        public static event ObjectEventHandler<SequenceData> OnNotifyDeleting_SequenceData;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
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

                }
				else 
				{
					SetInitializedProperty("ID");
				}
            }
        }
        private int _ID;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._CurrentNumber, binStream);
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.SequenceEfImpl>("Model.FK_Sequence_has_Data", "Sequence").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._CurrentNumber, binStream);
            BinarySerializer.FromStream(out this._fk_Sequence, binStream);
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
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this._CurrentNumber, xml, "CurrentNumber", "Kistl.App.Base");
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.SequenceEfImpl>("Model.FK_Sequence_has_Data", "Sequence").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "Sequence", "Kistl.App.Base");
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._CurrentNumber, xml, "CurrentNumber", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Sequence, xml, "Sequence", "Kistl.App.Base");
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