
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
    [System.Diagnostics.DebuggerDisplay("ViewDescriptor")]
    public class ViewDescriptor__Implementation__Frozen : BaseFrozenDataObject, ViewDescriptor
    {
    
		public ViewDescriptor__Implementation__Frozen()
		{
        }


        /// <summary>
        /// The control implementing this View
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef ControlRef
        {
            get
            {
                return _ControlRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ControlRef != value)
                {
					var __oldValue = _ControlRef;
                    NotifyPropertyChanging("ControlRef", __oldValue, value);
                    _ControlRef = value;
                    NotifyPropertyChanged("ControlRef", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _ControlRef;

        /// <summary>
        /// The PresentableModel usable by this View
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef PresentedModelRef
        {
            get
            {
                return _PresentedModelRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresentedModelRef != value)
                {
					var __oldValue = _PresentedModelRef;
                    NotifyPropertyChanging("PresentedModelRef", __oldValue, value);
                    _PresentedModelRef = value;
                    NotifyPropertyChanged("PresentedModelRef", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _PresentedModelRef;

        /// <summary>
        /// Which toolkit provides this View
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.Toolkit Toolkit
        {
            get
            {
                return _Toolkit;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Toolkit != value)
                {
					var __oldValue = _Toolkit;
                    NotifyPropertyChanging("Toolkit", __oldValue, value);
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Toolkit;

        /// <summary>
        /// The visual type of this View
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType VisualType
        {
            get
            {
                return _VisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_VisualType != value)
                {
					var __oldValue = _VisualType;
                    NotifyPropertyChanging("VisualType", __oldValue, value);
                    _VisualType = value;
                    NotifyPropertyChanged("VisualType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _VisualType;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ViewDescriptor));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ViewDescriptor != null)
            {
                OnToString_ViewDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ViewDescriptor> OnToString_ViewDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ViewDescriptor != null) OnPreSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPreSave_ViewDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ViewDescriptor != null) OnPostSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPostSave_ViewDescriptor;


        internal ViewDescriptor__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, ViewDescriptor__Implementation__Frozen> DataStore = new Dictionary<int, ViewDescriptor__Implementation__Frozen>(0);
		internal static void CreateInstances()
		{
		}

		internal static void FillDataStore() {
	
		}
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