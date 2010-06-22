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


    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestPhoneCompoundObject")]
    public class TestPhoneCompoundObject__Implementation__Memory : BaseCompoundObject, ICompoundObject, TestPhoneCompoundObject
    {
        [Obsolete]
        public TestPhoneCompoundObject__Implementation__Memory()
            : base(null)
        {
        }

        public TestPhoneCompoundObject__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
        }


        /// <summary>
        /// Enter Area Code
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string AreaCode
        {
            get
            {
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
                    if(OnAreaCode_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnAreaCode_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("AreaCode", __oldValue, __newValue);
                    _AreaCode = __newValue;
                    NotifyPropertyChanged("AreaCode", __oldValue, __newValue);
                    if(OnAreaCode_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnAreaCode_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _AreaCode;
		public static event PropertyGetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnAreaCode_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnAreaCode_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnAreaCode_PostSetter;

        /// <summary>
        /// Enter a Number
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Number
        {
            get
            {
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
                    if(OnNumber_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnNumber_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Number", __oldValue, __newValue);
                    _Number = __newValue;
                    NotifyPropertyChanged("Number", __oldValue, __newValue);
                    if(OnNumber_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnNumber_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Number;
		public static event PropertyGetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnNumber_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnNumber_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.TestPhoneCompoundObject, string> OnNumber_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(TestPhoneCompoundObject);
        }
        public TestPhoneCompoundObject__Implementation__Memory(IPersistenceObject parent, string property)
            : base(null) // TODO: pass parent's lazyCtx
        {
            AttachToObject(parent, property);
        }

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(this._AreaCode, binStream);
            BinarySerializer.ToStream(this._Number, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AreaCode, binStream);
            BinarySerializer.FromStream(out this._Number, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._AreaCode, xml, "AreaCode", "Kistl.App.Test");
            XmlStreamer.ToStream(this._Number, xml, "Number", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._AreaCode, xml, "AreaCode", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._Number, xml, "Number", "Kistl.App.Test");
        }

#endregion

    }


}