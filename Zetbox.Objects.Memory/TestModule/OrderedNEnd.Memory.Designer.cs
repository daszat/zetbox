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
    [System.Diagnostics.DebuggerDisplay("OrderedNEnd")]
    public class OrderedNEndMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, OrderedNEnd
    {
        private static readonly Guid _objectClassID = new Guid("e39c9cbb-3181-49e1-93ab-aed0d0e11728");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public OrderedNEndMemoryImpl()
            : base(null)
        {
        }

        public OrderedNEndMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
        // fkBackingName=_fk_OneEnd; fkGuidBackingName=_fk_guid_OneEnd;
        // referencedInterface=Zetbox.App.Test.OrderedOneEnd; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=NEnds; is list;
        // PositionStorage=NEnds_pos;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Test.OrderedOneEnd OneEnd
        {
            get { return OneEndImpl; }
            set { OneEndImpl = (Zetbox.App.Test.OrderedOneEndMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? __fk_OneEndCache;

        private int? _fk_OneEnd {
            get
            {
                return __fk_OneEndCache;
            }
            set
            {
                __fk_OneEndCache = value;
                // Recreate task to clear it's cache
                _triggerFetchOneEndTask = null;
            }
        }

        /// <summary>ForeignKey Property for OneEnd's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_OneEnd
		{
			get { return _fk_OneEnd; }
			set { _fk_OneEnd = value; }
		}


        Zetbox.API.Async.ZbTask<Zetbox.App.Test.OrderedOneEnd> _triggerFetchOneEndTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.Test.OrderedOneEnd> TriggerFetchOneEndAsync()
        {
            if (_triggerFetchOneEndTask != null) return _triggerFetchOneEndTask;

            if (_fk_OneEnd.HasValue)
                _triggerFetchOneEndTask = Context.FindAsync<Zetbox.App.Test.OrderedOneEnd>(_fk_OneEnd.Value);
            else
                _triggerFetchOneEndTask = new Zetbox.API.Async.ZbTask<Zetbox.App.Test.OrderedOneEnd>(Zetbox.API.Async.ZbTask.Synchron, () => null);

            _triggerFetchOneEndTask.OnResult(t =>
            {
                if (OnOneEnd_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Test.OrderedOneEnd>(t.Result);
                    OnOneEnd_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchOneEndTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Test.OrderedOneEndMemoryImpl OneEndImpl
        {
            get
            {
                return (Zetbox.App.Test.OrderedOneEndMemoryImpl)TriggerFetchOneEndAsync().Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_OneEnd == null) || (value != null && value.ID == _fk_OneEnd))
                {
                    SetInitializedProperty("OneEnd");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = OneEndImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("OneEnd", __oldValue, __newValue);

                if (OnOneEnd_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PreSetter(this, e);
                    __newValue = (Zetbox.App.Test.OrderedOneEndMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_OneEnd = __newValue == null ? (int?)null : __newValue.ID;

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.NEnds as IRelationListSync<Zetbox.App.Test.OrderedNEnd>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.NEnds as IRelationListSync<Zetbox.App.Test.OrderedNEnd>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("OneEnd", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnOneEnd_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PostSetter(this, e);
                }
            }
        }
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingValueProperty
        public int? NEnds_pos
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _NEnds_pos;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_NEnds_pos != value)
                {
                    var __oldValue = _NEnds_pos;
                    var __newValue = value;
                    NotifyPropertyChanging("NEnds_pos", __oldValue, __newValue);
                    _NEnds_pos = __newValue;
                    NotifyPropertyChanged("NEnds_pos", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                }
                else
                {
                    SetInitializedProperty("NEnds_pos");
                }
            }
        }
        private int? _NEnds_pos;
        // END Zetbox.Generator.Templates.Properties.NotifyingValueProperty
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
		public static event PropertyGetterHandler<Zetbox.App.Test.OrderedNEnd, Zetbox.App.Test.OrderedOneEnd> OnOneEnd_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.OrderedNEnd, Zetbox.App.Test.OrderedOneEnd> OnOneEnd_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.OrderedNEnd, Zetbox.App.Test.OrderedOneEnd> OnOneEnd_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.OrderedNEnd> OnOneEnd_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int? OtherInt
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _OtherInt;
                if (OnOtherInt_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnOtherInt_Getter(this, __e);
                    __result = _OtherInt = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_OtherInt != value)
                {
                    var __oldValue = _OtherInt;
                    var __newValue = value;
                    if (OnOtherInt_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOtherInt_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("OtherInt", __oldValue, __newValue);
                    _OtherInt = __newValue;
                    NotifyPropertyChanged("OtherInt", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnOtherInt_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOtherInt_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("OtherInt");
                }
            }
        }
        private int? _OtherInt;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.OrderedNEnd, int?> OnOtherInt_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.OrderedNEnd, int?> OnOtherInt_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.OrderedNEnd, int?> OnOtherInt_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.OrderedNEnd> OnOtherInt_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(OrderedNEnd);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (OrderedNEnd)obj;
            var otherImpl = (OrderedNEndMemoryImpl)obj;
            var me = (OrderedNEnd)this;

            me.OtherInt = other.OtherInt;
            this.NEnds_pos = otherImpl.NEnds_pos;
            this._fk_OneEnd = otherImpl._fk_OneEnd;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "OneEnd":
                    {
                        var __oldValue = _fk_OneEnd;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("OneEnd", __oldValue, __newValue);
                        _fk_OneEnd = __newValue;
                        NotifyPropertyChanged("OneEnd", __oldValue, __newValue);
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
                case "OneEnd":
                case "OtherInt":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "OneEnd":
                return TriggerFetchOneEndAsync();
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

            if (_fk_OneEnd.HasValue)
                OneEndImpl = (Zetbox.App.Test.OrderedOneEndMemoryImpl)Context.Find<Zetbox.App.Test.OrderedOneEnd>(_fk_OneEnd.Value);
            else
                OneEndImpl = null;
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
                    new PropertyDescriptorMemoryImpl<OrderedNEnd, Zetbox.App.Test.OrderedOneEnd>(
                        lazyCtx,
                        new Guid("40b1123f-b73d-4b33-ae0c-c65fe2c2c19c"),
                        "OneEnd",
                        null,
                        obj => obj.OneEnd,
                        (obj, val) => obj.OneEnd = val,
						obj => OnOneEnd_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<OrderedNEnd, int?>(
                        lazyCtx,
                        new Guid("7d5ffa69-671a-4e88-ab4b-e805d635fb9e"),
                        "OtherInt",
                        null,
                        obj => obj.OtherInt,
                        (obj, val) => obj.OtherInt = val,
						obj => OnOtherInt_IsValid), 
                    // position columns
                    // rel: OneEnd hasMany NEnds (d3b1b2d8-7ef6-4693-bbc1-b60a8352beee)
                    // rel.B.Type == cls && rel.B.HasPersistentOrder
                    new PropertyDescriptorMemoryImpl<OrderedNEndMemoryImpl, int?>(
                        lazyCtx,
                        null,
                        "NEnds_pos",
                        null,
                        obj => obj.NEnds_pos,
                        (obj, val) => obj.NEnds_pos = val,
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
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_OrderedNEnd")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_OrderedNEnd != null)
            {
                OnToString_OrderedNEnd(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<OrderedNEnd> OnToString_OrderedNEnd;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_OrderedNEnd")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_OrderedNEnd != null)
            {
                OnObjectIsValid_OrderedNEnd(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<OrderedNEnd> OnObjectIsValid_OrderedNEnd;

        [EventBasedMethod("OnNotifyPreSave_OrderedNEnd")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_OrderedNEnd != null) OnNotifyPreSave_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnNotifyPreSave_OrderedNEnd;

        [EventBasedMethod("OnNotifyPostSave_OrderedNEnd")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_OrderedNEnd != null) OnNotifyPostSave_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnNotifyPostSave_OrderedNEnd;

        [EventBasedMethod("OnNotifyCreated_OrderedNEnd")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("OneEnd");
            SetNotInitializedProperty("OtherInt");
            base.NotifyCreated();
            if (OnNotifyCreated_OrderedNEnd != null) OnNotifyCreated_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnNotifyCreated_OrderedNEnd;

        [EventBasedMethod("OnNotifyDeleting_OrderedNEnd")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_OrderedNEnd != null) OnNotifyDeleting_OrderedNEnd(this);
            OneEnd = null;
        }
        public static event ObjectEventHandler<OrderedNEnd> OnNotifyDeleting_OrderedNEnd;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(OneEnd != null ? OneEnd.ID : (int?)null);
            binStream.Write(this._NEnds_pos);
            binStream.Write(this._OtherInt);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_OneEnd = binStream.ReadNullableInt32();
            this._NEnds_pos = binStream.ReadNullableInt32();
            this._OtherInt = binStream.ReadNullableInt32();
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