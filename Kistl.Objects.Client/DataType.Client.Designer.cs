
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
                    NotifyPropertyChanging("ClassName");
                    _ClassName = value;
                    NotifyPropertyChanged("ClassName");
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
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DefaultIcon
        {
            get
            {
                return _fk_DefaultIcon;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DefaultIcon != value)
                {
                    NotifyPropertyChanging("DefaultIcon");
                    _fk_DefaultIcon = value;
                    NotifyPropertyChanging("DefaultIcon");
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
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");
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
                        
                    _MethodInvocationsWrapper = new BackReferenceCollection<Kistl.App.Base.MethodInvocation>(
                        "InvokeOnObjectClass",
                        this,
                        serverList);
                }
                return _MethodInvocationsWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.MethodInvocation> _MethodInvocationsWrapper;

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
                        
                    _MethodsWrapper = new BackReferenceCollection<Kistl.App.Base.Method>(
                        "ObjectClass",
                        this,
                        serverList);
                }
                return _MethodsWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.Method> _MethodsWrapper;

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
                // fix up inverse reference
                var oldValue = Module;
                if (value != null && value.ID != fk_Module)
                {
                    oldValue.DataTypes.Remove(this);
                    fk_Module = value.ID;
                    value.DataTypes.Add(this);
                }
                else
                {
                    oldValue.DataTypes.Remove(this);
                    fk_Module = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                return _fk_Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Module != value)
                {
                    NotifyPropertyChanging("Module");
                    _fk_Module = value;
                    NotifyPropertyChanging("Module");
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
        public ICollection<Kistl.App.Base.BaseProperty> Properties
        {
            get
            {
                if (_PropertiesWrapper == null)
                {
                    List<Kistl.App.Base.BaseProperty> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.BaseProperty>(this, "Properties");
                    else
                        serverList = new List<Kistl.App.Base.BaseProperty>();
                        
                    _PropertiesWrapper = new BackReferenceCollection<Kistl.App.Base.BaseProperty>(
                        "ObjectClass",
                        this,
                        serverList);
                }
                return _PropertiesWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.BaseProperty> _PropertiesWrapper;

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public virtual System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_DataType != null)
            {
                OnGetDataType_DataType(this, e);
            };
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
            };
            return e.Result;
        }
		public delegate void GetDataTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetDataTypeString_Handler<DataType> OnGetDataTypeString_DataType;



		public override Type GetInterfaceType()
		{
			return typeof(DataType);
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

#endregion

    }


}