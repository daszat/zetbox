
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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IsValidNamespaceConstraint")]
    public class IsValidNamespaceConstraint__Implementation__Frozen : Kistl.App.Base.IsValidIdentifierConstraint__Implementation__Frozen, IsValidNamespaceConstraint
    {


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IsValidNamespaceConstraint != null)
            {
                OnGetErrorText_IsValidNamespaceConstraint(this, e, constrainedValue, constrainedObject);
            };
            return e.Result;
        }
		public event GetErrorText_Handler<IsValidNamespaceConstraint> OnGetErrorText_IsValidNamespaceConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IsValidNamespaceConstraint != null)
            {
                OnIsValid_IsValidNamespaceConstraint(this, e, constrainedValue, constrainedObj);
            };
            return e.Result;
        }
		public event IsValid_Handler<IsValidNamespaceConstraint> OnIsValid_IsValidNamespaceConstraint;



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


        internal IsValidNamespaceConstraint__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, IsValidNamespaceConstraint__Implementation__Frozen> DataStore = new Dictionary<int, IsValidNamespaceConstraint__Implementation__Frozen>(1);
		static IsValidNamespaceConstraint__Implementation__Frozen()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[198] = 
			Kistl.App.Base.IsValidIdentifierConstraint__Implementation__Frozen.DataStore[198] = 
			DataStore[198] = new IsValidNamespaceConstraint__Implementation__Frozen(null, 198);

		}

		internal new static void FillDataStore() {
			DataStore[198].Reason = null;
			DataStore[198].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[42];
			DataStore[198].Seal();
	
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