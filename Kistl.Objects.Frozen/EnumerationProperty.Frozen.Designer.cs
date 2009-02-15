
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
    /// Metadefinition Object for Enumeration Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumerationProperty")]
    public class EnumerationProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, EnumerationProperty
    {


        /// <summary>
        /// Enumeration der Eigenschaft
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                return _Enumeration;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Enumeration != value)
                {
                    NotifyPropertyChanging("Enumeration");
                    _Enumeration = value;
                    NotifyPropertyChanged("Enumeration");;
                }
            }
        }
        private Kistl.App.Base.Enumeration _Enumeration;

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_EnumerationProperty != null)
            {
                OnGetPropertyTypeString_EnumerationProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_EnumerationProperty != null)
            {
                OnGetGUIRepresentation_EnumerationProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<EnumerationProperty> OnGetGUIRepresentation_EnumerationProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_EnumerationProperty != null)
            {
                OnGetPropertyType_EnumerationProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<EnumerationProperty> OnGetPropertyType_EnumerationProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumerationProperty != null)
            {
                OnToString_EnumerationProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<EnumerationProperty> OnToString_EnumerationProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationProperty != null) OnPreSave_EnumerationProperty(this);
        }
        public event ObjectEventHandler<EnumerationProperty> OnPreSave_EnumerationProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationProperty != null) OnPostSave_EnumerationProperty(this);
        }
        public event ObjectEventHandler<EnumerationProperty> OnPostSave_EnumerationProperty;


        internal EnumerationProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, EnumerationProperty__Implementation__Frozen> DataStore = new Dictionary<int, EnumerationProperty__Implementation__Frozen>(9);
		static EnumerationProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[110] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[110] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[110] = 
			DataStore[110] = new EnumerationProperty__Implementation__Frozen(null, 110);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[111] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[111] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[111] = 
			DataStore[111] = new EnumerationProperty__Implementation__Frozen(null, 111);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[113] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[113] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[113] = 
			DataStore[113] = new EnumerationProperty__Implementation__Frozen(null, 113);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[117] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[117] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[117] = 
			DataStore[117] = new EnumerationProperty__Implementation__Frozen(null, 117);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[118] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[118] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[118] = 
			DataStore[118] = new EnumerationProperty__Implementation__Frozen(null, 118);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[137] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[137] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[137] = 
			DataStore[137] = new EnumerationProperty__Implementation__Frozen(null, 137);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[150] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[150] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[150] = 
			DataStore[150] = new EnumerationProperty__Implementation__Frozen(null, 150);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[183] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[183] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[183] = 
			DataStore[183] = new EnumerationProperty__Implementation__Frozen(null, 183);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[210] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[210] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[210] = 
			DataStore[210] = new EnumerationProperty__Implementation__Frozen(null, 210);

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