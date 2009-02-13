
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
    /// Metadefinition Object for Enumerations.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Enumeration")]
    public class Enumeration__Implementation__ : Kistl.App.Base.DataType__Implementation__, Enumeration
    {


        /// <summary>
        /// Einträge der Enumeration
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.EnumerationEntry> EnumerationEntries
        {
            get
            {
                if (_EnumerationEntries == null)
                    _EnumerationEntries = new List<Kistl.App.Base.EnumerationEntry>();
                return _EnumerationEntries;
            }
        }
        private ICollection<Kistl.App.Base.EnumerationEntry> _EnumerationEntries;

        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Enumeration != null)
            {
                OnGetDataTypeString_Enumeration(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<Enumeration> OnGetDataTypeString_Enumeration;



        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Enumeration != null)
            {
                OnGetDataType_Enumeration(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<Enumeration> OnGetDataType_Enumeration;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Enumeration != null)
            {
                OnToString_Enumeration(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Enumeration> OnToString_Enumeration;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Enumeration != null) OnPreSave_Enumeration(this);
        }
        public event ObjectEventHandler<Enumeration> OnPreSave_Enumeration;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Enumeration != null) OnPostSave_Enumeration(this);
        }
        public event ObjectEventHandler<Enumeration> OnPostSave_Enumeration;


        internal Enumeration__Implementation__(FrozenContext ctx, int id)
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