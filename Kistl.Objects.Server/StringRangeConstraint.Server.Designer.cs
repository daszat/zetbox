
namespace Kistl.App.Base
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
    [EdmEntityType(NamespaceName="Model", Name="StringRangeConstraint")]
    [System.Diagnostics.DebuggerDisplay("StringRangeConstraint")]
    public class StringRangeConstraint__Implementation__ : Kistl.App.Base.Constraint__Implementation__, StringRangeConstraint
    {


        /// <summary>
        /// The maximal length of this StringProperty
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MaxLength != value)
                {
                    NotifyPropertyChanging("MaxLength");
                    _MaxLength = value;
                    NotifyPropertyChanged("MaxLength");
                }
            }
        }
        private int _MaxLength;

        /// <summary>
        /// The minimal length of this StringProperty
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int MinLength
        {
            get
            {
                return _MinLength;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MinLength != value)
                {
                    NotifyPropertyChanging("MinLength");
                    _MinLength = value;
                    NotifyPropertyChanged("MinLength");
                }
            }
        }
        private int _MinLength;

        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_StringRangeConstraint != null)
            {
                OnGetErrorText_StringRangeConstraint(this, e, constrainedValue, constrainedObject);
            };
            return e.Result;
        }
		public event GetErrorText_Handler<StringRangeConstraint> OnGetErrorText_StringRangeConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_StringRangeConstraint != null)
            {
                OnIsValid_StringRangeConstraint(this, e, constrainedValue, constrainedObj);
            };
            return e.Result;
        }
		public event IsValid_Handler<StringRangeConstraint> OnIsValid_StringRangeConstraint;



		public override Type GetInterfaceType()
		{
			return typeof(StringRangeConstraint);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StringRangeConstraint != null)
            {
                OnToString_StringRangeConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StringRangeConstraint> OnToString_StringRangeConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StringRangeConstraint != null) OnPreSave_StringRangeConstraint(this);
        }
        public event ObjectEventHandler<StringRangeConstraint> OnPreSave_StringRangeConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StringRangeConstraint != null) OnPostSave_StringRangeConstraint(this);
        }
        public event ObjectEventHandler<StringRangeConstraint> OnPostSave_StringRangeConstraint;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._MaxLength, binStream);
            BinarySerializer.ToStream(this._MinLength, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._MaxLength, binStream);
            BinarySerializer.FromStream(out this._MinLength, binStream);
        }

#endregion

    }


}