
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
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
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
            get
            {
                return _PhoneNumberMobile;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (!object.Equals(_PhoneNumberMobile, value))
                {
                    NotifyPropertyChanging("PhoneNumberMobile");
                    _PhoneNumberMobile = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
                    NotifyPropertyChanged("PhoneNumberMobile");
                }
            }
        }
        
        /// <summary>backing store for PhoneNumberMobile</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberMobile;
        
        /// <summary>EF sees only this property, for PhoneNumberMobile</summary>
        [XmlIgnore()]
        [EdmComplexProperty()]
        public Kistl.App.Test.TestPhoneStruct__Implementation__ PhoneNumberMobile__Implementation__
        {
            get
            {
                if (_PhoneNumberMobile == null)
                    return Kistl.App.Test.TestPhoneStruct__Implementation__.NoValue;
                return _PhoneNumberMobile;
            }
            set
            {
                if(!Object.Equals(Kistl.App.Test.TestPhoneStruct__Implementation__.NoValue, value))
                {
                    // use property to trigger notify
                    PhoneNumberMobile = value;
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
                return _PhoneNumberOffice;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (!object.Equals(_PhoneNumberOffice, value))
                {
                    NotifyPropertyChanging("PhoneNumberOffice");
                    _PhoneNumberOffice = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
                    NotifyPropertyChanged("PhoneNumberOffice");
                }
            }
        }
        
        /// <summary>backing store for PhoneNumberOffice</summary>
        private Kistl.App.Test.TestPhoneStruct__Implementation__ _PhoneNumberOffice;
        
        /// <summary>EF sees only this property, for PhoneNumberOffice</summary>
        [XmlIgnore()]
        [EdmComplexProperty()]
        public Kistl.App.Test.TestPhoneStruct__Implementation__ PhoneNumberOffice__Implementation__
        {
            get
            {
                if (_PhoneNumberOffice == null)
                    return Kistl.App.Test.TestPhoneStruct__Implementation__.NoValue;
                return _PhoneNumberOffice;
            }
            set
            {
                if(!Object.Equals(Kistl.App.Test.TestPhoneStruct__Implementation__.NoValue, value))
                {
                    // use property to trigger notify
                    PhoneNumberOffice = value;
                }
            }
        }


  
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Birthday, binStream);
            BinarySerializer.ToStream(this._PersonName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Birthday, binStream);
            BinarySerializer.FromStream(out this._PersonName, binStream);
        }

#endregion

    }


}