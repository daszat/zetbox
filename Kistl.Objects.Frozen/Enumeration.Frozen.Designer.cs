
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
    public class Enumeration__Implementation__Frozen : Kistl.App.Base.DataType__Implementation__Frozen, Enumeration
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
                    _EnumerationEntries = new ReadOnlyCollection<Kistl.App.Base.EnumerationEntry>(new List<Kistl.App.Base.EnumerationEntry>(0));
                return _EnumerationEntries;
            }
internal set { _EnumerationEntries = (ReadOnlyCollection<Kistl.App.Base.EnumerationEntry>)value; }
        }
        private ReadOnlyCollection<Kistl.App.Base.EnumerationEntry> _EnumerationEntries;

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


        internal Enumeration__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, Enumeration__Implementation__Frozen> DataStore = new Dictionary<int, Enumeration__Implementation__Frozen>(4);
		static Enumeration__Implementation__Frozen()
		{
			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[50] = 
			DataStore[50] = new Enumeration__Implementation__Frozen(null, 50);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[53] = 
			DataStore[53] = new Enumeration__Implementation__Frozen(null, 53);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[55] = 
			DataStore[55] = new Enumeration__Implementation__Frozen(null, 55);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[78] = 
			DataStore[78] = new Enumeration__Implementation__Frozen(null, 78);

		}

		internal new static void FillDataStore() {
			DataStore[50].EnumerationEntries = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.EnumerationEntry>(new List<Kistl.App.Base.EnumerationEntry>(2) {
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[2],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[3],
})
;
			DataStore[53].EnumerationEntries = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.EnumerationEntry>(new List<Kistl.App.Base.EnumerationEntry>(3) {
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[5],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[6],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[7],
})
;
			DataStore[55].EnumerationEntries = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.EnumerationEntry>(new List<Kistl.App.Base.EnumerationEntry>(20) {
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[40],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[41],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[42],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[43],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[44],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[45],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[46],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[47],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[48],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[49],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[50],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[51],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[52],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[53],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[54],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[55],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[56],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[57],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[58],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[59],
})
;
			DataStore[78].EnumerationEntries = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.EnumerationEntry>(new List<Kistl.App.Base.EnumerationEntry>(3) {
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[60],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[61],
Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[62],
})
;
	
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