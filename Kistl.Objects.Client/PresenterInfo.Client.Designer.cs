// <autogenerated/>


namespace Kistl.App.GUI
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
    [System.Diagnostics.DebuggerDisplay("PresenterInfo")]
    public class PresenterInfo__Implementation__ : BaseClientDataObject_ClientObjects, PresenterInfo
    {
    
		public PresenterInfo__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// which controls are handled by this Presenter
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType ControlType
        {
            get
            {
                return _ControlType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ControlType != value)
                {
					var __oldValue = _ControlType;
                    NotifyPropertyChanging("ControlType", __oldValue, value);
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _ControlType;

        /// <summary>
        /// The Assembly of the Data Type
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly DataAssembly
        {
            get
            {
                if (_fk_DataAssembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(_fk_DataAssembly.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_DataAssembly == null)
					return;
                else if (value != null && value.ID == _fk_DataAssembly)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = DataAssembly;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DataAssembly", oldValue, value);
                
				// next, set the local reference
                _fk_DataAssembly = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DataAssembly", oldValue, value);
            }
        }
        
        private int? _fk_DataAssembly;

        /// <summary>
        /// The CLR namespace and class name of the Data Type
        /// </summary>
        // value type property
        public virtual string DataTypeName
        {
            get
            {
                return _DataTypeName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DataTypeName != value)
                {
					var __oldValue = _DataTypeName;
                    NotifyPropertyChanging("DataTypeName", __oldValue, value);
                    _DataTypeName = value;
                    NotifyPropertyChanged("DataTypeName", __oldValue, value);
                }
            }
        }
        private string _DataTypeName;

        /// <summary>
        /// Where to find the implementation of the Presenter
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly PresenterAssembly
        {
            get
            {
                if (_fk_PresenterAssembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(_fk_PresenterAssembly.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_PresenterAssembly == null)
					return;
                else if (value != null && value.ID == _fk_PresenterAssembly)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = PresenterAssembly;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("PresenterAssembly", oldValue, value);
                
				// next, set the local reference
                _fk_PresenterAssembly = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("PresenterAssembly", oldValue, value);
            }
        }
        
        private int? _fk_PresenterAssembly;

        /// <summary>
        /// The CLR namespace and class name of the Presenter
        /// </summary>
        // value type property
        public virtual string PresenterTypeName
        {
            get
            {
                return _PresenterTypeName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresenterTypeName != value)
                {
					var __oldValue = _PresenterTypeName;
                    NotifyPropertyChanging("PresenterTypeName", __oldValue, value);
                    _PresenterTypeName = value;
                    NotifyPropertyChanged("PresenterTypeName", __oldValue, value);
                }
            }
        }
        private string _PresenterTypeName;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresenterInfo));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PresenterInfo)obj;
			var otherImpl = (PresenterInfo__Implementation__)obj;
			var me = (PresenterInfo)this;

			me.ControlType = other.ControlType;
			me.DataTypeName = other.DataTypeName;
			me.PresenterTypeName = other.PresenterTypeName;
			this._fk_DataAssembly = otherImpl._fk_DataAssembly;
			this._fk_PresenterAssembly = otherImpl._fk_PresenterAssembly;
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
            if (OnToString_PresenterInfo != null)
            {
                OnToString_PresenterInfo(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresenterInfo> OnToString_PresenterInfo;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresenterInfo != null) OnPreSave_PresenterInfo(this);
        }
        public event ObjectEventHandler<PresenterInfo> OnPreSave_PresenterInfo;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresenterInfo != null) OnPostSave_PresenterInfo(this);
        }
        public event ObjectEventHandler<PresenterInfo> OnPostSave_PresenterInfo;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ControlType":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(137).Constraints
						.Where(c => !c.IsValid(this, this.ControlType))
						.Select(c => c.GetErrorText(this, this.ControlType))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DataAssembly":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(147).Constraints
						.Where(c => !c.IsValid(this, this.DataAssembly))
						.Select(c => c.GetErrorText(this, this.DataAssembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DataTypeName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(148).Constraints
						.Where(c => !c.IsValid(this, this.DataTypeName))
						.Select(c => c.GetErrorText(this, this.DataTypeName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresenterAssembly":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(138).Constraints
						.Where(c => !c.IsValid(this, this.PresenterAssembly))
						.Select(c => c.GetErrorText(this, this.PresenterAssembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresenterTypeName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(139).Constraints
						.Where(c => !c.IsValid(this, this.PresenterTypeName))
						.Select(c => c.GetErrorText(this, this.PresenterTypeName))
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
                case "DataAssembly":
                    _fk_DataAssembly = id;
                    break;
                case "PresenterAssembly":
                    _fk_PresenterAssembly = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
			
            base.ToStream(binStream);
            BinarySerializer.ToStream((int)((PresenterInfo)this).ControlType, binStream);
            BinarySerializer.ToStream(this._fk_DataAssembly, binStream);
            BinarySerializer.ToStream(this._DataTypeName, binStream);
            BinarySerializer.ToStream(this._fk_PresenterAssembly, binStream);
            BinarySerializer.ToStream(this._PresenterTypeName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => ((PresenterInfo)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._fk_DataAssembly, binStream);
            BinarySerializer.FromStream(out this._DataTypeName, binStream);
            BinarySerializer.FromStream(out this._fk_PresenterAssembly, binStream);
            BinarySerializer.FromStream(out this._PresenterTypeName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream((int)this.ControlType, xml, "ControlType", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._fk_DataAssembly, xml, "DataAssembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._DataTypeName, xml, "DataTypeName", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._fk_PresenterAssembly, xml, "PresenterAssembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PresenterTypeName, xml, "PresenterTypeName", "Kistl.App.GUI");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStreamConverter(v => ((PresenterInfo)this).ControlType = (Kistl.App.GUI.VisualType)v, xml, "ControlType", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_DataAssembly, xml, "DataAssembly", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._DataTypeName, xml, "DataTypeName", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_PresenterAssembly, xml, "PresenterAssembly", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._PresenterTypeName, xml, "PresenterTypeName", "Kistl.App.GUI");
        }

#endregion

    }


}