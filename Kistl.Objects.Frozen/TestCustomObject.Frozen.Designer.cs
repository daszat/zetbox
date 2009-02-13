
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestCustomObject")]
    public class TestCustomObject__Implementation__ : BaseFrozenDataObject, TestCustomObject
    {


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
                    NotifyPropertyChanged("PersonName");;
                }
            }
        }
        private string _PersonName;

        /// <summary>
        /// Mobile Phone Number
        /// </summary>
        // struct property
        public virtual Kistl.App.Test.TestPhoneStruct PhoneNumberMobile
        {
            get
            {
                return _PhoneNumberMobile;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PhoneNumberMobile != value)
                {
                    NotifyPropertyChanging("PhoneNumberMobile");
                    _PhoneNumberMobile = value;
                    NotifyPropertyChanged("PhoneNumberMobile");;
                }
            }
        }
        private Kistl.App.Test.TestPhoneStruct _PhoneNumberMobile;

        /// <summary>
        /// Office Phone Number
        /// </summary>
        // struct property
        public virtual Kistl.App.Test.TestPhoneStruct PhoneNumberOffice
        {
            get
            {
                return _PhoneNumberOffice;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PhoneNumberOffice != value)
                {
                    NotifyPropertyChanging("PhoneNumberOffice");
                    _PhoneNumberOffice = value;
                    NotifyPropertyChanged("PhoneNumberOffice");;
                }
            }
        }
        private Kistl.App.Test.TestPhoneStruct _PhoneNumberOffice;

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
                    NotifyPropertyChanged("Birthday");;
                }
            }
        }
        private DateTime? _Birthday;

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


        internal TestCustomObject__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}