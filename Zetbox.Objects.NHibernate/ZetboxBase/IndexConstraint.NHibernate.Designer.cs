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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IndexConstraint")]
    public class IndexConstraintNHibernateImpl : Zetbox.App.Base.InstanceConstraintNHibernateImpl, IndexConstraint
    {
        private static readonly Guid _objectClassID = new Guid("1d5a58e9-fba6-4ef8-b3b7-9966a4dcba83");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public IndexConstraintNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public IndexConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new IndexConstraintProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public IndexConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx, IndexConstraintProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
            _isIsUniqueSet = Proxy.ID > 0;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly IndexConstraintProxy Proxy;

        /// <summary>
        /// Index is created as a Unique Index
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool IsUnique
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchIsUniqueOrDefault();
                if (OnIsUnique_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnIsUnique_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isIsUniqueSet = true;
                if (Proxy.IsUnique != value)
                {
                    var __oldValue = Proxy.IsUnique;
                    var __newValue = value;
                    if (OnIsUnique_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsUnique_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsUnique", __oldValue, __newValue);
                    Proxy.IsUnique = __newValue;
                    NotifyPropertyChanged("IsUnique", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnIsUnique_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsUnique_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("IsUnique");
                }
            }
        }


        private bool FetchIsUniqueOrDefault()
        {
            var __result = Proxy.IsUnique;
                if (!_isIsUniqueSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("2cc6e028-e01f-4879-bda8-78d459c0eaf4"));
                    if (__p != null) {
                        _isIsUniqueSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this.Proxy.IsUnique = (bool)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Zetbox.App.Base.IndexConstraint.IsUnique'");
                    }
                }
            return __result;
        }

        private bool _isIsUniqueSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.IndexConstraint, bool> OnIsUnique_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.IndexConstraint, bool> OnIsUnique_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.IndexConstraint, bool> OnIsUnique_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.IndexConstraint> OnIsUnique_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.Base.Property> Properties
		{
			get
			{
				if (_Properties == null)
				{
					_Properties 
						= new NHibernateBSideCollectionWrapper<Zetbox.App.Base.IndexConstraint, Zetbox.App.Base.Property, Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl.UniqueContraints_ensures_unique_on_Properties_RelationEntryProxy, Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl>(
                                () => this.Proxy.Properties,
                                p => (Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl.UniqueContraints_ensures_unique_on_Properties_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy));
                    _Properties.CollectionChanged += (s, e) => { this.NotifyPropertyChanged("Properties", null, null); if(OnProperties_PostSetter != null && IsAttached) OnProperties_PostSetter(this); };
                    if (Properties_was_eagerLoaded) { Properties_was_eagerLoaded = false; }
				}
				return (ICollection<Zetbox.App.Base.Property>)_Properties;
			}
		}

        public async System.Threading.Tasks.Task<ICollection<Zetbox.App.Base.Property>> GetProp_Properties()
        {
            await TriggerFetchPropertiesAsync();
            return _Properties;
        }

		private NHibernateBSideCollectionWrapper<Zetbox.App.Base.IndexConstraint, Zetbox.App.Base.Property, Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl> _Properties;
		// ignored, but required for Serialization
        private bool Properties_was_eagerLoaded = false;

        public System.Threading.Tasks.Task TriggerFetchPropertiesAsync()
        {
            return System.Threading.Tasks.Task.FromResult<ICollection<Zetbox.App.Base.Property>>(this.Properties);
        }

public static event PropertyListChangedHandler<Zetbox.App.Base.IndexConstraint> OnProperties_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.IndexConstraint> OnProperties_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_IndexConstraint")]
        public override async System.Threading.Tasks.Task<string> GetErrorText(Zetbox.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IndexConstraint != null)
            {
                await OnGetErrorText_IndexConstraint(this, e, constrainedObject);
            }
            else
            {
                e.Result = await base.GetErrorText(constrainedObject);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<IndexConstraint> OnGetErrorText_IndexConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IndexConstraint> OnGetErrorText_IndexConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_IndexConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_IndexConstraint_CanExec != null)
				{
					OnGetErrorText_IndexConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IndexConstraint> OnGetErrorText_IndexConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_IndexConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_IndexConstraint_CanExecReason != null)
				{
					OnGetErrorText_IndexConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnIsValid_IndexConstraint")]
        public override async System.Threading.Tasks.Task<bool> IsValid(Zetbox.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IndexConstraint != null)
            {
                await OnIsValid_IndexConstraint(this, e, constrainedObject);
            }
            else
            {
                e.Result = await base.IsValid(constrainedObject);
            }
            return e.Result;
        }
        public static event IsValid_Handler<IndexConstraint> OnIsValid_IndexConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IndexConstraint> OnIsValid_IndexConstraint_CanExec;

        [EventBasedMethod("OnIsValid_IndexConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_IndexConstraint_CanExec != null)
				{
					OnIsValid_IndexConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IndexConstraint> OnIsValid_IndexConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_IndexConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_IndexConstraint_CanExecReason != null)
				{
					OnIsValid_IndexConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(IndexConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (IndexConstraint)obj;
            var otherImpl = (IndexConstraintNHibernateImpl)obj;
            var me = (IndexConstraint)this;

            me.IsUnique = other.IsUnique;
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
                case "IsUnique":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Properties":
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
            case "Properties":
                return TriggerFetchPropertiesAsync();
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
                    new PropertyDescriptorNHibernateImpl<IndexConstraint, bool>(
                        lazyCtx,
                        new Guid("2cc6e028-e01f-4879-bda8-78d459c0eaf4"),
                        "IsUnique",
                        null,
                        obj => obj.IsUnique,
                        (obj, val) => obj.IsUnique = val,
						obj => OnIsUnique_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<IndexConstraint, ICollection<Zetbox.App.Base.Property>>(
                        lazyCtx,
                        new Guid("3e4bfd37-1037-472b-a5d7-2c20a777e6fd"),
                        "Properties",
                        null,
                        obj => obj.Properties,
                        null, // lists are read-only properties
                        obj => OnProperties_IsValid), 
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
        [EventBasedMethod("OnToString_IndexConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IndexConstraint != null)
            {
                OnToString_IndexConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<IndexConstraint> OnToString_IndexConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_IndexConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_IndexConstraint != null)
            {
                OnObjectIsValid_IndexConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<IndexConstraint> OnObjectIsValid_IndexConstraint;

        [EventBasedMethod("OnNotifyPreSave_IndexConstraint")]
        public override void NotifyPreSave()
        {
            FetchIsUniqueOrDefault();
            base.NotifyPreSave();
            if (OnNotifyPreSave_IndexConstraint != null) OnNotifyPreSave_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyPreSave_IndexConstraint;

        [EventBasedMethod("OnNotifyPostSave_IndexConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_IndexConstraint != null) OnNotifyPostSave_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyPostSave_IndexConstraint;

        [EventBasedMethod("OnNotifyCreated_IndexConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_IndexConstraint != null) OnNotifyCreated_IndexConstraint(this);
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyCreated_IndexConstraint;

        [EventBasedMethod("OnNotifyDeleting_IndexConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_IndexConstraint != null) OnNotifyDeleting_IndexConstraint(this);


            Properties.Clear();
        }
        public static event ObjectEventHandler<IndexConstraint> OnNotifyDeleting_IndexConstraint;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class IndexConstraintProxy
            : Zetbox.App.Base.InstanceConstraintNHibernateImpl.InstanceConstraintProxy
        {
            public IndexConstraintProxy()
            {
                Properties = new Collection<Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl.UniqueContraints_ensures_unique_on_Properties_RelationEntryProxy>();
            }

            public override Type ZetboxWrapper { get { return typeof(IndexConstraintNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(IndexConstraintProxy); } }

            public virtual bool IsUnique { get; set; }

            public virtual ICollection<Zetbox.App.Base.UniqueContraints_ensures_unique_on_Properties_RelationEntryNHibernateImpl.UniqueContraints_ensures_unique_on_Properties_RelationEntryProxy> Properties { get; set; }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._isIsUniqueSet);
            if (this._isIsUniqueSet) {
                binStream.Write(this.Proxy.IsUnique);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._isIsUniqueSet = binStream.ReadBoolean();
            if (this._isIsUniqueSet) {
                this.Proxy.IsUnique = binStream.ReadBoolean();
            }
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this.Proxy.IsUnique, xml, "IsUnique", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|IsUnique":
                // Import must have default value set
                this.Proxy.IsUnique = XmlStreamer.ReadBoolean(xml);
                this._isIsUniqueSet = true;
                break;
            }
        }

        #endregion

    }
}