
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
    /// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TypeRef")]
    public class TypeRef__Implementation__Frozen : BaseFrozenDataObject, TypeRef
    {


        /// <summary>
        /// The assembly containing the referenced Type.
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return _Assembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Assembly != value)
                {
                    NotifyPropertyChanging("Assembly");
                    _Assembly = value;
                    NotifyPropertyChanged("Assembly");
                }
            }
        }
        private Kistl.App.Base.Assembly _Assembly;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_FullName != value)
                {
                    NotifyPropertyChanging("FullName");
                    _FullName = value;
                    NotifyPropertyChanged("FullName");
                }
            }
        }
        private string _FullName;

        /// <summary>
        /// list of type arguments
        /// </summary>
        // collection reference property
        public virtual IList<Kistl.App.Base.TypeRef> GenericArguments
        {
            get
            {
                if (_GenericArguments == null)
                    _GenericArguments = new ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0));
                return _GenericArguments;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _GenericArguments = (ReadOnlyCollection<Kistl.App.Base.TypeRef>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.TypeRef> _GenericArguments;

        /// <summary>
        /// get the referenced <see cref="System.Type"/>
        /// </summary>

		public virtual System.Type AsType(System.Boolean throwOnError) 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnAsType_TypeRef != null)
            {
                OnAsType_TypeRef(this, e, throwOnError);
            }
            else
            {
                throw new NotImplementedException("No handler registered on TypeRef.AsType");
            }
            return e.Result;
        }
		public delegate void AsType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret, System.Boolean throwOnError);
		public event AsType_Handler<TypeRef> OnAsType_TypeRef;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TypeRef));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TypeRef != null)
            {
                OnToString_TypeRef(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TypeRef> OnToString_TypeRef;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TypeRef != null) OnPreSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPreSave_TypeRef;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TypeRef != null) OnPostSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPostSave_TypeRef;


        internal TypeRef__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, TypeRef__Implementation__Frozen> DataStore = new Dictionary<int, TypeRef__Implementation__Frozen>(272);
		internal static void CreateInstances()
		{
			DataStore[1] = new TypeRef__Implementation__Frozen(1);

			DataStore[2] = new TypeRef__Implementation__Frozen(2);

			DataStore[3] = new TypeRef__Implementation__Frozen(3);

			DataStore[4] = new TypeRef__Implementation__Frozen(4);

			DataStore[5] = new TypeRef__Implementation__Frozen(5);

			DataStore[6] = new TypeRef__Implementation__Frozen(6);

			DataStore[7] = new TypeRef__Implementation__Frozen(7);

			DataStore[8] = new TypeRef__Implementation__Frozen(8);

			DataStore[9] = new TypeRef__Implementation__Frozen(9);

			DataStore[10] = new TypeRef__Implementation__Frozen(10);

			DataStore[11] = new TypeRef__Implementation__Frozen(11);

			DataStore[12] = new TypeRef__Implementation__Frozen(12);

			DataStore[13] = new TypeRef__Implementation__Frozen(13);

			DataStore[14] = new TypeRef__Implementation__Frozen(14);

			DataStore[15] = new TypeRef__Implementation__Frozen(15);

			DataStore[16] = new TypeRef__Implementation__Frozen(16);

			DataStore[17] = new TypeRef__Implementation__Frozen(17);

			DataStore[18] = new TypeRef__Implementation__Frozen(18);

			DataStore[19] = new TypeRef__Implementation__Frozen(19);

			DataStore[20] = new TypeRef__Implementation__Frozen(20);

			DataStore[21] = new TypeRef__Implementation__Frozen(21);

			DataStore[22] = new TypeRef__Implementation__Frozen(22);

			DataStore[23] = new TypeRef__Implementation__Frozen(23);

			DataStore[24] = new TypeRef__Implementation__Frozen(24);

			DataStore[25] = new TypeRef__Implementation__Frozen(25);

			DataStore[26] = new TypeRef__Implementation__Frozen(26);

			DataStore[27] = new TypeRef__Implementation__Frozen(27);

			DataStore[28] = new TypeRef__Implementation__Frozen(28);

			DataStore[29] = new TypeRef__Implementation__Frozen(29);

			DataStore[30] = new TypeRef__Implementation__Frozen(30);

			DataStore[31] = new TypeRef__Implementation__Frozen(31);

			DataStore[32] = new TypeRef__Implementation__Frozen(32);

			DataStore[33] = new TypeRef__Implementation__Frozen(33);

			DataStore[34] = new TypeRef__Implementation__Frozen(34);

			DataStore[35] = new TypeRef__Implementation__Frozen(35);

			DataStore[36] = new TypeRef__Implementation__Frozen(36);

			DataStore[37] = new TypeRef__Implementation__Frozen(37);

			DataStore[38] = new TypeRef__Implementation__Frozen(38);

			DataStore[39] = new TypeRef__Implementation__Frozen(39);

			DataStore[40] = new TypeRef__Implementation__Frozen(40);

			DataStore[41] = new TypeRef__Implementation__Frozen(41);

			DataStore[42] = new TypeRef__Implementation__Frozen(42);

			DataStore[43] = new TypeRef__Implementation__Frozen(43);

			DataStore[44] = new TypeRef__Implementation__Frozen(44);

			DataStore[45] = new TypeRef__Implementation__Frozen(45);

			DataStore[46] = new TypeRef__Implementation__Frozen(46);

			DataStore[47] = new TypeRef__Implementation__Frozen(47);

			DataStore[48] = new TypeRef__Implementation__Frozen(48);

			DataStore[49] = new TypeRef__Implementation__Frozen(49);

			DataStore[50] = new TypeRef__Implementation__Frozen(50);

			DataStore[51] = new TypeRef__Implementation__Frozen(51);

			DataStore[52] = new TypeRef__Implementation__Frozen(52);

			DataStore[53] = new TypeRef__Implementation__Frozen(53);

			DataStore[54] = new TypeRef__Implementation__Frozen(54);

			DataStore[55] = new TypeRef__Implementation__Frozen(55);

			DataStore[56] = new TypeRef__Implementation__Frozen(56);

			DataStore[57] = new TypeRef__Implementation__Frozen(57);

			DataStore[58] = new TypeRef__Implementation__Frozen(58);

			DataStore[59] = new TypeRef__Implementation__Frozen(59);

			DataStore[60] = new TypeRef__Implementation__Frozen(60);

			DataStore[61] = new TypeRef__Implementation__Frozen(61);

			DataStore[62] = new TypeRef__Implementation__Frozen(62);

			DataStore[63] = new TypeRef__Implementation__Frozen(63);

			DataStore[64] = new TypeRef__Implementation__Frozen(64);

			DataStore[65] = new TypeRef__Implementation__Frozen(65);

			DataStore[66] = new TypeRef__Implementation__Frozen(66);

			DataStore[67] = new TypeRef__Implementation__Frozen(67);

			DataStore[68] = new TypeRef__Implementation__Frozen(68);

			DataStore[69] = new TypeRef__Implementation__Frozen(69);

			DataStore[70] = new TypeRef__Implementation__Frozen(70);

			DataStore[71] = new TypeRef__Implementation__Frozen(71);

			DataStore[72] = new TypeRef__Implementation__Frozen(72);

			DataStore[73] = new TypeRef__Implementation__Frozen(73);

			DataStore[74] = new TypeRef__Implementation__Frozen(74);

			DataStore[75] = new TypeRef__Implementation__Frozen(75);

			DataStore[76] = new TypeRef__Implementation__Frozen(76);

			DataStore[77] = new TypeRef__Implementation__Frozen(77);

			DataStore[78] = new TypeRef__Implementation__Frozen(78);

			DataStore[79] = new TypeRef__Implementation__Frozen(79);

			DataStore[80] = new TypeRef__Implementation__Frozen(80);

			DataStore[81] = new TypeRef__Implementation__Frozen(81);

			DataStore[82] = new TypeRef__Implementation__Frozen(82);

			DataStore[83] = new TypeRef__Implementation__Frozen(83);

			DataStore[84] = new TypeRef__Implementation__Frozen(84);

			DataStore[85] = new TypeRef__Implementation__Frozen(85);

			DataStore[86] = new TypeRef__Implementation__Frozen(86);

			DataStore[87] = new TypeRef__Implementation__Frozen(87);

			DataStore[88] = new TypeRef__Implementation__Frozen(88);

			DataStore[89] = new TypeRef__Implementation__Frozen(89);

			DataStore[90] = new TypeRef__Implementation__Frozen(90);

			DataStore[91] = new TypeRef__Implementation__Frozen(91);

			DataStore[92] = new TypeRef__Implementation__Frozen(92);

			DataStore[93] = new TypeRef__Implementation__Frozen(93);

			DataStore[94] = new TypeRef__Implementation__Frozen(94);

			DataStore[95] = new TypeRef__Implementation__Frozen(95);

			DataStore[96] = new TypeRef__Implementation__Frozen(96);

			DataStore[97] = new TypeRef__Implementation__Frozen(97);

			DataStore[98] = new TypeRef__Implementation__Frozen(98);

			DataStore[99] = new TypeRef__Implementation__Frozen(99);

			DataStore[100] = new TypeRef__Implementation__Frozen(100);

			DataStore[101] = new TypeRef__Implementation__Frozen(101);

			DataStore[102] = new TypeRef__Implementation__Frozen(102);

			DataStore[103] = new TypeRef__Implementation__Frozen(103);

			DataStore[104] = new TypeRef__Implementation__Frozen(104);

			DataStore[105] = new TypeRef__Implementation__Frozen(105);

			DataStore[106] = new TypeRef__Implementation__Frozen(106);

			DataStore[107] = new TypeRef__Implementation__Frozen(107);

			DataStore[108] = new TypeRef__Implementation__Frozen(108);

			DataStore[109] = new TypeRef__Implementation__Frozen(109);

			DataStore[110] = new TypeRef__Implementation__Frozen(110);

			DataStore[111] = new TypeRef__Implementation__Frozen(111);

			DataStore[112] = new TypeRef__Implementation__Frozen(112);

			DataStore[113] = new TypeRef__Implementation__Frozen(113);

			DataStore[114] = new TypeRef__Implementation__Frozen(114);

			DataStore[115] = new TypeRef__Implementation__Frozen(115);

			DataStore[116] = new TypeRef__Implementation__Frozen(116);

			DataStore[117] = new TypeRef__Implementation__Frozen(117);

			DataStore[118] = new TypeRef__Implementation__Frozen(118);

			DataStore[119] = new TypeRef__Implementation__Frozen(119);

			DataStore[120] = new TypeRef__Implementation__Frozen(120);

			DataStore[121] = new TypeRef__Implementation__Frozen(121);

			DataStore[122] = new TypeRef__Implementation__Frozen(122);

			DataStore[123] = new TypeRef__Implementation__Frozen(123);

			DataStore[124] = new TypeRef__Implementation__Frozen(124);

			DataStore[125] = new TypeRef__Implementation__Frozen(125);

			DataStore[126] = new TypeRef__Implementation__Frozen(126);

			DataStore[127] = new TypeRef__Implementation__Frozen(127);

			DataStore[128] = new TypeRef__Implementation__Frozen(128);

			DataStore[129] = new TypeRef__Implementation__Frozen(129);

			DataStore[130] = new TypeRef__Implementation__Frozen(130);

			DataStore[131] = new TypeRef__Implementation__Frozen(131);

			DataStore[132] = new TypeRef__Implementation__Frozen(132);

			DataStore[133] = new TypeRef__Implementation__Frozen(133);

			DataStore[134] = new TypeRef__Implementation__Frozen(134);

			DataStore[135] = new TypeRef__Implementation__Frozen(135);

			DataStore[136] = new TypeRef__Implementation__Frozen(136);

			DataStore[137] = new TypeRef__Implementation__Frozen(137);

			DataStore[138] = new TypeRef__Implementation__Frozen(138);

			DataStore[139] = new TypeRef__Implementation__Frozen(139);

			DataStore[140] = new TypeRef__Implementation__Frozen(140);

			DataStore[141] = new TypeRef__Implementation__Frozen(141);

			DataStore[142] = new TypeRef__Implementation__Frozen(142);

			DataStore[143] = new TypeRef__Implementation__Frozen(143);

			DataStore[144] = new TypeRef__Implementation__Frozen(144);

			DataStore[145] = new TypeRef__Implementation__Frozen(145);

			DataStore[146] = new TypeRef__Implementation__Frozen(146);

			DataStore[147] = new TypeRef__Implementation__Frozen(147);

			DataStore[148] = new TypeRef__Implementation__Frozen(148);

			DataStore[149] = new TypeRef__Implementation__Frozen(149);

			DataStore[150] = new TypeRef__Implementation__Frozen(150);

			DataStore[151] = new TypeRef__Implementation__Frozen(151);

			DataStore[152] = new TypeRef__Implementation__Frozen(152);

			DataStore[153] = new TypeRef__Implementation__Frozen(153);

			DataStore[154] = new TypeRef__Implementation__Frozen(154);

			DataStore[155] = new TypeRef__Implementation__Frozen(155);

			DataStore[156] = new TypeRef__Implementation__Frozen(156);

			DataStore[157] = new TypeRef__Implementation__Frozen(157);

			DataStore[158] = new TypeRef__Implementation__Frozen(158);

			DataStore[159] = new TypeRef__Implementation__Frozen(159);

			DataStore[160] = new TypeRef__Implementation__Frozen(160);

			DataStore[161] = new TypeRef__Implementation__Frozen(161);

			DataStore[162] = new TypeRef__Implementation__Frozen(162);

			DataStore[163] = new TypeRef__Implementation__Frozen(163);

			DataStore[164] = new TypeRef__Implementation__Frozen(164);

			DataStore[165] = new TypeRef__Implementation__Frozen(165);

			DataStore[166] = new TypeRef__Implementation__Frozen(166);

			DataStore[167] = new TypeRef__Implementation__Frozen(167);

			DataStore[168] = new TypeRef__Implementation__Frozen(168);

			DataStore[169] = new TypeRef__Implementation__Frozen(169);

			DataStore[170] = new TypeRef__Implementation__Frozen(170);

			DataStore[171] = new TypeRef__Implementation__Frozen(171);

			DataStore[172] = new TypeRef__Implementation__Frozen(172);

			DataStore[173] = new TypeRef__Implementation__Frozen(173);

			DataStore[174] = new TypeRef__Implementation__Frozen(174);

			DataStore[175] = new TypeRef__Implementation__Frozen(175);

			DataStore[176] = new TypeRef__Implementation__Frozen(176);

			DataStore[177] = new TypeRef__Implementation__Frozen(177);

			DataStore[178] = new TypeRef__Implementation__Frozen(178);

			DataStore[179] = new TypeRef__Implementation__Frozen(179);

			DataStore[180] = new TypeRef__Implementation__Frozen(180);

			DataStore[181] = new TypeRef__Implementation__Frozen(181);

			DataStore[182] = new TypeRef__Implementation__Frozen(182);

			DataStore[183] = new TypeRef__Implementation__Frozen(183);

			DataStore[184] = new TypeRef__Implementation__Frozen(184);

			DataStore[185] = new TypeRef__Implementation__Frozen(185);

			DataStore[186] = new TypeRef__Implementation__Frozen(186);

			DataStore[187] = new TypeRef__Implementation__Frozen(187);

			DataStore[188] = new TypeRef__Implementation__Frozen(188);

			DataStore[189] = new TypeRef__Implementation__Frozen(189);

			DataStore[190] = new TypeRef__Implementation__Frozen(190);

			DataStore[191] = new TypeRef__Implementation__Frozen(191);

			DataStore[192] = new TypeRef__Implementation__Frozen(192);

			DataStore[193] = new TypeRef__Implementation__Frozen(193);

			DataStore[194] = new TypeRef__Implementation__Frozen(194);

			DataStore[195] = new TypeRef__Implementation__Frozen(195);

			DataStore[196] = new TypeRef__Implementation__Frozen(196);

			DataStore[197] = new TypeRef__Implementation__Frozen(197);

			DataStore[198] = new TypeRef__Implementation__Frozen(198);

			DataStore[199] = new TypeRef__Implementation__Frozen(199);

			DataStore[200] = new TypeRef__Implementation__Frozen(200);

			DataStore[201] = new TypeRef__Implementation__Frozen(201);

			DataStore[202] = new TypeRef__Implementation__Frozen(202);

			DataStore[203] = new TypeRef__Implementation__Frozen(203);

			DataStore[204] = new TypeRef__Implementation__Frozen(204);

			DataStore[205] = new TypeRef__Implementation__Frozen(205);

			DataStore[206] = new TypeRef__Implementation__Frozen(206);

			DataStore[207] = new TypeRef__Implementation__Frozen(207);

			DataStore[208] = new TypeRef__Implementation__Frozen(208);

			DataStore[209] = new TypeRef__Implementation__Frozen(209);

			DataStore[210] = new TypeRef__Implementation__Frozen(210);

			DataStore[211] = new TypeRef__Implementation__Frozen(211);

			DataStore[212] = new TypeRef__Implementation__Frozen(212);

			DataStore[213] = new TypeRef__Implementation__Frozen(213);

			DataStore[214] = new TypeRef__Implementation__Frozen(214);

			DataStore[215] = new TypeRef__Implementation__Frozen(215);

			DataStore[216] = new TypeRef__Implementation__Frozen(216);

			DataStore[217] = new TypeRef__Implementation__Frozen(217);

			DataStore[218] = new TypeRef__Implementation__Frozen(218);

			DataStore[219] = new TypeRef__Implementation__Frozen(219);

			DataStore[220] = new TypeRef__Implementation__Frozen(220);

			DataStore[221] = new TypeRef__Implementation__Frozen(221);

			DataStore[222] = new TypeRef__Implementation__Frozen(222);

			DataStore[223] = new TypeRef__Implementation__Frozen(223);

			DataStore[224] = new TypeRef__Implementation__Frozen(224);

			DataStore[225] = new TypeRef__Implementation__Frozen(225);

			DataStore[226] = new TypeRef__Implementation__Frozen(226);

			DataStore[227] = new TypeRef__Implementation__Frozen(227);

			DataStore[228] = new TypeRef__Implementation__Frozen(228);

			DataStore[229] = new TypeRef__Implementation__Frozen(229);

			DataStore[230] = new TypeRef__Implementation__Frozen(230);

			DataStore[231] = new TypeRef__Implementation__Frozen(231);

			DataStore[232] = new TypeRef__Implementation__Frozen(232);

			DataStore[233] = new TypeRef__Implementation__Frozen(233);

			DataStore[234] = new TypeRef__Implementation__Frozen(234);

			DataStore[235] = new TypeRef__Implementation__Frozen(235);

			DataStore[236] = new TypeRef__Implementation__Frozen(236);

			DataStore[237] = new TypeRef__Implementation__Frozen(237);

			DataStore[238] = new TypeRef__Implementation__Frozen(238);

			DataStore[239] = new TypeRef__Implementation__Frozen(239);

			DataStore[240] = new TypeRef__Implementation__Frozen(240);

			DataStore[241] = new TypeRef__Implementation__Frozen(241);

			DataStore[242] = new TypeRef__Implementation__Frozen(242);

			DataStore[243] = new TypeRef__Implementation__Frozen(243);

			DataStore[244] = new TypeRef__Implementation__Frozen(244);

			DataStore[245] = new TypeRef__Implementation__Frozen(245);

			DataStore[246] = new TypeRef__Implementation__Frozen(246);

			DataStore[247] = new TypeRef__Implementation__Frozen(247);

			DataStore[248] = new TypeRef__Implementation__Frozen(248);

			DataStore[249] = new TypeRef__Implementation__Frozen(249);

			DataStore[250] = new TypeRef__Implementation__Frozen(250);

			DataStore[251] = new TypeRef__Implementation__Frozen(251);

			DataStore[252] = new TypeRef__Implementation__Frozen(252);

			DataStore[253] = new TypeRef__Implementation__Frozen(253);

			DataStore[254] = new TypeRef__Implementation__Frozen(254);

			DataStore[255] = new TypeRef__Implementation__Frozen(255);

			DataStore[256] = new TypeRef__Implementation__Frozen(256);

			DataStore[257] = new TypeRef__Implementation__Frozen(257);

			DataStore[258] = new TypeRef__Implementation__Frozen(258);

			DataStore[259] = new TypeRef__Implementation__Frozen(259);

			DataStore[260] = new TypeRef__Implementation__Frozen(260);

			DataStore[261] = new TypeRef__Implementation__Frozen(261);

			DataStore[262] = new TypeRef__Implementation__Frozen(262);

			DataStore[263] = new TypeRef__Implementation__Frozen(263);

			DataStore[264] = new TypeRef__Implementation__Frozen(264);

			DataStore[265] = new TypeRef__Implementation__Frozen(265);

			DataStore[266] = new TypeRef__Implementation__Frozen(266);

			DataStore[267] = new TypeRef__Implementation__Frozen(267);

			DataStore[268] = new TypeRef__Implementation__Frozen(268);

			DataStore[269] = new TypeRef__Implementation__Frozen(269);

			DataStore[270] = new TypeRef__Implementation__Frozen(270);

			DataStore[271] = new TypeRef__Implementation__Frozen(271);

			DataStore[272] = new TypeRef__Implementation__Frozen(272);

		}

		internal static void FillDataStore() {
			DataStore[1].FullName = @"Kistl.App.Base.CustomClientActions_KistlBase";
			DataStore[1].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[1];
			DataStore[1].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[1].Seal();
			DataStore[2].FullName = @"Kistl.App.Zeiterfassung.CustomServerActions_Zeiterfassung";
			DataStore[2].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[2];
			DataStore[2].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[2].Seal();
			DataStore[3].FullName = @"Kistl.App.Projekte.CustomServerActions_Projekte";
			DataStore[3].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[2];
			DataStore[3].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[3].Seal();
			DataStore[4].FullName = @"Kistl.App.Base.CustomServerActions_KistlBase";
			DataStore[4].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[2];
			DataStore[4].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[4].Seal();
			DataStore[5].FullName = @"Kistl.App.GUI.CustomClientActions_GUI";
			DataStore[5].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[1];
			DataStore[5].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[5].Seal();
			DataStore[6].FullName = @"Kistl.App.Zeiterfassung.CustomClientActions_Zeiterfassung";
			DataStore[6].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[1];
			DataStore[6].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[6].Seal();
			DataStore[7].FullName = @"Kistl.App.Base.CustomClientActions_KistlBase";
			DataStore[7].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[1];
			DataStore[7].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[7].Seal();
			DataStore[8].FullName = @"Kistl.App.Projekte.CustomClientActions_Projekte";
			DataStore[8].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[1];
			DataStore[8].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[8].Seal();
			DataStore[9].FullName = @"Kistl.Client.Presentables.ModuleModel";
			DataStore[9].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[9].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[9].Seal();
			DataStore[10].FullName = @"Kistl.Client.Presentables.ObjectClassModel";
			DataStore[10].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[10].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[10].Seal();
			DataStore[11].FullName = @"Kistl.Client.Presentables.DataTypeModel";
			DataStore[11].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[11].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[11].Seal();
			DataStore[12].FullName = @"Kistl.Client.Presentables.SaveContextCommand";
			DataStore[12].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[12].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[12].Seal();
			DataStore[13].FullName = @"Kistl.Client.Presentables.WorkspaceModel";
			DataStore[13].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[13].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[13].Seal();
			DataStore[14].FullName = @"Kistl.Client.ClientExtensions";
			DataStore[14].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[14].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[14].Seal();
			DataStore[15].FullName = @"Kistl.Client.ClientHelper";
			DataStore[15].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[15].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[15].Seal();
			DataStore[16].FullName = @"Kistl.Client.Presentables.ActionModel";
			DataStore[16].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[16].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[16].Seal();
			DataStore[17].FullName = @"Kistl.Client.Presentables.ObjectListModel";
			DataStore[17].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[17].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[17].Seal();
			DataStore[18].FullName = @"Kistl.Client.Presentables.DataObjectSelectionTaskModel";
			DataStore[18].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[18].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[18].Seal();
			DataStore[19].FullName = @"Kistl.Client.Presentables.SelectionTaskModel`1";
			DataStore[19].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[19].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[19].Seal();
			DataStore[20].FullName = @"Kistl.Client.Presentables.SimpleReferenceListPropertyModel`1";
			DataStore[20].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[20].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[20].Seal();
			DataStore[21].FullName = @"Kistl.Client.Presentables.ReferenceListPropertyModel`2";
			DataStore[21].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[21].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[21].Seal();
			DataStore[22].FullName = @"Kistl.Client.Presentables.IValueListModel`1";
			DataStore[22].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[22].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[22].Seal();
			DataStore[23].FullName = @"Kistl.Client.ServerDomainManager";
			DataStore[23].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[23].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[23].Seal();
			DataStore[24].FullName = @"Kistl.Client.Presentables.SynchronousThreadManager";
			DataStore[24].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[24].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[24].Seal();
			DataStore[25].FullName = @"Kistl.Client.Presentables.WPF.AsyncThreadManager";
			DataStore[25].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[25].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[25].Seal();
			DataStore[26].FullName = @"Kistl.Client.Presentables.WPF.UiThreadManager";
			DataStore[26].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[26].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[26].Seal();
			DataStore[27].FullName = @"Kistl.Client.Presentables.IThreadManager";
			DataStore[27].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[27].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[27].Seal();
			DataStore[28].FullName = @"Kistl.GUI.ObjectListMethodPresenter";
			DataStore[28].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[28].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[28].Seal();
			DataStore[29].FullName = @"Kistl.GUI.ObjectMethodPresenter";
			DataStore[29].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[29].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[29].Seal();
			DataStore[30].FullName = @"Kistl.GUI.DefaultMethodPresenter`1";
			DataStore[30].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[30].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[30].Seal();
			DataStore[31].FullName = @"Kistl.Client.TheseMethodsShouldBeImplementedOnKistlObjects";
			DataStore[31].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[31].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[31].Seal();
			DataStore[32].FullName = @"Kistl.GUI.Renderer.BasicRenderer`3";
			DataStore[32].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[32].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[32].Seal();
			DataStore[33].FullName = @"Kistl.GUI.Renderer.IRenderer";
			DataStore[33].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[33].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[33].Seal();
			DataStore[34].FullName = @"Kistl.Client.Presentables.ObjectReferenceModel";
			DataStore[34].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[34].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[34].Seal();
			DataStore[35].FullName = @"Kistl.GUI.DB.KistlGUIContext";
			DataStore[35].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[35].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[35].Seal();
			DataStore[36].FullName = @"Kistl.GUI.ObjectReferencePresenter`1";
			DataStore[36].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[36].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[36].Seal();
			DataStore[37].FullName = @"Kistl.GUI.ObjectListPresenter`1";
			DataStore[37].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[37].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[37].Seal();
			DataStore[38].FullName = @"Kistl.GUI.ListPresenter`2";
			DataStore[38].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[38].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[38].Seal();
			DataStore[39].FullName = @"Kistl.Client.GUI.IView";
			DataStore[39].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[39].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[39].Seal();
			DataStore[40].FullName = @"Kistl.Client.Presentables.ModelState";
			DataStore[40].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[40].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[40].Seal();
			DataStore[41].FullName = @"Kistl.Client.Presentables.MethodInvocationModel";
			DataStore[41].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[41].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[41].Seal();
			DataStore[42].FullName = @"Kistl.Client.Presentables.ObjectResultModel`1";
			DataStore[42].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[42].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[42].Seal();
			DataStore[43].FullName = @"Kistl.Client.Presentables.NullableResultModel`1";
			DataStore[43].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[43].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[43].Seal();
			DataStore[44].FullName = @"Kistl.Client.Presentables.MethodResultModel`1";
			DataStore[44].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[44].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[44].Seal();
			DataStore[45].FullName = @"Kistl.Client.Presentables.CommandModel";
			DataStore[45].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[45].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[45].Seal();
			DataStore[46].FullName = @"Kistl.Client.Presentables.ICommand";
			DataStore[46].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[46].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[46].Seal();
			DataStore[47].FullName = @"Kistl.Client.GuiApplicationContext";
			DataStore[47].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[47].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[47].Seal();
			DataStore[48].FullName = @"Kistl.Client.IGuiApplicationContext";
			DataStore[48].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[48].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[48].Seal();
			DataStore[49].FullName = @"Kistl.GUI.ExtensionHelper";
			DataStore[49].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[49].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[49].Seal();
			DataStore[50].FullName = @"Kistl.GUI.WorkspacePresenter";
			DataStore[50].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[50].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[50].Seal();
			DataStore[51].FullName = @"Kistl.GUI.ObjectPresenter";
			DataStore[51].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[51].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[51].Seal();
			DataStore[52].FullName = @"Kistl.GUI.ActionPresenter";
			DataStore[52].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[52].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[52].Seal();
			DataStore[53].FullName = @"Kistl.GUI.GroupPresenter";
			DataStore[53].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[53].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[53].Seal();
			DataStore[54].FullName = @"Kistl.GUI.ToolBarPresenter";
			DataStore[54].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[54].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[54].Seal();
			DataStore[55].FullName = @"Kistl.GUI.EnumerationPresenter`1";
			DataStore[55].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[55].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[55].Seal();
			DataStore[56].FullName = @"Kistl.GUI.DefaultValuePresenter`1";
			DataStore[56].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[56].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[56].Seal();
			DataStore[57].FullName = @"Kistl.GUI.DefaultStructPresenter`1";
			DataStore[57].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[57].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[57].Seal();
			DataStore[58].FullName = @"Kistl.GUI.DefaultListPresenter`1";
			DataStore[58].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[58].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[58].Seal();
			DataStore[59].FullName = @"Kistl.GUI.DefaultPresenter`4";
			DataStore[59].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[59].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[59].Seal();
			DataStore[60].FullName = @"Kistl.GUI.Presenter`1";
			DataStore[60].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[60].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[60].Seal();
			DataStore[61].FullName = @"Kistl.GUI.IPresenter";
			DataStore[61].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[61].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[61].Seal();
			DataStore[62].FullName = @"Kistl.Client.GUI.DB.DataMocks";
			DataStore[62].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[62].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[62].Seal();
			DataStore[63].FullName = @"Kistl.Client.GUI.DB.DebuggerLayout";
			DataStore[63].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[63].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[63].Seal();
			DataStore[64].FullName = @"Kistl.Client.GUI.DB.ActionLayout";
			DataStore[64].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[64].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[64].Seal();
			DataStore[65].FullName = @"Kistl.Client.GUI.DB.SelectionTaskLayout";
			DataStore[65].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[65].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[65].Seal();
			DataStore[66].FullName = @"Kistl.Client.GUI.DB.DataObjectReferenceLayout";
			DataStore[66].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[66].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[66].Seal();
			DataStore[67].FullName = @"Kistl.Client.GUI.DB.DataObjectListLayout";
			DataStore[67].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[67].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[67].Seal();
			DataStore[68].FullName = @"Kistl.Client.GUI.DB.DataObjectFullLayout";
			DataStore[68].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[68].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[68].Seal();
			DataStore[69].FullName = @"Kistl.Client.GUI.DB.DataObjectLineLayout";
			DataStore[69].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[69].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[69].Seal();
			DataStore[70].FullName = @"Kistl.Client.GUI.DB.ListValueLayout";
			DataStore[70].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[70].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[70].Seal();
			DataStore[71].FullName = @"Kistl.Client.GUI.DB.TextValueSelectionLayout";
			DataStore[71].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[71].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[71].Seal();
			DataStore[72].FullName = @"Kistl.Client.GUI.DB.TextValueLayout";
			DataStore[72].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[72].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[72].Seal();
			DataStore[73].FullName = @"Kistl.Client.GUI.DB.SimpleEnumValueLayout";
			DataStore[73].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[73].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[73].Seal();
			DataStore[74].FullName = @"Kistl.Client.GUI.DB.SimpleNullableValueLayout`1";
			DataStore[74].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[74].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[74].Seal();
			DataStore[75].FullName = @"Kistl.Client.GUI.DB.WorkspaceLayout";
			DataStore[75].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[75].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[75].Seal();
			DataStore[76].FullName = @"Kistl.Client.GUI.DB.StaticLayout";
			DataStore[76].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[76].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[76].Seal();
			DataStore[77].FullName = @"Kistl.Client.GUI.DB.Layout";
			DataStore[77].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[77].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[77].Seal();
			DataStore[78].FullName = @"Kistl.Client.GUI.DB.ModelDescriptor";
			DataStore[78].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[78].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[78].Seal();
			DataStore[79].FullName = @"Kistl.Client.GUI.DB.ViewDescriptor";
			DataStore[79].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[79].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[79].Seal();
			DataStore[80].FullName = @"Kistl.Client.GUI.DB.TypeRef";
			DataStore[80].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[80].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[80].Seal();
			DataStore[81].FullName = @"Kistl.Client.Presentables.ModelFactory";
			DataStore[81].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[81].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[81].Seal();
			DataStore[82].FullName = @"Kistl.GUI.DB.VisualHelper";
			DataStore[82].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[82].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[82].Seal();
			DataStore[83].FullName = @"Kistl.Client.Presentables.DataObjectModel";
			DataStore[83].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[83].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[83].Seal();
			DataStore[84].FullName = @"Kistl.GUI.DB.TemplateUsage";
			DataStore[84].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[84].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[84].Seal();
			DataStore[85].FullName = @"Kistl.GUI.DB.TemplateHelper";
			DataStore[85].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[85].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[85].Seal();
			DataStore[86].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[86].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[86].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[86].Seal();
			DataStore[87].FullName = @"Kistl.Client.Presentables.ChooseReferencePropertyModel`1";
			DataStore[87].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[87].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[87].Seal();
			DataStore[88].FullName = @"Kistl.Client.Presentables.ReferencePropertyModel`1";
			DataStore[88].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[88].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[88].Seal();
			DataStore[89].FullName = @"Kistl.Client.Presentables.NullableValuePropertyModel`1";
			DataStore[89].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[89].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[89].Seal();
			DataStore[90].FullName = @"Kistl.Client.Presentables.PropertyModel`1";
			DataStore[90].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[90].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[90].Seal();
			DataStore[91].FullName = @"Kistl.Client.Presentables.IValueModel`1";
			DataStore[91].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[91].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[91].Seal();
			DataStore[92].FullName = @"Kistl.Client.Presentables.IClearableValue";
			DataStore[92].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[92].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[92].Seal();
			DataStore[93].FullName = @"Kistl.Client.Presentables.IReadOnlyValueModel`1";
			DataStore[93].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[93].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[93].Seal();
			DataStore[94].FullName = @"Kistl.Client.Presentables.KistlContextModel";
			DataStore[94].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[94].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[94].Seal();
			DataStore[95].FullName = @"Kistl.Client.Presentables.KistlDebuggerAsModel";
			DataStore[95].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[95].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[95].Seal();
			DataStore[96].FullName = @"Kistl.Client.Presentables.PresentableModel";
			DataStore[96].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[96].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[96].Seal();
			DataStore[97].FullName = @"Kistl.GUI.FieldSize";
			DataStore[97].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[97].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[97].Seal();
			DataStore[98].FullName = @"Kistl.GUI.IActionControl";
			DataStore[98].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[98].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[98].Seal();
			DataStore[99].FullName = @"Kistl.GUI.IWorkspaceControl";
			DataStore[99].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[99].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[99].Seal();
			DataStore[100].FullName = @"Kistl.GUI.IObjectControl";
			DataStore[100].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[100].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[100].Seal();
			DataStore[101].FullName = @"Kistl.GUI.IReferenceListControl";
			DataStore[101].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[101].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[101].Seal();
			DataStore[102].FullName = @"Kistl.GUI.IReferenceControl";
			DataStore[102].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[102].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[102].Seal();
			DataStore[103].FullName = @"Kistl.GUI.IListControl`1";
			DataStore[103].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[103].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[103].Seal();
			DataStore[104].FullName = @"Kistl.GUI.IEnumControl";
			DataStore[104].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[104].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[104].Seal();
			DataStore[105].FullName = @"Kistl.GUI.IValueControl`1";
			DataStore[105].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[105].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[105].Seal();
			DataStore[106].FullName = @"Kistl.GUI.IBasicControl";
			DataStore[106].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[106].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[106].Seal();
			DataStore[107].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[107].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[107].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[107].Seal();
			DataStore[108].FullName = @"Kistl.App.Base.Multiplicity";
			DataStore[108].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[13];
			DataStore[108].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[108].Seal();
			DataStore[109].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[109].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[109].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[109].Seal();
			DataStore[110].FullName = @"Kistl.App.Base.StorageType";
			DataStore[110].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[13];
			DataStore[110].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[110].Seal();
			DataStore[111].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[111].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[111].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[111].Seal();
			DataStore[112].FullName = @"Kistl.App.GUI.VisualType";
			DataStore[112].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[13];
			DataStore[112].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[112].Seal();
			DataStore[113].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[113].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[113].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[113].Seal();
			DataStore[114].FullName = @"Kistl.App.GUI.Toolkit";
			DataStore[114].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[13];
			DataStore[114].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[114].Seal();
			DataStore[115].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[115].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[115].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[115].Seal();
			DataStore[116].FullName = @"Kistl.App.Test.TestEnum";
			DataStore[116].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[13];
			DataStore[116].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[116].Seal();
			DataStore[117].FullName = @"Kistl.Client.Presentables.ReferencePropertyModel`1[[System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[117].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[117].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[117].Seal();
			DataStore[118].FullName = @"Kistl.Client.Presentables.SimpleReferenceListPropertyModel`1[[System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[118].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[118].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[118].Seal();
			DataStore[119].FullName = @"Kistl.Client.Presentables.ChooseReferencePropertyModel`1[[System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[119].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[119].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[119].Seal();
			DataStore[120].FullName = @"Kistl.Client.Presentables.NullableValuePropertyModel`1[[System.Int32, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[120].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[120].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[120].Seal();
			DataStore[121].FullName = @"Kistl.Client.Presentables.NullableValuePropertyModel`1[[System.Double, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[121].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[121].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[121].Seal();
			DataStore[122].FullName = @"Kistl.Client.Presentables.NullableValuePropertyModel`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[122].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[122].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[122].Seal();
			DataStore[123].FullName = @"Kistl.Client.Presentables.NullableValuePropertyModel`1[[System.Boolean, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[123].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[123].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[123].Seal();
			DataStore[124].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[124].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[124].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[124].Seal();
			DataStore[125].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[125].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[125].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[125].Seal();
			DataStore[126].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[126].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[126].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[126].Seal();
			DataStore[127].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[127].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[127].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[127].Seal();
			DataStore[128].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[128].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[128].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[128].Seal();
			DataStore[129].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[129].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[129].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[129].Seal();
			DataStore[130].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[130].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[130].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[130].Seal();
			DataStore[131].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[131].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[131].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[131].Seal();
			DataStore[132].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[132].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[132].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[132].Seal();
			DataStore[133].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[133].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[133].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[133].Seal();
			DataStore[134].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[134].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[134].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[134].Seal();
			DataStore[135].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[135].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[135].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[135].Seal();
			DataStore[136].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[136].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[136].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[136].Seal();
			DataStore[137].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[137].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[137].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[137].Seal();
			DataStore[138].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[138].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[138].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[138].Seal();
			DataStore[139].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[139].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[139].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[139].Seal();
			DataStore[140].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[140].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[140].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[140].Seal();
			DataStore[141].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[141].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[141].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[141].Seal();
			DataStore[142].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[142].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[142].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[142].Seal();
			DataStore[143].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[143].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[143].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[143].Seal();
			DataStore[144].FullName = @"Kistl.Client.GUI.DB.SimpleNullableValueLayout`1[[System.Boolean, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
			DataStore[144].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[144].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[144].Seal();
			DataStore[145].FullName = @"Kistl.Client.WPF.View.KistlDebuggerView";
			DataStore[145].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[145].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[145].Seal();
			DataStore[146].FullName = @"Kistl.Client.WPF.View.ListValueView";
			DataStore[146].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[146].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[146].Seal();
			DataStore[147].FullName = @"Kistl.Client.WPF.View.EnumSelectionView";
			DataStore[147].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[147].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[147].Seal();
			DataStore[148].FullName = @"Kistl.Client.WPF.View.TextValueSelectionView";
			DataStore[148].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[148].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[148].Seal();
			DataStore[149].FullName = @"Kistl.Client.WPF.View.ActionView";
			DataStore[149].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[149].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[149].Seal();
			DataStore[150].FullName = @"Kistl.Client.WPF.View.SelectionDialog";
			DataStore[150].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[150].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[150].Seal();
			DataStore[151].FullName = @"Kistl.Client.WPF.View.NullableBoolValueView";
			DataStore[151].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[151].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[151].Seal();
			DataStore[152].FullName = @"Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader";
			DataStore[152].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[16];
			DataStore[152].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[152].Seal();
			DataStore[153].FullName = @"Kistl.Client.Forms.View.NullablePropertyTextBoxView";
			DataStore[153].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[17];
			DataStore[153].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[153].Seal();
			DataStore[154].FullName = @"Kistl.Client.WPF.View.NullablePropertyTextBoxView";
			DataStore[154].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[154].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[154].Seal();
			DataStore[155].FullName = @"Kistl.Client.WPF.View.DataObjectView";
			DataStore[155].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[155].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[155].Seal();
			DataStore[156].FullName = @"Kistl.Client.ASPNET.Toolkit.View.DataObjectListViewLoader";
			DataStore[156].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[16];
			DataStore[156].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[156].Seal();
			DataStore[157].FullName = @"Kistl.Client.Forms.View.DataObjectListView";
			DataStore[157].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[17];
			DataStore[157].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[157].Seal();
			DataStore[158].FullName = @"Kistl.Client.WPF.View.DataObjectListView";
			DataStore[158].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[158].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[158].Seal();
			DataStore[159].FullName = @"Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceViewLoader";
			DataStore[159].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[16];
			DataStore[159].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[159].Seal();
			DataStore[160].FullName = @"Kistl.Client.Forms.View.DataObjectReferenceView";
			DataStore[160].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[17];
			DataStore[160].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[160].Seal();
			DataStore[161].FullName = @"Kistl.Client.WPF.View.ObjectReferenceView";
			DataStore[161].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[161].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[161].Seal();
			DataStore[162].FullName = @"Kistl.Client.ASPNET.Toolkit.View.DataObjectFullViewLoader";
			DataStore[162].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[16];
			DataStore[162].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[162].Seal();
			DataStore[163].FullName = @"Kistl.Client.Forms.View.DataObjectFullView";
			DataStore[163].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[17];
			DataStore[163].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[163].Seal();
			DataStore[164].FullName = @"Kistl.Client.WPF.View.DataObjectFullView";
			DataStore[164].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[164].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[164].Seal();
			DataStore[165].FullName = @"Kistl.Client.ASPNET.Toolkit.View.WorkspaceViewLoader";
			DataStore[165].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[16];
			DataStore[165].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[165].Seal();
			DataStore[166].FullName = @"Kistl.Client.Forms.View.WorkspaceView";
			DataStore[166].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[17];
			DataStore[166].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[166].Seal();
			DataStore[167].FullName = @"Kistl.Client.WPF.View.WorkspaceView";
			DataStore[167].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18];
			DataStore[167].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[167].Seal();
			DataStore[168].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[168].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[168].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[168].Seal();
			DataStore[169].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[169].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[169].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[169].Seal();
			DataStore[170].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[170].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[170].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[170].Seal();
			DataStore[171].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[171].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[171].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[171].Seal();
			DataStore[172].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[172].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[172].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[172].Seal();
			DataStore[173].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[173].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[173].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[173].Seal();
			DataStore[174].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[174].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[174].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[174].Seal();
			DataStore[175].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[175].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[175].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[175].Seal();
			DataStore[176].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[176].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[176].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[176].Seal();
			DataStore[177].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[177].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[177].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[177].Seal();
			DataStore[178].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[178].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[178].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[178].Seal();
			DataStore[179].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[179].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[179].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[179].Seal();
			DataStore[180].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[180].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[180].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[180].Seal();
			DataStore[181].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[181].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[181].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[181].Seal();
			DataStore[182].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[182].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[182].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[182].Seal();
			DataStore[183].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[183].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[183].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[183].Seal();
			DataStore[184].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[184].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[184].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[184].Seal();
			DataStore[185].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[185].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[185].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[185].Seal();
			DataStore[186].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[186].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[186].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[186].Seal();
			DataStore[187].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[187].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[187].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[187].Seal();
			DataStore[188].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[188].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[188].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[188].Seal();
			DataStore[189].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[189].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[189].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[189].Seal();
			DataStore[190].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[190].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[190].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[190].Seal();
			DataStore[191].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[191].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[191].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[191].Seal();
			DataStore[192].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[192].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[192].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[192].Seal();
			DataStore[193].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[193].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[193].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[193].Seal();
			DataStore[194].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[194].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[194].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[194].Seal();
			DataStore[195].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[195].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[195].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[195].Seal();
			DataStore[196].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[196].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[196].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[196].Seal();
			DataStore[197].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[197].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[197].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[197].Seal();
			DataStore[198].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[198].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[198].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[198].Seal();
			DataStore[199].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[199].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[199].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[199].Seal();
			DataStore[200].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[200].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[200].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[200].Seal();
			DataStore[201].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[201].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[201].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[201].Seal();
			DataStore[202].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[202].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[202].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[202].Seal();
			DataStore[203].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[203].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[203].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[203].Seal();
			DataStore[204].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[204].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[204].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[204].Seal();
			DataStore[205].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[205].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[205].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[205].Seal();
			DataStore[206].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[206].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[206].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[206].Seal();
			DataStore[207].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[207].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[207].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[207].Seal();
			DataStore[208].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[208].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[208].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[208].Seal();
			DataStore[209].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[209].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[209].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[209].Seal();
			DataStore[210].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[210].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[210].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[210].Seal();
			DataStore[211].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[211].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[211].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[211].Seal();
			DataStore[212].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[212].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[212].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[212].Seal();
			DataStore[213].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[213].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[213].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[213].Seal();
			DataStore[214].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[214].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[214].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[214].Seal();
			DataStore[215].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[215].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[215].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[215].Seal();
			DataStore[216].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[216].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[216].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[216].Seal();
			DataStore[217].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[217].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[217].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[217].Seal();
			DataStore[218].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[218].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[218].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[218].Seal();
			DataStore[219].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[219].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[219].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[219].Seal();
			DataStore[220].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[220].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[220].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[220].Seal();
			DataStore[221].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[221].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[221].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[221].Seal();
			DataStore[222].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[222].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[222].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[222].Seal();
			DataStore[223].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[223].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[223].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[223].Seal();
			DataStore[224].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[224].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[224].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[224].Seal();
			DataStore[225].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[225].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[225].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[225].Seal();
			DataStore[226].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[226].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[226].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[226].Seal();
			DataStore[227].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[227].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[227].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[227].Seal();
			DataStore[228].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[228].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[228].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[228].Seal();
			DataStore[229].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[229].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[229].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[229].Seal();
			DataStore[230].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[230].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[230].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[230].Seal();
			DataStore[231].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[231].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[231].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[231].Seal();
			DataStore[232].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[232].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[232].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[232].Seal();
			DataStore[233].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[233].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[233].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[233].Seal();
			DataStore[234].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[234].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[234].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[234].Seal();
			DataStore[235].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[235].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[235].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[235].Seal();
			DataStore[236].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[236].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[236].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[236].Seal();
			DataStore[237].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[237].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[237].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[237].Seal();
			DataStore[238].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[238].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[238].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[238].Seal();
			DataStore[239].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[239].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[239].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[239].Seal();
			DataStore[240].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[240].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[240].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[240].Seal();
			DataStore[241].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[241].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[241].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[241].Seal();
			DataStore[242].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[242].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[242].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[242].Seal();
			DataStore[243].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[243].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[243].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[243].Seal();
			DataStore[244].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[244].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[244].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[244].Seal();
			DataStore[245].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[245].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[245].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[245].Seal();
			DataStore[246].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[246].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[246].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[246].Seal();
			DataStore[247].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[247].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[247].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[247].Seal();
			DataStore[248].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[248].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[248].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[248].Seal();
			DataStore[249].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[249].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[249].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[249].Seal();
			DataStore[250].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[250].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[250].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[250].Seal();
			DataStore[251].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[251].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[251].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[251].Seal();
			DataStore[252].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[252].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[252].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[252].Seal();
			DataStore[253].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[253].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[253].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[253].Seal();
			DataStore[254].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[254].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[254].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[254].Seal();
			DataStore[255].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[255].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[255].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[255].Seal();
			DataStore[256].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[256].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[256].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[256].Seal();
			DataStore[257].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[257].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[257].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[257].Seal();
			DataStore[258].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[258].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[258].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[258].Seal();
			DataStore[259].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[259].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[259].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[259].Seal();
			DataStore[260].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[260].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[260].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[260].Seal();
			DataStore[261].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[261].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[261].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[261].Seal();
			DataStore[262].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[262].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[262].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[262].Seal();
			DataStore[263].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[263].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[263].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[263].Seal();
			DataStore[264].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[264].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[264].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[264].Seal();
			DataStore[265].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[265].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[265].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[265].Seal();
			DataStore[266].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[266].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[266].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[266].Seal();
			DataStore[267].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[267].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[267].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[267].Seal();
			DataStore[268].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[268].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[268].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[268].Seal();
			DataStore[269].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[269].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[269].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[269].Seal();
			DataStore[270].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[270].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[270].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[270].Seal();
			DataStore[271].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[271].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[271].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[271].Seal();
			DataStore[272].FullName = @"Kistl.Client.Presentables.EnumerationPropertyModel`1";
			DataStore[272].Assembly = Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14];
			DataStore[272].GenericArguments = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.TypeRef>(new List<Kistl.App.Base.TypeRef>(0) {
});
			DataStore[272].Seal();
	
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