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
    /// A test class for persistently ordered 1:N relations
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("OrderedNEnd")]
    public class OrderedNEndMemoryImpl : Kistl.DalProvider.Memory.DataObjectMemoryImpl, OrderedNEnd
    {
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
        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
        // fkBackingName=_fk_OneEnd; fkGuidBackingName=_fk_guid_OneEnd;
        // referencedInterface=Kistl.App.Test.OrderedOneEnd; moduleNamespace=Kistl.App.Test;
        // inverse Navigator=NEnds; is list;
        // PositionStorage=NEnds_pos;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.Test.OrderedOneEnd OneEnd
        {
            get { return OneEndImpl; }
            set { OneEndImpl = (Kistl.App.Test.OrderedOneEndMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_OneEnd;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.Test.OrderedOneEndMemoryImpl OneEndImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Test.OrderedOneEndMemoryImpl __value;
                if (_fk_OneEnd.HasValue)
                    __value = (Kistl.App.Test.OrderedOneEndMemoryImpl)Context.Find<Kistl.App.Test.OrderedOneEnd>(_fk_OneEnd.Value);
                else
                    __value = null;

                if (OnOneEnd_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Test.OrderedOneEnd>(__value);
                    OnOneEnd_Getter(this, e);
                    __value = (Kistl.App.Test.OrderedOneEndMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_OneEnd == null)
                    return;
                else if (value != null && value.ID == _fk_OneEnd)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = OneEndImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("OneEnd", __oldValue, __newValue);

                if (OnOneEnd_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PreSetter(this, e);
                    __newValue = (Kistl.App.Test.OrderedOneEndMemoryImpl)e.Result;
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
                    (__oldValue.NEnds as IRelationListSync<Kistl.App.Test.OrderedNEnd>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.NEnds as IRelationListSync<Kistl.App.Test.OrderedNEnd>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("OneEnd", __oldValue, __newValue);

                if (OnOneEnd_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PostSetter(this, e);
                }
            }
        }
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingValueProperty
        public int? NEnds_pos
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int?);
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
                }
            }
        }
        private int? _NEnds_pos;
        // END Kistl.Generator.Templates.Properties.NotifyingValueProperty
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
		public static event PropertyGetterHandler<Kistl.App.Test.OrderedNEnd, Kistl.App.Test.OrderedOneEnd> OnOneEnd_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.OrderedNEnd, Kistl.App.Test.OrderedOneEnd> OnOneEnd_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.OrderedNEnd, Kistl.App.Test.OrderedOneEnd> OnOneEnd_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.OrderedNEnd> OnOneEnd_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public int? OtherInt
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int?);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _OtherInt;
                if (OnOtherInt_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnOtherInt_Getter(this, __e);
                    __result = __e.Result;
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
                    if (OnOtherInt_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnOtherInt_PostSetter(this, __e);
                    }
                }
            }
        }
        private int? _OtherInt;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.OrderedNEnd, int?> OnOtherInt_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.OrderedNEnd, int?> OnOtherInt_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.OrderedNEnd, int?> OnOtherInt_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.OrderedNEnd> OnOtherInt_IsValid;

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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_OneEnd.HasValue)
                OneEndImpl = (Kistl.App.Test.OrderedOneEndMemoryImpl)Context.Find<Kistl.App.Test.OrderedOneEnd>(_fk_OneEnd.Value);
            else
                OneEndImpl = null;
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
                    new PropertyDescriptorMemoryImpl<OrderedNEnd, Kistl.App.Test.OrderedOneEnd>(
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

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
            e.IsValid = b.IsValid;
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
        }
        public static event ObjectEventHandler<OrderedNEnd> OnNotifyDeleting_OrderedNEnd;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(OneEnd != null ? OneEnd.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._NEnds_pos, binStream);
            BinarySerializer.ToStream(this._OtherInt, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_OneEnd, binStream);
            BinarySerializer.FromStream(out this._NEnds_pos, binStream);
            BinarySerializer.FromStream(out this._OtherInt, binStream);
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
            XmlStreamer.ToStream(OneEnd != null ? OneEnd.ID : (int?)null, xml, "OneEnd", "Kistl.App.Test");
            XmlStreamer.ToStream(this._NEnds_pos, xml, "NEnds_pos", "Kistl.App.Test");
            XmlStreamer.ToStream(this._OtherInt, xml, "OtherInt", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_OneEnd, xml, "OneEnd", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._NEnds_pos, xml, "NEnds_pos", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._OtherInt, xml, "OtherInt", "Kistl.App.Test");
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