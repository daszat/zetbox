
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
    /// Metadefinition Object for Int Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IntProperty")]
    public class IntProperty__Implementation__ : Kistl.App.Base.ValueTypeProperty__Implementation__, IntProperty
    {


        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_IntProperty != null)
            {
                OnGetPropertyTypeString_IntProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<IntProperty> OnGetPropertyTypeString_IntProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_IntProperty != null)
            {
                OnGetGUIRepresentation_IntProperty(this, e);
            };
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
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<IntProperty> OnGetPropertyType_IntProperty;



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


        internal IntProperty__Implementation__(FrozenContext ctx, int id)
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