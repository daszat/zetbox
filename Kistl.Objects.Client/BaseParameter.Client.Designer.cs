
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
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");
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
                    NotifyPropertyChanging("IsList");
                    _IsList = value;
                    NotifyPropertyChanged("IsList");
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
                    NotifyPropertyChanging("IsReturnParameter");
                    _IsReturnParameter = value;
                    NotifyPropertyChanged("IsReturnParameter");
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
                // fix up inverse reference
                var oldValue = Method;
                if (value != null && value.ID != fk_Method)
                {
                    oldValue.Parameter.Remove(this);
                    fk_Method = value.ID;
                    value.Parameter.Add(this);
                }
                else
                {
                    oldValue.Parameter.Remove(this);
                    fk_Method = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                return _fk_Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Method != value)
                {
                    NotifyPropertyChanging("Method");
                    _fk_Method = value;
                    NotifyPropertyChanging("Method");
                }
            }
        }
        private int? _fk_Method;

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
                    NotifyPropertyChanging("ParameterName");
                    _ParameterName = value;
                    NotifyPropertyChanged("ParameterName");
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
            };
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
            };
            return e.Result;
        }
		public delegate void GetParameterTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetParameterTypeString_Handler<BaseParameter> OnGetParameterTypeString_BaseParameter;



		public override Type GetInterfaceType()
		{
			return typeof(BaseParameter);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._IsList, binStream);
            BinarySerializer.ToStream(this._IsReturnParameter, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStream(this._ParameterName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
            BinarySerializer.FromStream(out this._IsReturnParameter, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._ParameterName, binStream);
        }

#endregion

    }


}