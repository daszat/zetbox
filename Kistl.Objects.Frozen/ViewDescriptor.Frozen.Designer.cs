
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ViewDescriptor")]
    public class ViewDescriptor__Implementation__Frozen : BaseFrozenDataObject, ViewDescriptor
    {


        /// <summary>
        /// 
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef LayoutRef
        {
            get
            {
                return _LayoutRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_LayoutRef != value)
                {
                    NotifyPropertyChanging("LayoutRef");
                    _LayoutRef = value;
                    NotifyPropertyChanged("LayoutRef");
                }
            }
        }
        private Kistl.App.Base.TypeRef _LayoutRef;

        /// <summary>
        /// 
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
                    NotifyPropertyChanging("Toolkit");
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit");
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Toolkit;

        /// <summary>
        /// 
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef ViewRef
        {
            get
            {
                return _ViewRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ViewRef != value)
                {
                    NotifyPropertyChanging("ViewRef");
                    _ViewRef = value;
                    NotifyPropertyChanged("ViewRef");
                }
            }
        }
        private Kistl.App.Base.TypeRef _ViewRef;

		public override Type GetInterfaceType()
		{
			return typeof(ViewDescriptor);
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