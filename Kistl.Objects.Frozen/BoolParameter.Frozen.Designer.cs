
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
    /// Metadefinition Object for Bool Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BoolParameter")]
    public class BoolParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, BoolParameter
    {
    
		public BoolParameter__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_BoolParameter != null)
            {
                OnGetParameterType_BoolParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<BoolParameter> OnGetParameterType_BoolParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_BoolParameter != null)
            {
                OnGetParameterTypeString_BoolParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<BoolParameter> OnGetParameterTypeString_BoolParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(BoolParameter));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BoolParameter != null)
            {
                OnToString_BoolParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BoolParameter> OnToString_BoolParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BoolParameter != null) OnPreSave_BoolParameter(this);
        }
        public event ObjectEventHandler<BoolParameter> OnPreSave_BoolParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BoolParameter != null) OnPostSave_BoolParameter(this);
        }
        public event ObjectEventHandler<BoolParameter> OnPostSave_BoolParameter;


        internal BoolParameter__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, BoolParameter__Implementation__Frozen> DataStore = new Dictionary<int, BoolParameter__Implementation__Frozen>(3);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[7] = 
			DataStore[7] = new BoolParameter__Implementation__Frozen(7);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[29] = 
			DataStore[29] = new BoolParameter__Implementation__Frozen(29);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[38] = 
			DataStore[38] = new BoolParameter__Implementation__Frozen(38);

		}

		internal new static void FillDataStore() {
			DataStore[7].Description = null;
			DataStore[7].IsList = false;
			DataStore[7].IsReturnParameter = false;
			DataStore[7].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[83];
			DataStore[7].ParameterName = @"TestBool";
			DataStore[7].Seal();
			DataStore[29].Description = null;
			DataStore[29].IsList = false;
			DataStore[29].IsReturnParameter = true;
			DataStore[29].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[29].ParameterName = @"return";
			DataStore[29].Seal();
			DataStore[38].Description = @"whether to return null (false) or throw an Exception (true) on error";
			DataStore[38].IsList = false;
			DataStore[38].IsReturnParameter = false;
			DataStore[38].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[151];
			DataStore[38].ParameterName = @"throwOnError";
			DataStore[38].Seal();
	
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