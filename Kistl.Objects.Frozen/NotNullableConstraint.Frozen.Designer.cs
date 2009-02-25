
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
    [System.Diagnostics.DebuggerDisplay("NotNullableConstraint")]
    public class NotNullableConstraint__Implementation__Frozen : Kistl.App.Base.Constraint__Implementation__Frozen, NotNullableConstraint
    {


        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_NotNullableConstraint != null)
            {
                OnGetErrorText_NotNullableConstraint(this, e, constrainedValue, constrainedObject);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedValue, constrainedObject);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_NotNullableConstraint != null)
            {
                OnIsValid_NotNullableConstraint(this, e, constrainedValue, constrainedObj);
            }
            else
            {
                e.Result = base.IsValid(constrainedValue, constrainedObj);
            }
            return e.Result;
        }
		public event IsValid_Handler<NotNullableConstraint> OnIsValid_NotNullableConstraint;



		public override Type GetInterfaceType()
		{
			return typeof(NotNullableConstraint);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_NotNullableConstraint != null)
            {
                OnToString_NotNullableConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<NotNullableConstraint> OnToString_NotNullableConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_NotNullableConstraint != null) OnPreSave_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnPreSave_NotNullableConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_NotNullableConstraint != null) OnPostSave_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnPostSave_NotNullableConstraint;


        internal NotNullableConstraint__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, NotNullableConstraint__Implementation__Frozen> DataStore = new Dictionary<int, NotNullableConstraint__Implementation__Frozen>(79);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[74] = 
			DataStore[74] = new NotNullableConstraint__Implementation__Frozen(74);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[75] = 
			DataStore[75] = new NotNullableConstraint__Implementation__Frozen(75);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[76] = 
			DataStore[76] = new NotNullableConstraint__Implementation__Frozen(76);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[77] = 
			DataStore[77] = new NotNullableConstraint__Implementation__Frozen(77);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[78] = 
			DataStore[78] = new NotNullableConstraint__Implementation__Frozen(78);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[79] = 
			DataStore[79] = new NotNullableConstraint__Implementation__Frozen(79);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[80] = 
			DataStore[80] = new NotNullableConstraint__Implementation__Frozen(80);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[81] = 
			DataStore[81] = new NotNullableConstraint__Implementation__Frozen(81);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[82] = 
			DataStore[82] = new NotNullableConstraint__Implementation__Frozen(82);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[83] = 
			DataStore[83] = new NotNullableConstraint__Implementation__Frozen(83);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[84] = 
			DataStore[84] = new NotNullableConstraint__Implementation__Frozen(84);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[85] = 
			DataStore[85] = new NotNullableConstraint__Implementation__Frozen(85);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[86] = 
			DataStore[86] = new NotNullableConstraint__Implementation__Frozen(86);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[87] = 
			DataStore[87] = new NotNullableConstraint__Implementation__Frozen(87);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[88] = 
			DataStore[88] = new NotNullableConstraint__Implementation__Frozen(88);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[89] = 
			DataStore[89] = new NotNullableConstraint__Implementation__Frozen(89);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[90] = 
			DataStore[90] = new NotNullableConstraint__Implementation__Frozen(90);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[91] = 
			DataStore[91] = new NotNullableConstraint__Implementation__Frozen(91);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[92] = 
			DataStore[92] = new NotNullableConstraint__Implementation__Frozen(92);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[93] = 
			DataStore[93] = new NotNullableConstraint__Implementation__Frozen(93);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[94] = 
			DataStore[94] = new NotNullableConstraint__Implementation__Frozen(94);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[95] = 
			DataStore[95] = new NotNullableConstraint__Implementation__Frozen(95);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[96] = 
			DataStore[96] = new NotNullableConstraint__Implementation__Frozen(96);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[97] = 
			DataStore[97] = new NotNullableConstraint__Implementation__Frozen(97);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[98] = 
			DataStore[98] = new NotNullableConstraint__Implementation__Frozen(98);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[99] = 
			DataStore[99] = new NotNullableConstraint__Implementation__Frozen(99);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[100] = 
			DataStore[100] = new NotNullableConstraint__Implementation__Frozen(100);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[101] = 
			DataStore[101] = new NotNullableConstraint__Implementation__Frozen(101);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[102] = 
			DataStore[102] = new NotNullableConstraint__Implementation__Frozen(102);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[103] = 
			DataStore[103] = new NotNullableConstraint__Implementation__Frozen(103);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[104] = 
			DataStore[104] = new NotNullableConstraint__Implementation__Frozen(104);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[105] = 
			DataStore[105] = new NotNullableConstraint__Implementation__Frozen(105);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[106] = 
			DataStore[106] = new NotNullableConstraint__Implementation__Frozen(106);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[107] = 
			DataStore[107] = new NotNullableConstraint__Implementation__Frozen(107);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[108] = 
			DataStore[108] = new NotNullableConstraint__Implementation__Frozen(108);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[109] = 
			DataStore[109] = new NotNullableConstraint__Implementation__Frozen(109);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[110] = 
			DataStore[110] = new NotNullableConstraint__Implementation__Frozen(110);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[111] = 
			DataStore[111] = new NotNullableConstraint__Implementation__Frozen(111);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[113] = 
			DataStore[113] = new NotNullableConstraint__Implementation__Frozen(113);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[114] = 
			DataStore[114] = new NotNullableConstraint__Implementation__Frozen(114);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[115] = 
			DataStore[115] = new NotNullableConstraint__Implementation__Frozen(115);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[116] = 
			DataStore[116] = new NotNullableConstraint__Implementation__Frozen(116);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[117] = 
			DataStore[117] = new NotNullableConstraint__Implementation__Frozen(117);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[118] = 
			DataStore[118] = new NotNullableConstraint__Implementation__Frozen(118);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[119] = 
			DataStore[119] = new NotNullableConstraint__Implementation__Frozen(119);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[120] = 
			DataStore[120] = new NotNullableConstraint__Implementation__Frozen(120);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[123] = 
			DataStore[123] = new NotNullableConstraint__Implementation__Frozen(123);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[124] = 
			DataStore[124] = new NotNullableConstraint__Implementation__Frozen(124);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[125] = 
			DataStore[125] = new NotNullableConstraint__Implementation__Frozen(125);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[126] = 
			DataStore[126] = new NotNullableConstraint__Implementation__Frozen(126);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[127] = 
			DataStore[127] = new NotNullableConstraint__Implementation__Frozen(127);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[128] = 
			DataStore[128] = new NotNullableConstraint__Implementation__Frozen(128);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[129] = 
			DataStore[129] = new NotNullableConstraint__Implementation__Frozen(129);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[130] = 
			DataStore[130] = new NotNullableConstraint__Implementation__Frozen(130);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[131] = 
			DataStore[131] = new NotNullableConstraint__Implementation__Frozen(131);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[132] = 
			DataStore[132] = new NotNullableConstraint__Implementation__Frozen(132);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[133] = 
			DataStore[133] = new NotNullableConstraint__Implementation__Frozen(133);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[134] = 
			DataStore[134] = new NotNullableConstraint__Implementation__Frozen(134);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[135] = 
			DataStore[135] = new NotNullableConstraint__Implementation__Frozen(135);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[136] = 
			DataStore[136] = new NotNullableConstraint__Implementation__Frozen(136);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[137] = 
			DataStore[137] = new NotNullableConstraint__Implementation__Frozen(137);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[138] = 
			DataStore[138] = new NotNullableConstraint__Implementation__Frozen(138);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[139] = 
			DataStore[139] = new NotNullableConstraint__Implementation__Frozen(139);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[140] = 
			DataStore[140] = new NotNullableConstraint__Implementation__Frozen(140);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[141] = 
			DataStore[141] = new NotNullableConstraint__Implementation__Frozen(141);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[142] = 
			DataStore[142] = new NotNullableConstraint__Implementation__Frozen(142);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[143] = 
			DataStore[143] = new NotNullableConstraint__Implementation__Frozen(143);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[144] = 
			DataStore[144] = new NotNullableConstraint__Implementation__Frozen(144);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[145] = 
			DataStore[145] = new NotNullableConstraint__Implementation__Frozen(145);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[146] = 
			DataStore[146] = new NotNullableConstraint__Implementation__Frozen(146);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[150] = 
			DataStore[150] = new NotNullableConstraint__Implementation__Frozen(150);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[151] = 
			DataStore[151] = new NotNullableConstraint__Implementation__Frozen(151);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[202] = 
			DataStore[202] = new NotNullableConstraint__Implementation__Frozen(202);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[210] = 
			DataStore[210] = new NotNullableConstraint__Implementation__Frozen(210);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[211] = 
			DataStore[211] = new NotNullableConstraint__Implementation__Frozen(211);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[212] = 
			DataStore[212] = new NotNullableConstraint__Implementation__Frozen(212);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[213] = 
			DataStore[213] = new NotNullableConstraint__Implementation__Frozen(213);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[214] = 
			DataStore[214] = new NotNullableConstraint__Implementation__Frozen(214);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[215] = 
			DataStore[215] = new NotNullableConstraint__Implementation__Frozen(215);

		}

		internal new static void FillDataStore() {
			DataStore[74].Reason = null;
			DataStore[74].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[171];
			DataStore[74].Seal();
			DataStore[75].Reason = null;
			DataStore[75].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[169];
			DataStore[75].Seal();
			DataStore[76].Reason = null;
			DataStore[76].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[168];
			DataStore[76].Seal();
			DataStore[77].Reason = null;
			DataStore[77].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[163];
			DataStore[77].Seal();
			DataStore[78].Reason = null;
			DataStore[78].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[162];
			DataStore[78].Seal();
			DataStore[79].Reason = null;
			DataStore[79].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[155];
			DataStore[79].Seal();
			DataStore[80].Reason = null;
			DataStore[80].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[154];
			DataStore[80].Seal();
			DataStore[81].Reason = null;
			DataStore[81].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[153];
			DataStore[81].Seal();
			DataStore[82].Reason = null;
			DataStore[82].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[150];
			DataStore[82].Seal();
			DataStore[83].Reason = null;
			DataStore[83].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[149];
			DataStore[83].Seal();
			DataStore[84].Reason = null;
			DataStore[84].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[148];
			DataStore[84].Seal();
			DataStore[85].Reason = null;
			DataStore[85].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[139];
			DataStore[85].Seal();
			DataStore[86].Reason = null;
			DataStore[86].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[138];
			DataStore[86].Seal();
			DataStore[87].Reason = null;
			DataStore[87].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[137];
			DataStore[87].Seal();
			DataStore[88].Reason = null;
			DataStore[88].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[136];
			DataStore[88].Seal();
			DataStore[89].Reason = null;
			DataStore[89].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[135];
			DataStore[89].Seal();
			DataStore[90].Reason = null;
			DataStore[90].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[130];
			DataStore[90].Seal();
			DataStore[91].Reason = null;
			DataStore[91].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[129];
			DataStore[91].Seal();
			DataStore[92].Reason = null;
			DataStore[92].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[124];
			DataStore[92].Seal();
			DataStore[93].Reason = null;
			DataStore[93].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[119];
			DataStore[93].Seal();
			DataStore[94].Reason = null;
			DataStore[94].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[118];
			DataStore[94].Seal();
			DataStore[95].Reason = null;
			DataStore[95].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[117];
			DataStore[95].Seal();
			DataStore[96].Reason = null;
			DataStore[96].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[116];
			DataStore[96].Seal();
			DataStore[97].Reason = null;
			DataStore[97].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[115];
			DataStore[97].Seal();
			DataStore[98].Reason = null;
			DataStore[98].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[114];
			DataStore[98].Seal();
			DataStore[99].Reason = null;
			DataStore[99].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[113];
			DataStore[99].Seal();
			DataStore[100].Reason = null;
			DataStore[100].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[112];
			DataStore[100].Seal();
			DataStore[101].Reason = null;
			DataStore[101].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[111];
			DataStore[101].Seal();
			DataStore[102].Reason = null;
			DataStore[102].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[110];
			DataStore[102].Seal();
			DataStore[103].Reason = null;
			DataStore[103].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[109];
			DataStore[103].Seal();
			DataStore[104].Reason = null;
			DataStore[104].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[108];
			DataStore[104].Seal();
			DataStore[105].Reason = null;
			DataStore[105].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[107];
			DataStore[105].Seal();
			DataStore[106].Reason = null;
			DataStore[106].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[104];
			DataStore[106].Seal();
			DataStore[107].Reason = null;
			DataStore[107].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[100];
			DataStore[107].Seal();
			DataStore[108].Reason = null;
			DataStore[108].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[99];
			DataStore[108].Seal();
			DataStore[109].Reason = null;
			DataStore[109].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[97];
			DataStore[109].Seal();
			DataStore[110].Reason = null;
			DataStore[110].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[95];
			DataStore[110].Seal();
			DataStore[111].Reason = null;
			DataStore[111].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[94];
			DataStore[111].Seal();
			DataStore[113].Reason = null;
			DataStore[113].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[92];
			DataStore[113].Seal();
			DataStore[114].Reason = null;
			DataStore[114].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[91];
			DataStore[114].Seal();
			DataStore[115].Reason = null;
			DataStore[115].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[87];
			DataStore[115].Seal();
			DataStore[116].Reason = null;
			DataStore[116].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[84];
			DataStore[116].Seal();
			DataStore[117].Reason = null;
			DataStore[117].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[83];
			DataStore[117].Seal();
			DataStore[118].Reason = null;
			DataStore[118].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[79];
			DataStore[118].Seal();
			DataStore[119].Reason = null;
			DataStore[119].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[78];
			DataStore[119].Seal();
			DataStore[120].Reason = null;
			DataStore[120].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[77];
			DataStore[120].Seal();
			DataStore[123].Reason = null;
			DataStore[123].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[74];
			DataStore[123].Seal();
			DataStore[124].Reason = null;
			DataStore[124].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[73];
			DataStore[124].Seal();
			DataStore[125].Reason = null;
			DataStore[125].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[72];
			DataStore[125].Seal();
			DataStore[126].Reason = null;
			DataStore[126].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[71];
			DataStore[126].Seal();
			DataStore[127].Reason = null;
			DataStore[127].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[70];
			DataStore[127].Seal();
			DataStore[128].Reason = null;
			DataStore[128].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[68];
			DataStore[128].Seal();
			DataStore[129].Reason = null;
			DataStore[129].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[61];
			DataStore[129].Seal();
			DataStore[130].Reason = null;
			DataStore[130].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[59];
			DataStore[130].Seal();
			DataStore[131].Reason = null;
			DataStore[131].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[57];
			DataStore[131].Seal();
			DataStore[132].Reason = null;
			DataStore[132].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[56];
			DataStore[132].Seal();
			DataStore[133].Reason = null;
			DataStore[133].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[55];
			DataStore[133].Seal();
			DataStore[134].Reason = null;
			DataStore[134].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[54];
			DataStore[134].Seal();
			DataStore[135].Reason = null;
			DataStore[135].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[53];
			DataStore[135].Seal();
			DataStore[136].Reason = null;
			DataStore[136].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[52];
			DataStore[136].Seal();
			DataStore[137].Reason = null;
			DataStore[137].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[45];
			DataStore[137].Seal();
			DataStore[138].Reason = null;
			DataStore[138].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[43];
			DataStore[138].Seal();
			DataStore[139].Reason = null;
			DataStore[139].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[42];
			DataStore[139].Seal();
			DataStore[140].Reason = null;
			DataStore[140].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[30];
			DataStore[140].Seal();
			DataStore[141].Reason = null;
			DataStore[141].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[29];
			DataStore[141].Seal();
			DataStore[142].Reason = null;
			DataStore[142].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[26];
			DataStore[142].Seal();
			DataStore[143].Reason = null;
			DataStore[143].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[11];
			DataStore[143].Seal();
			DataStore[144].Reason = null;
			DataStore[144].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[8];
			DataStore[144].Seal();
			DataStore[145].Reason = null;
			DataStore[145].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[3];
			DataStore[145].Seal();
			DataStore[146].Reason = null;
			DataStore[146].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[1];
			DataStore[146].Seal();
			DataStore[150].Reason = null;
			DataStore[150].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[173];
			DataStore[150].Seal();
			DataStore[151].Reason = null;
			DataStore[151].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[172];
			DataStore[151].Seal();
			DataStore[202].Reason = null;
			DataStore[202].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[174];
			DataStore[202].Seal();
			DataStore[210].Reason = null;
			DataStore[210].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[182];
			DataStore[210].Seal();
			DataStore[211].Reason = null;
			DataStore[211].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[181];
			DataStore[211].Seal();
			DataStore[212].Reason = @"Cannot create legal O/R mapping without length";
			DataStore[212].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[28];
			DataStore[212].Seal();
			DataStore[213].Reason = null;
			DataStore[213].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[209];
			DataStore[213].Seal();
			DataStore[214].Reason = null;
			DataStore[214].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[210];
			DataStore[214].Seal();
			DataStore[215].Reason = @"A ViewDescriptor must reference the described View.";
			DataStore[215].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[211];
			DataStore[215].Seal();
	
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