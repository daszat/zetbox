
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
    /// Metadefinition Object for Int Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IntParameter")]
    public class IntParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, IntParameter
    {


        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_IntParameter != null)
            {
                OnGetParameterType_IntParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<IntParameter> OnGetParameterType_IntParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_IntParameter != null)
            {
                OnGetParameterTypeString_IntParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<IntParameter> OnGetParameterTypeString_IntParameter;



		public override Type GetInterfaceType()
		{
			return typeof(IntParameter);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IntParameter != null)
            {
                OnToString_IntParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IntParameter> OnToString_IntParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IntParameter != null) OnPreSave_IntParameter(this);
        }
        public event ObjectEventHandler<IntParameter> OnPreSave_IntParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IntParameter != null) OnPostSave_IntParameter(this);
        }
        public event ObjectEventHandler<IntParameter> OnPostSave_IntParameter;


        internal IntParameter__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, IntParameter__Implementation__Frozen> DataStore = new Dictionary<int, IntParameter__Implementation__Frozen>(1);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[2] = 
			DataStore[2] = new IntParameter__Implementation__Frozen(2);

		}

		internal new static void FillDataStore() {
			DataStore[2].ParameterName = @"TestInt";
			DataStore[2].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[83];
			DataStore[2].IsList = false;
			DataStore[2].IsReturnParameter = false;
			DataStore[2].Description = null;
			DataStore[2].Seal();
	
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