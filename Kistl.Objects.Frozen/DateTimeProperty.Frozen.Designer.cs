
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
    
		public DateTimeProperty__Implementation__Frozen()
		{
        }


        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_DateTimeProperty != null)
            {
                OnGetGUIRepresentation_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
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
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<DateTimeProperty> OnGetPropertyType_DateTimeProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_DateTimeProperty != null)
            {
                OnGetPropertyTypeString_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<DateTimeProperty> OnGetPropertyTypeString_DateTimeProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(DateTimeProperty));
		}

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


        internal DateTimeProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, DateTimeProperty__Implementation__Frozen> DataStore = new Dictionary<int, DateTimeProperty__Implementation__Frozen>(5);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[16] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[16] = 
			DataStore[16] = new DateTimeProperty__Implementation__Frozen(16);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[17] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[17] = 
			DataStore[17] = new DateTimeProperty__Implementation__Frozen(17);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[38] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[38] = 
			DataStore[38] = new DateTimeProperty__Implementation__Frozen(38);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[56] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[56] = 
			DataStore[56] = new DateTimeProperty__Implementation__Frozen(56);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[133] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[133] = 
			DataStore[133] = new DateTimeProperty__Implementation__Frozen(133);

		}

		internal new static void FillDataStore() {
			DataStore[16].AltText = @"Start Datum";
			DataStore[16].CategoryTags = null;
			DataStore[16].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[16].Description = @"Start Datum";
			DataStore[16].IsIndexed = false;
			DataStore[16].IsList = false;
			DataStore[16].IsNullable = true;
			DataStore[16].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[16].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[16].PropertyName = @"DatumVon";
			DataStore[16].Seal();
			DataStore[17].AltText = @"Enddatum";
			DataStore[17].CategoryTags = null;
			DataStore[17].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[17].Description = @"Enddatum";
			DataStore[17].IsIndexed = false;
			DataStore[17].IsList = false;
			DataStore[17].IsNullable = true;
			DataStore[17].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[17].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[17].PropertyName = @"DatumBis";
			DataStore[17].Seal();
			DataStore[38].AltText = @"Herzlichen Glückwunsch zum Geburtstag";
			DataStore[38].CategoryTags = null;
			DataStore[38].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[38].Description = @"Herzlichen Glückwunsch zum Geburtstag";
			DataStore[38].IsIndexed = false;
			DataStore[38].IsList = false;
			DataStore[38].IsNullable = true;
			DataStore[38].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[38].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[38].PropertyName = @"Geburtstag";
			DataStore[38].Seal();
			DataStore[56].AltText = @"Datum";
			DataStore[56].CategoryTags = null;
			DataStore[56].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[132],
});
			DataStore[56].Description = @"Datum";
			DataStore[56].IsIndexed = false;
			DataStore[56].IsList = false;
			DataStore[56].IsNullable = false;
			DataStore[56].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[56].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[56].PropertyName = @"Datum";
			DataStore[56].Seal();
			DataStore[133].AltText = @"Happy Birthday!";
			DataStore[133].CategoryTags = null;
			DataStore[133].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[133].Description = @"Happy Birthday!";
			DataStore[133].IsIndexed = false;
			DataStore[133].IsList = false;
			DataStore[133].IsNullable = true;
			DataStore[133].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[133].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[133].PropertyName = @"Birthday";
			DataStore[133].Seal();
	
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