
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
    /// Metadefinition Object for String Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StringParameter")]
    public class StringParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, StringParameter
    {


        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_StringParameter != null)
            {
                OnGetParameterType_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<StringParameter> OnGetParameterType_StringParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_StringParameter != null)
            {
                OnGetParameterTypeString_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<StringParameter> OnGetParameterTypeString_StringParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(StringParameter));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StringParameter != null)
            {
                OnToString_StringParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StringParameter> OnToString_StringParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StringParameter != null) OnPreSave_StringParameter(this);
        }
        public event ObjectEventHandler<StringParameter> OnPreSave_StringParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StringParameter != null) OnPostSave_StringParameter(this);
        }
        public event ObjectEventHandler<StringParameter> OnPostSave_StringParameter;


        internal StringParameter__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, StringParameter__Implementation__Frozen> DataStore = new Dictionary<int, StringParameter__Implementation__Frozen>(8);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[1] = 
			DataStore[1] = new StringParameter__Implementation__Frozen(1);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[3] = 
			DataStore[3] = new StringParameter__Implementation__Frozen(3);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[4] = 
			DataStore[4] = new StringParameter__Implementation__Frozen(4);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[5] = 
			DataStore[5] = new StringParameter__Implementation__Frozen(5);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[14] = 
			DataStore[14] = new StringParameter__Implementation__Frozen(14);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[23] = 
			DataStore[23] = new StringParameter__Implementation__Frozen(23);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[30] = 
			DataStore[30] = new StringParameter__Implementation__Frozen(30);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[33] = 
			DataStore[33] = new StringParameter__Implementation__Frozen(33);

		}

		internal new static void FillDataStore() {
			DataStore[1].Description = null;
			DataStore[1].IsList = false;
			DataStore[1].IsReturnParameter = false;
			DataStore[1].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[83];
			DataStore[1].ParameterName = @"TestString";
			DataStore[1].Seal();
			DataStore[3].Description = null;
			DataStore[3].IsList = false;
			DataStore[3].IsReturnParameter = true;
			DataStore[3].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[3].ParameterName = @"ReturnParameter";
			DataStore[3].Seal();
			DataStore[4].Description = null;
			DataStore[4].IsList = false;
			DataStore[4].IsReturnParameter = true;
			DataStore[4].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[46];
			DataStore[4].ParameterName = @"ReturnParameter";
			DataStore[4].Seal();
			DataStore[5].Description = null;
			DataStore[5].IsList = false;
			DataStore[5].IsReturnParameter = true;
			DataStore[5].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[5].ParameterName = @"ReturnParameter";
			DataStore[5].Seal();
			DataStore[14].Description = null;
			DataStore[14].IsList = false;
			DataStore[14].IsReturnParameter = false;
			DataStore[14].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[96];
			DataStore[14].ParameterName = @"message";
			DataStore[14].Seal();
			DataStore[23].Description = null;
			DataStore[23].IsList = false;
			DataStore[23].IsReturnParameter = true;
			DataStore[23].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[120];
			DataStore[23].ParameterName = @"ReturnParameter";
			DataStore[23].Seal();
			DataStore[30].Description = null;
			DataStore[30].IsList = false;
			DataStore[30].IsReturnParameter = true;
			DataStore[30].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[30].ParameterName = @"result";
			DataStore[30].Seal();
			DataStore[33].Description = null;
			DataStore[33].IsList = false;
			DataStore[33].IsReturnParameter = true;
			DataStore[33].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[141];
			DataStore[33].ParameterName = @"result";
			DataStore[33].Seal();
	
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