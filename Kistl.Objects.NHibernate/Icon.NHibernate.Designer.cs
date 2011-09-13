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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Icon")]
    public class IconNHibernateImpl : Kistl.DalProvider.NHibernate.DataObjectNHibernateImpl, Icon, Kistl.API.IExportableInternal
    {
        public IconNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public IconNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new IconProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public IconNHibernateImpl(Func<IFrozenContext> lazyCtx, IconProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
            _isExportGuidSet = Proxy.ID > 0;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly IconProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Blob
        // fkBackingName=this.Proxy.Blob; fkGuidBackingName=_fk_guid_Blob;
        // referencedInterface=Kistl.App.Base.Blob; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Blob Blob
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return null;
                Kistl.App.Base.BlobNHibernateImpl __value = (Kistl.App.Base.BlobNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Blob);

                if (OnBlob_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Blob>(__value);
                    OnBlob_Getter(this, e);
                    __value = (Kistl.App.Base.BlobNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Blob == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.BlobNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Blob);
                var __newValue = (Kistl.App.Base.BlobNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Blob", __oldValue, __newValue);

                if (OnBlob_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Blob>(__oldValue, __newValue);
                    OnBlob_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.BlobNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Blob = null;
                }
                else
                {
                    this.Proxy.Blob = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Blob", __oldValue, __newValue);

                if (OnBlob_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Blob>(__oldValue, __newValue);
                    OnBlob_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Blob's id, used on dehydration only</summary>
        private int? _fk_Blob = null;

        /// <summary>Backing store for Blob's guid, used on import only</summary>
        private Guid? _fk_guid_Blob = null;

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Blob
		public static event PropertyGetterHandler<Kistl.App.GUI.Icon, Kistl.App.Base.Blob> OnBlob_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Icon, Kistl.App.Base.Blob> OnBlob_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Icon, Kistl.App.Base.Blob> OnBlob_PostSetter;

        /// <summary>
        /// Export Guid
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid ExportGuid
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(Guid);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchExportGuidOrDefault();
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isExportGuidSet = true;
                if (Proxy.ExportGuid != value)
                {
                    var __oldValue = Proxy.ExportGuid;
                    var __newValue = value;
                    if (OnExportGuid_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    Proxy.ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if (OnExportGuid_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
            }
        }

        private Guid FetchExportGuidOrDefault()
        {
            var __result = Proxy.ExportGuid;
                if (!_isExportGuidSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("6ce563d7-28e8-4806-bdd1-84c220a6c3ca"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this.Proxy.ExportGuid = (Guid)__tmp_value;
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Kistl.App.GUI.Icon.ExportGuid'");
                    }
                }
            return __result;
        }

        private bool _isExportGuidSet = false;
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Icon, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Icon, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Icon, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// Filename of the Icon
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string IconFile
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IconFile;
                if (OnIconFile_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnIconFile_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IconFile != value)
                {
                    var __oldValue = Proxy.IconFile;
                    var __newValue = value;
                    if (OnIconFile_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnIconFile_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IconFile", __oldValue, __newValue);
                    Proxy.IconFile = __newValue;
                    NotifyPropertyChanged("IconFile", __oldValue, __newValue);
                    if (OnIconFile_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnIconFile_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Icon, string> OnIconFile_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Icon, string> OnIconFile_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Icon, string> OnIconFile_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Module
        // fkBackingName=this.Proxy.Module; fkGuidBackingName=_fk_guid_Module;
        // referencedInterface=Kistl.App.Base.Module; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return null;
                Kistl.App.Base.ModuleNHibernateImpl __value = (Kistl.App.Base.ModuleNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Module);

                if (OnModule_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Module>(__value);
                    OnModule_Getter(this, e);
                    __value = (Kistl.App.Base.ModuleNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Module == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.ModuleNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Module);
                var __newValue = (Kistl.App.Base.ModuleNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Module", __oldValue, __newValue);

                if (OnModule_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Module>(__oldValue, __newValue);
                    OnModule_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.ModuleNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Module = null;
                }
                else
                {
                    this.Proxy.Module = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Module", __oldValue, __newValue);

                if (OnModule_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Module>(__oldValue, __newValue);
                    OnModule_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Module's id, used on dehydration only</summary>
        private int? _fk_Module = null;

        /// <summary>Backing store for Module's guid, used on import only</summary>
        private Guid? _fk_guid_Module = null;

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Module
		public static event PropertyGetterHandler<Kistl.App.GUI.Icon, Kistl.App.Base.Module> OnModule_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Icon, Kistl.App.Base.Module> OnModule_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Icon, Kistl.App.Base.Module> OnModule_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnOpen_Icon")]
        public virtual void Open()
        {
            // base.Open();
            if (OnOpen_Icon != null)
            {
                OnOpen_Icon(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method Icon.Open");
            }
        }
        public delegate void Open_Handler<T>(T obj);
        public static event Open_Handler<Icon> OnOpen_Icon;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnUpload_Icon")]
        public virtual void Upload()
        {
            // base.Upload();
            if (OnUpload_Icon != null)
            {
                OnUpload_Icon(this);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method Icon.Upload");
            }
        }
        public delegate void Upload_Handler<T>(T obj);
        public static event Upload_Handler<Icon> OnUpload_Icon;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(Icon);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Icon)obj;
            var otherImpl = (IconNHibernateImpl)obj;
            var me = (Icon)this;

            me.ExportGuid = other.ExportGuid;
            me.IconFile = other.IconFile;
            this._fk_Blob = otherImpl._fk_Blob;
            this._fk_Module = otherImpl._fk_Module;
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
                case "Blob":
                    {
                        var __oldValue = (Kistl.App.Base.BlobNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Blob);
                        var __newValue = (Kistl.App.Base.BlobNHibernateImpl)(id == null ? null : OurContext.Find<Kistl.App.Base.Blob>(id.Value));
                        NotifyPropertyChanging("Blob", __oldValue, __newValue);
                        this.Proxy.Blob = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Blob", __oldValue, __newValue);
                    }
                    break;
                case "Module":
                    {
                        var __oldValue = (Kistl.App.Base.ModuleNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Module);
                        var __newValue = (Kistl.App.Base.ModuleNHibernateImpl)(id == null ? null : OurContext.Find<Kistl.App.Base.Module>(id.Value));
                        NotifyPropertyChanging("Module", __oldValue, __newValue);
                        this.Proxy.Module = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Module", __oldValue, __newValue);
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

            if (_fk_guid_Blob.HasValue)
                this.Proxy.Blob = ((Kistl.App.Base.BlobNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.Blob>(_fk_guid_Blob.Value)).Proxy;
            else
            if (_fk_Blob.HasValue)
                this.Proxy.Blob = ((Kistl.App.Base.BlobNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.Blob>(_fk_Blob.Value)).Proxy;
            else
                this.Proxy.Blob = null;

            if (_fk_guid_Module.HasValue)
                this.Proxy.Module = ((Kistl.App.Base.ModuleNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.Module>(_fk_guid_Module.Value)).Proxy;
            else
            if (_fk_Module.HasValue)
                this.Proxy.Module = ((Kistl.App.Base.ModuleNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.Module>(_fk_Module.Value)).Proxy;
            else
                this.Proxy.Module = null;
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
                    new PropertyDescriptorNHibernateImpl<IconNHibernateImpl, Kistl.App.Base.Blob>(
                        lazyCtx,
                        new Guid("f4dfb868-260d-450b-84b8-833dac4d25ee"),
                        "Blob",
                        null,
                        obj => obj.Blob,
                        (obj, val) => obj.Blob = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<IconNHibernateImpl, Guid>(
                        lazyCtx,
                        new Guid("6ce563d7-28e8-4806-bdd1-84c220a6c3ca"),
                        "ExportGuid",
                        null,
                        obj => obj.ExportGuid,
                        (obj, val) => obj.ExportGuid = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<IconNHibernateImpl, string>(
                        lazyCtx,
                        new Guid("cdbdfc01-5faa-416b-960f-2eb220f268fe"),
                        "IconFile",
                        null,
                        obj => obj.IconFile,
                        (obj, val) => obj.IconFile = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<IconNHibernateImpl, Kistl.App.Base.Module>(
                        lazyCtx,
                        new Guid("052273ac-706a-446b-bb86-83c726ee66d6"),
                        "Module",
                        null,
                        obj => obj.Module,
                        (obj, val) => obj.Module = val),
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
        [EventBasedMethod("OnToString_Icon")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Icon != null)
            {
                OnToString_Icon(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Icon> OnToString_Icon;

        [EventBasedMethod("OnPreSave_Icon")]
        public override void NotifyPreSave()
        {
            FetchExportGuidOrDefault();
            base.NotifyPreSave();
            if (OnPreSave_Icon != null) OnPreSave_Icon(this);
        }
        public static event ObjectEventHandler<Icon> OnPreSave_Icon;

        [EventBasedMethod("OnPostSave_Icon")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Icon != null) OnPostSave_Icon(this);
        }
        public static event ObjectEventHandler<Icon> OnPostSave_Icon;

        [EventBasedMethod("OnCreated_Icon")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Icon != null) OnCreated_Icon(this);
        }
        public static event ObjectEventHandler<Icon> OnCreated_Icon;

        [EventBasedMethod("OnDeleting_Icon")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Icon != null) OnDeleting_Icon(this);
        }
        public static event ObjectEventHandler<Icon> OnDeleting_Icon;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            // Follow Icon_has_Blob
            if (this.Blob != null && this.Blob.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.Blob);

            // Follow Icon_has_Module
            if (this.Module != null && this.Module.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.Module);

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            // Follow BoolProperty_has_FalseIcon
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Base.BoolProperty>()
                .Where(child => child.FalseIcon == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            // Follow BoolProperty_has_NullIcon
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Base.BoolProperty>()
                .Where(child => child.NullIcon == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            // Follow BoolProperty_has_TrueIcon
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Base.BoolProperty>()
                .Where(child => child.TrueIcon == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            // Follow DataType_has_DefaultIcon
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Base.DataType>()
                .Where(child => child.DefaultIcon == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            // Follow Method_has_Icon
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Base.Method>()
                .Where(child => child.Icon == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            return result;
        }


        public class IconProxy
            : IProxyObject, ISortKey<int>
        {
            public IconProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZBoxWrapper { get { return typeof(IconNHibernateImpl); } }
            public virtual Type ZBoxProxy { get { return typeof(IconProxy); } }

            public virtual Kistl.App.Base.BlobNHibernateImpl.BlobProxy Blob { get; set; }

            public virtual Guid ExportGuid { get; set; }

            public virtual string IconFile { get; set; }

            public virtual Kistl.App.Base.ModuleNHibernateImpl.ModuleProxy Module { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this.Proxy.Blob != null ? this.Proxy.Blob.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this.Proxy.ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this.Proxy.IconFile, binStream);
            BinarySerializer.ToStream(this.Proxy.Module != null ? this.Proxy.Module.ID : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_Blob, binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                Guid tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.ExportGuid = tmp;
            }
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.IconFile = tmp;
            }
            BinarySerializer.FromStream(out this._fk_Module, binStream);
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
            XmlStreamer.ToStream(this.Proxy.Blob != null ? this.Proxy.Blob.ID : (int?)null, xml, "Blob", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this.Proxy.ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this.Proxy.IconFile, xml, "IconFile", "Kistl.App.GUI");
            XmlStreamer.ToStream(this.Proxy.Module != null ? this.Proxy.Module.ID : (int?)null, xml, "Module", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_Blob, xml, "Blob", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                // yuck
                Guid tmp = this.Proxy.ExportGuid;
                XmlStreamer.FromStream(ref tmp, xml, "ExportGuid", "Kistl.App.Base");
                this.Proxy.ExportGuid = tmp;
            }
            {
                // yuck
                string tmp = this.Proxy.IconFile;
                XmlStreamer.FromStream(ref tmp, xml, "IconFile", "Kistl.App.GUI");
                this.Proxy.IconFile = tmp;
            }
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "Kistl.App.GUI");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            xml.WriteAttributeString("ExportGuid", this.Proxy.ExportGuid.ToString());
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this.Proxy.Blob != null ? this.Proxy.Blob.ExportGuid : (Guid?)null, xml, "Blob", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this.Proxy.IconFile, xml, "IconFile", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this.Proxy.Module != null ? this.Proxy.Module.ExportGuid : (Guid?)null, xml, "Module", "Kistl.App.GUI");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.FromStream(ref this._fk_guid_Blob, xml, "Blob", "Kistl.App.GUI");
            // Import must have default value set
            {
                // yuck
                Guid tmp = this.Proxy.ExportGuid;
                XmlStreamer.FromStream(ref tmp, xml, "ExportGuid", "Kistl.App.Base");
                this.Proxy.ExportGuid = tmp;
                this._isExportGuidSet = true;
            }
            {
                // yuck
                string tmp = this.Proxy.IconFile;
                XmlStreamer.FromStream(ref tmp, xml, "IconFile", "Kistl.App.GUI");
                this.Proxy.IconFile = tmp;
            }
            XmlStreamer.FromStream(ref this._fk_guid_Module, xml, "Module", "Kistl.App.GUI");
        }

        #endregion

    }
}