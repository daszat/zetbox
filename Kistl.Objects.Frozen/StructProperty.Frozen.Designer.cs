
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
    public class StructProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, StructProperty
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


        internal StructProperty__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


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