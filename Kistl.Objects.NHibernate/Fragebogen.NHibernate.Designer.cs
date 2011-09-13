// <autogenerated/>

namespace Kistl.App.Test
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
    [System.Diagnostics.DebuggerDisplay("Fragebogen")]
    public class FragebogenNHibernateImpl : Kistl.DalProvider.NHibernate.DataObjectNHibernateImpl, Fragebogen
    {
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

        // Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Test.Antwort> Antworten
        {
            get
            {
                if (_Antworten == null)
                {
                    _Antworten = new OneNRelationList<Kistl.App.Test.Antwort>(
                        "Fragebogen",
                        "gute_Antworten_pos",
                        this,
                        () => this.NotifyPropertyChanging("Antworten", null, null),
                        () => { this.NotifyPropertyChanged("Antworten", null, null); if(OnAntworten_PostSetter != null && IsAttached) OnAntworten_PostSetter(this); },
                        new ProjectedCollection<Kistl.App.Test.AntwortNHibernateImpl.AntwortProxy, Kistl.App.Test.Antwort>(
                            Proxy.Antworten,
                            p => (Kistl.App.Test.Antwort)OurContext.AttachAndWrap(p),
                            d => (Kistl.App.Test.AntwortNHibernateImpl.AntwortProxy)((NHibernatePersistenceObject)d).NHibernateProxy));
                }
                return _Antworten;
            }
        }
    
        private OneNRelationList<Kistl.App.Test.Antwort> _Antworten;
        private List<int> AntwortenIds;
        private bool Antworten_was_eagerLoaded = false;
public static event PropertyListChangedHandler<Kistl.App.Test.Fragebogen> OnAntworten_PostSetter;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int? BogenNummer
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(int?);
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
                    if (OnBogenNummer_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Kistl.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.Test.TestStudent> Student
		{
			get
			{
				if (_Student == null)
				{
					_Student 
						= new NHibernateASideCollectionWrapper<Kistl.App.Test.TestStudent, Kistl.App.Test.Fragebogen, TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl.TestStudent_füllt_aus_Fragebogen_RelationEntryProxy, TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl>(
                                this.Proxy.Student,
                                p => (TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl.TestStudent_füllt_aus_Fragebogen_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy),
                            entry => (IRelationListSync<TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl>)entry.A.Testbogen);
                    if (Student_was_eagerLoaded) { Student_was_eagerLoaded = false; }
				}
				return (ICollection<Kistl.App.Test.TestStudent>)_Student;
			}
		}

		private NHibernateASideCollectionWrapper<Kistl.App.Test.TestStudent, Kistl.App.Test.Fragebogen, TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl> _Student;
		// ignored, but required for Serialization
        private bool Student_was_eagerLoaded = false;

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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }


        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<FragebogenNHibernateImpl, IList<Kistl.App.Test.Antwort>>(
                        lazyCtx,
                        new Guid("e8f20c02-abea-4c91-850f-c321adfd46f0"),
                        "Antworten",
                        null,
                        obj => obj.Antworten,
                        null), // lists are read-only properties
                    // else
                    new PropertyDescriptorNHibernateImpl<FragebogenNHibernateImpl, int?>(
                        lazyCtx,
                        new Guid("b65f1a91-e063-4054-a2e7-d5dc0292e3fc"),
                        "BogenNummer",
                        null,
                        obj => obj.BogenNummer,
                        (obj, val) => obj.BogenNummer = val),
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<FragebogenNHibernateImpl, ICollection<Kistl.App.Test.TestStudent>>(
                        lazyCtx,
                        new Guid("3a91e745-0dd2-4f31-864e-eaf657ddb577"),
                        "Student",
                        null,
                        obj => obj.Student,
                        null), // lists are read-only properties
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

        [EventBasedMethod("OnPreSave_Fragebogen")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Fragebogen != null) OnPreSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnPreSave_Fragebogen;

        [EventBasedMethod("OnPostSave_Fragebogen")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Fragebogen != null) OnPostSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnPostSave_Fragebogen;

        [EventBasedMethod("OnCreated_Fragebogen")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Fragebogen != null) OnCreated_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnCreated_Fragebogen;

        [EventBasedMethod("OnDeleting_Fragebogen")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Fragebogen != null) OnDeleting_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnDeleting_Fragebogen;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            // Follow Ein_Fragebogen_enthaelt_gute_Antworten
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.Test.Antwort>()
                .Where(child => child.Fragebogen == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            return result;
        }


        public class FragebogenProxy
            : IProxyObject, ISortKey<int>
        {
            public FragebogenProxy()
            {
                Antworten = new Collection<Kistl.App.Test.AntwortNHibernateImpl.AntwortProxy>();
                Student = new Collection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl.TestStudent_füllt_aus_Fragebogen_RelationEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZBoxWrapper { get { return typeof(FragebogenNHibernateImpl); } }
            public virtual Type ZBoxProxy { get { return typeof(FragebogenProxy); } }

            public virtual ICollection<Kistl.App.Test.AntwortNHibernateImpl.AntwortProxy> Antworten { get; set; }

            public virtual int? BogenNummer { get; set; }

            public virtual ICollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryNHibernateImpl.TestStudent_füllt_aus_Fragebogen_RelationEntryProxy> Student { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;

			BinarySerializer.ToStream(eagerLoadLists, binStream);
			if (eagerLoadLists && auxObjects != null)
			{
				BinarySerializer.ToStream(true, binStream);
				BinarySerializer.ToStream(Antworten.Count, binStream);
				foreach(var obj in Antworten)
				{
					auxObjects.Add(obj);
					BinarySerializer.ToStream(obj.ID, binStream);
				}
			}
			else
			{
				BinarySerializer.ToStream(false, binStream);
			}
            BinarySerializer.ToStream(this.Proxy.BogenNummer, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {

			BinarySerializer.FromStream(out Antworten_was_eagerLoaded, binStream);
			{
				bool containsList;
				BinarySerializer.FromStream(out containsList, binStream);
				if (containsList)
				{
					int numElements;
					BinarySerializer.FromStream(out numElements, binStream);
					AntwortenIds = new List<int>(numElements);
					while (numElements-- > 0) 
					{
						int id;
						BinarySerializer.FromStream(out id, binStream);
						AntwortenIds.Add(id);
					}
				}
			}
            {
                int? tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.BogenNummer = tmp;
            }
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
            XmlStreamer.ToStream(this.Proxy.BogenNummer, xml, "BogenNummer", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                int? tmp = this.Proxy.BogenNummer;
                XmlStreamer.FromStream(ref tmp, xml, "BogenNummer", "Kistl.App.Test");
                this.Proxy.BogenNummer = tmp;
            }
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}