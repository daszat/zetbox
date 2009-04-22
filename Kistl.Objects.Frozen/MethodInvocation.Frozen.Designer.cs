
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
    /// Metadefinition Object for a MethodInvocation on a Method of a DataType.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MethodInvocation")]
    public class MethodInvocation__Implementation__Frozen : BaseFrozenDataObject, MethodInvocation
    {
    
		public MethodInvocation__Implementation__Frozen()
		{
        }


        /// <summary>
        /// The Type implementing this invocation
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef Implementor
        {
            get
            {
                return _Implementor;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Implementor != value)
                {
					var __oldValue = _Implementor;
                    NotifyPropertyChanging("Implementor", __oldValue, value);
                    _Implementor = value;
                    NotifyPropertyChanged("Implementor", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _Implementor;

        /// <summary>
        /// In dieser Objektklasse implementieren
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.DataType InvokeOnObjectClass
        {
            get
            {
                return _InvokeOnObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_InvokeOnObjectClass != value)
                {
					var __oldValue = _InvokeOnObjectClass;
                    NotifyPropertyChanging("InvokeOnObjectClass", __oldValue, value);
                    _InvokeOnObjectClass = value;
                    NotifyPropertyChanged("InvokeOnObjectClass", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.DataType _InvokeOnObjectClass;

        /// <summary>
        /// Name des implementierenden Members
        /// </summary>
        // value type property
        public virtual string MemberName
        {
            get
            {
                return _MemberName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MemberName != value)
                {
					var __oldValue = _MemberName;
                    NotifyPropertyChanging("MemberName", __oldValue, value);
                    _MemberName = value;
                    NotifyPropertyChanged("MemberName", __oldValue, value);
                }
            }
        }
        private string _MemberName;

        /// <summary>
        /// Methode, die Aufgerufen wird
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Method Method
        {
            get
            {
                return _Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method != value)
                {
					var __oldValue = _Method;
                    NotifyPropertyChanging("Method", __oldValue, value);
                    _Method = value;
                    NotifyPropertyChanged("Method", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Method _Method;

        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Module Module
        {
            get
            {
                return _Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Module != value)
                {
					var __oldValue = _Module;
                    NotifyPropertyChanging("Module", __oldValue, value);
                    _Module = value;
                    NotifyPropertyChanged("Module", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Module _Module;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(MethodInvocation));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocation != null)
            {
                OnToString_MethodInvocation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<MethodInvocation> OnToString_MethodInvocation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocation != null) OnPreSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPreSave_MethodInvocation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocation != null) OnPostSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPostSave_MethodInvocation;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Implementor":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(208).Constraints
						.Where(c => !c.IsValid(this, this.Implementor))
						.Select(c => c.GetErrorText(this, this.Implementor))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "InvokeOnObjectClass":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(79).Constraints
						.Where(c => !c.IsValid(this, this.InvokeOnObjectClass))
						.Select(c => c.GetErrorText(this, this.InvokeOnObjectClass))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MemberName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(77).Constraints
						.Where(c => !c.IsValid(this, this.MemberName))
						.Select(c => c.GetErrorText(this, this.MemberName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Method":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(74).Constraints
						.Where(c => !c.IsValid(this, this.Method))
						.Select(c => c.GetErrorText(this, this.Method))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Module":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(78).Constraints
						.Where(c => !c.IsValid(this, this.Module))
						.Select(c => c.GetErrorText(this, this.Module))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
        internal MethodInvocation__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, MethodInvocation__Implementation__Frozen> DataStore = new Dictionary<int, MethodInvocation__Implementation__Frozen>(97);
		internal static void CreateInstances()
		{
			DataStore[1] = new MethodInvocation__Implementation__Frozen(1);

			DataStore[2] = new MethodInvocation__Implementation__Frozen(2);

			DataStore[4] = new MethodInvocation__Implementation__Frozen(4);

			DataStore[5] = new MethodInvocation__Implementation__Frozen(5);

			DataStore[6] = new MethodInvocation__Implementation__Frozen(6);

			DataStore[9] = new MethodInvocation__Implementation__Frozen(9);

			DataStore[10] = new MethodInvocation__Implementation__Frozen(10);

			DataStore[11] = new MethodInvocation__Implementation__Frozen(11);

			DataStore[12] = new MethodInvocation__Implementation__Frozen(12);

			DataStore[13] = new MethodInvocation__Implementation__Frozen(13);

			DataStore[14] = new MethodInvocation__Implementation__Frozen(14);

			DataStore[15] = new MethodInvocation__Implementation__Frozen(15);

			DataStore[16] = new MethodInvocation__Implementation__Frozen(16);

			DataStore[17] = new MethodInvocation__Implementation__Frozen(17);

			DataStore[19] = new MethodInvocation__Implementation__Frozen(19);

			DataStore[20] = new MethodInvocation__Implementation__Frozen(20);

			DataStore[21] = new MethodInvocation__Implementation__Frozen(21);

			DataStore[22] = new MethodInvocation__Implementation__Frozen(22);

			DataStore[23] = new MethodInvocation__Implementation__Frozen(23);

			DataStore[24] = new MethodInvocation__Implementation__Frozen(24);

			DataStore[26] = new MethodInvocation__Implementation__Frozen(26);

			DataStore[28] = new MethodInvocation__Implementation__Frozen(28);

			DataStore[29] = new MethodInvocation__Implementation__Frozen(29);

			DataStore[30] = new MethodInvocation__Implementation__Frozen(30);

			DataStore[31] = new MethodInvocation__Implementation__Frozen(31);

			DataStore[32] = new MethodInvocation__Implementation__Frozen(32);

			DataStore[33] = new MethodInvocation__Implementation__Frozen(33);

			DataStore[34] = new MethodInvocation__Implementation__Frozen(34);

			DataStore[35] = new MethodInvocation__Implementation__Frozen(35);

			DataStore[37] = new MethodInvocation__Implementation__Frozen(37);

			DataStore[44] = new MethodInvocation__Implementation__Frozen(44);

			DataStore[45] = new MethodInvocation__Implementation__Frozen(45);

			DataStore[46] = new MethodInvocation__Implementation__Frozen(46);

			DataStore[47] = new MethodInvocation__Implementation__Frozen(47);

			DataStore[48] = new MethodInvocation__Implementation__Frozen(48);

			DataStore[49] = new MethodInvocation__Implementation__Frozen(49);

			DataStore[50] = new MethodInvocation__Implementation__Frozen(50);

			DataStore[51] = new MethodInvocation__Implementation__Frozen(51);

			DataStore[52] = new MethodInvocation__Implementation__Frozen(52);

			DataStore[53] = new MethodInvocation__Implementation__Frozen(53);

			DataStore[54] = new MethodInvocation__Implementation__Frozen(54);

			DataStore[55] = new MethodInvocation__Implementation__Frozen(55);

			DataStore[56] = new MethodInvocation__Implementation__Frozen(56);

			DataStore[57] = new MethodInvocation__Implementation__Frozen(57);

			DataStore[58] = new MethodInvocation__Implementation__Frozen(58);

			DataStore[59] = new MethodInvocation__Implementation__Frozen(59);

			DataStore[60] = new MethodInvocation__Implementation__Frozen(60);

			DataStore[61] = new MethodInvocation__Implementation__Frozen(61);

			DataStore[62] = new MethodInvocation__Implementation__Frozen(62);

			DataStore[63] = new MethodInvocation__Implementation__Frozen(63);

			DataStore[64] = new MethodInvocation__Implementation__Frozen(64);

			DataStore[65] = new MethodInvocation__Implementation__Frozen(65);

			DataStore[66] = new MethodInvocation__Implementation__Frozen(66);

			DataStore[67] = new MethodInvocation__Implementation__Frozen(67);

			DataStore[72] = new MethodInvocation__Implementation__Frozen(72);

			DataStore[73] = new MethodInvocation__Implementation__Frozen(73);

			DataStore[74] = new MethodInvocation__Implementation__Frozen(74);

			DataStore[75] = new MethodInvocation__Implementation__Frozen(75);

			DataStore[76] = new MethodInvocation__Implementation__Frozen(76);

			DataStore[77] = new MethodInvocation__Implementation__Frozen(77);

			DataStore[78] = new MethodInvocation__Implementation__Frozen(78);

			DataStore[79] = new MethodInvocation__Implementation__Frozen(79);

			DataStore[80] = new MethodInvocation__Implementation__Frozen(80);

			DataStore[81] = new MethodInvocation__Implementation__Frozen(81);

			DataStore[82] = new MethodInvocation__Implementation__Frozen(82);

			DataStore[83] = new MethodInvocation__Implementation__Frozen(83);

			DataStore[87] = new MethodInvocation__Implementation__Frozen(87);

			DataStore[88] = new MethodInvocation__Implementation__Frozen(88);

			DataStore[89] = new MethodInvocation__Implementation__Frozen(89);

			DataStore[90] = new MethodInvocation__Implementation__Frozen(90);

			DataStore[91] = new MethodInvocation__Implementation__Frozen(91);

			DataStore[92] = new MethodInvocation__Implementation__Frozen(92);

			DataStore[93] = new MethodInvocation__Implementation__Frozen(93);

			DataStore[94] = new MethodInvocation__Implementation__Frozen(94);

			DataStore[95] = new MethodInvocation__Implementation__Frozen(95);

			DataStore[96] = new MethodInvocation__Implementation__Frozen(96);

			DataStore[97] = new MethodInvocation__Implementation__Frozen(97);

			DataStore[98] = new MethodInvocation__Implementation__Frozen(98);

			DataStore[99] = new MethodInvocation__Implementation__Frozen(99);

			DataStore[100] = new MethodInvocation__Implementation__Frozen(100);

			DataStore[101] = new MethodInvocation__Implementation__Frozen(101);

			DataStore[102] = new MethodInvocation__Implementation__Frozen(102);

			DataStore[103] = new MethodInvocation__Implementation__Frozen(103);

			DataStore[104] = new MethodInvocation__Implementation__Frozen(104);

			DataStore[105] = new MethodInvocation__Implementation__Frozen(105);

			DataStore[106] = new MethodInvocation__Implementation__Frozen(106);

			DataStore[107] = new MethodInvocation__Implementation__Frozen(107);

			DataStore[108] = new MethodInvocation__Implementation__Frozen(108);

			DataStore[109] = new MethodInvocation__Implementation__Frozen(109);

			DataStore[111] = new MethodInvocation__Implementation__Frozen(111);

			DataStore[113] = new MethodInvocation__Implementation__Frozen(113);

			DataStore[114] = new MethodInvocation__Implementation__Frozen(114);

			DataStore[115] = new MethodInvocation__Implementation__Frozen(115);

			DataStore[116] = new MethodInvocation__Implementation__Frozen(116);

			DataStore[117] = new MethodInvocation__Implementation__Frozen(117);

			DataStore[118] = new MethodInvocation__Implementation__Frozen(118);

			DataStore[119] = new MethodInvocation__Implementation__Frozen(119);

		}

		internal static void FillDataStore() {
			DataStore[1].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[8];
			DataStore[1].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[1].MemberName = @"OnToString_Projekt";
			DataStore[1].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[8];
			DataStore[1].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[1].Seal();
			DataStore[2].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[8];
			DataStore[2].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[2].MemberName = @"OnToString_Mitarbeiter";
			DataStore[2].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[17];
			DataStore[2].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[2].Seal();
			DataStore[4].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[8];
			DataStore[4].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[4].MemberName = @"OnToString_Task";
			DataStore[4].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[11];
			DataStore[4].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[4].Seal();
			DataStore[5].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[5].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[5].MemberName = @"OnToString_DataType";
			DataStore[5].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[5];
			DataStore[5].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[5].Seal();
			DataStore[6].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[6].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[6].MemberName = @"OnToString_MethodInvokation";
			DataStore[6].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[44];
			DataStore[6].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[6].Seal();
			DataStore[9].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[9].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[9].MemberName = @"OnToString_Method";
			DataStore[9].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[20];
			DataStore[9].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[9].Seal();
			DataStore[10].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[10].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[10].MemberName = @"OnToString_Module";
			DataStore[10].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[23];
			DataStore[10].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[10].Seal();
			DataStore[11].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[8];
			DataStore[11].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[11].MemberName = @"OnToString_Auftrag";
			DataStore[11].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[26];
			DataStore[11].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[11].Seal();
			DataStore[12].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[6];
			DataStore[12].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[12].MemberName = @"OnToString_Zeitkonto";
			DataStore[12].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[29];
			DataStore[12].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[12].Seal();
			DataStore[13].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[6];
			DataStore[13].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[13].MemberName = @"OnToString_Taetigkeit";
			DataStore[13].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[32];
			DataStore[13].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[13].Seal();
			DataStore[14].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[8];
			DataStore[14].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[14].MemberName = @"OnToString_Kunde";
			DataStore[14].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[35];
			DataStore[14].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[14].Seal();
			DataStore[15].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[5];
			DataStore[15].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27];
			DataStore[15].MemberName = @"OnToString_Icon";
			DataStore[15].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[38];
			DataStore[15].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[15].Seal();
			DataStore[16].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[16].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[16].MemberName = @"OnToString_Assembly";
			DataStore[16].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[41];
			DataStore[16].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[16].Seal();
			DataStore[17].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[17].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14];
			DataStore[17].MemberName = @"OnToString_ObjectReferenceProperty";
			DataStore[17].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[14];
			DataStore[17].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[17].Seal();
			DataStore[19].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[8];
			DataStore[19].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[19].MemberName = @"OnRechnungErstellen_Auftrag";
			DataStore[19].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[3];
			DataStore[19].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[19].Seal();
			DataStore[20].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[20].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[9];
			DataStore[20].MemberName = @"OnGetPropertyTypeString_StringProperty";
			DataStore[20].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[20].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[20].Seal();
			DataStore[21].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[21].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[11];
			DataStore[21].MemberName = @"OnGetPropertyTypeString_IntProperty";
			DataStore[21].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[21].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[21].Seal();
			DataStore[22].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[22].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[12];
			DataStore[22].MemberName = @"OnGetPropertyTypeString_BoolProperty";
			DataStore[22].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[22].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[22].Seal();
			DataStore[23].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[23].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[13];
			DataStore[23].MemberName = @"OnGetPropertyTypeString_DoubleProperty";
			DataStore[23].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[23].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[23].Seal();
			DataStore[24].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[24].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[15];
			DataStore[24].MemberName = @"OnGetPropertyTypeString_DateTimeProperty";
			DataStore[24].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[24].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[24].Seal();
			DataStore[26].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[26].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14];
			DataStore[26].MemberName = @"OnGetPropertyTypeString_ObjectReferenceProperty";
			DataStore[26].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[26].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[26].Seal();
			DataStore[28].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[28].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[28].MemberName = @"OnPreSave_ObjectClass";
			DataStore[28].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[6];
			DataStore[28].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[28].Seal();
			DataStore[29].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[3];
			DataStore[29].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[29].MemberName = @"OnPreSetObject_Projekt";
			DataStore[29].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[9];
			DataStore[29].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[29].Seal();
			DataStore[30].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[3];
			DataStore[30].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[30].MemberName = @"OnPreSetObject_Task";
			DataStore[30].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[12];
			DataStore[30].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[30].Seal();
			DataStore[31].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[31].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[9];
			DataStore[31].MemberName = @"OnGetPropertyTypeString_StringProperty";
			DataStore[31].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[31].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[31].Seal();
			DataStore[32].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[32].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[11];
			DataStore[32].MemberName = @"OnGetPropertyTypeString_IntProperty";
			DataStore[32].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[32].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[32].Seal();
			DataStore[33].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[33].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[12];
			DataStore[33].MemberName = @"OnGetPropertyTypeString_BoolProperty";
			DataStore[33].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[33].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[33].Seal();
			DataStore[34].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[34].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[13];
			DataStore[34].MemberName = @"OnGetPropertyTypeString_DoubleProperty";
			DataStore[34].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[34].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[34].Seal();
			DataStore[35].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[35].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[15];
			DataStore[35].MemberName = @"OnGetPropertyTypeString_DateTimeProperty";
			DataStore[35].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[35].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[35].Seal();
			DataStore[37].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[37].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14];
			DataStore[37].MemberName = @"OnGetPropertyTypeString_ObjectReferenceProperty";
			DataStore[37].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[37].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[37].Seal();
			DataStore[44].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[6];
			DataStore[44].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31];
			DataStore[44].MemberName = @"OnToString_TaetigkeitsArt";
			DataStore[44].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[71];
			DataStore[44].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[44].Seal();
			DataStore[45].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[2];
			DataStore[45].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[45].MemberName = @"OnPreSave_Zeitkonto";
			DataStore[45].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[30];
			DataStore[45].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[45].Seal();
			DataStore[46].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[2];
			DataStore[46].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[46].MemberName = @"OnPreSave_Taetigkeit";
			DataStore[46].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[33];
			DataStore[46].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[46].Seal();
			DataStore[47].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[47].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[37];
			DataStore[47].MemberName = @"OnGetParameterTypeString_StringParameter";
			DataStore[47].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[47].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[47].Seal();
			DataStore[48].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[48].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[38];
			DataStore[48].MemberName = @"OnGetParameterTypeString_IntParameter";
			DataStore[48].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[48].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[48].Seal();
			DataStore[49].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[49].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[49].MemberName = @"OnToString_BaseParameter";
			DataStore[49].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[80];
			DataStore[49].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[49].Seal();
			DataStore[50].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[50].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[37];
			DataStore[50].MemberName = @"OnGetParameterTypeString_StringParameter";
			DataStore[50].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[50].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[50].Seal();
			DataStore[51].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[51].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[38];
			DataStore[51].MemberName = @"OnGetParameterTypeString_IntParameter";
			DataStore[51].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[51].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[51].Seal();
			DataStore[52].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[52].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[39];
			DataStore[52].MemberName = @"OnGetParameterTypeString_DoubleParameter";
			DataStore[52].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[52].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[52].Seal();
			DataStore[53].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[53].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[39];
			DataStore[53].MemberName = @"OnGetParameterTypeString_DoubleParameter";
			DataStore[53].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[53].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[53].Seal();
			DataStore[54].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[54].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[41];
			DataStore[54].MemberName = @"OnGetParameterTypeString_DateTimeParameter";
			DataStore[54].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[54].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[54].Seal();
			DataStore[55].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[55].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[40];
			DataStore[55].MemberName = @"OnGetParameterTypeString_BoolParameter";
			DataStore[55].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[55].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[55].Seal();
			DataStore[56].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[56].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[40];
			DataStore[56].MemberName = @"OnGetParameterTypeString_BoolParameter";
			DataStore[56].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[56].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[56].Seal();
			DataStore[57].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[57].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[41];
			DataStore[57].MemberName = @"OnGetParameterTypeString_DateTimeParameter";
			DataStore[57].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[57].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[57].Seal();
			DataStore[58].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[58].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42];
			DataStore[58].MemberName = @"OnGetParameterTypeString_ObjectParameter";
			DataStore[58].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[58].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[58].Seal();
			DataStore[59].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[59].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42];
			DataStore[59].MemberName = @"OnGetParameterTypeString_ObjectParameter";
			DataStore[59].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[59].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[59].Seal();
			DataStore[60].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[60].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43];
			DataStore[60].MemberName = @"OnGetParameterTypeString_CLRObjectParameter";
			DataStore[60].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[60].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[60].Seal();
			DataStore[61].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[61].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43];
			DataStore[61].MemberName = @"OnGetParameterTypeString_CLRObjectParameter";
			DataStore[61].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[82];
			DataStore[61].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[61].Seal();
			DataStore[62].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[62].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[62].MemberName = @"OnPreSave_BaseParameter";
			DataStore[62].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[79];
			DataStore[62].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[62].Seal();
			DataStore[63].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[63].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[63].MemberName = @"OnPreSave_Method";
			DataStore[63].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[21];
			DataStore[63].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[63].Seal();
			DataStore[64].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[64].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[47];
			DataStore[64].MemberName = @"OnGetPropertyTypeString_EnumerationProperty";
			DataStore[64].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[64].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[64].Seal();
			DataStore[65].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[65].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[47];
			DataStore[65].MemberName = @"OnGetPropertyTypeString_EnumerationProperty";
			DataStore[65].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[65].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[65].Seal();
			DataStore[66].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[66].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45];
			DataStore[66].MemberName = @"OnToString_Enumeration";
			DataStore[66].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[85];
			DataStore[66].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[66].Seal();
			DataStore[67].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[67].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[67].MemberName = @"OnToString_EnumerationEntry";
			DataStore[67].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[88];
			DataStore[67].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[67].Seal();
			DataStore[72].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[72].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[72].MemberName = @"OnGetDataType_DataType";
			DataStore[72].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[121];
			DataStore[72].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[72].Seal();
			DataStore[73].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[73].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[73].MemberName = @"OnGetDataType_DataType";
			DataStore[73].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[121];
			DataStore[73].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[73].Seal();
			DataStore[74].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[74].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[74].MemberName = @"OnGetDataTypeString_DataType";
			DataStore[74].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[120];
			DataStore[74].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[74].Seal();
			DataStore[75].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[75].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[75].MemberName = @"OnGetDataTypeString_DataType";
			DataStore[75].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[120];
			DataStore[75].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[75].Seal();
			DataStore[76].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[76].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[76].MemberName = @"OnGetParameterType_BaseParameter";
			DataStore[76].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[123];
			DataStore[76].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[76].Seal();
			DataStore[77].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[77].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[77].MemberName = @"OnGetParameterType_BaseParameter";
			DataStore[77].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[123];
			DataStore[77].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[77].Seal();
			DataStore[78].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[78].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42];
			DataStore[78].MemberName = @"OnGetParameterType_ObjectParameter";
			DataStore[78].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[123];
			DataStore[78].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[78].Seal();
			DataStore[79].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[79].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42];
			DataStore[79].MemberName = @"OnGetParameterType_ObjectParameter";
			DataStore[79].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[123];
			DataStore[79].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[79].Seal();
			DataStore[80].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[80].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[80].MemberName = @"OnGetReturnParameter_Method";
			DataStore[80].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[124];
			DataStore[80].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[80].Seal();
			DataStore[81].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[81].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[81].MemberName = @"OnGetInheritedMethods_ObjectClass";
			DataStore[81].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[125];
			DataStore[81].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[81].Seal();
			DataStore[82].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[82].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[64];
			DataStore[82].MemberName = @"OnGetPropertyTypeString_StructProperty";
			DataStore[82].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[82].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[82].Seal();
			DataStore[83].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[83].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[64];
			DataStore[83].MemberName = @"OnGetPropertyTypeString_StructProperty";
			DataStore[83].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[83].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[83].Seal();
			DataStore[87].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[87].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[87].MemberName = @"OnIsValid_Constraint";
			DataStore[87].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[87].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[87].Seal();
			DataStore[88].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[88].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[70];
			DataStore[88].MemberName = @"OnIsValid_NotNullableConstraint";
			DataStore[88].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[88].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[88].Seal();
			DataStore[89].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[89].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[70];
			DataStore[89].MemberName = @"OnGetErrorText_NotNullableConstraint";
			DataStore[89].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[89].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[89].Seal();
			DataStore[90].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[90].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[70];
			DataStore[90].MemberName = @"OnToString_NotNullableConstraint";
			DataStore[90].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[136];
			DataStore[90].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[90].Seal();
			DataStore[91].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[91].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[71];
			DataStore[91].MemberName = @"OnToString_IntegerRangeConstraint";
			DataStore[91].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[136];
			DataStore[91].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[91].Seal();
			DataStore[92].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[92].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[71];
			DataStore[92].MemberName = @"OnGetErrorText_IntegerRangeConstraint";
			DataStore[92].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[92].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[92].Seal();
			DataStore[93].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[93].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[71];
			DataStore[93].MemberName = @"OnIsValid_IntegerRangeConstraint";
			DataStore[93].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[93].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[93].Seal();
			DataStore[94].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[5];
			DataStore[94].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[94].MemberName = @"OnToString_ControlInfo";
			DataStore[94].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[141];
			DataStore[94].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[94].Seal();
			DataStore[95].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[95].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[73];
			DataStore[95].MemberName = @"OnToString_StringRangeConstraint";
			DataStore[95].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[136];
			DataStore[95].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[95].Seal();
			DataStore[96].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[96].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[73];
			DataStore[96].MemberName = @"OnGetErrorText_StringRangeConstraint";
			DataStore[96].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[96].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[96].Seal();
			DataStore[97].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[97].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[73];
			DataStore[97].MemberName = @"OnIsValid_StringRangeConstraint";
			DataStore[97].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[97].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[97].Seal();
			DataStore[98].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[98].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[74];
			DataStore[98].MemberName = @"OnGetErrorText_MethodInvocationConstraint";
			DataStore[98].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[98].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[98].Seal();
			DataStore[99].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[99].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[74];
			DataStore[99].MemberName = @"OnToString_MethodInvocationConstraint";
			DataStore[99].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[136];
			DataStore[99].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[99].Seal();
			DataStore[100].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[100].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[74];
			DataStore[100].MemberName = @"OnIsValid_MethodInvocationConstraint";
			DataStore[100].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[100].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[100].Seal();
			DataStore[101].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[5];
			DataStore[101].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[101].MemberName = @"OnToString_PresenterInfo";
			DataStore[101].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[126];
			DataStore[101].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[101].Seal();
			DataStore[102].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[5];
			DataStore[102].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[102].MemberName = @"OnPrepareDefault_Template";
			DataStore[102].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[144];
			DataStore[102].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[102].Seal();
			DataStore[103].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[5];
			DataStore[103].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[103].MemberName = @"OnToString_Visual";
			DataStore[103].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[129];
			DataStore[103].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[103].Seal();
			DataStore[104].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[104].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[75];
			DataStore[104].MemberName = @"OnIsValid_IsValidIdentifierConstraint";
			DataStore[104].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[104].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[104].Seal();
			DataStore[105].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[105].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[75];
			DataStore[105].MemberName = @"OnGetErrorText_IsValidIdentifierConstraint";
			DataStore[105].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[105].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[105].Seal();
			DataStore[106].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[106].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[75];
			DataStore[106].MemberName = @"OnToString_IsValidIdentifierConstraint";
			DataStore[106].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[136];
			DataStore[106].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[106].Seal();
			DataStore[107].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[107].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[76];
			DataStore[107].MemberName = @"OnIsValid_IsValidNamespaceConstraint";
			DataStore[107].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[107].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[107].Seal();
			DataStore[108].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[108].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[108].MemberName = @"OnAsType_TypeRef";
			DataStore[108].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[151];
			DataStore[108].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[108].Seal();
			DataStore[109].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[109].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[109].MemberName = @"OnToString_TypeRef";
			DataStore[109].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[148];
			DataStore[109].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[109].Seal();
			DataStore[111].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[111].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[111].MemberName = @"OnRegenerateTypeRefs_Assembly";
			DataStore[111].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[155];
			DataStore[111].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[111].Seal();
			DataStore[113].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[113].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82];
			DataStore[113].MemberName = @"OnToString_RelationEnd";
			DataStore[113].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[157];
			DataStore[113].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[113].Seal();
			DataStore[114].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[114].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[114].MemberName = @"OnToString_Relation";
			DataStore[114].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[146];
			DataStore[114].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[114].Seal();
			DataStore[115].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[115].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[115].MemberName = @"OnGetPropertyTypeString_Property";
			DataStore[115].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[115].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[115].Seal();
			DataStore[116].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[116].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[116].MemberName = @"OnGetPropertyTypeString_Property";
			DataStore[116].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[1];
			DataStore[116].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[116].Seal();
			DataStore[117].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[4];
			DataStore[117].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[117].MemberName = @"OnGetPropertyType_Property";
			DataStore[117].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[118];
			DataStore[117].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[117].Seal();
			DataStore[118].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[118].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[118].MemberName = @"OnGetPropertyType_Property";
			DataStore[118].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[118];
			DataStore[118].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[118].Seal();
			DataStore[119].Implementor = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[1];
			DataStore[119].InvokeOnObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[119].MemberName = @"OnToString_Property";
			DataStore[119].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[14];
			DataStore[119].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[119].Seal();
	
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