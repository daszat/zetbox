
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
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class Visual__Implementation__ : BaseFrozenDataObject, Visual
    {


        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        // value type property
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Which visual is represented here
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
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.GUI.Visual> Children
        {
            get
            {
                if (_Children == null)
                    _Children = new List<Kistl.App.GUI.Visual>();
                return _Children;
            }
        }
        private ICollection<Kistl.App.GUI.Visual> _Children;

        /// <summary>
        /// The Property to display
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.BaseProperty Property
        {
            get
            {
                return _Property;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Property != value)
                {
                    NotifyPropertyChanging("Property");
                    _Property = value;
                    NotifyPropertyChanged("Property");;
                }
            }
        }
        private Kistl.App.Base.BaseProperty _Property;

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Method Method
        {
            get
            {
                return _Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method != value)
                {
                    NotifyPropertyChanging("Method");
                    _Method = value;
                    NotifyPropertyChanged("Method");;
                }
            }
        }
        private Kistl.App.Base.Method _Method;

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.GUI.Visual> ContextMenu
        {
            get
            {
                if (_ContextMenu == null)
                    _ContextMenu = new List<Kistl.App.GUI.Visual>();
                return _ContextMenu;
            }
        }
        private ICollection<Kistl.App.GUI.Visual> _ContextMenu;

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Visual != null)
            {
                OnToString_Visual(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Visual> OnToString_Visual;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Visual != null) OnPreSave_Visual(this);
        }
        public event ObjectEventHandler<Visual> OnPreSave_Visual;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Visual != null) OnPostSave_Visual(this);
        }
        public event ObjectEventHandler<Visual> OnPostSave_Visual;


        internal Visual__Implementation__(FrozenContext ctx, int id)
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