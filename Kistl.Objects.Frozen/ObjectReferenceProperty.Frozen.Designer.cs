
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
    /// Metadefinition Object for ObjectReference Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectReferenceProperty")]
    public class ObjectReferenceProperty__Implementation__Frozen : Kistl.App.Base.Property__Implementation__Frozen, ObjectReferenceProperty
    {


        /// <summary>
        /// Pointer zur Objektklasse
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.ObjectClass ReferenceObjectClass
        {
            get
            {
                return _ReferenceObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ReferenceObjectClass != value)
                {
                    NotifyPropertyChanging("ReferenceObjectClass");
                    _ReferenceObjectClass = value;
                    NotifyPropertyChanged("ReferenceObjectClass");;
                }
            }
        }
        private Kistl.App.Base.ObjectClass _ReferenceObjectClass;

        /// <summary>
        /// This Property is the right Part of the selected Relation.
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Relation RightOf
        {
            get
            {
                return _RightOf;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_RightOf != value)
                {
                    NotifyPropertyChanging("RightOf");
                    _RightOf = value;
                    NotifyPropertyChanged("RightOf");;
                }
            }
        }
        private Kistl.App.Base.Relation _RightOf;

        /// <summary>
        /// This Property is the left Part of the selected Relation.
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Relation LeftOf
        {
            get
            {
                return _LeftOf;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_LeftOf != value)
                {
                    NotifyPropertyChanging("LeftOf");
                    _LeftOf = value;
                    NotifyPropertyChanged("LeftOf");;
                }
            }
        }
        private Kistl.App.Base.Relation _LeftOf;

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ObjectReferenceProperty != null)
            {
                OnGetPropertyTypeString_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<ObjectReferenceProperty> OnGetPropertyTypeString_ObjectReferenceProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_ObjectReferenceProperty != null)
            {
                OnGetGUIRepresentation_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<ObjectReferenceProperty> OnGetGUIRepresentation_ObjectReferenceProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_ObjectReferenceProperty != null)
            {
                OnGetPropertyType_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<ObjectReferenceProperty> OnGetPropertyType_ObjectReferenceProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectReferenceProperty != null)
            {
                OnToString_ObjectReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectReferenceProperty> OnToString_ObjectReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectReferenceProperty != null) OnPreSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPreSave_ObjectReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectReferenceProperty != null) OnPostSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPostSave_ObjectReferenceProperty;


        internal ObjectReferenceProperty__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, ObjectReferenceProperty__Implementation__Frozen> DataStore = new Dictionary<int, ObjectReferenceProperty__Implementation__Frozen>(68);
		static ObjectReferenceProperty__Implementation__Frozen()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[7] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[7] = 
			DataStore[7] = new ObjectReferenceProperty__Implementation__Frozen(null, 7);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[8] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[8] = 
			DataStore[8] = new ObjectReferenceProperty__Implementation__Frozen(null, 8);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[14] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[14] = 
			DataStore[14] = new ObjectReferenceProperty__Implementation__Frozen(null, 14);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[19] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[19] = 
			DataStore[19] = new ObjectReferenceProperty__Implementation__Frozen(null, 19);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[21] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[21] = 
			DataStore[21] = new ObjectReferenceProperty__Implementation__Frozen(null, 21);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[22] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[22] = 
			DataStore[22] = new ObjectReferenceProperty__Implementation__Frozen(null, 22);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[25] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[25] = 
			DataStore[25] = new ObjectReferenceProperty__Implementation__Frozen(null, 25);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[27] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[27] = 
			DataStore[27] = new ObjectReferenceProperty__Implementation__Frozen(null, 27);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[29] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[29] = 
			DataStore[29] = new ObjectReferenceProperty__Implementation__Frozen(null, 29);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[31] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[31] = 
			DataStore[31] = new ObjectReferenceProperty__Implementation__Frozen(null, 31);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[44] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[44] = 
			DataStore[44] = new ObjectReferenceProperty__Implementation__Frozen(null, 44);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[45] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[45] = 
			DataStore[45] = new ObjectReferenceProperty__Implementation__Frozen(null, 45);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[46] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[46] = 
			DataStore[46] = new ObjectReferenceProperty__Implementation__Frozen(null, 46);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[47] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[47] = 
			DataStore[47] = new ObjectReferenceProperty__Implementation__Frozen(null, 47);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[49] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[49] = 
			DataStore[49] = new ObjectReferenceProperty__Implementation__Frozen(null, 49);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[51] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[51] = 
			DataStore[51] = new ObjectReferenceProperty__Implementation__Frozen(null, 51);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[53] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[53] = 
			DataStore[53] = new ObjectReferenceProperty__Implementation__Frozen(null, 53);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[54] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[54] = 
			DataStore[54] = new ObjectReferenceProperty__Implementation__Frozen(null, 54);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[55] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[55] = 
			DataStore[55] = new ObjectReferenceProperty__Implementation__Frozen(null, 55);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[58] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[58] = 
			DataStore[58] = new ObjectReferenceProperty__Implementation__Frozen(null, 58);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[64] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[64] = 
			DataStore[64] = new ObjectReferenceProperty__Implementation__Frozen(null, 64);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[66] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[66] = 
			DataStore[66] = new ObjectReferenceProperty__Implementation__Frozen(null, 66);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[67] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[67] = 
			DataStore[67] = new ObjectReferenceProperty__Implementation__Frozen(null, 67);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[69] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[69] = 
			DataStore[69] = new ObjectReferenceProperty__Implementation__Frozen(null, 69);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[70] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[70] = 
			DataStore[70] = new ObjectReferenceProperty__Implementation__Frozen(null, 70);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[72] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[72] = 
			DataStore[72] = new ObjectReferenceProperty__Implementation__Frozen(null, 72);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[73] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[73] = 
			DataStore[73] = new ObjectReferenceProperty__Implementation__Frozen(null, 73);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[74] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[74] = 
			DataStore[74] = new ObjectReferenceProperty__Implementation__Frozen(null, 74);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[78] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[78] = 
			DataStore[78] = new ObjectReferenceProperty__Implementation__Frozen(null, 78);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[79] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[79] = 
			DataStore[79] = new ObjectReferenceProperty__Implementation__Frozen(null, 79);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[80] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[80] = 
			DataStore[80] = new ObjectReferenceProperty__Implementation__Frozen(null, 80);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[81] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[81] = 
			DataStore[81] = new ObjectReferenceProperty__Implementation__Frozen(null, 81);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[82] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[82] = 
			DataStore[82] = new ObjectReferenceProperty__Implementation__Frozen(null, 82);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[86] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[86] = 
			DataStore[86] = new ObjectReferenceProperty__Implementation__Frozen(null, 86);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[88] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[88] = 
			DataStore[88] = new ObjectReferenceProperty__Implementation__Frozen(null, 88);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[92] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[92] = 
			DataStore[92] = new ObjectReferenceProperty__Implementation__Frozen(null, 92);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[96] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[96] = 
			DataStore[96] = new ObjectReferenceProperty__Implementation__Frozen(null, 96);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[97] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[97] = 
			DataStore[97] = new ObjectReferenceProperty__Implementation__Frozen(null, 97);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[98] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[98] = 
			DataStore[98] = new ObjectReferenceProperty__Implementation__Frozen(null, 98);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[100] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[100] = 
			DataStore[100] = new ObjectReferenceProperty__Implementation__Frozen(null, 100);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[103] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[103] = 
			DataStore[103] = new ObjectReferenceProperty__Implementation__Frozen(null, 103);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[104] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[104] = 
			DataStore[104] = new ObjectReferenceProperty__Implementation__Frozen(null, 104);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[105] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[105] = 
			DataStore[105] = new ObjectReferenceProperty__Implementation__Frozen(null, 105);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[108] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[108] = 
			DataStore[108] = new ObjectReferenceProperty__Implementation__Frozen(null, 108);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[112] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[112] = 
			DataStore[112] = new ObjectReferenceProperty__Implementation__Frozen(null, 112);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[114] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[114] = 
			DataStore[114] = new ObjectReferenceProperty__Implementation__Frozen(null, 114);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[129] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[129] = 
			DataStore[129] = new ObjectReferenceProperty__Implementation__Frozen(null, 129);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[138] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[138] = 
			DataStore[138] = new ObjectReferenceProperty__Implementation__Frozen(null, 138);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[147] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[147] = 
			DataStore[147] = new ObjectReferenceProperty__Implementation__Frozen(null, 147);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[151] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[151] = 
			DataStore[151] = new ObjectReferenceProperty__Implementation__Frozen(null, 151);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[152] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[152] = 
			DataStore[152] = new ObjectReferenceProperty__Implementation__Frozen(null, 152);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[153] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[153] = 
			DataStore[153] = new ObjectReferenceProperty__Implementation__Frozen(null, 153);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[155] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[155] = 
			DataStore[155] = new ObjectReferenceProperty__Implementation__Frozen(null, 155);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[163] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[163] = 
			DataStore[163] = new ObjectReferenceProperty__Implementation__Frozen(null, 163);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[164] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[164] = 
			DataStore[164] = new ObjectReferenceProperty__Implementation__Frozen(null, 164);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[165] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[165] = 
			DataStore[165] = new ObjectReferenceProperty__Implementation__Frozen(null, 165);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[170] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[170] = 
			DataStore[170] = new ObjectReferenceProperty__Implementation__Frozen(null, 170);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[171] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[171] = 
			DataStore[171] = new ObjectReferenceProperty__Implementation__Frozen(null, 171);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[181] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[181] = 
			DataStore[181] = new ObjectReferenceProperty__Implementation__Frozen(null, 181);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[182] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[182] = 
			DataStore[182] = new ObjectReferenceProperty__Implementation__Frozen(null, 182);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[185] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[185] = 
			DataStore[185] = new ObjectReferenceProperty__Implementation__Frozen(null, 185);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[186] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[186] = 
			DataStore[186] = new ObjectReferenceProperty__Implementation__Frozen(null, 186);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[206] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[206] = 
			DataStore[206] = new ObjectReferenceProperty__Implementation__Frozen(null, 206);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[207] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[207] = 
			DataStore[207] = new ObjectReferenceProperty__Implementation__Frozen(null, 207);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[208] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[208] = 
			DataStore[208] = new ObjectReferenceProperty__Implementation__Frozen(null, 208);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[209] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[209] = 
			DataStore[209] = new ObjectReferenceProperty__Implementation__Frozen(null, 209);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[211] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[211] = 
			DataStore[211] = new ObjectReferenceProperty__Implementation__Frozen(null, 211);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[212] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[212] = 
			DataStore[212] = new ObjectReferenceProperty__Implementation__Frozen(null, 212);

		}

		internal new static void FillDataStore() {
			DataStore[7].RightOf = null;
			DataStore[7].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[1];
			DataStore[8].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[1];
			DataStore[8].LeftOf = null;
			DataStore[14].RightOf = null;
			DataStore[14].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[2];
			DataStore[19].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[2];
			DataStore[19].LeftOf = null;
			DataStore[21].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[3];
			DataStore[21].LeftOf = null;
			DataStore[22].RightOf = null;
			DataStore[22].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[3];
			DataStore[25].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[4];
			DataStore[25].LeftOf = null;
			DataStore[27].RightOf = null;
			DataStore[27].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[4];
			DataStore[29].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[5];
			DataStore[29].LeftOf = null;
			DataStore[31].RightOf = null;
			DataStore[31].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[5];
			DataStore[44].RightOf = null;
			DataStore[44].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[6];
			DataStore[45].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[6];
			DataStore[45].LeftOf = null;
			DataStore[46].RightOf = null;
			DataStore[46].LeftOf = null;
			DataStore[47].RightOf = null;
			DataStore[47].LeftOf = null;
			DataStore[49].RightOf = null;
			DataStore[49].LeftOf = null;
			DataStore[51].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[10];
			DataStore[51].LeftOf = null;
			DataStore[53].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[9];
			DataStore[53].LeftOf = null;
			DataStore[54].RightOf = null;
			DataStore[54].LeftOf = null;
			DataStore[55].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[8];
			DataStore[55].LeftOf = null;
			DataStore[58].RightOf = null;
			DataStore[58].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[8];
			DataStore[64].RightOf = null;
			DataStore[64].LeftOf = null;
			DataStore[66].RightOf = null;
			DataStore[66].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[9];
			DataStore[67].RightOf = null;
			DataStore[67].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[10];
			DataStore[69].RightOf = null;
			DataStore[69].LeftOf = null;
			DataStore[70].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[13];
			DataStore[70].LeftOf = null;
			DataStore[72].RightOf = null;
			DataStore[72].LeftOf = null;
			DataStore[73].RightOf = null;
			DataStore[73].LeftOf = null;
			DataStore[74].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[12];
			DataStore[74].LeftOf = null;
			DataStore[78].RightOf = null;
			DataStore[78].LeftOf = null;
			DataStore[79].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[11];
			DataStore[79].LeftOf = null;
			DataStore[80].RightOf = null;
			DataStore[80].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[11];
			DataStore[81].RightOf = null;
			DataStore[81].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[12];
			DataStore[82].RightOf = null;
			DataStore[82].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[13];
			DataStore[86].RightOf = null;
			DataStore[86].LeftOf = null;
			DataStore[88].RightOf = null;
			DataStore[88].LeftOf = null;
			DataStore[92].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[14];
			DataStore[92].LeftOf = null;
			DataStore[96].RightOf = null;
			DataStore[96].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[14];
			DataStore[97].RightOf = null;
			DataStore[97].LeftOf = null;
			DataStore[98].RightOf = null;
			DataStore[98].LeftOf = null;
			DataStore[100].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[15];
			DataStore[100].LeftOf = null;
			DataStore[103].RightOf = null;
			DataStore[103].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[15];
			DataStore[104].RightOf = null;
			DataStore[104].LeftOf = null;
			DataStore[105].RightOf = null;
			DataStore[105].LeftOf = null;
			DataStore[108].RightOf = null;
			DataStore[108].LeftOf = null;
			DataStore[112].RightOf = null;
			DataStore[112].LeftOf = null;
			DataStore[114].RightOf = null;
			DataStore[114].LeftOf = null;
			DataStore[129].RightOf = null;
			DataStore[129].LeftOf = null;
			DataStore[138].RightOf = null;
			DataStore[138].LeftOf = null;
			DataStore[147].RightOf = null;
			DataStore[147].LeftOf = null;
			DataStore[151].RightOf = null;
			DataStore[151].LeftOf = null;
			DataStore[152].RightOf = null;
			DataStore[152].LeftOf = null;
			DataStore[153].RightOf = null;
			DataStore[153].LeftOf = null;
			DataStore[155].RightOf = null;
			DataStore[155].LeftOf = null;
			DataStore[163].RightOf = null;
			DataStore[163].LeftOf = null;
			DataStore[164].RightOf = null;
			DataStore[164].LeftOf = null;
			DataStore[165].RightOf = null;
			DataStore[165].LeftOf = null;
			DataStore[170].RightOf = null;
			DataStore[170].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[16];
			DataStore[171].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[16];
			DataStore[171].LeftOf = null;
			DataStore[181].RightOf = null;
			DataStore[181].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[17];
			DataStore[182].RightOf = null;
			DataStore[182].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[18];
			DataStore[185].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[18];
			DataStore[185].LeftOf = null;
			DataStore[186].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[17];
			DataStore[186].LeftOf = null;
			DataStore[206].RightOf = null;
			DataStore[206].LeftOf = null;
			DataStore[207].RightOf = null;
			DataStore[207].LeftOf = null;
			DataStore[208].RightOf = null;
			DataStore[208].LeftOf = null;
			DataStore[209].RightOf = null;
			DataStore[209].LeftOf = null;
			DataStore[211].RightOf = null;
			DataStore[211].LeftOf = null;
			DataStore[212].RightOf = null;
			DataStore[212].LeftOf = null;
	
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