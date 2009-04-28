
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
    
		public Interface__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Interface != null)
            {
                OnGetDataType_Interface(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
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
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<Interface> OnGetDataTypeString_Interface;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Interface));
		}

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


        internal Interface__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, Interface__Implementation__Frozen> DataStore = new Dictionary<int, Interface__Implementation__Frozen>(3);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[48] = 
			DataStore[48] = new Interface__Implementation__Frozen(48);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[52] = 
			DataStore[52] = new Interface__Implementation__Frozen(52);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[88] = 
			DataStore[88] = new Interface__Implementation__Frozen(88);

		}

		internal new static void FillDataStore() {
			DataStore[48].ClassName = @"ITestInterface";
			DataStore[48].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[1];
			DataStore[48].Description = @"A Test Interface";
			DataStore[48].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[48].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(1) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[90],
});
			DataStore[48].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[48].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(3) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[107],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[108],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[111],
});
			DataStore[48].Seal();
			DataStore[52].ClassName = @"IRenderer";
			DataStore[52].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[2];
			DataStore[52].Description = null;
			DataStore[52].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[52].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[96],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[97],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[98],
});
			DataStore[52].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[52].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[113],
});
			DataStore[52].Seal();
			DataStore[88].ClassName = @"IExportable";
			DataStore[88].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[10];
			DataStore[88].Description = @"Marks a DataType as exportable";
			DataStore[88].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[88].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[88].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[88].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[251],
});
			DataStore[88].Seal();
	
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