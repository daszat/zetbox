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

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// A test class for persistently ordered 1:N relations
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="OrderedNEnd")]
    [System.Diagnostics.DebuggerDisplay("OrderedNEnd")]
    public class OrderedNEndEfImpl : BaseServerDataObject_EntityFramework, OrderedNEnd
    {
        [Obsolete]
        public OrderedNEndEfImpl()
            : base(null)
        {
        }

        public OrderedNEndEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_OneEnd_hasMany_NEnds
    A: ZeroOrOne OrderedOneEnd as OneEnd
    B: ZeroOrMore OrderedNEnd as NEnds
    Preferred Storage: MergeIntoB
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
        // fkBackingName=_fk_OneEnd; fkGuidBackingName=_fk_guid_OneEnd;
        // referencedInterface=Kistl.App.Test.OrderedOneEnd; moduleNamespace=Kistl.App.Test;
        // inverse Navigator=NEnds; is list;
        // PositionStorage=NEnds_pos;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Test.OrderedOneEnd OneEnd
        {
            get { return OneEndImpl; }
            set { OneEndImpl = (Kistl.App.Test.OrderedOneEndEfImpl)value; }
        }

        private int? _fk_OneEnd;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_OneEnd_hasMany_NEnds", "OneEnd")]
        public Kistl.App.Test.OrderedOneEndEfImpl OneEndImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Test.OrderedOneEndEfImpl __value;
                EntityReference<Kistl.App.Test.OrderedOneEndEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Test.OrderedOneEndEfImpl>(
                        "Model.FK_OneEnd_hasMany_NEnds",
                        "OneEnd");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnOneEnd_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Test.OrderedOneEnd>(__value);
                    OnOneEnd_Getter(this, e);
                    __value = (Kistl.App.Test.OrderedOneEndEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Test.OrderedOneEndEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Test.OrderedOneEndEfImpl>(
                        "Model.FK_OneEnd_hasMany_NEnds",
                        "OneEnd");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Test.OrderedOneEndEfImpl __oldValue = (Kistl.App.Test.OrderedOneEndEfImpl)r.Value;
                Kistl.App.Test.OrderedOneEndEfImpl __newValue = (Kistl.App.Test.OrderedOneEndEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("OneEnd", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("NEnds", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("NEnds", null, null, null);
                }

                if (OnOneEnd_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PreSetter(this, e);
                    __newValue = (Kistl.App.Test.OrderedOneEndEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Test.OrderedOneEndEfImpl)__newValue;

                if (OnOneEnd_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("OneEnd", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("NEnds", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("NEnds", null, null, null);
                }
            }
        }

        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
		public static event PropertyGetterHandler<Kistl.App.Test.OrderedNEnd, Kistl.App.Test.OrderedOneEnd> OnOneEnd_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.OrderedNEnd, Kistl.App.Test.OrderedOneEnd> OnOneEnd_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.OrderedNEnd, Kistl.App.Test.OrderedOneEnd> OnOneEnd_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.OrderedNEnd, int?> OnOtherInt_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.OrderedNEnd, int?> OnOtherInt_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.OrderedNEnd, int?> OnOtherInt_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(OrderedNEnd);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (OrderedNEnd)obj;
            var otherImpl = (OrderedNEndEfImpl)obj;
            var me = (OrderedNEnd)this;

            me.OtherInt = other.OtherInt;
            this.NEnds_pos = otherImpl.NEnds_pos;
            this._fk_OneEnd = otherImpl._fk_OneEnd;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

		public override void UpdateParent(string propertyName, int? id)
		{
			int? __oldValue, __newValue = id;
			
			switch(propertyName)
			{
                case "OneEnd":
                    __oldValue = _fk_OneEnd;
                    NotifyPropertyChanging("OneEnd", __oldValue, __newValue);
                    _fk_OneEnd = __newValue;
                    NotifyPropertyChanged("OneEnd", __oldValue, __newValue);
                    break;
				default:
					base.UpdateParent(propertyName, id);
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
                OneEndImpl = (Kistl.App.Test.OrderedOneEndEfImpl)Context.Find<Kistl.App.Test.OrderedOneEnd>(_fk_OneEnd.Value);
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
                    new PropertyDescriptorEfImpl<OrderedNEndEfImpl, Kistl.App.Test.OrderedOneEnd>(
                        lazyCtx,
                        new Guid("40b1123f-b73d-4b33-ae0c-c65fe2c2c19c"),
                        "OneEnd",
                        null,
                        obj => obj.OneEnd,
                        (obj, val) => obj.OneEnd = val),
                    // else
                    new PropertyDescriptorEfImpl<OrderedNEndEfImpl, int?>(
                        lazyCtx,
                        new Guid("7d5ffa69-671a-4e88-ab4b-e805d635fb9e"),
                        "OtherInt",
                        null,
                        obj => obj.OtherInt,
                        (obj, val) => obj.OtherInt = val),
                    // position columns
                    // rel: OneEnd hasMany NEnds (d3b1b2d8-7ef6-4693-bbc1-b60a8352beee)
                    // rel.B.Type == cls && rel.B.HasPersistentOrder
                    new PropertyDescriptorEfImpl<OrderedNEndEfImpl, int?>(
                        lazyCtx,
                        null,
                        "NEnds_pos",
                        null,
                        obj => obj.NEnds_pos,
                        (obj, val) => obj.NEnds_pos = val),
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

        [EventBasedMethod("OnPreSave_OrderedNEnd")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_OrderedNEnd != null) OnPreSave_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnPreSave_OrderedNEnd;

        [EventBasedMethod("OnPostSave_OrderedNEnd")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_OrderedNEnd != null) OnPostSave_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnPostSave_OrderedNEnd;

        [EventBasedMethod("OnCreated_OrderedNEnd")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_OrderedNEnd != null) OnCreated_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnCreated_OrderedNEnd;

        [EventBasedMethod("OnDeleting_OrderedNEnd")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_OrderedNEnd != null) OnDeleting_OrderedNEnd(this);
        }
        public static event ObjectEventHandler<OrderedNEnd> OnDeleting_OrderedNEnd;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var __oldValue = _ID;
                    var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);
                }
            }
        }
        private int _ID;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Test.OrderedOneEndEfImpl>("Model.FK_OneEnd_hasMany_NEnds", "OneEnd").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
            BinarySerializer.ToStream(this._NEnds_pos, binStream);
            BinarySerializer.ToStream(this._OtherInt, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
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
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Test.OrderedOneEndEfImpl>("Model.FK_OneEnd_hasMany_NEnds", "OneEnd").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "OneEnd", "Kistl.App.Test");
            }
            XmlStreamer.ToStream(this._NEnds_pos, xml, "NEnds_pos", "Kistl.App.Test");
            XmlStreamer.ToStream(this._OtherInt, xml, "OtherInt", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
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