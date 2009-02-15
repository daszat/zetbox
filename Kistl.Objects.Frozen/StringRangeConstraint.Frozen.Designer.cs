
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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StringRangeConstraint")]
    public class StringRangeConstraint__Implementation__Frozen : Kistl.App.Base.Constraint__Implementation__Frozen, StringRangeConstraint
    {


        /// <summary>
        /// The maximal length of this StringProperty
        /// </summary>
        // value type property
        public virtual int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MaxLength != value)
                {
                    NotifyPropertyChanging("MaxLength");
                    _MaxLength = value;
                    NotifyPropertyChanged("MaxLength");;
                }
            }
        }
        private int _MaxLength;

        /// <summary>
        /// The minimal length of this StringProperty
        /// </summary>
        // value type property
        public virtual int MinLength
        {
            get
            {
                return _MinLength;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MinLength != value)
                {
                    NotifyPropertyChanging("MinLength");
                    _MinLength = value;
                    NotifyPropertyChanged("MinLength");;
                }
            }
        }
        private int _MinLength;

        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedObj, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_StringRangeConstraint != null)
            {
                OnIsValid_StringRangeConstraint(this, e, constrainedObj, constrainedValue);
            };
            return e.Result;
        }
		public event IsValid_Handler<StringRangeConstraint> OnIsValid_StringRangeConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_StringRangeConstraint != null)
            {
                OnGetErrorText_StringRangeConstraint(this, e, constrainedObject, constrainedValue);
            };
            return e.Result;
        }
		public event GetErrorText_Handler<StringRangeConstraint> OnGetErrorText_StringRangeConstraint;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StringRangeConstraint != null)
            {
                OnToString_StringRangeConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StringRangeConstraint> OnToString_StringRangeConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StringRangeConstraint != null) OnPreSave_StringRangeConstraint(this);
        }
        public event ObjectEventHandler<StringRangeConstraint> OnPreSave_StringRangeConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StringRangeConstraint != null) OnPostSave_StringRangeConstraint(this);
        }
        public event ObjectEventHandler<StringRangeConstraint> OnPostSave_StringRangeConstraint;


        internal StringRangeConstraint__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, StringRangeConstraint__Implementation__Frozen> DataStore = new Dictionary<int, StringRangeConstraint__Implementation__Frozen>(47);
		static StringRangeConstraint__Implementation__Frozen()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[152] = 
			DataStore[152] = new StringRangeConstraint__Implementation__Frozen(null, 152);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[153] = 
			DataStore[153] = new StringRangeConstraint__Implementation__Frozen(null, 153);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[154] = 
			DataStore[154] = new StringRangeConstraint__Implementation__Frozen(null, 154);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[155] = 
			DataStore[155] = new StringRangeConstraint__Implementation__Frozen(null, 155);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[156] = 
			DataStore[156] = new StringRangeConstraint__Implementation__Frozen(null, 156);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[157] = 
			DataStore[157] = new StringRangeConstraint__Implementation__Frozen(null, 157);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[158] = 
			DataStore[158] = new StringRangeConstraint__Implementation__Frozen(null, 158);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[159] = 
			DataStore[159] = new StringRangeConstraint__Implementation__Frozen(null, 159);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[160] = 
			DataStore[160] = new StringRangeConstraint__Implementation__Frozen(null, 160);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[161] = 
			DataStore[161] = new StringRangeConstraint__Implementation__Frozen(null, 161);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[162] = 
			DataStore[162] = new StringRangeConstraint__Implementation__Frozen(null, 162);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[163] = 
			DataStore[163] = new StringRangeConstraint__Implementation__Frozen(null, 163);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[164] = 
			DataStore[164] = new StringRangeConstraint__Implementation__Frozen(null, 164);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[165] = 
			DataStore[165] = new StringRangeConstraint__Implementation__Frozen(null, 165);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[166] = 
			DataStore[166] = new StringRangeConstraint__Implementation__Frozen(null, 166);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[167] = 
			DataStore[167] = new StringRangeConstraint__Implementation__Frozen(null, 167);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[168] = 
			DataStore[168] = new StringRangeConstraint__Implementation__Frozen(null, 168);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[169] = 
			DataStore[169] = new StringRangeConstraint__Implementation__Frozen(null, 169);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[171] = 
			DataStore[171] = new StringRangeConstraint__Implementation__Frozen(null, 171);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[172] = 
			DataStore[172] = new StringRangeConstraint__Implementation__Frozen(null, 172);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[173] = 
			DataStore[173] = new StringRangeConstraint__Implementation__Frozen(null, 173);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[174] = 
			DataStore[174] = new StringRangeConstraint__Implementation__Frozen(null, 174);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[175] = 
			DataStore[175] = new StringRangeConstraint__Implementation__Frozen(null, 175);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[176] = 
			DataStore[176] = new StringRangeConstraint__Implementation__Frozen(null, 176);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[177] = 
			DataStore[177] = new StringRangeConstraint__Implementation__Frozen(null, 177);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[178] = 
			DataStore[178] = new StringRangeConstraint__Implementation__Frozen(null, 178);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[179] = 
			DataStore[179] = new StringRangeConstraint__Implementation__Frozen(null, 179);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[180] = 
			DataStore[180] = new StringRangeConstraint__Implementation__Frozen(null, 180);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[181] = 
			DataStore[181] = new StringRangeConstraint__Implementation__Frozen(null, 181);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[182] = 
			DataStore[182] = new StringRangeConstraint__Implementation__Frozen(null, 182);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[183] = 
			DataStore[183] = new StringRangeConstraint__Implementation__Frozen(null, 183);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[184] = 
			DataStore[184] = new StringRangeConstraint__Implementation__Frozen(null, 184);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[185] = 
			DataStore[185] = new StringRangeConstraint__Implementation__Frozen(null, 185);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[186] = 
			DataStore[186] = new StringRangeConstraint__Implementation__Frozen(null, 186);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[187] = 
			DataStore[187] = new StringRangeConstraint__Implementation__Frozen(null, 187);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[188] = 
			DataStore[188] = new StringRangeConstraint__Implementation__Frozen(null, 188);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[189] = 
			DataStore[189] = new StringRangeConstraint__Implementation__Frozen(null, 189);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[190] = 
			DataStore[190] = new StringRangeConstraint__Implementation__Frozen(null, 190);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[191] = 
			DataStore[191] = new StringRangeConstraint__Implementation__Frozen(null, 191);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[192] = 
			DataStore[192] = new StringRangeConstraint__Implementation__Frozen(null, 192);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[203] = 
			DataStore[203] = new StringRangeConstraint__Implementation__Frozen(null, 203);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[204] = 
			DataStore[204] = new StringRangeConstraint__Implementation__Frozen(null, 204);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[205] = 
			DataStore[205] = new StringRangeConstraint__Implementation__Frozen(null, 205);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[206] = 
			DataStore[206] = new StringRangeConstraint__Implementation__Frozen(null, 206);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[207] = 
			DataStore[207] = new StringRangeConstraint__Implementation__Frozen(null, 207);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[208] = 
			DataStore[208] = new StringRangeConstraint__Implementation__Frozen(null, 208);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[209] = 
			DataStore[209] = new StringRangeConstraint__Implementation__Frozen(null, 209);

		}

		internal new static void FillDataStore() {
			DataStore[152].MaxLength = 400;
			DataStore[152].MinLength = 0;
			DataStore[153].MaxLength = 200;
			DataStore[153].MinLength = 0;
			DataStore[154].MaxLength = 200;
			DataStore[154].MinLength = 0;
			DataStore[155].MaxLength = 200;
			DataStore[155].MinLength = 0;
			DataStore[156].MaxLength = 200;
			DataStore[156].MinLength = 0;
			DataStore[157].MaxLength = 200;
			DataStore[157].MinLength = 0;
			DataStore[158].MaxLength = 200;
			DataStore[158].MinLength = 0;
			DataStore[159].MaxLength = 200;
			DataStore[159].MinLength = 0;
			DataStore[160].MaxLength = 50;
			DataStore[160].MinLength = 0;
			DataStore[161].MaxLength = 50;
			DataStore[161].MinLength = 0;
			DataStore[162].MaxLength = 200;
			DataStore[162].MinLength = 1;
			DataStore[163].MaxLength = 200;
			DataStore[163].MinLength = 0;
			DataStore[164].MaxLength = 200;
			DataStore[164].MinLength = 0;
			DataStore[165].MaxLength = 200;
			DataStore[165].MinLength = 0;
			DataStore[166].MaxLength = 100;
			DataStore[166].MinLength = 0;
			DataStore[167].MaxLength = 200;
			DataStore[167].MinLength = 0;
			DataStore[168].MaxLength = 200;
			DataStore[168].MinLength = 0;
			DataStore[169].MaxLength = 200;
			DataStore[169].MinLength = 0;
			DataStore[171].MaxLength = 200;
			DataStore[171].MinLength = 0;
			DataStore[172].MaxLength = 200;
			DataStore[172].MinLength = 0;
			DataStore[173].MaxLength = 50;
			DataStore[173].MinLength = 0;
			DataStore[174].MaxLength = 100;
			DataStore[174].MinLength = 0;
			DataStore[175].MaxLength = 10;
			DataStore[175].MinLength = 0;
			DataStore[176].MaxLength = 200;
			DataStore[176].MinLength = 0;
			DataStore[177].MaxLength = 200;
			DataStore[177].MinLength = 0;
			DataStore[178].MaxLength = 200;
			DataStore[178].MinLength = 0;
			DataStore[179].MaxLength = 200;
			DataStore[179].MinLength = 0;
			DataStore[180].MaxLength = 100;
			DataStore[180].MinLength = 0;
			DataStore[181].MaxLength = 200;
			DataStore[181].MinLength = 0;
			DataStore[182].MaxLength = 200;
			DataStore[182].MinLength = 0;
			DataStore[183].MaxLength = 200;
			DataStore[183].MinLength = 0;
			DataStore[184].MaxLength = 50;
			DataStore[184].MinLength = 0;
			DataStore[185].MaxLength = 20;
			DataStore[185].MinLength = 0;
			DataStore[186].MaxLength = 100;
			DataStore[186].MinLength = 0;
			DataStore[187].MaxLength = 100;
			DataStore[187].MinLength = 0;
			DataStore[188].MaxLength = 100;
			DataStore[188].MinLength = 0;
			DataStore[189].MaxLength = 100;
			DataStore[189].MinLength = 0;
			DataStore[190].MaxLength = 100;
			DataStore[190].MinLength = 0;
			DataStore[191].MaxLength = 100;
			DataStore[191].MinLength = 1;
			DataStore[192].MaxLength = 51;
			DataStore[192].MinLength = 1;
			DataStore[203].MaxLength = 200;
			DataStore[203].MinLength = 0;
			DataStore[204].MaxLength = 200;
			DataStore[204].MinLength = 0;
			DataStore[205].MaxLength = 200;
			DataStore[205].MinLength = 0;
			DataStore[206].MaxLength = 200;
			DataStore[206].MinLength = 0;
			DataStore[207].MaxLength = 200;
			DataStore[207].MinLength = 0;
			DataStore[208].MaxLength = 200;
			DataStore[208].MinLength = 0;
			DataStore[209].MaxLength = 200;
			DataStore[209].MinLength = 0;
	
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