
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
    /// Metadefinition Object for Double Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DoubleProperty")]
    public class DoubleProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, DoubleProperty
    {


        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_DoubleProperty != null)
            {
                OnGetPropertyTypeString_DoubleProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<DoubleProperty> OnGetPropertyTypeString_DoubleProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_DoubleProperty != null)
            {
                OnGetGUIRepresentation_DoubleProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<DoubleProperty> OnGetGUIRepresentation_DoubleProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_DoubleProperty != null)
            {
                OnGetPropertyType_DoubleProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<DoubleProperty> OnGetPropertyType_DoubleProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DoubleProperty != null)
            {
                OnToString_DoubleProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DoubleProperty> OnToString_DoubleProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DoubleProperty != null) OnPreSave_DoubleProperty(this);
        }
        public event ObjectEventHandler<DoubleProperty> OnPreSave_DoubleProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DoubleProperty != null) OnPostSave_DoubleProperty(this);
        }
        public event ObjectEventHandler<DoubleProperty> OnPostSave_DoubleProperty;


        internal DoubleProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, DoubleProperty__Implementation__Frozen> DataStore = new Dictionary<int, DoubleProperty__Implementation__Frozen>(6);
		static DoubleProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[18] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[18] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[18] = 
			DataStore[18] = new DoubleProperty__Implementation__Frozen(null, 18);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[23] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[23] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[23] = 
			DataStore[23] = new DoubleProperty__Implementation__Frozen(null, 23);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[57] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[57] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[57] = 
			DataStore[57] = new DoubleProperty__Implementation__Frozen(null, 57);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[65] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[65] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[65] = 
			DataStore[65] = new DoubleProperty__Implementation__Frozen(null, 65);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[89] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[89] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[89] = 
			DataStore[89] = new DoubleProperty__Implementation__Frozen(null, 89);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[90] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[90] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[90] = 
			DataStore[90] = new DoubleProperty__Implementation__Frozen(null, 90);

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