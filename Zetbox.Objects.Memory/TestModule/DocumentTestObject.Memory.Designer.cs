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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// DocumentTestObject
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DocumentTestObject")]
    public class DocumentTestObjectMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, DocumentTestObject
    {
        private static readonly Guid _objectClassID = new Guid("58f806e0-77ef-4d7b-ab01-857a6e6432b2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DocumentTestObjectMemoryImpl()
            : base(null)
        {
        }

        public DocumentTestObjectMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnotherFile
        // fkBackingName=_fk_AnotherFile; fkGuidBackingName=_fk_guid_AnotherFile;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public at.dasz.DocumentManagement.File AnotherFile
        {
            get { return AnotherFileImpl; }
            set { AnotherFileImpl = (at.dasz.DocumentManagement.FileMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> GetProp_AnotherFile()
        {
            return TriggerFetchAnotherFileAsync();
        }

        public async System.Threading.Tasks.Task SetProp_AnotherFile(at.dasz.DocumentManagement.File newValue)
        {
            await TriggerFetchAnotherFileAsync();
            AnotherFileImpl = (at.dasz.DocumentManagement.FileMemoryImpl)newValue;
        }

        private int? __fk_AnotherFileCache;

        private int? _fk_AnotherFile {
            get
            {
                return __fk_AnotherFileCache;
            }
            set
            {
                __fk_AnotherFileCache = value;
                // Recreate task to clear it's cache
                _triggerFetchAnotherFileTask = null;
            }
        }

        /// <summary>ForeignKey Property for AnotherFile's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_AnotherFile
		{
			get { return _fk_AnotherFile; }
			set { _fk_AnotherFile = value; }
		}


        System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> _triggerFetchAnotherFileTask;
        public System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> TriggerFetchAnotherFileAsync()
        {
            if (_triggerFetchAnotherFileTask != null) return _triggerFetchAnotherFileTask;

            if (_fk_AnotherFile.HasValue)
                _triggerFetchAnotherFileTask = Context.FindAsync<at.dasz.DocumentManagement.File>(_fk_AnotherFile.Value);
            else
                _triggerFetchAnotherFileTask = System.Threading.Tasks.Task.FromResult<at.dasz.DocumentManagement.File>(null);

            _triggerFetchAnotherFileTask.OnResult(t =>
            {
                if (OnAnotherFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(t.Result);
                    OnAnotherFile_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchAnotherFileTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal at.dasz.DocumentManagement.FileMemoryImpl AnotherFileImpl
        {
            get
            {
                var task = TriggerFetchAnotherFileAsync();
                task.TryRunSynchronously();
                return (at.dasz.DocumentManagement.FileMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_AnotherFile == null) || (value != null && value.ID == _fk_AnotherFile))
                {
                    SetInitializedProperty("AnotherFile");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = AnotherFileImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("AnotherFile", __oldValue, __newValue);

                if (OnAnotherFile_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnotherFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_AnotherFile = __newValue == null ? (int?)null : __newValue.ID;

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
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnotherFile
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnAnotherFile_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnyFile
        // fkBackingName=_fk_AnyFile; fkGuidBackingName=_fk_guid_AnyFile;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public at.dasz.DocumentManagement.File AnyFile
        {
            get { return AnyFileImpl; }
            set { AnyFileImpl = (at.dasz.DocumentManagement.FileMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> GetProp_AnyFile()
        {
            return TriggerFetchAnyFileAsync();
        }

        public async System.Threading.Tasks.Task SetProp_AnyFile(at.dasz.DocumentManagement.File newValue)
        {
            await TriggerFetchAnyFileAsync();
            AnyFileImpl = (at.dasz.DocumentManagement.FileMemoryImpl)newValue;
        }

        private int? __fk_AnyFileCache;

        private int? _fk_AnyFile {
            get
            {
                return __fk_AnyFileCache;
            }
            set
            {
                __fk_AnyFileCache = value;
                // Recreate task to clear it's cache
                _triggerFetchAnyFileTask = null;
            }
        }

        /// <summary>ForeignKey Property for AnyFile's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_AnyFile
		{
			get { return _fk_AnyFile; }
			set { _fk_AnyFile = value; }
		}


        System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> _triggerFetchAnyFileTask;
        public System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> TriggerFetchAnyFileAsync()
        {
            if (_triggerFetchAnyFileTask != null) return _triggerFetchAnyFileTask;

            if (_fk_AnyFile.HasValue)
                _triggerFetchAnyFileTask = Context.FindAsync<at.dasz.DocumentManagement.File>(_fk_AnyFile.Value);
            else
                _triggerFetchAnyFileTask = System.Threading.Tasks.Task.FromResult<at.dasz.DocumentManagement.File>(null);

            _triggerFetchAnyFileTask.OnResult(t =>
            {
                if (OnAnyFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(t.Result);
                    OnAnyFile_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchAnyFileTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal at.dasz.DocumentManagement.FileMemoryImpl AnyFileImpl
        {
            get
            {
                var task = TriggerFetchAnyFileAsync();
                task.TryRunSynchronously();
                return (at.dasz.DocumentManagement.FileMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_AnyFile == null) || (value != null && value.ID == _fk_AnyFile))
                {
                    SetInitializedProperty("AnyFile");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = AnyFileImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("AnyFile", __oldValue, __newValue);

                if (OnAnyFile_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnyFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_AnyFile = __newValue == null ? (int?)null : __newValue.ID;

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
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnyFile
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnAnyFile_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Name
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = _Name = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    var __oldValue = _Name;
                    var __newValue = value;
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
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
        private string _Name;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
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
            var otherImpl = (DocumentTestObjectMemoryImpl)obj;
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
                        var __oldValue = _fk_AnotherFile;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("AnotherFile", __oldValue, __newValue);
                        _fk_AnotherFile = __newValue;
                        NotifyPropertyChanged("AnotherFile", __oldValue, __newValue);
                    }
                    break;
                case "AnyFile":
                    {
                        var __oldValue = _fk_AnyFile;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("AnyFile", __oldValue, __newValue);
                        _fk_AnyFile = __newValue;
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
                AnotherFileImpl = (at.dasz.DocumentManagement.FileMemoryImpl)(await Context.FindAsync<at.dasz.DocumentManagement.File>(_fk_AnotherFile.Value));
            else
                AnotherFileImpl = null;

            if (_fk_AnyFile.HasValue)
                AnyFileImpl = (at.dasz.DocumentManagement.FileMemoryImpl)(await Context.FindAsync<at.dasz.DocumentManagement.File>(_fk_AnyFile.Value));
            else
                AnyFileImpl = null;
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
                    new PropertyDescriptorMemoryImpl<DocumentTestObject, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("6f8a1d45-5064-4c7e-bd01-bcf892a536cd"),
                        "AnotherFile",
                        null,
                        obj => obj.AnotherFile,
                        (obj, val) => obj.AnotherFile = val,
						obj => OnAnotherFile_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<DocumentTestObject, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("427d1022-4953-4fc1-90aa-867fe3898688"),
                        "AnyFile",
                        null,
                        obj => obj.AnyFile,
                        (obj, val) => obj.AnyFile = val,
						obj => OnAnyFile_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<DocumentTestObject, string>(
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
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

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
            AnotherFile = null;
            AnyFile = null;
        }
        public static event ObjectEventHandler<DocumentTestObject> OnNotifyDeleting_DocumentTestObject;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(AnotherFile != null ? AnotherFile.ID : (int?)null);
            binStream.Write(AnyFile != null ? AnyFile.ID : (int?)null);
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_AnotherFile = binStream.ReadNullableInt32();
            this._fk_AnyFile = binStream.ReadNullableInt32();
            this._Name = binStream.ReadString();
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