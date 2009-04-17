
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="TestCustomObject")]
    [System.Diagnostics.DebuggerDisplay("TestCustomObject")]
    public class TestCustomObject__Implementation__ : BaseServerDataObject_EntityFramework, TestCustomObject
    {
    
		public TestCustomObject__Implementation__()
		{
            {
                _PhoneNumberMobile = new Kistl.App.Test.TestPhoneStruct__Implementation__(this, "PhoneNumberMobile");
                _PhoneNumberOffice = new Kistl.App.Test.TestPhoneStruct__Implementation__(this, "PhoneNumberOffice");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Happy Birthday!
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime? Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Birthday != value)
                {
					var __oldValue = _Birthday;
                    NotifyPropertyChanging("Birthday", __oldValue, value);
                    _Birthday = value;
                    NotifyPropertyChanged("Birthday", __oldValue, value);
                }
            }
        }
        private DateTime? _Birthday;

        /// <summary>
        /// Persons Name
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string PersonName
        {
            get
            {
                return _PersonName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PersonName != value)
                {
					var __oldValue = _PersonName;
                    NotifyPropertyChanging("PersonName", __oldValue, value);
                    _PersonName = value;
                    NotifyPropertyChanged("PersonName", __oldValue, value);
                }
            }
        }
        private string _PersonName;

        /// <summary>
        /// Mobile Phone Number
        /// </summary>
        // struct property
        // implement the user-visible interface
        public Kistl.App.Test.TestPhoneStruct PhoneNumberMobile
        {
            get
            {
                return PhoneNumberMobile__Implementation__;
            }
        }
        
        /// <summary>backing store for PhoneNumberMobile</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberMobile;
        
        /// <summary>backing property for PhoneNumberMobile, takes care of attaching/detaching the values, mapped via EF</summary>
        [XmlIgnore()]
        [EdmComplexProperty()]
        public Kistl.App.Test.TestPhoneStruct__Implementation__ PhoneNumberMobile__Implementation__
        {
            get
            {
                return _PhoneNumberMobile;
            }
            set
            {
                if (value == null)
					throw new ArgumentNullException("value");
                
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (!object.Equals(_PhoneNumberMobile, value))
                {
					var __oldValue = _PhoneNumberMobile;
                    NotifyPropertyChanging("PhoneNumberMobile", "PhoneNumberMobile__Implementation__", __oldValue, value);
                    if (_PhoneNumberMobile != null)
                    {
						_PhoneNumberMobile.DetachFromObject(this, "PhoneNumberMobile");
					}
                    _PhoneNumberMobile = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
					_PhoneNumberMobile.AttachToObject(this, "PhoneNumberMobile");
                    NotifyPropertyChanged("PhoneNumberMobile", "PhoneNumberMobile__Implementation__", __oldValue, value);
                }
            }
        }


  
        /// <summary>
        /// Office Phone Number
        /// </summary>
        // struct property
        // implement the user-visible interface
        public Kistl.App.Test.TestPhoneStruct PhoneNumberOffice
        {
            get
            {
                return PhoneNumberOffice__Implementation__;
            }
        }
        
        /// <summary>backing store for PhoneNumberOffice</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberOffice;
        
        /// <summary>backing property for PhoneNumberOffice, takes care of attaching/detaching the values, mapped via EF</summary>
        [XmlIgnore()]
        [EdmComplexProperty()]
        public Kistl.App.Test.TestPhoneStruct__Implementation__ PhoneNumberOffice__Implementation__
        {
            get
            {
                return _PhoneNumberOffice;
            }
            set
            {
                if (value == null)
					throw new ArgumentNullException("value");
                
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (!object.Equals(_PhoneNumberOffice, value))
                {
					var __oldValue = _PhoneNumberOffice;
                    NotifyPropertyChanging("PhoneNumberOffice", "PhoneNumberOffice__Implementation__", __oldValue, value);
                    if (_PhoneNumberOffice != null)
                    {
						_PhoneNumberOffice.DetachFromObject(this, "PhoneNumberOffice");
					}
                    _PhoneNumberOffice = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
					_PhoneNumberOffice.AttachToObject(this, "PhoneNumberOffice");
                    NotifyPropertyChanged("PhoneNumberOffice", "PhoneNumberOffice__Implementation__", __oldValue, value);
                }
            }
        }


  
		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TestCustomObject));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (TestCustomObject)obj;
			var otherImpl = (TestCustomObject__Implementation__)obj;
			var me = (TestCustomObject)this;

			me.Birthday = other.Birthday;
			me.PersonName = other.PersonName;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestCustomObject != null)
            {
                OnToString_TestCustomObject(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TestCustomObject> OnToString_TestCustomObject;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestCustomObject != null) OnPreSave_TestCustomObject(this);
        }
        public event ObjectEventHandler<TestCustomObject> OnPreSave_TestCustomObject;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestCustomObject != null) OnPostSave_TestCustomObject(this);
        }
        public event ObjectEventHandler<TestCustomObject> OnPostSave_TestCustomObject;



		public override void ReloadReferences()
		{
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Birthday, binStream);
            BinarySerializer.ToStream(this._PersonName, binStream);
			BinarySerializer.ToStream(this.PhoneNumberMobile__Implementation__, binStream);
			BinarySerializer.ToStream(this.PhoneNumberOffice__Implementation__, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Birthday, binStream);
            BinarySerializer.FromStream(out this._PersonName, binStream);
			{
				// trick compiler into generating correct temporary variable
				var tmp = this.PhoneNumberMobile__Implementation__;
				BinarySerializer.FromStream(out tmp, binStream);
				// use setter to de-/attach everything correctly
	            this.PhoneNumberMobile__Implementation__ = tmp;
	        }
			{
				// trick compiler into generating correct temporary variable
				var tmp = this.PhoneNumberOffice__Implementation__;
				BinarySerializer.FromStream(out tmp, binStream);
				// use setter to de-/attach everything correctly
	            this.PhoneNumberOffice__Implementation__ = tmp;
	        }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Birthday, xml, "Birthday", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PersonName, xml, "PersonName", "http://dasz.at/Kistl");
			// TODO: Add XML Serializer here
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