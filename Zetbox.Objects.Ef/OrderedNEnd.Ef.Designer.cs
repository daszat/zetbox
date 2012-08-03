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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// A test class for persistently ordered 1:N relations
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="OrderedNEnd")]
    [System.Diagnostics.DebuggerDisplay("OrderedNEnd")]
    public class OrderedNEndEfImpl : BaseServerDataObject_EntityFramework, OrderedNEnd
    {
        private static readonly Guid _objectClassID = new Guid("e39c9cbb-3181-49e1-93ab-aed0d0e11728");
        public override Guid ObjectClassID { get { return _objectClassID; } }

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
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
        // fkBackingName=_fk_OneEnd; fkGuidBackingName=_fk_guid_OneEnd;
        // referencedInterface=Zetbox.App.Test.OrderedOneEnd; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=NEnds; is list;
        // PositionStorage=NEnds_pos;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Test.OrderedOneEnd OneEnd
        {
            get { return OneEndImpl; }
            set { OneEndImpl = (Zetbox.App.Test.OrderedOneEndEfImpl)value; }
        }

        private int? _fk_OneEnd;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_OneEnd_hasMany_NEnds", "OneEnd")]
        public Zetbox.App.Test.OrderedOneEndEfImpl OneEndImpl
        {
            get
            {
                Zetbox.App.Test.OrderedOneEndEfImpl __value;
                EntityReference<Zetbox.App.Test.OrderedOneEndEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Test.OrderedOneEndEfImpl>(
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
                    var e = new PropertyGetterEventArgs<Zetbox.App.Test.OrderedOneEnd>(__value);
                    OnOneEnd_Getter(this, e);
                    __value = (Zetbox.App.Test.OrderedOneEndEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Test.OrderedOneEndEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Test.OrderedOneEndEfImpl>(
                        "Model.FK_OneEnd_hasMany_NEnds",
                        "OneEnd");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Test.OrderedOneEndEfImpl __oldValue = (Zetbox.App.Test.OrderedOneEndEfImpl)r.Value;
                Zetbox.App.Test.OrderedOneEndEfImpl __newValue = (Zetbox.App.Test.OrderedOneEndEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("OneEnd", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("NEnds", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("NEnds", null, null);
                }

                if (OnOneEnd_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PreSetter(this, e);
                    __newValue = (Zetbox.App.Test.OrderedOneEndEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Test.OrderedOneEndEfImpl)__newValue;

                if (OnOneEnd_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Test.OrderedOneEnd>(__oldValue, __newValue);
                    OnOneEnd_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("OneEnd", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("NEnds", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("NEnds", null, null);
                }
                UpdateChangedInfo = true;
            }
        }

        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                    UpdateChangedInfo = true;

                }
				else 
				{
					SetInitializedProperty("NEnds_pos");
				}
            }
        }
        private int? _NEnds_pos_store;
        private int? _NEnds_pos {
            get { return _NEnds_pos_store; }
            set {
                ReportEfPropertyChanging("NEnds_pos");
                _NEnds_pos_store = value;
                ReportEfPropertyChanged("NEnds_pos");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneEnd
		public static event PropertyGetterHandler<Zetbox.App.Test.OrderedNEnd, Zetbox.App.Test.OrderedOneEnd> OnOneEnd_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.OrderedNEnd, Zetbox.App.Test.OrderedOneEnd> OnOneEnd_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.OrderedNEnd, Zetbox.App.Test.OrderedOneEnd> OnOneEnd_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.OrderedNEnd> OnOneEnd_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                    UpdateChangedInfo = true;

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
        private int? _OtherInt_store;
        private int? _OtherInt {
            get { return _OtherInt_store; }
            set {
                ReportEfPropertyChanging("OtherInt");
                _OtherInt_store = value;
                ReportEfPropertyChanged("OtherInt");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
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
            var otherImpl = (OrderedNEndEfImpl)obj;
            var me = (OrderedNEnd)this;

            me.OtherInt = other.OtherInt;
            this.NEnds_pos = otherImpl.NEnds_pos;
            this._fk_OneEnd = otherImpl._fk_OneEnd;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

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
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_OneEnd.HasValue)
                OneEndImpl = (Zetbox.App.Test.OrderedOneEndEfImpl)Context.Find<Zetbox.App.Test.OrderedOneEnd>(_fk_OneEnd.Value);
            else
                OneEndImpl = null;
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
                    new PropertyDescriptorEfImpl<OrderedNEnd, Zetbox.App.Test.OrderedOneEnd>(
                        lazyCtx,
                        new Guid("40b1123f-b73d-4b33-ae0c-c65fe2c2c19c"),
                        "OneEnd",
                        null,
                        obj => obj.OneEnd,
                        (obj, val) => obj.OneEnd = val,
						obj => OnOneEnd_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<OrderedNEnd, int?>(
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
                    new PropertyDescriptorEfImpl<OrderedNEndEfImpl, int?>(
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
            OneEnd = null;
        }
        public static event ObjectEventHandler<OrderedNEnd> OnNotifyDeleting_OrderedNEnd;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty
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
                    UpdateChangedInfo = true;

                }
				else 
				{
					SetInitializedProperty("ID");
				}
            }
        }
        private int _ID;
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Zetbox.App.Test.OrderedOneEndEfImpl>("Model.FK_OneEnd_hasMany_NEnds", "OneEnd").EntityKey;
                binStream.Write(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null);
            }
            binStream.Write(this._NEnds_pos);
            binStream.Write(this._OtherInt);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_OneEnd);
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