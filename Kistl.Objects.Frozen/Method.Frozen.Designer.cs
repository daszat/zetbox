
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
    /// Metadefinition Object for Methods.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Method")]
    public class Method__Implementation__Frozen : BaseFrozenDataObject, Method
    {
    
		public Method__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Description of this Method
        /// </summary>
        // value type property
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Shows this Method in th GUI
        /// </summary>
        // value type property
        public virtual bool IsDisplayable
        {
            get
            {
                return _IsDisplayable;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsDisplayable != value)
                {
					var __oldValue = _IsDisplayable;
                    NotifyPropertyChanging("IsDisplayable", __oldValue, value);
                    _IsDisplayable = value;
                    NotifyPropertyChanged("IsDisplayable", __oldValue, value);
                }
            }
        }
        private bool _IsDisplayable;

        /// <summary>
        /// Methodenaufrufe implementiert in dieser Objekt Klasse
        /// </summary>
        // object list property
        public virtual ICollection<Kistl.App.Base.MethodInvocation> MethodInvokations
        {
            get
            {
                if (_MethodInvokations == null)
                    _MethodInvokations = new ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0));
                return _MethodInvokations;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _MethodInvokations = (ReadOnlyCollection<Kistl.App.Base.MethodInvocation>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.MethodInvocation> _MethodInvokations;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string MethodName
        {
            get
            {
                return _MethodName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MethodName != value)
                {
					var __oldValue = _MethodName;
                    NotifyPropertyChanging("MethodName", __oldValue, value);
                    _MethodName = value;
                    NotifyPropertyChanged("MethodName", __oldValue, value);
                }
            }
        }
        private string _MethodName;

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

        /// <summary>
        /// 
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                return _ObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjectClass != value)
                {
					var __oldValue = _ObjectClass;
                    NotifyPropertyChanging("ObjectClass", __oldValue, value);
                    _ObjectClass = value;
                    NotifyPropertyChanged("ObjectClass", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.DataType _ObjectClass;

        /// <summary>
        /// Parameter der Methode
        /// </summary>
        // object list property
        public virtual IList<Kistl.App.Base.BaseParameter> Parameter
        {
            get
            {
                if (_Parameter == null)
                    _Parameter = new ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0));
                return _Parameter;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _Parameter = (ReadOnlyCollection<Kistl.App.Base.BaseParameter>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.BaseParameter> _Parameter;

        /// <summary>
        /// Returns the Return Parameter Meta Object of this Method Meta Object.
        /// </summary>

		public virtual Kistl.App.Base.BaseParameter GetReturnParameter() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.BaseParameter>();
            if (OnGetReturnParameter_Method != null)
            {
                OnGetReturnParameter_Method(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Method.GetReturnParameter");
            }
            return e.Result;
        }
		public delegate void GetReturnParameter_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.BaseParameter> ret);
		public event GetReturnParameter_Handler<Method> OnGetReturnParameter_Method;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Method));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Method != null)
            {
                OnToString_Method(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Method> OnToString_Method;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Method != null) OnPreSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPreSave_Method;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Method != null) OnPostSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPostSave_Method;


        internal Method__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Method__Implementation__Frozen> DataStore = new Dictionary<int, Method__Implementation__Frozen>(122);
		internal static void CreateInstances()
		{
			DataStore[1] = new Method__Implementation__Frozen(1);

			DataStore[3] = new Method__Implementation__Frozen(3);

			DataStore[4] = new Method__Implementation__Frozen(4);

			DataStore[5] = new Method__Implementation__Frozen(5);

			DataStore[6] = new Method__Implementation__Frozen(6);

			DataStore[7] = new Method__Implementation__Frozen(7);

			DataStore[8] = new Method__Implementation__Frozen(8);

			DataStore[9] = new Method__Implementation__Frozen(9);

			DataStore[10] = new Method__Implementation__Frozen(10);

			DataStore[11] = new Method__Implementation__Frozen(11);

			DataStore[12] = new Method__Implementation__Frozen(12);

			DataStore[13] = new Method__Implementation__Frozen(13);

			DataStore[14] = new Method__Implementation__Frozen(14);

			DataStore[15] = new Method__Implementation__Frozen(15);

			DataStore[16] = new Method__Implementation__Frozen(16);

			DataStore[17] = new Method__Implementation__Frozen(17);

			DataStore[18] = new Method__Implementation__Frozen(18);

			DataStore[19] = new Method__Implementation__Frozen(19);

			DataStore[20] = new Method__Implementation__Frozen(20);

			DataStore[21] = new Method__Implementation__Frozen(21);

			DataStore[22] = new Method__Implementation__Frozen(22);

			DataStore[23] = new Method__Implementation__Frozen(23);

			DataStore[24] = new Method__Implementation__Frozen(24);

			DataStore[25] = new Method__Implementation__Frozen(25);

			DataStore[26] = new Method__Implementation__Frozen(26);

			DataStore[27] = new Method__Implementation__Frozen(27);

			DataStore[28] = new Method__Implementation__Frozen(28);

			DataStore[29] = new Method__Implementation__Frozen(29);

			DataStore[30] = new Method__Implementation__Frozen(30);

			DataStore[31] = new Method__Implementation__Frozen(31);

			DataStore[32] = new Method__Implementation__Frozen(32);

			DataStore[33] = new Method__Implementation__Frozen(33);

			DataStore[34] = new Method__Implementation__Frozen(34);

			DataStore[35] = new Method__Implementation__Frozen(35);

			DataStore[36] = new Method__Implementation__Frozen(36);

			DataStore[37] = new Method__Implementation__Frozen(37);

			DataStore[38] = new Method__Implementation__Frozen(38);

			DataStore[39] = new Method__Implementation__Frozen(39);

			DataStore[40] = new Method__Implementation__Frozen(40);

			DataStore[41] = new Method__Implementation__Frozen(41);

			DataStore[42] = new Method__Implementation__Frozen(42);

			DataStore[43] = new Method__Implementation__Frozen(43);

			DataStore[44] = new Method__Implementation__Frozen(44);

			DataStore[45] = new Method__Implementation__Frozen(45);

			DataStore[71] = new Method__Implementation__Frozen(71);

			DataStore[72] = new Method__Implementation__Frozen(72);

			DataStore[73] = new Method__Implementation__Frozen(73);

			DataStore[74] = new Method__Implementation__Frozen(74);

			DataStore[75] = new Method__Implementation__Frozen(75);

			DataStore[76] = new Method__Implementation__Frozen(76);

			DataStore[79] = new Method__Implementation__Frozen(79);

			DataStore[80] = new Method__Implementation__Frozen(80);

			DataStore[81] = new Method__Implementation__Frozen(81);

			DataStore[82] = new Method__Implementation__Frozen(82);

			DataStore[83] = new Method__Implementation__Frozen(83);

			DataStore[84] = new Method__Implementation__Frozen(84);

			DataStore[85] = new Method__Implementation__Frozen(85);

			DataStore[86] = new Method__Implementation__Frozen(86);

			DataStore[87] = new Method__Implementation__Frozen(87);

			DataStore[88] = new Method__Implementation__Frozen(88);

			DataStore[89] = new Method__Implementation__Frozen(89);

			DataStore[90] = new Method__Implementation__Frozen(90);

			DataStore[91] = new Method__Implementation__Frozen(91);

			DataStore[92] = new Method__Implementation__Frozen(92);

			DataStore[93] = new Method__Implementation__Frozen(93);

			DataStore[95] = new Method__Implementation__Frozen(95);

			DataStore[96] = new Method__Implementation__Frozen(96);

			DataStore[97] = new Method__Implementation__Frozen(97);

			DataStore[98] = new Method__Implementation__Frozen(98);

			DataStore[106] = new Method__Implementation__Frozen(106);

			DataStore[107] = new Method__Implementation__Frozen(107);

			DataStore[108] = new Method__Implementation__Frozen(108);

			DataStore[109] = new Method__Implementation__Frozen(109);

			DataStore[110] = new Method__Implementation__Frozen(110);

			DataStore[111] = new Method__Implementation__Frozen(111);

			DataStore[112] = new Method__Implementation__Frozen(112);

			DataStore[113] = new Method__Implementation__Frozen(113);

			DataStore[114] = new Method__Implementation__Frozen(114);

			DataStore[115] = new Method__Implementation__Frozen(115);

			DataStore[116] = new Method__Implementation__Frozen(116);

			DataStore[117] = new Method__Implementation__Frozen(117);

			DataStore[118] = new Method__Implementation__Frozen(118);

			DataStore[120] = new Method__Implementation__Frozen(120);

			DataStore[121] = new Method__Implementation__Frozen(121);

			DataStore[123] = new Method__Implementation__Frozen(123);

			DataStore[124] = new Method__Implementation__Frozen(124);

			DataStore[125] = new Method__Implementation__Frozen(125);

			DataStore[126] = new Method__Implementation__Frozen(126);

			DataStore[127] = new Method__Implementation__Frozen(127);

			DataStore[128] = new Method__Implementation__Frozen(128);

			DataStore[129] = new Method__Implementation__Frozen(129);

			DataStore[130] = new Method__Implementation__Frozen(130);

			DataStore[131] = new Method__Implementation__Frozen(131);

			DataStore[132] = new Method__Implementation__Frozen(132);

			DataStore[133] = new Method__Implementation__Frozen(133);

			DataStore[134] = new Method__Implementation__Frozen(134);

			DataStore[135] = new Method__Implementation__Frozen(135);

			DataStore[136] = new Method__Implementation__Frozen(136);

			DataStore[137] = new Method__Implementation__Frozen(137);

			DataStore[138] = new Method__Implementation__Frozen(138);

			DataStore[139] = new Method__Implementation__Frozen(139);

			DataStore[141] = new Method__Implementation__Frozen(141);

			DataStore[142] = new Method__Implementation__Frozen(142);

			DataStore[143] = new Method__Implementation__Frozen(143);

			DataStore[144] = new Method__Implementation__Frozen(144);

			DataStore[145] = new Method__Implementation__Frozen(145);

			DataStore[146] = new Method__Implementation__Frozen(146);

			DataStore[147] = new Method__Implementation__Frozen(147);

			DataStore[148] = new Method__Implementation__Frozen(148);

			DataStore[149] = new Method__Implementation__Frozen(149);

			DataStore[150] = new Method__Implementation__Frozen(150);

			DataStore[151] = new Method__Implementation__Frozen(151);

			DataStore[155] = new Method__Implementation__Frozen(155);

			DataStore[157] = new Method__Implementation__Frozen(157);

			DataStore[158] = new Method__Implementation__Frozen(158);

			DataStore[159] = new Method__Implementation__Frozen(159);

			DataStore[160] = new Method__Implementation__Frozen(160);

			DataStore[161] = new Method__Implementation__Frozen(161);

			DataStore[162] = new Method__Implementation__Frozen(162);

			DataStore[163] = new Method__Implementation__Frozen(163);

			DataStore[164] = new Method__Implementation__Frozen(164);

			DataStore[165] = new Method__Implementation__Frozen(165);

		}

		internal static void FillDataStore() {
			DataStore[1].Description = @"Returns the String representation of this Property Meta Object.";
			DataStore[1].IsDisplayable = true;
			DataStore[1].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(18) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[20],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[21],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[22],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[23],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[24],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[26],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[31],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[32],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[33],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[34],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[35],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[37],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[64],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[65],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[82],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[83],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[115],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[116],
});
			DataStore[1].MethodName = @"GetPropertyTypeString";
			DataStore[1].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[1].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[1].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[3],
});
			DataStore[1].Seal();
			DataStore[3].Description = @"Testmethode zum Erstellen von Rechnungen mit Word";
			DataStore[3].IsDisplayable = true;
			DataStore[3].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[19],
});
			DataStore[3].MethodName = @"RechnungErstellen";
			DataStore[3].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[3].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[3].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[3].Seal();
			DataStore[4].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[4].IsDisplayable = false;
			DataStore[4].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[4].MethodName = @"PostSave";
			DataStore[4].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[4].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[4].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[4].Seal();
			DataStore[5].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[5].IsDisplayable = false;
			DataStore[5].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[5],
});
			DataStore[5].MethodName = @"ToString";
			DataStore[5].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[5].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[5].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[5].Seal();
			DataStore[6].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[6].IsDisplayable = false;
			DataStore[6].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[28],
});
			DataStore[6].MethodName = @"PreSave";
			DataStore[6].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[6].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[6].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[6].Seal();
			DataStore[7].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[7].IsDisplayable = false;
			DataStore[7].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[7].MethodName = @"PostSave";
			DataStore[7].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[7].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[7].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[7].Seal();
			DataStore[8].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[8].IsDisplayable = false;
			DataStore[8].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[1],
});
			DataStore[8].MethodName = @"ToString";
			DataStore[8].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[8].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[8].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[8].Seal();
			DataStore[9].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[9].IsDisplayable = false;
			DataStore[9].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[29],
});
			DataStore[9].MethodName = @"PreSave";
			DataStore[9].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[9].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[9].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[9].Seal();
			DataStore[10].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[10].IsDisplayable = false;
			DataStore[10].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[10].MethodName = @"PostSave";
			DataStore[10].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[10].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[10].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[10].Seal();
			DataStore[11].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[11].IsDisplayable = false;
			DataStore[11].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[4],
});
			DataStore[11].MethodName = @"ToString";
			DataStore[11].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[11].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[11].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[11].Seal();
			DataStore[12].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[12].IsDisplayable = false;
			DataStore[12].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[30],
});
			DataStore[12].MethodName = @"PreSave";
			DataStore[12].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[12].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[12].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[12].Seal();
			DataStore[13].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[13].IsDisplayable = false;
			DataStore[13].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[13].MethodName = @"PostSave";
			DataStore[13].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[13].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[13].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[13].Seal();
			DataStore[14].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[14].IsDisplayable = false;
			DataStore[14].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[17],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[119],
});
			DataStore[14].MethodName = @"ToString";
			DataStore[14].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[14].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[14].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[14].Seal();
			DataStore[15].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[15].IsDisplayable = false;
			DataStore[15].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[15].MethodName = @"PreSave";
			DataStore[15].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[15].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[15].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[15].Seal();
			DataStore[16].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[16].IsDisplayable = false;
			DataStore[16].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[16].MethodName = @"PostSave";
			DataStore[16].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[16].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[16].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[16].Seal();
			DataStore[17].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[17].IsDisplayable = false;
			DataStore[17].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[2],
});
			DataStore[17].MethodName = @"ToString";
			DataStore[17].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[17].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[17].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[17].Seal();
			DataStore[18].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[18].IsDisplayable = false;
			DataStore[18].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[18].MethodName = @"PreSave";
			DataStore[18].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[18].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[18].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[18].Seal();
			DataStore[19].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[19].IsDisplayable = false;
			DataStore[19].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[19].MethodName = @"PostSave";
			DataStore[19].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[19].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[19].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[19].Seal();
			DataStore[20].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[20].IsDisplayable = false;
			DataStore[20].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[9],
});
			DataStore[20].MethodName = @"ToString";
			DataStore[20].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[20].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[20].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[20].Seal();
			DataStore[21].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[21].IsDisplayable = false;
			DataStore[21].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[63],
});
			DataStore[21].MethodName = @"PreSave";
			DataStore[21].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[21].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[21].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[21].Seal();
			DataStore[22].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[22].IsDisplayable = false;
			DataStore[22].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[22].MethodName = @"PostSave";
			DataStore[22].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[22].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[22].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[22].Seal();
			DataStore[23].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[23].IsDisplayable = false;
			DataStore[23].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[10],
});
			DataStore[23].MethodName = @"ToString";
			DataStore[23].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[23].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[23].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[23].Seal();
			DataStore[24].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[24].IsDisplayable = false;
			DataStore[24].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[24].MethodName = @"PreSave";
			DataStore[24].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[24].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[24].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[24].Seal();
			DataStore[25].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[25].IsDisplayable = false;
			DataStore[25].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[25].MethodName = @"PostSave";
			DataStore[25].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[25].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[25].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[25].Seal();
			DataStore[26].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[26].IsDisplayable = false;
			DataStore[26].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[11],
});
			DataStore[26].MethodName = @"ToString";
			DataStore[26].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[26].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[26].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[26].Seal();
			DataStore[27].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[27].IsDisplayable = false;
			DataStore[27].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[27].MethodName = @"PreSave";
			DataStore[27].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[27].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[27].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[27].Seal();
			DataStore[28].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[28].IsDisplayable = false;
			DataStore[28].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[28].MethodName = @"PostSave";
			DataStore[28].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[28].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[28].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[28].Seal();
			DataStore[29].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[29].IsDisplayable = false;
			DataStore[29].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[12],
});
			DataStore[29].MethodName = @"ToString";
			DataStore[29].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[29].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[29].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[29].Seal();
			DataStore[30].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[30].IsDisplayable = false;
			DataStore[30].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[45],
});
			DataStore[30].MethodName = @"PreSave";
			DataStore[30].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[30].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[30].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[30].Seal();
			DataStore[31].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[31].IsDisplayable = false;
			DataStore[31].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[31].MethodName = @"PostSave";
			DataStore[31].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[31].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[31].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[31].Seal();
			DataStore[32].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[32].IsDisplayable = false;
			DataStore[32].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[13],
});
			DataStore[32].MethodName = @"ToString";
			DataStore[32].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[32].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[32].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[32].Seal();
			DataStore[33].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[33].IsDisplayable = false;
			DataStore[33].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[46],
});
			DataStore[33].MethodName = @"PreSave";
			DataStore[33].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[33].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[33].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[33].Seal();
			DataStore[34].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[34].IsDisplayable = false;
			DataStore[34].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[34].MethodName = @"PostSave";
			DataStore[34].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[34].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[34].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[34].Seal();
			DataStore[35].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[35].IsDisplayable = false;
			DataStore[35].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[14],
});
			DataStore[35].MethodName = @"ToString";
			DataStore[35].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[35].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[35].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[35].Seal();
			DataStore[36].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[36].IsDisplayable = false;
			DataStore[36].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[36].MethodName = @"PreSave";
			DataStore[36].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[36].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26];
			DataStore[36].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[36].Seal();
			DataStore[37].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[37].IsDisplayable = false;
			DataStore[37].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[37].MethodName = @"PostSave";
			DataStore[37].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[37].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27];
			DataStore[37].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[37].Seal();
			DataStore[38].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[38].IsDisplayable = false;
			DataStore[38].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[15],
});
			DataStore[38].MethodName = @"ToString";
			DataStore[38].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[38].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27];
			DataStore[38].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[38].Seal();
			DataStore[39].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[39].IsDisplayable = false;
			DataStore[39].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[39].MethodName = @"PreSave";
			DataStore[39].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[39].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27];
			DataStore[39].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[39].Seal();
			DataStore[40].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[40].IsDisplayable = false;
			DataStore[40].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[40].MethodName = @"PostSave";
			DataStore[40].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[40].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[40].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[40].Seal();
			DataStore[41].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[41].IsDisplayable = false;
			DataStore[41].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[16],
});
			DataStore[41].MethodName = @"ToString";
			DataStore[41].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[41].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[41].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[41].Seal();
			DataStore[42].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[42].IsDisplayable = false;
			DataStore[42].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[42].MethodName = @"PreSave";
			DataStore[42].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[42].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[42].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[42].Seal();
			DataStore[43].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[43].IsDisplayable = false;
			DataStore[43].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[43].MethodName = @"PostSave";
			DataStore[43].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[43].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[43].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[43].Seal();
			DataStore[44].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[44].IsDisplayable = false;
			DataStore[44].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[6],
});
			DataStore[44].MethodName = @"ToString";
			DataStore[44].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[44].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[44].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[44].Seal();
			DataStore[45].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[45].IsDisplayable = false;
			DataStore[45].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[45].MethodName = @"PreSave";
			DataStore[45].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[45].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[45].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[45].Seal();
			DataStore[71].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[71].IsDisplayable = false;
			DataStore[71].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[44],
});
			DataStore[71].MethodName = @"ToString";
			DataStore[71].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[71].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31];
			DataStore[71].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[71].Seal();
			DataStore[72].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[72].IsDisplayable = false;
			DataStore[72].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[72].MethodName = @"PreSave";
			DataStore[72].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[72].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31];
			DataStore[72].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[72].Seal();
			DataStore[73].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[73].IsDisplayable = false;
			DataStore[73].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[73].MethodName = @"PostSave";
			DataStore[73].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[73].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31];
			DataStore[73].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[73].Seal();
			DataStore[74].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[74].IsDisplayable = false;
			DataStore[74].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[74].MethodName = @"ToString";
			DataStore[74].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[74].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[74].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[74].Seal();
			DataStore[75].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[75].IsDisplayable = false;
			DataStore[75].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[75].MethodName = @"PreSave";
			DataStore[75].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[75].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[75].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[75].Seal();
			DataStore[76].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[76].IsDisplayable = false;
			DataStore[76].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[76].MethodName = @"PostSave";
			DataStore[76].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[76].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[76].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[76].Seal();
			DataStore[79].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[79].IsDisplayable = false;
			DataStore[79].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[62],
});
			DataStore[79].MethodName = @"PreSave";
			DataStore[79].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[79].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[79].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[79].Seal();
			DataStore[80].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[80].IsDisplayable = false;
			DataStore[80].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[49],
});
			DataStore[80].MethodName = @"ToString";
			DataStore[80].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[80].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[80].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[80].Seal();
			DataStore[81].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[81].IsDisplayable = false;
			DataStore[81].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[81].MethodName = @"PostSave";
			DataStore[81].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[81].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[81].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[81].Seal();
			DataStore[82].Description = @"Returns the String representation of this Method-Parameter Meta Object.";
			DataStore[82].IsDisplayable = false;
			DataStore[82].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(14) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[47],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[48],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[50],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[51],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[52],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[53],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[54],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[55],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[56],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[57],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[58],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[59],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[60],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[61],
});
			DataStore[82].MethodName = @"GetParameterTypeString";
			DataStore[82].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[82].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[82].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[5],
});
			DataStore[82].Seal();
			DataStore[83].Description = null;
			DataStore[83].IsDisplayable = false;
			DataStore[83].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[83].MethodName = @"TestMethodForParameter";
			DataStore[83].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[83].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[83].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(8) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[1],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[2],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[6],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[7],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[8],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[9],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[10],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[11],
});
			DataStore[83].Seal();
			DataStore[84].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[84].IsDisplayable = false;
			DataStore[84].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[84].MethodName = @"PreSave";
			DataStore[84].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[84].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45];
			DataStore[84].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[84].Seal();
			DataStore[85].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[85].IsDisplayable = false;
			DataStore[85].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[66],
});
			DataStore[85].MethodName = @"ToString";
			DataStore[85].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[85].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45];
			DataStore[85].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[85].Seal();
			DataStore[86].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[86].IsDisplayable = false;
			DataStore[86].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[86].MethodName = @"PostSave";
			DataStore[86].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[86].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45];
			DataStore[86].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[86].Seal();
			DataStore[87].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[87].IsDisplayable = false;
			DataStore[87].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[87].MethodName = @"PreSave";
			DataStore[87].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[87].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[87].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[87].Seal();
			DataStore[88].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[88].IsDisplayable = false;
			DataStore[88].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[67],
});
			DataStore[88].MethodName = @"ToString";
			DataStore[88].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[88].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[88].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[88].Seal();
			DataStore[89].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[89].IsDisplayable = false;
			DataStore[89].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[89].MethodName = @"PostSave";
			DataStore[89].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[89].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[89].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[89].Seal();
			DataStore[90].Description = null;
			DataStore[90].IsDisplayable = true;
			DataStore[90].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[90].MethodName = @"TestMethod";
			DataStore[90].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[90].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[48];
			DataStore[90].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[12],
});
			DataStore[90].Seal();
			DataStore[91].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[91].IsDisplayable = false;
			DataStore[91].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[91].MethodName = @"PreSave";
			DataStore[91].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[91].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51];
			DataStore[91].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[91].Seal();
			DataStore[92].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[92].IsDisplayable = false;
			DataStore[92].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[92].MethodName = @"ToString";
			DataStore[92].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[92].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51];
			DataStore[92].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[92].Seal();
			DataStore[93].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[93].IsDisplayable = false;
			DataStore[93].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[93].MethodName = @"PostSave";
			DataStore[93].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[93].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51];
			DataStore[93].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[93].Seal();
			DataStore[95].Description = @"testmethod";
			DataStore[95].IsDisplayable = true;
			DataStore[95].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[95].MethodName = @"TestMethod";
			DataStore[95].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[95].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51];
			DataStore[95].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[13],
});
			DataStore[95].Seal();
			DataStore[96].Description = null;
			DataStore[96].IsDisplayable = false;
			DataStore[96].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[96].MethodName = @"ShowMessage";
			DataStore[96].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[96].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[52];
			DataStore[96].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[14],
});
			DataStore[96].Seal();
			DataStore[97].Description = null;
			DataStore[97].IsDisplayable = false;
			DataStore[97].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[97].MethodName = @"ShowObject";
			DataStore[97].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[97].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[52];
			DataStore[97].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[16],
});
			DataStore[97].Seal();
			DataStore[98].Description = null;
			DataStore[98].IsDisplayable = false;
			DataStore[98].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[98].MethodName = @"ChooseObject";
			DataStore[98].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[98].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[52];
			DataStore[98].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(3) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[18],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[19],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[20],
});
			DataStore[98].Seal();
			DataStore[106].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[106].IsDisplayable = false;
			DataStore[106].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[106].MethodName = @"PreSave";
			DataStore[106].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[106].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[106].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[106].Seal();
			DataStore[107].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[107].IsDisplayable = false;
			DataStore[107].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[107].MethodName = @"ToString";
			DataStore[107].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[107].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[107].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[107].Seal();
			DataStore[108].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[108].IsDisplayable = false;
			DataStore[108].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[108].MethodName = @"PostSave";
			DataStore[108].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[108].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58];
			DataStore[108].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[108].Seal();
			DataStore[109].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[109].IsDisplayable = false;
			DataStore[109].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[109].MethodName = @"PreSave";
			DataStore[109].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[109].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[59];
			DataStore[109].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[109].Seal();
			DataStore[110].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[110].IsDisplayable = false;
			DataStore[110].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[110].MethodName = @"ToString";
			DataStore[110].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[110].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[59];
			DataStore[110].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[110].Seal();
			DataStore[111].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[111].IsDisplayable = false;
			DataStore[111].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[111].MethodName = @"PostSave";
			DataStore[111].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[111].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[59];
			DataStore[111].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[111].Seal();
			DataStore[112].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[112].IsDisplayable = false;
			DataStore[112].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[112].MethodName = @"PreSave";
			DataStore[112].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[112].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[60];
			DataStore[112].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[112].Seal();
			DataStore[113].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[113].IsDisplayable = false;
			DataStore[113].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[113].MethodName = @"ToString";
			DataStore[113].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[113].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[60];
			DataStore[113].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[113].Seal();
			DataStore[114].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[114].IsDisplayable = false;
			DataStore[114].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[114].MethodName = @"PostSave";
			DataStore[114].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[114].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[60];
			DataStore[114].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[114].Seal();
			DataStore[115].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[115].IsDisplayable = false;
			DataStore[115].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[115].MethodName = @"PreSave";
			DataStore[115].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[115].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[61];
			DataStore[115].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[115].Seal();
			DataStore[116].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[116].IsDisplayable = false;
			DataStore[116].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[116].MethodName = @"ToString";
			DataStore[116].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[116].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[61];
			DataStore[116].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[116].Seal();
			DataStore[117].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[117].IsDisplayable = false;
			DataStore[117].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[117].MethodName = @"PostSave";
			DataStore[117].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[117].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[61];
			DataStore[117].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[117].Seal();
			DataStore[118].Description = @"Returns the resulting Type of this Property Meta Object.";
			DataStore[118].IsDisplayable = false;
			DataStore[118].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[117],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[118],
});
			DataStore[118].MethodName = @"GetPropertyType";
			DataStore[118].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[118].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7];
			DataStore[118].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[21],
});
			DataStore[118].Seal();
			DataStore[120].Description = @"Returns the String representation of this Datatype Meta Object.";
			DataStore[120].IsDisplayable = false;
			DataStore[120].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[74],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[75],
});
			DataStore[120].MethodName = @"GetDataTypeString";
			DataStore[120].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[120].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[120].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[23],
});
			DataStore[120].Seal();
			DataStore[121].Description = @"Returns the resulting Type of this Datatype Meta Object.";
			DataStore[121].IsDisplayable = false;
			DataStore[121].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[72],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[73],
});
			DataStore[121].MethodName = @"GetDataType";
			DataStore[121].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[121].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[121].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[24],
});
			DataStore[121].Seal();
			DataStore[123].Description = @"Returns the resulting Type of this Method-Parameter Meta Object.";
			DataStore[123].IsDisplayable = false;
			DataStore[123].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(4) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[76],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[77],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[78],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[79],
});
			DataStore[123].MethodName = @"GetParameterType";
			DataStore[123].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[123].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[123].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[25],
});
			DataStore[123].Seal();
			DataStore[124].Description = @"Returns the Return Parameter Meta Object of this Method Meta Object.";
			DataStore[124].IsDisplayable = true;
			DataStore[124].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[80],
});
			DataStore[124].MethodName = @"GetReturnParameter";
			DataStore[124].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[124].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[124].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[26],
});
			DataStore[124].Seal();
			DataStore[125].Description = null;
			DataStore[125].IsDisplayable = true;
			DataStore[125].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[81],
});
			DataStore[125].MethodName = @"GetInheritedMethods";
			DataStore[125].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[125].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[125].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[27],
});
			DataStore[125].Seal();
			DataStore[126].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[126].IsDisplayable = false;
			DataStore[126].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[101],
});
			DataStore[126].MethodName = @"ToString";
			DataStore[126].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[126].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[126].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[126].Seal();
			DataStore[127].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[127].IsDisplayable = false;
			DataStore[127].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[127].MethodName = @"PreSave";
			DataStore[127].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[127].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[127].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[127].Seal();
			DataStore[128].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[128].IsDisplayable = false;
			DataStore[128].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[128].MethodName = @"PostSave";
			DataStore[128].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[128].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[128].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[128].Seal();
			DataStore[129].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[129].IsDisplayable = false;
			DataStore[129].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[103],
});
			DataStore[129].MethodName = @"ToString";
			DataStore[129].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[129].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[129].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[129].Seal();
			DataStore[130].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[130].IsDisplayable = false;
			DataStore[130].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[130].MethodName = @"PostSave";
			DataStore[130].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[130].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[130].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[130].Seal();
			DataStore[131].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[131].IsDisplayable = false;
			DataStore[131].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[131].MethodName = @"PreSave";
			DataStore[131].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[131].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[131].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[131].Seal();
			DataStore[132].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[132].IsDisplayable = false;
			DataStore[132].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[132].MethodName = @"ToString";
			DataStore[132].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[132].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[132].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[132].Seal();
			DataStore[133].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[133].IsDisplayable = false;
			DataStore[133].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[133].MethodName = @"PreSave";
			DataStore[133].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[133].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[133].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[133].Seal();
			DataStore[134].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[134].IsDisplayable = false;
			DataStore[134].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[134].MethodName = @"PostSave";
			DataStore[134].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[134].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[134].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[134].Seal();
			DataStore[135].Description = null;
			DataStore[135].IsDisplayable = false;
			DataStore[135].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(7) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[87],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[88],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[93],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[97],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[100],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[104],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[107],
});
			DataStore[135].MethodName = @"IsValid";
			DataStore[135].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[135].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[135].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(3) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[28],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[29],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[34],
});
			DataStore[135].Seal();
			DataStore[136].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[136].IsDisplayable = false;
			DataStore[136].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(5) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[90],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[91],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[95],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[99],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[106],
});
			DataStore[136].MethodName = @"ToString";
			DataStore[136].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[136].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[136].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[136].Seal();
			DataStore[137].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[137].IsDisplayable = false;
			DataStore[137].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[137].MethodName = @"PreSave";
			DataStore[137].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[137].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[137].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[137].Seal();
			DataStore[138].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[138].IsDisplayable = false;
			DataStore[138].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[138].MethodName = @"PostSave";
			DataStore[138].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[138].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[138].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[138].Seal();
			DataStore[139].Description = null;
			DataStore[139].IsDisplayable = false;
			DataStore[139].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(5) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[89],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[92],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[96],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[98],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[105],
});
			DataStore[139].MethodName = @"GetErrorText";
			DataStore[139].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[139].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[139].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(3) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[30],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[31],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[35],
});
			DataStore[139].Seal();
			DataStore[141].Description = @"Autogenerated! Returns a String that represents the current Object.";
			DataStore[141].IsDisplayable = false;
			DataStore[141].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[94],
});
			DataStore[141].MethodName = @"ToString";
			DataStore[141].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[141].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[141].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[33],
});
			DataStore[141].Seal();
			DataStore[142].Description = @"Autogenerated! Method is called by the Context before a commit occurs.";
			DataStore[142].IsDisplayable = false;
			DataStore[142].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[142].MethodName = @"PreSave";
			DataStore[142].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[142].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[142].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[142].Seal();
			DataStore[143].Description = @"Autogenerated! Method is called by the Context after a commit occurs.";
			DataStore[143].IsDisplayable = false;
			DataStore[143].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[143].MethodName = @"PostSave";
			DataStore[143].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[143].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[143].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[143].Seal();
			DataStore[144].Description = null;
			DataStore[144].IsDisplayable = true;
			DataStore[144].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[102],
});
			DataStore[144].MethodName = @"PrepareDefault";
			DataStore[144].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[144].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[144].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(1) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[36],
});
			DataStore[144].Seal();
			DataStore[145].Description = null;
			DataStore[145].IsDisplayable = false;
			DataStore[145].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[145].MethodName = @"PreSave";
			DataStore[145].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[145].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[145].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[145].Seal();
			DataStore[146].Description = null;
			DataStore[146].IsDisplayable = false;
			DataStore[146].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[114],
});
			DataStore[146].MethodName = @"ToString";
			DataStore[146].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[146].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[146].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[146].Seal();
			DataStore[147].Description = null;
			DataStore[147].IsDisplayable = false;
			DataStore[147].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[147].MethodName = @"PostSave";
			DataStore[147].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[147].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[147].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[147].Seal();
			DataStore[148].Description = null;
			DataStore[148].IsDisplayable = false;
			DataStore[148].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[109],
});
			DataStore[148].MethodName = @"ToString";
			DataStore[148].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[148].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[148].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[148].Seal();
			DataStore[149].Description = null;
			DataStore[149].IsDisplayable = false;
			DataStore[149].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[149].MethodName = @"PreSave";
			DataStore[149].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[149].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[149].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[149].Seal();
			DataStore[150].Description = null;
			DataStore[150].IsDisplayable = false;
			DataStore[150].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[150].MethodName = @"PostSave";
			DataStore[150].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[150].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[150].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[150].Seal();
			DataStore[151].Description = @"get the referenced <see cref=""System.Type""/>";
			DataStore[151].IsDisplayable = false;
			DataStore[151].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[108],
});
			DataStore[151].MethodName = @"AsType";
			DataStore[151].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[151].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[151].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(2) {
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[37],
Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[38],
});
			DataStore[151].Seal();
			DataStore[155].Description = @"Regenerates the stored list of TypeRefs from the loaded assembly";
			DataStore[155].IsDisplayable = true;
			DataStore[155].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[111],
});
			DataStore[155].MethodName = @"RegenerateTypeRefs";
			DataStore[155].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[155].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[155].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[155].Seal();
			DataStore[157].Description = null;
			DataStore[157].IsDisplayable = false;
			DataStore[157].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[113],
});
			DataStore[157].MethodName = @"ToString";
			DataStore[157].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[157].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82];
			DataStore[157].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[157].Seal();
			DataStore[158].Description = null;
			DataStore[158].IsDisplayable = false;
			DataStore[158].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[158].MethodName = @"PostSave";
			DataStore[158].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[158].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82];
			DataStore[158].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[158].Seal();
			DataStore[159].Description = null;
			DataStore[159].IsDisplayable = false;
			DataStore[159].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[159].MethodName = @"PreSave";
			DataStore[159].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[159].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82];
			DataStore[159].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[159].Seal();
			DataStore[160].Description = null;
			DataStore[160].IsDisplayable = false;
			DataStore[160].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[160].MethodName = @"PreSave";
			DataStore[160].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[160].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[83];
			DataStore[160].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[160].Seal();
			DataStore[161].Description = null;
			DataStore[161].IsDisplayable = false;
			DataStore[161].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[161].MethodName = @"PostSave";
			DataStore[161].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[161].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[83];
			DataStore[161].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[161].Seal();
			DataStore[162].Description = null;
			DataStore[162].IsDisplayable = false;
			DataStore[162].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[162].MethodName = @"ToString";
			DataStore[162].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[162].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[83];
			DataStore[162].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[162].Seal();
			DataStore[163].Description = null;
			DataStore[163].IsDisplayable = false;
			DataStore[163].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[163].MethodName = @"PreSave";
			DataStore[163].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[163].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[85];
			DataStore[163].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[163].Seal();
			DataStore[164].Description = null;
			DataStore[164].IsDisplayable = false;
			DataStore[164].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[164].MethodName = @"PostSave";
			DataStore[164].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[164].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[85];
			DataStore[164].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[164].Seal();
			DataStore[165].Description = null;
			DataStore[165].IsDisplayable = false;
			DataStore[165].MethodInvokations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[165].MethodName = @"ToString";
			DataStore[165].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[165].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[85];
			DataStore[165].Parameter = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.BaseParameter>(new List<Kistl.App.Base.BaseParameter>(0) {
});
			DataStore[165].Seal();
	
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