
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
                return _PhoneNumberMobile;
            }
            set
            {
                if (value == null)
					throw new ArgumentNullException("value");
                
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (!object.Equals(_PhoneNumberMobile, value))
                {
                    NotifyPropertyChanging("PhoneNumberMobile", "PhoneNumberMobile__Implementation__");
                    if (_PhoneNumberMobile != null)
                    {
						_PhoneNumberMobile.DetachFromObject(this, "PhoneNumberMobile");
					}
                    _PhoneNumberMobile = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
					_PhoneNumberMobile.AttachToObject(this, "PhoneNumberMobile");
                    NotifyPropertyChanged("PhoneNumberMobile", "PhoneNumberMobile__Implementation__");
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
                return _PhoneNumberOffice;
            }
            set
            {
                if (value == null)
					throw new ArgumentNullException("value");
                
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (!object.Equals(_PhoneNumberOffice, value))
                {
                    NotifyPropertyChanging("PhoneNumberOffice", "PhoneNumberOffice__Implementation__");
                    if (_PhoneNumberOffice != null)
                    {
						_PhoneNumberOffice.DetachFromObject(this, "PhoneNumberOffice");
					}
                    _PhoneNumberOffice = (Kistl.App.Test.TestPhoneStruct__Implementation__)value;
					_PhoneNumberOffice.AttachToObject(this, "PhoneNumberOffice");
                    NotifyPropertyChanged("PhoneNumberOffice", "PhoneNumberOffice__Implementation__");
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