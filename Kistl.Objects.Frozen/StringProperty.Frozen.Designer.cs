
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
    
		public StringProperty__Implementation__Frozen()
		{
        }


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
					var __oldValue = _Length;
                    NotifyPropertyChanging("Length", __oldValue, value);
                    _Length = value;
                    NotifyPropertyChanged("Length", __oldValue, value);
                }
            }
        }
        private int _Length;

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
                e.Result = base.GetPropertyType();
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
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<StringProperty> OnGetPropertyTypeString_StringProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(StringProperty));
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Length":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(28).Constraints
						.Where(c => !c.IsValid(this, this.Length))
						.Select(c => c.GetErrorText(this, this.Length))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
        internal StringProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, StringProperty__Implementation__Frozen> DataStore = new Dictionary<int, StringProperty__Implementation__Frozen>(51);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[1] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[1] = 
			DataStore[1] = new StringProperty__Implementation__Frozen(1);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[3] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[3] = 
			DataStore[3] = new StringProperty__Implementation__Frozen(3);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[9] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[9] = 
			DataStore[9] = new StringProperty__Implementation__Frozen(9);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[13] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[13] = 
			DataStore[13] = new StringProperty__Implementation__Frozen(13);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[15] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[15] = 
			DataStore[15] = new StringProperty__Implementation__Frozen(15);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[20] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[20] = 
			DataStore[20] = new StringProperty__Implementation__Frozen(20);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[30] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[30] = 
			DataStore[30] = new StringProperty__Implementation__Frozen(30);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[39] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[39] = 
			DataStore[39] = new StringProperty__Implementation__Frozen(39);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[40] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[40] = 
			DataStore[40] = new StringProperty__Implementation__Frozen(40);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[41] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[41] = 
			DataStore[41] = new StringProperty__Implementation__Frozen(41);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[42] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[42] = 
			DataStore[42] = new StringProperty__Implementation__Frozen(42);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[43] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[43] = 
			DataStore[43] = new StringProperty__Implementation__Frozen(43);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[48] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[48] = 
			DataStore[48] = new StringProperty__Implementation__Frozen(48);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[50] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[50] = 
			DataStore[50] = new StringProperty__Implementation__Frozen(50);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[52] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[52] = 
			DataStore[52] = new StringProperty__Implementation__Frozen(52);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[59] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[59] = 
			DataStore[59] = new StringProperty__Implementation__Frozen(59);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[60] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[60] = 
			DataStore[60] = new StringProperty__Implementation__Frozen(60);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[61] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[61] = 
			DataStore[61] = new StringProperty__Implementation__Frozen(61);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[62] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[62] = 
			DataStore[62] = new StringProperty__Implementation__Frozen(62);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[63] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[63] = 
			DataStore[63] = new StringProperty__Implementation__Frozen(63);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[68] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[68] = 
			DataStore[68] = new StringProperty__Implementation__Frozen(68);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[71] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[71] = 
			DataStore[71] = new StringProperty__Implementation__Frozen(71);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[77] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[77] = 
			DataStore[77] = new StringProperty__Implementation__Frozen(77);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[85] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[85] = 
			DataStore[85] = new StringProperty__Implementation__Frozen(85);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[91] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[91] = 
			DataStore[91] = new StringProperty__Implementation__Frozen(91);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[99] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[99] = 
			DataStore[99] = new StringProperty__Implementation__Frozen(99);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[107] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[107] = 
			DataStore[107] = new StringProperty__Implementation__Frozen(107);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[109] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[109] = 
			DataStore[109] = new StringProperty__Implementation__Frozen(109);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[115] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[115] = 
			DataStore[115] = new StringProperty__Implementation__Frozen(115);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[127] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[127] = 
			DataStore[127] = new StringProperty__Implementation__Frozen(127);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[128] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[128] = 
			DataStore[128] = new StringProperty__Implementation__Frozen(128);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[130] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[130] = 
			DataStore[130] = new StringProperty__Implementation__Frozen(130);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[136] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[136] = 
			DataStore[136] = new StringProperty__Implementation__Frozen(136);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[139] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[139] = 
			DataStore[139] = new StringProperty__Implementation__Frozen(139);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[148] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[148] = 
			DataStore[148] = new StringProperty__Implementation__Frozen(148);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[149] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[149] = 
			DataStore[149] = new StringProperty__Implementation__Frozen(149);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[154] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[154] = 
			DataStore[154] = new StringProperty__Implementation__Frozen(154);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[162] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[162] = 
			DataStore[162] = new StringProperty__Implementation__Frozen(162);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[167] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[167] = 
			DataStore[167] = new StringProperty__Implementation__Frozen(167);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[175] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[175] = 
			DataStore[175] = new StringProperty__Implementation__Frozen(175);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[176] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[176] = 
			DataStore[176] = new StringProperty__Implementation__Frozen(176);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[177] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[177] = 
			DataStore[177] = new StringProperty__Implementation__Frozen(177);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[178] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[178] = 
			DataStore[178] = new StringProperty__Implementation__Frozen(178);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[179] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[179] = 
			DataStore[179] = new StringProperty__Implementation__Frozen(179);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[180] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[180] = 
			DataStore[180] = new StringProperty__Implementation__Frozen(180);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[184] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[184] = 
			DataStore[184] = new StringProperty__Implementation__Frozen(184);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[205] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[205] = 
			DataStore[205] = new StringProperty__Implementation__Frozen(205);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[216] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[216] = 
			DataStore[216] = new StringProperty__Implementation__Frozen(216);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[225] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[225] = 
			DataStore[225] = new StringProperty__Implementation__Frozen(225);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[232] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[232] = 
			DataStore[232] = new StringProperty__Implementation__Frozen(232);

			Kistl.App.Base.Property__Implementation__Frozen.DataStore[237] = 
			Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[237] = 
			DataStore[237] = new StringProperty__Implementation__Frozen(237);

		}

		internal new static void FillDataStore() {
			DataStore[1].AltText = @"Der Name der Objektklasse";
			DataStore[1].CategoryTags = @"DataModel Description";
			DataStore[1].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(3) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[146],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[192],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[195],
});
			DataStore[1].Description = @"Der Name der Objektklasse";
			DataStore[1].IsIndexed = false;
			DataStore[1].IsList = false;
			DataStore[1].IsNullable = false;
			DataStore[1].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[1].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[1].PropertyName = @"ClassName";
			DataStore[1].ValueModelDescriptor = null;
			DataStore[1].Length = 51;
			DataStore[1].Seal();
			DataStore[3].AltText = @"Tabellenname in der Datenbank";
			DataStore[3].CategoryTags = @"Physical";
			DataStore[3].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(3) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[145],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[191],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[194],
});
			DataStore[3].Description = @"Tabellenname in der Datenbank";
			DataStore[3].IsIndexed = false;
			DataStore[3].IsList = false;
			DataStore[3].IsNullable = false;
			DataStore[3].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[3].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[3].PropertyName = @"TableName";
			DataStore[3].ValueModelDescriptor = null;
			DataStore[3].Length = 100;
			DataStore[3].Seal();
			DataStore[9].AltText = null;
			DataStore[9].CategoryTags = @"DataModel";
			DataStore[9].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[190],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[196],
});
			DataStore[9].Description = null;
			DataStore[9].IsIndexed = false;
			DataStore[9].IsList = false;
			DataStore[9].IsNullable = true;
			DataStore[9].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[9].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[9].PropertyName = @"PropertyName";
			DataStore[9].ValueModelDescriptor = null;
			DataStore[9].Length = 100;
			DataStore[9].Seal();
			DataStore[13].AltText = @"Projektname";
			DataStore[13].CategoryTags = null;
			DataStore[13].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[189],
});
			DataStore[13].Description = @"Projektname";
			DataStore[13].IsIndexed = false;
			DataStore[13].IsList = false;
			DataStore[13].IsNullable = true;
			DataStore[13].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[13].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[13].PropertyName = @"Name";
			DataStore[13].ValueModelDescriptor = null;
			DataStore[13].Length = 100;
			DataStore[13].Seal();
			DataStore[15].AltText = @"Taskname";
			DataStore[15].CategoryTags = null;
			DataStore[15].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[188],
});
			DataStore[15].Description = @"Taskname";
			DataStore[15].IsIndexed = false;
			DataStore[15].IsList = false;
			DataStore[15].IsNullable = true;
			DataStore[15].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[15].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[15].PropertyName = @"Name";
			DataStore[15].ValueModelDescriptor = null;
			DataStore[15].Length = 100;
			DataStore[15].Seal();
			DataStore[20].AltText = @"Vorname Nachname";
			DataStore[20].CategoryTags = null;
			DataStore[20].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[187],
});
			DataStore[20].Description = @"Vorname Nachname";
			DataStore[20].IsIndexed = false;
			DataStore[20].IsList = false;
			DataStore[20].IsNullable = true;
			DataStore[20].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[20].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[20].PropertyName = @"Name";
			DataStore[20].ValueModelDescriptor = null;
			DataStore[20].Length = 100;
			DataStore[20].Seal();
			DataStore[30].AltText = null;
			DataStore[30].CategoryTags = null;
			DataStore[30].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(3) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[140],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[186],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[199],
});
			DataStore[30].Description = null;
			DataStore[30].IsIndexed = false;
			DataStore[30].IsList = false;
			DataStore[30].IsNullable = false;
			DataStore[30].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[30].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[30].PropertyName = @"MethodName";
			DataStore[30].ValueModelDescriptor = null;
			DataStore[30].Length = 100;
			DataStore[30].Seal();
			DataStore[39].AltText = @"NNNN TTMMYY";
			DataStore[39].CategoryTags = null;
			DataStore[39].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[185],
});
			DataStore[39].Description = @"NNNN TTMMYY";
			DataStore[39].IsIndexed = false;
			DataStore[39].IsList = false;
			DataStore[39].IsNullable = true;
			DataStore[39].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[39].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[39].PropertyName = @"SVNr";
			DataStore[39].ValueModelDescriptor = null;
			DataStore[39].Length = 20;
			DataStore[39].Seal();
			DataStore[40].AltText = @"+43 123 12345678";
			DataStore[40].CategoryTags = null;
			DataStore[40].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[184],
});
			DataStore[40].Description = @"+43 123 12345678";
			DataStore[40].IsIndexed = false;
			DataStore[40].IsList = false;
			DataStore[40].IsNullable = true;
			DataStore[40].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[40].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[40].PropertyName = @"TelefonNummer";
			DataStore[40].ValueModelDescriptor = null;
			DataStore[40].Length = 50;
			DataStore[40].Seal();
			DataStore[41].AltText = null;
			DataStore[41].CategoryTags = @"Description";
			DataStore[41].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[183],
});
			DataStore[41].Description = null;
			DataStore[41].IsIndexed = false;
			DataStore[41].IsList = false;
			DataStore[41].IsNullable = true;
			DataStore[41].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[41].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[41].PropertyName = @"AltText";
			DataStore[41].ValueModelDescriptor = null;
			DataStore[41].Length = 200;
			DataStore[41].Seal();
			DataStore[42].AltText = @"CLR Namespace des Moduls";
			DataStore[42].CategoryTags = null;
			DataStore[42].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(3) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[139],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[182],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[198],
});
			DataStore[42].Description = @"CLR Namespace des Moduls";
			DataStore[42].IsIndexed = false;
			DataStore[42].IsList = false;
			DataStore[42].IsNullable = false;
			DataStore[42].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[42].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[42].PropertyName = @"Namespace";
			DataStore[42].ValueModelDescriptor = null;
			DataStore[42].Length = 200;
			DataStore[42].Seal();
			DataStore[43].AltText = @"Name des Moduls";
			DataStore[43].CategoryTags = null;
			DataStore[43].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[138],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[181],
});
			DataStore[43].Description = @"Name des Moduls";
			DataStore[43].IsIndexed = false;
			DataStore[43].IsList = false;
			DataStore[43].IsNullable = false;
			DataStore[43].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[43].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[43].PropertyName = @"ModuleName";
			DataStore[43].ValueModelDescriptor = null;
			DataStore[43].Length = 200;
			DataStore[43].Seal();
			DataStore[48].AltText = @"Bitte geben Sie den Kundennamen ein";
			DataStore[48].CategoryTags = null;
			DataStore[48].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[180],
});
			DataStore[48].Description = @"Bitte geben Sie den Kundennamen ein";
			DataStore[48].IsIndexed = false;
			DataStore[48].IsList = false;
			DataStore[48].IsNullable = true;
			DataStore[48].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[48].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[48].PropertyName = @"Kundenname";
			DataStore[48].ValueModelDescriptor = null;
			DataStore[48].Length = 100;
			DataStore[48].Seal();
			DataStore[50].AltText = @"Bitte füllen Sie einen sprechenden Auftragsnamen aus";
			DataStore[50].CategoryTags = null;
			DataStore[50].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[179],
});
			DataStore[50].Description = @"Bitte füllen Sie einen sprechenden Auftragsnamen aus";
			DataStore[50].IsIndexed = false;
			DataStore[50].IsList = false;
			DataStore[50].IsNullable = true;
			DataStore[50].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[50].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[50].PropertyName = @"Auftragsname";
			DataStore[50].ValueModelDescriptor = null;
			DataStore[50].Length = 200;
			DataStore[50].Seal();
			DataStore[52].AltText = @"Name des Zeiterfassungskontos";
			DataStore[52].CategoryTags = null;
			DataStore[52].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[136],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[178],
});
			DataStore[52].Description = @"Name des Zeiterfassungskontos";
			DataStore[52].IsIndexed = false;
			DataStore[52].IsList = false;
			DataStore[52].IsNullable = false;
			DataStore[52].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[52].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[52].PropertyName = @"Kontoname";
			DataStore[52].ValueModelDescriptor = null;
			DataStore[52].Length = 200;
			DataStore[52].Seal();
			DataStore[59].AltText = @"Name des Kunden";
			DataStore[59].CategoryTags = null;
			DataStore[59].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[130],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[177],
});
			DataStore[59].Description = @"Name des Kunden";
			DataStore[59].IsIndexed = false;
			DataStore[59].IsList = false;
			DataStore[59].IsNullable = false;
			DataStore[59].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[59].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[59].PropertyName = @"Kundenname";
			DataStore[59].ValueModelDescriptor = null;
			DataStore[59].Length = 200;
			DataStore[59].Seal();
			DataStore[60].AltText = @"Adresse & Hausnummer";
			DataStore[60].CategoryTags = null;
			DataStore[60].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[176],
});
			DataStore[60].Description = @"Adresse & Hausnummer";
			DataStore[60].IsIndexed = false;
			DataStore[60].IsList = false;
			DataStore[60].IsNullable = true;
			DataStore[60].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[60].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[60].PropertyName = @"Adresse";
			DataStore[60].ValueModelDescriptor = null;
			DataStore[60].Length = 200;
			DataStore[60].Seal();
			DataStore[61].AltText = @"Postleitzahl";
			DataStore[61].CategoryTags = null;
			DataStore[61].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[129],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[175],
});
			DataStore[61].Description = @"Postleitzahl";
			DataStore[61].IsIndexed = false;
			DataStore[61].IsList = false;
			DataStore[61].IsNullable = false;
			DataStore[61].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[61].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[61].PropertyName = @"PLZ";
			DataStore[61].ValueModelDescriptor = null;
			DataStore[61].Length = 10;
			DataStore[61].Seal();
			DataStore[62].AltText = @"Ort";
			DataStore[62].CategoryTags = null;
			DataStore[62].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[174],
});
			DataStore[62].Description = @"Ort";
			DataStore[62].IsIndexed = false;
			DataStore[62].IsList = false;
			DataStore[62].IsNullable = true;
			DataStore[62].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[62].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[62].PropertyName = @"Ort";
			DataStore[62].ValueModelDescriptor = null;
			DataStore[62].Length = 100;
			DataStore[62].Seal();
			DataStore[63].AltText = @"Land";
			DataStore[63].CategoryTags = null;
			DataStore[63].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[173],
});
			DataStore[63].Description = @"Land";
			DataStore[63].IsIndexed = false;
			DataStore[63].IsList = false;
			DataStore[63].IsNullable = true;
			DataStore[63].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[63].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[63].PropertyName = @"Land";
			DataStore[63].ValueModelDescriptor = null;
			DataStore[63].Length = 50;
			DataStore[63].Seal();
			DataStore[68].AltText = @"Filename of the Icon";
			DataStore[68].CategoryTags = null;
			DataStore[68].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[128],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[172],
});
			DataStore[68].Description = @"Filename of the Icon";
			DataStore[68].IsIndexed = false;
			DataStore[68].IsList = false;
			DataStore[68].IsNullable = false;
			DataStore[68].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[68].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27];
			DataStore[68].PropertyName = @"IconFile";
			DataStore[68].ValueModelDescriptor = null;
			DataStore[68].Length = 200;
			DataStore[68].Seal();
			DataStore[71].AltText = @"Full Assemblyname eg. MyActions, Version=1.0.0.0";
			DataStore[71].CategoryTags = null;
			DataStore[71].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[126],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[171],
});
			DataStore[71].Description = @"Full Assemblyname eg. MyActions, Version=1.0.0.0";
			DataStore[71].IsIndexed = false;
			DataStore[71].IsList = false;
			DataStore[71].IsNullable = false;
			DataStore[71].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[71].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[71].PropertyName = @"AssemblyName";
			DataStore[71].ValueModelDescriptor = null;
			DataStore[71].Length = 200;
			DataStore[71].Seal();
			DataStore[77].AltText = @"Name des implementierenden Members";
			DataStore[77].CategoryTags = null;
			DataStore[77].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[120],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[169],
});
			DataStore[77].Description = @"Name des implementierenden Members";
			DataStore[77].IsIndexed = false;
			DataStore[77].IsList = false;
			DataStore[77].IsNullable = false;
			DataStore[77].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[77].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[77].PropertyName = @"MemberName";
			DataStore[77].ValueModelDescriptor = null;
			DataStore[77].Length = 200;
			DataStore[77].Seal();
			DataStore[85].AltText = @"EMails des Kunden - können mehrere sein";
			DataStore[85].CategoryTags = null;
			DataStore[85].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[168],
});
			DataStore[85].Description = @"EMails des Kunden - können mehrere sein";
			DataStore[85].IsIndexed = false;
			DataStore[85].IsList = true;
			DataStore[85].IsNullable = true;
			DataStore[85].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[85].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[85].PropertyName = @"EMails";
			DataStore[85].ValueModelDescriptor = null;
			DataStore[85].Length = 200;
			DataStore[85].Seal();
			DataStore[91].AltText = @"Name des Parameter";
			DataStore[91].CategoryTags = null;
			DataStore[91].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(3) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[114],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[166],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[200],
});
			DataStore[91].Description = @"Name des Parameter";
			DataStore[91].IsIndexed = false;
			DataStore[91].IsList = false;
			DataStore[91].IsNullable = false;
			DataStore[91].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[91].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[91].PropertyName = @"ParameterName";
			DataStore[91].ValueModelDescriptor = null;
			DataStore[91].Length = 100;
			DataStore[91].Seal();
			DataStore[99].AltText = @"Name des CLR Datentypen";
			DataStore[99].CategoryTags = null;
			DataStore[99].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[108],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[165],
});
			DataStore[99].Description = @"Name des CLR Datentypen";
			DataStore[99].IsIndexed = false;
			DataStore[99].IsList = false;
			DataStore[99].IsNullable = false;
			DataStore[99].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[99].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43];
			DataStore[99].PropertyName = @"FullTypeName";
			DataStore[99].ValueModelDescriptor = null;
			DataStore[99].Length = 200;
			DataStore[99].Seal();
			DataStore[107].AltText = @"String Property für das Testinterface";
			DataStore[107].CategoryTags = null;
			DataStore[107].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[105],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[164],
});
			DataStore[107].Description = @"String Property für das Testinterface";
			DataStore[107].IsIndexed = false;
			DataStore[107].IsList = false;
			DataStore[107].IsNullable = false;
			DataStore[107].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[107].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[48];
			DataStore[107].PropertyName = @"StringProp";
			DataStore[107].ValueModelDescriptor = null;
			DataStore[107].Length = 200;
			DataStore[107].Seal();
			DataStore[109].AltText = @"String Property";
			DataStore[109].CategoryTags = null;
			DataStore[109].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[103],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[163],
});
			DataStore[109].Description = @"String Property";
			DataStore[109].IsIndexed = false;
			DataStore[109].IsList = false;
			DataStore[109].IsNullable = false;
			DataStore[109].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[109].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51];
			DataStore[109].PropertyName = @"StringProp";
			DataStore[109].ValueModelDescriptor = null;
			DataStore[109].Length = 200;
			DataStore[109].Seal();
			DataStore[115].AltText = @"The name of the class implementing this Control";
			DataStore[115].CategoryTags = null;
			DataStore[115].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[97],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[162],
});
			DataStore[115].Description = @"The name of the class implementing this Control";
			DataStore[115].IsIndexed = false;
			DataStore[115].IsList = false;
			DataStore[115].IsNullable = false;
			DataStore[115].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[115].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[115].PropertyName = @"ClassName";
			DataStore[115].ValueModelDescriptor = null;
			DataStore[115].Length = 200;
			DataStore[115].Seal();
			DataStore[127].AltText = @"Enter a Number";
			DataStore[127].CategoryTags = null;
			DataStore[127].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[161],
});
			DataStore[127].Description = @"Enter a Number";
			DataStore[127].IsIndexed = false;
			DataStore[127].IsList = false;
			DataStore[127].IsNullable = true;
			DataStore[127].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[127].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[63];
			DataStore[127].PropertyName = @"Number";
			DataStore[127].ValueModelDescriptor = null;
			DataStore[127].Length = 50;
			DataStore[127].Seal();
			DataStore[128].AltText = @"Enter Area Code";
			DataStore[128].CategoryTags = null;
			DataStore[128].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[160],
});
			DataStore[128].Description = @"Enter Area Code";
			DataStore[128].IsIndexed = false;
			DataStore[128].IsList = false;
			DataStore[128].IsNullable = true;
			DataStore[128].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[128].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[63];
			DataStore[128].PropertyName = @"AreaCode";
			DataStore[128].ValueModelDescriptor = null;
			DataStore[128].Length = 50;
			DataStore[128].Seal();
			DataStore[130].AltText = @"Persons Name";
			DataStore[130].CategoryTags = null;
			DataStore[130].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[90],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[159],
});
			DataStore[130].Description = @"Persons Name";
			DataStore[130].IsIndexed = false;
			DataStore[130].IsList = false;
			DataStore[130].IsNullable = false;
			DataStore[130].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[130].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[130].PropertyName = @"PersonName";
			DataStore[130].ValueModelDescriptor = null;
			DataStore[130].Length = 200;
			DataStore[130].Seal();
			DataStore[136].AltText = @"CLR name of this entry";
			DataStore[136].CategoryTags = null;
			DataStore[136].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(3) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[88],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[158],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[201],
});
			DataStore[136].Description = @"CLR name of this entry";
			DataStore[136].IsIndexed = false;
			DataStore[136].IsList = false;
			DataStore[136].IsNullable = false;
			DataStore[136].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[136].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[136].PropertyName = @"Name";
			DataStore[136].ValueModelDescriptor = null;
			DataStore[136].Length = 200;
			DataStore[136].Seal();
			DataStore[139].AltText = @"The CLR namespace and class name of the Presenter";
			DataStore[139].CategoryTags = null;
			DataStore[139].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[85],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[157],
});
			DataStore[139].Description = @"The CLR namespace and class name of the Presenter";
			DataStore[139].IsIndexed = false;
			DataStore[139].IsList = false;
			DataStore[139].IsNullable = false;
			DataStore[139].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[139].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[139].PropertyName = @"PresenterTypeName";
			DataStore[139].ValueModelDescriptor = null;
			DataStore[139].Length = 200;
			DataStore[139].Seal();
			DataStore[148].AltText = @"The CLR namespace and class name of the Data Type";
			DataStore[148].CategoryTags = null;
			DataStore[148].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[84],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[156],
});
			DataStore[148].Description = @"The CLR namespace and class name of the Data Type";
			DataStore[148].IsIndexed = false;
			DataStore[148].IsList = false;
			DataStore[148].IsNullable = false;
			DataStore[148].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[148].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[148].PropertyName = @"DataTypeName";
			DataStore[148].ValueModelDescriptor = null;
			DataStore[148].Length = 200;
			DataStore[148].Seal();
			DataStore[149].AltText = @"A short description of the utility of this visual";
			DataStore[149].CategoryTags = null;
			DataStore[149].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[83],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[155],
});
			DataStore[149].Description = @"A short description of the utility of this visual";
			DataStore[149].IsIndexed = false;
			DataStore[149].IsList = false;
			DataStore[149].IsNullable = false;
			DataStore[149].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[149].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[149].PropertyName = @"Description";
			DataStore[149].ValueModelDescriptor = null;
			DataStore[149].Length = 200;
			DataStore[149].Seal();
			DataStore[154].AltText = @"a short name to identify this Template to the user";
			DataStore[154].CategoryTags = null;
			DataStore[154].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[80],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[154],
});
			DataStore[154].Description = @"a short name to identify this Template to the user";
			DataStore[154].IsIndexed = false;
			DataStore[154].IsList = false;
			DataStore[154].IsNullable = false;
			DataStore[154].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[154].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[154].PropertyName = @"DisplayName";
			DataStore[154].ValueModelDescriptor = null;
			DataStore[154].Length = 200;
			DataStore[154].Seal();
			DataStore[162].AltText = @"FullName of the Type that is displayed with this Template";
			DataStore[162].CategoryTags = null;
			DataStore[162].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[78],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[153],
});
			DataStore[162].Description = @"FullName of the Type that is displayed with this Template";
			DataStore[162].IsIndexed = false;
			DataStore[162].IsList = false;
			DataStore[162].IsNullable = false;
			DataStore[162].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[162].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[162].PropertyName = @"DisplayedTypeFullName";
			DataStore[162].ValueModelDescriptor = null;
			DataStore[162].Length = 200;
			DataStore[162].Seal();
			DataStore[167].AltText = @"The reason of this constraint";
			DataStore[167].CategoryTags = null;
			DataStore[167].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[152],
});
			DataStore[167].Description = @"The reason of this constraint";
			DataStore[167].IsIndexed = false;
			DataStore[167].IsList = false;
			DataStore[167].IsNullable = true;
			DataStore[167].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[167].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[167].PropertyName = @"Reason";
			DataStore[167].ValueModelDescriptor = null;
			DataStore[167].Length = 400;
			DataStore[167].Seal();
			DataStore[175].AltText = @"Description of this DataType";
			DataStore[175].CategoryTags = @"Description";
			DataStore[175].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[207],
});
			DataStore[175].Description = @"Description of this DataType";
			DataStore[175].IsIndexed = false;
			DataStore[175].IsList = false;
			DataStore[175].IsNullable = true;
			DataStore[175].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[175].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[175].PropertyName = @"Description";
			DataStore[175].ValueModelDescriptor = null;
			DataStore[175].Length = 200;
			DataStore[175].Seal();
			DataStore[176].AltText = @"Description of this Property";
			DataStore[176].CategoryTags = @"Description";
			DataStore[176].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[206],
});
			DataStore[176].Description = @"Description of this Property";
			DataStore[176].IsIndexed = false;
			DataStore[176].IsList = false;
			DataStore[176].IsNullable = true;
			DataStore[176].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[176].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[176].PropertyName = @"Description";
			DataStore[176].ValueModelDescriptor = null;
			DataStore[176].Length = 200;
			DataStore[176].Seal();
			DataStore[177].AltText = @"Description of this Parameter";
			DataStore[177].CategoryTags = null;
			DataStore[177].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[205],
});
			DataStore[177].Description = @"Description of this Parameter";
			DataStore[177].IsIndexed = false;
			DataStore[177].IsList = false;
			DataStore[177].IsNullable = true;
			DataStore[177].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[177].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[177].PropertyName = @"Description";
			DataStore[177].ValueModelDescriptor = null;
			DataStore[177].Length = 200;
			DataStore[177].Seal();
			DataStore[178].AltText = @"Description of this Enumeration Entry";
			DataStore[178].CategoryTags = null;
			DataStore[178].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[204],
});
			DataStore[178].Description = @"Description of this Enumeration Entry";
			DataStore[178].IsIndexed = false;
			DataStore[178].IsList = false;
			DataStore[178].IsNullable = true;
			DataStore[178].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[178].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[178].PropertyName = @"Description";
			DataStore[178].ValueModelDescriptor = null;
			DataStore[178].Length = 200;
			DataStore[178].Seal();
			DataStore[179].AltText = @"Description of this Module";
			DataStore[179].CategoryTags = null;
			DataStore[179].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[203],
});
			DataStore[179].Description = @"Description of this Module";
			DataStore[179].IsIndexed = false;
			DataStore[179].IsList = false;
			DataStore[179].IsNullable = true;
			DataStore[179].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[179].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[179].PropertyName = @"Description";
			DataStore[179].ValueModelDescriptor = null;
			DataStore[179].Length = 200;
			DataStore[179].Seal();
			DataStore[180].AltText = @"Description of this Method";
			DataStore[180].CategoryTags = null;
			DataStore[180].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[208],
});
			DataStore[180].Description = @"Description of this Method";
			DataStore[180].IsIndexed = false;
			DataStore[180].IsList = false;
			DataStore[180].IsNullable = true;
			DataStore[180].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[180].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[180].PropertyName = @"Description";
			DataStore[180].ValueModelDescriptor = null;
			DataStore[180].Length = 200;
			DataStore[180].Seal();
			DataStore[184].AltText = @"Description of this Relation";
			DataStore[184].CategoryTags = null;
			DataStore[184].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[209],
});
			DataStore[184].Description = @"Description of this Relation";
			DataStore[184].IsIndexed = false;
			DataStore[184].IsList = false;
			DataStore[184].IsNullable = true;
			DataStore[184].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[184].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[184].PropertyName = @"Description";
			DataStore[184].ValueModelDescriptor = null;
			DataStore[184].Length = 200;
			DataStore[184].Seal();
			DataStore[205].AltText = null;
			DataStore[205].CategoryTags = null;
			DataStore[205].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[205].Description = null;
			DataStore[205].IsIndexed = false;
			DataStore[205].IsList = false;
			DataStore[205].IsNullable = false;
			DataStore[205].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[205].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[205].PropertyName = @"FullName";
			DataStore[205].ValueModelDescriptor = null;
			DataStore[205].Length = 200;
			DataStore[205].Seal();
			DataStore[216].AltText = @"This end's role name in the relation";
			DataStore[216].CategoryTags = null;
			DataStore[216].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[216].Description = @"This end's role name in the relation";
			DataStore[216].IsIndexed = false;
			DataStore[216].IsList = false;
			DataStore[216].IsNullable = false;
			DataStore[216].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[216].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82];
			DataStore[216].PropertyName = @"RoleName";
			DataStore[216].ValueModelDescriptor = null;
			DataStore[216].Length = 200;
			DataStore[216].Seal();
			DataStore[225].AltText = null;
			DataStore[225].CategoryTags = @"Description GUI";
			DataStore[225].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[225].Description = @"A space separated list of category names containing this Property";
			DataStore[225].IsIndexed = false;
			DataStore[225].IsList = false;
			DataStore[225].IsNullable = true;
			DataStore[225].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[225].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[225].PropertyName = @"CategoryTags";
			DataStore[225].ValueModelDescriptor = null;
			DataStore[225].Length = 4000;
			DataStore[225].Seal();
			DataStore[232].AltText = null;
			DataStore[232].CategoryTags = @"Summary,Main";
			DataStore[232].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[232].Description = @"describe this PresentableModel";
			DataStore[232].IsIndexed = false;
			DataStore[232].IsList = false;
			DataStore[232].IsNullable = false;
			DataStore[232].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[232].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[85];
			DataStore[232].PropertyName = @"Description";
			DataStore[232].ValueModelDescriptor = null;
			DataStore[232].Length = 4000;
			DataStore[232].Seal();
			DataStore[237].AltText = null;
			DataStore[237].CategoryTags = @"Main";
			DataStore[237].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[237].Description = @"Platz für Notizen";
			DataStore[237].IsIndexed = false;
			DataStore[237].IsList = false;
			DataStore[237].IsNullable = false;
			DataStore[237].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[237].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[237].PropertyName = @"Notizen";
			DataStore[237].ValueModelDescriptor = null;
			DataStore[237].Length = 4000;
			DataStore[237].Seal();
	
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
        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}