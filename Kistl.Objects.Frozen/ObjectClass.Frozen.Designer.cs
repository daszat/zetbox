
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
    /// Metadefinition Object for ObjectClasses.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectClass")]
    public class ObjectClass__Implementation__Frozen : Kistl.App.Base.DataType__Implementation__Frozen, ObjectClass
    {


        /// <summary>
        /// Tabellenname in der Datenbank
        /// </summary>
        // value type property
        public virtual string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TableName != value)
                {
                    NotifyPropertyChanging("TableName");
                    _TableName = value;
                    NotifyPropertyChanged("TableName");;
                }
            }
        }
        private string _TableName;

        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                return _BaseObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_BaseObjectClass != value)
                {
                    NotifyPropertyChanging("BaseObjectClass");
                    _BaseObjectClass = value;
                    NotifyPropertyChanged("BaseObjectClass");;
                }
            }
        }
        private Kistl.App.Base.ObjectClass _BaseObjectClass;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                if (_SubClasses == null)
                    _SubClasses = new ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0));
                return _SubClasses;
            }
internal set { _SubClasses = (ReadOnlyCollection<Kistl.App.Base.ObjectClass>)value; }
        }
        private ReadOnlyCollection<Kistl.App.Base.ObjectClass> _SubClasses;

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
        {
            get
            {
                if (_ImplementsInterfaces == null)
                    _ImplementsInterfaces = new ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0));
                return _ImplementsInterfaces;
            }
