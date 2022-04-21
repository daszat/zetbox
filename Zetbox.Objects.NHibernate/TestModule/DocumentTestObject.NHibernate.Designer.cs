// <autogenerated/>

namespace Zetbox.App.Test
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
    /// DocumentTestObject
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DocumentTestObject")]
    public class DocumentTestObjectNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, DocumentTestObject
    {
        private static readonly Guid _objectClassID = new Guid("58f806e0-77ef-4d7b-ab01-857a6e6432b2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public DocumentTestObjectNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public DocumentTestObjectNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new DocumentTestObjectProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public DocumentTestObjectNHibernateImpl(Func<IFrozenContext> lazyCtx, DocumentTestObjectProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly DocumentTestObjectProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnotherFile
        // fkBackingName=this.Proxy.AnotherFile; fkGuidBackingName=_fk_guid_AnotherFile;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events
        
        public System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> GetProp_AnotherFile()
        {
            return System.Threading.Tasks.Task.FromResult(AnotherFile);
        }

        public async System.Threading.Tasks.Task SetProp_AnotherFile(at.dasz.DocumentManagement.File newValue)
        {
            await TriggerFetchAnotherFileAsync();
            AnotherFile = newValue;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public at.dasz.DocumentManagement.File AnotherFile
        {
            get
            {
                at.dasz.DocumentManagement.FileNHibernateImpl __value = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.AnotherFile);

                if (OnAnotherFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(__value);
                    OnAnotherFile_Getter(this, e);
                    __value = (at.dasz.DocumentManagement.FileNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.AnotherFile == null)
                {
                    SetInitializedProperty("AnotherFile");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.AnotherFile);
                var __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("AnotherFile");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("AnotherFile", __oldValue, __newValue);

                if (OnAnotherFile_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnotherFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.AnotherFile = null;
                }
                else
                {
                    this.Proxy.AnotherFile = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("AnotherFile", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnAnotherFile_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnotherFile_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for AnotherFile's id, used on dehydration only</summary>
        private int? _fk_AnotherFile = null;

        /// <summary>ForeignKey Property for AnotherFile's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_AnotherFile
		{
			get { return AnotherFile != null ? AnotherFile.ID : (int?)null; }
			set { _fk_AnotherFile = value; }
		}


    public System.Threading.Tasks.Task TriggerFetchAnotherFileAsync()
    {
        return System.Threading.Tasks.Task.FromResult<at.dasz.DocumentManagement.File>(this.AnotherFile);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnotherFile
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnAnotherFile_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnyFile
        // fkBackingName=this.Proxy.AnyFile; fkGuidBackingName=_fk_guid_AnyFile;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events
        
        public System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> GetProp_AnyFile()
        {
            return System.Threading.Tasks.Task.FromResult(AnyFile);
        }

        public async System.Threading.Tasks.Task SetProp_AnyFile(at.dasz.DocumentManagement.File newValue)
        {
            await TriggerFetchAnyFileAsync();
            AnyFile = newValue;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public at.dasz.DocumentManagement.File AnyFile
        {
            get
            {
                at.dasz.DocumentManagement.FileNHibernateImpl __value = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.AnyFile);

                if (OnAnyFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(__value);
                    OnAnyFile_Getter(this, e);
                    __value = (at.dasz.DocumentManagement.FileNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.AnyFile == null)
                {
                    SetInitializedProperty("AnyFile");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.AnyFile);
                var __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("AnyFile");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("AnyFile", __oldValue, __newValue);

                if (OnAnyFile_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnyFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.AnyFile = null;
                }
                else
                {
                    this.Proxy.AnyFile = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("AnyFile", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnAnyFile_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnyFile_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for AnyFile's id, used on dehydration only</summary>
        private int? _fk_AnyFile = null;

        /// <summary>ForeignKey Property for AnyFile's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_AnyFile
		{
			get { return AnyFile != null ? AnyFile.ID : (int?)null; }
			set { _fk_AnyFile = value; }
		}


    public System.Threading.Tasks.Task TriggerFetchAnyFileAsync()
    {
        return System.Threading.Tasks.Task.FromResult<at.dasz.DocumentManagement.File>(this.AnyFile);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnyFile
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnAnyFile_IsValid;

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
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(DocumentTestObject);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DocumentTestObject)obj;
            var otherImpl = (DocumentTestObjectNHibernateImpl)obj;
            var me = (DocumentTestObject)this;

            me.Name = other.Name;
            this._fk_AnotherFile = otherImpl._fk_AnotherFile;
            this._fk_AnyFile = otherImpl._fk_AnyFile;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "AnotherFile":
                    {
                        var __oldValue = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.AnotherFile);
                        var __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)parentObj;
                        NotifyPropertyChanging("AnotherFile", __oldValue, __newValue);
                        this.Proxy.AnotherFile = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("AnotherFile", __oldValue, __newValue);
                    }
                    break;
                case "AnyFile":
                    {
                        var __oldValue = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.AnyFile);
                        var __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)parentObj;
                        NotifyPropertyChanging("AnyFile", __oldValue, __newValue);
                        this.Proxy.AnyFile = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("AnyFile", __oldValue, __newValue);
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
                case "AnotherFile":
                case "AnyFile":
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "AnotherFile":
                return TriggerFetchAnotherFileAsync();
            case "AnyFile":
                return TriggerFetchAnyFileAsync();
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

            if (_fk_AnotherFile.HasValue)
                this.AnotherFile = ((at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.FindPersistenceObject<at.dasz.DocumentManagement.File>(_fk_AnotherFile.Value));
            else
                this.AnotherFile = null;

            if (_fk_AnyFile.HasValue)
                this.AnyFile = ((at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.FindPersistenceObject<at.dasz.DocumentManagement.File>(_fk_AnyFile.Value));
            else
                this.AnyFile = null;
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
                    new PropertyDescriptorNHibernateImpl<DocumentTestObject, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("6f8a1d45-5064-4c7e-bd01-bcf892a536cd"),
                        "AnotherFile",
                        null,
                        obj => obj.AnotherFile,
                        (obj, val) => obj.AnotherFile = val,
						obj => OnAnotherFile_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<DocumentTestObject, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("427d1022-4953-4fc1-90aa-867fe3898688"),
                        "AnyFile",
                        null,
                        obj => obj.AnyFile,
                        (obj, val) => obj.AnyFile = val,
						obj => OnAnyFile_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<DocumentTestObject, string>(
                        lazyCtx,
                        new Guid("d666f08f-498a-44cf-82e9-9b4f7bfa1c74"),
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
        [EventBasedMethod("OnToString_DocumentTestObject")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DocumentTestObject != null)
            {
                OnToString_DocumentTestObject(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DocumentTestObject> OnToString_DocumentTestObject;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DocumentTestObject")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DocumentTestObject != null)
            {
                OnObjectIsValid_DocumentTestObject(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DocumentTestObject> OnObjectIsValid_DocumentTestObject;

        [EventBasedMethod("OnNotifyPreSave_DocumentTestObject")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DocumentTestObject != null) OnNotifyPreSave_DocumentTestObject(this);
        }
        public static event ObjectEventHandler<DocumentTestObject> OnNotifyPreSave_DocumentTestObject;

        [EventBasedMethod("OnNotifyPostSave_DocumentTestObject")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DocumentTestObject != null) OnNotifyPostSave_DocumentTestObject(this);
        }
        public static event ObjectEventHandler<DocumentTestObject> OnNotifyPostSave_DocumentTestObject;

        [EventBasedMethod("OnNotifyCreated_DocumentTestObject")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("AnotherFile");
            SetNotInitializedProperty("AnyFile");
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_DocumentTestObject != null) OnNotifyCreated_DocumentTestObject(this);
        }
        public static event ObjectEventHandler<DocumentTestObject> OnNotifyCreated_DocumentTestObject;

        [EventBasedMethod("OnNotifyDeleting_DocumentTestObject")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DocumentTestObject != null) OnNotifyDeleting_DocumentTestObject(this);

            // FK_TestObj_has_AnotherFile
            if (AnotherFile != null) {
                ((NHibernatePersistenceObject)AnotherFile).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)AnotherFile);
            }
            // FK_TestObj_has_AnyFile
            if (AnyFile != null) {
                ((NHibernatePersistenceObject)AnyFile).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)AnyFile);
            }

            AnotherFile = null;
            AnyFile = null;
        }
        public static event ObjectEventHandler<DocumentTestObject> OnNotifyDeleting_DocumentTestObject;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class DocumentTestObjectProxy
            : IProxyObject, ISortKey<int>
        {
            public DocumentTestObjectProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(DocumentTestObjectNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(DocumentTestObjectProxy); } }

            public virtual at.dasz.DocumentManagement.FileNHibernateImpl.FileProxy AnotherFile { get; set; }

            public virtual at.dasz.DocumentManagement.FileNHibernateImpl.FileProxy AnyFile { get; set; }

            public virtual string Name { get; set; }


			[System.Runtime.Serialization.IgnoreDataMember]
			int ISortKey<int>.InternalSortKey { get { return ID; } }
        }

        // make proxy available for the provider
        [System.Runtime.Serialization.IgnoreDataMember]
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.AnotherFile != null ? OurContext.GetIdFromProxy(this.Proxy.AnotherFile) : (int?)null);
            binStream.Write(this.Proxy.AnyFile != null ? OurContext.GetIdFromProxy(this.Proxy.AnyFile) : (int?)null);
            binStream.Write(this.Proxy.Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_AnotherFile);
            binStream.Read(out this._fk_AnyFile);
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