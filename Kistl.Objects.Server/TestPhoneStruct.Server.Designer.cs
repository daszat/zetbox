
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
    [EdmComplexType(NamespaceName="Model", Name="TestPhoneStruct")]
    [System.Diagnostics.DebuggerDisplay("TestPhoneStruct")]
    public class TestPhoneStruct__Implementation__ : BaseServerStructObject_EntityFramework, TestPhoneStruct, IStruct
    {
    
		public TestPhoneStruct__Implementation__()
		{
        }


        /// <summary>
        /// Enter Area Code
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                    NotifyPropertyChanging("AreaCode");
                    _AreaCode = value;
                    NotifyPropertyChanged("AreaCode");
                }
            }
        }
        private string _AreaCode;

        /// <summary>
        /// Enter a Number
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                    NotifyPropertyChanging("Number");
                    _Number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }
        private string _Number;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TestPhoneStruct));
		}
        public TestPhoneStruct__Implementation__(IPersistenceObject parent, string property)
        {
            AttachToObject(parent, property);
        }

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AreaCode, binStream);
            BinarySerializer.ToStream(this._Number, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AreaCode, binStream);
            BinarySerializer.FromStream(out this._Number, binStream);
        }

#endregion

    }


}