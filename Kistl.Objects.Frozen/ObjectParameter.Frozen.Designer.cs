
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
    /// Metadefinition Object for Object Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectParameter")]
    public class ObjectParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, ObjectParameter
    {


        /// <summary>
        /// Kistl-Typ des Parameters
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.DataType DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DataType != value)
                {
                    NotifyPropertyChanging("DataType");
                    _DataType = value;
                    NotifyPropertyChanged("DataType");
                }
            }
        }
        private Kistl.App.Base.DataType _DataType;

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
                base.GetParameterType();
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
                base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<ObjectParameter> OnGetParameterTypeString_ObjectParameter;



		public override Type GetInterfaceType()
		{
			return typeof(ObjectParameter);
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


        internal ObjectParameter__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, ObjectParameter__Implementation__Frozen> DataStore = new Dictionary<int, ObjectParameter__Implementation__Frozen>(5);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[10] = 
			DataStore[10] = new ObjectParameter__Implementation__Frozen(10);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[26] = 
			DataStore[26] = new ObjectParameter__Implementation__Frozen(26);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[27] = 
			DataStore[27] = new ObjectParameter__Implementation__Frozen(27);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[36] = 
			DataStore[36] = new ObjectParameter__Implementation__Frozen(36);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[39] = 
			DataStore[39] = new ObjectParameter__Implementation__Frozen(39);

		}

		internal new static void FillDataStore() {
			DataStore[10].ParameterName = @"TestObjectParameter";
			DataStore[10].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[83];
			DataStore[10].IsList = false;
			DataStore[10].IsReturnParameter = false;
			DataStore[10].Description = null;
			DataStore[10].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[10].Seal();
			DataStore[26].ParameterName = @"Result";
			DataStore[26].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[124];
			DataStore[26].IsList = false;
			DataStore[26].IsReturnParameter = true;
			DataStore[26].Description = null;
			DataStore[26].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[26].Seal();
			DataStore[27].ParameterName = @"Result";
			DataStore[27].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[125];
			DataStore[27].IsList = true;
			DataStore[27].IsReturnParameter = true;
			DataStore[27].Description = null;
			DataStore[27].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[27].Seal();
			DataStore[36].ParameterName = @"cls";
			DataStore[36].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[144];
			DataStore[36].IsList = false;
			DataStore[36].IsReturnParameter = false;
			DataStore[36].Description = null;
			DataStore[36].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[36].Seal();
			DataStore[39].ParameterName = @"result";
			DataStore[39].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[156];
			DataStore[39].IsList = false;
			DataStore[39].IsReturnParameter = true;
			DataStore[39].Description = @"returns the TypeRef of the default model for this ObjectClass";
			DataStore[39].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[39].Seal();
	
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