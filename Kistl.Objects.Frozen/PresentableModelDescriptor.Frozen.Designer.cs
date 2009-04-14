
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
    [System.Diagnostics.DebuggerDisplay("PresentableModelDescriptor")]
    public class PresentableModelDescriptor__Implementation__Frozen : BaseFrozenDataObject, PresentableModelDescriptor
    {
    
		public PresentableModelDescriptor__Implementation__Frozen()
		{
        }


        /// <summary>
        /// The default visual type used for this PresentableModel
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType DefaultVisualType
        {
            get
            {
                return _DefaultVisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultVisualType != value)
                {
					var __oldValue = _DefaultVisualType;
                    NotifyPropertyChanging("DefaultVisualType", __oldValue, value);
                    _DefaultVisualType = value;
                    NotifyPropertyChanged("DefaultVisualType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _DefaultVisualType;

        /// <summary>
        /// describe this PresentableModel
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
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// The described CLR class' reference
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef PresentableModelRef
        {
            get
            {
                return _PresentableModelRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresentableModelRef != value)
                {
					var __oldValue = _PresentableModelRef;
                    NotifyPropertyChanging("PresentableModelRef", __oldValue, value);
                    _PresentableModelRef = value;
                    NotifyPropertyChanged("PresentableModelRef", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _PresentableModelRef;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresentableModelDescriptor));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PresentableModelDescriptor != null)
            {
                OnToString_PresentableModelDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresentableModelDescriptor> OnToString_PresentableModelDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresentableModelDescriptor != null) OnPreSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPreSave_PresentableModelDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresentableModelDescriptor != null) OnPostSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPostSave_PresentableModelDescriptor;


        internal PresentableModelDescriptor__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, PresentableModelDescriptor__Implementation__Frozen> DataStore = new Dictionary<int, PresentableModelDescriptor__Implementation__Frozen>(0);
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