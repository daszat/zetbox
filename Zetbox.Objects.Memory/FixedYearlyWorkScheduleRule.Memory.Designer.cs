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
    /// This rule applies every year on the same date
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("FixedYearlyWorkScheduleRule")]
    public class FixedYearlyWorkScheduleRuleMemoryImpl : Zetbox.App.Calendar.YearlyWorkScheduleRuleMemoryImpl, FixedYearlyWorkScheduleRule
    {
        private static readonly Guid _objectClassID = new Guid("632137c7-1cb1-4c60-bda6-31aa2060b39a");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public FixedYearlyWorkScheduleRuleMemoryImpl()
            : base(null)
        {
        }

        public FixedYearlyWorkScheduleRuleMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int Day
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Day;
                if (OnDay_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnDay_Getter(this, __e);
                    __result = _Day = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Day != value)
                {
                    var __oldValue = _Day;
                    var __newValue = value;
                    if (OnDay_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnDay_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Day", __oldValue, __newValue);
                    _Day = __newValue;
                    NotifyPropertyChanged("Day", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDay_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnDay_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Day");
                }
            }
        }
        private int _Day;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule, int> OnDay_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule, int> OnDay_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule, int> OnDay_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule> OnDay_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int Month
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Month;
                if (OnMonth_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnMonth_Getter(this, __e);
                    __result = _Month = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Month != value)
                {
                    var __oldValue = _Month;
                    var __newValue = value;
                    if (OnMonth_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnMonth_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Month", __oldValue, __newValue);
                    _Month = __newValue;
                    NotifyPropertyChanged("Month", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnMonth_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnMonth_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Month");
                }
            }
        }
        private int _Month;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule, int> OnMonth_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule, int> OnMonth_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule, int> OnMonth_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Calendar.FixedYearlyWorkScheduleRule> OnMonth_IsValid;

        /// <summary>
        /// Checks if the Rule applies to the given date
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnAppliesTo_FixedYearlyWorkScheduleRule")]
        public override bool AppliesTo(DateTime date)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnAppliesTo_FixedYearlyWorkScheduleRule != null)
            {
                OnAppliesTo_FixedYearlyWorkScheduleRule(this, e, date);
            }
            else
            {
                e.Result = base.AppliesTo(date);
            }
            return e.Result;
        }
        public static event AppliesTo_Handler<FixedYearlyWorkScheduleRule> OnAppliesTo_FixedYearlyWorkScheduleRule;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<FixedYearlyWorkScheduleRule> OnAppliesTo_FixedYearlyWorkScheduleRule_CanExec;

        [EventBasedMethod("OnAppliesTo_FixedYearlyWorkScheduleRule_CanExec")]
        public override bool AppliesToCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnAppliesTo_FixedYearlyWorkScheduleRule_CanExec != null)
				{
					OnAppliesTo_FixedYearlyWorkScheduleRule_CanExec(this, e);
				}
				else
				{
					e.Result = base.AppliesToCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<FixedYearlyWorkScheduleRule> OnAppliesTo_FixedYearlyWorkScheduleRule_CanExecReason;

        [EventBasedMethod("OnAppliesTo_FixedYearlyWorkScheduleRule_CanExecReason")]
        public override string AppliesToCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnAppliesTo_FixedYearlyWorkScheduleRule_CanExecReason != null)
				{
					OnAppliesTo_FixedYearlyWorkScheduleRule_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.AppliesToCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(FixedYearlyWorkScheduleRule);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (FixedYearlyWorkScheduleRule)obj;
            var otherImpl = (FixedYearlyWorkScheduleRuleMemoryImpl)obj;
            var me = (FixedYearlyWorkScheduleRule)this;

            me.Day = other.Day;
            me.Month = other.Month;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Day":
                case "Month":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
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
                    // else
                    new PropertyDescriptorMemoryImpl<FixedYearlyWorkScheduleRule, int>(
                        lazyCtx,
                        new Guid("7d16cd01-93ce-44d2-bb69-ac06f5b61aaf"),
                        "Day",
                        null,
                        obj => obj.Day,
                        (obj, val) => obj.Day = val,
						obj => OnDay_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<FixedYearlyWorkScheduleRule, int>(
                        lazyCtx,
                        new Guid("43be3542-1b21-4423-aeb4-f9e411b2453f"),
                        "Month",
                        null,
                        obj => obj.Month,
                        (obj, val) => obj.Month = val,
						obj => OnMonth_IsValid), 
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
        [EventBasedMethod("OnToString_FixedYearlyWorkScheduleRule")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_FixedYearlyWorkScheduleRule != null)
            {
                OnToString_FixedYearlyWorkScheduleRule(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<FixedYearlyWorkScheduleRule> OnToString_FixedYearlyWorkScheduleRule;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_FixedYearlyWorkScheduleRule")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_FixedYearlyWorkScheduleRule != null)
            {
                OnObjectIsValid_FixedYearlyWorkScheduleRule(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<FixedYearlyWorkScheduleRule> OnObjectIsValid_FixedYearlyWorkScheduleRule;

        [EventBasedMethod("OnNotifyPreSave_FixedYearlyWorkScheduleRule")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_FixedYearlyWorkScheduleRule != null) OnNotifyPreSave_FixedYearlyWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<FixedYearlyWorkScheduleRule> OnNotifyPreSave_FixedYearlyWorkScheduleRule;

        [EventBasedMethod("OnNotifyPostSave_FixedYearlyWorkScheduleRule")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_FixedYearlyWorkScheduleRule != null) OnNotifyPostSave_FixedYearlyWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<FixedYearlyWorkScheduleRule> OnNotifyPostSave_FixedYearlyWorkScheduleRule;

        [EventBasedMethod("OnNotifyCreated_FixedYearlyWorkScheduleRule")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Day");
            SetNotInitializedProperty("Month");
            base.NotifyCreated();
            if (OnNotifyCreated_FixedYearlyWorkScheduleRule != null) OnNotifyCreated_FixedYearlyWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<FixedYearlyWorkScheduleRule> OnNotifyCreated_FixedYearlyWorkScheduleRule;

        [EventBasedMethod("OnNotifyDeleting_FixedYearlyWorkScheduleRule")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_FixedYearlyWorkScheduleRule != null) OnNotifyDeleting_FixedYearlyWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<FixedYearlyWorkScheduleRule> OnNotifyDeleting_FixedYearlyWorkScheduleRule;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Day);
            binStream.Write(this._Month);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Day = binStream.ReadInt32();
            this._Month = binStream.ReadInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Calendar")) XmlStreamer.ToStream(this._Day, xml, "Day", "Zetbox.App.Calendar");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Calendar")) XmlStreamer.ToStream(this._Month, xml, "Month", "Zetbox.App.Calendar");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Calendar|Day":
                this._Day = XmlStreamer.ReadInt32(xml);
                break;
            case "Zetbox.App.Calendar|Month":
                this._Month = XmlStreamer.ReadInt32(xml);
                break;
            }
        }

        #endregion

    }
}