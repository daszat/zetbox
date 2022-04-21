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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EventTestObject")]
    public class EventTestObjectMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, EventTestObject
    {
        private static readonly Guid _objectClassID = new Guid("1be8e748-c714-42f9-aeb1-c9f180b2f126");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public EventTestObjectMemoryImpl()
            : base(null)
        {
        }

        public EventTestObjectMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Event
        // fkBackingName=_fk_Event; fkGuidBackingName=_fk_guid_Event;
        // referencedInterface=Zetbox.App.Calendar.Event; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Calendar.Event Event
        {
            get { return EventImpl; }
            set { EventImpl = (Zetbox.App.Calendar.EventMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<Zetbox.App.Calendar.Event> GetProp_Event()
        {
            return TriggerFetchEventAsync();
        }

        public async System.Threading.Tasks.Task SetProp_Event(Zetbox.App.Calendar.Event newValue)
        {
            await TriggerFetchEventAsync();
            EventImpl = (Zetbox.App.Calendar.EventMemoryImpl)newValue;
        }

        private int? __fk_EventCache;

        private int? _fk_Event {
            get
            {
                return __fk_EventCache;
            }
            set
            {
                __fk_EventCache = value;
                // Recreate task to clear it's cache
                _triggerFetchEventTask = null;
            }
        }

        /// <summary>ForeignKey Property for Event's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Event
		{
			get { return _fk_Event; }
			set { _fk_Event = value; }
		}


        System.Threading.Tasks.Task<Zetbox.App.Calendar.Event> _triggerFetchEventTask;
        public System.Threading.Tasks.Task<Zetbox.App.Calendar.Event> TriggerFetchEventAsync()
        {
            if (_triggerFetchEventTask != null) return _triggerFetchEventTask;

            if (_fk_Event.HasValue)
                _triggerFetchEventTask = Context.FindAsync<Zetbox.App.Calendar.Event>(_fk_Event.Value);
            else
                _triggerFetchEventTask = new System.Threading.Tasks.Task<Zetbox.App.Calendar.Event>(() => null);

            _triggerFetchEventTask.OnResult(t =>
            {
                if (OnEvent_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Calendar.Event>(t.Result);
                    OnEvent_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchEventTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Calendar.EventMemoryImpl EventImpl
        {
            get
            {
                var task = TriggerFetchEventAsync();
                task.TryRunSynchronously();
                task.Wait();
                return (Zetbox.App.Calendar.EventMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Event == null) || (value != null && value.ID == _fk_Event))
                {
                    SetInitializedProperty("Event");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = EventImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Event", __oldValue, __newValue);

                if (OnEvent_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Calendar.Event>(__oldValue, __newValue);
                    OnEvent_PreSetter(this, e);
                    __newValue = (Zetbox.App.Calendar.EventMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Event = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Event", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnEvent_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Calendar.Event>(__oldValue, __newValue);
                    OnEvent_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Event
		public static event PropertyGetterHandler<Zetbox.App.Test.EventTestObject, Zetbox.App.Calendar.Event> OnEvent_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.EventTestObject, Zetbox.App.Calendar.Event> OnEvent_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.EventTestObject, Zetbox.App.Calendar.Event> OnEvent_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.EventTestObject> OnEvent_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
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
        private string _Name;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.EventTestObject, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.EventTestObject, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.EventTestObject, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.EventTestObject> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(EventTestObject);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (EventTestObject)obj;
            var otherImpl = (EventTestObjectMemoryImpl)obj;
            var me = (EventTestObject)this;

            me.Name = other.Name;
            this._fk_Event = otherImpl._fk_Event;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Event":
                    {
                        var __oldValue = _fk_Event;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Event", __oldValue, __newValue);
                        _fk_Event = __newValue;
                        NotifyPropertyChanged("Event", __oldValue, __newValue);
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
                case "Event":
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Event":
                return TriggerFetchEventAsync();
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

            if (_fk_Event.HasValue)
                EventImpl = (Zetbox.App.Calendar.EventMemoryImpl)Context.Find<Zetbox.App.Calendar.Event>(_fk_Event.Value);
            else
                EventImpl = null;
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
                    new PropertyDescriptorMemoryImpl<EventTestObject, Zetbox.App.Calendar.Event>(
                        lazyCtx,
                        new Guid("adf59fe9-12fe-413e-8dc7-d9190aa6bf13"),
                        "Event",
                        null,
                        obj => obj.Event,
                        (obj, val) => obj.Event = val,
						obj => OnEvent_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<EventTestObject, string>(
                        lazyCtx,
                        new Guid("57f6c265-b768-40e8-b546-bd410439d354"),
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
        [EventBasedMethod("OnToString_EventTestObject")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EventTestObject != null)
            {
                OnToString_EventTestObject(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<EventTestObject> OnToString_EventTestObject;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_EventTestObject")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_EventTestObject != null)
            {
                OnObjectIsValid_EventTestObject(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<EventTestObject> OnObjectIsValid_EventTestObject;

        [EventBasedMethod("OnNotifyPreSave_EventTestObject")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_EventTestObject != null) OnNotifyPreSave_EventTestObject(this);
        }
        public static event ObjectEventHandler<EventTestObject> OnNotifyPreSave_EventTestObject;

        [EventBasedMethod("OnNotifyPostSave_EventTestObject")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_EventTestObject != null) OnNotifyPostSave_EventTestObject(this);
        }
        public static event ObjectEventHandler<EventTestObject> OnNotifyPostSave_EventTestObject;

        [EventBasedMethod("OnNotifyCreated_EventTestObject")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Event");
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_EventTestObject != null) OnNotifyCreated_EventTestObject(this);
        }
        public static event ObjectEventHandler<EventTestObject> OnNotifyCreated_EventTestObject;

        [EventBasedMethod("OnNotifyDeleting_EventTestObject")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_EventTestObject != null) OnNotifyDeleting_EventTestObject(this);
            Event = null;
        }
        public static event ObjectEventHandler<EventTestObject> OnNotifyDeleting_EventTestObject;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(Event != null ? Event.ID : (int?)null);
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_Event = binStream.ReadNullableInt32();
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