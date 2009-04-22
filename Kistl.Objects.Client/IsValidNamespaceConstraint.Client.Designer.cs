
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
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IsValidNamespaceConstraint")]
    public class IsValidNamespaceConstraint__Implementation__ : Kistl.App.Base.IsValidIdentifierConstraint__Implementation__, IsValidNamespaceConstraint
    {
    
		public IsValidNamespaceConstraint__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IsValidNamespaceConstraint != null)
            {
                OnGetErrorText_IsValidNamespaceConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<IsValidNamespaceConstraint> OnGetErrorText_IsValidNamespaceConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IsValidNamespaceConstraint != null)
            {
                OnIsValid_IsValidNamespaceConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
		public event IsValid_Handler<IsValidNamespaceConstraint> OnIsValid_IsValidNamespaceConstraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(IsValidNamespaceConstraint));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (IsValidNamespaceConstraint)obj;
			var otherImpl = (IsValidNamespaceConstraint__Implementation__)obj;
			var me = (IsValidNamespaceConstraint)this;

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
            if (OnToString_IsValidNamespaceConstraint != null)
            {
                OnToString_IsValidNamespaceConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IsValidNamespaceConstraint> OnToString_IsValidNamespaceConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IsValidNamespaceConstraint != null) OnPreSave_IsValidNamespaceConstraint(this);
        }
        public event ObjectEventHandler<IsValidNamespaceConstraint> OnPreSave_IsValidNamespaceConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IsValidNamespaceConstraint != null) OnPostSave_IsValidNamespaceConstraint(this);
        }
        public event ObjectEventHandler<IsValidNamespaceConstraint> OnPostSave_IsValidNamespaceConstraint;



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

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
        }

#endregion

    }


}