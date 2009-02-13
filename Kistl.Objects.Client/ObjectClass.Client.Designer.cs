
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
    /// Metadefinition Object for ObjectClasses.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectClass")]
    public class ObjectClass__Implementation__ : Kistl.App.Base.DataType__Implementation__, ObjectClass
    {


        /// <summary>
        /// Tabellenname in der Datenbank
        /// </summary>
        // value type property
        public virtual string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TableName != value)
                {
                    NotifyPropertyChanging("TableName");
                    _TableName = value;
                    NotifyPropertyChanged("TableName");;
                }
            }
        }
        private string _TableName;

        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                if (fk_BaseObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectClass>(fk_BaseObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = BaseObjectClass;
                if (value != null && value.ID != fk_BaseObjectClass)
                {
                    oldValue.SubClasses.Remove(this);
                    fk_BaseObjectClass = value.ID;
                    value.SubClasses.Add(this);
                }
                else
                {
                    oldValue.SubClasses.Remove(this);
                    fk_BaseObjectClass = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_BaseObjectClass
        {
            get
            {
                return _fk_BaseObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_BaseObjectClass != value)
                {
                    NotifyPropertyChanging("BaseObjectClass");
                    _fk_BaseObjectClass = value;
                    NotifyPropertyChanging("BaseObjectClass");
                }
            }
        }
        private int? _fk_BaseObjectClass;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                if (_SubClassesWrapper == null)
                {
                    List<Kistl.App.Base.ObjectClass> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.ObjectClass>(this, "SubClasses");
                    else
                        serverList = new List<Kistl.App.Base.ObjectClass>();
                        
                    _SubClassesWrapper = new BackReferenceCollection<Kistl.App.Base.ObjectClass>(
                        "BaseObjectClass",
                        this,
                        serverList);
                }
                return _SubClassesWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.ObjectClass> _SubClassesWrapper;

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
        {
            get
            {
                if (_ImplementsInterfacesWrapper == null)
                {
                    List<Kistl.App.Base.Interface> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.Interface>(this, "ImplementsInterfaces");
                    else
                        serverList = new List<Kistl.App.Base.Interface>();
                        
                    _ImplementsInterfacesWrapper = new BackReferenceCollection<Kistl.App.Base.Interface>(
                        "ObjectClass",
                        this,
                        serverList);
                }
                return _ImplementsInterfacesWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.Interface> _ImplementsInterfacesWrapper;

        /// <summary>
        /// Setting this to true marks the instances of this class as "simple." At first this will only mean that they'll be displayed inline.
        /// </summary>
        // value type property
        public virtual bool IsSimpleObject
        {
            get
            {
                return _IsSimpleObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsSimpleObject != value)
                {
                    NotifyPropertyChanging("IsSimpleObject");
                    _IsSimpleObject = value;
                    NotifyPropertyChanged("IsSimpleObject");;
                }
            }
        }
        private bool _IsSimpleObject;

        /// <summary>
        /// if true then all Instances appear in FozenContext.
        /// </summary>
        // value type property
        public virtual bool IsFrozenObject
        {
            get
            {
                return _IsFrozenObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsFrozenObject != value)
                {
                    NotifyPropertyChanging("IsFrozenObject");
                    _IsFrozenObject = value;
                    NotifyPropertyChanged("IsFrozenObject");;
                }
            }
        }
        private bool _IsFrozenObject;

        /// <summary>
        /// The default model to use for the UI
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef DefaultModel
        {
            get
            {
                if (fk_DefaultModel.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_DefaultModel.Value);
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
        public int? fk_DefaultModel
        {
            get
            {
                return _fk_DefaultModel;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DefaultModel != value)
                {
                    NotifyPropertyChanging("DefaultModel");
                    _fk_DefaultModel = value;
                    NotifyPropertyChanging("DefaultModel");
                }
            }
        }
        private int? _fk_DefaultModel;

        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_ObjectClass != null)
            {
                OnGetDataTypeString_ObjectClass(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;



        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_ObjectClass != null)
            {
                OnGetDataType_ObjectClass(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<ObjectClass> OnGetDataType_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual IList<Kistl.App.Base.Method> GetInheritedMethods() 
        {
            var e = new MethodReturnEventArgs<IList<Kistl.App.Base.Method>>();
            if (OnGetInheritedMethods_ObjectClass != null)
            {
                OnGetInheritedMethods_ObjectClass(this, e);
            };
            return e.Result;
        }
		public delegate void GetInheritedMethods_Handler<T>(T obj, MethodReturnEventArgs<IList<Kistl.App.Base.Method>> ret);
		public event GetInheritedMethods_Handler<ObjectClass> OnGetInheritedMethods_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual Kistl.App.Base.TypeRef GetDefaultModelRef() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.TypeRef>();
            if (OnGetDefaultModelRef_ObjectClass != null)
            {
                OnGetDefaultModelRef_ObjectClass(this, e);
            };
            return e.Result;
        }
		public delegate void GetDefaultModelRef_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.TypeRef> ret);
		public event GetDefaultModelRef_Handler<ObjectClass> OnGetDefaultModelRef_ObjectClass;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectClass != null)
            {
                OnToString_ObjectClass(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectClass> OnToString_ObjectClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectClass != null) OnPreSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPreSave_ObjectClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectClass != null) OnPostSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPostSave_ObjectClass;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._TableName, binStream);
            BinarySerializer.ToStream(this._fk_BaseObjectClass, binStream);
            BinarySerializer.ToStream(this._IsSimpleObject, binStream);
            BinarySerializer.ToStream(this._IsFrozenObject, binStream);
            BinarySerializer.ToStream(this._fk_DefaultModel, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._TableName, binStream);
            BinarySerializer.FromStream(out this._fk_BaseObjectClass, binStream);
            BinarySerializer.FromStream(out this._IsSimpleObject, binStream);
            BinarySerializer.FromStream(out this._IsFrozenObject, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultModel, binStream);
        }

#endregion

    }


}