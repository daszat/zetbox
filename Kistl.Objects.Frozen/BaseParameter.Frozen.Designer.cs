
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
    /// Metadefinition Object for Parameter. This class is abstract.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BaseParameter")]
    public class BaseParameter__Implementation__Frozen : BaseFrozenDataObject, BaseParameter
    {
    
		public BaseParameter__Implementation__Frozen()
		{
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
        public virtual Kistl.App.Base.Method Method
        {
            get
            {
                return _Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method != value)
                {
					var __oldValue = _Method;
                    NotifyPropertyChanging("Method", __oldValue, value);
                    _Method = value;
                    NotifyPropertyChanged("Method", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Method _Method;

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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Description":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(177).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsList":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(94).Constraints
						.Where(c => !c.IsValid(this, this.IsList))
						.Select(c => c.GetErrorText(this, this.IsList))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsReturnParameter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(95).Constraints
						.Where(c => !c.IsValid(this, this.IsReturnParameter))
						.Select(c => c.GetErrorText(this, this.IsReturnParameter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Method":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(92).Constraints
						.Where(c => !c.IsValid(this, this.Method))
						.Select(c => c.GetErrorText(this, this.Method))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ParameterName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(91).Constraints
						.Where(c => !c.IsValid(this, this.ParameterName))
						.Select(c => c.GetErrorText(this, this.ParameterName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
        internal BaseParameter__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, BaseParameter__Implementation__Frozen> DataStore = new Dictionary<int, BaseParameter__Implementation__Frozen>(0);
		internal static void CreateInstances()
		{
		}

		internal static void FillDataStore() {
	
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
        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}