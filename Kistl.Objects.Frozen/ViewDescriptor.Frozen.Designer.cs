
namespace Kistl.App.GUI
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
    [System.Diagnostics.DebuggerDisplay("ViewDescriptor")]
    public class ViewDescriptor__Implementation__Frozen : BaseFrozenDataObject, ViewDescriptor
    {
    
		public ViewDescriptor__Implementation__Frozen()
		{
        }


        /// <summary>
        /// The control implementing this View
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef ControlRef
        {
            get
            {
                return _ControlRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ControlRef != value)
                {
					var __oldValue = _ControlRef;
                    NotifyPropertyChanging("ControlRef", __oldValue, value);
                    _ControlRef = value;
                    NotifyPropertyChanged("ControlRef", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _ControlRef;

        /// <summary>
        /// The PresentableModel usable by this View
        /// </summary>
        // object reference property
        public virtual Kistl.App.GUI.PresentableModelDescriptor PresentedModelDescriptor
        {
            get
            {
                return _PresentedModelDescriptor;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresentedModelDescriptor != value)
                {
					var __oldValue = _PresentedModelDescriptor;
                    NotifyPropertyChanging("PresentedModelDescriptor", __oldValue, value);
                    _PresentedModelDescriptor = value;
                    NotifyPropertyChanged("PresentedModelDescriptor", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.PresentableModelDescriptor _PresentedModelDescriptor;

        /// <summary>
        /// Which toolkit provides this View
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.Toolkit Toolkit
        {
            get
            {
                return _Toolkit;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Toolkit != value)
                {
					var __oldValue = _Toolkit;
                    NotifyPropertyChanging("Toolkit", __oldValue, value);
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Toolkit;

        /// <summary>
        /// The visual type of this View
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType VisualType
        {
            get
            {
                return _VisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_VisualType != value)
                {
					var __oldValue = _VisualType;
                    NotifyPropertyChanging("VisualType", __oldValue, value);
                    _VisualType = value;
                    NotifyPropertyChanged("VisualType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _VisualType;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ViewDescriptor));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ViewDescriptor != null)
            {
                OnToString_ViewDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ViewDescriptor> OnToString_ViewDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ViewDescriptor != null) OnPreSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPreSave_ViewDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ViewDescriptor != null) OnPostSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPostSave_ViewDescriptor;


        internal ViewDescriptor__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, ViewDescriptor__Implementation__Frozen> DataStore = new Dictionary<int, ViewDescriptor__Implementation__Frozen>(64);
		internal static void CreateInstances()
		{
			DataStore[1] = new ViewDescriptor__Implementation__Frozen(1);

			DataStore[2] = new ViewDescriptor__Implementation__Frozen(2);

			DataStore[3] = new ViewDescriptor__Implementation__Frozen(3);

			DataStore[4] = new ViewDescriptor__Implementation__Frozen(4);

			DataStore[5] = new ViewDescriptor__Implementation__Frozen(5);

			DataStore[6] = new ViewDescriptor__Implementation__Frozen(6);

			DataStore[7] = new ViewDescriptor__Implementation__Frozen(7);

			DataStore[8] = new ViewDescriptor__Implementation__Frozen(8);

			DataStore[9] = new ViewDescriptor__Implementation__Frozen(9);

			DataStore[10] = new ViewDescriptor__Implementation__Frozen(10);

			DataStore[11] = new ViewDescriptor__Implementation__Frozen(11);

			DataStore[12] = new ViewDescriptor__Implementation__Frozen(12);

			DataStore[13] = new ViewDescriptor__Implementation__Frozen(13);

			DataStore[14] = new ViewDescriptor__Implementation__Frozen(14);

			DataStore[15] = new ViewDescriptor__Implementation__Frozen(15);

			DataStore[16] = new ViewDescriptor__Implementation__Frozen(16);

			DataStore[17] = new ViewDescriptor__Implementation__Frozen(17);

			DataStore[18] = new ViewDescriptor__Implementation__Frozen(18);

			DataStore[19] = new ViewDescriptor__Implementation__Frozen(19);

			DataStore[20] = new ViewDescriptor__Implementation__Frozen(20);

			DataStore[21] = new ViewDescriptor__Implementation__Frozen(21);

			DataStore[22] = new ViewDescriptor__Implementation__Frozen(22);

			DataStore[23] = new ViewDescriptor__Implementation__Frozen(23);

			DataStore[24] = new ViewDescriptor__Implementation__Frozen(24);

			DataStore[25] = new ViewDescriptor__Implementation__Frozen(25);

			DataStore[26] = new ViewDescriptor__Implementation__Frozen(26);

			DataStore[27] = new ViewDescriptor__Implementation__Frozen(27);

			DataStore[28] = new ViewDescriptor__Implementation__Frozen(28);

			DataStore[29] = new ViewDescriptor__Implementation__Frozen(29);

			DataStore[30] = new ViewDescriptor__Implementation__Frozen(30);

			DataStore[31] = new ViewDescriptor__Implementation__Frozen(31);

			DataStore[32] = new ViewDescriptor__Implementation__Frozen(32);

			DataStore[33] = new ViewDescriptor__Implementation__Frozen(33);

			DataStore[34] = new ViewDescriptor__Implementation__Frozen(34);

			DataStore[35] = new ViewDescriptor__Implementation__Frozen(35);

			DataStore[36] = new ViewDescriptor__Implementation__Frozen(36);

			DataStore[37] = new ViewDescriptor__Implementation__Frozen(37);

			DataStore[38] = new ViewDescriptor__Implementation__Frozen(38);

			DataStore[39] = new ViewDescriptor__Implementation__Frozen(39);

			DataStore[40] = new ViewDescriptor__Implementation__Frozen(40);

			DataStore[41] = new ViewDescriptor__Implementation__Frozen(41);

			DataStore[42] = new ViewDescriptor__Implementation__Frozen(42);

			DataStore[43] = new ViewDescriptor__Implementation__Frozen(43);

			DataStore[44] = new ViewDescriptor__Implementation__Frozen(44);

			DataStore[45] = new ViewDescriptor__Implementation__Frozen(45);

			DataStore[46] = new ViewDescriptor__Implementation__Frozen(46);

			DataStore[47] = new ViewDescriptor__Implementation__Frozen(47);

			DataStore[48] = new ViewDescriptor__Implementation__Frozen(48);

			DataStore[49] = new ViewDescriptor__Implementation__Frozen(49);

			DataStore[50] = new ViewDescriptor__Implementation__Frozen(50);

			DataStore[51] = new ViewDescriptor__Implementation__Frozen(51);

			DataStore[52] = new ViewDescriptor__Implementation__Frozen(52);

			DataStore[53] = new ViewDescriptor__Implementation__Frozen(53);

			DataStore[54] = new ViewDescriptor__Implementation__Frozen(54);

			DataStore[55] = new ViewDescriptor__Implementation__Frozen(55);

			DataStore[56] = new ViewDescriptor__Implementation__Frozen(56);

			DataStore[57] = new ViewDescriptor__Implementation__Frozen(57);

			DataStore[58] = new ViewDescriptor__Implementation__Frozen(58);

			DataStore[59] = new ViewDescriptor__Implementation__Frozen(59);

			DataStore[60] = new ViewDescriptor__Implementation__Frozen(60);

			DataStore[61] = new ViewDescriptor__Implementation__Frozen(61);

			DataStore[62] = new ViewDescriptor__Implementation__Frozen(62);

			DataStore[63] = new ViewDescriptor__Implementation__Frozen(63);

			DataStore[64] = new ViewDescriptor__Implementation__Frozen(64);

		}

		internal static void FillDataStore() {
			DataStore[1].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[145];
			DataStore[1].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[1];
			DataStore[1].Toolkit = (Toolkit)0;
			DataStore[1].VisualType = (VisualType)22;
			DataStore[1].Seal();
			DataStore[2].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[165];
			DataStore[2].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[2];
			DataStore[2].Toolkit = (Toolkit)1;
			DataStore[2].VisualType = (VisualType)24;
			DataStore[2].Seal();
			DataStore[3].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[166];
			DataStore[3].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[2];
			DataStore[3].Toolkit = (Toolkit)3;
			DataStore[3].VisualType = (VisualType)24;
			DataStore[3].Seal();
			DataStore[4].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[167];
			DataStore[4].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[2];
			DataStore[4].Toolkit = (Toolkit)0;
			DataStore[4].VisualType = (VisualType)24;
			DataStore[4].Seal();
			DataStore[5].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[150];
			DataStore[5].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[3];
			DataStore[5].Toolkit = (Toolkit)0;
			DataStore[5].VisualType = (VisualType)23;
			DataStore[5].Seal();
			DataStore[6].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[149];
			DataStore[6].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[4];
			DataStore[6].Toolkit = (Toolkit)0;
			DataStore[6].VisualType = (VisualType)17;
			DataStore[6].Seal();
			DataStore[7].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[281];
			DataStore[7].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[5];
			DataStore[7].Toolkit = (Toolkit)1;
			DataStore[7].VisualType = (VisualType)4;
			DataStore[7].Seal();
			DataStore[8].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[282];
			DataStore[8].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[5];
			DataStore[8].Toolkit = (Toolkit)3;
			DataStore[8].VisualType = (VisualType)4;
			DataStore[8].Seal();
			DataStore[9].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[161];
			DataStore[9].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[5];
			DataStore[9].Toolkit = (Toolkit)0;
			DataStore[9].VisualType = (VisualType)4;
			DataStore[9].Seal();
			DataStore[10].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[156];
			DataStore[10].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[6];
			DataStore[10].Toolkit = (Toolkit)1;
			DataStore[10].VisualType = (VisualType)3;
			DataStore[10].Seal();
			DataStore[11].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[157];
			DataStore[11].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[6];
			DataStore[11].Toolkit = (Toolkit)3;
			DataStore[11].VisualType = (VisualType)3;
			DataStore[11].Seal();
			DataStore[12].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[158];
			DataStore[12].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[6];
			DataStore[12].Toolkit = (Toolkit)0;
			DataStore[12].VisualType = (VisualType)3;
			DataStore[12].Seal();
			DataStore[13].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[162];
			DataStore[13].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[13].Toolkit = (Toolkit)1;
			DataStore[13].VisualType = (VisualType)1;
			DataStore[13].Seal();
			DataStore[14].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[163];
			DataStore[14].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[14].Toolkit = (Toolkit)3;
			DataStore[14].VisualType = (VisualType)1;
			DataStore[14].Seal();
			DataStore[15].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[155];
			DataStore[15].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[15].Toolkit = (Toolkit)0;
			DataStore[15].VisualType = (VisualType)21;
			DataStore[15].Seal();
			DataStore[16].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[164];
			DataStore[16].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[16].Toolkit = (Toolkit)0;
			DataStore[16].VisualType = (VisualType)1;
			DataStore[16].Seal();
			DataStore[17].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[148];
			DataStore[17].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[8];
			DataStore[17].Toolkit = (Toolkit)0;
			DataStore[17].VisualType = (VisualType)26;
			DataStore[17].Seal();
			DataStore[18].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[146];
			DataStore[18].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[9];
			DataStore[18].Toolkit = (Toolkit)0;
			DataStore[18].VisualType = (VisualType)14;
			DataStore[18].Seal();
			DataStore[19].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[19].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[9];
			DataStore[19].Toolkit = (Toolkit)1;
			DataStore[19].VisualType = (VisualType)13;
			DataStore[19].Seal();
			DataStore[20].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[20].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[9];
			DataStore[20].Toolkit = (Toolkit)3;
			DataStore[20].VisualType = (VisualType)13;
			DataStore[20].Seal();
			DataStore[21].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[21].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[9];
			DataStore[21].Toolkit = (Toolkit)0;
			DataStore[21].VisualType = (VisualType)13;
			DataStore[21].Seal();
			DataStore[22].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[22].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[10];
			DataStore[22].Toolkit = (Toolkit)1;
			DataStore[22].VisualType = (VisualType)13;
			DataStore[22].Seal();
			DataStore[23].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[23].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[10];
			DataStore[23].Toolkit = (Toolkit)3;
			DataStore[23].VisualType = (VisualType)13;
			DataStore[23].Seal();
			DataStore[24].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[24].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[10];
			DataStore[24].Toolkit = (Toolkit)0;
			DataStore[24].VisualType = (VisualType)13;
			DataStore[24].Seal();
			DataStore[25].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[25].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[11];
			DataStore[25].Toolkit = (Toolkit)1;
			DataStore[25].VisualType = (VisualType)13;
			DataStore[25].Seal();
			DataStore[26].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[26].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[11];
			DataStore[26].Toolkit = (Toolkit)3;
			DataStore[26].VisualType = (VisualType)13;
			DataStore[26].Seal();
			DataStore[27].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[27].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[11];
			DataStore[27].Toolkit = (Toolkit)0;
			DataStore[27].VisualType = (VisualType)13;
			DataStore[27].Seal();
			DataStore[28].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[28].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[12];
			DataStore[28].Toolkit = (Toolkit)1;
			DataStore[28].VisualType = (VisualType)13;
			DataStore[28].Seal();
			DataStore[29].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[29].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[12];
			DataStore[29].Toolkit = (Toolkit)3;
			DataStore[29].VisualType = (VisualType)13;
			DataStore[29].Seal();
			DataStore[30].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[30].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[12];
			DataStore[30].Toolkit = (Toolkit)0;
			DataStore[30].VisualType = (VisualType)13;
			DataStore[30].Seal();
			DataStore[31].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[31].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[13];
			DataStore[31].Toolkit = (Toolkit)1;
			DataStore[31].VisualType = (VisualType)13;
			DataStore[31].Seal();
			DataStore[32].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[32].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[13];
			DataStore[32].Toolkit = (Toolkit)3;
			DataStore[32].VisualType = (VisualType)13;
			DataStore[32].Seal();
			DataStore[33].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[151];
			DataStore[33].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[13];
			DataStore[33].Toolkit = (Toolkit)0;
			DataStore[33].VisualType = (VisualType)5;
			DataStore[33].Seal();
			DataStore[34].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[278];
			DataStore[34].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[15];
			DataStore[34].Toolkit = (Toolkit)0;
			DataStore[34].VisualType = (VisualType)2;
			DataStore[34].Seal();
			DataStore[35].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[35].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[16];
			DataStore[35].Toolkit = (Toolkit)1;
			DataStore[35].VisualType = (VisualType)13;
			DataStore[35].Seal();
			DataStore[36].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[36].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[16];
			DataStore[36].Toolkit = (Toolkit)3;
			DataStore[36].VisualType = (VisualType)13;
			DataStore[36].Seal();
			DataStore[37].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[37].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[16];
			DataStore[37].Toolkit = (Toolkit)0;
			DataStore[37].VisualType = (VisualType)13;
			DataStore[37].Seal();
			DataStore[38].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[38].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[17];
			DataStore[38].Toolkit = (Toolkit)1;
			DataStore[38].VisualType = (VisualType)13;
			DataStore[38].Seal();
			DataStore[39].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[39].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[17];
			DataStore[39].Toolkit = (Toolkit)3;
			DataStore[39].VisualType = (VisualType)13;
			DataStore[39].Seal();
			DataStore[40].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[40].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[17];
			DataStore[40].Toolkit = (Toolkit)0;
			DataStore[40].VisualType = (VisualType)13;
			DataStore[40].Seal();
			DataStore[41].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[41].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[18];
			DataStore[41].Toolkit = (Toolkit)1;
			DataStore[41].VisualType = (VisualType)13;
			DataStore[41].Seal();
			DataStore[42].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[42].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[18];
			DataStore[42].Toolkit = (Toolkit)3;
			DataStore[42].VisualType = (VisualType)13;
			DataStore[42].Seal();
			DataStore[43].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[43].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[18];
			DataStore[43].Toolkit = (Toolkit)0;
			DataStore[43].VisualType = (VisualType)13;
			DataStore[43].Seal();
			DataStore[44].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[44].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[19];
			DataStore[44].Toolkit = (Toolkit)1;
			DataStore[44].VisualType = (VisualType)13;
			DataStore[44].Seal();
			DataStore[45].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[45].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[19];
			DataStore[45].Toolkit = (Toolkit)3;
			DataStore[45].VisualType = (VisualType)13;
			DataStore[45].Seal();
			DataStore[46].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[154];
			DataStore[46].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[19];
			DataStore[46].Toolkit = (Toolkit)0;
			DataStore[46].VisualType = (VisualType)13;
			DataStore[46].Seal();
			DataStore[47].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[47].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[20];
			DataStore[47].Toolkit = (Toolkit)1;
			DataStore[47].VisualType = (VisualType)13;
			DataStore[47].Seal();
			DataStore[48].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[48].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[20];
			DataStore[48].Toolkit = (Toolkit)3;
			DataStore[48].VisualType = (VisualType)13;
			DataStore[48].Seal();
			DataStore[49].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[151];
			DataStore[49].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[20];
			DataStore[49].Toolkit = (Toolkit)0;
			DataStore[49].VisualType = (VisualType)5;
			DataStore[49].Seal();
			DataStore[50].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[50].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[21];
			DataStore[50].Toolkit = (Toolkit)1;
			DataStore[50].VisualType = (VisualType)13;
			DataStore[50].Seal();
			DataStore[51].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[51].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[21];
			DataStore[51].Toolkit = (Toolkit)3;
			DataStore[51].VisualType = (VisualType)13;
			DataStore[51].Seal();
			DataStore[52].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[147];
			DataStore[52].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[21];
			DataStore[52].Toolkit = (Toolkit)0;
			DataStore[52].VisualType = (VisualType)15;
			DataStore[52].Seal();
			DataStore[53].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[53].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[22];
			DataStore[53].Toolkit = (Toolkit)1;
			DataStore[53].VisualType = (VisualType)13;
			DataStore[53].Seal();
			DataStore[54].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[54].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[22];
			DataStore[54].Toolkit = (Toolkit)3;
			DataStore[54].VisualType = (VisualType)13;
			DataStore[54].Seal();
			DataStore[55].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[147];
			DataStore[55].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[22];
			DataStore[55].Toolkit = (Toolkit)0;
			DataStore[55].VisualType = (VisualType)15;
			DataStore[55].Seal();
			DataStore[56].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[56].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[23];
			DataStore[56].Toolkit = (Toolkit)1;
			DataStore[56].VisualType = (VisualType)13;
			DataStore[56].Seal();
			DataStore[57].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[57].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[23];
			DataStore[57].Toolkit = (Toolkit)3;
			DataStore[57].VisualType = (VisualType)13;
			DataStore[57].Seal();
			DataStore[58].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[147];
			DataStore[58].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[23];
			DataStore[58].Toolkit = (Toolkit)0;
			DataStore[58].VisualType = (VisualType)15;
			DataStore[58].Seal();
			DataStore[59].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[59].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[24];
			DataStore[59].Toolkit = (Toolkit)1;
			DataStore[59].VisualType = (VisualType)13;
			DataStore[59].Seal();
			DataStore[60].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[60].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[24];
			DataStore[60].Toolkit = (Toolkit)3;
			DataStore[60].VisualType = (VisualType)13;
			DataStore[60].Seal();
			DataStore[61].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[147];
			DataStore[61].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[24];
			DataStore[61].Toolkit = (Toolkit)0;
			DataStore[61].VisualType = (VisualType)15;
			DataStore[61].Seal();
			DataStore[62].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[152];
			DataStore[62].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[25];
			DataStore[62].Toolkit = (Toolkit)1;
			DataStore[62].VisualType = (VisualType)13;
			DataStore[62].Seal();
			DataStore[63].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[153];
			DataStore[63].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[25];
			DataStore[63].Toolkit = (Toolkit)3;
			DataStore[63].VisualType = (VisualType)13;
			DataStore[63].Seal();
			DataStore[64].ControlRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[147];
			DataStore[64].PresentedModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[25];
			DataStore[64].Toolkit = (Toolkit)0;
			DataStore[64].VisualType = (VisualType)15;
			DataStore[64].Seal();
	
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