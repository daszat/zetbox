
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
    /// Metadefinition Object for String Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StringProperty")]
    public class StringProperty__Implementation__ : Kistl.App.Base.ValueTypeProperty__Implementation__, StringProperty
    {


        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Length != value)
                {
                    NotifyPropertyChanging("Length");
                    _Length = value;
                    NotifyPropertyChanged("Length");
                }
            }
        }
        private int _Length;

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_StringProperty != null)
            {
                OnGetGUIRepresentation_StringProperty(this, e);
            }
            else
            {
                base.GetGUIRepresentation();
            }
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<StringProperty> OnGetGUIRepresentation_StringProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_StringProperty != null)
            {
                OnGetPropertyType_StringProperty(this, e);
            }
            else
            {
                base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<StringProperty> OnGetPropertyType_StringProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_StringProperty != null)
            {
                OnGetPropertyTypeString_StringProperty(this, e);
            }
            else
            {
                base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<StringProperty> OnGetPropertyTypeString_StringProperty;



		public override Type GetInterfaceType()
		{
			return typeof(StringProperty);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StringProperty != null)
            {
                OnToString_StringProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StringProperty> OnToString_StringProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StringProperty != null) OnPreSave_StringProperty(this);
        }
        public event ObjectEventHandler<StringProperty> OnPreSave_StringProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StringProperty != null) OnPostSave_StringProperty(this);
        }
        public event ObjectEventHandler<StringProperty> OnPostSave_StringProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Length, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Length, binStream);
        }

#endregion

    }


}