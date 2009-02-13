
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
    /// Metadefinition Object for Interfaces.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Interface")]
    public class Interface__Implementation__ : Kistl.App.Base.DataType__Implementation__, Interface
    {


        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Interface != null)
            {
                OnGetDataTypeString_Interface(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<Interface> OnGetDataTypeString_Interface;



        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Interface != null)
            {
                OnGetDataType_Interface(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<Interface> OnGetDataType_Interface;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Interface != null)
            {
                OnToString_Interface(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Interface> OnToString_Interface;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Interface != null) OnPreSave_Interface(this);
        }
        public event ObjectEventHandler<Interface> OnPreSave_Interface;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Interface != null) OnPostSave_Interface(this);
        }
        public event ObjectEventHandler<Interface> OnPostSave_Interface;


        internal Interface__Implementation__(FrozenContext ctx, int id)
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