
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

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PresenterInfo")]
    public class PresenterInfo__Implementation__ : BaseClientDataObject, PresenterInfo
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
                if (fk_DataAssembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_DataAssembly.Value);
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
        
        // provide a way to directly access the foreign key int
        public int? fk_DataAssembly
        {
            get
            {
                return _fk_DataAssembly;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DataAssembly != value)
                {
					var __oldValue = _fk_DataAssembly;
                    NotifyPropertyChanging("DataAssembly", __oldValue, value);
                    _fk_DataAssembly = value;
                    NotifyPropertyChanged("DataAssembly", __oldValue, value);
                }
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
                if (fk_PresenterAssembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_PresenterAssembly.Value);
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
        
        // provide a way to directly access the foreign key int
        public int? fk_PresenterAssembly
        {
            get
            {
                return _fk_PresenterAssembly;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_PresenterAssembly != value)
                {
					var __oldValue = _fk_PresenterAssembly;
                    NotifyPropertyChanging("PresenterAssembly", __oldValue, value);
                    _fk_PresenterAssembly = value;
                    NotifyPropertyChanged("PresenterAssembly", __oldValue, value);
                }
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
			this.fk_DataAssembly = otherImpl.fk_DataAssembly;
			this.fk_PresenterAssembly = otherImpl.fk_PresenterAssembly;
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "DataAssembly":
                    fk_DataAssembly = id;
                    break;
                case "PresenterAssembly":
                    fk_PresenterAssembly = id;
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

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            // TODO: Add XML Serializer here
            XmlStreamer.ToStream(this._fk_DataAssembly, xml, "fk_DataAssembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._DataTypeName, xml, "DataTypeName", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_PresenterAssembly, xml, "fk_PresenterAssembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PresenterTypeName, xml, "PresenterTypeName", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}