// <autogenerated/>

namespace Zetbox.App.Calendar
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
    /// Sync provider for work schedules
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("WorkScheduleSyncProvider")]
    public class WorkScheduleSyncProviderMemoryImpl : Zetbox.App.Calendar.SyncProviderMemoryImpl, WorkScheduleSyncProvider
    {
        private static readonly Guid _objectClassID = new Guid("ed44a638-a19d-430c-b19f-766a1820fc67");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public WorkScheduleSyncProviderMemoryImpl()
            : base(null)
        {
        }

        public WorkScheduleSyncProviderMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Calendar
        // fkBackingName=_fk_Calendar; fkGuidBackingName=_fk_guid_Calendar;
        // referencedInterface=Zetbox.App.Calendar.CalendarBook; moduleNamespace=Zetbox.App.Calendar;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Calendar.CalendarBook Calendar
        {
            get { return CalendarImpl; }
            set { CalendarImpl = (Zetbox.App.Calendar.CalendarBookMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<Zetbox.App.Calendar.CalendarBook> GetProp_Calendar()
        {
            return TriggerFetchCalendarAsync();
        }

        public async System.Threading.Tasks.Task SetProp_Calendar(Zetbox.App.Calendar.CalendarBook newValue)
        {
            await TriggerFetchCalendarAsync();
            CalendarImpl = (Zetbox.App.Calendar.CalendarBookMemoryImpl)newValue;
        }

        private int? __fk_CalendarCache;

        private int? _fk_Calendar {
            get
            {
                return __fk_CalendarCache;
            }
            set
            {
                __fk_CalendarCache = value;
                // Recreate task to clear it's cache
                _triggerFetchCalendarTask = null;
            }
        }

        /// <summary>ForeignKey Property for Calendar's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Calendar
		{
			get { return _fk_Calendar; }
			set { _fk_Calendar = value; }
		}


        System.Threading.Tasks.Task<Zetbox.App.Calendar.CalendarBook> _triggerFetchCalendarTask;
        public System.Threading.Tasks.Task<Zetbox.App.Calendar.CalendarBook> TriggerFetchCalendarAsync()
        {
            if (_triggerFetchCalendarTask != null) return _triggerFetchCalendarTask;

            if (_fk_Calendar.HasValue)
                _triggerFetchCalendarTask = Context.FindAsync<Zetbox.App.Calendar.CalendarBook>(_fk_Calendar.Value);
            else
                _triggerFetchCalendarTask = System.Threading.Tasks.Task.FromResult<Zetbox.App.Calendar.CalendarBook>(null);

            _triggerFetchCalendarTask.OnResult(t =>
            {
                if (OnCalendar_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Calendar.CalendarBook>(t.Result);
                    OnCalendar_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchCalendarTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Calendar.CalendarBookMemoryImpl CalendarImpl
        {
            get
            {
                var task = TriggerFetchCalendarAsync();
                task.TryRunSynchronously();
                return (Zetbox.App.Calendar.CalendarBookMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Calendar == null) || (value != null && value.ID == _fk_Calendar))
                {
                    SetInitializedProperty("Calendar");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = CalendarImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Calendar", __oldValue, __newValue);

                if (OnCalendar_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Calendar.CalendarBook>(__oldValue, __newValue);
                    OnCalendar_PreSetter(this, e);
                    __newValue = (Zetbox.App.Calendar.CalendarBookMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Calendar = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Calendar", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnCalendar_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Calendar.CalendarBook>(__oldValue, __newValue);
                    OnCalendar_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Calendar
		public static event PropertyGetterHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider, Zetbox.App.Calendar.CalendarBook> OnCalendar_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider, Zetbox.App.Calendar.CalendarBook> OnCalendar_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider, Zetbox.App.Calendar.CalendarBook> OnCalendar_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider> OnCalendar_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for WorkSchedule
        // fkBackingName=_fk_WorkSchedule; fkGuidBackingName=_fk_guid_WorkSchedule;
        // referencedInterface=Zetbox.App.Calendar.WorkSchedule; moduleNamespace=Zetbox.App.Calendar;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Calendar.WorkSchedule WorkSchedule
        {
            get { return WorkScheduleImpl; }
            set { WorkScheduleImpl = (Zetbox.App.Calendar.WorkScheduleMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<Zetbox.App.Calendar.WorkSchedule> GetProp_WorkSchedule()
        {
            return TriggerFetchWorkScheduleAsync();
        }

        public async System.Threading.Tasks.Task SetProp_WorkSchedule(Zetbox.App.Calendar.WorkSchedule newValue)
        {
            await TriggerFetchWorkScheduleAsync();
            WorkScheduleImpl = (Zetbox.App.Calendar.WorkScheduleMemoryImpl)newValue;
        }

        private int? __fk_WorkScheduleCache;

        private int? _fk_WorkSchedule {
            get
            {
                return __fk_WorkScheduleCache;
            }
            set
            {
                __fk_WorkScheduleCache = value;
                // Recreate task to clear it's cache
                _triggerFetchWorkScheduleTask = null;
            }
        }

        /// <summary>ForeignKey Property for WorkSchedule's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_WorkSchedule
		{
			get { return _fk_WorkSchedule; }
			set { _fk_WorkSchedule = value; }
		}


        System.Threading.Tasks.Task<Zetbox.App.Calendar.WorkSchedule> _triggerFetchWorkScheduleTask;
        public System.Threading.Tasks.Task<Zetbox.App.Calendar.WorkSchedule> TriggerFetchWorkScheduleAsync()
        {
            if (_triggerFetchWorkScheduleTask != null) return _triggerFetchWorkScheduleTask;

            if (_fk_WorkSchedule.HasValue)
                _triggerFetchWorkScheduleTask = Context.FindAsync<Zetbox.App.Calendar.WorkSchedule>(_fk_WorkSchedule.Value);
            else
                _triggerFetchWorkScheduleTask = System.Threading.Tasks.Task.FromResult<Zetbox.App.Calendar.WorkSchedule>(null);

            _triggerFetchWorkScheduleTask.OnResult(t =>
            {
                if (OnWorkSchedule_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Calendar.WorkSchedule>(t.Result);
                    OnWorkSchedule_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchWorkScheduleTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Calendar.WorkScheduleMemoryImpl WorkScheduleImpl
        {
            get
            {
                var task = TriggerFetchWorkScheduleAsync();
                task.TryRunSynchronously();
                return (Zetbox.App.Calendar.WorkScheduleMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_WorkSchedule == null) || (value != null && value.ID == _fk_WorkSchedule))
                {
                    SetInitializedProperty("WorkSchedule");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = WorkScheduleImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("WorkSchedule", __oldValue, __newValue);

                if (OnWorkSchedule_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Calendar.WorkSchedule>(__oldValue, __newValue);
                    OnWorkSchedule_PreSetter(this, e);
                    __newValue = (Zetbox.App.Calendar.WorkScheduleMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_WorkSchedule = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("WorkSchedule", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnWorkSchedule_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Calendar.WorkSchedule>(__oldValue, __newValue);
                    OnWorkSchedule_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for WorkSchedule
		public static event PropertyGetterHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider, Zetbox.App.Calendar.WorkSchedule> OnWorkSchedule_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider, Zetbox.App.Calendar.WorkSchedule> OnWorkSchedule_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider, Zetbox.App.Calendar.WorkSchedule> OnWorkSchedule_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Calendar.WorkScheduleSyncProvider> OnWorkSchedule_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnPerformSync_WorkScheduleSyncProvider")]
        public override void PerformSync()
        {
            // base.PerformSync();
            if (OnPerformSync_WorkScheduleSyncProvider != null)
            {
                OnPerformSync_WorkScheduleSyncProvider(this);
            }
            else
            {
                base.PerformSync();
            }
        }
        public static event PerformSync_Handler<WorkScheduleSyncProvider> OnPerformSync_WorkScheduleSyncProvider;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<WorkScheduleSyncProvider> OnPerformSync_WorkScheduleSyncProvider_CanExec;

        [EventBasedMethod("OnPerformSync_WorkScheduleSyncProvider_CanExec")]
        public override bool PerformSyncCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnPerformSync_WorkScheduleSyncProvider_CanExec != null)
				{
					OnPerformSync_WorkScheduleSyncProvider_CanExec(this, e);
				}
				else
				{
					e.Result = base.PerformSyncCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<WorkScheduleSyncProvider> OnPerformSync_WorkScheduleSyncProvider_CanExecReason;

        [EventBasedMethod("OnPerformSync_WorkScheduleSyncProvider_CanExecReason")]
        public override string PerformSyncCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnPerformSync_WorkScheduleSyncProvider_CanExecReason != null)
				{
					OnPerformSync_WorkScheduleSyncProvider_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.PerformSyncCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Schedules the next sync immediately
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnScheduleSyncNow_WorkScheduleSyncProvider")]
        public override void ScheduleSyncNow()
        {
            // base.ScheduleSyncNow();
            if (OnScheduleSyncNow_WorkScheduleSyncProvider != null)
            {
                OnScheduleSyncNow_WorkScheduleSyncProvider(this);
            }
            else
            {
                base.ScheduleSyncNow();
            }
        }
        public static event ScheduleSyncNow_Handler<WorkScheduleSyncProvider> OnScheduleSyncNow_WorkScheduleSyncProvider;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<WorkScheduleSyncProvider> OnScheduleSyncNow_WorkScheduleSyncProvider_CanExec;

        [EventBasedMethod("OnScheduleSyncNow_WorkScheduleSyncProvider_CanExec")]
        public override bool ScheduleSyncNowCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnScheduleSyncNow_WorkScheduleSyncProvider_CanExec != null)
				{
					OnScheduleSyncNow_WorkScheduleSyncProvider_CanExec(this, e);
				}
				else
				{
					e.Result = base.ScheduleSyncNowCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<WorkScheduleSyncProvider> OnScheduleSyncNow_WorkScheduleSyncProvider_CanExecReason;

        [EventBasedMethod("OnScheduleSyncNow_WorkScheduleSyncProvider_CanExecReason")]
        public override string ScheduleSyncNowCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnScheduleSyncNow_WorkScheduleSyncProvider_CanExecReason != null)
				{
					OnScheduleSyncNow_WorkScheduleSyncProvider_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.ScheduleSyncNowCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(WorkScheduleSyncProvider);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (WorkScheduleSyncProvider)obj;
            var otherImpl = (WorkScheduleSyncProviderMemoryImpl)obj;
            var me = (WorkScheduleSyncProvider)this;

            this._fk_Calendar = otherImpl._fk_Calendar;
            this._fk_WorkSchedule = otherImpl._fk_WorkSchedule;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Calendar":
                    {
                        var __oldValue = _fk_Calendar;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Calendar", __oldValue, __newValue);
                        _fk_Calendar = __newValue;
                        NotifyPropertyChanged("Calendar", __oldValue, __newValue);
                    }
                    break;
                case "WorkSchedule":
                    {
                        var __oldValue = _fk_WorkSchedule;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("WorkSchedule", __oldValue, __newValue);
                        _fk_WorkSchedule = __newValue;
                        NotifyPropertyChanged("WorkSchedule", __oldValue, __newValue);
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
                case "Calendar":
                case "WorkSchedule":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Calendar":
                return TriggerFetchCalendarAsync();
            case "WorkSchedule":
                return TriggerFetchWorkScheduleAsync();
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

            if (_fk_Calendar.HasValue)
                CalendarImpl = (Zetbox.App.Calendar.CalendarBookMemoryImpl)(await Context.FindAsync<Zetbox.App.Calendar.CalendarBook>(_fk_Calendar.Value));
            else
                CalendarImpl = null;

            if (_fk_WorkSchedule.HasValue)
                WorkScheduleImpl = (Zetbox.App.Calendar.WorkScheduleMemoryImpl)(await Context.FindAsync<Zetbox.App.Calendar.WorkSchedule>(_fk_WorkSchedule.Value));
            else
                WorkScheduleImpl = null;
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
                    new PropertyDescriptorMemoryImpl<WorkScheduleSyncProvider, Zetbox.App.Calendar.CalendarBook>(
                        lazyCtx,
                        new Guid("f67558bb-7415-4a41-9196-7c39426746df"),
                        "Calendar",
                        null,
                        obj => obj.Calendar,
                        (obj, val) => obj.Calendar = val,
						obj => OnCalendar_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<WorkScheduleSyncProvider, Zetbox.App.Calendar.WorkSchedule>(
                        lazyCtx,
                        new Guid("72dcb583-17bc-4247-a7c1-39f607b4905c"),
                        "WorkSchedule",
                        null,
                        obj => obj.WorkSchedule,
                        (obj, val) => obj.WorkSchedule = val,
						obj => OnWorkSchedule_IsValid), 
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
        [EventBasedMethod("OnToString_WorkScheduleSyncProvider")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_WorkScheduleSyncProvider != null)
            {
                OnToString_WorkScheduleSyncProvider(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<WorkScheduleSyncProvider> OnToString_WorkScheduleSyncProvider;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_WorkScheduleSyncProvider")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_WorkScheduleSyncProvider != null)
            {
                OnObjectIsValid_WorkScheduleSyncProvider(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<WorkScheduleSyncProvider> OnObjectIsValid_WorkScheduleSyncProvider;

        [EventBasedMethod("OnNotifyPreSave_WorkScheduleSyncProvider")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_WorkScheduleSyncProvider != null) OnNotifyPreSave_WorkScheduleSyncProvider(this);
        }
        public static event ObjectEventHandler<WorkScheduleSyncProvider> OnNotifyPreSave_WorkScheduleSyncProvider;

        [EventBasedMethod("OnNotifyPostSave_WorkScheduleSyncProvider")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_WorkScheduleSyncProvider != null) OnNotifyPostSave_WorkScheduleSyncProvider(this);
        }
        public static event ObjectEventHandler<WorkScheduleSyncProvider> OnNotifyPostSave_WorkScheduleSyncProvider;

        [EventBasedMethod("OnNotifyCreated_WorkScheduleSyncProvider")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Calendar");
            SetNotInitializedProperty("WorkSchedule");
            base.NotifyCreated();
            if (OnNotifyCreated_WorkScheduleSyncProvider != null) OnNotifyCreated_WorkScheduleSyncProvider(this);
        }
        public static event ObjectEventHandler<WorkScheduleSyncProvider> OnNotifyCreated_WorkScheduleSyncProvider;

        [EventBasedMethod("OnNotifyDeleting_WorkScheduleSyncProvider")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_WorkScheduleSyncProvider != null) OnNotifyDeleting_WorkScheduleSyncProvider(this);
            Calendar = null;
            WorkSchedule = null;
        }
        public static event ObjectEventHandler<WorkScheduleSyncProvider> OnNotifyDeleting_WorkScheduleSyncProvider;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(Calendar != null ? Calendar.ID : (int?)null);
            binStream.Write(WorkSchedule != null ? WorkSchedule.ID : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_Calendar = binStream.ReadNullableInt32();
            this._fk_WorkSchedule = binStream.ReadNullableInt32();
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