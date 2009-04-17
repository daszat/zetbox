
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

    using Kistl.API.Client;

    /// <summary>
    /// Metadefinition Object for Properties. This class is abstract.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Property")]
    public class Property__Implementation__ : BaseClientDataObject, Property
    {
    
		public Property__Implementation__()
		{
            {
            }
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
					var __oldValue = _CategoryTags;
                    NotifyPropertyChanging("CategoryTags", __oldValue, value);
                    _CategoryTags = value;
                    NotifyPropertyChanged("CategoryTags", __oldValue, value);
                }
            }
        }
        private string _CategoryTags;

        /// <summary>
        /// The list of constraints applying to this Property
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Constraint> Constraints
        {
            get
            {
                if (_ConstraintsWrapper == null)
                {
                    List<Kistl.App.Base.Constraint> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.Constraint>(this, "Constraints");
                    else
                        serverList = new List<Kistl.App.Base.Constraint>();
                        
                    _ConstraintsWrapper = new OneNRelationCollection<Kistl.App.Base.Constraint>(
                        "ConstrainedProperty",
                        this,
                        serverList);
                }
                return _ConstraintsWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.Constraint> _ConstraintsWrapper;

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
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                if (fk_Module.HasValue)
                    return Context.Find<Kistl.App.Base.Module>(fk_Module.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Module == null)
					return;
                else if (value != null && value.ID == _fk_Module)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Module;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Module", oldValue, value);
                
				// next, set the local reference
                _fk_Module = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Module", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                return _fk_Module;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Module != value)
                {
					var __oldValue = _fk_Module;
                    NotifyPropertyChanging("Module", __oldValue, value);
                    _fk_Module = value;
                    NotifyPropertyChanged("Module", __oldValue, value);
                }
            }
        }
        private int? _fk_Module;

        /// <summary>
        /// 
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                if (fk_ObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.DataType>(fk_ObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_ObjectClass == null)
					return;
                else if (value != null && value.ID == _fk_ObjectClass)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = ObjectClass;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ObjectClass", oldValue, value);
                
				// next, set the local reference
                _fk_ObjectClass = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.Properties as OneNRelationCollection<Kistl.App.Base.Property>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Properties as OneNRelationCollection<Kistl.App.Base.Property>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ObjectClass", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ObjectClass
        {
            get
            {
                return _fk_ObjectClass;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ObjectClass != value)
                {
					var __oldValue = _fk_ObjectClass;
                    NotifyPropertyChanging("ObjectClass", __oldValue, value);
                    _fk_ObjectClass = value;
                    NotifyPropertyChanged("ObjectClass", __oldValue, value);
                }
            }
        }
        private int? _fk_ObjectClass;

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

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Property)obj;
			var otherImpl = (Property__Implementation__)obj;
			var me = (Property)this;

			me.AltText = other.AltText;
			me.CategoryTags = other.CategoryTags;
			me.Description = other.Description;
			me.IsIndexed = other.IsIndexed;
			me.IsList = other.IsList;
			me.IsNullable = other.IsNullable;
			me.PropertyName = other.PropertyName;
			this.fk_Module = otherImpl.fk_Module;
			this.fk_ObjectClass = otherImpl.fk_ObjectClass;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Module":
                    fk_Module = id;
                    break;
                case "ObjectClass":
                    fk_ObjectClass = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AltText, binStream);
            BinarySerializer.ToStream(this._CategoryTags, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._IsIndexed, binStream);
            BinarySerializer.ToStream(this._IsList, binStream);
            BinarySerializer.ToStream(this._IsNullable, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._fk_ObjectClass, binStream);
            BinarySerializer.ToStream(this._PropertyName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AltText, binStream);
            BinarySerializer.FromStream(out this._CategoryTags, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._IsIndexed, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
            BinarySerializer.FromStream(out this._IsNullable, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectClass, binStream);
            BinarySerializer.FromStream(out this._PropertyName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._AltText, xml, "AltText", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._CategoryTags, xml, "CategoryTags", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsIndexed, xml, "IsIndexed", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsList, xml, "IsList", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsNullable, xml, "IsNullable", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_Module, xml, "fk_Module", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_ObjectClass, xml, "fk_ObjectClass", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PropertyName, xml, "PropertyName", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}