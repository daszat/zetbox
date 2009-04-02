
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
    [System.Diagnostics.DebuggerDisplay("Property")]
    public class Property__Implementation__Frozen : BaseFrozenDataObject, Property
    {
    
		public Property__Implementation__Frozen()
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
					var __oldValue = _AltText;
                    NotifyPropertyChanging("AltText", __oldValue, value);
                    _AltText = value;
                    NotifyPropertyChanged("AltText", __oldValue, value);
                }
            }
        }
        private string _AltText;

        /// <summary>
        /// A space separated list of category names containing this Property
        /// </summary>
        // value type property
        public virtual string CategoryTags
        {
            get
            {
                return _CategoryTags;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_CategoryTags != value)
                {
                    NotifyPropertyChanging("CategoryTags");
                    _CategoryTags = value;
                    NotifyPropertyChanged("CategoryTags");
                }
            }
        }
        private string _CategoryTags;

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
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Whether or not a list-valued property has a index
        /// </summary>
        // value type property
        public virtual bool IsIndexed
        {
            get
            {
                return _IsIndexed;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsIndexed != value)
                {
					var __oldValue = _IsIndexed;
                    NotifyPropertyChanging("IsIndexed", __oldValue, value);
                    _IsIndexed = value;
                    NotifyPropertyChanged("IsIndexed", __oldValue, value);
                }
            }
        }
        private bool _IsIndexed;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual bool IsList
        {
            get
            {
                return _IsList;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsList != value)
                {
					var __oldValue = _IsList;
                    NotifyPropertyChanging("IsList", __oldValue, value);
                    _IsList = value;
                    NotifyPropertyChanged("IsList", __oldValue, value);
                }
            }
        }
        private bool _IsList;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual bool IsNullable
        {
            get
            {
                return _IsNullable;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsNullable != value)
                {
					var __oldValue = _IsNullable;
                    NotifyPropertyChanging("IsNullable", __oldValue, value);
                    _IsNullable = value;
                    NotifyPropertyChanged("IsNullable", __oldValue, value);
                }
            }
        }
        private bool _IsNullable;

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
					var __oldValue = _Module;
                    NotifyPropertyChanging("Module", __oldValue, value);
                    _Module = value;
                    NotifyPropertyChanged("Module", __oldValue, value);
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
					var __oldValue = _ObjectClass;
                    NotifyPropertyChanging("ObjectClass", __oldValue, value);
                    _ObjectClass = value;
                    NotifyPropertyChanged("ObjectClass", __oldValue, value);
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
					var __oldValue = _PropertyName;
                    NotifyPropertyChanging("PropertyName", __oldValue, value);
                    _PropertyName = value;
                    NotifyPropertyChanged("PropertyName", __oldValue, value);
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
            if (OnGetGUIRepresentation_Property != null)
            {
                OnGetGUIRepresentation_Property(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Property.GetGUIRepresentation");
            }
            return e.Result;
        }
		public delegate void GetGUIRepresentation_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetGUIRepresentation_Handler<Property> OnGetGUIRepresentation_Property;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public virtual System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_Property != null)
            {
                OnGetPropertyType_Property(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Property.GetPropertyType");
            }
            return e.Result;
        }
		public delegate void GetPropertyType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetPropertyType_Handler<Property> OnGetPropertyType_Property;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public virtual string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_Property != null)
            {
                OnGetPropertyTypeString_Property(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Property.GetPropertyTypeString");
            }
            return e.Result;
        }
		public delegate void GetPropertyTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetPropertyTypeString_Handler<Property> OnGetPropertyTypeString_Property;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Property));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Property != null)
            {
                OnToString_Property(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Property> OnToString_Property;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Property != null) OnPreSave_Property(this);
        }
        public event ObjectEventHandler<Property> OnPreSave_Property;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Property != null) OnPostSave_Property(this);
        }
        public event ObjectEventHandler<Property> OnPostSave_Property;


        internal Property__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Property__Implementation__Frozen> DataStore = new Dictionary<int, Property__Implementation__Frozen>(0);
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