
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="IsValidIdentifierConstraint")]
    [System.Diagnostics.DebuggerDisplay("IsValidIdentifierConstraint")]
    public class IsValidIdentifierConstraint__Implementation__ : Kistl.App.Base.Constraint__Implementation__, IsValidIdentifierConstraint
    {


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IsValidIdentifierConstraint != null)
            {
                OnGetErrorText_IsValidIdentifierConstraint(this, e, constrainedValue, constrainedObject);
            }
            else
            {
                base.GetErrorText(constrainedValue, constrainedObject);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<IsValidIdentifierConstraint> OnGetErrorText_IsValidIdentifierConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IsValidIdentifierConstraint != null)
            {
                OnIsValid_IsValidIdentifierConstraint(this, e, constrainedValue, constrainedObj);
            }
            else
            {
                base.IsValid(constrainedValue, constrainedObj);
            }
            return e.Result;
        }
		public event IsValid_Handler<IsValidIdentifierConstraint> OnIsValid_IsValidIdentifierConstraint;



		public override Type GetInterfaceType()
		{
			return typeof(IsValidIdentifierConstraint);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IsValidIdentifierConstraint != null)
            {
                OnToString_IsValidIdentifierConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IsValidIdentifierConstraint> OnToString_IsValidIdentifierConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IsValidIdentifierConstraint != null) OnPreSave_IsValidIdentifierConstraint(this);
        }
        public event ObjectEventHandler<IsValidIdentifierConstraint> OnPreSave_IsValidIdentifierConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IsValidIdentifierConstraint != null) OnPostSave_IsValidIdentifierConstraint(this);
        }
        public event ObjectEventHandler<IsValidIdentifierConstraint> OnPostSave_IsValidIdentifierConstraint;




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