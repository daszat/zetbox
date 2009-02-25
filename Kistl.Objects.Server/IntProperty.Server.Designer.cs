
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for Int Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="IntProperty")]
    [System.Diagnostics.DebuggerDisplay("IntProperty")]
    public class IntProperty__Implementation__ : Kistl.App.Base.ValueTypeProperty__Implementation__, IntProperty
    {


        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_IntProperty != null)
            {
                OnGetGUIRepresentation_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<IntProperty> OnGetGUIRepresentation_IntProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_IntProperty != null)
            {
                OnGetPropertyType_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<IntProperty> OnGetPropertyType_IntProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_IntProperty != null)
            {
                OnGetPropertyTypeString_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<IntProperty> OnGetPropertyTypeString_IntProperty;



		public override Type GetInterfaceType()
		{
			return typeof(IntProperty);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IntProperty != null)
            {
                OnToString_IntProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IntProperty> OnToString_IntProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IntProperty != null) OnPreSave_IntProperty(this);
        }
        public event ObjectEventHandler<IntProperty> OnPreSave_IntProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IntProperty != null) OnPostSave_IntProperty(this);
        }
        public event ObjectEventHandler<IntProperty> OnPostSave_IntProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

#endregion

    }


}