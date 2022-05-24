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
    /// This rule applies every year, n days relative to easter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EasterWorkScheduleRule")]
    public class EasterWorkScheduleRuleMemoryImpl : Zetbox.App.Calendar.YearlyWorkScheduleRuleMemoryImpl, EasterWorkScheduleRule
    {
        private static readonly Guid _objectClassID = new Guid("d321c926-952d-4810-904b-a57251dd5e9d");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public EasterWorkScheduleRuleMemoryImpl()
            : base(null)
        {
        }

        public EasterWorkScheduleRuleMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Offset to eater. Null or zero, if easter is meant. Negative numbers are before easter.
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int? Offset
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Offset;
                if (OnOffset_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnOffset_Getter(this, __e);
                    __result = _Offset = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Offset != value)
                {
                    var __oldValue = _Offset;
                    var __newValue = value;
                    if (OnOffset_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOffset_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Offset", __oldValue, __newValue);
                    _Offset = __newValue;
                    NotifyPropertyChanged("Offset", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnOffset_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOffset_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Offset");
                }
            }
        }
        private int? _Offset;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Calendar.EasterWorkScheduleRule, int?> OnOffset_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Calendar.EasterWorkScheduleRule, int?> OnOffset_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Calendar.EasterWorkScheduleRule, int?> OnOffset_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Calendar.EasterWorkScheduleRule> OnOffset_IsValid;

        /// <summary>
        /// Checks if the Rule applies to the given date
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnAppliesTo_EasterWorkScheduleRule")]
        public override async System.Threading.Tasks.Task<bool> AppliesTo(DateTime date)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnAppliesTo_EasterWorkScheduleRule != null)
            {
                await OnAppliesTo_EasterWorkScheduleRule(this, e, date);
            }
            else
            {
                e.Result = await base.AppliesTo(date);
            }
            return e.Result;
        }
        public static event AppliesTo_Handler<EasterWorkScheduleRule> OnAppliesTo_EasterWorkScheduleRule;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EasterWorkScheduleRule> OnAppliesTo_EasterWorkScheduleRule_CanExec;

        [EventBasedMethod("OnAppliesTo_EasterWorkScheduleRule_CanExec")]
        public override bool AppliesToCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnAppliesTo_EasterWorkScheduleRule_CanExec != null)
				{
					OnAppliesTo_EasterWorkScheduleRule_CanExec(this, e);
				}
				else
				{
					e.Result = base.AppliesToCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EasterWorkScheduleRule> OnAppliesTo_EasterWorkScheduleRule_CanExecReason;

        [EventBasedMethod("OnAppliesTo_EasterWorkScheduleRule_CanExecReason")]
        public override string AppliesToCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnAppliesTo_EasterWorkScheduleRule_CanExecReason != null)
				{
					OnAppliesTo_EasterWorkScheduleRule_CanExecReason(this, e);
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
            return typeof(EasterWorkScheduleRule);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (EasterWorkScheduleRule)obj;
            var otherImpl = (EasterWorkScheduleRuleMemoryImpl)obj;
            var me = (EasterWorkScheduleRule)this;

            me.Offset = other.Offset;
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
                case "Offset":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
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
                    // else
                    new PropertyDescriptorMemoryImpl<EasterWorkScheduleRule, int?>(
                        lazyCtx,
                        new Guid("0fdcab86-001e-429d-af31-a5d0df5e6c75"),
                        "Offset",
                        null,
                        obj => obj.Offset,
                        (obj, val) => obj.Offset = val,
						obj => OnOffset_IsValid), 
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
        [EventBasedMethod("OnToString_EasterWorkScheduleRule")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EasterWorkScheduleRule != null)
            {
                OnToString_EasterWorkScheduleRule(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<EasterWorkScheduleRule> OnToString_EasterWorkScheduleRule;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_EasterWorkScheduleRule")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_EasterWorkScheduleRule != null)
            {
                OnObjectIsValid_EasterWorkScheduleRule(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<EasterWorkScheduleRule> OnObjectIsValid_EasterWorkScheduleRule;

        [EventBasedMethod("OnNotifyPreSave_EasterWorkScheduleRule")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_EasterWorkScheduleRule != null) OnNotifyPreSave_EasterWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<EasterWorkScheduleRule> OnNotifyPreSave_EasterWorkScheduleRule;

        [EventBasedMethod("OnNotifyPostSave_EasterWorkScheduleRule")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_EasterWorkScheduleRule != null) OnNotifyPostSave_EasterWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<EasterWorkScheduleRule> OnNotifyPostSave_EasterWorkScheduleRule;

        [EventBasedMethod("OnNotifyCreated_EasterWorkScheduleRule")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Offset");
            base.NotifyCreated();
            if (OnNotifyCreated_EasterWorkScheduleRule != null) OnNotifyCreated_EasterWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<EasterWorkScheduleRule> OnNotifyCreated_EasterWorkScheduleRule;

        [EventBasedMethod("OnNotifyDeleting_EasterWorkScheduleRule")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_EasterWorkScheduleRule != null) OnNotifyDeleting_EasterWorkScheduleRule(this);
        }
        public static event ObjectEventHandler<EasterWorkScheduleRule> OnNotifyDeleting_EasterWorkScheduleRule;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Offset);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Offset = binStream.ReadNullableInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Calendar")) XmlStreamer.ToStream(this._Offset, xml, "Offset", "Zetbox.App.Calendar");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Calendar|Offset":
                this._Offset = XmlStreamer.ReadNullableInt32(xml);
                break;
            }
        }

        #endregion

    }
}