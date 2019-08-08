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
    /// Holds the current Number of a database sequence
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SequenceData")]
    public class SequenceDataNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, SequenceData
    {
        private static readonly Guid _objectClassID = new Guid("6efc1387-cffc-4cff-9af3-19365d888f4b");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public SequenceDataNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public SequenceDataNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new SequenceDataProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public SequenceDataNHibernateImpl(Func<IFrozenContext> lazyCtx, SequenceDataProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly SequenceDataProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int CurrentNumber
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.CurrentNumber;
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
                if (Proxy.CurrentNumber != value)
                {
                    var __oldValue = Proxy.CurrentNumber;
                    var __newValue = value;
                    if (OnCurrentNumber_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnCurrentNumber_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("CurrentNumber", __oldValue, __newValue);
                    Proxy.CurrentNumber = __newValue;
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

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.SequenceData, int> OnCurrentNumber_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.SequenceData, int> OnCurrentNumber_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.SequenceData, int> OnCurrentNumber_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.SequenceData> OnCurrentNumber_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Sequence
        // fkBackingName=this.Proxy.Sequence; fkGuidBackingName=_fk_guid_Sequence;
        // referencedInterface=Zetbox.App.Base.Sequence; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=Data; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.Sequence Sequence
        {
            get
            {
                Zetbox.App.Base.SequenceNHibernateImpl __value = (Zetbox.App.Base.SequenceNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Sequence);

                if (OnSequence_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Sequence>(__value);
                    OnSequence_Getter(this, e);
                    __value = (Zetbox.App.Base.SequenceNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Sequence == null)
                {
                    SetInitializedProperty("Sequence");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Base.SequenceNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Sequence);
                var __newValue = (Zetbox.App.Base.SequenceNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("Sequence");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Sequence", __oldValue, __newValue);

                if (OnSequence_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Sequence>(__oldValue, __newValue);
                    OnSequence_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.SequenceNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Sequence = null;
                }
                else
                {
                    this.Proxy.Sequence = __newValue.Proxy;
                }

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // unset old reference
                    __oldValue.Data = null;
                }

                if (__newValue != null)
                {
                    // set new reference
                    __newValue.Data = this;
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Sequence", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnSequence_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Sequence>(__oldValue, __newValue);
                    OnSequence_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Sequence's id, used on dehydration only</summary>
        private int? _fk_Sequence = null;

        /// <summary>ForeignKey Property for Sequence's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Sequence
		{
			get { return Sequence != null ? Sequence.ID : (int?)null; }
			set { _fk_Sequence = value; }
		}


    public Zetbox.API.Async.ZbTask TriggerFetchSequenceAsync()
    {
        return new Zetbox.API.Async.ZbTask<Zetbox.App.Base.Sequence>(this.Sequence);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Sequence
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
            var otherImpl = (SequenceDataNHibernateImpl)obj;
            var me = (SequenceData)this;

            me.CurrentNumber = other.CurrentNumber;
            this._fk_Sequence = otherImpl._fk_Sequence;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Sequence":
                    {
                        var __oldValue = (Zetbox.App.Base.SequenceNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Sequence);
                        var __newValue = (Zetbox.App.Base.SequenceNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Sequence", __oldValue, __newValue);
                        this.Proxy.Sequence = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Sequence", __oldValue, __newValue);
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
                case "CurrentNumber":
                case "Sequence":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

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
                this.Sequence = ((Zetbox.App.Base.SequenceNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Base.Sequence>(_fk_Sequence.Value));
            else
                this.Sequence = null;
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
                    new PropertyDescriptorNHibernateImpl<SequenceData, int>(
                        lazyCtx,
                        new Guid("e557569b-1ed8-49a6-959e-0a6bc3ffa591"),
                        "CurrentNumber",
                        null,
                        obj => obj.CurrentNumber,
                        (obj, val) => obj.CurrentNumber = val,
						obj => OnCurrentNumber_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<SequenceData, Zetbox.App.Base.Sequence>(
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
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

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

            // FK_Sequence_has_Data
            if (Sequence != null) {
                ((NHibernatePersistenceObject)Sequence).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)Sequence);
            }

            Sequence = null;
        }
        public static event ObjectEventHandler<SequenceData> OnNotifyDeleting_SequenceData;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class SequenceDataProxy
            : IProxyObject, ISortKey<int>
        {
            public SequenceDataProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(SequenceDataNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(SequenceDataProxy); } }

            public virtual int CurrentNumber { get; set; }

            public virtual Zetbox.App.Base.SequenceNHibernateImpl.SequenceProxy Sequence { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.CurrentNumber);
            binStream.Write(this.Proxy.Sequence != null ? OurContext.GetIdFromProxy(this.Proxy.Sequence) : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this.Proxy.CurrentNumber = binStream.ReadInt32();
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