// <autogenerated/>

namespace at.dasz.DocumentManagement
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
    /// Excerpt from a file
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Excerpt")]
    public class ExcerptNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, Excerpt
    {
        private static readonly Guid _objectClassID = new Guid("51516bc2-b1b3-41cd-a84a-b04877ee6c14");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public ExcerptNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public ExcerptNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new ExcerptProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public ExcerptNHibernateImpl(Func<IFrozenContext> lazyCtx, ExcerptProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly ExcerptProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for File
        // fkBackingName=this.Proxy.File; fkGuidBackingName=_fk_guid_File;
        // referencedInterface=at.dasz.DocumentManagement.File; moduleNamespace=at.dasz.DocumentManagement;
        // inverse Navigator=Excerpt; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public at.dasz.DocumentManagement.File File
        {
            get
            {
                at.dasz.DocumentManagement.FileNHibernateImpl __value = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.File);

                if (OnFile_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<at.dasz.DocumentManagement.File>(__value);
                    OnFile_Getter(this, e);
                    __value = (at.dasz.DocumentManagement.FileNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.File == null)
                {
                    SetInitializedProperty("File");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.File);
                var __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("File");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("File", __oldValue, __newValue);

                if (OnFile_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnFile_PreSetter(this, e);
                    __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.File = null;
                }
                else
                {
                    this.Proxy.File = __newValue.Proxy;
                }

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // unset old reference
                    __oldValue.Excerpt = null;
                }

                if (__newValue != null)
                {
                    // set new reference
                    __newValue.Excerpt = this;
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("File", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnFile_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<at.dasz.DocumentManagement.File>(__oldValue, __newValue);
                    OnFile_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for File's id, used on dehydration only</summary>
        private int? _fk_File = null;

        /// <summary>ForeignKey Property for File's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_File
		{
			get { return File != null ? File.ID : (int?)null; }
			set { _fk_File = value; }
		}


    public Zetbox.API.Async.ZbTask TriggerFetchFileAsync()
    {
        return new Zetbox.API.Async.ZbTask<at.dasz.DocumentManagement.File>(this.File);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for File
		public static event PropertyGetterHandler<at.dasz.DocumentManagement.Excerpt, at.dasz.DocumentManagement.File> OnFile_Getter;
		public static event PropertyPreSetterHandler<at.dasz.DocumentManagement.Excerpt, at.dasz.DocumentManagement.File> OnFile_PreSetter;
		public static event PropertyPostSetterHandler<at.dasz.DocumentManagement.Excerpt, at.dasz.DocumentManagement.File> OnFile_PostSetter;

        public static event PropertyIsValidHandler<at.dasz.DocumentManagement.Excerpt> OnFile_IsValid;

        /// <summary>
        /// Text of the Excerpt
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Text
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Text;
                if (OnText_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnText_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Text != value)
                {
                    var __oldValue = Proxy.Text;
                    var __newValue = value;
                    if (OnText_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnText_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Text", __oldValue, __newValue);
                    Proxy.Text = __newValue;
                    NotifyPropertyChanged("Text", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnText_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnText_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Text");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<at.dasz.DocumentManagement.Excerpt, string> OnText_Getter;
		public static event PropertyPreSetterHandler<at.dasz.DocumentManagement.Excerpt, string> OnText_PreSetter;
		public static event PropertyPostSetterHandler<at.dasz.DocumentManagement.Excerpt, string> OnText_PostSetter;

        public static event PropertyIsValidHandler<at.dasz.DocumentManagement.Excerpt> OnText_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Excerpt);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Excerpt)obj;
            var otherImpl = (ExcerptNHibernateImpl)obj;
            var me = (Excerpt)this;

            me.Text = other.Text;
            this._fk_File = otherImpl._fk_File;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "File":
                    {
                        var __oldValue = (at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.File);
                        var __newValue = (at.dasz.DocumentManagement.FileNHibernateImpl)parentObj;
                        NotifyPropertyChanging("File", __oldValue, __newValue);
                        this.Proxy.File = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("File", __oldValue, __newValue);
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
                case "File":
                case "Text":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "File":
                return TriggerFetchFileAsync();
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

            if (_fk_File.HasValue)
                this.File = ((at.dasz.DocumentManagement.FileNHibernateImpl)OurContext.FindPersistenceObject<at.dasz.DocumentManagement.File>(_fk_File.Value));
            else
                this.File = null;
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
                    new PropertyDescriptorNHibernateImpl<Excerpt, at.dasz.DocumentManagement.File>(
                        lazyCtx,
                        new Guid("f36d87fe-bd2f-472c-a716-46667ab1e0bd"),
                        "File",
                        null,
                        obj => obj.File,
                        (obj, val) => obj.File = val,
						obj => OnFile_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Excerpt, string>(
                        lazyCtx,
                        new Guid("af39fe5d-383d-40fd-a780-439727195612"),
                        "Text",
                        null,
                        obj => obj.Text,
                        (obj, val) => obj.Text = val,
						obj => OnText_IsValid), 
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
        [EventBasedMethod("OnToString_Excerpt")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Excerpt != null)
            {
                OnToString_Excerpt(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Excerpt> OnToString_Excerpt;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Excerpt")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Excerpt != null)
            {
                OnObjectIsValid_Excerpt(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Excerpt> OnObjectIsValid_Excerpt;

        [EventBasedMethod("OnNotifyPreSave_Excerpt")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Excerpt != null) OnNotifyPreSave_Excerpt(this);
        }
        public static event ObjectEventHandler<Excerpt> OnNotifyPreSave_Excerpt;

        [EventBasedMethod("OnNotifyPostSave_Excerpt")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Excerpt != null) OnNotifyPostSave_Excerpt(this);
        }
        public static event ObjectEventHandler<Excerpt> OnNotifyPostSave_Excerpt;

        [EventBasedMethod("OnNotifyCreated_Excerpt")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("File");
            SetNotInitializedProperty("Text");
            base.NotifyCreated();
            if (OnNotifyCreated_Excerpt != null) OnNotifyCreated_Excerpt(this);
        }
        public static event ObjectEventHandler<Excerpt> OnNotifyCreated_Excerpt;

        [EventBasedMethod("OnNotifyDeleting_Excerpt")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Excerpt != null) OnNotifyDeleting_Excerpt(this);

            // FK_File_has_Excerpt
            if (File != null) {
                ((NHibernatePersistenceObject)File).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)File);
            }

            File = null;
        }
        public static event ObjectEventHandler<Excerpt> OnNotifyDeleting_Excerpt;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class ExcerptProxy
            : IProxyObject, ISortKey<int>
        {
            public ExcerptProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(ExcerptNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(ExcerptProxy); } }

            public virtual at.dasz.DocumentManagement.FileNHibernateImpl.FileProxy File { get; set; }

            public virtual string Text { get; set; }

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
            binStream.Write(this.Proxy.File != null ? OurContext.GetIdFromProxy(this.Proxy.File) : (int?)null);
            binStream.Write(this.Proxy.Text);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_File);
            this.Proxy.Text = binStream.ReadString();
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