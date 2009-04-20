
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
    /// Metadefinition Object for Methods.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Method")]
    public class Method__Implementation__ : BaseClientDataObject, Method
    {
    
		public Method__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Description of this Method
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
        /// Shows this Method in th GUI
        /// </summary>
        // value type property
        public virtual bool IsDisplayable
        {
            get
            {
                return _IsDisplayable;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsDisplayable != value)
                {
					var __oldValue = _IsDisplayable;
                    NotifyPropertyChanging("IsDisplayable", __oldValue, value);
                    _IsDisplayable = value;
                    NotifyPropertyChanged("IsDisplayable", __oldValue, value);
                }
            }
        }
        private bool _IsDisplayable;

        /// <summary>
        /// Methodenaufrufe implementiert in dieser Objekt Klasse
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.MethodInvocation> MethodInvokations
        {
            get
            {
                if (_MethodInvokationsWrapper == null)
                {
                    List<Kistl.App.Base.MethodInvocation> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.MethodInvocation>(this, "MethodInvokations");
                    else
                        serverList = new List<Kistl.App.Base.MethodInvocation>();
                        
                    _MethodInvokationsWrapper = new OneNRelationCollection<Kistl.App.Base.MethodInvocation>(
                        "Method",
                        this,
                        serverList);
                }
                return _MethodInvokationsWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.MethodInvocation> _MethodInvokationsWrapper;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string MethodName
        {
            get
            {
                return _MethodName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MethodName != value)
                {
					var __oldValue = _MethodName;
                    NotifyPropertyChanging("MethodName", __oldValue, value);
                    _MethodName = value;
                    NotifyPropertyChanged("MethodName", __oldValue, value);
                }
            }
        }
        private string _MethodName;

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
					(oldValue.Methods as OneNRelationCollection<Kistl.App.Base.Method>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Methods as OneNRelationCollection<Kistl.App.Base.Method>).AddWithoutSetParent(this);
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
        /// Parameter der Methode
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Base.BaseParameter> Parameter
        {
            get
            {
                if (_ParameterWrapper == null)
                {
                    List<Kistl.App.Base.BaseParameter> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.BaseParameter>(this, "Parameter");
                    else
                        serverList = new List<Kistl.App.Base.BaseParameter>();
                        
                    _ParameterWrapper = new OneNRelationCollection<Kistl.App.Base.BaseParameter>(
                        "Method",
                        this,
                        serverList);
                }
                return _ParameterWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.BaseParameter> _ParameterWrapper;

        /// <summary>
        /// Returns the Return Parameter Meta Object of this Method Meta Object.
        /// </summary>

		public virtual Kistl.App.Base.BaseParameter GetReturnParameter() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.BaseParameter>();
            if (OnGetReturnParameter_Method != null)
            {
                OnGetReturnParameter_Method(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Method.GetReturnParameter");
            }
            return e.Result;
        }
		public delegate void GetReturnParameter_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.BaseParameter> ret);
		public event GetReturnParameter_Handler<Method> OnGetReturnParameter_Method;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Method));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Method)obj;
			var otherImpl = (Method__Implementation__)obj;
			var me = (Method)this;

			me.Description = other.Description;
			me.IsDisplayable = other.IsDisplayable;
			me.MethodName = other.MethodName;
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
            if (OnToString_Method != null)
            {
                OnToString_Method(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Method> OnToString_Method;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Method != null) OnPreSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPreSave_Method;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Method != null) OnPostSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPostSave_Method;



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
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._IsDisplayable, binStream);
            BinarySerializer.ToStream(this._MethodName, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._fk_ObjectClass, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._IsDisplayable, binStream);
            BinarySerializer.FromStream(out this._MethodName, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectClass, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.ToStream(this._IsDisplayable, xml, "IsDisplayable", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._MethodName, xml, "MethodName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_ObjectClass, xml, "ObjectClass", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsDisplayable, xml, "IsDisplayable", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._MethodName, xml, "MethodName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._fk_ObjectClass, xml, "ObjectClass", "http://dasz.at/Kistl");
        }

#endregion

    }


}