
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

    using Kistl.API.Client;

    /// <summary>
    /// Metadefinition Object for Enumeration Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumerationProperty")]
    public class EnumerationProperty__Implementation__ : Kistl.App.Base.ValueTypeProperty__Implementation__, EnumerationProperty
    {


        /// <summary>
        /// Enumeration der Eigenschaft
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                if (fk_Enumeration.HasValue)
                    return Context.Find<Kistl.App.Base.Enumeration>(fk_Enumeration.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                fk_Enumeration = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Enumeration
        {
            get
            {
                return _fk_Enumeration;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Enumeration != value)
                {
                    NotifyPropertyChanging("Enumeration");
                    _fk_Enumeration = value;
                    NotifyPropertyChanging("Enumeration");
                }
            }
        }
        private int? _fk_Enumeration;

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_EnumerationProperty != null)
            {
                OnGetGUIRepresentation_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
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
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<EnumerationProperty> OnGetPropertyType_EnumerationProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_EnumerationProperty != null)
            {
                OnGetPropertyTypeString_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty;



		public override Type GetInterfaceType()
		{
			return typeof(EnumerationProperty);
		}

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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Enumeration, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Enumeration, binStream);
        }

#endregion

    }


}