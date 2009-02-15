
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

		public override bool IsValid(System.Object constrainedObj, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_NotNullableConstraint != null)
            {
                OnIsValid_NotNullableConstraint(this, e, constrainedObj, constrainedValue);
            };
            return e.Result;
        }
		public event IsValid_Handler<NotNullableConstraint> OnIsValid_NotNullableConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_NotNullableConstraint != null)
            {
                OnGetErrorText_NotNullableConstraint(this, e, constrainedObject, constrainedValue);
            };
            return e.Result;
        }
		public event GetErrorText_Handler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint;



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


        internal NotNullableConstraint__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, NotNullableConstraint__Implementation__Frozen> DataStore = new Dictionary<int, NotNullableConstraint__Implementation__Frozen>(79);
		static NotNullableConstraint__Implementation__Frozen()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[74] = 
			DataStore[74] = new NotNullableConstraint__Implementation__Frozen(null, 74);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[75] = 
			DataStore[75] = new NotNullableConstraint__Implementation__Frozen(null, 75);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[76] = 
			DataStore[76] = new NotNullableConstraint__Implementation__Frozen(null, 76);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[77] = 
			DataStore[77] = new NotNullableConstraint__Implementation__Frozen(null, 77);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[78] = 
			DataStore[78] = new NotNullableConstraint__Implementation__Frozen(null, 78);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[79] = 
			DataStore[79] = new NotNullableConstraint__Implementation__Frozen(null, 79);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[80] = 
			DataStore[80] = new NotNullableConstraint__Implementation__Frozen(null, 80);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[81] = 
			DataStore[81] = new NotNullableConstraint__Implementation__Frozen(null, 81);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[82] = 
			DataStore[82] = new NotNullableConstraint__Implementation__Frozen(null, 82);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[83] = 
			DataStore[83] = new NotNullableConstraint__Implementation__Frozen(null, 83);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[84] = 
			DataStore[84] = new NotNullableConstraint__Implementation__Frozen(null, 84);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[85] = 
			DataStore[85] = new NotNullableConstraint__Implementation__Frozen(null, 85);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[86] = 
			DataStore[86] = new NotNullableConstraint__Implementation__Frozen(null, 86);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[87] = 
			DataStore[87] = new NotNullableConstraint__Implementation__Frozen(null, 87);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[88] = 
			DataStore[88] = new NotNullableConstraint__Implementation__Frozen(null, 88);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[89] = 
			DataStore[89] = new NotNullableConstraint__Implementation__Frozen(null, 89);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[90] = 
			DataStore[90] = new NotNullableConstraint__Implementation__Frozen(null, 90);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[91] = 
			DataStore[91] = new NotNullableConstraint__Implementation__Frozen(null, 91);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[92] = 
			DataStore[92] = new NotNullableConstraint__Implementation__Frozen(null, 92);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[93] = 
			DataStore[93] = new NotNullableConstraint__Implementation__Frozen(null, 93);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[94] = 
			DataStore[94] = new NotNullableConstraint__Implementation__Frozen(null, 94);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[95] = 
			DataStore[95] = new NotNullableConstraint__Implementation__Frozen(null, 95);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[96] = 
			DataStore[96] = new NotNullableConstraint__Implementation__Frozen(null, 96);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[97] = 
			DataStore[97] = new NotNullableConstraint__Implementation__Frozen(null, 97);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[98] = 
			DataStore[98] = new NotNullableConstraint__Implementation__Frozen(null, 98);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[99] = 
			DataStore[99] = new NotNullableConstraint__Implementation__Frozen(null, 99);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[100] = 
			DataStore[100] = new NotNullableConstraint__Implementation__Frozen(null, 100);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[101] = 
			DataStore[101] = new NotNullableConstraint__Implementation__Frozen(null, 101);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[102] = 
			DataStore[102] = new NotNullableConstraint__Implementation__Frozen(null, 102);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[103] = 
			DataStore[103] = new NotNullableConstraint__Implementation__Frozen(null, 103);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[104] = 
			DataStore[104] = new NotNullableConstraint__Implementation__Frozen(null, 104);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[105] = 
			DataStore[105] = new NotNullableConstraint__Implementation__Frozen(null, 105);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[106] = 
			DataStore[106] = new NotNullableConstraint__Implementation__Frozen(null, 106);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[107] = 
			DataStore[107] = new NotNullableConstraint__Implementation__Frozen(null, 107);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[108] = 
			DataStore[108] = new NotNullableConstraint__Implementation__Frozen(null, 108);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[109] = 
			DataStore[109] = new NotNullableConstraint__Implementation__Frozen(null, 109);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[110] = 
			DataStore[110] = new NotNullableConstraint__Implementation__Frozen(null, 110);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[111] = 
			DataStore[111] = new NotNullableConstraint__Implementation__Frozen(null, 111);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[113] = 
			DataStore[113] = new NotNullableConstraint__Implementation__Frozen(null, 113);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[114] = 
			DataStore[114] = new NotNullableConstraint__Implementation__Frozen(null, 114);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[115] = 
			DataStore[115] = new NotNullableConstraint__Implementation__Frozen(null, 115);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[116] = 
			DataStore[116] = new NotNullableConstraint__Implementation__Frozen(null, 116);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[117] = 
			DataStore[117] = new NotNullableConstraint__Implementation__Frozen(null, 117);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[118] = 
			DataStore[118] = new NotNullableConstraint__Implementation__Frozen(null, 118);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[119] = 
			DataStore[119] = new NotNullableConstraint__Implementation__Frozen(null, 119);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[120] = 
			DataStore[120] = new NotNullableConstraint__Implementation__Frozen(null, 120);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[123] = 
			DataStore[123] = new NotNullableConstraint__Implementation__Frozen(null, 123);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[124] = 
			DataStore[124] = new NotNullableConstraint__Implementation__Frozen(null, 124);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[125] = 
			DataStore[125] = new NotNullableConstraint__Implementation__Frozen(null, 125);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[126] = 
			DataStore[126] = new NotNullableConstraint__Implementation__Frozen(null, 126);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[127] = 
			DataStore[127] = new NotNullableConstraint__Implementation__Frozen(null, 127);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[128] = 
			DataStore[128] = new NotNullableConstraint__Implementation__Frozen(null, 128);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[129] = 
			DataStore[129] = new NotNullableConstraint__Implementation__Frozen(null, 129);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[130] = 
			DataStore[130] = new NotNullableConstraint__Implementation__Frozen(null, 130);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[131] = 
			DataStore[131] = new NotNullableConstraint__Implementation__Frozen(null, 131);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[132] = 
			DataStore[132] = new NotNullableConstraint__Implementation__Frozen(null, 132);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[133] = 
			DataStore[133] = new NotNullableConstraint__Implementation__Frozen(null, 133);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[134] = 
			DataStore[134] = new NotNullableConstraint__Implementation__Frozen(null, 134);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[135] = 
			DataStore[135] = new NotNullableConstraint__Implementation__Frozen(null, 135);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[136] = 
			DataStore[136] = new NotNullableConstraint__Implementation__Frozen(null, 136);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[137] = 
			DataStore[137] = new NotNullableConstraint__Implementation__Frozen(null, 137);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[138] = 
			DataStore[138] = new NotNullableConstraint__Implementation__Frozen(null, 138);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[139] = 
			DataStore[139] = new NotNullableConstraint__Implementation__Frozen(null, 139);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[140] = 
			DataStore[140] = new NotNullableConstraint__Implementation__Frozen(null, 140);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[141] = 
			DataStore[141] = new NotNullableConstraint__Implementation__Frozen(null, 141);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[142] = 
			DataStore[142] = new NotNullableConstraint__Implementation__Frozen(null, 142);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[143] = 
			DataStore[143] = new NotNullableConstraint__Implementation__Frozen(null, 143);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[144] = 
			DataStore[144] = new NotNullableConstraint__Implementation__Frozen(null, 144);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[145] = 
			DataStore[145] = new NotNullableConstraint__Implementation__Frozen(null, 145);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[146] = 
			DataStore[146] = new NotNullableConstraint__Implementation__Frozen(null, 146);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[150] = 
			DataStore[150] = new NotNullableConstraint__Implementation__Frozen(null, 150);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[151] = 
			DataStore[151] = new NotNullableConstraint__Implementation__Frozen(null, 151);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[202] = 
			DataStore[202] = new NotNullableConstraint__Implementation__Frozen(null, 202);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[210] = 
			DataStore[210] = new NotNullableConstraint__Implementation__Frozen(null, 210);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[211] = 
			DataStore[211] = new NotNullableConstraint__Implementation__Frozen(null, 211);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[212] = 
			DataStore[212] = new NotNullableConstraint__Implementation__Frozen(null, 212);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[213] = 
			DataStore[213] = new NotNullableConstraint__Implementation__Frozen(null, 213);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[214] = 
			DataStore[214] = new NotNullableConstraint__Implementation__Frozen(null, 214);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[215] = 
			DataStore[215] = new NotNullableConstraint__Implementation__Frozen(null, 215);

		}

		internal new static void FillDataStore() {
	
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