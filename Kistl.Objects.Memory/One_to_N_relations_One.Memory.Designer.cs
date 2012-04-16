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

    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.Memory;

    /// <summary>
    /// The One-Side of the classes for the One_to_N_relations Tests
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("One_to_N_relations_One")]
    public class One_to_N_relations_OneMemoryImpl : Kistl.DalProvider.Memory.DataObjectMemoryImpl, One_to_N_relations_One
    {
        private static readonly Guid _objectClassID = new Guid("e98ca434-19be-4daa-8920-d979a1d98522");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public One_to_N_relations_OneMemoryImpl()
            : base(null)
        {
        }

        public One_to_N_relations_OneMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// A property to test queries across the Relation
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string Name
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
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
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.One_to_N_relations_One, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.One_to_N_relations_One, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.One_to_N_relations_One, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.One_to_N_relations_One> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // Kistl.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Test.One_to_N_relations_N> NSide
        {
            get
            {
                if (_NSide == null)
                {
                    List<Kistl.App.Test.One_to_N_relations_N> serverList;
                    if (Helper.IsPersistedObject(this))
                    {
                        serverList = Context.GetListOf<Kistl.App.Test.One_to_N_relations_N>(this, "NSide");
                    }
                    else
                    {
                        serverList = new List<Kistl.App.Test.One_to_N_relations_N>();
                    }
    
                    _NSide = new OneNRelationList<Kistl.App.Test.One_to_N_relations_N>(
                        "OneSide",
                        null,
                        this,
                        () => { this.NotifyPropertyChanged("NSide", null, null); if(OnNSide_PostSetter != null && IsAttached) OnNSide_PostSetter(this); },
                        serverList);
                }
                return _NSide;
            }
        }
    
        private OneNRelationList<Kistl.App.Test.One_to_N_relations_N> _NSide;

public static event PropertyListChangedHandler<Kistl.App.Test.One_to_N_relations_One> OnNSide_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.One_to_N_relations_One> OnNSide_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(One_to_N_relations_One);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (One_to_N_relations_One)obj;
            var otherImpl = (One_to_N_relations_OneMemoryImpl)obj;
            var me = (One_to_N_relations_One)this;

            me.Name = other.Name;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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
                    // else
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_One, string>(
                        lazyCtx,
                        new Guid("eea22954-2845-4b34-a721-358469fd0740"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_One, ICollection<Kistl.App.Test.One_to_N_relations_N>>(
                        lazyCtx,
                        new Guid("00c825ba-6df2-4739-8074-2a85aae274a4"),
                        "NSide",
                        null,
                        obj => obj.NSide,
                        null, // lists are read-only properties
                        obj => OnNSide_IsValid), 
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
        [EventBasedMethod("OnToString_One_to_N_relations_One")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_One_to_N_relations_One != null)
            {
                OnToString_One_to_N_relations_One(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<One_to_N_relations_One> OnToString_One_to_N_relations_One;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_One_to_N_relations_One")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_One_to_N_relations_One != null)
            {
                OnObjectIsValid_One_to_N_relations_One(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<One_to_N_relations_One> OnObjectIsValid_One_to_N_relations_One;

        [EventBasedMethod("OnNotifyPreSave_One_to_N_relations_One")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_One_to_N_relations_One != null) OnNotifyPreSave_One_to_N_relations_One(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_One> OnNotifyPreSave_One_to_N_relations_One;

        [EventBasedMethod("OnNotifyPostSave_One_to_N_relations_One")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_One_to_N_relations_One != null) OnNotifyPostSave_One_to_N_relations_One(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_One> OnNotifyPostSave_One_to_N_relations_One;

        [EventBasedMethod("OnNotifyCreated_One_to_N_relations_One")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_One_to_N_relations_One != null) OnNotifyCreated_One_to_N_relations_One(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_One> OnNotifyCreated_One_to_N_relations_One;

        [EventBasedMethod("OnNotifyDeleting_One_to_N_relations_One")]
        public override void NotifyDeleting()
        {
            NSide.Clear();
            base.NotifyDeleting();
            if (OnNotifyDeleting_One_to_N_relations_One != null) OnNotifyDeleting_One_to_N_relations_One(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_One> OnNotifyDeleting_One_to_N_relations_One;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._Name, binStream);
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
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Test");
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