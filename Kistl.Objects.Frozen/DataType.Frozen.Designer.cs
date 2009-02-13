
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
    /// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DataType")]
    public class DataType__Implementation__ : BaseFrozenDataObject, DataType
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
                    NotifyPropertyChanged("ClassName");;
                }
            }
        }
        private string _ClassName;

        /// <summary>
        /// Eigenschaften der Objektklasse
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.BaseProperty> Properties
        {
            get
            {
                if (_Properties == null)
                    _Properties = new List<Kistl.App.Base.BaseProperty>();
                return _Properties;
            }
        }
        private ICollection<Kistl.App.Base.BaseProperty> _Properties;

        /// <summary>
        /// Liste aller Methoden der Objektklasse.
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.Method> Methods
        {
            get
            {
                if (_Methods == null)
                    _Methods = new List<Kistl.App.Base.Method>();
                return _Methods;
            }
        }
        private ICollection<Kistl.App.Base.Method> _Methods;

        /// <summary>
        /// Modul der Objektklasse
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
                    NotifyPropertyChanged("Module");;
                }
            }
        }
        private Kistl.App.Base.Module _Module;

        /// <summary>
        /// Standard Icon wenn IIcon nicht implementiert ist
        /// </summary>
        // object reference property
        public virtual Kistl.App.GUI.Icon DefaultIcon
        {
            get
            {
                return _DefaultIcon;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultIcon != value)
                {
                    NotifyPropertyChanging("DefaultIcon");
                    _DefaultIcon = value;
                    NotifyPropertyChanged("DefaultIcon");;
                }
            }
        }
        private Kistl.App.GUI.Icon _DefaultIcon;

        /// <summary>
        /// all implemented Methods in this DataType
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.MethodInvocation> MethodInvocations
        {
            get
            {
                if (_MethodInvocations == null)
                    _MethodInvocations = new List<Kistl.App.Base.MethodInvocation>();
                return _MethodInvocations;
            }
        }
        private ICollection<Kistl.App.Base.MethodInvocation> _MethodInvocations;

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
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

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


        internal DataType__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }
		internal Dictionary<int, DataType> DataStore = new Dictionary<int, DataType>(0);
		static DataType__Implementation__()
		{
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