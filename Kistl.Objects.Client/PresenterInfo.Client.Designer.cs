
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
                    NotifyPropertyChanging("ControlType");
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType");;
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
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DataAssembly
        {
            get
            {
                return _fk_DataAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DataAssembly != value)
                {
                    NotifyPropertyChanging("DataAssembly");
                    _fk_DataAssembly = value;
                    NotifyPropertyChanging("DataAssembly");
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
                    NotifyPropertyChanging("DataTypeName");
                    _DataTypeName = value;
                    NotifyPropertyChanged("DataTypeName");;
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
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_PresenterAssembly
        {
            get
            {
                return _fk_PresenterAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_PresenterAssembly != value)
                {
                    NotifyPropertyChanging("PresenterAssembly");
                    _fk_PresenterAssembly = value;
                    NotifyPropertyChanging("PresenterAssembly");
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
                    NotifyPropertyChanging("PresenterTypeName");
                    _PresenterTypeName = value;
                    NotifyPropertyChanged("PresenterTypeName");;
                }
            }
        }
        private string _PresenterTypeName;

		public override Type GetInterfaceType()
		{
			return typeof(PresenterInfo);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream((int)this.ControlType, binStream);
            BinarySerializer.ToStream(this._fk_DataAssembly, binStream);
            BinarySerializer.ToStream(this._DataTypeName, binStream);
            BinarySerializer.ToStream(this._fk_PresenterAssembly, binStream);
            BinarySerializer.ToStream(this._PresenterTypeName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => this.ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._fk_DataAssembly, binStream);
            BinarySerializer.FromStream(out this._DataTypeName, binStream);
            BinarySerializer.FromStream(out this._fk_PresenterAssembly, binStream);
            BinarySerializer.FromStream(out this._PresenterTypeName, binStream);
        }

#endregion

    }


}