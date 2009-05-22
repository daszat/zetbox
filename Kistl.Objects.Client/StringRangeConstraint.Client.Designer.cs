// <autogenerated/>


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

    using Kistl.API.Client;
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// 
    /// </summary>
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

		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_StringRangeConstraint != null)
            {
                OnGetErrorText_StringRangeConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<StringRangeConstraint> OnGetErrorText_StringRangeConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_StringRangeConstraint != null)
            {
                OnIsValid_StringRangeConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "MaxLength":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(172).Constraints
						.Where(c => !c.IsValid(this, this.MaxLength))
						.Select(c => c.GetErrorText(this, this.MaxLength))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MinLength":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(173).Constraints
						.Where(c => !c.IsValid(this, this.MinLength))
						.Select(c => c.GetErrorText(this, this.MinLength))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

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
            BinarySerializer.ToStream(this._MaxLength, binStream);
            BinarySerializer.ToStream(this._MinLength, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._MaxLength, binStream);
            BinarySerializer.FromStream(out this._MinLength, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._MaxLength, xml, "MaxLength", "Kistl.App.Base");
            XmlStreamer.ToStream(this._MinLength, xml, "MinLength", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._MaxLength, xml, "MaxLength", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._MinLength, xml, "MinLength", "Kistl.App.Base");
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
            base.Export(xml, modules);
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._MaxLength, xml, "MaxLength", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._MinLength, xml, "MinLength", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
			
            base.MergeImport(xml);
            XmlStreamer.FromStream(ref this._MaxLength, xml, "MaxLength", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._MinLength, xml, "MinLength", "Kistl.App.Base");
        }

#endregion

    }


}