
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

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestObjClass")]
    public class TestObjClass__Implementation__ : BaseClientDataObject, TestObjClass
    {
    
		public TestObjClass__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// test
        /// </summary>
        // value type property
        public virtual int? MyIntProperty
        {
            get
            {
                return _MyIntProperty;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MyIntProperty != value)
                {
					var __oldValue = _MyIntProperty;
                    NotifyPropertyChanging("MyIntProperty", __oldValue, value);
                    _MyIntProperty = value;
                    NotifyPropertyChanged("MyIntProperty", __oldValue, value);
                }
            }
        }
        private int? _MyIntProperty;

        /// <summary>
        /// testtest
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Kunde ObjectProp
        {
            get
            {
                if (fk_ObjectProp.HasValue)
                    return Context.Find<Kistl.App.Projekte.Kunde>(fk_ObjectProp.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_ObjectProp == null)
					return;
                else if (value != null && value.ID == _fk_ObjectProp)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = ObjectProp;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ObjectProp", oldValue, value);
                
				// next, set the local reference
                _fk_ObjectProp = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ObjectProp", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ObjectProp
        {
            get
            {
                return _fk_ObjectProp;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ObjectProp != value)
                {
					var __oldValue = _fk_ObjectProp;
                    NotifyPropertyChanging("ObjectProp", __oldValue, value);
                    _fk_ObjectProp = value;
                    NotifyPropertyChanged("ObjectProp", __oldValue, value);
                }
            }
        }
        private int? _fk_ObjectProp;

        /// <summary>
        /// String Property
        /// </summary>
        // value type property
        public virtual string StringProp
        {
            get
            {
                return _StringProp;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_StringProp != value)
                {
					var __oldValue = _StringProp;
                    NotifyPropertyChanging("StringProp", __oldValue, value);
                    _StringProp = value;
                    NotifyPropertyChanged("StringProp", __oldValue, value);
                }
            }
        }
        private string _StringProp;

        /// <summary>
        /// Test Enumeration Property
        /// </summary>
        // enumeration property
        public virtual Kistl.App.Test.TestEnum TestEnumProp
        {
            get
            {
                return _TestEnumProp;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TestEnumProp != value)
                {
					var __oldValue = _TestEnumProp;
                    NotifyPropertyChanging("TestEnumProp", __oldValue, value);
                    _TestEnumProp = value;
                    NotifyPropertyChanged("TestEnumProp", __oldValue, value);
                }
            }
        }
        private Kistl.App.Test.TestEnum _TestEnumProp;

        /// <summary>
        /// testmethod
        /// </summary>

		public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod) 
		{
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
				OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
			}
			else
			{
                throw new NotImplementedException("No handler registered on TestObjClass.TestMethod");
			}
        }
		public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
		public event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TestObjClass));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (TestObjClass)obj;
			var otherImpl = (TestObjClass__Implementation__)obj;
			var me = (TestObjClass)this;

			me.MyIntProperty = other.MyIntProperty;
			me.StringProp = other.StringProp;
			me.TestEnumProp = other.TestEnumProp;
			this.fk_ObjectProp = otherImpl.fk_ObjectProp;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestObjClass != null)
            {
                OnToString_TestObjClass(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "ObjectProp":
                    fk_ObjectProp = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._MyIntProperty, binStream);
            BinarySerializer.ToStream(this._fk_ObjectProp, binStream);
            BinarySerializer.ToStream(this._StringProp, binStream);
            BinarySerializer.ToStream((int)((TestObjClass)this).TestEnumProp, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._MyIntProperty, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectProp, binStream);
            BinarySerializer.FromStream(out this._StringProp, binStream);
            BinarySerializer.FromStreamConverter(v => ((TestObjClass)this).TestEnumProp = (Kistl.App.Test.TestEnum)v, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._MyIntProperty, xml, "MyIntProperty", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_ObjectProp, xml, "fk_ObjectProp", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._StringProp, xml, "StringProp", "http://dasz.at/Kistl");
            // TODO: Add XML Serializer here
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}