
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
    [EdmEntityType(NamespaceName="Model", Name="IntegerRangeConstraint")]
    [System.Diagnostics.DebuggerDisplay("IntegerRangeConstraint")]
    public class IntegerRangeConstraint__Implementation__ : Kistl.App.Base.Constraint__Implementation__, IntegerRangeConstraint
    {
    
		public IntegerRangeConstraint__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The biggest value accepted by this constraint
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int Max
        {
            get
            {
                return _Max;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Max != value)
                {
					var __oldValue = _Max;
                    NotifyPropertyChanging("Max", __oldValue, value);
                    _Max = value;
                    NotifyPropertyChanged("Max", __oldValue, value);
                }
            }
        }
        private int _Max;

        /// <summary>
        /// The smallest value accepted by this constraint
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int Min
        {
            get
            {
                return _Min;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Min != value)
                {
					var __oldValue = _Min;
                    NotifyPropertyChanging("Min", __oldValue, value);
                    _Min = value;
                    NotifyPropertyChanged("Min", __oldValue, value);
                }
            }
        }
        private int _Min;

        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IntegerRangeConstraint != null)
            {
                OnGetErrorText_IntegerRangeConstraint(this, e, constrainedValue, constrainedObject);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedValue, constrainedObject);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<IntegerRangeConstraint> OnGetErrorText_IntegerRangeConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IntegerRangeConstraint != null)
            {
                OnIsValid_IntegerRangeConstraint(this, e, constrainedValue, constrainedObj);
            }
            else
            {
                e.Result = base.IsValid(constrainedValue, constrainedObj);
            }
            return e.Result;
        }
		public event IsValid_Handler<IntegerRangeConstraint> OnIsValid_IntegerRangeConstraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(IntegerRangeConstraint));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (IntegerRangeConstraint)obj;
			var otherImpl = (IntegerRangeConstraint__Implementation__)obj;
			var me = (IntegerRangeConstraint)this;

			me.Max = other.Max;
			me.Min = other.Min;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IntegerRangeConstraint != null)
            {
                OnToString_IntegerRangeConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IntegerRangeConstraint> OnToString_IntegerRangeConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IntegerRangeConstraint != null) OnPreSave_IntegerRangeConstraint(this);
        }
        public event ObjectEventHandler<IntegerRangeConstraint> OnPreSave_IntegerRangeConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IntegerRangeConstraint != null) OnPostSave_IntegerRangeConstraint(this);
        }
        public event ObjectEventHandler<IntegerRangeConstraint> OnPostSave_IntegerRangeConstraint;



		public override void ReloadReferences()
		{
			base.ReloadReferences();
			
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Max, binStream);
            BinarySerializer.ToStream(this._Min, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Max, binStream);
            BinarySerializer.FromStream(out this._Min, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Max, xml, "Max", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Min, xml, "Min", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Max, xml, "Max", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Min, xml, "Min", "http://dasz.at/Kistl");
        }

#endregion

    }


}