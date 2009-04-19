
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
    /// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DataType")]
    public class DataType__Implementation__ : BaseClientDataObject, DataType
    {
    
		public DataType__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Der Name der Objektklasse
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
					var __oldValue = _ClassName;
                    NotifyPropertyChanging("ClassName", __oldValue, value);
                    _ClassName = value;
                    NotifyPropertyChanged("ClassName", __oldValue, value);
                }
            }
        }
        private string _ClassName;

        /// <summary>
        /// Standard Icon wenn IIcon nicht implementiert ist
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Icon DefaultIcon
        {
            get
            {
                if (fk_DefaultIcon.HasValue)
                    return Context.Find<Kistl.App.GUI.Icon>(fk_DefaultIcon.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_DefaultIcon == null)
					return;
                else if (value != null && value.ID == _fk_DefaultIcon)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = DefaultIcon;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DefaultIcon", oldValue, value);
                
				// next, set the local reference
                _fk_DefaultIcon = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DefaultIcon", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DefaultIcon
        {
            get
            {
                return _fk_DefaultIcon;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DefaultIcon != value)
                {
					var __oldValue = _fk_DefaultIcon;
                    NotifyPropertyChanging("DefaultIcon", __oldValue, value);
                    _fk_DefaultIcon = value;
                    NotifyPropertyChanged("DefaultIcon", __oldValue, value);
                }
            }
        }
        private int? _fk_DefaultIcon;

        /// <summary>
        /// Description of this DataType
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
        /// all implemented Methods in this DataType
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.MethodInvocation> MethodInvocations
        {
            get
            {
                if (_MethodInvocationsWrapper == null)
                {
                    List<Kistl.App.Base.MethodInvocation> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.MethodInvocation>(this, "MethodInvocations");
                    else
                        serverList = new List<Kistl.App.Base.MethodInvocation>();
                        
                    _MethodInvocationsWrapper = new OneNRelationCollection<Kistl.App.Base.MethodInvocation>(
                        "InvokeOnObjectClass",
                        this,
                        serverList);
                }
                return _MethodInvocationsWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.MethodInvocation> _MethodInvocationsWrapper;

        /// <summary>
        /// Liste aller Methoden der Objektklasse.
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Method> Methods
        {
            get
            {
                if (_MethodsWrapper == null)
                {
                    List<Kistl.App.Base.Method> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.Method>(this, "Methods");
                    else
                        serverList = new List<Kistl.App.Base.Method>();
                        
                    _MethodsWrapper = new OneNRelationCollection<Kistl.App.Base.Method>(
                        "ObjectClass",
                        this,
                        serverList);
                }
                return _MethodsWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.Method> _MethodsWrapper;

        /// <summary>
        /// Modul der Objektklasse
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
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.DataTypes as OneNRelationCollection<Kistl.App.Base.DataType>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.DataTypes as OneNRelationCollection<Kistl.App.Base.DataType>).AddWithoutSetParent(this);
                }
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
        /// Eigenschaften der Objektklasse
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Property> Properties
        {
            get
            {
                if (_PropertiesWrapper == null)
                {
                    List<Kistl.App.Base.Property> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.Property>(this, "Properties");
                    else
                        serverList = new List<Kistl.App.Base.Property>();
                        
                    _PropertiesWrapper = new OneNRelationCollection<Kistl.App.Base.Property>(
                        "ObjectClass",
                        this,
                        serverList);
                }
                return _PropertiesWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.Property> _PropertiesWrapper;

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public virtual System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_DataType != null)
            {
                OnGetDataType_DataType(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on DataType.GetDataType");
            }
            return e.Result;
        }
		public delegate void GetDataType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetDataType_Handler<DataType> OnGetDataType_DataType;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public virtual string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_DataType != null)
            {
                OnGetDataTypeString_DataType(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on DataType.GetDataTypeString");
            }
            return e.Result;
        }
		public delegate void GetDataTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetDataTypeString_Handler<DataType> OnGetDataTypeString_DataType;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(DataType));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (DataType)obj;
			var otherImpl = (DataType__Implementation__)obj;
			var me = (DataType)this;

			me.ClassName = other.ClassName;
			me.Description = other.Description;
			this.fk_DefaultIcon = otherImpl.fk_DefaultIcon;
			this.fk_Module = otherImpl.fk_Module;
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
            if (OnToString_DataType != null)
            {
                OnToString_DataType(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DataType> OnToString_DataType;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DataType != null) OnPreSave_DataType(this);
        }
        public event ObjectEventHandler<DataType> OnPreSave_DataType;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DataType != null) OnPostSave_DataType(this);
        }
        public event ObjectEventHandler<DataType> OnPostSave_DataType;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "DefaultIcon":
                    fk_DefaultIcon = id;
                    break;
                case "Module":
                    fk_Module = id;
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
            BinarySerializer.ToStream(this._ClassName, binStream);
            BinarySerializer.ToStream(this._fk_DefaultIcon, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._ClassName, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultIcon, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._ClassName, xml, "ClassName", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._fk_DefaultIcon, xml, "DefaultIcon", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._ClassName, xml, "ClassName", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._fk_DefaultIcon, xml, "DefaultIcon", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
        }

#endregion

    }


}