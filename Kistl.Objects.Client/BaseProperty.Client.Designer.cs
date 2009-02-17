
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
    [System.Diagnostics.DebuggerDisplay("BaseProperty")]
    public class BaseProperty__Implementation__ : BaseClientDataObject, BaseProperty
    {


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
                    NotifyPropertyChanged("AltText");;
                }
            }
        }
        private string _AltText;

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
                        
                    _ConstraintsWrapper = new BackReferenceCollection<Kistl.App.Base.Constraint>(
                        "ConstrainedProperty",
                        this,
                        serverList);
                }
                return _ConstraintsWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.Constraint> _ConstraintsWrapper;

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
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

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
                // fix up inverse reference
                var oldValue = ObjectClass;
                if (value != null && value.ID != fk_ObjectClass)
                {
                    oldValue.Properties.Remove(this);
                    fk_ObjectClass = value.ID;
                    value.Properties.Add(this);
                }
                else
                {
                    oldValue.Properties.Remove(this);
                    fk_ObjectClass = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ObjectClass
        {
            get
            {
                return _fk_ObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ObjectClass != value)
                {
                    NotifyPropertyChanging("ObjectClass");
                    _fk_ObjectClass = value;
                    NotifyPropertyChanging("ObjectClass");
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
                    NotifyPropertyChanging("PropertyName");
                    _PropertyName = value;
                    NotifyPropertyChanged("PropertyName");;
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
            };
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
            };
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
            };
            return e.Result;
        }
		public delegate void GetPropertyTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetPropertyTypeString_Handler<BaseProperty> OnGetPropertyTypeString_BaseProperty;



		public override Type GetInterfaceType()
		{
			return typeof(BaseProperty);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AltText, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._fk_ObjectClass, binStream);
            BinarySerializer.ToStream(this._PropertyName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AltText, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectClass, binStream);
            BinarySerializer.FromStream(out this._PropertyName, binStream);
        }

#endregion

    }


}