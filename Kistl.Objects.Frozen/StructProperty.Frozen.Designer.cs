
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
    /// Metadefinition Object for Struct Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StructProperty")]
    public class StructProperty__Implementation__Frozen : Kistl.App.Base.Property__Implementation__Frozen, StructProperty
    {


        /// <summary>
        /// Definition of this Struct
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Struct StructDefinition
        {
            get
            {
                return _StructDefinition;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_StructDefinition != value)
                {
                    NotifyPropertyChanging("StructDefinition");
                    _StructDefinition = value;
                    NotifyPropertyChanged("StructDefinition");;
                }
            }
        }
        private Kistl.App.Base.Struct _StructDefinition;

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_StructProperty != null)
            {
                OnGetGUIRepresentation_StructProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<StructProperty> OnGetGUIRepresentation_StructProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_StructProperty != null)
            {
                OnGetPropertyType_StructProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<StructProperty> OnGetPropertyType_StructProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_StructProperty != null)
            {
                OnGetPropertyTypeString_StructProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<StructProperty> OnGetPropertyTypeString_StructProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StructProperty != null)
            {
                OnToString_StructProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StructProperty> OnToString_StructProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StructProperty != null) OnPreSave_StructProperty(this);
        }
        public event ObjectEventHandler<StructProperty> OnPreSave_StructProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StructProperty != null) OnPostSave_StructProperty(this);
        }
        public event ObjectEventHandler<StructProperty> OnPostSave_StructProperty;


        internal StructProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, StructProperty__Implementation__Frozen> DataStore = new Dictionary<int, StructProperty__Implementation__Frozen>(2);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[131] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[131] = 
			DataStore[131] = new StructProperty__Implementation__Frozen(131);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[132] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[132] = 
			DataStore[132] = new StructProperty__Implementation__Frozen(132);

		}

		internal new static void FillDataStore() {
			DataStore[131].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[131].PropertyName = @"PhoneNumberMobile";
			DataStore[131].AltText = @"Mobile Phone Number";
			DataStore[131].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[131].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[131].Description = @"Mobile Phone Number";
			DataStore[131].IsList = false;
			DataStore[131].IsNullable = true;
			DataStore[131].IsIndexed = false;
			DataStore[131].Seal();
			DataStore[132].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[132].PropertyName = @"PhoneNumberOffice";
			DataStore[132].AltText = @"Office Phone Number";
			DataStore[132].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[132].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[132].Description = @"Office Phone Number";
			DataStore[132].IsList = false;
			DataStore[132].IsNullable = true;
			DataStore[132].IsIndexed = false;
			DataStore[132].Seal();
	
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