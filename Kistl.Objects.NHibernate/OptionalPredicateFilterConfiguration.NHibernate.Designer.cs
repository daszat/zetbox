// <autogenerated/>

namespace Kistl.App.GUI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Utils;
    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.NHibernate;

    /// <summary>
    /// Filter configuration for filtering on an instance with an user selectable, optional and constant predicate
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("OptionalPredicateFilterConfiguration")]
    public class OptionalPredicateFilterConfigurationNHibernateImpl : Kistl.App.GUI.ObjectClassFilterConfigurationNHibernateImpl, OptionalPredicateFilterConfiguration
    {
        public OptionalPredicateFilterConfigurationNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public OptionalPredicateFilterConfigurationNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new OptionalPredicateFilterConfigurationProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public OptionalPredicateFilterConfigurationNHibernateImpl(Func<IFrozenContext> lazyCtx, OptionalPredicateFilterConfigurationProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly OptionalPredicateFilterConfigurationProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Predicate
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Predicate;
                if (OnPredicate_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnPredicate_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Predicate != value)
                {
                    var __oldValue = Proxy.Predicate;
                    var __newValue = value;
                    if (OnPredicate_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnPredicate_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Predicate", __oldValue, __newValue);
                    Proxy.Predicate = __newValue;
                    NotifyPropertyChanged("Predicate", __oldValue, __newValue);
                    if (OnPredicate_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnPredicate_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.OptionalPredicateFilterConfiguration, string> OnPredicate_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.OptionalPredicateFilterConfiguration, string> OnPredicate_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.OptionalPredicateFilterConfiguration, string> OnPredicate_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_OptionalPredicateFilterConfiguration")]
        public override Kistl.API.IFilterModel CreateFilterModel()
        {
            var e = new MethodReturnEventArgs<Kistl.API.IFilterModel>();
            if (OnCreateFilterModel_OptionalPredicateFilterConfiguration != null)
            {
                OnCreateFilterModel_OptionalPredicateFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.CreateFilterModel();
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<OptionalPredicateFilterConfiguration> OnCreateFilterModel_OptionalPredicateFilterConfiguration;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_OptionalPredicateFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_OptionalPredicateFilterConfiguration != null)
            {
                OnGetLabel_OptionalPredicateFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<OptionalPredicateFilterConfiguration> OnGetLabel_OptionalPredicateFilterConfiguration;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(OptionalPredicateFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (OptionalPredicateFilterConfiguration)obj;
            var otherImpl = (OptionalPredicateFilterConfigurationNHibernateImpl)obj;
            var me = (OptionalPredicateFilterConfiguration)this;

            me.Predicate = other.Predicate;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }


        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
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
                    new PropertyDescriptorNHibernateImpl<OptionalPredicateFilterConfigurationNHibernateImpl, string>(
                        lazyCtx,
                        new Guid("bcc5a62f-9401-4b88-9cd9-2b33be6fa81a"),
                        "Predicate",
                        null,
                        obj => obj.Predicate,
                        (obj, val) => obj.Predicate = val),
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_OptionalPredicateFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_OptionalPredicateFilterConfiguration != null)
            {
                OnToString_OptionalPredicateFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<OptionalPredicateFilterConfiguration> OnToString_OptionalPredicateFilterConfiguration;

        [EventBasedMethod("OnPreSave_OptionalPredicateFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_OptionalPredicateFilterConfiguration != null) OnPreSave_OptionalPredicateFilterConfiguration(this);
        }
        public static event ObjectEventHandler<OptionalPredicateFilterConfiguration> OnPreSave_OptionalPredicateFilterConfiguration;

        [EventBasedMethod("OnPostSave_OptionalPredicateFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_OptionalPredicateFilterConfiguration != null) OnPostSave_OptionalPredicateFilterConfiguration(this);
        }
        public static event ObjectEventHandler<OptionalPredicateFilterConfiguration> OnPostSave_OptionalPredicateFilterConfiguration;

        [EventBasedMethod("OnCreated_OptionalPredicateFilterConfiguration")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_OptionalPredicateFilterConfiguration != null) OnCreated_OptionalPredicateFilterConfiguration(this);
        }
        public static event ObjectEventHandler<OptionalPredicateFilterConfiguration> OnCreated_OptionalPredicateFilterConfiguration;

        [EventBasedMethod("OnDeleting_OptionalPredicateFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_OptionalPredicateFilterConfiguration != null) OnDeleting_OptionalPredicateFilterConfiguration(this);
        }
        public static event ObjectEventHandler<OptionalPredicateFilterConfiguration> OnDeleting_OptionalPredicateFilterConfiguration;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            return result;
        }


        public class OptionalPredicateFilterConfigurationProxy
            : Kistl.App.GUI.ObjectClassFilterConfigurationNHibernateImpl.ObjectClassFilterConfigurationProxy
        {
            public OptionalPredicateFilterConfigurationProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(OptionalPredicateFilterConfigurationNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(OptionalPredicateFilterConfigurationProxy); } }

            public virtual string Predicate { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this.Proxy.Predicate, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.Predicate = tmp;
            }
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.ToStream(this.Proxy.Predicate, xml, "Predicate", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                string tmp = this.Proxy.Predicate;
                XmlStreamer.FromStream(ref tmp, xml, "Predicate", "Kistl.App.GUI");
                this.Proxy.Predicate = tmp;
            }
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this.Proxy.Predicate, xml, "Predicate", "Kistl.App.GUI");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            {
                // yuck
                string tmp = this.Proxy.Predicate;
                XmlStreamer.FromStream(ref tmp, xml, "Predicate", "Kistl.App.GUI");
                this.Proxy.Predicate = tmp;
            }
        }

        #endregion

    }
}