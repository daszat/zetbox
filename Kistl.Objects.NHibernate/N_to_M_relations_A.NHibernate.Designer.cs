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
    /// The A-Side of the classes for the N_to_M_relations Tests
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("N_to_M_relations_A")]
    public class N_to_M_relations_ANHibernateImpl : Kistl.DalProvider.NHibernate.DataObjectNHibernateImpl, N_to_M_relations_A
    {
        private static readonly Guid _objectClassID = new Guid("f17be553-bc55-49d5-9da9-869161bdd6f6");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public N_to_M_relations_ANHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public N_to_M_relations_ANHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new N_to_M_relations_AProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public N_to_M_relations_ANHibernateImpl(Func<IFrozenContext> lazyCtx, N_to_M_relations_AProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly N_to_M_relations_AProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Kistl.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.Test.N_to_M_relations_B> BSide
		{
			get
			{
				if (_BSide == null)
				{
					_BSide 
						= new NHibernateBSideCollectionWrapper<Kistl.App.Test.N_to_M_relations_A, Kistl.App.Test.N_to_M_relations_B, Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryProxy, Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl>(
                                () => this.Proxy.BSide,
                                p => (Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy),
                            entry => (IRelationListSync<Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl>)entry.B.ASide);
                    if (BSide_was_eagerLoaded) { BSide_was_eagerLoaded = false; }
				}
				return (ICollection<Kistl.App.Test.N_to_M_relations_B>)_BSide;
			}
		}

		private NHibernateBSideCollectionWrapper<Kistl.App.Test.N_to_M_relations_A, Kistl.App.Test.N_to_M_relations_B, Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl> _BSide;
		// ignored, but required for Serialization
        private bool BSide_was_eagerLoaded = false;

        public static event PropertyIsValidHandler<Kistl.App.Test.N_to_M_relations_A> OnBSide_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Name
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
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

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Test.N_to_M_relations_A, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.N_to_M_relations_A, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.N_to_M_relations_A, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.N_to_M_relations_A> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(N_to_M_relations_A);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (N_to_M_relations_A)obj;
            var otherImpl = (N_to_M_relations_ANHibernateImpl)obj;
            var me = (N_to_M_relations_A)this;

            me.Name = other.Name;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }

        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

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

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

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
                    new PropertyDescriptorNHibernateImpl<N_to_M_relations_A, ICollection<Kistl.App.Test.N_to_M_relations_B>>(
                        lazyCtx,
                        new Guid("3afe0378-20f3-46f9-8391-da25414716ff"),
                        "BSide",
                        null,
                        obj => obj.BSide,
                        null, // lists are read-only properties
                        obj => OnBSide_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<N_to_M_relations_A, string>(
                        lazyCtx,
                        new Guid("084ec7c9-1623-43f1-9afc-e61f934df963"),
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_N_to_M_relations_A")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_N_to_M_relations_A != null)
            {
                OnToString_N_to_M_relations_A(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<N_to_M_relations_A> OnToString_N_to_M_relations_A;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_N_to_M_relations_A")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_N_to_M_relations_A != null)
            {
                OnObjectIsValid_N_to_M_relations_A(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<N_to_M_relations_A> OnObjectIsValid_N_to_M_relations_A;

        [EventBasedMethod("OnNotifyPreSave_N_to_M_relations_A")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_N_to_M_relations_A != null) OnNotifyPreSave_N_to_M_relations_A(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_A> OnNotifyPreSave_N_to_M_relations_A;

        [EventBasedMethod("OnNotifyPostSave_N_to_M_relations_A")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_N_to_M_relations_A != null) OnNotifyPostSave_N_to_M_relations_A(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_A> OnNotifyPostSave_N_to_M_relations_A;

        [EventBasedMethod("OnNotifyCreated_N_to_M_relations_A")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_N_to_M_relations_A != null) OnNotifyCreated_N_to_M_relations_A(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_A> OnNotifyCreated_N_to_M_relations_A;

        [EventBasedMethod("OnNotifyDeleting_N_to_M_relations_A")]
        public override void NotifyDeleting()
        {
            BSide.Clear();
            base.NotifyDeleting();
            if (OnNotifyDeleting_N_to_M_relations_A != null) OnNotifyDeleting_N_to_M_relations_A(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_A> OnNotifyDeleting_N_to_M_relations_A;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            return result;
        }


        public class N_to_M_relations_AProxy
            : IProxyObject, ISortKey<int>
        {
            public N_to_M_relations_AProxy()
            {
                BSide = new Collection<Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZBoxWrapper { get { return typeof(N_to_M_relations_ANHibernateImpl); } }
            public virtual Type ZBoxProxy { get { return typeof(N_to_M_relations_AProxy); } }

            public virtual ICollection<Kistl.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryProxy> BSide { get; set; }

            public virtual string Name { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.Name, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.Name = tmp;
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
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this.Proxy.Name, xml, "Name", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                string tmp = this.Proxy.Name;
                XmlStreamer.FromStream(ref tmp, xml, "Name", "Kistl.App.Test");
                this.Proxy.Name = tmp;
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