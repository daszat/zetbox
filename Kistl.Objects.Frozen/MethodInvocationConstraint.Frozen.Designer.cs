
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
    [System.Diagnostics.DebuggerDisplay("MethodInvocationConstraint")]
    public class MethodInvocationConstraint__Implementation__Frozen : Kistl.App.Base.Constraint__Implementation__Frozen, MethodInvocationConstraint
    {
    
		public MethodInvocationConstraint__Implementation__Frozen()
		{
        }


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_MethodInvocationConstraint != null)
            {
                OnGetErrorText_MethodInvocationConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<MethodInvocationConstraint> OnGetErrorText_MethodInvocationConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_MethodInvocationConstraint != null)
            {
                OnIsValid_MethodInvocationConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
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


        internal MethodInvocationConstraint__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, MethodInvocationConstraint__Implementation__Frozen> DataStore = new Dictionary<int, MethodInvocationConstraint__Implementation__Frozen>(1);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[193] = 
			DataStore[193] = new MethodInvocationConstraint__Implementation__Frozen(193);

		}

		internal new static void FillDataStore() {
			DataStore[193].ConstrainedProperty = Kistl.App.Base.Property__Implementation__Frozen.DataStore[74];
			DataStore[193].Reason = @"Method.ObjectClass and InvokeOnObjectClass have to match.";
			DataStore[193].Seal();
	
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
        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}