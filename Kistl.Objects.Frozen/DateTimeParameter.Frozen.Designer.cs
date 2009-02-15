
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
    /// Metadefinition Object for DateTime Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DateTimeParameter")]
    public class DateTimeParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, DateTimeParameter
    {


        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_DateTimeParameter != null)
            {
                OnGetParameterTypeString_DateTimeParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterTypeString_Handler<DateTimeParameter> OnGetParameterTypeString_DateTimeParameter;



        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_DateTimeParameter != null)
            {
                OnGetParameterType_DateTimeParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterType_Handler<DateTimeParameter> OnGetParameterType_DateTimeParameter;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DateTimeParameter != null)
            {
                OnToString_DateTimeParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DateTimeParameter> OnToString_DateTimeParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DateTimeParameter != null) OnPreSave_DateTimeParameter(this);
        }
        public event ObjectEventHandler<DateTimeParameter> OnPreSave_DateTimeParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DateTimeParameter != null) OnPostSave_DateTimeParameter(this);
        }
        public event ObjectEventHandler<DateTimeParameter> OnPostSave_DateTimeParameter;


        internal DateTimeParameter__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, DateTimeParameter__Implementation__Frozen> DataStore = new Dictionary<int, DateTimeParameter__Implementation__Frozen>(4);
		static DateTimeParameter__Implementation__Frozen()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[8] = 
			DataStore[8] = new DateTimeParameter__Implementation__Frozen(null, 8);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[9] = 
			DataStore[9] = new DateTimeParameter__Implementation__Frozen(null, 9);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[12] = 
			DataStore[12] = new DateTimeParameter__Implementation__Frozen(null, 12);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[13] = 
			DataStore[13] = new DateTimeParameter__Implementation__Frozen(null, 13);

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