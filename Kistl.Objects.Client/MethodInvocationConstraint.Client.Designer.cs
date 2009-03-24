
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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MethodInvocationConstraint")]
    public class MethodInvocationConstraint__Implementation__ : Kistl.App.Base.Constraint__Implementation__, MethodInvocationConstraint
    {
    
		public MethodInvocationConstraint__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_MethodInvocationConstraint != null)
            {
                OnGetErrorText_MethodInvocationConstraint(this, e, constrainedValue, constrainedObject);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedValue, constrainedObject);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<MethodInvocationConstraint> OnGetErrorText_MethodInvocationConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_MethodInvocationConstraint != null)
            {
                OnIsValid_MethodInvocationConstraint(this, e, constrainedValue, constrainedObj);
            }
            else
            {
                e.Result = base.IsValid(constrainedValue, constrainedObj);
            }
            return e.Result;
        }
		public event IsValid_Handler<MethodInvocationConstraint> OnIsValid_MethodInvocationConstraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(MethodInvocationConstraint));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocationConstraint != null)
            {
                OnToString_MethodInvocationConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<MethodInvocationConstraint> OnToString_MethodInvocationConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocationConstraint != null) OnPreSave_MethodInvocationConstraint(this);
        }
        public event ObjectEventHandler<MethodInvocationConstraint> OnPreSave_MethodInvocationConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocationConstraint != null) OnPostSave_MethodInvocationConstraint(this);
        }
        public event ObjectEventHandler<MethodInvocationConstraint> OnPostSave_MethodInvocationConstraint;



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