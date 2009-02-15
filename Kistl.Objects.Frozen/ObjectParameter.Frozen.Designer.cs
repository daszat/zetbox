
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
                    NotifyPropertyChanged("DataType");;
                }
            }
        }
        private Kistl.App.Base.DataType _DataType;

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_ObjectParameter != null)
            {
                OnGetParameterTypeString_ObjectParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterTypeString_Handler<ObjectParameter> OnGetParameterTypeString_ObjectParameter;



        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_ObjectParameter != null)
            {
                OnGetParameterType_ObjectParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterType_Handler<ObjectParameter> OnGetParameterType_ObjectParameter;



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


        internal ObjectParameter__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, ObjectParameter__Implementation__Frozen> DataStore = new Dictionary<int, ObjectParameter__Implementation__Frozen>(5);
		static ObjectParameter__Implementation__Frozen()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[10] = 
			DataStore[10] = new ObjectParameter__Implementation__Frozen(null, 10);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[26] = 
			DataStore[26] = new ObjectParameter__Implementation__Frozen(null, 26);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[27] = 
			DataStore[27] = new ObjectParameter__Implementation__Frozen(null, 27);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[36] = 
			DataStore[36] = new ObjectParameter__Implementation__Frozen(null, 36);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[39] = 
			DataStore[39] = new ObjectParameter__Implementation__Frozen(null, 39);

		}

		internal new static void FillDataStore() {
			DataStore[10].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[26].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[27].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[36].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[39].DataType = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
	
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