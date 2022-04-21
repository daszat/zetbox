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
    /// Describes a Company
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestCompany")]
    public class TestCompanyNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, TestCompany
    {
        private static readonly Guid _objectClassID = new Guid("352a4ade-1dca-4d28-9630-66bbcc1622ea");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public TestCompanyNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public TestCompanyNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new TestCompanyProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public TestCompanyNHibernateImpl(Func<IFrozenContext> lazyCtx, TestCompanyProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly TestCompanyProxy Proxy;

        /// <summary>
        /// Company name
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
		public static event PropertyGetterHandler<Zetbox.App.Test.TestCompany, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestCompany, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestCompany, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestCompany> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // CompoundObject list property

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ValueCollectionProperty
        public IList<Zetbox.App.Test.TestPhoneCompoundObject> Phones
        {
            get
            {
                if (_Phones == null)
                {
                    _Phones = new ClientValueListWrapper<TestCompany, Zetbox.App.Test.TestPhoneCompoundObject, Zetbox.App.Test.TestCompany_Phones_CollectionEntry, Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl, ICollection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl>>(
                            this.Context,
                            this,
                            () => { this.NotifyPropertyChanged("Phones", null, null); if(OnPhones_PostSetter != null && IsAttached) OnPhones_PostSetter(this); },
                            PhonesCollection);
                }
                return _Phones;
            }
        }

        private ProjectedCollection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl.TestCompany_Phones_CollectionEntryProxy, Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl> PhonesCollection
        {
            get {
                if (_PhonesCollection == null)
                {
                    _PhonesCollection = new ProjectedCollection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl.TestCompany_Phones_CollectionEntryProxy, Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl>(
                        () => this.Proxy.Phones,
                        p => (Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                        d => (Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl.TestCompany_Phones_CollectionEntryProxy)((NHibernatePersistenceObject)d).NHibernateProxy);
                }
                return _PhonesCollection;
            }
        }

        public System.Threading.Tasks.Task<IList<Zetbox.App.Test.TestPhoneCompoundObject>> GetProp_Phones()
        {
            return System.Threading.Tasks.Task.FromResult(Phones);
        }

        private ClientValueListWrapper<TestCompany, Zetbox.App.Test.TestPhoneCompoundObject, Zetbox.App.Test.TestCompany_Phones_CollectionEntry, Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl, ICollection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl>> _Phones;
        private ProjectedCollection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl.TestCompany_Phones_CollectionEntryProxy, Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl> _PhonesCollection;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ValueCollectionProperty
public static event PropertyListChangedHandler<Zetbox.App.Test.TestCompany> OnPhones_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestCompany> OnPhones_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(TestCompany);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestCompany)obj;
            var otherImpl = (TestCompanyNHibernateImpl)obj;
            var me = (TestCompany)this;

            me.Name = other.Name;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
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
                    new PropertyDescriptorNHibernateImpl<TestCompany, string>(
                        lazyCtx,
                        new Guid("4a038e35-fffb-4ba7-8009-1954c317a799"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<TestCompany, IList<Zetbox.App.Test.TestPhoneCompoundObject>>(
                        lazyCtx,
                        new Guid("477dd46f-24d1-4db8-934b-131adea34f13"),
                        "Phones",
                        null,
                        obj => obj.Phones,
                        null, // lists are read-only properties
                        obj => OnPhones_IsValid), 
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
        [EventBasedMethod("OnToString_TestCompany")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestCompany != null)
            {
                OnToString_TestCompany(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TestCompany> OnToString_TestCompany;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_TestCompany")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_TestCompany != null)
            {
                OnObjectIsValid_TestCompany(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<TestCompany> OnObjectIsValid_TestCompany;

        [EventBasedMethod("OnNotifyPreSave_TestCompany")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_TestCompany != null) OnNotifyPreSave_TestCompany(this);
        }
        public static event ObjectEventHandler<TestCompany> OnNotifyPreSave_TestCompany;

        [EventBasedMethod("OnNotifyPostSave_TestCompany")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_TestCompany != null) OnNotifyPostSave_TestCompany(this);
        }
        public static event ObjectEventHandler<TestCompany> OnNotifyPostSave_TestCompany;

        [EventBasedMethod("OnNotifyCreated_TestCompany")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_TestCompany != null) OnNotifyCreated_TestCompany(this);
        }
        public static event ObjectEventHandler<TestCompany> OnNotifyCreated_TestCompany;

        [EventBasedMethod("OnNotifyDeleting_TestCompany")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_TestCompany != null) OnNotifyDeleting_TestCompany(this);
            foreach(NHibernatePersistenceObject x in PhonesCollection) {
                x.ParentsToDelete.Add(this);
                ChildrenToDelete.Add(x);
            }


            Phones.Clear();
        }
        public static event ObjectEventHandler<TestCompany> OnNotifyDeleting_TestCompany;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class TestCompanyProxy
            : IProxyObject, ISortKey<int>
        {
            public TestCompanyProxy()
            {
                Phones = new Collection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl.TestCompany_Phones_CollectionEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(TestCompanyNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(TestCompanyProxy); } }

            public virtual string Name { get; set; }

            public virtual ICollection<Zetbox.App.Test.TestCompany_Phones_CollectionEntryNHibernateImpl.TestCompany_Phones_CollectionEntryProxy> Phones { get; set; }

            public virtual int? Phones_pos { get; set; }


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
            binStream.Write(this.Proxy.Name);
            binStream.WriteCollectionEntries(this.PhonesCollection);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
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