
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
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_StringParameter != null)
            {
                OnGetParameterTypeString_StringParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterTypeString_Handler<StringParameter> OnGetParameterTypeString_StringParameter;



        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_StringParameter != null)
            {
                OnGetParameterType_StringParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterType_Handler<StringParameter> OnGetParameterType_StringParameter;



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


        internal StringParameter__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, StringParameter__Implementation__Frozen> DataStore = new Dictionary<int, StringParameter__Implementation__Frozen>(8);
		static StringParameter__Implementation__Frozen()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[1] = 
			DataStore[1] = new StringParameter__Implementation__Frozen(null, 1);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[3] = 
			DataStore[3] = new StringParameter__Implementation__Frozen(null, 3);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[4] = 
			DataStore[4] = new StringParameter__Implementation__Frozen(null, 4);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[5] = 
			DataStore[5] = new StringParameter__Implementation__Frozen(null, 5);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[14] = 
			DataStore[14] = new StringParameter__Implementation__Frozen(null, 14);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[23] = 
			DataStore[23] = new StringParameter__Implementation__Frozen(null, 23);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[30] = 
			DataStore[30] = new StringParameter__Implementation__Frozen(null, 30);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[33] = 
			DataStore[33] = new StringParameter__Implementation__Frozen(null, 33);

		}

		internal new static void FillDataStore() {
	
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