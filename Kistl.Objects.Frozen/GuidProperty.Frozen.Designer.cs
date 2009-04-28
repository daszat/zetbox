
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
    /// Guid Property
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("GuidProperty")]
    public class GuidProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, GuidProperty
    {
    
		public GuidProperty__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_GuidProperty != null)
            {
                OnGetPropertyType_GuidProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<GuidProperty> OnGetPropertyType_GuidProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_GuidProperty != null)
            {
                OnGetPropertyTypeString_GuidProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<GuidProperty> OnGetPropertyTypeString_GuidProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(GuidProperty));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_GuidProperty != null)
            {
                OnToString_GuidProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<GuidProperty> OnToString_GuidProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_GuidProperty != null) OnPreSave_GuidProperty(this);
        }
        public event ObjectEventHandler<GuidProperty> OnPreSave_GuidProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_GuidProperty != null) OnPostSave_GuidProperty(this);
        }
        public event ObjectEventHandler<GuidProperty> OnPostSave_GuidProperty;


        internal GuidProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, GuidProperty__Implementation__Frozen> DataStore = new Dictionary<int, GuidProperty__Implementation__Frozen>(2);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[251] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[251] = 
			DataStore[251] = new GuidProperty__Implementation__Frozen(251);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[252] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[252] = 
			DataStore[252] = new GuidProperty__Implementation__Frozen(252);

		}

		internal new static void FillDataStore() {
			DataStore[251].AltText = @"Export Guid";
			DataStore[251].CategoryTags = @"Export";
			DataStore[251].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[251].Description = @"Export Guid";
			DataStore[251].IsIndexed = false;
			DataStore[251].IsList = false;
			DataStore[251].IsNullable = false;
			DataStore[251].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[251].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[88];
			DataStore[251].PropertyName = @"ExportGuid";
			DataStore[251].ValueModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[39];
			DataStore[251].Seal();
			DataStore[252].AltText = @"Export Guid";
			DataStore[252].CategoryTags = @"Export";
			DataStore[252].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[252].Description = @"Export Guid";
			DataStore[252].IsIndexed = false;
			DataStore[252].IsList = false;
			DataStore[252].IsNullable = false;
			DataStore[252].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[252].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[252].PropertyName = @"ExportGuid";
			DataStore[252].ValueModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[39];
			DataStore[252].Seal();
	
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