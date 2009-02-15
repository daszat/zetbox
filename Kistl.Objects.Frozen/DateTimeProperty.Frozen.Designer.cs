
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
    /// Metadefinition Object for DateTime Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DateTimeProperty")]
    public class DateTimeProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, DateTimeProperty
    {


        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_DateTimeProperty != null)
            {
                OnGetPropertyTypeString_DateTimeProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<DateTimeProperty> OnGetPropertyTypeString_DateTimeProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_DateTimeProperty != null)
            {
                OnGetGUIRepresentation_DateTimeProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<DateTimeProperty> OnGetGUIRepresentation_DateTimeProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_DateTimeProperty != null)
            {
                OnGetPropertyType_DateTimeProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<DateTimeProperty> OnGetPropertyType_DateTimeProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DateTimeProperty != null)
            {
                OnToString_DateTimeProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DateTimeProperty> OnToString_DateTimeProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DateTimeProperty != null) OnPreSave_DateTimeProperty(this);
        }
        public event ObjectEventHandler<DateTimeProperty> OnPreSave_DateTimeProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DateTimeProperty != null) OnPostSave_DateTimeProperty(this);
        }
        public event ObjectEventHandler<DateTimeProperty> OnPostSave_DateTimeProperty;


        internal DateTimeProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, DateTimeProperty__Implementation__Frozen> DataStore = new Dictionary<int, DateTimeProperty__Implementation__Frozen>(5);
		static DateTimeProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[16] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[16] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[16] = 
			DataStore[16] = new DateTimeProperty__Implementation__Frozen(null, 16);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[17] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[17] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[17] = 
			DataStore[17] = new DateTimeProperty__Implementation__Frozen(null, 17);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[38] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[38] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[38] = 
			DataStore[38] = new DateTimeProperty__Implementation__Frozen(null, 38);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[56] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[56] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[56] = 
			DataStore[56] = new DateTimeProperty__Implementation__Frozen(null, 56);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[133] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[133] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[133] = 
			DataStore[133] = new DateTimeProperty__Implementation__Frozen(null, 133);

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