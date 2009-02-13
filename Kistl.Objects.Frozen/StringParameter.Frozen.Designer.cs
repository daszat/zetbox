
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
    public class StringParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, StringParameter
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


        internal StringParameter__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


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