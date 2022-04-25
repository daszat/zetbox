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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// The One-Side of the classes for the One_to_N_relations Tests
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("One_to_N_relations_One")]
    public class One_to_N_relations_OneMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, One_to_N_relations_One
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
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
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
        private string _Name;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.One_to_N_relations_One, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.One_to_N_relations_One, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.One_to_N_relations_One, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.One_to_N_relations_One> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // Zetbox.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Zetbox.App.Test.One_to_N_relations_N> NSide
        {
            get
            {
                if (_NSide == null)
                {
                    var task = TriggerFetchNSideAsync();
                    task.TryRunSynchronously();
                }
                return _NSide;
            }
        }

        public async System.Threading.Tasks.Task<ICollection<Zetbox.App.Test.One_to_N_relations_N>> GetProp_NSide()
        {
            await TriggerFetchNSideAsync();
            return _NSide;
        }

        System.Threading.Tasks.Task _triggerFetchNSideTask;
        public System.Threading.Tasks.Task TriggerFetchNSideAsync()
        {
            if (_triggerFetchNSideTask != null) return _triggerFetchNSideTask;

            List<Zetbox.App.Test.One_to_N_relations_N> serverList = null;
            if (Helper.IsPersistedObject(this))
            {
                _triggerFetchNSideTask = Context.GetListOfAsync<Zetbox.App.Test.One_to_N_relations_N>(this, "NSide")
                    .OnResult(t =>
                    {
                        serverList = t.Result;
                    });
            }
            else
            {
                _triggerFetchNSideTask = System.Threading.Tasks.Task.FromResult(new List<Zetbox.App.Test.One_to_N_relations_N>()).ContinueWith(t =>
                {
                    serverList = t.Result;
                });
            }

            _triggerFetchNSideTask = _triggerFetchNSideTask.OnResult(t =>
            {
                _NSide = new OneNRelationList<Zetbox.App.Test.One_to_N_relations_N>(
                    "OneSide",
                    null,
                    this,
                    OnNSideCollectionChanged,
                    serverList);
            });
            return _triggerFetchNSideTask;
        }

        internal void OnNSideCollectionChanged()
        {
            NotifyPropertyChanged("NSide", null, null);
            if (OnNSide_PostSetter != null && IsAttached)
                OnNSide_PostSetter(this);
        }

        private OneNRelationList<Zetbox.App.Test.One_to_N_relations_N> _NSide;
public static event PropertyListChangedHandler<Zetbox.App.Test.One_to_N_relations_One> OnNSide_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.One_to_N_relations_One> OnNSide_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // Zetbox.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Zetbox.App.Test.One_to_N_relations_OrderedN> OrderedNSide
        {
            get
            {
                if (_OrderedNSide == null)
                {
                    var task = TriggerFetchOrderedNSideAsync();
                    task.TryRunSynchronously();
                }
                return _OrderedNSide;
            }
        }

        public async System.Threading.Tasks.Task<IList<Zetbox.App.Test.One_to_N_relations_OrderedN>> GetProp_OrderedNSide()
        {
            await TriggerFetchOrderedNSideAsync();
            return _OrderedNSide;
        }

        System.Threading.Tasks.Task _triggerFetchOrderedNSideTask;
        public System.Threading.Tasks.Task TriggerFetchOrderedNSideAsync()
        {
            if (_triggerFetchOrderedNSideTask != null) return _triggerFetchOrderedNSideTask;

            List<Zetbox.App.Test.One_to_N_relations_OrderedN> serverList = null;
            if (Helper.IsPersistedObject(this))
            {
                _triggerFetchOrderedNSideTask = Context.GetListOfAsync<Zetbox.App.Test.One_to_N_relations_OrderedN>(this, "OrderedNSide")
                    .OnResult(t =>
                    {
                        serverList = t.Result;
                    });
            }
            else
            {
                _triggerFetchOrderedNSideTask = System.Threading.Tasks.Task.FromResult(new List<Zetbox.App.Test.One_to_N_relations_OrderedN>()).ContinueWith(t =>
                {
                    serverList = t.Result;
                });
            }

            _triggerFetchOrderedNSideTask = _triggerFetchOrderedNSideTask.OnResult(t =>
            {
                _OrderedNSide = new OneNRelationList<Zetbox.App.Test.One_to_N_relations_OrderedN>(
                    "OneSide",
                    "OrderedNSide_pos",
                    this,
                    OnOrderedNSideCollectionChanged,
                    serverList);
            });
            return _triggerFetchOrderedNSideTask;
        }

        internal void OnOrderedNSideCollectionChanged()
        {
            NotifyPropertyChanged("OrderedNSide", null, null);
            if (OnOrderedNSide_PostSetter != null && IsAttached)
                OnOrderedNSide_PostSetter(this);
        }

        private OneNRelationList<Zetbox.App.Test.One_to_N_relations_OrderedN> _OrderedNSide;
public static event PropertyListChangedHandler<Zetbox.App.Test.One_to_N_relations_One> OnOrderedNSide_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.One_to_N_relations_One> OnOrderedNSide_IsValid;

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
                case "NSide":
                case "OrderedNSide":
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
            case "NSide":
                return TriggerFetchNSideAsync();
            case "OrderedNSide":
                return TriggerFetchOrderedNSideAsync();
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
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_One, string>(
                        lazyCtx,
                        new Guid("eea22954-2845-4b34-a721-358469fd0740"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_One, ICollection<Zetbox.App.Test.One_to_N_relations_N>>(
                        lazyCtx,
                        new Guid("00c825ba-6df2-4739-8074-2a85aae274a4"),
                        "NSide",
                        null,
                        obj => obj.NSide,
                        null, // lists are read-only properties
                        obj => OnNSide_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_One, IList<Zetbox.App.Test.One_to_N_relations_OrderedN>>(
                        lazyCtx,
                        new Guid("084f914f-a4e6-4703-a01a-7fdda5f720d0"),
                        "OrderedNSide",
                        null,
                        obj => obj.OrderedNSide,
                        null, // lists are read-only properties
                        obj => OnOrderedNSide_IsValid), 
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
            base.NotifyDeleting();
            if (OnNotifyDeleting_One_to_N_relations_One != null) OnNotifyDeleting_One_to_N_relations_One(this);
            NSide.Clear();
            OrderedNSide.Clear();
        }
        public static event ObjectEventHandler<One_to_N_relations_One> OnNotifyDeleting_One_to_N_relations_One;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
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