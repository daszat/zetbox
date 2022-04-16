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
    /// Tests the RecurrenceRule
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RecurrenceRuleTester")]
    public class RecurrenceRuleTesterMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, RecurrenceRuleTester
    {
        private static readonly Guid _objectClassID = new Guid("b6c60adc-19f0-4169-8b78-53d7eaa549f7");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public RecurrenceRuleTesterMemoryImpl()
            : base(null)
        {
            RuleImpl = new Zetbox.App.Base.RecurrenceRuleMemoryImpl(null, this, "Rule");
        }

        public RecurrenceRuleTesterMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
            RuleImpl = new Zetbox.App.Base.RecurrenceRuleMemoryImpl(lazyCtx, this, "Rule");
        }

        /// <summary>
        /// 
        /// </summary>
        // CompoundObject property
        // BEGIN Zetbox.Generator.Templates.Properties.CompoundObjectPropertyTemplate
        // implement the user-visible interface
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.RecurrenceRule Rule
        {
            get { return RuleImpl; }
            set { RuleImpl = (Zetbox.App.Base.RecurrenceRuleMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        /// <summary>backing store for Rule</summary>
        private Zetbox.App.Base.RecurrenceRuleMemoryImpl _Rule;

        /// <summary>backing property for Rule, takes care of attaching/detaching the values</summary>
        public Zetbox.App.Base.RecurrenceRuleMemoryImpl RuleImpl
        {
            get
            {
                return _Rule;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value == null)
                    throw new ArgumentNullException("value");
                if (!object.Equals(_Rule, value))
                {
                    var __oldValue = _Rule;
                    var __newValue = value;

                    NotifyPropertyChanging("Rule", __oldValue, __newValue);

                    if (_Rule != null)
                    {
                        _Rule.DetachFromObject(this, "Rule");
                    }
                    __newValue = (Zetbox.App.Base.RecurrenceRuleMemoryImpl)__newValue.Clone();
                    _Rule = __newValue;
                    _Rule.AttachToObject(this, "Rule");

                    NotifyPropertyChanged("Rule", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.CompoundObjectPropertyTemplate
        public static event PropertyIsValidHandler<Zetbox.App.Test.RecurrenceRuleTester> OnRule_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetCurrent_RecurrenceRuleTester")]
        public virtual DateTime GetCurrent(DateTime? dt)
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnGetCurrent_RecurrenceRuleTester != null)
            {
                OnGetCurrent_RecurrenceRuleTester(this, e, dt);
            }
            else
            {
                throw new NotImplementedException("No handler registered on RecurrenceRuleTester.GetCurrent");
            }
            return e.Result;
        }
        public delegate void GetCurrent_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret, DateTime? dt);
        public static event GetCurrent_Handler<RecurrenceRuleTester> OnGetCurrent_RecurrenceRuleTester;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<RecurrenceRuleTester> OnGetCurrent_RecurrenceRuleTester_CanExec;

        [EventBasedMethod("OnGetCurrent_RecurrenceRuleTester_CanExec")]
        public virtual bool GetCurrentCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetCurrent_RecurrenceRuleTester_CanExec != null)
				{
					OnGetCurrent_RecurrenceRuleTester_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<RecurrenceRuleTester> OnGetCurrent_RecurrenceRuleTester_CanExecReason;

        [EventBasedMethod("OnGetCurrent_RecurrenceRuleTester_CanExecReason")]
        public virtual string GetCurrentCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetCurrent_RecurrenceRuleTester_CanExecReason != null)
				{
					OnGetCurrent_RecurrenceRuleTester_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetNext_RecurrenceRuleTester")]
        public virtual DateTime GetNext(DateTime? dt)
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnGetNext_RecurrenceRuleTester != null)
            {
                OnGetNext_RecurrenceRuleTester(this, e, dt);
            }
            else
            {
                throw new NotImplementedException("No handler registered on RecurrenceRuleTester.GetNext");
            }
            return e.Result;
        }
        public delegate void GetNext_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret, DateTime? dt);
        public static event GetNext_Handler<RecurrenceRuleTester> OnGetNext_RecurrenceRuleTester;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<RecurrenceRuleTester> OnGetNext_RecurrenceRuleTester_CanExec;

        [EventBasedMethod("OnGetNext_RecurrenceRuleTester_CanExec")]
        public virtual bool GetNextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetNext_RecurrenceRuleTester_CanExec != null)
				{
					OnGetNext_RecurrenceRuleTester_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<RecurrenceRuleTester> OnGetNext_RecurrenceRuleTester_CanExecReason;

        [EventBasedMethod("OnGetNext_RecurrenceRuleTester_CanExecReason")]
        public virtual string GetNextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetNext_RecurrenceRuleTester_CanExecReason != null)
				{
					OnGetNext_RecurrenceRuleTester_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(RecurrenceRuleTester);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (RecurrenceRuleTester)obj;
            var otherImpl = (RecurrenceRuleTesterMemoryImpl)obj;
            var me = (RecurrenceRuleTester)this;

            if (me.Rule == null && other.Rule != null) {
                me.Rule = (Zetbox.App.Base.RecurrenceRule)other.Rule.Clone();
            } else if (me.Rule != null && other.Rule == null) {
                me.Rule = null;
            } else if (me.Rule != null && other.Rule != null) {
                me.Rule.ApplyChangesFrom(other.Rule);
            }
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
                case "Rule":
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
                    new PropertyDescriptorMemoryImpl<RecurrenceRuleTester, Zetbox.App.Base.RecurrenceRule>(
                        lazyCtx,
                        new Guid("404eda18-44aa-456c-aaa7-c1c45c6b7008"),
                        "Rule",
                        null,
                        obj => obj.Rule,
                        (obj, val) => obj.Rule = val,
						obj => OnRule_IsValid), 
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
        [EventBasedMethod("OnToString_RecurrenceRuleTester")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RecurrenceRuleTester != null)
            {
                OnToString_RecurrenceRuleTester(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RecurrenceRuleTester> OnToString_RecurrenceRuleTester;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_RecurrenceRuleTester")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_RecurrenceRuleTester != null)
            {
                OnObjectIsValid_RecurrenceRuleTester(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<RecurrenceRuleTester> OnObjectIsValid_RecurrenceRuleTester;

        [EventBasedMethod("OnNotifyPreSave_RecurrenceRuleTester")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_RecurrenceRuleTester != null) OnNotifyPreSave_RecurrenceRuleTester(this);
        }
        public static event ObjectEventHandler<RecurrenceRuleTester> OnNotifyPreSave_RecurrenceRuleTester;

        [EventBasedMethod("OnNotifyPostSave_RecurrenceRuleTester")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_RecurrenceRuleTester != null) OnNotifyPostSave_RecurrenceRuleTester(this);
        }
        public static event ObjectEventHandler<RecurrenceRuleTester> OnNotifyPostSave_RecurrenceRuleTester;

        [EventBasedMethod("OnNotifyCreated_RecurrenceRuleTester")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_RecurrenceRuleTester != null) OnNotifyCreated_RecurrenceRuleTester(this);
        }
        public static event ObjectEventHandler<RecurrenceRuleTester> OnNotifyCreated_RecurrenceRuleTester;

        [EventBasedMethod("OnNotifyDeleting_RecurrenceRuleTester")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_RecurrenceRuleTester != null) OnNotifyDeleting_RecurrenceRuleTester(this);
        }
        public static event ObjectEventHandler<RecurrenceRuleTester> OnNotifyDeleting_RecurrenceRuleTester;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Rule);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            {
                // use backing store to avoid notifications
                this.RuleImpl = binStream.ReadCompoundObject<Zetbox.App.Base.RecurrenceRuleMemoryImpl>();
                this.RuleImpl.AttachToObject(this, "Rule");
            }
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