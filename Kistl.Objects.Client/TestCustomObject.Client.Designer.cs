
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
    [System.Diagnostics.DebuggerDisplay("TestCustomObject")]
    public class TestCustomObject__Implementation__ : BaseClientDataObject, TestCustomObject
    {
    
		public TestCustomObject__Implementation__()
		{
            {
                _PhoneNumberMobile = new Kistl.App.Test.TestPhoneStruct__Implementation__(this, "PhoneNumberMobile");
                _PhoneNumberOffice = new Kistl.App.Test.TestPhoneStruct__Implementation__(this, "PhoneNumberOffice");
            }
        }


        /// <summary>
        /// Happy Birthday!
        /// </summary>
        // value type property
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
            get { return _PhoneNumberMobile; }
        }
        
        /// <summary>backing store for PhoneNumberMobile</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberMobileStore;
        
        /// <summary>backing property for PhoneNumberMobile, takes care of attaching/detaching the values</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberMobile {
            get { return _PhoneNumberMobileStore; }
            set {
				if (_PhoneNumberMobileStore != null)
				{
	                _PhoneNumberMobileStore.DetachFromObject(this, "PhoneNumberMobile");
				}
                _PhoneNumberMobileStore = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
                _PhoneNumberMobileStore.AttachToObject(this, "PhoneNumberMobile");
            }
		}
  
        /// <summary>
        /// Office Phone Number
        /// </summary>
        // struct property
        // implement the user-visible interface
        public Kistl.App.Test.TestPhoneStruct PhoneNumberOffice
        {
            get { return _PhoneNumberOffice; }
        }
        
        /// <summary>backing store for PhoneNumberOffice</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberOfficeStore;
        
        /// <summary>backing property for PhoneNumberOffice, takes care of attaching/detaching the values</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberOffice {
            get { return _PhoneNumberOfficeStore; }
            set {
				if (_PhoneNumberOfficeStore != null)
				{
	                _PhoneNumberOfficeStore.DetachFromObject(this, "PhoneNumberOffice");
				}
                _PhoneNumberOfficeStore = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
                _PhoneNumberOfficeStore.AttachToObject(this, "PhoneNumberOffice");
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Birthday, binStream);
            BinarySerializer.ToStream(this._PersonName, binStream);
			BinarySerializer.ToStream(this._PhoneNumberMobile, binStream);
			BinarySerializer.ToStream(this._PhoneNumberOffice, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Birthday, binStream);
            BinarySerializer.FromStream(out this._PersonName, binStream);
			{
				// trick compiler into generating correct temporary variable
				var tmp = this._PhoneNumberMobile;
				BinarySerializer.FromStream(out tmp, binStream);
				// use setter to de-/attach everything correctly
	            this._PhoneNumberMobile = tmp;
	        }
			{
				// trick compiler into generating correct temporary variable
				var tmp = this._PhoneNumberOffice;
				BinarySerializer.FromStream(out tmp, binStream);
				// use setter to de-/attach everything correctly
	            this._PhoneNumberOffice = tmp;
	        }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Birthday, xml, "Birthday", "Kistl.App.Test");
            XmlStreamer.ToStream(this._PersonName, xml, "PersonName", "Kistl.App.Test");
			// TODO: Add XML Serializer here
			// TODO: Add XML Serializer here
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Birthday, xml, "Birthday", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._PersonName, xml, "PersonName", "Kistl.App.Test");
			// TODO: Add XML Serializer here
			// TODO: Add XML Serializer here
        }

#endregion

    }


}