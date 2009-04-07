
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
    /// Metadefinition Object for Double Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DoubleParameter")]
    public class DoubleParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, DoubleParameter
    {
    
		public DoubleParameter__Implementation__()
		{
            {
            }
        }


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

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (DoubleParameter)obj;
			var otherImpl = (DoubleParameter__Implementation__)obj;
			var me = (DoubleParameter)this;

		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

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