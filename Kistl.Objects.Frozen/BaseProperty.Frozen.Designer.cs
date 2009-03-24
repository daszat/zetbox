
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
    /// Metadefinition Object for Properties. This class is abstract.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BaseProperty")]
    public class BaseProperty__Implementation__Frozen : BaseFrozenDataObject, BaseProperty
    {
    
		public BaseProperty__Implementation__Frozen()
		{
        }


        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string AltText
        {
            get
            {
                return _AltText;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AltText != value)
                {
                    NotifyPropertyChanging("AltText");
                    _AltText = value;
                    NotifyPropertyChanged("AltText");
                }
            }
        }
        private string _AltText;

        /// <summary>
        /// The list of constraints applying to this Property
        /// </summary>
        // object list property
        public virtual ICollection<Kistl.App.Base.Constraint> Constraints
        {
            get
            {
                if (_Constraints == null)
                    _Constraints = new ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0));
                return _Constraints;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _Constraints = (ReadOnlyCollection<Kistl.App.Base.Constraint>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.Constraint> _Constraints;

        /// <summary>
        /// Description of this Property
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
                    NotifyPropertyChanged("Description");
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Module Module
        {
            get
            {
                return _Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Module != value)
                {
                    NotifyPropertyChanging("Module");
                    _Module = value;
                    NotifyPropertyChanged("Module");
                }
            }
        }
        private Kistl.App.Base.Module _Module;

        /// <summary>
        /// 
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                return _ObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjectClass != value)
                {
                    NotifyPropertyChanging("ObjectClass");
                    _ObjectClass = value;
                    NotifyPropertyChanged("ObjectClass");
                }
            }
        }
        private Kistl.App.Base.DataType _ObjectClass;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PropertyName != value)
                {
                    NotifyPropertyChanging("PropertyName");
                    _PropertyName = value;
                    NotifyPropertyChanged("PropertyName");
                }
            }
        }
        private string _PropertyName;

        /// <summary>
        /// 
        /// </summary>

		public virtual string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_BaseProperty != null)
            {
                OnGetGUIRepresentation_BaseProperty(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseProperty.GetGUIRepresentation");
            }
            return e.Result;
        }
		public delegate void GetGUIRepresentation_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetGUIRepresentation_Handler<BaseProperty> OnGetGUIRepresentation_BaseProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public virtual System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BaseProperty != null)
            {
                OnGetPropertyType_BaseProperty(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseProperty.GetPropertyType");
            }
            return e.Result;
        }
		public delegate void GetPropertyType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetPropertyType_Handler<BaseProperty> OnGetPropertyType_BaseProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public virtual string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BaseProperty != null)
            {
                OnGetPropertyTypeString_BaseProperty(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseProperty.GetPropertyTypeString");
            }
            return e.Result;
        }
		public delegate void GetPropertyTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetPropertyTypeString_Handler<BaseProperty> OnGetPropertyTypeString_BaseProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(BaseProperty));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BaseProperty != null)
            {
                OnToString_BaseProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BaseProperty> OnToString_BaseProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BaseProperty != null) OnPreSave_BaseProperty(this);
        }
        public event ObjectEventHandler<BaseProperty> OnPreSave_BaseProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BaseProperty != null) OnPostSave_BaseProperty(this);
        }
        public event ObjectEventHandler<BaseProperty> OnPostSave_BaseProperty;


        internal BaseProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, BaseProperty__Implementation__Frozen> DataStore = new Dictionary<int, BaseProperty__Implementation__Frozen>(0);
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