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
    /// Metadefinition Object for Enumerations.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Enumeration")]
    public class EnumerationMemoryImpl : Zetbox.App.Base.DataTypeMemoryImpl, Enumeration
    {
        private static readonly Guid _objectClassID = new Guid("ee475de2-d626-49e9-9e40-6bb12cb026d4");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public EnumerationMemoryImpl()
            : base(null)
        {
        }

        public EnumerationMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Enumeration Entries are Flags
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public bool AreFlags
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _AreFlags;
                if (OnAreFlags_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnAreFlags_Getter(this, __e);
                    __result = _AreFlags = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_AreFlags != value)
                {
                    var __oldValue = _AreFlags;
                    var __newValue = value;
                    if (OnAreFlags_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnAreFlags_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("AreFlags", __oldValue, __newValue);
                    _AreFlags = __newValue;
                    NotifyPropertyChanged("AreFlags", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnAreFlags_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnAreFlags_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("AreFlags");
                }
            }
        }
        private bool _AreFlags;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.Enumeration, bool> OnAreFlags_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.Enumeration, bool> OnAreFlags_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.Enumeration, bool> OnAreFlags_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.Enumeration> OnAreFlags_IsValid;

        /// <summary>
        /// Einträge der Enumeration
        /// </summary>
        // object list property
        // Zetbox.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Zetbox.App.Base.EnumerationEntry> EnumerationEntries
        {
            get
            {
                if (_EnumerationEntries == null)
                {
                    var task = TriggerFetchEnumerationEntriesAsync();
                    task.TryRunSynchronously();
                }
                return _EnumerationEntries;
            }
        }

        public async System.Threading.Tasks.Task<IList<Zetbox.App.Base.EnumerationEntry>> GetProp_EnumerationEntries()
        {
            await TriggerFetchEnumerationEntriesAsync();
            return _EnumerationEntries;
        }

        System.Threading.Tasks.Task _triggerFetchEnumerationEntriesTask;
        public System.Threading.Tasks.Task TriggerFetchEnumerationEntriesAsync()
        {
            if (_triggerFetchEnumerationEntriesTask != null) return _triggerFetchEnumerationEntriesTask;
            System.Threading.Tasks.Task task;

            List<Zetbox.App.Base.EnumerationEntry> serverList = null;
            if (Helper.IsPersistedObject(this))
            {
                task = Context.GetListOfAsync<Zetbox.App.Base.EnumerationEntry>(this, "EnumerationEntries")
                    .OnResult(t =>
                    {
                        serverList = t.Result;
                    });
            }
            else
            {
                task = System.Threading.Tasks.Task.FromResult(new List<Zetbox.App.Base.EnumerationEntry>()).OnResult(t =>
                {
                    serverList = t.Result;
                });
            }

            task = task.OnResult(t =>
            {
                _EnumerationEntries = new OneNRelationList<Zetbox.App.Base.EnumerationEntry>(
                    "Enumeration",
                    "EnumerationEntries_pos",
                    this,
                    OnEnumerationEntriesCollectionChanged,
                    serverList);
            });
            return _triggerFetchEnumerationEntriesTask = task;
        }

        internal void OnEnumerationEntriesCollectionChanged()
        {
            NotifyPropertyChanged("EnumerationEntries", null, null);
            if (OnEnumerationEntries_PostSetter != null && IsAttached)
                OnEnumerationEntries_PostSetter(this);
        }

        private OneNRelationList<Zetbox.App.Base.EnumerationEntry> _EnumerationEntries;
public static event PropertyListChangedHandler<Zetbox.App.Base.Enumeration> OnEnumerationEntries_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.Enumeration> OnEnumerationEntries_IsValid;

        /// <summary>
        /// Property wizard
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnAddProperty_Enumeration")]
        public override Zetbox.App.Base.Property AddProperty()
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.Property>();
            if (OnAddProperty_Enumeration != null)
            {
                OnAddProperty_Enumeration(this, e);
            }
            else
            {
                e.Result = base.AddProperty();
            }
            return e.Result;
        }
        public static event AddProperty_Handler<Enumeration> OnAddProperty_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnAddProperty_Enumeration_CanExec;

        [EventBasedMethod("OnAddProperty_Enumeration_CanExec")]
        public override bool AddPropertyCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnAddProperty_Enumeration_CanExec != null)
				{
					OnAddProperty_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = base.AddPropertyCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnAddProperty_Enumeration_CanExecReason;

        [EventBasedMethod("OnAddProperty_Enumeration_CanExecReason")]
        public override string AddPropertyCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnAddProperty_Enumeration_CanExecReason != null)
				{
					OnAddProperty_Enumeration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.AddPropertyCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDataType_Enumeration")]
        public override System.Type GetDataType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Enumeration != null)
            {
                OnGetDataType_Enumeration(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
        public static event GetDataType_Handler<Enumeration> OnGetDataType_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetDataType_Enumeration_CanExec;

        [EventBasedMethod("OnGetDataType_Enumeration_CanExec")]
        public override bool GetDataTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDataType_Enumeration_CanExec != null)
				{
					OnGetDataType_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetDataType_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetDataType_Enumeration_CanExecReason")]
        public override string GetDataTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDataType_Enumeration_CanExecReason != null)
				{
					OnGetDataType_Enumeration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDataTypeString_Enumeration")]
        public override string GetDataTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Enumeration != null)
            {
                OnGetDataTypeString_Enumeration(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
        public static event GetDataTypeString_Handler<Enumeration> OnGetDataTypeString_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetDataTypeString_Enumeration_CanExec;

        [EventBasedMethod("OnGetDataTypeString_Enumeration_CanExec")]
        public override bool GetDataTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDataTypeString_Enumeration_CanExec != null)
				{
					OnGetDataTypeString_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetDataTypeString_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetDataTypeString_Enumeration_CanExecReason")]
        public override string GetDataTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDataTypeString_Enumeration_CanExecReason != null)
				{
					OnGetDataTypeString_Enumeration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetEntryByName_Enumeration")]
        public virtual Zetbox.App.Base.EnumerationEntry GetEntryByName(string name)
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry>();
            if (OnGetEntryByName_Enumeration != null)
            {
                OnGetEntryByName_Enumeration(this, e, name);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Enumeration.GetEntryByName");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetEntryByName_Handler<T>(T obj, MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry> ret, string name);
        public static event GetEntryByName_Handler<Enumeration> OnGetEntryByName_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetEntryByName_Enumeration_CanExec;

        [EventBasedMethod("OnGetEntryByName_Enumeration_CanExec")]
        public virtual bool GetEntryByNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetEntryByName_Enumeration_CanExec != null)
				{
					OnGetEntryByName_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetEntryByName_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetEntryByName_Enumeration_CanExecReason")]
        public virtual string GetEntryByNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetEntryByName_Enumeration_CanExecReason != null)
				{
					OnGetEntryByName_Enumeration_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetEntryByValue_Enumeration")]
        public virtual Zetbox.App.Base.EnumerationEntry GetEntryByValue(int val)
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry>();
            if (OnGetEntryByValue_Enumeration != null)
            {
                OnGetEntryByValue_Enumeration(this, e, val);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Enumeration.GetEntryByValue");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetEntryByValue_Handler<T>(T obj, MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry> ret, int val);
        public static event GetEntryByValue_Handler<Enumeration> OnGetEntryByValue_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetEntryByValue_Enumeration_CanExec;

        [EventBasedMethod("OnGetEntryByValue_Enumeration_CanExec")]
        public virtual bool GetEntryByValueCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetEntryByValue_Enumeration_CanExec != null)
				{
					OnGetEntryByValue_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetEntryByValue_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetEntryByValue_Enumeration_CanExecReason")]
        public virtual string GetEntryByValueCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetEntryByValue_Enumeration_CanExecReason != null)
				{
					OnGetEntryByValue_Enumeration_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetLabelByName_Enumeration")]
        public virtual string GetLabelByName(string name)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabelByName_Enumeration != null)
            {
                OnGetLabelByName_Enumeration(this, e, name);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Enumeration.GetLabelByName");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetLabelByName_Handler<T>(T obj, MethodReturnEventArgs<string> ret, string name);
        public static event GetLabelByName_Handler<Enumeration> OnGetLabelByName_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetLabelByName_Enumeration_CanExec;

        [EventBasedMethod("OnGetLabelByName_Enumeration_CanExec")]
        public virtual bool GetLabelByNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabelByName_Enumeration_CanExec != null)
				{
					OnGetLabelByName_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetLabelByName_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetLabelByName_Enumeration_CanExecReason")]
        public virtual string GetLabelByNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabelByName_Enumeration_CanExecReason != null)
				{
					OnGetLabelByName_Enumeration_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetLabelByValue_Enumeration")]
        public virtual string GetLabelByValue(int val)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabelByValue_Enumeration != null)
            {
                OnGetLabelByValue_Enumeration(this, e, val);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Enumeration.GetLabelByValue");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetLabelByValue_Handler<T>(T obj, MethodReturnEventArgs<string> ret, int val);
        public static event GetLabelByValue_Handler<Enumeration> OnGetLabelByValue_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetLabelByValue_Enumeration_CanExec;

        [EventBasedMethod("OnGetLabelByValue_Enumeration_CanExec")]
        public virtual bool GetLabelByValueCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabelByValue_Enumeration_CanExec != null)
				{
					OnGetLabelByValue_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetLabelByValue_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetLabelByValue_Enumeration_CanExecReason")]
        public virtual string GetLabelByValueCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabelByValue_Enumeration_CanExecReason != null)
				{
					OnGetLabelByValue_Enumeration_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetName_Enumeration")]
        public virtual string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_Enumeration != null)
            {
                OnGetName_Enumeration(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Enumeration.GetName");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetName_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
        public static event GetName_Handler<Enumeration> OnGetName_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnGetName_Enumeration_CanExec;

        [EventBasedMethod("OnGetName_Enumeration_CanExec")]
        public virtual bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_Enumeration_CanExec != null)
				{
					OnGetName_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnGetName_Enumeration_CanExecReason;

        [EventBasedMethod("OnGetName_Enumeration_CanExecReason")]
        public virtual string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_Enumeration_CanExecReason != null)
				{
					OnGetName_Enumeration_CanExecReason(this, e);
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
        /// Implements all available interfaces as Properties and Methods
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnImplementInterfaces_Enumeration")]
        public override void ImplementInterfaces()
        {
            // base.ImplementInterfaces();
            if (OnImplementInterfaces_Enumeration != null)
            {
                OnImplementInterfaces_Enumeration(this);
            }
            else
            {
                base.ImplementInterfaces();
            }
        }
        public static event ImplementInterfaces_Handler<Enumeration> OnImplementInterfaces_Enumeration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Enumeration> OnImplementInterfaces_Enumeration_CanExec;

        [EventBasedMethod("OnImplementInterfaces_Enumeration_CanExec")]
        public override bool ImplementInterfacesCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnImplementInterfaces_Enumeration_CanExec != null)
				{
					OnImplementInterfaces_Enumeration_CanExec(this, e);
				}
				else
				{
					e.Result = base.ImplementInterfacesCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Enumeration> OnImplementInterfaces_Enumeration_CanExecReason;

        [EventBasedMethod("OnImplementInterfaces_Enumeration_CanExecReason")]
        public override string ImplementInterfacesCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnImplementInterfaces_Enumeration_CanExecReason != null)
				{
					OnImplementInterfaces_Enumeration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.ImplementInterfacesCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(Enumeration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Enumeration)obj;
            var otherImpl = (EnumerationMemoryImpl)obj;
            var me = (Enumeration)this;

            me.AreFlags = other.AreFlags;
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
                case "AreFlags":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "EnumerationEntries":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "EnumerationEntries":
                return TriggerFetchEnumerationEntriesAsync();
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
                    new PropertyDescriptorMemoryImpl<Enumeration, bool>(
                        lazyCtx,
                        new Guid("1ef92eea-d8b3-4f95-a694-9ca09ceff0e5"),
                        "AreFlags",
                        null,
                        obj => obj.AreFlags,
                        (obj, val) => obj.AreFlags = val,
						obj => OnAreFlags_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Enumeration, IList<Zetbox.App.Base.EnumerationEntry>>(
                        lazyCtx,
                        new Guid("1619c8a7-b969-4c05-851c-7a2545cda484"),
                        "EnumerationEntries",
                        null,
                        obj => obj.EnumerationEntries,
                        null, // lists are read-only properties
                        obj => OnEnumerationEntries_IsValid), 
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
        [EventBasedMethod("OnToString_Enumeration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Enumeration != null)
            {
                OnToString_Enumeration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Enumeration> OnToString_Enumeration;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Enumeration")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Enumeration != null)
            {
                OnObjectIsValid_Enumeration(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Enumeration> OnObjectIsValid_Enumeration;

        [EventBasedMethod("OnNotifyPreSave_Enumeration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Enumeration != null) OnNotifyPreSave_Enumeration(this);
        }
        public static event ObjectEventHandler<Enumeration> OnNotifyPreSave_Enumeration;

        [EventBasedMethod("OnNotifyPostSave_Enumeration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Enumeration != null) OnNotifyPostSave_Enumeration(this);
        }
        public static event ObjectEventHandler<Enumeration> OnNotifyPostSave_Enumeration;

        [EventBasedMethod("OnNotifyCreated_Enumeration")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("AreFlags");
            base.NotifyCreated();
            if (OnNotifyCreated_Enumeration != null) OnNotifyCreated_Enumeration(this);
        }
        public static event ObjectEventHandler<Enumeration> OnNotifyCreated_Enumeration;

        [EventBasedMethod("OnNotifyDeleting_Enumeration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Enumeration != null) OnNotifyDeleting_Enumeration(this);
            EnumerationEntries.Clear();
        }
        public static event ObjectEventHandler<Enumeration> OnNotifyDeleting_Enumeration;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._AreFlags);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._AreFlags = binStream.ReadBoolean();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._AreFlags, xml, "AreFlags", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|AreFlags":
                this._AreFlags = XmlStreamer.ReadBoolean(xml);
                break;
            }
        }

        #endregion

    }
}