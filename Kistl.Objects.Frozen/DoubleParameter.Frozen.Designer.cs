
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
    /// Metadefinition Object for Double Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DoubleParameter")]
    public class DoubleParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, DoubleParameter
    {


        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_DoubleParameter != null)
            {
                OnGetParameterType_DoubleParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<DoubleParameter> OnGetParameterType_DoubleParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_DoubleParameter != null)
            {
                OnGetParameterTypeString_DoubleParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<DoubleParameter> OnGetParameterTypeString_DoubleParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(DoubleParameter));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DoubleParameter != null)
            {
                OnToString_DoubleParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DoubleParameter> OnToString_DoubleParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DoubleParameter != null) OnPreSave_DoubleParameter(this);
        }
        public event ObjectEventHandler<DoubleParameter> OnPreSave_DoubleParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DoubleParameter != null) OnPostSave_DoubleParameter(this);
        }
        public event ObjectEventHandler<DoubleParameter> OnPostSave_DoubleParameter;


        internal DoubleParameter__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, DoubleParameter__Implementation__Frozen> DataStore = new Dictionary<int, DoubleParameter__Implementation__Frozen>(1);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[6] = 
			DataStore[6] = new DoubleParameter__Implementation__Frozen(6);

		}

		internal new static void FillDataStore() {
			DataStore[6].Description = null;
			DataStore[6].IsList = false;
			DataStore[6].IsReturnParameter = false;
			DataStore[6].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[83];
			DataStore[6].ParameterName = @"TestDouble";
			DataStore[6].Seal();
	
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