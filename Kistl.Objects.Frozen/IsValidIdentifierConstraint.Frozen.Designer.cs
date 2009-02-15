
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
    [System.Diagnostics.DebuggerDisplay("IsValidIdentifierConstraint")]
    public class IsValidIdentifierConstraint__Implementation__Frozen : Kistl.App.Base.Constraint__Implementation__Frozen, IsValidIdentifierConstraint
    {


        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedObj, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IsValidIdentifierConstraint != null)
            {
                OnIsValid_IsValidIdentifierConstraint(this, e, constrainedObj, constrainedValue);
            };
            return e.Result;
        }
		public event IsValid_Handler<IsValidIdentifierConstraint> OnIsValid_IsValidIdentifierConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IsValidIdentifierConstraint != null)
            {
                OnGetErrorText_IsValidIdentifierConstraint(this, e, constrainedObject, constrainedValue);
            };
            return e.Result;
        }
		public event GetErrorText_Handler<IsValidIdentifierConstraint> OnGetErrorText_IsValidIdentifierConstraint;



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


        internal IsValidIdentifierConstraint__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, IsValidIdentifierConstraint__Implementation__Frozen> DataStore = new Dictionary<int, IsValidIdentifierConstraint__Implementation__Frozen>(6);
		static IsValidIdentifierConstraint__Implementation__Frozen()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[194] = 
			DataStore[194] = new IsValidIdentifierConstraint__Implementation__Frozen(null, 194);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[195] = 
			DataStore[195] = new IsValidIdentifierConstraint__Implementation__Frozen(null, 195);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[196] = 
			DataStore[196] = new IsValidIdentifierConstraint__Implementation__Frozen(null, 196);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[199] = 
			DataStore[199] = new IsValidIdentifierConstraint__Implementation__Frozen(null, 199);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[200] = 
			DataStore[200] = new IsValidIdentifierConstraint__Implementation__Frozen(null, 200);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[201] = 
			DataStore[201] = new IsValidIdentifierConstraint__Implementation__Frozen(null, 201);

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