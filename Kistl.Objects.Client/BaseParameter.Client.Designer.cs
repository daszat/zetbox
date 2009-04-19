
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
    /// Metadefinition Object for Parameter. This class is abstract.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BaseParameter")]
    public class BaseParameter__Implementation__ : BaseClientDataObject, BaseParameter
    {
    
		public BaseParameter__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Description of this Parameter
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
        /// Parameter wird als List<> generiert
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
        /// Es darf nur ein Return Parameter angegeben werden
        /// </summary>
        // value type property
        public virtual bool IsReturnParameter
        {
            get
            {
                return _IsReturnParameter;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsReturnParameter != value)
                {
					var __oldValue = _IsReturnParameter;
                    NotifyPropertyChanging("IsReturnParameter", __oldValue, value);
                    _IsReturnParameter = value;
                    NotifyPropertyChanged("IsReturnParameter", __oldValue, value);
                }
            }
        }
        private bool _IsReturnParameter;

        /// <summary>
        /// Methode des Parameters
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
                if (fk_Method.HasValue)
                    return Context.Find<Kistl.App.Base.Method>(fk_Method.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Method == null)
					return;
                else if (value != null && value.ID == _fk_Method)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Method;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Method", oldValue, value);
                
				// next, set the local reference
                _fk_Method = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.Parameter as OneNRelationCollection<Kistl.App.Base.BaseParameter>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Parameter as OneNRelationCollection<Kistl.App.Base.BaseParameter>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Method", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                return _fk_Method;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Method != value)
                {
					var __oldValue = _fk_Method;
                    NotifyPropertyChanging("Method", __oldValue, value);
                    _fk_Method = value;
                    NotifyPropertyChanged("Method", __oldValue, value);
                }
            }
        }
        private int? _fk_Method;
        public virtual int? Method_pos
        {
            get
            {
                return _Method_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method_pos != value)
                {
					var __oldValue = _Method_pos;
                    NotifyPropertyChanging("Method_pos", __oldValue, value);
                    _Method_pos = value;
                    NotifyPropertyChanged("Method_pos", __oldValue, value);
                }
            }
        }
        private int? _Method_pos;

        /// <summary>
        /// Name des Parameter
        /// </summary>
        // value type property
        public virtual string ParameterName
        {
            get
            {
                return _ParameterName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ParameterName != value)
                {
					var __oldValue = _ParameterName;
                    NotifyPropertyChanging("ParameterName", __oldValue, value);
                    _ParameterName = value;
                    NotifyPropertyChanged("ParameterName", __oldValue, value);
                }
            }
        }
        private string _ParameterName;

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public virtual System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_BaseParameter != null)
            {
                OnGetParameterType_BaseParameter(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseParameter.GetParameterType");
            }
            return e.Result;
        }
		public delegate void GetParameterType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetParameterType_Handler<BaseParameter> OnGetParameterType_BaseParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public virtual string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_BaseParameter != null)
            {
                OnGetParameterTypeString_BaseParameter(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseParameter.GetParameterTypeString");
            }
            return e.Result;
        }
		public delegate void GetParameterTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetParameterTypeString_Handler<BaseParameter> OnGetParameterTypeString_BaseParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(BaseParameter));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (BaseParameter)obj;
			var otherImpl = (BaseParameter__Implementation__)obj;
			var me = (BaseParameter)this;

			me.Description = other.Description;
			me.IsList = other.IsList;
			me.IsReturnParameter = other.IsReturnParameter;
			me.ParameterName = other.ParameterName;
			this.fk_Method = otherImpl.fk_Method;
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
            if (OnToString_BaseParameter != null)
            {
                OnToString_BaseParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BaseParameter> OnToString_BaseParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BaseParameter != null) OnPreSave_BaseParameter(this);
        }
        public event ObjectEventHandler<BaseParameter> OnPreSave_BaseParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BaseParameter != null) OnPostSave_BaseParameter(this);
        }
        public event ObjectEventHandler<BaseParameter> OnPostSave_BaseParameter;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Method":
                    fk_Method = id;
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
            BinarySerializer.ToStream(this._IsList, binStream);
            BinarySerializer.ToStream(this._IsReturnParameter, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStream(this._Method_pos, binStream);
            BinarySerializer.ToStream(this._ParameterName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
            BinarySerializer.FromStream(out this._IsReturnParameter, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._Method_pos, binStream);
            BinarySerializer.FromStream(out this._ParameterName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._IsList, xml, "IsList", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._IsReturnParameter, xml, "IsReturnParameter", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._fk_Method, xml, "Method", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Method_pos, xml, "Method_pos", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._ParameterName, xml, "ParameterName", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._IsList, xml, "IsList", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._IsReturnParameter, xml, "IsReturnParameter", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._fk_Method, xml, "Method", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Method_pos, xml, "Method_pos", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._ParameterName, xml, "ParameterName", "http://dasz.at/Kistl");
        }

#endregion

    }


}