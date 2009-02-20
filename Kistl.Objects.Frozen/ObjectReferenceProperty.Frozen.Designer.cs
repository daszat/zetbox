
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
                    NotifyPropertyChanged("LeftOf");
                }
            }
        }
        private Kistl.App.Base.Relation _LeftOf;

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
                    NotifyPropertyChanged("ReferenceObjectClass");
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
                    NotifyPropertyChanged("RightOf");
                }
            }
        }
        private Kistl.App.Base.Relation _RightOf;

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



		public override Type GetInterfaceType()
		{
			return typeof(ObjectReferenceProperty);
		}

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


        internal ObjectReferenceProperty__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, ObjectReferenceProperty__Implementation__Frozen> DataStore = new Dictionary<int, ObjectReferenceProperty__Implementation__Frozen>(68);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[7] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[7] = 
			DataStore[7] = new ObjectReferenceProperty__Implementation__Frozen(7);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[8] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[8] = 
			DataStore[8] = new ObjectReferenceProperty__Implementation__Frozen(8);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[14] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[14] = 
			DataStore[14] = new ObjectReferenceProperty__Implementation__Frozen(14);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[19] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[19] = 
			DataStore[19] = new ObjectReferenceProperty__Implementation__Frozen(19);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[21] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[21] = 
			DataStore[21] = new ObjectReferenceProperty__Implementation__Frozen(21);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[22] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[22] = 
			DataStore[22] = new ObjectReferenceProperty__Implementation__Frozen(22);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[25] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[25] = 
			DataStore[25] = new ObjectReferenceProperty__Implementation__Frozen(25);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[27] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[27] = 
			DataStore[27] = new ObjectReferenceProperty__Implementation__Frozen(27);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[29] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[29] = 
			DataStore[29] = new ObjectReferenceProperty__Implementation__Frozen(29);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[31] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[31] = 
			DataStore[31] = new ObjectReferenceProperty__Implementation__Frozen(31);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[44] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[44] = 
			DataStore[44] = new ObjectReferenceProperty__Implementation__Frozen(44);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[45] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[45] = 
			DataStore[45] = new ObjectReferenceProperty__Implementation__Frozen(45);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[46] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[46] = 
			DataStore[46] = new ObjectReferenceProperty__Implementation__Frozen(46);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[47] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[47] = 
			DataStore[47] = new ObjectReferenceProperty__Implementation__Frozen(47);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[49] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[49] = 
			DataStore[49] = new ObjectReferenceProperty__Implementation__Frozen(49);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[51] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[51] = 
			DataStore[51] = new ObjectReferenceProperty__Implementation__Frozen(51);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[53] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[53] = 
			DataStore[53] = new ObjectReferenceProperty__Implementation__Frozen(53);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[54] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[54] = 
			DataStore[54] = new ObjectReferenceProperty__Implementation__Frozen(54);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[55] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[55] = 
			DataStore[55] = new ObjectReferenceProperty__Implementation__Frozen(55);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[58] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[58] = 
			DataStore[58] = new ObjectReferenceProperty__Implementation__Frozen(58);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[64] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[64] = 
			DataStore[64] = new ObjectReferenceProperty__Implementation__Frozen(64);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[66] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[66] = 
			DataStore[66] = new ObjectReferenceProperty__Implementation__Frozen(66);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[67] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[67] = 
			DataStore[67] = new ObjectReferenceProperty__Implementation__Frozen(67);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[69] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[69] = 
			DataStore[69] = new ObjectReferenceProperty__Implementation__Frozen(69);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[70] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[70] = 
			DataStore[70] = new ObjectReferenceProperty__Implementation__Frozen(70);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[72] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[72] = 
			DataStore[72] = new ObjectReferenceProperty__Implementation__Frozen(72);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[73] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[73] = 
			DataStore[73] = new ObjectReferenceProperty__Implementation__Frozen(73);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[74] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[74] = 
			DataStore[74] = new ObjectReferenceProperty__Implementation__Frozen(74);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[78] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[78] = 
			DataStore[78] = new ObjectReferenceProperty__Implementation__Frozen(78);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[79] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[79] = 
			DataStore[79] = new ObjectReferenceProperty__Implementation__Frozen(79);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[80] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[80] = 
			DataStore[80] = new ObjectReferenceProperty__Implementation__Frozen(80);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[81] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[81] = 
			DataStore[81] = new ObjectReferenceProperty__Implementation__Frozen(81);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[82] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[82] = 
			DataStore[82] = new ObjectReferenceProperty__Implementation__Frozen(82);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[86] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[86] = 
			DataStore[86] = new ObjectReferenceProperty__Implementation__Frozen(86);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[88] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[88] = 
			DataStore[88] = new ObjectReferenceProperty__Implementation__Frozen(88);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[92] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[92] = 
			DataStore[92] = new ObjectReferenceProperty__Implementation__Frozen(92);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[96] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[96] = 
			DataStore[96] = new ObjectReferenceProperty__Implementation__Frozen(96);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[97] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[97] = 
			DataStore[97] = new ObjectReferenceProperty__Implementation__Frozen(97);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[98] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[98] = 
			DataStore[98] = new ObjectReferenceProperty__Implementation__Frozen(98);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[100] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[100] = 
			DataStore[100] = new ObjectReferenceProperty__Implementation__Frozen(100);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[103] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[103] = 
			DataStore[103] = new ObjectReferenceProperty__Implementation__Frozen(103);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[104] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[104] = 
			DataStore[104] = new ObjectReferenceProperty__Implementation__Frozen(104);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[105] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[105] = 
			DataStore[105] = new ObjectReferenceProperty__Implementation__Frozen(105);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[108] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[108] = 
			DataStore[108] = new ObjectReferenceProperty__Implementation__Frozen(108);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[112] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[112] = 
			DataStore[112] = new ObjectReferenceProperty__Implementation__Frozen(112);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[114] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[114] = 
			DataStore[114] = new ObjectReferenceProperty__Implementation__Frozen(114);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[129] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[129] = 
			DataStore[129] = new ObjectReferenceProperty__Implementation__Frozen(129);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[138] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[138] = 
			DataStore[138] = new ObjectReferenceProperty__Implementation__Frozen(138);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[147] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[147] = 
			DataStore[147] = new ObjectReferenceProperty__Implementation__Frozen(147);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[151] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[151] = 
			DataStore[151] = new ObjectReferenceProperty__Implementation__Frozen(151);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[152] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[152] = 
			DataStore[152] = new ObjectReferenceProperty__Implementation__Frozen(152);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[153] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[153] = 
			DataStore[153] = new ObjectReferenceProperty__Implementation__Frozen(153);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[155] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[155] = 
			DataStore[155] = new ObjectReferenceProperty__Implementation__Frozen(155);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[163] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[163] = 
			DataStore[163] = new ObjectReferenceProperty__Implementation__Frozen(163);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[164] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[164] = 
			DataStore[164] = new ObjectReferenceProperty__Implementation__Frozen(164);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[165] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[165] = 
			DataStore[165] = new ObjectReferenceProperty__Implementation__Frozen(165);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[170] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[170] = 
			DataStore[170] = new ObjectReferenceProperty__Implementation__Frozen(170);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[171] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[171] = 
			DataStore[171] = new ObjectReferenceProperty__Implementation__Frozen(171);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[181] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[181] = 
			DataStore[181] = new ObjectReferenceProperty__Implementation__Frozen(181);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[182] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[182] = 
			DataStore[182] = new ObjectReferenceProperty__Implementation__Frozen(182);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[185] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[185] = 
			DataStore[185] = new ObjectReferenceProperty__Implementation__Frozen(185);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[186] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[186] = 
			DataStore[186] = new ObjectReferenceProperty__Implementation__Frozen(186);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[206] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[206] = 
			DataStore[206] = new ObjectReferenceProperty__Implementation__Frozen(206);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[207] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[207] = 
			DataStore[207] = new ObjectReferenceProperty__Implementation__Frozen(207);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[208] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[208] = 
			DataStore[208] = new ObjectReferenceProperty__Implementation__Frozen(208);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[209] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[209] = 
			DataStore[209] = new ObjectReferenceProperty__Implementation__Frozen(209);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[211] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[211] = 
			DataStore[211] = new ObjectReferenceProperty__Implementation__Frozen(211);

			Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[212] = 
			Kistl.App.Base.Property__Implementation__Frozen.DataStore[212] = 
			DataStore[212] = new ObjectReferenceProperty__Implementation__Frozen(212);

		}

		internal new static void FillDataStore() {
			DataStore[7].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[7].PropertyName = @"Properties";
			DataStore[7].AltText = @"Eigenschaften der Objektklasse";
			DataStore[7].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[7].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[7].Description = @"Eigenschaften der Objektklasse";
			DataStore[7].IsList = true;
			DataStore[7].IsNullable = true;
			DataStore[7].IsIndexed = false;
			DataStore[7].RightOf = null;
			DataStore[7].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[1];
			DataStore[7].Seal();
			DataStore[8].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[5];
			DataStore[8].PropertyName = @"ObjectClass";
			DataStore[8].AltText = null;
			DataStore[8].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[8].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[144],
});
			DataStore[8].Description = null;
			DataStore[8].IsList = false;
			DataStore[8].IsNullable = false;
			DataStore[8].IsIndexed = false;
			DataStore[8].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[1];
			DataStore[8].LeftOf = null;
			DataStore[8].Seal();
			DataStore[14].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[14].PropertyName = @"Tasks";
			DataStore[14].AltText = null;
			DataStore[14].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[14].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[14].Description = null;
			DataStore[14].IsList = true;
			DataStore[14].IsNullable = true;
			DataStore[14].IsIndexed = false;
			DataStore[14].RightOf = null;
			DataStore[14].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[2];
			DataStore[14].Seal();
			DataStore[19].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4];
			DataStore[19].PropertyName = @"Projekt";
			DataStore[19].AltText = @"Verknüpfung zum Projekt";
			DataStore[19].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[19].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[19].Description = @"Verknüpfung zum Projekt";
			DataStore[19].IsList = false;
			DataStore[19].IsNullable = true;
			DataStore[19].IsIndexed = false;
			DataStore[19].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[2];
			DataStore[19].LeftOf = null;
			DataStore[19].Seal();
			DataStore[21].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6];
			DataStore[21].PropertyName = @"Projekte";
			DataStore[21].AltText = @"Projekte des Mitarbeiters für die er Verantwortlich ist";
			DataStore[21].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[21].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[21].Description = @"Projekte des Mitarbeiters für die er Verantwortlich ist";
			DataStore[21].IsList = true;
			DataStore[21].IsNullable = true;
			DataStore[21].IsIndexed = true;
			DataStore[21].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[3];
			DataStore[21].LeftOf = null;
			DataStore[21].Seal();
			DataStore[22].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[22].PropertyName = @"Mitarbeiter";
			DataStore[22].AltText = null;
			DataStore[22].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[22].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[22].Description = null;
			DataStore[22].IsList = true;
			DataStore[22].IsNullable = true;
			DataStore[22].IsIndexed = true;
			DataStore[22].RightOf = null;
			DataStore[22].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[3];
			DataStore[22].Seal();
			DataStore[25].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[25].PropertyName = @"BaseObjectClass";
			DataStore[25].AltText = @"Pointer auf die Basisklasse";
			DataStore[25].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[25].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[25].Description = @"Pointer auf die Basisklasse";
			DataStore[25].IsList = false;
			DataStore[25].IsNullable = true;
			DataStore[25].IsIndexed = false;
			DataStore[25].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[4];
			DataStore[25].LeftOf = null;
			DataStore[25].Seal();
			DataStore[27].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[27].PropertyName = @"SubClasses";
			DataStore[27].AltText = @"Liste der vererbten Klassen";
			DataStore[27].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[27].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[27].Description = @"Liste der vererbten Klassen";
			DataStore[27].IsList = true;
			DataStore[27].IsNullable = true;
			DataStore[27].IsIndexed = false;
			DataStore[27].RightOf = null;
			DataStore[27].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[4];
			DataStore[27].Seal();
			DataStore[29].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[29].PropertyName = @"ObjectClass";
			DataStore[29].AltText = null;
			DataStore[29].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[29].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[141],
});
			DataStore[29].Description = null;
			DataStore[29].IsList = false;
			DataStore[29].IsNullable = false;
			DataStore[29].IsIndexed = false;
			DataStore[29].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[5];
			DataStore[29].LeftOf = null;
			DataStore[29].Seal();
			DataStore[31].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[31].PropertyName = @"Methods";
			DataStore[31].AltText = @"Liste aller Methoden der Objektklasse.";
			DataStore[31].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[31].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[31].Description = @"Liste aller Methoden der Objektklasse.";
			DataStore[31].IsList = true;
			DataStore[31].IsNullable = true;
			DataStore[31].IsIndexed = false;
			DataStore[31].RightOf = null;
			DataStore[31].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[5];
			DataStore[31].Seal();
			DataStore[44].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[44].PropertyName = @"DataTypes";
			DataStore[44].AltText = @"Datentypendes Modules";
			DataStore[44].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[44].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[44].Description = @"Datentypendes Modules";
			DataStore[44].IsList = true;
			DataStore[44].IsNullable = true;
			DataStore[44].IsIndexed = false;
			DataStore[44].RightOf = null;
			DataStore[44].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[6];
			DataStore[44].Seal();
			DataStore[45].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[45].PropertyName = @"Module";
			DataStore[45].AltText = @"Modul der Objektklasse";
			DataStore[45].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[45].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[137],
});
			DataStore[45].Description = @"Modul der Objektklasse";
			DataStore[45].IsList = false;
			DataStore[45].IsNullable = false;
			DataStore[45].IsIndexed = false;
			DataStore[45].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[6];
			DataStore[45].LeftOf = null;
			DataStore[45].Seal();
			DataStore[46].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14];
			DataStore[46].PropertyName = @"ReferenceObjectClass";
			DataStore[46].AltText = @"Pointer zur Objektklasse";
			DataStore[46].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[46].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[46].Description = @"Pointer zur Objektklasse";
			DataStore[46].IsList = false;
			DataStore[46].IsNullable = false;
			DataStore[46].IsIndexed = false;
			DataStore[46].RightOf = null;
			DataStore[46].LeftOf = null;
			DataStore[46].Seal();
			DataStore[47].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[16];
			DataStore[47].PropertyName = @"ReferenceProperty";
			DataStore[47].AltText = @"Das Property, welches auf diese Klasse zeigt";
			DataStore[47].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[47].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[47].Description = @"Das Property, welches auf diese Klasse zeigt";
			DataStore[47].IsList = false;
			DataStore[47].IsNullable = true;
			DataStore[47].IsIndexed = false;
			DataStore[47].RightOf = null;
			DataStore[47].LeftOf = null;
			DataStore[47].Seal();
			DataStore[49].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[49].PropertyName = @"Mitarbeiter";
			DataStore[49].AltText = null;
			DataStore[49].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[49].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[49].Description = null;
			DataStore[49].IsList = false;
			DataStore[49].IsNullable = true;
			DataStore[49].IsIndexed = false;
			DataStore[49].RightOf = null;
			DataStore[49].LeftOf = null;
			DataStore[49].Seal();
			DataStore[51].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[51].PropertyName = @"Projekt";
			DataStore[51].AltText = @"Projekt zum Auftrag";
			DataStore[51].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[51].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[51].Description = @"Projekt zum Auftrag";
			DataStore[51].IsList = false;
			DataStore[51].IsNullable = true;
			DataStore[51].IsIndexed = false;
			DataStore[51].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[10];
			DataStore[51].LeftOf = null;
			DataStore[51].Seal();
			DataStore[53].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[23];
			DataStore[53].PropertyName = @"Projekt";
			DataStore[53].AltText = @"Projekt des Kostenträgers";
			DataStore[53].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[53].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[135],
});
			DataStore[53].Description = @"Projekt des Kostenträgers";
			DataStore[53].IsList = false;
			DataStore[53].IsNullable = false;
			DataStore[53].IsIndexed = false;
			DataStore[53].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[9];
			DataStore[53].LeftOf = null;
			DataStore[53].Seal();
			DataStore[54].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[54].PropertyName = @"Mitarbeiter";
			DataStore[54].AltText = @"Mitarbeiter";
			DataStore[54].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[54].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[134],
});
			DataStore[54].Description = @"Mitarbeiter";
			DataStore[54].IsList = false;
			DataStore[54].IsNullable = false;
			DataStore[54].IsIndexed = false;
			DataStore[54].RightOf = null;
			DataStore[54].LeftOf = null;
			DataStore[54].Seal();
			DataStore[55].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[55].PropertyName = @"Zeitkonto";
			DataStore[55].AltText = @"Zeitkonto";
			DataStore[55].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[55].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[133],
});
			DataStore[55].Description = @"Zeitkonto";
			DataStore[55].IsList = false;
			DataStore[55].IsNullable = false;
			DataStore[55].IsIndexed = false;
			DataStore[55].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[8];
			DataStore[55].LeftOf = null;
			DataStore[55].Seal();
			DataStore[58].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[58].PropertyName = @"Taetigkeiten";
			DataStore[58].AltText = @"Tätigkeiten";
			DataStore[58].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[58].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[58].Description = @"Tätigkeiten";
			DataStore[58].IsList = true;
			DataStore[58].IsNullable = true;
			DataStore[58].IsIndexed = false;
			DataStore[58].RightOf = null;
			DataStore[58].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[8];
			DataStore[58].Seal();
			DataStore[64].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19];
			DataStore[64].PropertyName = @"Kunde";
			DataStore[64].AltText = @"Kunde des Projektes";
			DataStore[64].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[64].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[64].Description = @"Kunde des Projektes";
			DataStore[64].IsList = false;
			DataStore[64].IsNullable = true;
			DataStore[64].IsIndexed = false;
			DataStore[64].RightOf = null;
			DataStore[64].LeftOf = null;
			DataStore[64].Seal();
			DataStore[66].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[66].PropertyName = @"Kostentraeger";
			DataStore[66].AltText = @"Kostenträger";
			DataStore[66].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[66].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[66].Description = @"Kostenträger";
			DataStore[66].IsList = true;
			DataStore[66].IsNullable = true;
			DataStore[66].IsIndexed = false;
			DataStore[66].RightOf = null;
			DataStore[66].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[9];
			DataStore[66].Seal();
			DataStore[67].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3];
			DataStore[67].PropertyName = @"Auftraege";
			DataStore[67].AltText = @"Aufträge";
			DataStore[67].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[67].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[67].Description = @"Aufträge";
			DataStore[67].IsList = true;
			DataStore[67].IsNullable = true;
			DataStore[67].IsIndexed = false;
			DataStore[67].RightOf = null;
			DataStore[67].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[10];
			DataStore[67].Seal();
			DataStore[69].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[69].PropertyName = @"DefaultIcon";
			DataStore[69].AltText = @"Standard Icon wenn IIcon nicht implementiert ist";
			DataStore[69].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[69].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[69].Description = @"Standard Icon wenn IIcon nicht implementiert ist";
			DataStore[69].IsList = false;
			DataStore[69].IsNullable = true;
			DataStore[69].IsIndexed = false;
			DataStore[69].RightOf = null;
			DataStore[69].LeftOf = null;
			DataStore[69].Seal();
			DataStore[70].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29];
			DataStore[70].PropertyName = @"Module";
			DataStore[70].AltText = @"Module";
			DataStore[70].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[70].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[127],
});
			DataStore[70].Description = @"Module";
			DataStore[70].IsList = false;
			DataStore[70].IsNullable = false;
			DataStore[70].IsIndexed = false;
			DataStore[70].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[13];
			DataStore[70].LeftOf = null;
			DataStore[70].Seal();
			DataStore[72].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[5];
			DataStore[72].PropertyName = @"Module";
			DataStore[72].AltText = @"Zugehörig zum Modul";
			DataStore[72].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[72].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[125],
});
			DataStore[72].Description = @"Zugehörig zum Modul";
			DataStore[72].IsList = false;
			DataStore[72].IsNullable = false;
			DataStore[72].IsIndexed = false;
			DataStore[72].RightOf = null;
			DataStore[72].LeftOf = null;
			DataStore[72].Seal();
			DataStore[73].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[73].PropertyName = @"Module";
			DataStore[73].AltText = @"Zugehörig zum Modul";
			DataStore[73].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[73].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[124],
});
			DataStore[73].Description = @"Zugehörig zum Modul";
			DataStore[73].IsList = false;
			DataStore[73].IsNullable = false;
			DataStore[73].IsIndexed = false;
			DataStore[73].RightOf = null;
			DataStore[73].LeftOf = null;
			DataStore[73].Seal();
			DataStore[74].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[74].PropertyName = @"Method";
			DataStore[74].AltText = @"Methode, die Aufgerufen wird";
			DataStore[74].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[74].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(2) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[123],
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[193],
});
			DataStore[74].Description = @"Methode, die Aufgerufen wird";
			DataStore[74].IsList = false;
			DataStore[74].IsNullable = false;
			DataStore[74].IsIndexed = false;
			DataStore[74].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[12];
			DataStore[74].LeftOf = null;
			DataStore[74].Seal();
			DataStore[78].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[78].PropertyName = @"Module";
			DataStore[78].AltText = @"Zugehörig zum Modul";
			DataStore[78].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[78].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[119],
});
			DataStore[78].Description = @"Zugehörig zum Modul";
			DataStore[78].IsList = false;
			DataStore[78].IsNullable = false;
			DataStore[78].IsIndexed = false;
			DataStore[78].RightOf = null;
			DataStore[78].LeftOf = null;
			DataStore[78].Seal();
			DataStore[79].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[79].PropertyName = @"InvokeOnObjectClass";
			DataStore[79].AltText = @"In dieser Objektklasse implementieren";
			DataStore[79].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[79].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[118],
});
			DataStore[79].Description = @"In dieser Objektklasse implementieren";
			DataStore[79].IsList = false;
			DataStore[79].IsNullable = false;
			DataStore[79].IsIndexed = false;
			DataStore[79].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[11];
			DataStore[79].LeftOf = null;
			DataStore[79].Seal();
			DataStore[80].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33];
			DataStore[80].PropertyName = @"MethodInvocations";
			DataStore[80].AltText = @"all implemented Methods in this DataType";
			DataStore[80].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[80].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[80].Description = @"all implemented Methods in this DataType";
			DataStore[80].IsList = true;
			DataStore[80].IsNullable = true;
			DataStore[80].IsIndexed = false;
			DataStore[80].RightOf = null;
			DataStore[80].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[11];
			DataStore[80].Seal();
			DataStore[81].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[81].PropertyName = @"MethodInvokations";
			DataStore[81].AltText = @"Methodenaufrufe implementiert in dieser Objekt Klasse";
			DataStore[81].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[81].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[81].Description = @"Methodenaufrufe implementiert in dieser Objekt Klasse";
			DataStore[81].IsList = true;
			DataStore[81].IsNullable = true;
			DataStore[81].IsIndexed = false;
			DataStore[81].RightOf = null;
			DataStore[81].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[12];
			DataStore[81].Seal();
			DataStore[82].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18];
			DataStore[82].PropertyName = @"Assemblies";
			DataStore[82].AltText = @"Assemblies des Moduls";
			DataStore[82].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[82].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[82].Description = @"Assemblies des Moduls";
			DataStore[82].IsList = true;
			DataStore[82].IsNullable = true;
			DataStore[82].IsIndexed = false;
			DataStore[82].RightOf = null;
			DataStore[82].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[13];
			DataStore[82].Seal();
			DataStore[86].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20];
			DataStore[86].PropertyName = @"Mitarbeiter";
			DataStore[86].AltText = @"Zugeordnete Mitarbeiter";
			DataStore[86].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[86].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[86].Description = @"Zugeordnete Mitarbeiter";
			DataStore[86].IsList = true;
			DataStore[86].IsNullable = true;
			DataStore[86].IsIndexed = false;
			DataStore[86].RightOf = null;
			DataStore[86].LeftOf = null;
			DataStore[86].Seal();
			DataStore[88].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25];
			DataStore[88].PropertyName = @"TaetigkeitsArt";
			DataStore[88].AltText = @"Art der Tätigkeit";
			DataStore[88].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[88].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[88].Description = @"Art der Tätigkeit";
			DataStore[88].IsList = false;
			DataStore[88].IsNullable = true;
			DataStore[88].IsIndexed = false;
			DataStore[88].RightOf = null;
			DataStore[88].LeftOf = null;
			DataStore[88].Seal();
			DataStore[92].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36];
			DataStore[92].PropertyName = @"Method";
			DataStore[92].AltText = @"Methode des Parameters";
			DataStore[92].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[92].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[113],
});
			DataStore[92].Description = @"Methode des Parameters";
			DataStore[92].IsList = false;
			DataStore[92].IsNullable = false;
			DataStore[92].IsIndexed = false;
			DataStore[92].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[14];
			DataStore[92].LeftOf = null;
			DataStore[92].Seal();
			DataStore[96].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10];
			DataStore[96].PropertyName = @"Parameter";
			DataStore[96].AltText = @"Parameter der Methode";
			DataStore[96].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[96].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[96].Description = @"Parameter der Methode";
			DataStore[96].IsList = true;
			DataStore[96].IsNullable = true;
			DataStore[96].IsIndexed = true;
			DataStore[96].RightOf = null;
			DataStore[96].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[14];
			DataStore[96].Seal();
			DataStore[97].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42];
			DataStore[97].PropertyName = @"DataType";
			DataStore[97].AltText = @"Kistl-Typ des Parameters";
			DataStore[97].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[97].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[109],
});
			DataStore[97].Description = @"Kistl-Typ des Parameters";
			DataStore[97].IsList = false;
			DataStore[97].IsNullable = false;
			DataStore[97].IsIndexed = false;
			DataStore[97].RightOf = null;
			DataStore[97].LeftOf = null;
			DataStore[97].Seal();
			DataStore[98].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43];
			DataStore[98].PropertyName = @"Assembly";
			DataStore[98].AltText = @"Assembly des CLR Objektes, NULL für Default Assemblies";
			DataStore[98].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[98].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[98].Description = @"Assembly des CLR Objektes, NULL für Default Assemblies";
			DataStore[98].IsList = false;
			DataStore[98].IsNullable = true;
			DataStore[98].IsIndexed = false;
			DataStore[98].RightOf = null;
			DataStore[98].LeftOf = null;
			DataStore[98].Seal();
			DataStore[100].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46];
			DataStore[100].PropertyName = @"Enumeration";
			DataStore[100].AltText = @"Übergeordnete Enumeration";
			DataStore[100].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[100].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[107],
});
			DataStore[100].Description = @"Übergeordnete Enumeration";
			DataStore[100].IsList = false;
			DataStore[100].IsNullable = false;
			DataStore[100].IsIndexed = false;
			DataStore[100].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[15];
			DataStore[100].LeftOf = null;
			DataStore[100].Seal();
			DataStore[103].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45];
			DataStore[103].PropertyName = @"EnumerationEntries";
			DataStore[103].AltText = @"Einträge der Enumeration";
			DataStore[103].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[103].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[103].Description = @"Einträge der Enumeration";
			DataStore[103].IsList = true;
			DataStore[103].IsNullable = true;
			DataStore[103].IsIndexed = false;
			DataStore[103].RightOf = null;
			DataStore[103].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[15];
			DataStore[103].Seal();
			DataStore[104].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[47];
			DataStore[104].PropertyName = @"Enumeration";
			DataStore[104].AltText = @"Enumeration der Eigenschaft";
			DataStore[104].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[104].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[106],
});
			DataStore[104].Description = @"Enumeration der Eigenschaft";
			DataStore[104].IsList = false;
			DataStore[104].IsNullable = false;
			DataStore[104].IsIndexed = false;
			DataStore[104].RightOf = null;
			DataStore[104].LeftOf = null;
			DataStore[104].Seal();
			DataStore[105].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[105].PropertyName = @"ImplementsInterfaces";
			DataStore[105].AltText = @"Interfaces der Objektklasse";
			DataStore[105].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[105].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[105].Description = @"Interfaces der Objektklasse";
			DataStore[105].IsList = true;
			DataStore[105].IsNullable = true;
			DataStore[105].IsIndexed = false;
			DataStore[105].RightOf = null;
			DataStore[105].LeftOf = null;
			DataStore[105].Seal();
			DataStore[108].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[48];
			DataStore[108].PropertyName = @"ObjectProp";
			DataStore[108].AltText = @"Objektpointer für das Testinterface";
			DataStore[108].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[108].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[104],
});
			DataStore[108].Description = @"Objektpointer für das Testinterface";
			DataStore[108].IsList = false;
			DataStore[108].IsNullable = false;
			DataStore[108].IsIndexed = false;
			DataStore[108].RightOf = null;
			DataStore[108].LeftOf = null;
			DataStore[108].Seal();
			DataStore[112].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51];
			DataStore[112].PropertyName = @"ObjectProp";
			DataStore[112].AltText = @"testtest";
			DataStore[112].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[112].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[100],
});
			DataStore[112].Description = @"testtest";
			DataStore[112].IsList = false;
			DataStore[112].IsNullable = false;
			DataStore[112].IsIndexed = false;
			DataStore[112].RightOf = null;
			DataStore[112].LeftOf = null;
			DataStore[112].Seal();
			DataStore[114].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54];
			DataStore[114].PropertyName = @"Assembly";
			DataStore[114].AltText = @"The assembly containing the Control";
			DataStore[114].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[114].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[98],
});
			DataStore[114].Description = @"The assembly containing the Control";
			DataStore[114].IsList = false;
			DataStore[114].IsNullable = false;
			DataStore[114].IsIndexed = false;
			DataStore[114].RightOf = null;
			DataStore[114].LeftOf = null;
			DataStore[114].Seal();
			DataStore[129].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[64];
			DataStore[129].PropertyName = @"StructDefinition";
			DataStore[129].AltText = @"Definition of this Struct";
			DataStore[129].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[129].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[91],
});
			DataStore[129].Description = @"Definition of this Struct";
			DataStore[129].IsList = false;
			DataStore[129].IsNullable = false;
			DataStore[129].IsIndexed = false;
			DataStore[129].RightOf = null;
			DataStore[129].LeftOf = null;
			DataStore[129].Seal();
			DataStore[138].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[138].PropertyName = @"PresenterAssembly";
			DataStore[138].AltText = @"Where to find the implementation of the Presenter";
			DataStore[138].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[138].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[86],
});
			DataStore[138].Description = @"Where to find the implementation of the Presenter";
			DataStore[138].IsList = false;
			DataStore[138].IsNullable = false;
			DataStore[138].IsIndexed = false;
			DataStore[138].RightOf = null;
			DataStore[138].LeftOf = null;
			DataStore[138].Seal();
			DataStore[147].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66];
			DataStore[147].PropertyName = @"DataAssembly";
			DataStore[147].AltText = @"The Assembly of the Data Type";
			DataStore[147].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[147].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[147].Description = @"The Assembly of the Data Type";
			DataStore[147].IsList = false;
			DataStore[147].IsNullable = true;
			DataStore[147].IsIndexed = false;
			DataStore[147].RightOf = null;
			DataStore[147].LeftOf = null;
			DataStore[147].Seal();
			DataStore[151].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[151].PropertyName = @"Children";
			DataStore[151].AltText = @"if this is a container, here are the visually contained/controlled children of this Visual";
			DataStore[151].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[151].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[151].Description = @"if this is a container, here are the visually contained/controlled children of this Visual";
			DataStore[151].IsList = true;
			DataStore[151].IsNullable = true;
			DataStore[151].IsIndexed = false;
			DataStore[151].RightOf = null;
			DataStore[151].LeftOf = null;
			DataStore[151].Seal();
			DataStore[152].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[152].PropertyName = @"Property";
			DataStore[152].AltText = @"The Property to display";
			DataStore[152].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[152].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[152].Description = @"The Property to display";
			DataStore[152].IsList = false;
			DataStore[152].IsNullable = true;
			DataStore[152].IsIndexed = false;
			DataStore[152].RightOf = null;
			DataStore[152].LeftOf = null;
			DataStore[152].Seal();
			DataStore[153].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[153].PropertyName = @"Method";
			DataStore[153].AltText = @"The Method whose return value shoud be displayed";
			DataStore[153].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[153].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[81],
});
			DataStore[153].Description = @"The Method whose return value shoud be displayed";
			DataStore[153].IsList = false;
			DataStore[153].IsNullable = false;
			DataStore[153].IsIndexed = false;
			DataStore[153].RightOf = null;
			DataStore[153].LeftOf = null;
			DataStore[153].Seal();
			DataStore[155].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[155].PropertyName = @"VisualTree";
			DataStore[155].AltText = @"The visual representation of this Template";
			DataStore[155].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[155].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[79],
});
			DataStore[155].Description = @"The visual representation of this Template";
			DataStore[155].IsList = false;
			DataStore[155].IsNullable = false;
			DataStore[155].IsIndexed = false;
			DataStore[155].RightOf = null;
			DataStore[155].LeftOf = null;
			DataStore[155].Seal();
			DataStore[163].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[163].PropertyName = @"DisplayedTypeAssembly";
			DataStore[163].AltText = @"Assembly of the Type that is displayed with this Template";
			DataStore[163].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[163].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[77],
});
			DataStore[163].Description = @"Assembly of the Type that is displayed with this Template";
			DataStore[163].IsList = false;
			DataStore[163].IsNullable = false;
			DataStore[163].IsIndexed = false;
			DataStore[163].RightOf = null;
			DataStore[163].LeftOf = null;
			DataStore[163].Seal();
			DataStore[164].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67];
			DataStore[164].PropertyName = @"ContextMenu";
			DataStore[164].AltText = @"The context menu for this Visual";
			DataStore[164].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[164].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[164].Description = @"The context menu for this Visual";
			DataStore[164].IsList = true;
			DataStore[164].IsNullable = true;
			DataStore[164].IsIndexed = false;
			DataStore[164].RightOf = null;
			DataStore[164].LeftOf = null;
			DataStore[164].Seal();
			DataStore[165].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68];
			DataStore[165].PropertyName = @"Menu";
			DataStore[165].AltText = @"The main menu for this Template";
			DataStore[165].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[165].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[165].Description = @"The main menu for this Template";
			DataStore[165].IsList = true;
			DataStore[165].IsNullable = true;
			DataStore[165].IsIndexed = false;
			DataStore[165].RightOf = null;
			DataStore[165].LeftOf = null;
			DataStore[165].Seal();
			DataStore[170].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[5];
			DataStore[170].PropertyName = @"Constraints";
			DataStore[170].AltText = @"The list of constraints applying to this Property";
			DataStore[170].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[170].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[170].Description = @"The list of constraints applying to this Property";
			DataStore[170].IsList = true;
			DataStore[170].IsNullable = true;
			DataStore[170].IsIndexed = false;
			DataStore[170].RightOf = null;
			DataStore[170].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[16];
			DataStore[170].Seal();
			DataStore[171].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69];
			DataStore[171].PropertyName = @"ConstrainedProperty";
			DataStore[171].AltText = @"The property to be constrained";
			DataStore[171].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[171].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[74],
});
			DataStore[171].Description = @"The property to be constrained";
			DataStore[171].IsList = false;
			DataStore[171].IsNullable = false;
			DataStore[171].IsIndexed = false;
			DataStore[171].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[16];
			DataStore[171].LeftOf = null;
			DataStore[171].Seal();
			DataStore[181].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[181].PropertyName = @"LeftPart";
			DataStore[181].AltText = @"Left Part of the Relation";
			DataStore[181].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[181].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[211],
});
			DataStore[181].Description = @"Left Part of the Relation";
			DataStore[181].IsList = false;
			DataStore[181].IsNullable = false;
			DataStore[181].IsIndexed = false;
			DataStore[181].RightOf = null;
			DataStore[181].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[17];
			DataStore[181].Seal();
			DataStore[182].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77];
			DataStore[182].PropertyName = @"RightPart";
			DataStore[182].AltText = @"Right Part of the Relation";
			DataStore[182].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[182].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[210],
});
			DataStore[182].Description = @"Right Part of the Relation";
			DataStore[182].IsList = false;
			DataStore[182].IsNullable = false;
			DataStore[182].IsIndexed = false;
			DataStore[182].RightOf = null;
			DataStore[182].LeftOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[18];
			DataStore[182].Seal();
			DataStore[185].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14];
			DataStore[185].PropertyName = @"RightOf";
			DataStore[185].AltText = @"This Property is the right Part of the selected Relation.";
			DataStore[185].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[185].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[185].Description = @"This Property is the right Part of the selected Relation.";
			DataStore[185].IsList = false;
			DataStore[185].IsNullable = true;
			DataStore[185].IsIndexed = false;
			DataStore[185].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[18];
			DataStore[185].LeftOf = null;
			DataStore[185].Seal();
			DataStore[186].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14];
			DataStore[186].PropertyName = @"LeftOf";
			DataStore[186].AltText = @"This Property is the left Part of the selected Relation.";
			DataStore[186].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[186].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[186].Description = @"This Property is the left Part of the selected Relation.";
			DataStore[186].IsList = false;
			DataStore[186].IsNullable = true;
			DataStore[186].IsIndexed = false;
			DataStore[186].RightOf = Kistl.App.Base.Relation__Implementation__Frozen.DataStore[17];
			DataStore[186].LeftOf = null;
			DataStore[186].Seal();
			DataStore[206].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[206].PropertyName = @"Assembly";
			DataStore[206].AltText = @"The assembly containing the referenced Type.";
			DataStore[206].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[206].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[206].Description = @"The assembly containing the referenced Type.";
			DataStore[206].IsList = false;
			DataStore[206].IsNullable = false;
			DataStore[206].IsIndexed = false;
			DataStore[206].RightOf = null;
			DataStore[206].LeftOf = null;
			DataStore[206].Seal();
			DataStore[207].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79];
			DataStore[207].PropertyName = @"GenericArguments";
			DataStore[207].AltText = @"list of type arguments";
			DataStore[207].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[207].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[207].Description = @"list of type arguments";
			DataStore[207].IsList = true;
			DataStore[207].IsNullable = true;
			DataStore[207].IsIndexed = true;
			DataStore[207].RightOf = null;
			DataStore[207].LeftOf = null;
			DataStore[207].Seal();
			DataStore[208].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30];
			DataStore[208].PropertyName = @"Implementor";
			DataStore[208].AltText = @"The Type implementing this invocation";
			DataStore[208].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[208].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[208].Description = @"The Type implementing this invocation";
			DataStore[208].IsList = false;
			DataStore[208].IsNullable = false;
			DataStore[208].IsIndexed = false;
			DataStore[208].RightOf = null;
			DataStore[208].LeftOf = null;
			DataStore[208].Seal();
			DataStore[209].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[80];
			DataStore[209].PropertyName = @"LayoutRef";
			DataStore[209].AltText = @"Which Layout is handled by this View";
			DataStore[209].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[209].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[213],
});
			DataStore[209].Description = null;
			DataStore[209].IsList = false;
			DataStore[209].IsNullable = false;
			DataStore[209].IsIndexed = false;
			DataStore[209].RightOf = null;
			DataStore[209].LeftOf = null;
			DataStore[209].Seal();
			DataStore[211].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[80];
			DataStore[211].PropertyName = @"ViewRef";
			DataStore[211].AltText = @"the Type of a View for this Layout";
			DataStore[211].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[211].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(1) {
Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[215],
});
			DataStore[211].Description = null;
			DataStore[211].IsList = false;
			DataStore[211].IsNullable = false;
			DataStore[211].IsIndexed = false;
			DataStore[211].RightOf = null;
			DataStore[211].LeftOf = null;
			DataStore[211].Seal();
			DataStore[212].ObjectClass = Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2];
			DataStore[212].PropertyName = @"DefaultModel";
			DataStore[212].AltText = @"The default model to use for the UI";
			DataStore[212].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[212].Constraints = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Constraint>(new List<Kistl.App.Base.Constraint>(0) {
});
			DataStore[212].Description = @"The default model to use for the UI";
			DataStore[212].IsList = false;
			DataStore[212].IsNullable = true;
			DataStore[212].IsIndexed = false;
			DataStore[212].RightOf = null;
			DataStore[212].LeftOf = null;
			DataStore[212].Seal();
	
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