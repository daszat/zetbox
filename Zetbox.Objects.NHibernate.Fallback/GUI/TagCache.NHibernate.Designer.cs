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

    using Zetbox.API.Utils;
    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.NHibernate;

    /// <summary>
    /// Cache entity for property tags
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TagCache")]
    public class TagCacheNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, TagCache
    {
        private static readonly Guid _objectClassID = new Guid("891c1e32-7545-49d5-9c14-da0e7e061e8f");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public TagCacheNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public TagCacheNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new TagCacheProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public TagCacheNHibernateImpl(Func<IFrozenContext> lazyCtx, TagCacheProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly TagCacheProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Name
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Name != value)
                {
                    var __oldValue = Proxy.Name;
                    var __newValue = value;
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    Proxy.Name = __newValue;
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

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.GUI.TagCache, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.TagCache, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.TagCache, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.TagCache> OnName_IsValid;

        /// <summary>
        /// Rebuilds the tag cache
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnRebuild_TagCache")]
        public virtual void Rebuild()
        {
            // base.Rebuild();
            if (OnRebuild_TagCache != null)
            {
                OnRebuild_TagCache(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method TagCache.Rebuild");
            }
        }
        public delegate void Rebuild_Handler<T>(T obj);
        public static event Rebuild_Handler<TagCache> OnRebuild_TagCache;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<TagCache> OnRebuild_TagCache_CanExec;

        [EventBasedMethod("OnRebuild_TagCache_CanExec")]
        public virtual bool RebuildCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnRebuild_TagCache_CanExec != null)
				{
					OnRebuild_TagCache_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<TagCache> OnRebuild_TagCache_CanExecReason;

        [EventBasedMethod("OnRebuild_TagCache_CanExecReason")]
        public virtual string RebuildCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnRebuild_TagCache_CanExecReason != null)
				{
					OnRebuild_TagCache_CanExecReason(this, e);
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
            return typeof(TagCache);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TagCache)obj;
            var otherImpl = (TagCacheNHibernateImpl)obj;
            var me = (TagCache)this;

            me.Name = other.Name;
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
                case "Name":
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
                    new PropertyDescriptorNHibernateImpl<TagCache, string>(
                        lazyCtx,
                        new Guid("3fe05228-66d6-42dd-8a0f-526ba4ac4503"),
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
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_TagCache")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TagCache != null)
            {
                OnToString_TagCache(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TagCache> OnToString_TagCache;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_TagCache")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_TagCache != null)
            {
                OnObjectIsValid_TagCache(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<TagCache> OnObjectIsValid_TagCache;

        [EventBasedMethod("OnNotifyPreSave_TagCache")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_TagCache != null) OnNotifyPreSave_TagCache(this);
        }
        public static event ObjectEventHandler<TagCache> OnNotifyPreSave_TagCache;

        [EventBasedMethod("OnNotifyPostSave_TagCache")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_TagCache != null) OnNotifyPostSave_TagCache(this);
        }
        public static event ObjectEventHandler<TagCache> OnNotifyPostSave_TagCache;

        [EventBasedMethod("OnNotifyCreated_TagCache")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_TagCache != null) OnNotifyCreated_TagCache(this);
        }
        public static event ObjectEventHandler<TagCache> OnNotifyCreated_TagCache;

        [EventBasedMethod("OnNotifyDeleting_TagCache")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_TagCache != null) OnNotifyDeleting_TagCache(this);


        }
        public static event ObjectEventHandler<TagCache> OnNotifyDeleting_TagCache;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class TagCacheProxy
            : IProxyObject, ISortKey<int>
        {
            public TagCacheProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(TagCacheNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(TagCacheProxy); } }

            public virtual string Name { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this.Proxy.Name = binStream.ReadString();
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