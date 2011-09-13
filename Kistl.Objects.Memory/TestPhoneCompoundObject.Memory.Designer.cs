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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestPhoneCompoundObject")]
    public class TestPhoneCompoundObjectMemoryImpl : CompoundObjectDefaultImpl, ICompoundObject, TestPhoneCompoundObject
    {
        [Obsolete]
        public TestPhoneCompoundObjectMemoryImpl()
            : base(null)
        {
        }

        public TestPhoneCompoundObjectMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
        public TestPhoneCompoundObjectMemoryImpl(IPersistenceObject parent, string property) : this(false, parent, property) {}
        public TestPhoneCompoundObjectMemoryImpl(bool ignore, IPersistenceObject parent, string property)
            : base(null) // TODO: pass parent's lazyCtx
        {
            AttachToObject(parent, property);
        }

        /// <summary>
        /// Enter Area Code
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string AreaCode
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _AreaCode;
                if (OnAreaCode_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnAreaCode_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_AreaCode != value)
                {
                    var __oldValue = _AreaCode;
                    var __newValue = value;
                    if (OnAreaCode_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnAreaCode_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("AreaCode", __oldValue, __newValue);
                    _AreaCode = __newValue;
                    NotifyPropertyChanged("AreaCode", __oldValue, __newValue);
                    if (OnAreaCode_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnAreaCode_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _AreaCode;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnAreaCode_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnAreaCode_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnAreaCode_PostSetter;

        /// <summary>
        /// Enter a Number
        /// </summary>
        // value type property
        // BEGIN Kistl.Generator.Templates.Properties.NotifyingDataProperty
        public string Number
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Number;
                if (OnNumber_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnNumber_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Number != value)
                {
                    var __oldValue = _Number;
                    var __newValue = value;
                    if (OnNumber_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnNumber_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Number", __oldValue, __newValue);
                    _Number = __newValue;
                    NotifyPropertyChanged("Number", __oldValue, __newValue);
                    if (OnNumber_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnNumber_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Number;
        // END Kistl.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnNumber_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnNumber_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnNumber_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(TestPhoneCompoundObject);
        }

        public override void ApplyChangesFrom(ICompoundObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestPhoneCompoundObject)obj;
            var otherImpl = (TestPhoneCompoundObjectMemoryImpl)obj;
            var me = (TestPhoneCompoundObject)this;

            me.AreaCode = other.AreaCode;
            me.Number = other.Number;
        }
        #region Kistl.Generator.Templates.CompoundObjects.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_TestPhoneCompoundObject")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestPhoneCompoundObject != null)
            {
                OnToString_TestPhoneCompoundObject(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TestPhoneCompoundObject> OnToString_TestPhoneCompoundObject;

        #endregion // Kistl.Generator.Templates.CompoundObjects.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this._AreaCode, binStream);
            BinarySerializer.ToStream(this._Number, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._AreaCode, binStream);
            BinarySerializer.FromStream(out this._Number, binStream);
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
            XmlStreamer.ToStream(this._AreaCode, xml, "AreaCode", "Kistl.App.Test");
            XmlStreamer.ToStream(this._Number, xml, "Number", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._AreaCode, xml, "AreaCode", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._Number, xml, "Number", "Kistl.App.Test");
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