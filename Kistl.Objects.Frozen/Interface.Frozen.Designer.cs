
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
    /// Metadefinition Object for Interfaces.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Interface")]
    public class Interface__Implementation__Frozen : Kistl.App.Base.DataType__Implementation__Frozen, Interface
    {


        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Interface != null)
            {
                OnGetDataType_Interface(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<Interface> OnGetDataType_Interface;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Interface != null)
            {
                OnGetDataTypeString_Interface(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<Interface> OnGetDataTypeString_Interface;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Interface != null)
            {
                OnToString_Interface(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Interface> OnToString_Interface;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Interface != null) OnPreSave_Interface(this);
        }
        public event ObjectEventHandler<Interface> OnPreSave_Interface;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Interface != null) OnPostSave_Interface(this);
        }
        public event ObjectEventHandler<Interface> OnPostSave_Interface;


        internal Interface__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, Interface__Implementation__Frozen> DataStore = new Dictionary<int, Interface__Implementation__Frozen>(2);
		static Interface__Implementation__Frozen()
		{
			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[48] = 
			DataStore[48] = new Interface__Implementation__Frozen(null, 48);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[52] = 
			DataStore[52] = new Interface__Implementation__Frozen(null, 52);

		}

		internal new static void FillDataStore() {
			DataStore[48].ClassName = @"ITestInterface";
			DataStore[48].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseProperty>(new List<Kistl.App.Base.BaseProperty>(3) {
Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[107],
Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[108],
Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[111],
});
			DataStore[48].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(1) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[90],
});
			DataStore[48].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[48].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[1];
			DataStore[48].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[48].Description = @"A Test Interface";
			DataStore[48].Seal();
			DataStore[52].ClassName = @"IRenderer";
			DataStore[52].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseProperty>(new List<Kistl.App.Base.BaseProperty>(1) {
Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[113],
});
			DataStore[52].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[96],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[97],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[98],
});
			DataStore[52].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[52].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[2];
			DataStore[52].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[52].Description = null;
			DataStore[52].Seal();
	
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