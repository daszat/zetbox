
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
    [System.Diagnostics.DebuggerDisplay("ControlInfo")]
    public class ControlInfo__Implementation__ : BaseClientDataObject, ControlInfo
    {


        /// <summary>
        /// The assembly containing the Control
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                if (fk_Assembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_Assembly.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Assembly == null)
					return;
                else if (value != null && value.ID == _fk_Assembly)
					return;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Assembly");
				           
				// next, set the local reference
                _fk_Assembly = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Assembly");
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Assembly
        {
            get
            {
                return _fk_Assembly;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Assembly != value)
                {
                    NotifyPropertyChanging("Assembly");
                    _fk_Assembly = value;
                    NotifyPropertyChanged("Assembly");
                }
            }
        }
        private int? _fk_Assembly;

        /// <summary>
        /// The name of the class implementing this Control
        /// </summary>
        // value type property
        public virtual string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ClassName != value)
                {
                    NotifyPropertyChanging("ClassName");
                    _ClassName = value;
                    NotifyPropertyChanged("ClassName");
                }
            }
        }
        private string _ClassName;

        /// <summary>
        /// The type of Control of this implementation
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
                    NotifyPropertyChanging("ControlType");
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType");
                }
            }
        }
        private Kistl.App.GUI.VisualType _ControlType;

        /// <summary>
        /// Whether or not this Control can contain other Controls
        /// </summary>
        // value type property
        public virtual bool IsContainer
        {
            get
            {
                return _IsContainer;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsContainer != value)
                {
                    NotifyPropertyChanging("IsContainer");
                    _IsContainer = value;
                    NotifyPropertyChanged("IsContainer");
                }
            }
        }
        private bool _IsContainer;

        /// <summary>
        /// The toolkit of this Control.
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.Toolkit Platform
        {
            get
            {
                return _Platform;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Platform != value)
                {
                    NotifyPropertyChanging("Platform");
                    _Platform = value;
                    NotifyPropertyChanged("Platform");
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Platform;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ControlInfo));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ControlInfo != null)
            {
                OnToString_ControlInfo(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ControlInfo> OnToString_ControlInfo;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ControlInfo != null) OnPreSave_ControlInfo(this);
        }
        public event ObjectEventHandler<ControlInfo> OnPreSave_ControlInfo;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ControlInfo != null) OnPostSave_ControlInfo(this);
        }
        public event ObjectEventHandler<ControlInfo> OnPostSave_ControlInfo;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Assembly, binStream);
            BinarySerializer.ToStream(this._ClassName, binStream);
            BinarySerializer.ToStream((int)((ControlInfo)this).ControlType, binStream);
            BinarySerializer.ToStream(this._IsContainer, binStream);
            BinarySerializer.ToStream((int)((ControlInfo)this).Platform, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._ClassName, binStream);
            BinarySerializer.FromStreamConverter(v => ((ControlInfo)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._IsContainer, binStream);
            BinarySerializer.FromStreamConverter(v => ((ControlInfo)this).Platform = (Kistl.App.GUI.Toolkit)v, binStream);
        }

#endregion

    }


}