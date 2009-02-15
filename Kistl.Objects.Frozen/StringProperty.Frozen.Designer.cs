
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
    /// Metadefinition Object for String Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StringProperty")]
    public class StringProperty__Implementation__Frozen : Kistl.App.Base.ValueTypeProperty__Implementation__Frozen, StringProperty
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
                    NotifyPropertyChanged("Length");;
                }
            }
        }
        private int _Length;

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_StringProperty != null)
            {
                OnGetPropertyTypeString_StringProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<StringProperty> OnGetPropertyTypeString_StringProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_StringProperty != null)
            {
                OnGetGUIRepresentation_StringProperty(this, e);
            };
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
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<StringProperty> OnGetPropertyType_StringProperty;



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


        internal StringProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, StringProperty__Implementation__Frozen> DataStore = new Dictionary<int, StringProperty__Implementation__Frozen>(48);
		static StringProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[1] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[1] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[1] = 
			DataStore[1] = new StringProperty__Implementation__Frozen(null, 1);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[3] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[3] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[3] = 
			DataStore[3] = new StringProperty__Implementation__Frozen(null, 3);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[9] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[9] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[9] = 
			DataStore[9] = new StringProperty__Implementation__Frozen(null, 9);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[13] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[13] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[13] = 
			DataStore[13] = new StringProperty__Implementation__Frozen(null, 13);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[15] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[15] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[15] = 
			DataStore[15] = new StringProperty__Implementation__Frozen(null, 15);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[20] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[20] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[20] = 
			DataStore[20] = new StringProperty__Implementation__Frozen(null, 20);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[30] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[30] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[30] = 
			DataStore[30] = new StringProperty__Implementation__Frozen(null, 30);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[39] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[39] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[39] = 
			DataStore[39] = new StringProperty__Implementation__Frozen(null, 39);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[40] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[40] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[40] = 
			DataStore[40] = new StringProperty__Implementation__Frozen(null, 40);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[41] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[41] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[41] = 
			DataStore[41] = new StringProperty__Implementation__Frozen(null, 41);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[42] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[42] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[42] = 
			DataStore[42] = new StringProperty__Implementation__Frozen(null, 42);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[43] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[43] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[43] = 
			DataStore[43] = new StringProperty__Implementation__Frozen(null, 43);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[48] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[48] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[48] = 
			DataStore[48] = new StringProperty__Implementation__Frozen(null, 48);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[50] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[50] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[50] = 
			DataStore[50] = new StringProperty__Implementation__Frozen(null, 50);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[52] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[52] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[52] = 
			DataStore[52] = new StringProperty__Implementation__Frozen(null, 52);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[59] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[59] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[59] = 
			DataStore[59] = new StringProperty__Implementation__Frozen(null, 59);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[60] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[60] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[60] = 
			DataStore[60] = new StringProperty__Implementation__Frozen(null, 60);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[61] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[61] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[61] = 
			DataStore[61] = new StringProperty__Implementation__Frozen(null, 61);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[62] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[62] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[62] = 
			DataStore[62] = new StringProperty__Implementation__Frozen(null, 62);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[63] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[63] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[63] = 
			DataStore[63] = new StringProperty__Implementation__Frozen(null, 63);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[68] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[68] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[68] = 
			DataStore[68] = new StringProperty__Implementation__Frozen(null, 68);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[71] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[71] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[71] = 
			DataStore[71] = new StringProperty__Implementation__Frozen(null, 71);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[77] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[77] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[77] = 
			DataStore[77] = new StringProperty__Implementation__Frozen(null, 77);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[85] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[85] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[85] = 
			DataStore[85] = new StringProperty__Implementation__Frozen(null, 85);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[87] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[87] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[87] = 
			DataStore[87] = new StringProperty__Implementation__Frozen(null, 87);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[91] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[91] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[91] = 
			DataStore[91] = new StringProperty__Implementation__Frozen(null, 91);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[99] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[99] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[99] = 
			DataStore[99] = new StringProperty__Implementation__Frozen(null, 99);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[107] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[107] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[107] = 
			DataStore[107] = new StringProperty__Implementation__Frozen(null, 107);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[109] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[109] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[109] = 
			DataStore[109] = new StringProperty__Implementation__Frozen(null, 109);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[115] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[115] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[115] = 
			DataStore[115] = new StringProperty__Implementation__Frozen(null, 115);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[127] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[127] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[127] = 
			DataStore[127] = new StringProperty__Implementation__Frozen(null, 127);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[128] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[128] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[128] = 
			DataStore[128] = new StringProperty__Implementation__Frozen(null, 128);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[130] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[130] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[130] = 
			DataStore[130] = new StringProperty__Implementation__Frozen(null, 130);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[136] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[136] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[136] = 
			DataStore[136] = new StringProperty__Implementation__Frozen(null, 136);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[139] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[139] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[139] = 
			DataStore[139] = new StringProperty__Implementation__Frozen(null, 139);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[148] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[148] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[148] = 
			DataStore[148] = new StringProperty__Implementation__Frozen(null, 148);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[149] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[149] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[149] = 
			DataStore[149] = new StringProperty__Implementation__Frozen(null, 149);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[154] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[154] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[154] = 
			DataStore[154] = new StringProperty__Implementation__Frozen(null, 154);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[162] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[162] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[162] = 
			DataStore[162] = new StringProperty__Implementation__Frozen(null, 162);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[167] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[167] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[167] = 
			DataStore[167] = new StringProperty__Implementation__Frozen(null, 167);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[175] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[175] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[175] = 
			DataStore[175] = new StringProperty__Implementation__Frozen(null, 175);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[176] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[176] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[176] = 
			DataStore[176] = new StringProperty__Implementation__Frozen(null, 176);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[177] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[177] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[177] = 
			DataStore[177] = new StringProperty__Implementation__Frozen(null, 177);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[178] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[178] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[178] = 
			DataStore[178] = new StringProperty__Implementation__Frozen(null, 178);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[179] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[179] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[179] = 
			DataStore[179] = new StringProperty__Implementation__Frozen(null, 179);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[180] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[180] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[180] = 
			DataStore[180] = new StringProperty__Implementation__Frozen(null, 180);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[184] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[184] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[184] = 
			DataStore[184] = new StringProperty__Implementation__Frozen(null, 184);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[205] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[205] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[205] = 
			DataStore[205] = new StringProperty__Implementation__Frozen(null, 205);

		}

		internal new static void FillDataStore() {
			DataStore[1].Length = 51;
			DataStore[3].Length = 100;
			DataStore[9].Length = 100;
			DataStore[13].Length = 100;
			DataStore[15].Length = 100;
			DataStore[20].Length = 100;
			DataStore[30].Length = 100;
			DataStore[39].Length = 20;
			DataStore[40].Length = 50;
			DataStore[41].Length = 200;
			DataStore[42].Length = 200;
			DataStore[43].Length = 200;
			DataStore[48].Length = 100;
			DataStore[50].Length = 200;
			DataStore[52].Length = 200;
			DataStore[59].Length = 200;
			DataStore[60].Length = 200;
			DataStore[61].Length = 10;
			DataStore[62].Length = 100;
			DataStore[63].Length = 50;
			DataStore[68].Length = 200;
			DataStore[71].Length = 200;
			DataStore[77].Length = 200;
			DataStore[85].Length = 200;
			DataStore[87].Length = 200;
			DataStore[91].Length = 100;
			DataStore[99].Length = 200;
			DataStore[107].Length = 200;
			DataStore[109].Length = 200;
			DataStore[115].Length = 200;
			DataStore[127].Length = 50;
			DataStore[128].Length = 50;
			DataStore[130].Length = 200;
			DataStore[136].Length = 200;
			DataStore[139].Length = 200;
			DataStore[148].Length = 200;
			DataStore[149].Length = 200;
			DataStore[154].Length = 200;
			DataStore[162].Length = 200;
			DataStore[167].Length = 400;
			DataStore[175].Length = 200;
			DataStore[176].Length = 200;
			DataStore[177].Length = 200;
			DataStore[178].Length = 200;
			DataStore[179].Length = 200;
			DataStore[180].Length = 200;
			DataStore[184].Length = 200;
			DataStore[205].Length = 200;
	
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