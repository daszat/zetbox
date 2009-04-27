
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
    
		public BoolProperty__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BoolProperty != null)
            {
                OnGetPropertyType_BoolProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<BoolProperty> OnGetPropertyType_BoolProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BoolProperty != null)
            {
                OnGetPropertyTypeString_BoolProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<BoolProperty> OnGetPropertyTypeString_BoolProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(BoolProperty));
		}

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


        internal BoolProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, BoolProperty__Implementation__Frozen> DataStore = new Dictionary<int, BoolProperty__Implementation__Frozen>(11);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[11] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[11] = 
			DataStore[11] = new BoolProperty__Implementation__Frozen(11);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[26] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[26] = 
			DataStore[26] = new BoolProperty__Implementation__Frozen(26);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[83] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[83] = 
			DataStore[83] = new BoolProperty__Implementation__Frozen(83);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[94] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[94] = 
			DataStore[94] = new BoolProperty__Implementation__Frozen(94);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[95] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[95] = 
			DataStore[95] = new BoolProperty__Implementation__Frozen(95);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[116] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[116] = 
			DataStore[116] = new BoolProperty__Implementation__Frozen(116);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[119] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[119] = 
			DataStore[119] = new BoolProperty__Implementation__Frozen(119);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[124] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[124] = 
			DataStore[124] = new BoolProperty__Implementation__Frozen(124);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[174] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[174] = 
			DataStore[174] = new BoolProperty__Implementation__Frozen(174);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[204] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[204] = 
			DataStore[204] = new BoolProperty__Implementation__Frozen(204);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[220] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[220] = 
			DataStore[220] = new BoolProperty__Implementation__Frozen(220);

		}

		internal new static void FillDataStore() {
			DataStore[11].AltText = null;
			DataStore[11].CategoryTags = @"DataModel";
			DataStore[11].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[143],
});
			DataStore[11].Description = null;
			DataStore[11].IsIndexed = false;
			DataStore[11].IsList = false;
			DataStore[11].IsNullable = false;
			DataStore[11].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[11].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[11].PropertyName = @"IsList";
			DataStore[11].ValueModelDescriptor = null;
			DataStore[11].Seal();
			DataStore[26].AltText = null;
			DataStore[26].CategoryTags = @"DataModel";
			DataStore[26].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[142],
});
			DataStore[26].Description = null;
			DataStore[26].IsIndexed = false;
			DataStore[26].IsList = false;
			DataStore[26].IsNullable = false;
			DataStore[26].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[26].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[26].PropertyName = @"IsNullable";
			DataStore[26].ValueModelDescriptor = null;
			DataStore[26].Seal();
			DataStore[83].AltText = @"Legt fest, ob es sich um ein Client-Assembly handelt.";
			DataStore[83].CategoryTags = null;
			DataStore[83].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[117],
});
			DataStore[83].Description = @"Legt fest, ob es sich um ein Client-Assembly handelt.";
			DataStore[83].IsIndexed = false;
			DataStore[83].IsList = false;
			DataStore[83].IsNullable = false;
			DataStore[83].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[83].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[83].PropertyName = @"IsClientAssembly";
			DataStore[83].ValueModelDescriptor = null;
			DataStore[83].Seal();
			DataStore[94].AltText = @"Parameter wird als List<> generiert";
			DataStore[94].CategoryTags = null;
			DataStore[94].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[111],
});
			DataStore[94].Description = @"Parameter wird als List<> generiert";
			DataStore[94].IsIndexed = false;
			DataStore[94].IsList = false;
			DataStore[94].IsNullable = false;
			DataStore[94].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[94].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[94].PropertyName = @"IsList";
			DataStore[94].ValueModelDescriptor = null;
			DataStore[94].Seal();
			DataStore[95].AltText = @"Es darf nur ein Return Parameter angegeben werden";
			DataStore[95].CategoryTags = null;
			DataStore[95].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[110],
});
			DataStore[95].Description = @"Es darf nur ein Return Parameter angegeben werden";
			DataStore[95].IsIndexed = false;
			DataStore[95].IsList = false;
			DataStore[95].IsNullable = false;
			DataStore[95].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[95].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[95].PropertyName = @"IsReturnParameter";
			DataStore[95].ValueModelDescriptor = null;
			DataStore[95].Seal();
			DataStore[116].AltText = @"Whether or not this Control can contain other Controls";
			DataStore[116].CategoryTags = null;
			DataStore[116].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[96],
});
			DataStore[116].Description = @"Whether or not this Control can contain other Controls";
			DataStore[116].IsIndexed = false;
			DataStore[116].IsList = false;
			DataStore[116].IsNullable = false;
			DataStore[116].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[116].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[116].PropertyName = @"IsContainer";
			DataStore[116].ValueModelDescriptor = null;
			DataStore[116].Seal();
			DataStore[119].AltText = @"Setting this to true marks the instances of this class as ""simple."" At first this will only mean that they'll be displayed inline.";
			DataStore[119].CategoryTags = @"DataModel";
			DataStore[119].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[93],
});
			DataStore[119].Description = @"Setting this to true marks the instances of this class as ""simple."" At first this will only mean that they'll be displayed inline.";
			DataStore[119].IsIndexed = false;
			DataStore[119].IsList = false;
			DataStore[119].IsNullable = false;
			DataStore[119].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[119].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[119].PropertyName = @"IsSimpleObject";
			DataStore[119].ValueModelDescriptor = null;
			DataStore[119].Seal();
			DataStore[124].AltText = @"Shows this Method in th GUI";
			DataStore[124].CategoryTags = null;
			DataStore[124].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[92],
});
			DataStore[124].Description = @"Shows this Method in th GUI";
			DataStore[124].IsIndexed = false;
			DataStore[124].IsList = false;
			DataStore[124].IsNullable = false;
			DataStore[124].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[124].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[124].PropertyName = @"IsDisplayable";
			DataStore[124].ValueModelDescriptor = null;
			DataStore[124].Seal();
			DataStore[174].AltText = @"if true then all Instances appear in FozenContext.";
			DataStore[174].CategoryTags = @"Physical";
			DataStore[174].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[202],
});
			DataStore[174].Description = @"if true then all Instances appear in FozenContext.";
			DataStore[174].IsIndexed = false;
			DataStore[174].IsList = false;
			DataStore[174].IsNullable = false;
			DataStore[174].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[174].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[174].PropertyName = @"IsFrozenObject";
			DataStore[174].ValueModelDescriptor = null;
			DataStore[174].Seal();
			DataStore[204].AltText = @"Whether or not a list-valued property has a index";
			DataStore[204].CategoryTags = @"DataModel";
			DataStore[204].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[244],
});
			DataStore[204].Description = @"Whether or not a list-valued property has a index";
			DataStore[204].IsIndexed = false;
			DataStore[204].IsList = false;
			DataStore[204].IsNullable = false;
			DataStore[204].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[204].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[204].PropertyName = @"IsIndexed";
			DataStore[204].ValueModelDescriptor = null;
			DataStore[204].Seal();
			DataStore[220].AltText = @"Is true, if this RelationEnd persists the order of its elements";
			DataStore[220].CategoryTags = null;
			DataStore[220].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[236],
});
			DataStore[220].Description = @"Is true, if this RelationEnd persists the order of its elements";
			DataStore[220].IsIndexed = false;
			DataStore[220].IsList = false;
			DataStore[220].IsNullable = false;
			DataStore[220].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[220].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82];
			DataStore[220].PropertyName = @"HasPersistentOrder";
			DataStore[220].ValueModelDescriptor = null;
			DataStore[220].Seal();
	
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