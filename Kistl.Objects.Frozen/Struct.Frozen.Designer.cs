
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
    /// Metadefinition Object for Structs.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Struct")]
    public class Struct__Implementation__ : Kistl.App.Base.DataType__Implementation__, Struct
    {


        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Struct != null)
            {
                OnGetDataTypeString_Struct(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<Struct> OnGetDataTypeString_Struct;



        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Struct != null)
            {
                OnGetDataType_Struct(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<Struct> OnGetDataType_Struct;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Struct != null)
            {
                OnToString_Struct(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Struct> OnToString_Struct;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Struct != null) OnPreSave_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnPreSave_Struct;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Struct != null) OnPostSave_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnPostSave_Struct;


        internal Struct__Implementation__(FrozenContext ctx, int id)
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