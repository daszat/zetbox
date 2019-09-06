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
    [System.Diagnostics.DebuggerDisplay("Fragebogen")]
    public class FragebogenNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, Fragebogen
    {
        private static readonly Guid _objectClassID = new Guid("a78ff235-4511-431b-8437-939f7fecded4");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public FragebogenNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public FragebogenNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new FragebogenProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public FragebogenNHibernateImpl(Func<IFrozenContext> lazyCtx, FragebogenProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly FragebogenProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // object list property

        // Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Zetbox.App.Test.Antwort> Antworten
        {
            get
            {
                if (_Antworten == null)
                {
                    _Antworten = new OneNRelationList<Zetbox.App.Test.Antwort>(
                        "Fragebogen",
                        "gute_Antworten_pos",
                        this,
                        () => this.NotifyPropertyChanging("Antworten", null, null),
                        () => { this.NotifyPropertyChanged("Antworten", null, null); if(OnAntworten_PostSetter != null && IsAttached) OnAntworten_PostSetter(this); },
                        new ProjectedCollection<Zetbox.App.Test.AntwortNHibernateImpl.AntwortProxy, Zetbox.App.Test.Antwort>(
                            () => Proxy.Antworten,
                            p => (Zetbox.App.Test.Antwort)OurContext.AttachAndWrap(p),
                            d => (Zetbox.App.Test.AntwortNHibernateImpl.AntwortProxy)((NHibernatePersistenceObject)d).NHibernateProxy));
                }
                return _Antworten;
            }
        }
    
        private OneNRelationList<Zetbox.App.Test.Antwort> _Antworten;
        private List<int> AntwortenIds;
        private bool Antworten_was_eagerLoaded = false;

        public Zetbox.API.Async.ZbTask TriggerFetchAntwortenAsync()
        {
            return new Zetbox.API.Async.ZbTask<IList<Zetbox.App.Test.Antwort>>(this.Antworten);
        }

public static event PropertyListChangedHandler<Zetbox.App.Test.Fragebogen> OnAntworten_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Fragebogen> OnAntworten_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int? BogenNummer
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.BogenNummer;
                if (OnBogenNummer_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnBogenNummer_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.BogenNummer != value)
                {
                    var __oldValue = Proxy.BogenNummer;
                    var __newValue = value;
                    if (OnBogenNummer_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("BogenNummer", __oldValue, __newValue);
                    Proxy.BogenNummer = __newValue;
                    NotifyPropertyChanged("BogenNummer", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnBogenNummer_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("BogenNummer");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Fragebogen, int?> OnBogenNummer_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Fragebogen, int?> OnBogenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Fragebogen, int?> OnBogenNummer_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Fragebogen> OnBogenNummer_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.Test.TestStudent> Student
		{
			get
			{
				if (_Student == null)
				{
					_Student 
						= new NHibernateASideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl>(
                                () => this.Proxy.Student,
                                p => (Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy));
                    _Student.CollectionChanged += (s, e) => { this.NotifyPropertyChanged("Student", null, null); if(OnStudent_PostSetter != null && IsAttached) OnStudent_PostSetter(this); };
                    if (Student_was_eagerLoaded) { Student_was_eagerLoaded = false; }
				}
				return (ICollection<Zetbox.App.Test.TestStudent>)_Student;
			}
		}

		private NHibernateASideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl> _Student;
		// ignored, but required for Serialization
        private bool Student_was_eagerLoaded = false;

        public Zetbox.API.Async.ZbTask TriggerFetchStudentAsync()
        {
            return new Zetbox.API.Async.ZbTask<ICollection<Zetbox.App.Test.TestStudent>>(this.Student);
        }

public static event PropertyListChangedHandler<Zetbox.App.Test.Fragebogen> OnStudent_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Fragebogen> OnStudent_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Fragebogen);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Fragebogen)obj;
            var otherImpl = (FragebogenNHibernateImpl)obj;
            var me = (Fragebogen)this;

            me.BogenNummer = other.BogenNummer;
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
                case "BogenNummer":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Antworten":
                case "Student":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Antworten":
                return TriggerFetchAntwortenAsync();
            case "Student":
                return TriggerFetchStudentAsync();
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<Fragebogen, IList<Zetbox.App.Test.Antwort>>(
                        lazyCtx,
                        new Guid("e8f20c02-abea-4c91-850f-c321adfd46f0"),
                        "Antworten",
                        null,
                        obj => obj.Antworten,
                        null, // lists are read-only properties
                        obj => OnAntworten_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Fragebogen, int?>(
                        lazyCtx,
                        new Guid("b65f1a91-e063-4054-a2e7-d5dc0292e3fc"),
                        "BogenNummer",
                        null,
                        obj => obj.BogenNummer,
                        (obj, val) => obj.BogenNummer = val,
						obj => OnBogenNummer_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<Fragebogen, ICollection<Zetbox.App.Test.TestStudent>>(
                        lazyCtx,
                        new Guid("3a91e745-0dd2-4f31-864e-eaf657ddb577"),
                        "Student",
                        null,
                        obj => obj.Student,
                        null, // lists are read-only properties
                        obj => OnStudent_IsValid), 
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
        [EventBasedMethod("OnToString_Fragebogen")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Fragebogen != null)
            {
                OnToString_Fragebogen(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Fragebogen> OnToString_Fragebogen;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Fragebogen")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Fragebogen != null)
            {
                OnObjectIsValid_Fragebogen(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Fragebogen> OnObjectIsValid_Fragebogen;

        [EventBasedMethod("OnNotifyPreSave_Fragebogen")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Fragebogen != null) OnNotifyPreSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyPreSave_Fragebogen;

        [EventBasedMethod("OnNotifyPostSave_Fragebogen")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Fragebogen != null) OnNotifyPostSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyPostSave_Fragebogen;

        [EventBasedMethod("OnNotifyCreated_Fragebogen")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("BogenNummer");
            base.NotifyCreated();
            if (OnNotifyCreated_Fragebogen != null) OnNotifyCreated_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyCreated_Fragebogen;

        [EventBasedMethod("OnNotifyDeleting_Fragebogen")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Fragebogen != null) OnNotifyDeleting_Fragebogen(this);

            // FK_Ein_Fragebogen_enthaelt_gute_Antworten ZeroOrMore
            foreach(NHibernatePersistenceObject x in Antworten) {
                x.ParentsToDelete.Add(this);
                ChildrenToDelete.Add(x);
            }

            Antworten.Clear();
            Student.Clear();
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyDeleting_Fragebogen;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class FragebogenProxy
            : IProxyObject, ISortKey<int>
        {
            public FragebogenProxy()
            {
                Antworten = new Collection<Zetbox.App.Test.AntwortNHibernateImpl.AntwortProxy>();
                Student = new Collection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(FragebogenNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(FragebogenProxy); } }

            public virtual ICollection<Zetbox.App.Test.AntwortNHibernateImpl.AntwortProxy> Antworten { get; set; }

            public virtual int? BogenNummer { get; set; }

            public virtual ICollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryNHibernateImpl.Student_füllt_aus_Testbogen_RelationEntryProxy> Student { get; set; }


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

            binStream.Write(eagerLoadLists);
            if (eagerLoadLists && auxObjects != null)
            {
                binStream.Write(true);
                binStream.Write(Antworten.Count);
                foreach(var obj in Antworten)
                {
                    auxObjects.Add(obj);
                    binStream.Write(obj.ID);
                }
            }
            else
            {
                binStream.Write(false);
            }
            binStream.Write(this.Proxy.BogenNummer);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {

            Antworten_was_eagerLoaded = binStream.ReadBoolean();
            {
                bool containsList = binStream.ReadBoolean();
                if (containsList)
                {
                    int numElements = binStream.ReadInt32();
                    AntwortenIds = new List<int>(numElements);
                    while (numElements-- > 0) 
                    {
                        int id = binStream.ReadInt32();
                        AntwortenIds.Add(id);
                    }
                }
            }
            this.Proxy.BogenNummer = binStream.ReadNullableInt32();
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