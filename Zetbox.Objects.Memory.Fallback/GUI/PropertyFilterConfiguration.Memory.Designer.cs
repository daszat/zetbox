// <autogenerated/>

namespace Zetbox.App.GUI
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
    /// Abstract base class for Property Filter
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PropertyFilterConfiguration")]
    public abstract class PropertyFilterConfigurationMemoryImpl : Zetbox.App.GUI.FilterConfigurationMemoryImpl, PropertyFilterConfiguration
    {
        private static readonly Guid _objectClassID = new Guid("b7099b5a-295a-4de0-9aa0-c5577e39adcb");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public PropertyFilterConfigurationMemoryImpl()
            : base(null)
        {
        }

        public PropertyFilterConfigurationMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
        // fkBackingName=_fk_Property; fkGuidBackingName=_fk_guid_Property;
        // referencedInterface=Zetbox.App.Base.Property; moduleNamespace=Zetbox.App.GUI;
        // inverse Navigator=FilterConfiguration; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.Property Property
        {
            get { return PropertyImpl; }
            set { PropertyImpl = (Zetbox.App.Base.PropertyMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<Zetbox.App.Base.Property> GetProp_Property()
        {
            return TriggerFetchPropertyAsync();
        }

        public async System.Threading.Tasks.Task SetProp_Property(Zetbox.App.Base.Property newValue)
        {
            await TriggerFetchPropertyAsync();
            PropertyImpl = (Zetbox.App.Base.PropertyMemoryImpl)newValue;
        }

        private int? __fk_PropertyCache;

        private int? _fk_Property {
            get
            {
                return __fk_PropertyCache;
            }
            set
            {
                __fk_PropertyCache = value;
                // Recreate task to clear it's cache
                _triggerFetchPropertyTask = null;
            }
        }

        /// <summary>ForeignKey Property for Property's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Property
		{
			get { return _fk_Property; }
			set { _fk_Property = value; }
		}

        private Guid? _fk_guid_Property = null;

        System.Threading.Tasks.Task<Zetbox.App.Base.Property> _triggerFetchPropertyTask;
        public System.Threading.Tasks.Task<Zetbox.App.Base.Property> TriggerFetchPropertyAsync()
        {
            if (_triggerFetchPropertyTask != null) return _triggerFetchPropertyTask;

            if (_fk_Property.HasValue)
                _triggerFetchPropertyTask = Context.FindAsync<Zetbox.App.Base.Property>(_fk_Property.Value);
            else
                _triggerFetchPropertyTask = new System.Threading.Tasks.Task<Zetbox.App.Base.Property>(() => null);

            _triggerFetchPropertyTask.OnResult(t =>
            {
                if (OnProperty_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Property>(t.Result);
                    OnProperty_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchPropertyTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Base.PropertyMemoryImpl PropertyImpl
        {
            get
            {
                var task = TriggerFetchPropertyAsync();
                task.TryRunSynchronously();
                task.Wait();
                return (Zetbox.App.Base.PropertyMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Property == null) || (value != null && value.ID == _fk_Property))
                {
                    SetInitializedProperty("Property");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = PropertyImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Property", __oldValue, __newValue);

                if (OnProperty_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.PropertyMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Property = __newValue == null ? (int?)null : __newValue.ID;

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // unset old reference
                    __oldValue.FilterConfiguration = null;
                }

                if (__newValue != null)
                {
                    // set new reference
                    __newValue.FilterConfiguration = this;
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Property", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnProperty_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
		public static event PropertyGetterHandler<Zetbox.App.GUI.PropertyFilterConfiguration, Zetbox.App.Base.Property> OnProperty_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.PropertyFilterConfiguration, Zetbox.App.Base.Property> OnProperty_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.PropertyFilterConfiguration, Zetbox.App.Base.Property> OnProperty_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.PropertyFilterConfiguration> OnProperty_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_PropertyFilterConfiguration")]
        public override Zetbox.API.IFilterModel CreateFilterModel(Zetbox.API.IZetboxContext ctx)
        {
            var e = new MethodReturnEventArgs<Zetbox.API.IFilterModel>();
            if (OnCreateFilterModel_PropertyFilterConfiguration != null)
            {
                OnCreateFilterModel_PropertyFilterConfiguration(this, e, ctx);
            }
            else
            {
                e.Result = base.CreateFilterModel(ctx);
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<PropertyFilterConfiguration> OnCreateFilterModel_PropertyFilterConfiguration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<PropertyFilterConfiguration> OnCreateFilterModel_PropertyFilterConfiguration_CanExec;

        [EventBasedMethod("OnCreateFilterModel_PropertyFilterConfiguration_CanExec")]
        public override bool CreateFilterModelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnCreateFilterModel_PropertyFilterConfiguration_CanExec != null)
				{
					OnCreateFilterModel_PropertyFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<PropertyFilterConfiguration> OnCreateFilterModel_PropertyFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnCreateFilterModel_PropertyFilterConfiguration_CanExecReason")]
        public override string CreateFilterModelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnCreateFilterModel_PropertyFilterConfiguration_CanExecReason != null)
				{
					OnCreateFilterModel_PropertyFilterConfiguration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_PropertyFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_PropertyFilterConfiguration != null)
            {
                OnGetLabel_PropertyFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<PropertyFilterConfiguration> OnGetLabel_PropertyFilterConfiguration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<PropertyFilterConfiguration> OnGetLabel_PropertyFilterConfiguration_CanExec;

        [EventBasedMethod("OnGetLabel_PropertyFilterConfiguration_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_PropertyFilterConfiguration_CanExec != null)
				{
					OnGetLabel_PropertyFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<PropertyFilterConfiguration> OnGetLabel_PropertyFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnGetLabel_PropertyFilterConfiguration_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_PropertyFilterConfiguration_CanExecReason != null)
				{
					OnGetLabel_PropertyFilterConfiguration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(PropertyFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (PropertyFilterConfiguration)obj;
            var otherImpl = (PropertyFilterConfigurationMemoryImpl)obj;
            var me = (PropertyFilterConfiguration)this;

            this._fk_Property = otherImpl._fk_Property;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Property":
                    {
                        var __oldValue = _fk_Property;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Property", __oldValue, __newValue);
                        _fk_Property = __newValue;
                        NotifyPropertyChanged("Property", __oldValue, __newValue);
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
                case "Property":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Property":
                return TriggerFetchPropertyAsync();
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

            if (_fk_guid_Property.HasValue)
                PropertyImpl = (Zetbox.App.Base.PropertyMemoryImpl)Context.FindPersistenceObject<Zetbox.App.Base.Property>(_fk_guid_Property.Value);
            else
            if (_fk_Property.HasValue)
                PropertyImpl = (Zetbox.App.Base.PropertyMemoryImpl)Context.Find<Zetbox.App.Base.Property>(_fk_Property.Value);
            else
                PropertyImpl = null;
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
                    new PropertyDescriptorMemoryImpl<PropertyFilterConfiguration, Zetbox.App.Base.Property>(
                        lazyCtx,
                        new Guid("384208e7-eb27-41f1-ac12-b05822c0a2ad"),
                        "Property",
                        null,
                        obj => obj.Property,
                        (obj, val) => obj.Property = val,
						obj => OnProperty_IsValid), 
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
        [EventBasedMethod("OnToString_PropertyFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PropertyFilterConfiguration != null)
            {
                OnToString_PropertyFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<PropertyFilterConfiguration> OnToString_PropertyFilterConfiguration;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_PropertyFilterConfiguration")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_PropertyFilterConfiguration != null)
            {
                OnObjectIsValid_PropertyFilterConfiguration(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<PropertyFilterConfiguration> OnObjectIsValid_PropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyPreSave_PropertyFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_PropertyFilterConfiguration != null) OnNotifyPreSave_PropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<PropertyFilterConfiguration> OnNotifyPreSave_PropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyPostSave_PropertyFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_PropertyFilterConfiguration != null) OnNotifyPostSave_PropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<PropertyFilterConfiguration> OnNotifyPostSave_PropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyCreated_PropertyFilterConfiguration")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Property");
            base.NotifyCreated();
            if (OnNotifyCreated_PropertyFilterConfiguration != null) OnNotifyCreated_PropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<PropertyFilterConfiguration> OnNotifyCreated_PropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyDeleting_PropertyFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_PropertyFilterConfiguration != null) OnNotifyDeleting_PropertyFilterConfiguration(this);
            Property = null;
        }
        public static event ObjectEventHandler<PropertyFilterConfiguration> OnNotifyDeleting_PropertyFilterConfiguration;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(Property != null ? Property.ID : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_Property = binStream.ReadNullableInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.GUI")) XmlStreamer.ToStream(Property != null ? Property.ExportGuid : (Guid?)null, xml, "Property", "Zetbox.App.GUI");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.GUI|Property":
                this._fk_guid_Property = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}