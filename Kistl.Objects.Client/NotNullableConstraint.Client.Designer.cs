
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
    [System.Diagnostics.DebuggerDisplay("NotNullableConstraint")]
    public class NotNullableConstraint__Implementation__ : Kistl.App.Base.Constraint__Implementation__, NotNullableConstraint
    {


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_NotNullableConstraint != null)
            {
                OnGetErrorText_NotNullableConstraint(this, e, constrainedValue, constrainedObject);
            };
            return e.Result;
        }
		public event GetErrorText_Handler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_NotNullableConstraint != null)
            {
                OnIsValid_NotNullableConstraint(this, e, constrainedValue, constrainedObj);
            };
            return e.Result;
        }
		public event IsValid_Handler<NotNullableConstraint> OnIsValid_NotNullableConstraint;



		public override Type GetInterfaceType()
		{
			return typeof(NotNullableConstraint);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_NotNullableConstraint != null)
            {
                OnToString_NotNullableConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<NotNullableConstraint> OnToString_NotNullableConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_NotNullableConstraint != null) OnPreSave_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnPreSave_NotNullableConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_NotNullableConstraint != null) OnPostSave_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnPostSave_NotNullableConstraint;




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