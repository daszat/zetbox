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
    /// The N-Side of the classes for the One_to_N_relations Tests
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("One_to_N_relations_N")]
    public class One_to_N_relations_NMemoryImpl : Kistl.DalProvider.Memory.DataObjectMemoryImpl, One_to_N_relations_N
    {
        [Obsolete]
        public One_to_N_relations_NMemoryImpl()
            : base(null)
        {
        }

        public One_to_N_relations_NMemoryImpl(Func<IFrozenContext> lazyCtx)
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
            }
        }
        private string _Name;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.One_to_N_relations_N, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.One_to_N_relations_N, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.One_to_N_relations_N, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.One_to_N_relations_N> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneSide
        // fkBackingName=_fk_OneSide; fkGuidBackingName=_fk_guid_OneSide;
        // referencedInterface=Kistl.App.Test.One_to_N_relations_One; moduleNamespace=Kistl.App.Test;
        // inverse Navigator=NSide; is list;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.Test.One_to_N_relations_One OneSide
        {
            get { return OneSideImpl; }
            set { OneSideImpl = (Kistl.App.Test.One_to_N_relations_OneMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_OneSide;


        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.Test.One_to_N_relations_OneMemoryImpl OneSideImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Test.One_to_N_relations_OneMemoryImpl __value;
                if (_fk_OneSide.HasValue)
                    __value = (Kistl.App.Test.One_to_N_relations_OneMemoryImpl)Context.Find<Kistl.App.Test.One_to_N_relations_One>(_fk_OneSide.Value);
                else
                    __value = null;

                if (OnOneSide_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Test.One_to_N_relations_One>(__value);
                    OnOneSide_Getter(this, e);
                    __value = (Kistl.App.Test.One_to_N_relations_OneMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_OneSide == null)
                    return;
                else if (value != null && value.ID == _fk_OneSide)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = OneSideImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("OneSide", __oldValue, __newValue);

                if (OnOneSide_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Test.One_to_N_relations_One>(__oldValue, __newValue);
                    OnOneSide_PreSetter(this, e);
                    __newValue = (Kistl.App.Test.One_to_N_relations_OneMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_OneSide = __newValue == null ? (int?)null : __newValue.ID;

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.NSide as IRelationListSync<Kistl.App.Test.One_to_N_relations_N>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.NSide as IRelationListSync<Kistl.App.Test.One_to_N_relations_N>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("OneSide", __oldValue, __newValue);

                if (OnOneSide_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Test.One_to_N_relations_One>(__oldValue, __newValue);
                    OnOneSide_PostSetter(this, e);
                }
            }
        }
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for OneSide
		public static event PropertyGetterHandler<Kistl.App.Test.One_to_N_relations_N, Kistl.App.Test.One_to_N_relations_One> OnOneSide_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.One_to_N_relations_N, Kistl.App.Test.One_to_N_relations_One> OnOneSide_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.One_to_N_relations_N, Kistl.App.Test.One_to_N_relations_One> OnOneSide_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.One_to_N_relations_N> OnOneSide_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(One_to_N_relations_N);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (One_to_N_relations_N)obj;
            var otherImpl = (One_to_N_relations_NMemoryImpl)obj;
            var me = (One_to_N_relations_N)this;

            me.Name = other.Name;
            this._fk_OneSide = otherImpl._fk_OneSide;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "OneSide":
                    {
                        var __oldValue = _fk_OneSide;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("OneSide", __oldValue, __newValue);
                        _fk_OneSide = __newValue;
                        NotifyPropertyChanged("OneSide", __oldValue, __newValue);
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

            if (_fk_OneSide.HasValue)
                OneSideImpl = (Kistl.App.Test.One_to_N_relations_OneMemoryImpl)Context.Find<Kistl.App.Test.One_to_N_relations_One>(_fk_OneSide.Value);
            else
                OneSideImpl = null;
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
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_N, string>(
                        lazyCtx,
                        new Guid("1b96dcd0-bf73-4855-84e5-7f8b1621672a"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<One_to_N_relations_N, Kistl.App.Test.One_to_N_relations_One>(
                        lazyCtx,
                        new Guid("598a1fc0-442e-436f-8dab-c04112c1709e"),
                        "OneSide",
                        null,
                        obj => obj.OneSide,
                        (obj, val) => obj.OneSide = val,
						obj => OnOneSide_IsValid), 
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
        [EventBasedMethod("OnToString_One_to_N_relations_N")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_One_to_N_relations_N != null)
            {
                OnToString_One_to_N_relations_N(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<One_to_N_relations_N> OnToString_One_to_N_relations_N;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_One_to_N_relations_N")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_One_to_N_relations_N != null)
            {
                OnObjectIsValid_One_to_N_relations_N(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<One_to_N_relations_N> OnObjectIsValid_One_to_N_relations_N;

        [EventBasedMethod("OnNotifyPreSave_One_to_N_relations_N")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_One_to_N_relations_N != null) OnNotifyPreSave_One_to_N_relations_N(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_N> OnNotifyPreSave_One_to_N_relations_N;

        [EventBasedMethod("OnNotifyPostSave_One_to_N_relations_N")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_One_to_N_relations_N != null) OnNotifyPostSave_One_to_N_relations_N(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_N> OnNotifyPostSave_One_to_N_relations_N;

        [EventBasedMethod("OnNotifyCreated_One_to_N_relations_N")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            SetNotInitializedProperty("OneSide");
            base.NotifyCreated();
            if (OnNotifyCreated_One_to_N_relations_N != null) OnNotifyCreated_One_to_N_relations_N(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_N> OnNotifyCreated_One_to_N_relations_N;

        [EventBasedMethod("OnNotifyDeleting_One_to_N_relations_N")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_One_to_N_relations_N != null) OnNotifyDeleting_One_to_N_relations_N(this);
        }
        public static event ObjectEventHandler<One_to_N_relations_N> OnNotifyDeleting_One_to_N_relations_N;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(OneSide != null ? OneSide.ID : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._fk_OneSide, binStream);
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
            XmlStreamer.ToStream(OneSide != null ? OneSide.ID : (int?)null, xml, "OneSide", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._fk_OneSide, xml, "OneSide", "Kistl.App.Test");
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