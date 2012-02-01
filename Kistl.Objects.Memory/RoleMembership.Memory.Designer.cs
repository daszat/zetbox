// <autogenerated/>

namespace Kistl.App.Base
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

    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RoleMembership")]
    public class RoleMembershipMemoryImpl : Kistl.App.Base.AccessControlMemoryImpl, RoleMembership
    {
        [Obsolete]
        public RoleMembershipMemoryImpl()
            : base(null)
        {
        }

        public RoleMembershipMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public IList<Kistl.App.Base.Relation> Relations
		{
			get
			{
				if (_Relations == null)
				{
					Context.FetchRelation<Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryMemoryImpl>(new Guid("f74d425f-e733-4cba-baca-f4a05fbc0a80"), RelationEndRole.A, this);
					_Relations 
						= new ObservableBSideListWrapper<Kistl.App.Base.RoleMembership, Kistl.App.Base.Relation, Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryMemoryImpl, ICollection<Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (IList<Kistl.App.Base.Relation>)_Relations;
			}
		}

		private ObservableBSideListWrapper<Kistl.App.Base.RoleMembership, Kistl.App.Base.Relation, Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryMemoryImpl, ICollection<Kistl.App.Base.RoleMembership_resolves_Relation_RelationEntryMemoryImpl>> _Relations;

        public override Type GetImplementedInterface()
        {
            return typeof(RoleMembership);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (RoleMembership)obj;
            var otherImpl = (RoleMembershipMemoryImpl)obj;
            var me = (RoleMembership)this;

        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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
                    new PropertyDescriptorMemoryImpl<RoleMembershipMemoryImpl, IList<Kistl.App.Base.Relation>>(
                        lazyCtx,
                        new Guid("fb799900-1a5b-4b62-a445-5dae8febdd28"),
                        "Relations",
                        null,
                        obj => obj.Relations,
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
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_RoleMembership")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RoleMembership != null)
            {
                OnToString_RoleMembership(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RoleMembership> OnToString_RoleMembership;

        [EventBasedMethod("OnPreSave_RoleMembership")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_RoleMembership != null) OnPreSave_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnPreSave_RoleMembership;

        [EventBasedMethod("OnPostSave_RoleMembership")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_RoleMembership != null) OnPostSave_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnPostSave_RoleMembership;

        [EventBasedMethod("OnCreated_RoleMembership")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_RoleMembership != null) OnCreated_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnCreated_RoleMembership;

        [EventBasedMethod("OnDeleting_RoleMembership")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_RoleMembership != null) OnDeleting_RoleMembership(this);
        }
        public static event ObjectEventHandler<RoleMembership> OnDeleting_RoleMembership;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
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
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}