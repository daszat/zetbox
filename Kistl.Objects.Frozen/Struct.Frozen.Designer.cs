
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
    /// Metadefinition Object for Structs.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Struct")]
    public class Struct__Implementation__Frozen : Kistl.App.Base.DataType__Implementation__Frozen, Struct
    {


        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Struct != null)
            {
                OnGetDataType_Struct(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
		public event GetDataType_Handler<Struct> OnGetDataType_Struct;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Struct != null)
            {
                OnGetDataTypeString_Struct(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<Struct> OnGetDataTypeString_Struct;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Struct));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Struct != null)
            {
                OnToString_Struct(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Struct> OnToString_Struct;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Struct != null) OnPreSave_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnPreSave_Struct;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Struct != null) OnPostSave_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnPostSave_Struct;


        internal Struct__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, Struct__Implementation__Frozen> DataStore = new Dictionary<int, Struct__Implementation__Frozen>(1);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[63] = 
			DataStore[63] = new Struct__Implementation__Frozen(63);

		}

		internal new static void FillDataStore() {
			DataStore[63].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseProperty>(new List<Kistl.App.Base.BaseProperty>(2) {
Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[127],
Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[128],
});
			DataStore[63].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[63].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[63].DefaultIcon = null;
			DataStore[63].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[63].ClassName = @"TestPhoneStruct";
			DataStore[63].Description = null;
			DataStore[63].Seal();
	
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