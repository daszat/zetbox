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
    /// A recurrence rule
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RecurrenceRule")]
    public class RecurrenceRuleMemoryImpl : CompoundObjectDefaultImpl, ICompoundObject, RecurrenceRule
    {
        private static readonly Guid _compoundObjectID = new Guid("3d4ec88b-fe8e-452e-a71d-03143a75aeb0");
        public override Guid CompoundObjectID { get { return _compoundObjectID; } }

        [Obsolete]
        public RecurrenceRuleMemoryImpl()
            : base(null)
        {
        }

        public RecurrenceRuleMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
        public RecurrenceRuleMemoryImpl(IPersistenceObject parent, string property) : this(null, parent, property) {} // TODO: pass parent's lazyCtx
        public RecurrenceRuleMemoryImpl(Func<IFrozenContext> lazyCtx, IPersistenceObject parent, string property)
            : base(lazyCtx)
        {
            AttachToObject(parent, property);
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public double? DaysOffset
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DaysOffset;
                if (OnDaysOffset_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnDaysOffset_Getter(this, __e);
                    __result = _DaysOffset = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DaysOffset != value)
                {
                    var __oldValue = _DaysOffset;
                    var __newValue = value;
                    if (OnDaysOffset_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnDaysOffset_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DaysOffset", __oldValue, __newValue);
                    _DaysOffset = __newValue;
                    NotifyPropertyChanged("DaysOffset", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDaysOffset_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnDaysOffset_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("DaysOffset");
				}
            }
        }
        private double? _DaysOffset;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnDaysOffset_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnDaysOffset_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnDaysOffset_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public bool EveryDay
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _EveryDay;
                if (!_isEveryDaySet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("aa9da83f-fbbf-44d0-8e7c-71f306e3481b"));
                    if (__p != null) {
                        _isEveryDaySet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._EveryDay = (bool)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'RecurrenceRule.EveryDay'");
                    }
                }
                if (OnEveryDay_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnEveryDay_Getter(this, __e);
                    __result = _EveryDay = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isEveryDaySet = true;
                if (_EveryDay != value)
                {
                    var __oldValue = _EveryDay;
                    var __newValue = value;
                    if (OnEveryDay_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryDay_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("EveryDay", __oldValue, __newValue);
                    _EveryDay = __newValue;
                    NotifyPropertyChanged("EveryDay", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnEveryDay_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryDay_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("EveryDay");
				}
            }
        }
        private bool _EveryDay;
        private bool _isEveryDaySet = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryDay_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryDay_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryDay_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // enumeration property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public Zetbox.App.Base.DayOfWeek? EveryDayOfWeek
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _EveryDayOfWeek;
                if (OnEveryDayOfWeek_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Base.DayOfWeek?>(__result);
                    OnEveryDayOfWeek_Getter(this, __e);
                    __result = _EveryDayOfWeek = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_EveryDayOfWeek != value)
                {
                    var __oldValue = _EveryDayOfWeek;
                    var __newValue = value;
                    if (OnEveryDayOfWeek_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Base.DayOfWeek?>(__oldValue, __newValue);
                        OnEveryDayOfWeek_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("EveryDayOfWeek", __oldValue, __newValue);
                    _EveryDayOfWeek = __newValue;
                    NotifyPropertyChanged("EveryDayOfWeek", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnEveryDayOfWeek_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Zetbox.App.Base.DayOfWeek?>(__oldValue, __newValue);
                        OnEveryDayOfWeek_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("EveryDayOfWeek");
				}
            }
        }
        private Zetbox.App.Base.DayOfWeek? _EveryDayOfWeek;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, Zetbox.App.Base.DayOfWeek?> OnEveryDayOfWeek_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, Zetbox.App.Base.DayOfWeek?> OnEveryDayOfWeek_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, Zetbox.App.Base.DayOfWeek?> OnEveryDayOfWeek_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public bool EveryMonth
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _EveryMonth;
                if (!_isEveryMonthSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("7681ae11-bc74-48af-ae6d-76fdcac54489"));
                    if (__p != null) {
                        _isEveryMonthSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._EveryMonth = (bool)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'RecurrenceRule.EveryMonth'");
                    }
                }
                if (OnEveryMonth_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnEveryMonth_Getter(this, __e);
                    __result = _EveryMonth = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isEveryMonthSet = true;
                if (_EveryMonth != value)
                {
                    var __oldValue = _EveryMonth;
                    var __newValue = value;
                    if (OnEveryMonth_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryMonth_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("EveryMonth", __oldValue, __newValue);
                    _EveryMonth = __newValue;
                    NotifyPropertyChanged("EveryMonth", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnEveryMonth_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryMonth_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("EveryMonth");
				}
            }
        }
        private bool _EveryMonth;
        private bool _isEveryMonthSet = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryMonth_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryMonth_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryMonth_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public bool EveryQuater
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _EveryQuater;
                if (!_isEveryQuaterSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("ed292ca2-3da3-4004-a8c2-06c746e3a49e"));
                    if (__p != null) {
                        _isEveryQuaterSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._EveryQuater = (bool)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'RecurrenceRule.EveryQuater'");
                    }
                }
                if (OnEveryQuater_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnEveryQuater_Getter(this, __e);
                    __result = _EveryQuater = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isEveryQuaterSet = true;
                if (_EveryQuater != value)
                {
                    var __oldValue = _EveryQuater;
                    var __newValue = value;
                    if (OnEveryQuater_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryQuater_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("EveryQuater", __oldValue, __newValue);
                    _EveryQuater = __newValue;
                    NotifyPropertyChanged("EveryQuater", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnEveryQuater_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryQuater_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("EveryQuater");
				}
            }
        }
        private bool _EveryQuater;
        private bool _isEveryQuaterSet = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryQuater_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryQuater_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryQuater_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public bool EveryYear
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _EveryYear;
                if (!_isEveryYearSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("4638ca9a-aa2b-4ef2-9450-9b5bb39f0de4"));
                    if (__p != null) {
                        _isEveryYearSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._EveryYear = (bool)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'RecurrenceRule.EveryYear'");
                    }
                }
                if (OnEveryYear_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnEveryYear_Getter(this, __e);
                    __result = _EveryYear = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isEveryYearSet = true;
                if (_EveryYear != value)
                {
                    var __oldValue = _EveryYear;
                    var __newValue = value;
                    if (OnEveryYear_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryYear_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("EveryYear", __oldValue, __newValue);
                    _EveryYear = __newValue;
                    NotifyPropertyChanged("EveryYear", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnEveryYear_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnEveryYear_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("EveryYear");
				}
            }
        }
        private bool _EveryYear;
        private bool _isEveryYearSet = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryYear_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryYear_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, bool> OnEveryYear_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public double? HoursOffset
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _HoursOffset;
                if (OnHoursOffset_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnHoursOffset_Getter(this, __e);
                    __result = _HoursOffset = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_HoursOffset != value)
                {
                    var __oldValue = _HoursOffset;
                    var __newValue = value;
                    if (OnHoursOffset_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnHoursOffset_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("HoursOffset", __oldValue, __newValue);
                    _HoursOffset = __newValue;
                    NotifyPropertyChanged("HoursOffset", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnHoursOffset_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnHoursOffset_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("HoursOffset");
				}
            }
        }
        private double? _HoursOffset;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnHoursOffset_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnHoursOffset_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnHoursOffset_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public double? MinutesOffset
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _MinutesOffset;
                if (OnMinutesOffset_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double?>(__result);
                    OnMinutesOffset_Getter(this, __e);
                    __result = _MinutesOffset = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MinutesOffset != value)
                {
                    var __oldValue = _MinutesOffset;
                    var __newValue = value;
                    if (OnMinutesOffset_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double?>(__oldValue, __newValue);
                        OnMinutesOffset_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("MinutesOffset", __oldValue, __newValue);
                    _MinutesOffset = __newValue;
                    NotifyPropertyChanged("MinutesOffset", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnMinutesOffset_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double?>(__oldValue, __newValue);
                        OnMinutesOffset_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("MinutesOffset");
				}
            }
        }
        private double? _MinutesOffset;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnMinutesOffset_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnMinutesOffset_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, double?> OnMinutesOffset_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int? MonthsOffset
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _MonthsOffset;
                if (OnMonthsOffset_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnMonthsOffset_Getter(this, __e);
                    __result = _MonthsOffset = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MonthsOffset != value)
                {
                    var __oldValue = _MonthsOffset;
                    var __newValue = value;
                    if (OnMonthsOffset_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMonthsOffset_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("MonthsOffset", __oldValue, __newValue);
                    _MonthsOffset = __newValue;
                    NotifyPropertyChanged("MonthsOffset", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnMonthsOffset_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMonthsOffset_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("MonthsOffset");
				}
            }
        }
        private int? _MonthsOffset;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.RecurrenceRule, int?> OnMonthsOffset_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.RecurrenceRule, int?> OnMonthsOffset_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.RecurrenceRule, int?> OnMonthsOffset_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetCurrent_RecurrenceRule")]
        public virtual DateTime GetCurrent()
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnGetCurrent_RecurrenceRule != null)
            {
                OnGetCurrent_RecurrenceRule(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on RecurrenceRule.GetCurrent");
            }
            return e.Result;
        }
        public delegate void GetCurrent_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret);
        public static event GetCurrent_Handler<RecurrenceRule> OnGetCurrent_RecurrenceRule;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<RecurrenceRule> OnGetCurrent_RecurrenceRule_CanExec;

        [EventBasedMethod("OnGetCurrent_RecurrenceRule_CanExec")]
        public virtual bool GetCurrentCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetCurrent_RecurrenceRule_CanExec != null)
				{
					OnGetCurrent_RecurrenceRule_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<RecurrenceRule> OnGetCurrent_RecurrenceRule_CanExecReason;

        [EventBasedMethod("OnGetCurrent_RecurrenceRule_CanExecReason")]
        public virtual string GetCurrentCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetCurrent_RecurrenceRule_CanExecReason != null)
				{
					OnGetCurrent_RecurrenceRule_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetCurrent1_RecurrenceRule")]
        public virtual DateTime GetCurrent(DateTime dt)
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnGetCurrent1_RecurrenceRule != null)
            {
                OnGetCurrent1_RecurrenceRule(this, e, dt);
            }
            else
            {
                throw new NotImplementedException("No handler registered on RecurrenceRule.GetCurrent");
            }
            return e.Result;
        }
        public delegate void GetCurrent1_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret, DateTime dt);
        public static event GetCurrent1_Handler<RecurrenceRule> OnGetCurrent1_RecurrenceRule;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetNext_RecurrenceRule")]
        public virtual DateTime GetNext()
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnGetNext_RecurrenceRule != null)
            {
                OnGetNext_RecurrenceRule(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on RecurrenceRule.GetNext");
            }
            return e.Result;
        }
        public delegate void GetNext_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret);
        public static event GetNext_Handler<RecurrenceRule> OnGetNext_RecurrenceRule;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<RecurrenceRule> OnGetNext_RecurrenceRule_CanExec;

        [EventBasedMethod("OnGetNext_RecurrenceRule_CanExec")]
        public virtual bool GetNextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetNext_RecurrenceRule_CanExec != null)
				{
					OnGetNext_RecurrenceRule_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<RecurrenceRule> OnGetNext_RecurrenceRule_CanExecReason;

        [EventBasedMethod("OnGetNext_RecurrenceRule_CanExecReason")]
        public virtual string GetNextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetNext_RecurrenceRule_CanExecReason != null)
				{
					OnGetNext_RecurrenceRule_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetNext1_RecurrenceRule")]
        public virtual DateTime GetNext(DateTime dt)
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnGetNext1_RecurrenceRule != null)
            {
                OnGetNext1_RecurrenceRule(this, e, dt);
            }
            else
            {
                throw new NotImplementedException("No handler registered on RecurrenceRule.GetNext");
            }
            return e.Result;
        }
        public delegate void GetNext1_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret, DateTime dt);
        public static event GetNext1_Handler<RecurrenceRule> OnGetNext1_RecurrenceRule;

        public override Type GetImplementedInterface()
        {
            return typeof(RecurrenceRule);
        }

        public override void ApplyChangesFrom(ICompoundObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (RecurrenceRule)obj;
            var otherImpl = (RecurrenceRuleMemoryImpl)obj;
            var me = (RecurrenceRule)this;

            me.DaysOffset = other.DaysOffset;
            me.EveryDay = other.EveryDay;
            me.EveryDayOfWeek = other.EveryDayOfWeek;
            me.EveryMonth = other.EveryMonth;
            me.EveryQuater = other.EveryQuater;
            me.EveryYear = other.EveryYear;
            me.HoursOffset = other.HoursOffset;
            me.MinutesOffset = other.MinutesOffset;
            me.MonthsOffset = other.MonthsOffset;
        }
        #region Zetbox.Generator.Templates.CompoundObjects.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_RecurrenceRule")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RecurrenceRule != null)
            {
                OnToString_RecurrenceRule(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RecurrenceRule> OnToString_RecurrenceRule;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_RecurrenceRule")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_RecurrenceRule != null)
            {
                OnObjectIsValid_RecurrenceRule(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<RecurrenceRule> OnObjectIsValid_RecurrenceRule;

        #endregion // Zetbox.Generator.Templates.CompoundObjects.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._DaysOffset);
            binStream.Write(this._isEveryDaySet);
            if (this._isEveryDaySet) {
                binStream.Write(this._EveryDay);
            }
            binStream.Write((int?)((Zetbox.App.Base.RecurrenceRule)this).EveryDayOfWeek);
            binStream.Write(this._isEveryMonthSet);
            if (this._isEveryMonthSet) {
                binStream.Write(this._EveryMonth);
            }
            binStream.Write(this._isEveryQuaterSet);
            if (this._isEveryQuaterSet) {
                binStream.Write(this._EveryQuater);
            }
            binStream.Write(this._isEveryYearSet);
            if (this._isEveryYearSet) {
                binStream.Write(this._EveryYear);
            }
            binStream.Write(this._HoursOffset);
            binStream.Write(this._MinutesOffset);
            binStream.Write(this._MonthsOffset);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._DaysOffset = binStream.ReadNullableDouble();
            this._isEveryDaySet = binStream.ReadBoolean();
            if (this._isEveryDaySet) {
                this._EveryDay = binStream.ReadBoolean();
            }
            ((Zetbox.App.Base.RecurrenceRule)this).EveryDayOfWeek = (Zetbox.App.Base.DayOfWeek?)binStream.ReadNullableInt32();
            this._isEveryMonthSet = binStream.ReadBoolean();
            if (this._isEveryMonthSet) {
                this._EveryMonth = binStream.ReadBoolean();
            }
            this._isEveryQuaterSet = binStream.ReadBoolean();
            if (this._isEveryQuaterSet) {
                this._EveryQuater = binStream.ReadBoolean();
            }
            this._isEveryYearSet = binStream.ReadBoolean();
            if (this._isEveryYearSet) {
                this._EveryYear = binStream.ReadBoolean();
            }
            this._HoursOffset = binStream.ReadNullableDouble();
            this._MinutesOffset = binStream.ReadNullableDouble();
            this._MonthsOffset = binStream.ReadNullableInt32();
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