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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ANewObjectClass")]
    [System.Diagnostics.DebuggerDisplay("ANewObjectClass")]
    public class ANewObjectClassEfImpl : BaseServerDataObject_EntityFramework, ANewObjectClass
    {
        [Obsolete]
        public ANewObjectClassEfImpl()
            : base(null)
        {
        }

        public ANewObjectClassEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string TestString
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestString;
                if (OnTestString_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnTestString_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestString != value)
                {
                    var __oldValue = _TestString;
                    var __newValue = value;
                    if (OnTestString_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnTestString_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestString", __oldValue, __newValue);
                    _TestString = __newValue;
                    NotifyPropertyChanged("TestString", __oldValue, __newValue);
                    if (OnTestString_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnTestString_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _TestString;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.ANewObjectClass, string> OnTestString_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.ANewObjectClass, string> OnTestString_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.ANewObjectClass, string> OnTestString_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(ANewObjectClass);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ANewObjectClass)obj;
            var otherImpl = (ANewObjectClassEfImpl)obj;
            var me = (ANewObjectClass)this;

            me.TestString = other.TestString;
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
                    // else
                    new PropertyDescriptorEfImpl<ANewObjectClassEfImpl, string>(
                        lazyCtx,
                        new Guid("e7371fa9-cd18-4cdc-91c3-a73c0984a019"),
                        "TestString",
                        null,
                        obj => obj.TestString,
                        (obj, val) => obj.TestString = val),
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
        [EventBasedMethod("OnToString_ANewObjectClass")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ANewObjectClass != null)
            {
                OnToString_ANewObjectClass(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ANewObjectClass> OnToString_ANewObjectClass;

        [EventBasedMethod("OnPreSave_ANewObjectClass")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ANewObjectClass != null) OnPreSave_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnPreSave_ANewObjectClass;

        [EventBasedMethod("OnPostSave_ANewObjectClass")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ANewObjectClass != null) OnPostSave_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnPostSave_ANewObjectClass;

        [EventBasedMethod("OnCreated_ANewObjectClass")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_ANewObjectClass != null) OnCreated_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnCreated_ANewObjectClass;

        [EventBasedMethod("OnDeleting_ANewObjectClass")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_ANewObjectClass != null) OnDeleting_ANewObjectClass(this);
        }
        public static event ObjectEventHandler<ANewObjectClass> OnDeleting_ANewObjectClass;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(int);
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this._TestString, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._TestString, binStream);
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.ToStream(this._TestString, xml, "TestString", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._TestString, xml, "TestString", "Kistl.App.Test");
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