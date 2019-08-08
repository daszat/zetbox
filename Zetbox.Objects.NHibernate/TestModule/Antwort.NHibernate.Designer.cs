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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Antwort")]
    public class AntwortNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, Antwort
    {
        private static readonly Guid _objectClassID = new Guid("e5577f89-58d1-4983-ab35-60e6624780df");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public AntwortNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public AntwortNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new AntwortProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public AntwortNHibernateImpl(Func<IFrozenContext> lazyCtx, AntwortProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly AntwortProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Frage
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Frage;
                if (OnFrage_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnFrage_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Frage != value)
                {
                    var __oldValue = Proxy.Frage;
                    var __newValue = value;
                    if (OnFrage_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnFrage_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Frage", __oldValue, __newValue);
                    Proxy.Frage = __newValue;
                    NotifyPropertyChanged("Frage", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnFrage_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnFrage_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Frage");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, string> OnFrage_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, string> OnFrage_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, string> OnFrage_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnFrage_IsValid;

        /// <summary>
        /// Workaround for Case 1376
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Fragebogen
        // fkBackingName=this.Proxy.Fragebogen; fkGuidBackingName=_fk_guid_Fragebogen;
        // referencedInterface=Zetbox.App.Test.Fragebogen; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=Antworten; is list;
        // PositionStorage=gute_Antworten_pos;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public Zetbox.App.Test.Fragebogen Fragebogen
        {
            get
            {
                Zetbox.App.Test.FragebogenNHibernateImpl __value = (Zetbox.App.Test.FragebogenNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Fragebogen);

                if (OnFragebogen_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Test.Fragebogen>(__value);
                    OnFragebogen_Getter(this, e);
                    __value = (Zetbox.App.Test.FragebogenNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Fragebogen == null)
                {
                    SetInitializedProperty("Fragebogen");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Test.FragebogenNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Fragebogen);
                var __newValue = (Zetbox.App.Test.FragebogenNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("Fragebogen");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Fragebogen", __oldValue, __newValue);

                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Antworten", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Antworten", null, null);
                }

                if (OnFragebogen_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Test.Fragebogen>(__oldValue, __newValue);
                    OnFragebogen_PreSetter(this, e);
                    __newValue = (Zetbox.App.Test.FragebogenNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Fragebogen = null;
                }
                else
                {
                    this.Proxy.Fragebogen = __newValue.Proxy;
                }

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.Antworten as IRelationListSync<Zetbox.App.Test.Antwort>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.Antworten as IRelationListSync<Zetbox.App.Test.Antwort>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Fragebogen", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnFragebogen_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Test.Fragebogen>(__oldValue, __newValue);
                    OnFragebogen_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Fragebogen's id, used on dehydration only</summary>
        private int? _fk_Fragebogen = null;

        /// <summary>ForeignKey Property for Fragebogen's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Fragebogen
		{
			get { return Fragebogen != null ? Fragebogen.ID : (int?)null; }
			set { _fk_Fragebogen = value; }
		}


    public Zetbox.API.Async.ZbTask TriggerFetchFragebogenAsync()
    {
        return new Zetbox.API.Async.ZbTask<Zetbox.App.Test.Fragebogen>(this.Fragebogen);
    }

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.NotifyingValueProperty
        public virtual int? gute_Antworten_pos
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.gute_Antworten_pos;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.gute_Antworten_pos != value)
                {
                    var __oldValue = Proxy.gute_Antworten_pos;
                    var __newValue = value;
                    NotifyPropertyChanging("gute_Antworten_pos", __oldValue, __newValue);
                    Proxy.gute_Antworten_pos = __newValue;
                    NotifyPropertyChanged("gute_Antworten_pos", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                }
                else
                {
                    SetInitializedProperty("gute_Antworten_pos");
                }
            }
        }
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.NotifyingValueProperty
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Fragebogen
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, Zetbox.App.Test.Fragebogen> OnFragebogen_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, Zetbox.App.Test.Fragebogen> OnFragebogen_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, Zetbox.App.Test.Fragebogen> OnFragebogen_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnFragebogen_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int FragenNummer
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.FragenNummer;
                if (OnFragenNummer_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnFragenNummer_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.FragenNummer != value)
                {
                    var __oldValue = Proxy.FragenNummer;
                    var __newValue = value;
                    if (OnFragenNummer_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnFragenNummer_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("FragenNummer", __oldValue, __newValue);
                    Proxy.FragenNummer = __newValue;
                    NotifyPropertyChanged("FragenNummer", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnFragenNummer_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnFragenNummer_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("FragenNummer");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, int> OnFragenNummer_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, int> OnFragenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, int> OnFragenNummer_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnFragenNummer_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int? GegebeneAntwort
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.GegebeneAntwort;
                if (OnGegebeneAntwort_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnGegebeneAntwort_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.GegebeneAntwort != value)
                {
                    var __oldValue = Proxy.GegebeneAntwort;
                    var __newValue = value;
                    if (OnGegebeneAntwort_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnGegebeneAntwort_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("GegebeneAntwort", __oldValue, __newValue);
                    Proxy.GegebeneAntwort = __newValue;
                    NotifyPropertyChanged("GegebeneAntwort", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnGegebeneAntwort_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnGegebeneAntwort_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("GegebeneAntwort");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, int?> OnGegebeneAntwort_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, int?> OnGegebeneAntwort_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, int?> OnGegebeneAntwort_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnGegebeneAntwort_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Antwort);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Antwort)obj;
            var otherImpl = (AntwortNHibernateImpl)obj;
            var me = (Antwort)this;

            me.Frage = other.Frage;
            me.FragenNummer = other.FragenNummer;
            me.GegebeneAntwort = other.GegebeneAntwort;
            this.gute_Antworten_pos = otherImpl.gute_Antworten_pos;
            this._fk_Fragebogen = otherImpl._fk_Fragebogen;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Fragebogen":
                    {
                        var __oldValue = (Zetbox.App.Test.FragebogenNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Fragebogen);
                        var __newValue = (Zetbox.App.Test.FragebogenNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Fragebogen", __oldValue, __newValue);
                        this.Proxy.Fragebogen = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Fragebogen", __oldValue, __newValue);
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
                case "Frage":
                case "Fragebogen":
                case "FragenNummer":
                case "GegebeneAntwort":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Fragebogen":
                return TriggerFetchFragebogenAsync();
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

            if (_fk_Fragebogen.HasValue)
                this.Fragebogen = ((Zetbox.App.Test.FragebogenNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Test.Fragebogen>(_fk_Fragebogen.Value));
            else
                this.Fragebogen = null;
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
                    new PropertyDescriptorNHibernateImpl<Antwort, string>(
                        lazyCtx,
                        new Guid("311cb474-be7d-4e6b-b803-379e6523720c"),
                        "Frage",
                        null,
                        obj => obj.Frage,
                        (obj, val) => obj.Frage = val,
						obj => OnFrage_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Antwort, Zetbox.App.Test.Fragebogen>(
                        lazyCtx,
                        new Guid("ae20c23b-0cfa-422a-9f8d-797e9f70bf82"),
                        "Fragebogen",
                        null,
                        obj => obj.Fragebogen,
                        (obj, val) => obj.Fragebogen = val,
						obj => OnFragebogen_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Antwort, int>(
                        lazyCtx,
                        new Guid("87a005fa-6249-4aab-b90e-b50c97487c09"),
                        "FragenNummer",
                        null,
                        obj => obj.FragenNummer,
                        (obj, val) => obj.FragenNummer = val,
						obj => OnFragenNummer_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Antwort, int?>(
                        lazyCtx,
                        new Guid("bbddff1f-943e-48cb-b097-377040280f0a"),
                        "GegebeneAntwort",
                        null,
                        obj => obj.GegebeneAntwort,
                        (obj, val) => obj.GegebeneAntwort = val,
						obj => OnGegebeneAntwort_IsValid), 
                    // position columns
                    // rel: Ein_Fragebogen enthaelt gute_Antworten (0f425937-0d1e-4887-ae65-a162b45fc93e)
                    // rel.B.Type == cls && rel.B.HasPersistentOrder
                    new PropertyDescriptorNHibernateImpl<AntwortNHibernateImpl, int?>(
                        lazyCtx,
                        null,
                        "gute_Antworten_pos",
                        null,
                        obj => obj.gute_Antworten_pos,
                        (obj, val) => obj.gute_Antworten_pos = val,
						null),
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
        [EventBasedMethod("OnToString_Antwort")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Antwort != null)
            {
                OnToString_Antwort(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Antwort> OnToString_Antwort;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Antwort")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Antwort != null)
            {
                OnObjectIsValid_Antwort(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Antwort> OnObjectIsValid_Antwort;

        [EventBasedMethod("OnNotifyPreSave_Antwort")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Antwort != null) OnNotifyPreSave_Antwort(this);
        }
        public static event ObjectEventHandler<Antwort> OnNotifyPreSave_Antwort;

        [EventBasedMethod("OnNotifyPostSave_Antwort")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Antwort != null) OnNotifyPostSave_Antwort(this);
        }
        public static event ObjectEventHandler<Antwort> OnNotifyPostSave_Antwort;

        [EventBasedMethod("OnNotifyCreated_Antwort")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Frage");
            SetNotInitializedProperty("Fragebogen");
            SetNotInitializedProperty("FragenNummer");
            SetNotInitializedProperty("GegebeneAntwort");
            base.NotifyCreated();
            if (OnNotifyCreated_Antwort != null) OnNotifyCreated_Antwort(this);
        }
        public static event ObjectEventHandler<Antwort> OnNotifyCreated_Antwort;

        [EventBasedMethod("OnNotifyDeleting_Antwort")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Antwort != null) OnNotifyDeleting_Antwort(this);

            // FK_Ein_Fragebogen_enthaelt_gute_Antworten
            if (Fragebogen != null) {
                ((NHibernatePersistenceObject)Fragebogen).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)Fragebogen);
            }

            Fragebogen = null;
        }
        public static event ObjectEventHandler<Antwort> OnNotifyDeleting_Antwort;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class AntwortProxy
            : IProxyObject, ISortKey<int>
        {
            public AntwortProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(AntwortNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(AntwortProxy); } }

            public virtual string Frage { get; set; }

            public virtual Zetbox.App.Test.FragebogenNHibernateImpl.FragebogenProxy Fragebogen { get; set; }

            public virtual int FragenNummer { get; set; }

            public virtual int? GegebeneAntwort { get; set; }

            public virtual int? gute_Antworten_pos { get; set; }

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
            binStream.Write(this.Proxy.Frage);
            binStream.Write(this.Proxy.Fragebogen != null ? OurContext.GetIdFromProxy(this.Proxy.Fragebogen) : (int?)null);
            binStream.Write(this.Proxy.gute_Antworten_pos);
            binStream.Write(this.Proxy.FragenNummer);
            binStream.Write(this.Proxy.GegebeneAntwort);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this.Proxy.Frage = binStream.ReadString();
            binStream.Read(out this._fk_Fragebogen);
            this.Proxy.gute_Antworten_pos = binStream.ReadNullableInt32();
            this.Proxy.FragenNummer = binStream.ReadInt32();
            this.Proxy.GegebeneAntwort = binStream.ReadNullableInt32();
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