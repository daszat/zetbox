// <autogenerated/>


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
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DataType")]
    public class DataType__Implementation__ : BaseClientDataObject_ClientObjects, DataType, Kistl.API.IExportableInternal
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
                if (_fk_DefaultIcon.HasValue)
                    return Context.Find<Kistl.App.GUI.Icon>(_fk_DefaultIcon.Value);
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
        /// Export Guid
        /// </summary>
        // value type property
        public virtual Guid ExportGuid
        {
            get
            {
                return _ExportGuid;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ExportGuid != value)
                {
					var __oldValue = _ExportGuid;
                    NotifyPropertyChanging("ExportGuid", __oldValue, value);
                    _ExportGuid = value;
                    NotifyPropertyChanged("ExportGuid", __oldValue, value);
                }
            }
        }
        private Guid _ExportGuid;

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
                if (_fk_Module.HasValue)
                    return Context.Find<Kistl.App.Base.Module>(_fk_Module.Value);
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
        /// 
        /// </summary>
        // value type property
        public virtual bool ShowIconInLists
        {
            get
            {
                return _ShowIconInLists;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ShowIconInLists != value)
                {
					var __oldValue = _ShowIconInLists;
                    NotifyPropertyChanging("ShowIconInLists", __oldValue, value);
                    _ShowIconInLists = value;
                    NotifyPropertyChanged("ShowIconInLists", __oldValue, value);
                }
            }
        }
        private bool _ShowIconInLists;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual bool ShowIdInLists
        {
            get
            {
                return _ShowIdInLists;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ShowIdInLists != value)
                {
					var __oldValue = _ShowIdInLists;
                    NotifyPropertyChanging("ShowIdInLists", __oldValue, value);
                    _ShowIdInLists = value;
                    NotifyPropertyChanged("ShowIdInLists", __oldValue, value);
                }
            }
        }
        private bool _ShowIdInLists;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual bool ShowNameInLists
        {
            get
            {
                return _ShowNameInLists;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ShowNameInLists != value)
                {
					var __oldValue = _ShowNameInLists;
                    NotifyPropertyChanging("ShowNameInLists", __oldValue, value);
                    _ShowNameInLists = value;
                    NotifyPropertyChanged("ShowNameInLists", __oldValue, value);
                }
            }
        }
        private bool _ShowNameInLists;

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
			me.ExportGuid = other.ExportGuid;
			me.ShowIconInLists = other.ShowIconInLists;
			me.ShowIdInLists = other.ShowIdInLists;
			me.ShowNameInLists = other.ShowNameInLists;
			this._fk_DefaultIcon = otherImpl._fk_DefaultIcon;
			this._fk_Module = otherImpl._fk_Module;
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ClassName":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(1).Constraints
						.Where(c => !c.IsValid(this, this.ClassName))
						.Select(c => c.GetErrorText(this, this.ClassName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DefaultIcon":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(69).Constraints
						.Where(c => !c.IsValid(this, this.DefaultIcon))
						.Select(c => c.GetErrorText(this, this.DefaultIcon))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Description":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(175).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(252).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MethodInvocations":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(80).Constraints
						.Where(c => !c.IsValid(this, this.MethodInvocations))
						.Select(c => c.GetErrorText(this, this.MethodInvocations))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Methods":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(31).Constraints
						.Where(c => !c.IsValid(this, this.Methods))
						.Select(c => c.GetErrorText(this, this.Methods))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Module":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(45).Constraints
						.Where(c => !c.IsValid(this, this.Module))
						.Select(c => c.GetErrorText(this, this.Module))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Properties":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(7).Constraints
						.Where(c => !c.IsValid(this, this.Properties))
						.Select(c => c.GetErrorText(this, this.Properties))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ShowIconInLists":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(265).Constraints
						.Where(c => !c.IsValid(this, this.ShowIconInLists))
						.Select(c => c.GetErrorText(this, this.ShowIconInLists))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ShowIdInLists":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(266).Constraints
						.Where(c => !c.IsValid(this, this.ShowIdInLists))
						.Select(c => c.GetErrorText(this, this.ShowIdInLists))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ShowNameInLists":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(264).Constraints
						.Where(c => !c.IsValid(this, this.ShowNameInLists))
						.Select(c => c.GetErrorText(this, this.ShowNameInLists))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "DefaultIcon":
                    _fk_DefaultIcon = id;
                    break;
                case "Module":
                    _fk_Module = id;
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
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._ShowIconInLists, binStream);
            BinarySerializer.ToStream(this._ShowIdInLists, binStream);
            BinarySerializer.ToStream(this._ShowNameInLists, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._ClassName, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultIcon, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._ExportGuid, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._ShowIconInLists, binStream);
            BinarySerializer.FromStream(out this._ShowIdInLists, binStream);
            BinarySerializer.FromStream(out this._ShowNameInLists, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._ClassName, xml, "ClassName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_DefaultIcon, xml, "DefaultIcon", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._ShowIconInLists, xml, "ShowIconInLists", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._ShowIdInLists, xml, "ShowIdInLists", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._ShowNameInLists, xml, "ShowNameInLists", "Kistl.App.GUI");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._ClassName, xml, "ClassName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_DefaultIcon, xml, "DefaultIcon", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._ShowIconInLists, xml, "ShowIconInLists", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._ShowIdInLists, xml, "ShowIdInLists", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._ShowNameInLists, xml, "ShowNameInLists", "Kistl.App.GUI");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ClassName, xml, "ClassName", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._ShowIconInLists, xml, "ShowIconInLists", "Kistl.App.GUI");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._ShowIdInLists, xml, "ShowIdInLists", "Kistl.App.GUI");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._ShowNameInLists, xml, "ShowNameInLists", "Kistl.App.GUI");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ClassName, xml, "ClassName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ShowIconInLists, xml, "ShowIconInLists", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._ShowIdInLists, xml, "ShowIdInLists", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._ShowNameInLists, xml, "ShowNameInLists", "Kistl.App.GUI");
        }

#endregion

    }


}