
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
    /// Metadefinition Object for Object Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectParameter")]
    public class ObjectParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, ObjectParameter
    {
    
		public ObjectParameter__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Kistl-Typ des Parameters
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType DataType
        {
            get
            {
                if (fk_DataType.HasValue)
                    return Context.Find<Kistl.App.Base.DataType>(fk_DataType.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_DataType == null)
					return;
                else if (value != null && value.ID == _fk_DataType)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = DataType;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DataType", oldValue, value);
                
				// next, set the local reference
                _fk_DataType = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DataType", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DataType
        {
            get
            {
                return _fk_DataType;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DataType != value)
                {
					var __oldValue = _fk_DataType;
                    NotifyPropertyChanging("DataType", __oldValue, value);
                    _fk_DataType = value;
                    NotifyPropertyChanged("DataType", __oldValue, value);
                }
            }
        }
        private int? _fk_DataType;

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_ObjectParameter != null)
            {
                OnGetParameterType_ObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<ObjectParameter> OnGetParameterType_ObjectParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_ObjectParameter != null)
            {
                OnGetParameterTypeString_ObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<ObjectParameter> OnGetParameterTypeString_ObjectParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectParameter));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ObjectParameter)obj;
			var otherImpl = (ObjectParameter__Implementation__)obj;
			var me = (ObjectParameter)this;

			this.fk_DataType = otherImpl.fk_DataType;
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
            if (OnToString_ObjectParameter != null)
            {
                OnToString_ObjectParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectParameter> OnToString_ObjectParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectParameter != null) OnPreSave_ObjectParameter(this);
        }
        public event ObjectEventHandler<ObjectParameter> OnPreSave_ObjectParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectParameter != null) OnPostSave_ObjectParameter(this);
        }
        public event ObjectEventHandler<ObjectParameter> OnPostSave_ObjectParameter;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "DataType":
                    fk_DataType = id;
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
            BinarySerializer.ToStream(this._fk_DataType, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_DataType, binStream);
        }

#endregion

    }


}