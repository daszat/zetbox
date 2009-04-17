
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
    [System.Diagnostics.DebuggerDisplay("TestPhoneStruct")]
    public class TestPhoneStruct__Implementation__ : BaseFrozenStruct, TestPhoneStruct, IStruct
    {
    
		public TestPhoneStruct__Implementation__()
		{
        }


        /// <summary>
        /// Enter Area Code
        /// </summary>
        // value type property
        public virtual string AreaCode
        {
            get
            {
                return _AreaCode;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AreaCode != value)
                {
					var __oldValue = _AreaCode;
                    NotifyPropertyChanging("AreaCode", __oldValue, value);
                    _AreaCode = value;
                    NotifyPropertyChanged("AreaCode", __oldValue, value);
                }
            }
        }
        private string _AreaCode;

        /// <summary>
        /// Enter a Number
        /// </summary>
        // value type property
        public virtual string Number
        {
            get
            {
                return _Number;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Number != value)
                {
					var __oldValue = _Number;
                    NotifyPropertyChanging("Number", __oldValue, value);
                    _Number = value;
                    NotifyPropertyChanged("Number", __oldValue, value);
                }
            }
        }
        private string _Number;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TestPhoneStruct));
		}
        internal TestPhoneStruct__Implementation__(int id)
            : base(id)
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
        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}