
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
    
		public StringRangeConstraint__Implementation__()
		{
            {
            }
        }


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
					var __oldValue = _MaxLength;
                    NotifyPropertyChanging("MaxLength", __oldValue, value);
                    _MaxLength = value;
                    NotifyPropertyChanged("MaxLength", __oldValue, value);
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
					var __oldValue = _MinLength;
                    NotifyPropertyChanging("MinLength", __oldValue, value);
                    _MinLength = value;
                    NotifyPropertyChanged("MinLength", __oldValue, value);
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
            }
            else
            {
                e.Result = base.GetErrorText(constrainedValue, constrainedObject);
            }
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
            }
            else
            {
                e.Result = base.IsValid(constrainedValue, constrainedObj);
            }
            return e.Result;
        }
		public event IsValid_Handler<StringRangeConstraint> OnIsValid_StringRangeConstraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(StringRangeConstraint));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (StringRangeConstraint)obj;
			var otherImpl = (StringRangeConstraint__Implementation__)obj;
			var me = (StringRangeConstraint)this;

			me.MaxLength = other.MaxLength;
			me.MinLength = other.MinLength;
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



		public override void ReloadReferences()
		{
			base.ReloadReferences();
			
			// fix direct object references
		}
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

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._MaxLength, xml, "MaxLength", "Kistl.App.Base");
            XmlStreamer.ToStream(this._MinLength, xml, "MinLength", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._MaxLength, xml, "MaxLength", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._MinLength, xml, "MinLength", "Kistl.App.Base");
        }

#endregion

    }


}