internal set { _ImplementsInterfaces = (ReadOnlyCollection<Kistl.App.Base.Interface>)value; }
        }
        private ReadOnlyCollection<Kistl.App.Base.Interface> _ImplementsInterfaces;

        /// <summary>
        /// Setting this to true marks the instances of this class as "simple." At first this will only mean that they'll be displayed inline.
        /// </summary>
        // value type property
        public virtual bool IsSimpleObject
        {
            get
            {
                return _IsSimpleObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsSimpleObject != value)
                {
                    NotifyPropertyChanging("IsSimpleObject");
                    _IsSimpleObject = value;
                    NotifyPropertyChanged("IsSimpleObject");;
                }
            }
        }
        private bool _IsSimpleObject;

        /// <summary>
        /// if true then all Instances appear in FozenContext.
        /// </summary>
        // value type property
        public virtual bool IsFrozenObject
        {
            get
            {
                return _IsFrozenObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsFrozenObject != value)
                {
                    NotifyPropertyChanging("IsFrozenObject");
                    _IsFrozenObject = value;
                    NotifyPropertyChanged("IsFrozenObject");;
                }
            }
        }
        private bool _IsFrozenObject;

        /// <summary>
        /// The default model to use for the UI
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef DefaultModel
        {
            get
            {
                return _DefaultModel;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultModel != value)
                {
                    NotifyPropertyChanging("DefaultModel");
                    _DefaultModel = value;
                    NotifyPropertyChanged("DefaultModel");;
                }
            }
        }
        private Kistl.App.Base.TypeRef _DefaultModel;

        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_ObjectClass != null)
            {
                OnGetDataTypeString_ObjectClass(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;



        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_ObjectClass != null)
            {
                OnGetDataType_ObjectClass(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<ObjectClass> OnGetDataType_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual IList<Kistl.App.Base.Method> GetInheritedMethods() 
        {
            var e = new MethodReturnEventArgs<IList<Kistl.App.Base.Method>>();
            if (OnGetInheritedMethods_ObjectClass != null)
            {
                OnGetInheritedMethods_ObjectClass(this, e);
            };
            return e.Result;
        }
		public delegate void GetInheritedMethods_Handler<T>(T obj, MethodReturnEventArgs<IList<Kistl.App.Base.Method>> ret);
		public event GetInheritedMethods_Handler<ObjectClass> OnGetInheritedMethods_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual Kistl.App.Base.TypeRef GetDefaultModelRef() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.TypeRef>();
            if (OnGetDefaultModelRef_ObjectClass != null)
            {
                OnGetDefaultModelRef_ObjectClass(this, e);
            };
            return e.Result;
        }
		public delegate void GetDefaultModelRef_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.TypeRef> ret);
		public event GetDefaultModelRef_Handler<ObjectClass> OnGetDefaultModelRef_ObjectClass;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectClass != null)
            {
                OnToString_ObjectClass(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectClass> OnToString_ObjectClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectClass != null) OnPreSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPreSave_ObjectClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectClass != null) OnPostSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPostSave_ObjectClass;


        internal ObjectClass__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, ObjectClass__Implementation__Frozen> DataStore = new Dictionary<int, ObjectClass__Implementation__Frozen>(60);
		static ObjectClass__Implementation__Frozen()
		{
			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2] = 
			DataStore[2] = new ObjectClass__Implementation__Frozen(null, 2);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3] = 
			DataStore[3] = new ObjectClass__Implementation__Frozen(null, 3);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4] = 
			DataStore[4] = new ObjectClass__Implementation__Frozen(null, 4);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[5] = 
			DataStore[5] = new ObjectClass__Implementation__Frozen(null, 5);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6] = 
			DataStore[6] = new ObjectClass__Implementation__Frozen(null, 6);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7] = 
			DataStore[7] = new ObjectClass__Implementation__Frozen(null, 7);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[8] = 
			DataStore[8] = new ObjectClass__Implementation__Frozen(null, 8);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[9] = 
			DataStore[9] = new ObjectClass__Implementation__Frozen(null, 9);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10] = 
			DataStore[10] = new ObjectClass__Implementation__Frozen(null, 10);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[11] = 
			DataStore[11] = new ObjectClass__Implementation__Frozen(null, 11);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[12] = 
			DataStore[12] = new ObjectClass__Implementation__Frozen(null, 12);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[13] = 
			DataStore[13] = new ObjectClass__Implementation__Frozen(null, 13);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14] = 
			DataStore[14] = new ObjectClass__Implementation__Frozen(null, 14);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[15] = 
			DataStore[15] = new ObjectClass__Implementation__Frozen(null, 15);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[16] = 
			DataStore[16] = new ObjectClass__Implementation__Frozen(null, 16);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18] = 
			DataStore[18] = new ObjectClass__Implementation__Frozen(null, 18);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19] = 
			DataStore[19] = new ObjectClass__Implementation__Frozen(null, 19);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20] = 
			DataStore[20] = new ObjectClass__Implementation__Frozen(null, 20);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[21] = 
			DataStore[21] = new ObjectClass__Implementation__Frozen(null, 21);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[23] = 
			DataStore[23] = new ObjectClass__Implementation__Frozen(null, 23);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25] = 
			DataStore[25] = new ObjectClass__Implementation__Frozen(null, 25);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26] = 
			DataStore[26] = new ObjectClass__Implementation__Frozen(null, 26);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27] = 
			DataStore[27] = new ObjectClass__Implementation__Frozen(null, 27);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29] = 
			DataStore[29] = new ObjectClass__Implementation__Frozen(null, 29);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30] = 
			DataStore[30] = new ObjectClass__Implementation__Frozen(null, 30);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31] = 
			DataStore[31] = new ObjectClass__Implementation__Frozen(null, 31);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33] = 
			DataStore[33] = new ObjectClass__Implementation__Frozen(null, 33);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36] = 
			DataStore[36] = new ObjectClass__Implementation__Frozen(null, 36);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[37] = 
			DataStore[37] = new ObjectClass__Implementation__Frozen(null, 37);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[38] = 
			DataStore[38] = new ObjectClass__Implementation__Frozen(null, 38);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[39] = 
			DataStore[39] = new ObjectClass__Implementation__Frozen(null, 39);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[40] = 
			DataStore[40] = new ObjectClass__Implementation__Frozen(null, 40);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[41] = 
			DataStore[41] = new ObjectClass__Implementation__Frozen(null, 41);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42] = 
			DataStore[42] = new ObjectClass__Implementation__Frozen(null, 42);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43] = 
			DataStore[43] = new ObjectClass__Implementation__Frozen(null, 43);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[44] = 
			DataStore[44] = new ObjectClass__Implementation__Frozen(null, 44);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45] = 
			DataStore[45] = new ObjectClass__Implementation__Frozen(null, 45);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46] = 
			DataStore[46] = new ObjectClass__Implementation__Frozen(null, 46);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[47] = 
			DataStore[47] = new ObjectClass__Implementation__Frozen(null, 47);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51] = 
			DataStore[51] = new ObjectClass__Implementation__Frozen(null, 51);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54] = 
			DataStore[54] = new ObjectClass__Implementation__Frozen(null, 54);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58] = 
			DataStore[58] = new ObjectClass__Implementation__Frozen(null, 58);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[59] = 
			DataStore[59] = new ObjectClass__Implementation__Frozen(null, 59);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[60] = 
			DataStore[60] = new ObjectClass__Implementation__Frozen(null, 60);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[61] = 
			DataStore[61] = new ObjectClass__Implementation__Frozen(null, 61);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[62] = 
			DataStore[62] = new ObjectClass__Implementation__Frozen(null, 62);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[64] = 
			DataStore[64] = new ObjectClass__Implementation__Frozen(null, 64);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66] = 
			DataStore[66] = new ObjectClass__Implementation__Frozen(null, 66);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67] = 
			DataStore[67] = new ObjectClass__Implementation__Frozen(null, 67);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68] = 
			DataStore[68] = new ObjectClass__Implementation__Frozen(null, 68);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69] = 
			DataStore[69] = new ObjectClass__Implementation__Frozen(null, 69);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[70] = 
			DataStore[70] = new ObjectClass__Implementation__Frozen(null, 70);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[71] = 
			DataStore[71] = new ObjectClass__Implementation__Frozen(null, 71);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[73] = 
			DataStore[73] = new ObjectClass__Implementation__Frozen(null, 73);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[74] = 
			DataStore[74] = new ObjectClass__Implementation__Frozen(null, 74);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[75] = 
			DataStore[75] = new ObjectClass__Implementation__Frozen(null, 75);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[76] = 
			DataStore[76] = new ObjectClass__Implementation__Frozen(null, 76);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77] = 
			DataStore[77] = new ObjectClass__Implementation__Frozen(null, 77);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79] = 
			DataStore[79] = new ObjectClass__Implementation__Frozen(null, 79);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[80] = 
			DataStore[80] = new ObjectClass__Implementation__Frozen(null, 80);

		}

		internal new static void FillDataStore() {
			DataStore[2].TableName = @"ObjectClasses";
			DataStore[2].IsSimpleObject = false;
			DataStore[2].IsFrozenObject = false;
			DataStore[2].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[10];
			DataStore[3].TableName = @"Projekte";
			DataStore[3].BaseObjectClass = null;
			DataStore[3].IsSimpleObject = false;
			DataStore[3].IsFrozenObject = false;
			DataStore[3].DefaultModel = null;
			DataStore[4].TableName = @"Tasks";
			DataStore[4].BaseObjectClass = null;
			DataStore[4].IsSimpleObject = false;
			DataStore[4].IsFrozenObject = false;
			DataStore[4].DefaultModel = null;
			DataStore[5].TableName = @"BaseProperties";
			DataStore[5].BaseObjectClass = null;
			DataStore[5].IsSimpleObject = false;
			DataStore[5].IsFrozenObject = true;
			DataStore[5].DefaultModel = null;
			DataStore[6].TableName = @"Mitarbeiter";
			DataStore[6].BaseObjectClass = null;
			DataStore[6].IsSimpleObject = false;
			DataStore[6].IsFrozenObject = false;
			DataStore[6].DefaultModel = null;
			DataStore[7].TableName = @"Properties";
			DataStore[7].IsSimpleObject = false;
			DataStore[7].IsFrozenObject = false;
			DataStore[7].DefaultModel = null;
			DataStore[8].TableName = @"ValueTypeProperties";
			DataStore[8].IsSimpleObject = false;
			DataStore[8].IsFrozenObject = false;
			DataStore[8].DefaultModel = null;
			DataStore[9].TableName = @"StringProperties";
			DataStore[9].IsSimpleObject = false;
			DataStore[9].IsFrozenObject = false;
			DataStore[9].DefaultModel = null;
			DataStore[10].TableName = @"Methods";
			DataStore[10].BaseObjectClass = null;
			DataStore[10].IsSimpleObject = false;
			DataStore[10].IsFrozenObject = true;
			DataStore[10].DefaultModel = null;
			DataStore[11].TableName = @"IntProperties";
			DataStore[11].IsSimpleObject = false;
			DataStore[11].IsFrozenObject = false;
			DataStore[11].DefaultModel = null;
			DataStore[12].TableName = @"BoolProperties";
			DataStore[12].IsSimpleObject = false;
			DataStore[12].IsFrozenObject = false;
			DataStore[12].DefaultModel = null;
			DataStore[13].TableName = @"DoubleProperties";
			DataStore[13].IsSimpleObject = false;
			DataStore[13].IsFrozenObject = false;
			DataStore[13].DefaultModel = null;
			DataStore[14].TableName = @"ObjectReferenceProperties";
			DataStore[14].IsSimpleObject = false;
			DataStore[14].IsFrozenObject = false;
			DataStore[14].DefaultModel = null;
			DataStore[15].TableName = @"DateTimeProperties";
			DataStore[15].IsSimpleObject = false;
			DataStore[15].IsFrozenObject = false;
			DataStore[15].DefaultModel = null;
			DataStore[16].TableName = @"BackReferenceProperties";
			DataStore[16].IsSimpleObject = false;
			DataStore[16].IsFrozenObject = false;
			DataStore[16].DefaultModel = null;
			DataStore[18].TableName = @"Modules";
			DataStore[18].BaseObjectClass = null;
			DataStore[18].IsSimpleObject = false;
			DataStore[18].IsFrozenObject = true;
			DataStore[18].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[9];
			DataStore[19].TableName = @"Auftraege";
			DataStore[19].BaseObjectClass = null;
			DataStore[19].IsSimpleObject = false;
			DataStore[19].IsFrozenObject = false;
			DataStore[19].DefaultModel = null;
			DataStore[20].TableName = @"Zeitkonten";
			DataStore[20].BaseObjectClass = null;
			DataStore[20].IsSimpleObject = false;
			DataStore[20].IsFrozenObject = false;
			DataStore[20].DefaultModel = null;
			DataStore[21].TableName = @"Kostenstellen";
			DataStore[21].IsSimpleObject = false;
			DataStore[21].IsFrozenObject = false;
			DataStore[21].DefaultModel = null;
			DataStore[23].TableName = @"Kostentraeger";
			DataStore[23].IsSimpleObject = false;
			DataStore[23].IsFrozenObject = false;
			DataStore[23].DefaultModel = null;
			DataStore[25].TableName = @"Taetigkeiten";
			DataStore[25].BaseObjectClass = null;
			DataStore[25].IsSimpleObject = false;
			DataStore[25].IsFrozenObject = false;
			DataStore[25].DefaultModel = null;
			DataStore[26].TableName = @"Kunden";
			DataStore[26].BaseObjectClass = null;
			DataStore[26].IsSimpleObject = false;
			DataStore[26].IsFrozenObject = false;
			DataStore[26].DefaultModel = null;
			DataStore[27].TableName = @"Icons";
			DataStore[27].BaseObjectClass = null;
			DataStore[27].IsSimpleObject = true;
			DataStore[27].IsFrozenObject = true;
			DataStore[27].DefaultModel = null;
			DataStore[29].TableName = @"Assemblies";
			DataStore[29].BaseObjectClass = null;
			DataStore[29].IsSimpleObject = false;
			DataStore[29].IsFrozenObject = true;
			DataStore[29].DefaultModel = null;
			DataStore[30].TableName = @"MethodInvocations";
			DataStore[30].BaseObjectClass = null;
			DataStore[30].IsSimpleObject = false;
			DataStore[30].IsFrozenObject = true;
			DataStore[30].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[41];
			DataStore[31].TableName = @"TaetigkeitsArten";
			DataStore[31].BaseObjectClass = null;
			DataStore[31].IsSimpleObject = false;
			DataStore[31].IsFrozenObject = false;
			DataStore[31].DefaultModel = null;
			DataStore[33].TableName = @"DataTypes";
			DataStore[33].BaseObjectClass = null;
			DataStore[33].IsSimpleObject = false;
			DataStore[33].IsFrozenObject = true;
			DataStore[33].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[11];
			DataStore[36].TableName = @"BaseParameters";
			DataStore[36].BaseObjectClass = null;
			DataStore[36].IsSimpleObject = false;
			DataStore[36].IsFrozenObject = true;
			DataStore[36].DefaultModel = null;
			DataStore[37].TableName = @"StringParameters";
			DataStore[37].IsSimpleObject = false;
			DataStore[37].IsFrozenObject = false;
			DataStore[37].DefaultModel = null;
			DataStore[38].TableName = @"IntParameters";
			DataStore[38].IsSimpleObject = false;
			DataStore[38].IsFrozenObject = false;
			DataStore[38].DefaultModel = null;
			DataStore[39].TableName = @"DoubleParameters";
			DataStore[39].IsSimpleObject = false;
			DataStore[39].IsFrozenObject = false;
			DataStore[39].DefaultModel = null;
			DataStore[40].TableName = @"BoolParameters";
			DataStore[40].IsSimpleObject = false;
			DataStore[40].IsFrozenObject = false;
			DataStore[40].DefaultModel = null;
			DataStore[41].TableName = @"DateTimeParameters";
			DataStore[41].IsSimpleObject = false;
			DataStore[41].IsFrozenObject = false;
			DataStore[41].DefaultModel = null;
			DataStore[42].TableName = @"ObjectParameters";
			DataStore[42].IsSimpleObject = false;
			DataStore[42].IsFrozenObject = false;
			DataStore[42].DefaultModel = null;
			DataStore[43].TableName = @"CLRObjectParameters";
			DataStore[43].IsSimpleObject = false;
			DataStore[43].IsFrozenObject = false;
			DataStore[43].DefaultModel = null;
			DataStore[44].TableName = @"Interfaces";
			DataStore[44].IsSimpleObject = false;
			DataStore[44].IsFrozenObject = false;
			DataStore[44].DefaultModel = null;
			DataStore[45].TableName = @"Enumerations";
			DataStore[45].IsSimpleObject = false;
			DataStore[45].IsFrozenObject = false;
			DataStore[45].DefaultModel = null;
			DataStore[46].TableName = @"EnumerationEntries";
			DataStore[46].BaseObjectClass = null;
			DataStore[46].IsSimpleObject = true;
			DataStore[46].IsFrozenObject = true;
			DataStore[46].DefaultModel = null;
			DataStore[47].TableName = @"EnumerationProperties";
			DataStore[47].IsSimpleObject = false;
			DataStore[47].IsFrozenObject = false;
			DataStore[47].DefaultModel = null;
			DataStore[51].TableName = @"TestObjClasses";
			DataStore[51].BaseObjectClass = null;
			DataStore[51].IsSimpleObject = false;
			DataStore[51].IsFrozenObject = false;
			DataStore[51].DefaultModel = null;
			DataStore[54].TableName = @"ControlInfos";
			DataStore[54].BaseObjectClass = null;
			DataStore[54].IsSimpleObject = false;
			DataStore[54].IsFrozenObject = false;
			DataStore[54].DefaultModel = null;
			DataStore[58].TableName = @"TestCustomObjects";
			DataStore[58].BaseObjectClass = null;
			DataStore[58].IsSimpleObject = false;
			DataStore[58].IsFrozenObject = false;
			DataStore[58].DefaultModel = null;
			DataStore[59].TableName = @"Muhblas";
			DataStore[59].BaseObjectClass = null;
			DataStore[59].IsSimpleObject = false;
			DataStore[59].IsFrozenObject = false;
			DataStore[59].DefaultModel = null;
			DataStore[60].TableName = @"AnotherTests";
			DataStore[60].BaseObjectClass = null;
			DataStore[60].IsSimpleObject = false;
			DataStore[60].IsFrozenObject = false;
			DataStore[60].DefaultModel = null;
			DataStore[61].TableName = @"LastTests";
			DataStore[61].BaseObjectClass = null;
			DataStore[61].IsSimpleObject = false;
			DataStore[61].IsFrozenObject = false;
			DataStore[61].DefaultModel = null;
			DataStore[62].TableName = @"Structs";
			DataStore[62].IsSimpleObject = false;
			DataStore[62].IsFrozenObject = false;
			DataStore[62].DefaultModel = null;
			DataStore[64].TableName = @"StructProperties";
			DataStore[64].IsSimpleObject = false;
			DataStore[64].IsFrozenObject = false;
			DataStore[64].DefaultModel = null;
			DataStore[66].TableName = @"PresenterInfos";
			DataStore[66].BaseObjectClass = null;
			DataStore[66].IsSimpleObject = false;
			DataStore[66].IsFrozenObject = false;
			DataStore[66].DefaultModel = null;
			DataStore[67].TableName = @"Visuals";
			DataStore[67].BaseObjectClass = null;
			DataStore[67].IsSimpleObject = false;
			DataStore[67].IsFrozenObject = false;
			DataStore[67].DefaultModel = null;
			DataStore[68].TableName = @"Templates";
			DataStore[68].BaseObjectClass = null;
			DataStore[68].IsSimpleObject = false;
			DataStore[68].IsFrozenObject = false;
			DataStore[68].DefaultModel = null;
			DataStore[69].TableName = @"Constraints";
			DataStore[69].BaseObjectClass = null;
			DataStore[69].IsSimpleObject = false;
			DataStore[69].IsFrozenObject = true;
			DataStore[69].DefaultModel = null;
			DataStore[70].TableName = @"NotNullableConstraints";
			DataStore[70].IsSimpleObject = false;
			DataStore[70].IsFrozenObject = false;
			DataStore[70].DefaultModel = null;
			DataStore[71].TableName = @"IntegerRangeConstraints";
			DataStore[71].IsSimpleObject = false;
			DataStore[71].IsFrozenObject = false;
			DataStore[71].DefaultModel = null;
			DataStore[73].TableName = @"StringRangeConstraints";
			DataStore[73].IsSimpleObject = false;
			DataStore[73].IsFrozenObject = false;
			DataStore[73].DefaultModel = null;
			DataStore[74].TableName = @"MethodInvocationConstraints";
			DataStore[74].IsSimpleObject = false;
			DataStore[74].IsFrozenObject = false;
			DataStore[74].DefaultModel = null;
			DataStore[75].TableName = @"IsValidIdentifierConstraints";
			DataStore[75].IsSimpleObject = false;
			DataStore[75].IsFrozenObject = false;
			DataStore[75].DefaultModel = null;
			DataStore[76].TableName = @"IsValidNamespaceConstraints";
			DataStore[76].IsSimpleObject = false;
			DataStore[76].IsFrozenObject = false;
			DataStore[76].DefaultModel = null;
			DataStore[77].TableName = @"Relations";
			DataStore[77].BaseObjectClass = null;
			DataStore[77].IsSimpleObject = false;
			DataStore[77].IsFrozenObject = true;
			DataStore[77].DefaultModel = null;
			DataStore[79].TableName = @"TypeRefs";
			DataStore[79].BaseObjectClass = null;
			DataStore[79].IsSimpleObject = false;
			DataStore[79].IsFrozenObject = true;
			DataStore[79].DefaultModel = null;
			DataStore[80].TableName = @"ViewDescriptors";
			DataStore[80].BaseObjectClass = null;
			DataStore[80].IsSimpleObject = false;
			DataStore[80].IsFrozenObject = true;
			DataStore[80].DefaultModel = null;
	
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