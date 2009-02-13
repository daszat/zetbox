
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PresenterInfo")]
    public class PresenterInfo__Implementation__ : BaseFrozenDataObject, PresenterInfo
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
        /// Where to find the implementation of the Presenter
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Assembly PresenterAssembly
        {
            get
            {
                return _PresenterAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresenterAssembly != value)
                {
                    NotifyPropertyChanging("PresenterAssembly");
                    _PresenterAssembly = value;
                    NotifyPropertyChanged("PresenterAssembly");;
                }
            }
        }
        private Kistl.App.Base.Assembly _PresenterAssembly;

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

        /// <summary>
        /// The Assembly of the Data Type
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Assembly DataAssembly
        {
            get
            {
                return _DataAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DataAssembly != value)
                {
                    NotifyPropertyChanging("DataAssembly");
                    _DataAssembly = value;
                    NotifyPropertyChanged("DataAssembly");;
                }
            }
        }
        private Kistl.App.Base.Assembly _DataAssembly;

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


        internal PresenterInfo__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
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

#endregion

    }


}