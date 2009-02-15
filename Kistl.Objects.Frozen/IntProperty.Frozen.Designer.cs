
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
    /// Metadefinition Object for Int Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IntProperty")]
    public class IntProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, IntProperty
    {


        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_IntProperty != null)
            {
                OnGetPropertyTypeString_IntProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<IntProperty> OnGetPropertyTypeString_IntProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_IntProperty != null)
            {
                OnGetGUIRepresentation_IntProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<IntProperty> OnGetGUIRepresentation_IntProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_IntProperty != null)
            {
                OnGetPropertyType_IntProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<IntProperty> OnGetPropertyType_IntProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IntProperty != null)
            {
                OnToString_IntProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IntProperty> OnToString_IntProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IntProperty != null) OnPreSave_IntProperty(this);
        }
        public event ObjectEventHandler<IntProperty> OnPreSave_IntProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IntProperty != null) OnPostSave_IntProperty(this);
        }
        public event ObjectEventHandler<IntProperty> OnPostSave_IntProperty;


        internal IntProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, IntProperty__Implementation__Frozen> DataStore = new Dictionary<int, IntProperty__Implementation__Frozen>(7);
		static IntProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[28] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[28] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[28] = 
			DataStore[28] = new IntProperty__Implementation__Frozen(null, 28);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[126] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[126] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[126] = 
			DataStore[126] = new IntProperty__Implementation__Frozen(null, 126);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[135] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[135] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[135] = 
			DataStore[135] = new IntProperty__Implementation__Frozen(null, 135);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[168] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[168] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[168] = 
			DataStore[168] = new IntProperty__Implementation__Frozen(null, 168);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[169] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[169] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[169] = 
			DataStore[169] = new IntProperty__Implementation__Frozen(null, 169);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[172] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[172] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[172] = 
			DataStore[172] = new IntProperty__Implementation__Frozen(null, 172);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[173] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[173] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[173] = 
			DataStore[173] = new IntProperty__Implementation__Frozen(null, 173);

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