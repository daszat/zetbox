// <autogenerated/>

namespace Kistl.App.Calendar
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
    /// This rule applies every year, n days relative to easter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EasterCalendarRule")]
    public class EasterCalendarRuleNHibernateImpl : Kistl.App.Calendar.YearlyCalendarRuleNHibernateImpl, EasterCalendarRule
    {
        public EasterCalendarRuleNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public EasterCalendarRuleNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new EasterCalendarRuleProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public EasterCalendarRuleNHibernateImpl(Func<IFrozenContext> lazyCtx, EasterCalendarRuleProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly EasterCalendarRuleProxy Proxy;

        /// <summary>
        /// Offset to eater. Null or zero, if easter is meant. Negative numbers are before easter.
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int? Offset
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int?);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Offset;
                if (OnOffset_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnOffset_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Offset != value)
                {
                    var __oldValue = Proxy.Offset;
                    var __newValue = value;
                    if (OnOffset_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOffset_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Offset", __oldValue, __newValue);
                    Proxy.Offset = __newValue;
                    NotifyPropertyChanged("Offset", __oldValue, __newValue);
                    if (OnOffset_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOffset_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Calendar.EasterCalendarRule, int?> OnOffset_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Calendar.EasterCalendarRule, int?> OnOffset_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Calendar.EasterCalendarRule, int?> OnOffset_PostSetter;

        /// <summary>
        /// Checks if the Rule applies to the given date
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnAppliesTo_EasterCalendarRule")]
        public override bool AppliesTo(DateTime date)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnAppliesTo_EasterCalendarRule != null)
            {
                OnAppliesTo_EasterCalendarRule(this, e, date);
            }
            else
            {
                e.Result = base.AppliesTo(date);
            }
            return e.Result;
        }
        public static event AppliesTo_Handler<EasterCalendarRule> OnAppliesTo_EasterCalendarRule;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(EasterCalendarRule);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (EasterCalendarRule)obj;
            var otherImpl = (EasterCalendarRuleNHibernateImpl)obj;
            var me = (EasterCalendarRule)this;

            me.Offset = other.Offset;
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
                    new PropertyDescriptorNHibernateImpl<EasterCalendarRuleNHibernateImpl, int?>(
                        lazyCtx,
                        new Guid("0fdcab86-001e-429d-af31-a5d0df5e6c75"),
                        "Offset",
                        null,
                        obj => obj.Offset,
                        (obj, val) => obj.Offset = val),
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
        [EventBasedMethod("OnToString_EasterCalendarRule")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EasterCalendarRule != null)
            {
                OnToString_EasterCalendarRule(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<EasterCalendarRule> OnToString_EasterCalendarRule;

        [EventBasedMethod("OnPreSave_EasterCalendarRule")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EasterCalendarRule != null) OnPreSave_EasterCalendarRule(this);
        }
        public static event ObjectEventHandler<EasterCalendarRule> OnPreSave_EasterCalendarRule;

        [EventBasedMethod("OnPostSave_EasterCalendarRule")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EasterCalendarRule != null) OnPostSave_EasterCalendarRule(this);
        }
        public static event ObjectEventHandler<EasterCalendarRule> OnPostSave_EasterCalendarRule;

        [EventBasedMethod("OnCreated_EasterCalendarRule")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_EasterCalendarRule != null) OnCreated_EasterCalendarRule(this);
        }
        public static event ObjectEventHandler<EasterCalendarRule> OnCreated_EasterCalendarRule;

        [EventBasedMethod("OnDeleting_EasterCalendarRule")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_EasterCalendarRule != null) OnDeleting_EasterCalendarRule(this);
        }
        public static event ObjectEventHandler<EasterCalendarRule> OnDeleting_EasterCalendarRule;

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


        public class EasterCalendarRuleProxy
            : Kistl.App.Calendar.YearlyCalendarRuleNHibernateImpl.YearlyCalendarRuleProxy
        {
            public EasterCalendarRuleProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(EasterCalendarRuleNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(EasterCalendarRuleProxy); } }

            public virtual int? Offset { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.Offset, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                int? tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.Offset = tmp;
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
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this.Proxy.Offset, xml, "Offset", "Kistl.App.Calendar");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                int? tmp = this.Proxy.Offset;
                XmlStreamer.FromStream(ref tmp, xml, "Offset", "Kistl.App.Calendar");
                this.Proxy.Offset = tmp;
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
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.Calendar")) XmlStreamer.ToStream(this.Proxy.Offset, xml, "Offset", "Kistl.App.Calendar");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                // yuck
                int? tmp = this.Proxy.Offset;
                XmlStreamer.FromStream(ref tmp, xml, "Offset", "Kistl.App.Calendar");
                this.Proxy.Offset = tmp;
            }
        }

        #endregion

    }
}