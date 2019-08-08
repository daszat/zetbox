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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// DocumentTestObject
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="DocumentTestObjectEfImpl")]
    [System.Diagnostics.DebuggerDisplay("DocumentTestObject")]
    public class DocumentTestObjectEfImpl : BaseServerDataObject_EntityFramework, DocumentTestObject
    {
        private static readonly Guid _objectClassID = new Guid("58f806e0-77ef-4d7b-ab01-857a6e6432b2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DocumentTestObjectEfImpl()
            : base(null)
        {
        }

        public DocumentTestObjectEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_TestObj_has_AnotherFile
    A: ZeroOrMore DocumentTestObject as TestObj
    B: ZeroOrOne File as AnotherFile
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnotherFile
        // fkBackingName=_fk_AnotherFile; fkGuidBackingName=_fk_guid_AnotherFile;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public at.dasz.DocumentManagement.File AnotherFile
        {
            get { return AnotherFileImpl; }
            set { AnotherFileImpl = (at.dasz.DocumentManagement.FileEfImpl)value; }
        }

        private int? _fk_AnotherFile;

        /// <summary>ForeignKey Property for AnotherFile's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_AnotherFile
		{
			get { return AnotherFile != null ? AnotherFile.ID : (int?)null; }
			set { _fk_AnotherFile = value; }
		}


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TestObj_has_AnotherFile", "AnotherFile")]
        public at.dasz.DocumentManagement.FileEfImpl AnotherFileImpl
        {
            get
            {
                at.dasz.DocumentManagement.FileEfImpl __value;
                EntityReference<at.dasz.DocumentManagement.FileEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<at.dasz.DocumentManagement.FileEfImpl>(
                        "Model.FK_TestObj_has_AnotherFile",
                        "AnotherFile");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                __value = r.Value;
                if (OnAnotherFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(__value);
                    OnAnotherFile_Getter(this, e);
                    __value = (at.dasz.DocumentManagement.FileEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<at.dasz.DocumentManagement.FileEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<at.dasz.DocumentManagement.FileEfImpl>(
                        "Model.FK_TestObj_has_AnotherFile",
                        "AnotherFile");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                at.dasz.DocumentManagement.FileEfImpl __oldValue = (at.dasz.DocumentManagement.FileEfImpl)r.Value;
                at.dasz.DocumentManagement.FileEfImpl __newValue = (at.dasz.DocumentManagement.FileEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("AnotherFile", __oldValue, __newValue);

                if (OnAnotherFile_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnotherFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileEfImpl)e.Result;
                }

                r.Value = (at.dasz.DocumentManagement.FileEfImpl)__newValue;

                if (OnAnotherFile_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnotherFile_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("AnotherFile", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;
            }
        }

        public Zetbox.API.Async.ZbTask TriggerFetchAnotherFileAsync()
        {
            return new Zetbox.API.Async.ZbTask<at.dasz.DocumentManagement.File>(this.AnotherFile);
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnotherFile
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnotherFile_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnAnotherFile_IsValid;

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_TestObj_has_AnyFile
    A: ZeroOrMore DocumentTestObject as TestObj
    B: ZeroOrOne File as AnyFile
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnyFile
        // fkBackingName=_fk_AnyFile; fkGuidBackingName=_fk_guid_AnyFile;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public at.dasz.DocumentManagement.File AnyFile
        {
            get { return AnyFileImpl; }
            set { AnyFileImpl = (at.dasz.DocumentManagement.FileEfImpl)value; }
        }

        private int? _fk_AnyFile;

        /// <summary>ForeignKey Property for AnyFile's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_AnyFile
		{
			get { return AnyFile != null ? AnyFile.ID : (int?)null; }
			set { _fk_AnyFile = value; }
		}


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TestObj_has_AnyFile", "AnyFile")]
        public at.dasz.DocumentManagement.FileEfImpl AnyFileImpl
        {
            get
            {
                at.dasz.DocumentManagement.FileEfImpl __value;
                EntityReference<at.dasz.DocumentManagement.FileEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<at.dasz.DocumentManagement.FileEfImpl>(
                        "Model.FK_TestObj_has_AnyFile",
                        "AnyFile");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                __value = r.Value;
                if (OnAnyFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(__value);
                    OnAnyFile_Getter(this, e);
                    __value = (at.dasz.DocumentManagement.FileEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<at.dasz.DocumentManagement.FileEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<at.dasz.DocumentManagement.FileEfImpl>(
                        "Model.FK_TestObj_has_AnyFile",
                        "AnyFile");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                at.dasz.DocumentManagement.FileEfImpl __oldValue = (at.dasz.DocumentManagement.FileEfImpl)r.Value;
                at.dasz.DocumentManagement.FileEfImpl __newValue = (at.dasz.DocumentManagement.FileEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("AnyFile", __oldValue, __newValue);

                if (OnAnyFile_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnyFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileEfImpl)e.Result;
                }

                r.Value = (at.dasz.DocumentManagement.FileEfImpl)__newValue;

                if (OnAnyFile_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnAnyFile_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("AnyFile", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;
            }
        }

        public Zetbox.API.Async.ZbTask TriggerFetchAnyFileAsync()
        {
            return new Zetbox.API.Async.ZbTask<at.dasz.DocumentManagement.File>(this.AnyFile);
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for AnyFile
		public static event PropertyGetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.DocumentTestObject, at.dasz.DocumentManagement.File> OnAnyFile_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.DocumentTestObject> OnAnyFile_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        private string _Name_store;
        private string _Name {
            get { return _Name_store; }
            set {
                ReportEfPropertyChanging("Name");
                _Name_store = value;
                ReportEfPropertyChanged("Name");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
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
            var otherImpl = (DocumentTestObjectEfImpl)obj;
            var me = (DocumentTestObject)this;

            me.Name = other.Name;
            this._fk_AnotherFile = otherImpl._fk_AnotherFile;
            this._fk_AnyFile = otherImpl._fk_AnyFile;
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
                case "AnotherFile":
                case "AnyFile":
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
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

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_AnotherFile.HasValue)
                AnotherFileImpl = (at.dasz.DocumentManagement.FileEfImpl)Context.Find<at.dasz.DocumentManagement.File>(_fk_AnotherFile.Value);
            else
                AnotherFileImpl = null;

            if (_fk_AnyFile.HasValue)
                AnyFileImpl = (at.dasz.DocumentManagement.FileEfImpl)Context.Find<at.dasz.DocumentManagement.File>(_fk_AnyFile.Value);
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
                    new PropertyDescriptorEfImpl<DocumentTestObject, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("6f8a1d45-5064-4c7e-bd01-bcf892a536cd"),
                        "AnotherFile",
                        null,
                        obj => obj.AnotherFile,
                        (obj, val) => obj.AnotherFile = val,
						obj => OnAnotherFile_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<DocumentTestObject, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("427d1022-4953-4fc1-90aa-867fe3898688"),
                        "AnyFile",
                        null,
                        obj => obj.AnyFile,
                        (obj, val) => obj.AnyFile = val,
						obj => OnAnyFile_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<DocumentTestObject, string>(
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
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var __oldValue = _ID;
                    var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                }
                else
                {
                    SetInitializedProperty("ID");
                }
            }
        }
        private int _ID;
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var r = this.RelationshipManager.GetRelatedReference<at.dasz.DocumentManagement.FileEfImpl>("Model.FK_TestObj_has_AnotherFile", "AnotherFile");
                var key = r.EntityKey;
                binStream.Write(r.Value != null ? r.Value.ID : (key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null));
            }
            {
                var r = this.RelationshipManager.GetRelatedReference<at.dasz.DocumentManagement.FileEfImpl>("Model.FK_TestObj_has_AnyFile", "AnyFile");
                var key = r.EntityKey;
                binStream.Write(r.Value != null ? r.Value.ID : (key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null));
            }
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_AnotherFile);
            binStream.Read(out this._fk_AnyFile);
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