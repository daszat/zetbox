// <autogenerated/>

namespace Kistl.App.Base
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
    /// Metadefinition Object for ValueType Properties. This class is abstract.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ValueTypeProperty")]
    public abstract class ValueTypePropertyNHibernateImpl : Kistl.App.Base.PropertyNHibernateImpl, ValueTypeProperty
    {
        public ValueTypePropertyNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public ValueTypePropertyNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new ValueTypePropertyProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public ValueTypePropertyNHibernateImpl(Func<IFrozenContext> lazyCtx, ValueTypePropertyProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly ValueTypePropertyProxy Proxy;

        /// <summary>
        /// Whether or not a list-valued property has a index
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool HasPersistentOrder
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.HasPersistentOrder;
                if (OnHasPersistentOrder_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnHasPersistentOrder_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.HasPersistentOrder != value)
                {
                    var __oldValue = Proxy.HasPersistentOrder;
                    var __newValue = value;
                    if (OnHasPersistentOrder_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("HasPersistentOrder", __oldValue, __newValue);
                    Proxy.HasPersistentOrder = __newValue;
                    NotifyPropertyChanged("HasPersistentOrder", __oldValue, __newValue);
                    if (OnHasPersistentOrder_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnHasPersistentOrder_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnHasPersistentOrder_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnHasPersistentOrder_PostSetter;

        /// <summary>
        /// If true, a property getter will be invoked to get the properties value. No Backingstore is generated, thus there is no setter.
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool IsCalculated
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IsCalculated;
                if (OnIsCalculated_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnIsCalculated_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IsCalculated != value)
                {
                    var __oldValue = Proxy.IsCalculated;
                    var __newValue = value;
                    if (OnIsCalculated_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsCalculated_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsCalculated", __oldValue, __newValue);
                    Proxy.IsCalculated = __newValue;
                    NotifyPropertyChanged("IsCalculated", __oldValue, __newValue);
                    if (OnIsCalculated_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsCalculated_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnIsCalculated_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnIsCalculated_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnIsCalculated_PostSetter;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool IsList
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IsList;
                if (OnIsList_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnIsList_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IsList != value)
                {
                    var __oldValue = Proxy.IsList;
                    var __newValue = value;
                    if (OnIsList_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsList", __oldValue, __newValue);
                    Proxy.IsList = __newValue;
                    NotifyPropertyChanged("IsList", __oldValue, __newValue);
                    if (OnIsList_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnIsList_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnIsList_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ValueTypeProperty, bool> OnIsList_PostSetter;

        /// <summary>
        /// The element type for multi-valued properties. The property type string in all other cases.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetElementTypeString_ValueTypeProperty")]
        public override string GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_ValueTypeProperty != null)
            {
                OnGetElementTypeString_ValueTypeProperty(this, e);
            }
            else
            {
                e.Result = base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<ValueTypeProperty> OnGetElementTypeString_ValueTypeProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_ValueTypeProperty")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_ValueTypeProperty != null)
            {
                OnGetLabel_ValueTypeProperty(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<ValueTypeProperty> OnGetLabel_ValueTypeProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_ValueTypeProperty")]
        public override string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_ValueTypeProperty != null)
            {
                OnGetName_ValueTypeProperty(this, e);
            }
            else
            {
                e.Result = base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<ValueTypeProperty> OnGetName_ValueTypeProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyType_ValueTypeProperty")]
        public override System.Type GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_ValueTypeProperty != null)
            {
                OnGetPropertyType_ValueTypeProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<ValueTypeProperty> OnGetPropertyType_ValueTypeProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyTypeString_ValueTypeProperty")]
        public override string GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ValueTypeProperty != null)
            {
                OnGetPropertyTypeString_ValueTypeProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<ValueTypeProperty> OnGetPropertyTypeString_ValueTypeProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(ValueTypeProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ValueTypeProperty)obj;
            var otherImpl = (ValueTypePropertyNHibernateImpl)obj;
            var me = (ValueTypeProperty)this;

            me.HasPersistentOrder = other.HasPersistentOrder;
            me.IsCalculated = other.IsCalculated;
            me.IsList = other.IsList;
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
                    new PropertyDescriptorNHibernateImpl<ValueTypePropertyNHibernateImpl, bool>(
                        lazyCtx,
                        new Guid("b62c7fee-bb67-46a6-b481-81554e788aa0"),
                        "HasPersistentOrder",
                        null,
                        obj => obj.HasPersistentOrder,
                        (obj, val) => obj.HasPersistentOrder = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ValueTypePropertyNHibernateImpl, bool>(
                        lazyCtx,
                        new Guid("2eed845e-887d-4230-8410-0b442ba7724b"),
                        "IsCalculated",
                        null,
                        obj => obj.IsCalculated,
                        (obj, val) => obj.IsCalculated = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ValueTypePropertyNHibernateImpl, bool>(
                        lazyCtx,
                        new Guid("b2bd1528-c22f-4e12-b80f-f8234a2c0831"),
                        "IsList",
                        null,
                        obj => obj.IsList,
                        (obj, val) => obj.IsList = val),
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
        [EventBasedMethod("OnToString_ValueTypeProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ValueTypeProperty != null)
            {
                OnToString_ValueTypeProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ValueTypeProperty> OnToString_ValueTypeProperty;

        [EventBasedMethod("OnPreSave_ValueTypeProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ValueTypeProperty != null) OnPreSave_ValueTypeProperty(this);
        }
        public static event ObjectEventHandler<ValueTypeProperty> OnPreSave_ValueTypeProperty;

        [EventBasedMethod("OnPostSave_ValueTypeProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ValueTypeProperty != null) OnPostSave_ValueTypeProperty(this);
        }
        public static event ObjectEventHandler<ValueTypeProperty> OnPostSave_ValueTypeProperty;

        [EventBasedMethod("OnCreated_ValueTypeProperty")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_ValueTypeProperty != null) OnCreated_ValueTypeProperty(this);
        }
        public static event ObjectEventHandler<ValueTypeProperty> OnCreated_ValueTypeProperty;

        [EventBasedMethod("OnDeleting_ValueTypeProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_ValueTypeProperty != null) OnDeleting_ValueTypeProperty(this);
        }
        public static event ObjectEventHandler<ValueTypeProperty> OnDeleting_ValueTypeProperty;

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


        public class ValueTypePropertyProxy
            : Kistl.App.Base.PropertyNHibernateImpl.PropertyProxy
        {
            public ValueTypePropertyProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(ValueTypePropertyNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(ValueTypePropertyProxy); } }

            public virtual bool HasPersistentOrder { get; set; }

            public virtual bool IsCalculated { get; set; }

            public virtual bool IsList { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.HasPersistentOrder, binStream);
            BinarySerializer.ToStream(this.Proxy.IsCalculated, binStream);
            BinarySerializer.ToStream(this.Proxy.IsList, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                bool tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.HasPersistentOrder = tmp;
            }
            {
                bool tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.IsCalculated = tmp;
            }
            {
                bool tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.IsList = tmp;
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
            XmlStreamer.ToStream(this.Proxy.HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.IsCalculated, xml, "IsCalculated", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.IsList, xml, "IsList", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                bool tmp = this.Proxy.HasPersistentOrder;
                XmlStreamer.FromStream(ref tmp, xml, "HasPersistentOrder", "Kistl.App.Base");
                this.Proxy.HasPersistentOrder = tmp;
            }
            {
                // yuck
                bool tmp = this.Proxy.IsCalculated;
                XmlStreamer.FromStream(ref tmp, xml, "IsCalculated", "Kistl.App.Base");
                this.Proxy.IsCalculated = tmp;
            }
            {
                // yuck
                bool tmp = this.Proxy.IsList;
                XmlStreamer.FromStream(ref tmp, xml, "IsList", "Kistl.App.Base");
                this.Proxy.IsList = tmp;
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.IsCalculated, xml, "IsCalculated", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.IsList, xml, "IsList", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                // yuck
                bool tmp = this.Proxy.HasPersistentOrder;
                XmlStreamer.FromStream(ref tmp, xml, "HasPersistentOrder", "Kistl.App.Base");
                this.Proxy.HasPersistentOrder = tmp;
            }
            {
                // yuck
                bool tmp = this.Proxy.IsCalculated;
                XmlStreamer.FromStream(ref tmp, xml, "IsCalculated", "Kistl.App.Base");
                this.Proxy.IsCalculated = tmp;
            }
            {
                // yuck
                bool tmp = this.Proxy.IsList;
                XmlStreamer.FromStream(ref tmp, xml, "IsList", "Kistl.App.Base");
                this.Proxy.IsList = tmp;
            }
        }

        #endregion

    }
}