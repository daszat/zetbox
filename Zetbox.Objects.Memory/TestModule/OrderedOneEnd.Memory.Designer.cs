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
    /// A test class for persistently ordered 1:N relations
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("OrderedOneEnd")]
    public class OrderedOneEndMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, OrderedOneEnd
    {
        private static readonly Guid _objectClassID = new Guid("c170405f-c29d-4d87-a4f7-a622185a94d9");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public OrderedOneEndMemoryImpl()
            : base(null)
        {
        }

        public OrderedOneEndMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // Zetbox.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Zetbox.App.Test.OrderedNEnd> NEnds
        {
            get
            {
                if (_NEnds == null)
                {
                    var task = TriggerFetchNEndsAsync();
                    task.TryRunSynchronously();
                }
                return _NEnds;
            }
        }

        public async System.Threading.Tasks.Task<IList<Zetbox.App.Test.OrderedNEnd>> GetProp_NEnds()
        {
            await TriggerFetchNEndsAsync();
            return _NEnds;
        }

        System.Threading.Tasks.Task _triggerFetchNEndsTask;
        public System.Threading.Tasks.Task TriggerFetchNEndsAsync()
        {
            if (_triggerFetchNEndsTask != null) return _triggerFetchNEndsTask;

            List<Zetbox.App.Test.OrderedNEnd> serverList = null;
            if (Helper.IsPersistedObject(this))
            {
                _triggerFetchNEndsTask = Context.GetListOfAsync<Zetbox.App.Test.OrderedNEnd>(this, "NEnds")
                    .OnResult(t =>
                    {
                        serverList = t.Result;
                    });
            }
            else
            {
                _triggerFetchNEndsTask = System.Threading.Tasks.Task.FromResult(new List<Zetbox.App.Test.OrderedNEnd>()).ContinueWith(t =>
                {
                    serverList = t.Result;
                });
            }

            _triggerFetchNEndsTask = _triggerFetchNEndsTask.OnResult(t =>
            {
                _NEnds = new OneNRelationList<Zetbox.App.Test.OrderedNEnd>(
                    "OneEnd",
                    "NEnds_pos",
                    this,
                    OnNEndsCollectionChanged,
                    serverList);
            });
            return _triggerFetchNEndsTask;
        }

        internal void OnNEndsCollectionChanged()
        {
            NotifyPropertyChanged("NEnds", null, null);
            if (OnNEnds_PostSetter != null && IsAttached)
                OnNEnds_PostSetter(this);
        }

        private OneNRelationList<Zetbox.App.Test.OrderedNEnd> _NEnds;
public static event PropertyListChangedHandler<Zetbox.App.Test.OrderedOneEnd> OnNEnds_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.OrderedOneEnd> OnNEnds_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int? SomeInt
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _SomeInt;
                if (OnSomeInt_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnSomeInt_Getter(this, __e);
                    __result = _SomeInt = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_SomeInt != value)
                {
                    var __oldValue = _SomeInt;
                    var __newValue = value;
                    if (OnSomeInt_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnSomeInt_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("SomeInt", __oldValue, __newValue);
                    _SomeInt = __newValue;
                    NotifyPropertyChanged("SomeInt", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnSomeInt_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnSomeInt_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("SomeInt");
                }
            }
        }
        private int? _SomeInt;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.OrderedOneEnd, int?> OnSomeInt_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.OrderedOneEnd, int?> OnSomeInt_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.OrderedOneEnd, int?> OnSomeInt_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.OrderedOneEnd> OnSomeInt_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(OrderedOneEnd);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (OrderedOneEnd)obj;
            var otherImpl = (OrderedOneEndMemoryImpl)obj;
            var me = (OrderedOneEnd)this;

            me.SomeInt = other.SomeInt;
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
                case "SomeInt":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "NEnds":
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
            case "NEnds":
                return TriggerFetchNEndsAsync();
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<OrderedOneEnd, IList<Zetbox.App.Test.OrderedNEnd>>(
                        lazyCtx,
                        new Guid("7b7a227b-462a-4923-bda6-15264f0af841"),
                        "NEnds",
                        null,
                        obj => obj.NEnds,
                        null, // lists are read-only properties
                        obj => OnNEnds_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<OrderedOneEnd, int?>(
                        lazyCtx,
                        new Guid("1062a9d3-b936-42b8-99ba-2353087cbce1"),
                        "SomeInt",
                        null,
                        obj => obj.SomeInt,
                        (obj, val) => obj.SomeInt = val,
						obj => OnSomeInt_IsValid), 
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
        [EventBasedMethod("OnToString_OrderedOneEnd")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_OrderedOneEnd != null)
            {
                OnToString_OrderedOneEnd(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<OrderedOneEnd> OnToString_OrderedOneEnd;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_OrderedOneEnd")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_OrderedOneEnd != null)
            {
                OnObjectIsValid_OrderedOneEnd(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<OrderedOneEnd> OnObjectIsValid_OrderedOneEnd;

        [EventBasedMethod("OnNotifyPreSave_OrderedOneEnd")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_OrderedOneEnd != null) OnNotifyPreSave_OrderedOneEnd(this);
        }
        public static event ObjectEventHandler<OrderedOneEnd> OnNotifyPreSave_OrderedOneEnd;

        [EventBasedMethod("OnNotifyPostSave_OrderedOneEnd")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_OrderedOneEnd != null) OnNotifyPostSave_OrderedOneEnd(this);
        }
        public static event ObjectEventHandler<OrderedOneEnd> OnNotifyPostSave_OrderedOneEnd;

        [EventBasedMethod("OnNotifyCreated_OrderedOneEnd")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("SomeInt");
            base.NotifyCreated();
            if (OnNotifyCreated_OrderedOneEnd != null) OnNotifyCreated_OrderedOneEnd(this);
        }
        public static event ObjectEventHandler<OrderedOneEnd> OnNotifyCreated_OrderedOneEnd;

        [EventBasedMethod("OnNotifyDeleting_OrderedOneEnd")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_OrderedOneEnd != null) OnNotifyDeleting_OrderedOneEnd(this);
            NEnds.Clear();
        }
        public static event ObjectEventHandler<OrderedOneEnd> OnNotifyDeleting_OrderedOneEnd;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._SomeInt);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._SomeInt = binStream.ReadNullableInt32();
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