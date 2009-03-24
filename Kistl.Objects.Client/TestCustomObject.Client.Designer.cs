
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
                    NotifyPropertyChanging("Birthday");
                    _Birthday = value;
                    NotifyPropertyChanged("Birthday");
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
                    NotifyPropertyChanging("PersonName");
                    _PersonName = value;
                    NotifyPropertyChanged("PersonName");
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
            set { _PhoneNumberMobile = (Kistl.App.Test.TestPhoneStruct__Implementation__)value; }
        }
        
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberMobile;
  
        /// <summary>
        /// Office Phone Number
        /// </summary>
        // struct property
        // implement the user-visible interface
        public Kistl.App.Test.TestPhoneStruct PhoneNumberOffice
        {
            get { return _PhoneNumberOffice; }
            set { _PhoneNumberOffice = (Kistl.App.Test.TestPhoneStruct__Implementation__)value; }
        }
        
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberOffice;
  
		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TestCustomObject));
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
            BinarySerializer.FromStream(out this._PhoneNumberMobile, binStream);
            BinarySerializer.FromStream(out this._PhoneNumberOffice, binStream);
        }

#endregion

    }


}