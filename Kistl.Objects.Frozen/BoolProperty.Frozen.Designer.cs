
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
    /// Metadefinition Object for Bool Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BoolProperty")]
    public class BoolProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, BoolProperty
    {


        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BoolProperty != null)
            {
                OnGetPropertyTypeString_BoolProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<BoolProperty> OnGetPropertyTypeString_BoolProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_BoolProperty != null)
            {
                OnGetGUIRepresentation_BoolProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<BoolProperty> OnGetGUIRepresentation_BoolProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BoolProperty != null)
            {
                OnGetPropertyType_BoolProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<BoolProperty> OnGetPropertyType_BoolProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BoolProperty != null)
            {
                OnToString_BoolProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BoolProperty> OnToString_BoolProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BoolProperty != null) OnPreSave_BoolProperty(this);
        }
        public event ObjectEventHandler<BoolProperty> OnPreSave_BoolProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BoolProperty != null) OnPostSave_BoolProperty(this);
        }
        public event ObjectEventHandler<BoolProperty> OnPostSave_BoolProperty;


        internal BoolProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, BoolProperty__Implementation__Frozen> DataStore = new Dictionary<int, BoolProperty__Implementation__Frozen>(11);
		static BoolProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[11] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[11] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[11] = 
			DataStore[11] = new BoolProperty__Implementation__Frozen(null, 11);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[26] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[26] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[26] = 
			DataStore[26] = new BoolProperty__Implementation__Frozen(null, 26);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[83] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[83] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[83] = 
			DataStore[83] = new BoolProperty__Implementation__Frozen(null, 83);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[84] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[84] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[84] = 
			DataStore[84] = new BoolProperty__Implementation__Frozen(null, 84);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[94] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[94] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[94] = 
			DataStore[94] = new BoolProperty__Implementation__Frozen(null, 94);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[95] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[95] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[95] = 
			DataStore[95] = new BoolProperty__Implementation__Frozen(null, 95);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[116] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[116] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[116] = 
			DataStore[116] = new BoolProperty__Implementation__Frozen(null, 116);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[119] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[119] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[119] = 
			DataStore[119] = new BoolProperty__Implementation__Frozen(null, 119);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[124] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[124] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[124] = 
			DataStore[124] = new BoolProperty__Implementation__Frozen(null, 124);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[174] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[174] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[174] = 
			DataStore[174] = new BoolProperty__Implementation__Frozen(null, 174);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[204] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[204] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[204] = 
			DataStore[204] = new BoolProperty__Implementation__Frozen(null, 204);

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