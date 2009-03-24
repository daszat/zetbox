
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
    /// Metadefinition Object for BackReference Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BackReferenceProperty")]
    public class BackReferenceProperty__Implementation__Frozen : Kistl.App.Base.BaseProperty__Implementation__Frozen, BackReferenceProperty
    {
    
		public BackReferenceProperty__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Serialisierung der Liste zum Client
        /// </summary>
        // value type property
        public virtual bool PreFetchToClient
        {
            get
            {
                return _PreFetchToClient;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PreFetchToClient != value)
                {
                    NotifyPropertyChanging("PreFetchToClient");
                    _PreFetchToClient = value;
                    NotifyPropertyChanged("PreFetchToClient");
                }
            }
        }
        private bool _PreFetchToClient;

        /// <summary>
        /// Das Property, welches auf diese Klasse zeigt
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.ObjectReferenceProperty ReferenceProperty
        {
            get
            {
                return _ReferenceProperty;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ReferenceProperty != value)
                {
                    NotifyPropertyChanging("ReferenceProperty");
                    _ReferenceProperty = value;
                    NotifyPropertyChanged("ReferenceProperty");
                }
            }
        }
        private Kistl.App.Base.ObjectReferenceProperty _ReferenceProperty;

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_BackReferenceProperty != null)
            {
                OnGetGUIRepresentation_BackReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<BackReferenceProperty> OnGetGUIRepresentation_BackReferenceProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BackReferenceProperty != null)
            {
                OnGetPropertyType_BackReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<BackReferenceProperty> OnGetPropertyType_BackReferenceProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BackReferenceProperty != null)
            {
                OnGetPropertyTypeString_BackReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<BackReferenceProperty> OnGetPropertyTypeString_BackReferenceProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(BackReferenceProperty));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BackReferenceProperty != null)
            {
                OnToString_BackReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BackReferenceProperty> OnToString_BackReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BackReferenceProperty != null) OnPreSave_BackReferenceProperty(this);
        }
        public event ObjectEventHandler<BackReferenceProperty> OnPreSave_BackReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BackReferenceProperty != null) OnPostSave_BackReferenceProperty(this);
        }
        public event ObjectEventHandler<BackReferenceProperty> OnPostSave_BackReferenceProperty;


        internal BackReferenceProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, BackReferenceProperty__Implementation__Frozen> DataStore = new Dictionary<int, BackReferenceProperty__Implementation__Frozen>(0);
		internal new static void CreateInstances()
		{
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