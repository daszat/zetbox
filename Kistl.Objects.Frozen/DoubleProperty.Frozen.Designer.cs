
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
    
		public DoubleProperty__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_DoubleProperty != null)
            {
                OnGetPropertyType_DoubleProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<DoubleProperty> OnGetPropertyType_DoubleProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_DoubleProperty != null)
            {
                OnGetPropertyTypeString_DoubleProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<DoubleProperty> OnGetPropertyTypeString_DoubleProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(DoubleProperty));
		}

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


        internal DoubleProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, DoubleProperty__Implementation__Frozen> DataStore = new Dictionary<int, DoubleProperty__Implementation__Frozen>(5);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[18] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[18] = 
			DataStore[18] = new DoubleProperty__Implementation__Frozen(18);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[23] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[23] = 
			DataStore[23] = new DoubleProperty__Implementation__Frozen(23);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[65] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[65] = 
			DataStore[65] = new DoubleProperty__Implementation__Frozen(65);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[89] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[89] = 
			DataStore[89] = new DoubleProperty__Implementation__Frozen(89);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[90] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[90] = 
			DataStore[90] = new DoubleProperty__Implementation__Frozen(90);

		}

		internal new static void FillDataStore() {
			DataStore[18].AltText = @"Aufwand in Stunden";
			DataStore[18].CategoryTags = null;
			DataStore[18].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[18].Description = @"Aufwand in Stunden";
			DataStore[18].IsIndexed = false;
			DataStore[18].IsList = false;
			DataStore[18].IsNullable = true;
			DataStore[18].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[18].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[18].PropertyName = @"Aufwand";
			DataStore[18].ValueModelDescriptor = null;
			DataStore[18].Seal();
			DataStore[23].AltText = null;
			DataStore[23].CategoryTags = null;
			DataStore[23].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[23].Description = null;
			DataStore[23].IsIndexed = false;
			DataStore[23].IsList = false;
			DataStore[23].IsNullable = true;
			DataStore[23].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[23].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[23].PropertyName = @"AufwandGes";
			DataStore[23].ValueModelDescriptor = null;
			DataStore[23].Seal();
			DataStore[65].AltText = @"Wert in EUR des Auftrages";
			DataStore[65].CategoryTags = null;
			DataStore[65].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[65].Description = @"Wert in EUR des Auftrages";
			DataStore[65].IsIndexed = false;
			DataStore[65].IsList = false;
			DataStore[65].IsNullable = true;
			DataStore[65].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[65].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[65].PropertyName = @"Auftragswert";
			DataStore[65].ValueModelDescriptor = null;
			DataStore[65].Seal();
			DataStore[89].AltText = @"Maximal erlaubte Stundenanzahl";
			DataStore[89].CategoryTags = @"Summary,Main";
			DataStore[89].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[89].Description = @"Maximal erlaubte Stundenanzahl";
			DataStore[89].IsIndexed = false;
			DataStore[89].IsList = false;
			DataStore[89].IsNullable = true;
			DataStore[89].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[89].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[89].PropertyName = @"MaxStunden";
			DataStore[89].ValueModelDescriptor = null;
			DataStore[89].Seal();
			DataStore[90].AltText = @"Aktuell gebuchte Stunden";
			DataStore[90].CategoryTags = @"Summary,Main";
			DataStore[90].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[90].Description = @"Aktuell gebuchte Stunden";
			DataStore[90].IsIndexed = false;
			DataStore[90].IsList = false;
			DataStore[90].IsNullable = true;
			DataStore[90].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[90].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[90].PropertyName = @"AktuelleStunden";
			DataStore[90].ValueModelDescriptor = null;
			DataStore[90].Seal();
	
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