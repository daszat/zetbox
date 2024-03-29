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
    [System.Diagnostics.DebuggerDisplay("TestStudent")]
    public class TestStudentNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, TestStudent
    {
        private static readonly Guid _objectClassID = new Guid("9efc763c-9cdf-41e3-930c-7505fc4ac840");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public TestStudentNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public TestStudentNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new TestStudentProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public TestStudentNHibernateImpl(Func<IFrozenContext> lazyCtx, TestStudentProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly TestStudentProxy Proxy;

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
		public static event PropertyGetterHandler<Zetbox.App.Test.TestStudent, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestStudent, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestStudent, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestStudent> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.Test.Fragebogen> Testbogen
		{
			get
			{
				if (_Testbogen == null)
				{
					_Testbogen 
						= new NHibernateBSideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl>(
                                () => this.Proxy.Testbogen,
                                p => (Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy));
                    _Testbogen.CollectionChanged += (s, e) => { this.NotifyPropertyChanged("Testbogen", null, null); if(OnTestbogen_PostSetter != null && IsAttached) OnTestbogen_PostSetter(this); };
                    if (Testbogen_was_eagerLoaded) { Testbogen_was_eagerLoaded = false; }
				}
				return (ICollection<Zetbox.App.Test.Fragebogen>)_Testbogen;
			}
		}

        public async System.Threading.Tasks.Task<ICollection<Zetbox.App.Test.Fragebogen>> GetProp_Testbogen()
        {
            await TriggerFetchTestbogenAsync();
            return _Testbogen;
        }

		private NHibernateBSideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl> _Testbogen;
		// ignored, but required for Serialization
        private bool Testbogen_was_eagerLoaded = false;

        public System.Threading.Tasks.Task TriggerFetchTestbogenAsync()
        {
            return System.Threading.Tasks.Task.FromResult<ICollection<Zetbox.App.Test.Fragebogen>>(this.Testbogen);
        }

public static event PropertyListChangedHandler<Zetbox.App.Test.TestStudent> OnTestbogen_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestStudent> OnTestbogen_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(TestStudent);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestStudent)obj;
            var otherImpl = (TestStudentNHibernateImpl)obj;
            var me = (TestStudent)this;

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

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Testbogen":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Testbogen":
                return TriggerFetchTestbogenAsync();
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
                    new PropertyDescriptorNHibernateImpl<TestStudent, string>(
                        lazyCtx,
                        new Guid("190b4492-c1cb-40a2-8941-84b8ff3ac141"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<TestStudent, ICollection<Zetbox.App.Test.Fragebogen>>(
                        lazyCtx,
                        new Guid("f330d95b-372d-4302-b4d1-73afc5fa71de"),
                        "Testbogen",
                        null,
                        obj => obj.Testbogen,
                        null, // lists are read-only properties
                        obj => OnTestbogen_IsValid), 
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
        [EventBasedMethod("OnToString_TestStudent")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestStudent != null)
            {
                OnToString_TestStudent(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TestStudent> OnToString_TestStudent;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_TestStudent")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_TestStudent != null)
            {
                OnObjectIsValid_TestStudent(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<TestStudent> OnObjectIsValid_TestStudent;

        [EventBasedMethod("OnNotifyPreSave_TestStudent")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_TestStudent != null) OnNotifyPreSave_TestStudent(this);
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyPreSave_TestStudent;

        [EventBasedMethod("OnNotifyPostSave_TestStudent")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_TestStudent != null) OnNotifyPostSave_TestStudent(this);
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyPostSave_TestStudent;

        [EventBasedMethod("OnNotifyCreated_TestStudent")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_TestStudent != null) OnNotifyCreated_TestStudent(this);
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyCreated_TestStudent;

        [EventBasedMethod("OnNotifyDeleting_TestStudent")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_TestStudent != null) OnNotifyDeleting_TestStudent(this);


            Testbogen.Clear();
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyDeleting_TestStudent;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class TestStudentProxy
            : IProxyObject, ISortKey<int>
        {
            public TestStudentProxy()
            {
                Testbogen = new Collection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(TestStudentNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(TestStudentProxy); } }

            public virtual string Name { get; set; }

            public virtual ICollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy> Testbogen { get; set; }


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