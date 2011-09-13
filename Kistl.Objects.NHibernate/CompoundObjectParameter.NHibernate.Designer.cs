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
    /// Metadefinition Object for a CompoundObject Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CompoundObjectParameter")]
    public class CompoundObjectParameterNHibernateImpl : Kistl.App.Base.BaseParameterNHibernateImpl, CompoundObjectParameter
    {
        public CompoundObjectParameterNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public CompoundObjectParameterNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new CompoundObjectParameterProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public CompoundObjectParameterNHibernateImpl(Func<IFrozenContext> lazyCtx, CompoundObjectParameterProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly CompoundObjectParameterProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObject
        // fkBackingName=this.Proxy.CompoundObject; fkGuidBackingName=_fk_guid_CompoundObject;
        // referencedInterface=Kistl.App.Base.CompoundObject; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.CompoundObject CompoundObject
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return null;
                Kistl.App.Base.CompoundObjectNHibernateImpl __value = (Kistl.App.Base.CompoundObjectNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.CompoundObject);

                if (OnCompoundObject_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.CompoundObject>(__value);
                    OnCompoundObject_Getter(this, e);
                    __value = (Kistl.App.Base.CompoundObjectNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.CompoundObject == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.CompoundObjectNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.CompoundObject);
                var __newValue = (Kistl.App.Base.CompoundObjectNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("CompoundObject", __oldValue, __newValue);

                if (OnCompoundObject_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObject_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.CompoundObjectNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.CompoundObject = null;
                }
                else
                {
                    this.Proxy.CompoundObject = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("CompoundObject", __oldValue, __newValue);

                if (OnCompoundObject_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObject_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for CompoundObject's id, used on dehydration only</summary>
        private int? _fk_CompoundObject = null;

        /// <summary>Backing store for CompoundObject's guid, used on import only</summary>
        private Guid? _fk_guid_CompoundObject = null;

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObject
		public static event PropertyGetterHandler<Kistl.App.Base.CompoundObjectParameter, Kistl.App.Base.CompoundObject> OnCompoundObject_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.CompoundObjectParameter, Kistl.App.Base.CompoundObject> OnCompoundObject_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.CompoundObjectParameter, Kistl.App.Base.CompoundObject> OnCompoundObject_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_CompoundObjectParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_CompoundObjectParameter != null)
            {
                OnGetLabel_CompoundObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<CompoundObjectParameter> OnGetLabel_CompoundObjectParameter;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterType_CompoundObjectParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_CompoundObjectParameter != null)
            {
                OnGetParameterType_CompoundObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<CompoundObjectParameter> OnGetParameterType_CompoundObjectParameter;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterTypeString_CompoundObjectParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_CompoundObjectParameter != null)
            {
                OnGetParameterTypeString_CompoundObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<CompoundObjectParameter> OnGetParameterTypeString_CompoundObjectParameter;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObjectParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CompoundObjectParameter)obj;
            var otherImpl = (CompoundObjectParameterNHibernateImpl)obj;
            var me = (CompoundObjectParameter)this;

            this._fk_CompoundObject = otherImpl._fk_CompoundObject;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }

        public override void UpdateParent(string propertyName, int? id)
        {
            switch(propertyName)
            {
                case "CompoundObject":
                    {
                        var __oldValue = (Kistl.App.Base.CompoundObjectNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.CompoundObject);
                        var __newValue = (Kistl.App.Base.CompoundObjectNHibernateImpl)(id == null ? null : OurContext.Find<Kistl.App.Base.CompoundObject>(id.Value));
                        NotifyPropertyChanging("CompoundObject", __oldValue, __newValue);
                        this.Proxy.CompoundObject = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("CompoundObject", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, id);
                    break;
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_CompoundObject.HasValue)
                this.Proxy.CompoundObject = ((Kistl.App.Base.CompoundObjectNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.CompoundObject>(_fk_guid_CompoundObject.Value)).Proxy;
            else
            if (_fk_CompoundObject.HasValue)
                this.Proxy.CompoundObject = ((Kistl.App.Base.CompoundObjectNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.CompoundObject>(_fk_CompoundObject.Value)).Proxy;
            else
                this.Proxy.CompoundObject = null;
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
                    new PropertyDescriptorNHibernateImpl<CompoundObjectParameterNHibernateImpl, Kistl.App.Base.CompoundObject>(
                        lazyCtx,
                        new Guid("43d03fec-b595-46d0-b5d5-cf4c5d21fda7"),
                        "CompoundObject",
                        null,
                        obj => obj.CompoundObject,
                        (obj, val) => obj.CompoundObject = val),
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
        [EventBasedMethod("OnToString_CompoundObjectParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CompoundObjectParameter != null)
            {
                OnToString_CompoundObjectParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CompoundObjectParameter> OnToString_CompoundObjectParameter;

        [EventBasedMethod("OnPreSave_CompoundObjectParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CompoundObjectParameter != null) OnPreSave_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnPreSave_CompoundObjectParameter;

        [EventBasedMethod("OnPostSave_CompoundObjectParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CompoundObjectParameter != null) OnPostSave_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnPostSave_CompoundObjectParameter;

        [EventBasedMethod("OnCreated_CompoundObjectParameter")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_CompoundObjectParameter != null) OnCreated_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnCreated_CompoundObjectParameter;

        [EventBasedMethod("OnDeleting_CompoundObjectParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_CompoundObjectParameter != null) OnDeleting_CompoundObjectParameter(this);
        }
        public static event ObjectEventHandler<CompoundObjectParameter> OnDeleting_CompoundObjectParameter;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            // Follow CPParameter_has_CompoundObject
            if (this.CompoundObject != null && this.CompoundObject.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.CompoundObject);

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            return result;
        }


        public class CompoundObjectParameterProxy
            : Kistl.App.Base.BaseParameterNHibernateImpl.BaseParameterProxy
        {
            public CompoundObjectParameterProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(CompoundObjectParameterNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(CompoundObjectParameterProxy); } }

            public virtual Kistl.App.Base.CompoundObjectNHibernateImpl.CompoundObjectProxy CompoundObject { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this.Proxy.CompoundObject != null ? this.Proxy.CompoundObject.ID : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_CompoundObject, binStream);
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
            XmlStreamer.ToStream(this.Proxy.CompoundObject != null ? this.Proxy.CompoundObject.ID : (int?)null, xml, "CompoundObject", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_CompoundObject, xml, "CompoundObject", "Kistl.App.Base");
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.CompoundObject != null ? this.Proxy.CompoundObject.ExportGuid : (Guid?)null, xml, "CompoundObject", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.FromStream(ref this._fk_guid_CompoundObject, xml, "CompoundObject", "Kistl.App.Base");
        }

        #endregion

    }
}