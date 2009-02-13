
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
    [System.Diagnostics.DebuggerDisplay("ControlInfo")]
    public class ControlInfo__Implementation__ : BaseFrozenDataObject, ControlInfo
    {


        /// <summary>
        /// The assembly containing the Control
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return _Assembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Assembly != value)
                {
                    NotifyPropertyChanging("Assembly");
                    _Assembly = value;
                    NotifyPropertyChanged("Assembly");;
                }
            }
        }
        private Kistl.App.Base.Assembly _Assembly;

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
                    NotifyPropertyChanged("ClassName");;
                }
            }
        }
        private string _ClassName;

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
                    NotifyPropertyChanged("IsContainer");;
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
                    NotifyPropertyChanged("Platform");;
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Platform;

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
                    NotifyPropertyChanged("ControlType");;
                }
            }
        }
        private Kistl.App.GUI.VisualType _ControlType;

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


        internal ControlInfo__Implementation__(FrozenContext ctx, int id)
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