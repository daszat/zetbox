
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
    /// Metadefinition Object for Int Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IntParameter")]
    public class IntParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, IntParameter
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
            };
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
            };
            return e.Result;
        }
		public event GetParameterTypeString_Handler<IntParameter> OnGetParameterTypeString_IntParameter;



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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

#endregion

    }


}