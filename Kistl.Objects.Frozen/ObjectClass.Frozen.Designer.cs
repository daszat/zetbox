
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
        public virtual Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                return _BaseObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_BaseObjectClass != value)
                {
                    NotifyPropertyChanging("BaseObjectClass");
                    _BaseObjectClass = value;
                    NotifyPropertyChanged("BaseObjectClass");;
                }
            }
        }
        private Kistl.App.Base.ObjectClass _BaseObjectClass;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                if (_SubClasses == null)
                    _SubClasses = new List<Kistl.App.Base.ObjectClass>();
                return _SubClasses;
            }
        }
        private ICollection<Kistl.App.Base.ObjectClass> _SubClasses;

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
        {
            get
            {
                if (_ImplementsInterfaces == null)
                    _ImplementsInterfaces = new List<Kistl.App.Base.Interface>();
                return _ImplementsInterfaces;
            }
        }
        private ICollection<Kistl.App.Base.Interface> _ImplementsInterfaces;

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
        public virtual Kistl.App.Base.TypeRef DefaultModel
        {
            get
            {
                return _DefaultModel;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultModel != value)
                {
                    NotifyPropertyChanging("DefaultModel");
                    _DefaultModel = value;
                    NotifyPropertyChanged("DefaultModel");;
                }
            }
        }
        private Kistl.App.Base.TypeRef _DefaultModel;

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


        internal ObjectClass__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


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