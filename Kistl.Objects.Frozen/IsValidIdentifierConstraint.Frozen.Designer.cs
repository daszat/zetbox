
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

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IsValidIdentifierConstraint != null)
            {
                OnGetErrorText_IsValidIdentifierConstraint(this, e, constrainedValue, constrainedObject);
            };
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
            };
            return e.Result;
        }
		public event IsValid_Handler<IsValidIdentifierConstraint> OnIsValid_IsValidIdentifierConstraint;



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


        internal IsValidIdentifierConstraint__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, IsValidIdentifierConstraint__Implementation__Frozen> DataStore = new Dictionary<int, IsValidIdentifierConstraint__Implementation__Frozen>(6);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[194] = 
			DataStore[194] = new IsValidIdentifierConstraint__Implementation__Frozen(194);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[195] = 
			DataStore[195] = new IsValidIdentifierConstraint__Implementation__Frozen(195);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[196] = 
			DataStore[196] = new IsValidIdentifierConstraint__Implementation__Frozen(196);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[199] = 
			DataStore[199] = new IsValidIdentifierConstraint__Implementation__Frozen(199);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[200] = 
			DataStore[200] = new IsValidIdentifierConstraint__Implementation__Frozen(200);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[201] = 
			DataStore[201] = new IsValidIdentifierConstraint__Implementation__Frozen(201);

		}

		internal new static void FillDataStore() {
			DataStore[194].Reason = null;
			DataStore[194].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[3];
			DataStore[194].Seal();
			DataStore[195].Reason = null;
			DataStore[195].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[1];
			DataStore[195].Seal();
			DataStore[196].Reason = null;
			DataStore[196].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[9];
			DataStore[196].Seal();
			DataStore[199].Reason = null;
			DataStore[199].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[30];
			DataStore[199].Seal();
			DataStore[200].Reason = null;
			DataStore[200].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[91];
			DataStore[200].Seal();
			DataStore[201].Reason = null;
			DataStore[201].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[136];
			DataStore[201].Seal();
	
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