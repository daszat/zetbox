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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// Abstract base class for Property Filter
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="PropertyFilterConfiguration")]
    [System.Diagnostics.DebuggerDisplay("PropertyFilterConfiguration")]
    public abstract class PropertyFilterConfigurationEfImpl : Zetbox.App.GUI.FilterConfigurationEfImpl, PropertyFilterConfiguration
    {
        private static readonly Guid _objectClassID = new Guid("b7099b5a-295a-4de0-9aa0-c5577e39adcb");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public PropertyFilterConfigurationEfImpl()
            : base(null)
        {
        }

        public PropertyFilterConfigurationEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Property_Has_PropertyFilterConfiguration
    A: One Property as Property
    B: ZeroOrOne PropertyFilterConfiguration as PropertyFilterConfiguration
    Preferred Storage: MergeIntoB
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
        // fkBackingName=_fk_Property; fkGuidBackingName=_fk_guid_Property;
        // referencedInterface=Zetbox.App.Base.Property; moduleNamespace=Zetbox.App.GUI;
        // inverse Navigator=FilterConfiguration; is reference;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.Property Property
        {
            get { return PropertyImpl; }
            set { PropertyImpl = (Zetbox.App.Base.PropertyEfImpl)value; }
        }

        private int? _fk_Property;

        private Guid? _fk_guid_Property = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Property_Has_PropertyFilterConfiguration", "Property")]
        public Zetbox.App.Base.PropertyEfImpl PropertyImpl
        {
            get
            {
                Zetbox.App.Base.PropertyEfImpl __value;
                EntityReference<Zetbox.App.Base.PropertyEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.PropertyEfImpl>(
                        "Model.FK_Property_Has_PropertyFilterConfiguration",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnProperty_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Property>(__value);
                    OnProperty_Getter(this, e);
                    __value = (Zetbox.App.Base.PropertyEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Base.PropertyEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.PropertyEfImpl>(
                        "Model.FK_Property_Has_PropertyFilterConfiguration",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Base.PropertyEfImpl __oldValue = (Zetbox.App.Base.PropertyEfImpl)r.Value;
                Zetbox.App.Base.PropertyEfImpl __newValue = (Zetbox.App.Base.PropertyEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Property", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("FilterConfiguration", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("FilterConfiguration", null, null);
                }

                if (OnProperty_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.PropertyEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Base.PropertyEfImpl)__newValue;

                if (OnProperty_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Property", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("FilterConfiguration", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("FilterConfiguration", null, null);
                }
                UpdateChangedInfo = true;
            }
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
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
            var otherImpl = (PropertyFilterConfigurationEfImpl)obj;
            var me = (PropertyFilterConfiguration)this;

            this._fk_Property = otherImpl._fk_Property;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

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
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_Property.HasValue)
                PropertyImpl = (Zetbox.App.Base.PropertyEfImpl)Context.FindPersistenceObject<Zetbox.App.Base.Property>(_fk_guid_Property.Value);
            else
            if (_fk_Property.HasValue)
                PropertyImpl = (Zetbox.App.Base.PropertyEfImpl)Context.Find<Zetbox.App.Base.Property>(_fk_Property.Value);
            else
                PropertyImpl = null;
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
                    new PropertyDescriptorEfImpl<PropertyFilterConfiguration, Zetbox.App.Base.Property>(
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
            e.IsValid = b.IsValid;
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
        }
        public static event ObjectEventHandler<PropertyFilterConfiguration> OnNotifyDeleting_PropertyFilterConfiguration;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Zetbox.App.Base.PropertyEfImpl>("Model.FK_Property_Has_PropertyFilterConfiguration", "Property").EntityKey;
                binStream.Write(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_Property);
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