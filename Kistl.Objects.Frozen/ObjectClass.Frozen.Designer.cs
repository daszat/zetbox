
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
    
		public ObjectClass__Implementation__Frozen()
		{
        }


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
					var __oldValue = _BaseObjectClass;
                    NotifyPropertyChanging("BaseObjectClass", __oldValue, value);
                    _BaseObjectClass = value;
                    NotifyPropertyChanged("BaseObjectClass", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.ObjectClass _BaseObjectClass;

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
					var __oldValue = _DefaultModel;
                    NotifyPropertyChanging("DefaultModel", __oldValue, value);
                    _DefaultModel = value;
                    NotifyPropertyChanged("DefaultModel", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _DefaultModel;

        /// <summary>
        /// The default PresentableModel to use for this ObjectClass
        /// </summary>
        // object reference property
        public virtual Kistl.App.GUI.PresentableModelDescriptor DefaultPresentableModelDescriptor
        {
            get
            {
                return _DefaultPresentableModelDescriptor;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultPresentableModelDescriptor != value)
                {
					var __oldValue = _DefaultPresentableModelDescriptor;
                    NotifyPropertyChanging("DefaultPresentableModelDescriptor", __oldValue, value);
                    _DefaultPresentableModelDescriptor = value;
                    NotifyPropertyChanged("DefaultPresentableModelDescriptor", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.PresentableModelDescriptor _DefaultPresentableModelDescriptor;

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
        // collection reference property
        public virtual ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
        {
            get
            {
                if (_ImplementsInterfaces == null)
                    _ImplementsInterfaces = new ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0));
                return _ImplementsInterfaces;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _ImplementsInterfaces = (ReadOnlyCollection<Kistl.App.Base.Interface>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.Interface> _ImplementsInterfaces;

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
					var __oldValue = _IsFrozenObject;
                    NotifyPropertyChanging("IsFrozenObject", __oldValue, value);
                    _IsFrozenObject = value;
                    NotifyPropertyChanged("IsFrozenObject", __oldValue, value);
                }
            }
        }
        private bool _IsFrozenObject;

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
					var __oldValue = _IsSimpleObject;
                    NotifyPropertyChanging("IsSimpleObject", __oldValue, value);
                    _IsSimpleObject = value;
                    NotifyPropertyChanged("IsSimpleObject", __oldValue, value);
                }
            }
        }
        private bool _IsSimpleObject;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
        // object list property
        public virtual ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                if (_SubClasses == null)
                    _SubClasses = new ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0));
                return _SubClasses;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _SubClasses = (ReadOnlyCollection<Kistl.App.Base.ObjectClass>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.ObjectClass> _SubClasses;

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
					var __oldValue = _TableName;
                    NotifyPropertyChanging("TableName", __oldValue, value);
                    _TableName = value;
                    NotifyPropertyChanged("TableName", __oldValue, value);
                }
            }
        }
        private string _TableName;

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_ObjectClass != null)
            {
                OnGetDataType_ObjectClass(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
		public event GetDataType_Handler<ObjectClass> OnGetDataType_ObjectClass;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_ObjectClass != null)
            {
                OnGetDataTypeString_ObjectClass(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual IList<Kistl.App.Base.Method> GetInheritedMethods() 
        {
            var e = new MethodReturnEventArgs<IList<Kistl.App.Base.Method>>();
            if (OnGetInheritedMethods_ObjectClass != null)
            {
                OnGetInheritedMethods_ObjectClass(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ObjectClass.GetInheritedMethods");
            }
            return e.Result;
        }
		public delegate void GetInheritedMethods_Handler<T>(T obj, MethodReturnEventArgs<IList<Kistl.App.Base.Method>> ret);
		public event GetInheritedMethods_Handler<ObjectClass> OnGetInheritedMethods_ObjectClass;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectClass));
		}

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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "BaseObjectClass":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(25).Constraints
						.Where(c => !c.IsValid(this, this.BaseObjectClass))
						.Select(c => c.GetErrorText(this, this.BaseObjectClass))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DefaultModel":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(212).Constraints
						.Where(c => !c.IsValid(this, this.DefaultModel))
						.Select(c => c.GetErrorText(this, this.DefaultModel))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DefaultPresentableModelDescriptor":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(234).Constraints
						.Where(c => !c.IsValid(this, this.DefaultPresentableModelDescriptor))
						.Select(c => c.GetErrorText(this, this.DefaultPresentableModelDescriptor))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ImplementsInterfaces":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(105).Constraints
						.Where(c => !c.IsValid(this, this.ImplementsInterfaces))
						.Select(c => c.GetErrorText(this, this.ImplementsInterfaces))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsFrozenObject":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(174).Constraints
						.Where(c => !c.IsValid(this, this.IsFrozenObject))
						.Select(c => c.GetErrorText(this, this.IsFrozenObject))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsSimpleObject":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(119).Constraints
						.Where(c => !c.IsValid(this, this.IsSimpleObject))
						.Select(c => c.GetErrorText(this, this.IsSimpleObject))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "SubClasses":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(27).Constraints
						.Where(c => !c.IsValid(this, this.SubClasses))
						.Select(c => c.GetErrorText(this, this.SubClasses))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "TableName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(3).Constraints
						.Where(c => !c.IsValid(this, this.TableName))
						.Select(c => c.GetErrorText(this, this.TableName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
        internal ObjectClass__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, ObjectClass__Implementation__Frozen> DataStore = new Dictionary<int, ObjectClass__Implementation__Frozen>(59);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2] = 
			DataStore[2] = new ObjectClass__Implementation__Frozen(2);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3] = 
			DataStore[3] = new ObjectClass__Implementation__Frozen(3);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4] = 
			DataStore[4] = new ObjectClass__Implementation__Frozen(4);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6] = 
			DataStore[6] = new ObjectClass__Implementation__Frozen(6);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7] = 
			DataStore[7] = new ObjectClass__Implementation__Frozen(7);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[8] = 
			DataStore[8] = new ObjectClass__Implementation__Frozen(8);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[9] = 
			DataStore[9] = new ObjectClass__Implementation__Frozen(9);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10] = 
			DataStore[10] = new ObjectClass__Implementation__Frozen(10);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[11] = 
			DataStore[11] = new ObjectClass__Implementation__Frozen(11);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[12] = 
			DataStore[12] = new ObjectClass__Implementation__Frozen(12);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[13] = 
			DataStore[13] = new ObjectClass__Implementation__Frozen(13);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14] = 
			DataStore[14] = new ObjectClass__Implementation__Frozen(14);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[15] = 
			DataStore[15] = new ObjectClass__Implementation__Frozen(15);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18] = 
			DataStore[18] = new ObjectClass__Implementation__Frozen(18);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19] = 
			DataStore[19] = new ObjectClass__Implementation__Frozen(19);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20] = 
			DataStore[20] = new ObjectClass__Implementation__Frozen(20);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[21] = 
			DataStore[21] = new ObjectClass__Implementation__Frozen(21);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[23] = 
			DataStore[23] = new ObjectClass__Implementation__Frozen(23);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26] = 
			DataStore[26] = new ObjectClass__Implementation__Frozen(26);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27] = 
			DataStore[27] = new ObjectClass__Implementation__Frozen(27);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29] = 
			DataStore[29] = new ObjectClass__Implementation__Frozen(29);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30] = 
			DataStore[30] = new ObjectClass__Implementation__Frozen(30);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31] = 
			DataStore[31] = new ObjectClass__Implementation__Frozen(31);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33] = 
			DataStore[33] = new ObjectClass__Implementation__Frozen(33);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36] = 
			DataStore[36] = new ObjectClass__Implementation__Frozen(36);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[37] = 
			DataStore[37] = new ObjectClass__Implementation__Frozen(37);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[38] = 
			DataStore[38] = new ObjectClass__Implementation__Frozen(38);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[39] = 
			DataStore[39] = new ObjectClass__Implementation__Frozen(39);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[40] = 
			DataStore[40] = new ObjectClass__Implementation__Frozen(40);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[41] = 
			DataStore[41] = new ObjectClass__Implementation__Frozen(41);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42] = 
			DataStore[42] = new ObjectClass__Implementation__Frozen(42);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43] = 
			DataStore[43] = new ObjectClass__Implementation__Frozen(43);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[44] = 
			DataStore[44] = new ObjectClass__Implementation__Frozen(44);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45] = 
			DataStore[45] = new ObjectClass__Implementation__Frozen(45);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46] = 
			DataStore[46] = new ObjectClass__Implementation__Frozen(46);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[47] = 
			DataStore[47] = new ObjectClass__Implementation__Frozen(47);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51] = 
			DataStore[51] = new ObjectClass__Implementation__Frozen(51);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54] = 
			DataStore[54] = new ObjectClass__Implementation__Frozen(54);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58] = 
			DataStore[58] = new ObjectClass__Implementation__Frozen(58);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[59] = 
			DataStore[59] = new ObjectClass__Implementation__Frozen(59);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[60] = 
			DataStore[60] = new ObjectClass__Implementation__Frozen(60);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[61] = 
			DataStore[61] = new ObjectClass__Implementation__Frozen(61);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[62] = 
			DataStore[62] = new ObjectClass__Implementation__Frozen(62);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[64] = 
			DataStore[64] = new ObjectClass__Implementation__Frozen(64);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66] = 
			DataStore[66] = new ObjectClass__Implementation__Frozen(66);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67] = 
			DataStore[67] = new ObjectClass__Implementation__Frozen(67);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68] = 
			DataStore[68] = new ObjectClass__Implementation__Frozen(68);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69] = 
			DataStore[69] = new ObjectClass__Implementation__Frozen(69);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[70] = 
			DataStore[70] = new ObjectClass__Implementation__Frozen(70);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[71] = 
			DataStore[71] = new ObjectClass__Implementation__Frozen(71);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[73] = 
			DataStore[73] = new ObjectClass__Implementation__Frozen(73);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[74] = 
			DataStore[74] = new ObjectClass__Implementation__Frozen(74);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[75] = 
			DataStore[75] = new ObjectClass__Implementation__Frozen(75);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[76] = 
			DataStore[76] = new ObjectClass__Implementation__Frozen(76);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77] = 
			DataStore[77] = new ObjectClass__Implementation__Frozen(77);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79] = 
			DataStore[79] = new ObjectClass__Implementation__Frozen(79);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82] = 
			DataStore[82] = new ObjectClass__Implementation__Frozen(82);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[83] = 
			DataStore[83] = new ObjectClass__Implementation__Frozen(83);

			Kistl.App.Base.DataType__Implementation__Frozen.DataStore[85] = 
			DataStore[85] = new ObjectClass__Implementation__Frozen(85);

		}

		internal new static void FillDataStore() {
			DataStore[2].ClassName = @"ObjectClass";
			DataStore[2].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[11];
			DataStore[2].Description = @"Metadefinition Object for ObjectClasses.";
			DataStore[2].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[28],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[81],
});
			DataStore[2].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[4],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[5],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[6],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[125],
});
			DataStore[2].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[2].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(8) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[27],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[25],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[3],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[105],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[119],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[174],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[212],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[234],
});
			DataStore[2].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[33];
			DataStore[2].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[10];
			DataStore[2].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[33];
			DataStore[2].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[2].IsFrozenObject = false;
			DataStore[2].IsSimpleObject = false;
			DataStore[2].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[2].TableName = @"ObjectClasses";
			DataStore[2].Seal();
			DataStore[3].ClassName = @"Projekt";
			DataStore[3].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[3];
			DataStore[3].Description = null;
			DataStore[3].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[1],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[29],
});
			DataStore[3].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[7],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[8],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[9],
});
			DataStore[3].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[3].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(7) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[14],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[22],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[67],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[66],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[13],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[23],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[48],
});
			DataStore[3].BaseObjectClass = null;
			DataStore[3].DefaultModel = null;
			DataStore[3].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[3].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[3].IsFrozenObject = false;
			DataStore[3].IsSimpleObject = false;
			DataStore[3].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[3].TableName = @"Projekte";
			DataStore[3].Seal();
			DataStore[4].ClassName = @"Task";
			DataStore[4].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[7];
			DataStore[4].Description = null;
			DataStore[4].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[4],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[30],
});
			DataStore[4].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[10],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[11],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[12],
});
			DataStore[4].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[4].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[19],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[15],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[16],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[17],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[18],
});
			DataStore[4].BaseObjectClass = null;
			DataStore[4].DefaultModel = null;
			DataStore[4].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[4].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[4].IsFrozenObject = false;
			DataStore[4].IsSimpleObject = false;
			DataStore[4].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[4].TableName = @"Tasks";
			DataStore[4].Seal();
			DataStore[6].ClassName = @"Mitarbeiter";
			DataStore[6].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[5];
			DataStore[6].Description = null;
			DataStore[6].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[2],
});
			DataStore[6].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[16],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[17],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[18],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[83],
});
			DataStore[6].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[6].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[21],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[20],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[38],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[39],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[40],
});
			DataStore[6].BaseObjectClass = null;
			DataStore[6].DefaultModel = null;
			DataStore[6].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[6].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[6].IsFrozenObject = false;
			DataStore[6].IsSimpleObject = false;
			DataStore[6].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[6].TableName = @"Mitarbeiter";
			DataStore[6].Seal();
			DataStore[7].ClassName = @"Property";
			DataStore[7].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[7].Description = @"Metadefinition Object for Properties. This class is abstract.";
			DataStore[7].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(5) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[115],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[116],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[119],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[117],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[118],
});
			DataStore[7].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(5) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[1],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[13],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[14],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[15],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[118],
});
			DataStore[7].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[7].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(11) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[8],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[72],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[9],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[11],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[26],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[41],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[170],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[176],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[204],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[225],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[236],
});
			DataStore[7].BaseObjectClass = null;
			DataStore[7].DefaultModel = null;
			DataStore[7].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[7].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[7].IsFrozenObject = true;
			DataStore[7].IsSimpleObject = false;
			DataStore[7].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(3) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[14],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[64],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8],
});
			DataStore[7].TableName = @"Properties";
			DataStore[7].Seal();
			DataStore[8].ClassName = @"ValueTypeProperty";
			DataStore[8].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[8].Description = @"Metadefinition Object for ValueType Properties. This class is abstract.";
			DataStore[8].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[8].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[8].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[8].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[8].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[7];
			DataStore[8].DefaultModel = null;
			DataStore[8].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[8].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[8].IsFrozenObject = false;
			DataStore[8].IsSimpleObject = false;
			DataStore[8].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(6) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[9],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[11],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[47],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[13],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[15],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[12],
});
			DataStore[8].TableName = @"ValueTypeProperties";
			DataStore[8].Seal();
			DataStore[9].ClassName = @"StringProperty";
			DataStore[9].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[9].Description = @"Metadefinition Object for String Properties.";
			DataStore[9].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[20],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[31],
});
			DataStore[9].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[9].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[9].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[28],
});
			DataStore[9].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8];
			DataStore[9].DefaultModel = null;
			DataStore[9].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[9].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[9].IsFrozenObject = false;
			DataStore[9].IsSimpleObject = false;
			DataStore[9].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[9].TableName = @"StringProperties";
			DataStore[9].Seal();
			DataStore[10].ClassName = @"Method";
			DataStore[10].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[2];
			DataStore[10].Description = @"Metadefinition Object for Methods.";
			DataStore[10].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[9],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[63],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[80],
});
			DataStore[10].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[19],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[20],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[21],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[124],
});
			DataStore[10].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[10].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(7) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[29],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[73],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[81],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[96],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[30],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[124],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[180],
});
			DataStore[10].BaseObjectClass = null;
			DataStore[10].DefaultModel = null;
			DataStore[10].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[10].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[10].IsFrozenObject = true;
			DataStore[10].IsSimpleObject = false;
			DataStore[10].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[10].TableName = @"Methods";
			DataStore[10].Seal();
			DataStore[11].ClassName = @"IntProperty";
			DataStore[11].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[11].Description = @"Metadefinition Object for Int Properties.";
			DataStore[11].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[21],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[32],
});
			DataStore[11].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[11].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[11].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[11].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8];
			DataStore[11].DefaultModel = null;
			DataStore[11].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[11].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[11].IsFrozenObject = false;
			DataStore[11].IsSimpleObject = false;
			DataStore[11].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[11].TableName = @"IntProperties";
			DataStore[11].Seal();
			DataStore[12].ClassName = @"BoolProperty";
			DataStore[12].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[12].Description = @"Metadefinition Object for Bool Properties.";
			DataStore[12].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[22],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[33],
});
			DataStore[12].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[12].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[12].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[12].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8];
			DataStore[12].DefaultModel = null;
			DataStore[12].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[12].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[12].IsFrozenObject = false;
			DataStore[12].IsSimpleObject = false;
			DataStore[12].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[12].TableName = @"BoolProperties";
			DataStore[12].Seal();
			DataStore[13].ClassName = @"DoubleProperty";
			DataStore[13].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[13].Description = @"Metadefinition Object for Double Properties.";
			DataStore[13].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[23],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[34],
});
			DataStore[13].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[13].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[13].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[13].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8];
			DataStore[13].DefaultModel = null;
			DataStore[13].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[13].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[13].IsFrozenObject = false;
			DataStore[13].IsSimpleObject = false;
			DataStore[13].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[13].TableName = @"DoubleProperties";
			DataStore[13].Seal();
			DataStore[14].ClassName = @"ObjectReferenceProperty";
			DataStore[14].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[14].Description = @"Metadefinition Object for ObjectReference Properties.";
			DataStore[14].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[26],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[37],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[17],
});
			DataStore[14].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[14].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[14].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(2) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[46],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[222],
});
			DataStore[14].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[7];
			DataStore[14].DefaultModel = null;
			DataStore[14].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[14].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[14].IsFrozenObject = false;
			DataStore[14].IsSimpleObject = false;
			DataStore[14].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[14].TableName = @"ObjectReferenceProperties";
			DataStore[14].Seal();
			DataStore[15].ClassName = @"DateTimeProperty";
			DataStore[15].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[15].Description = @"Metadefinition Object for DateTime Properties.";
			DataStore[15].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[24],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[35],
});
			DataStore[15].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[15].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[15].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[15].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8];
			DataStore[15].DefaultModel = null;
			DataStore[15].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[15].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[15].IsFrozenObject = false;
			DataStore[15].IsSimpleObject = false;
			DataStore[15].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[15].TableName = @"DateTimeProperties";
			DataStore[15].Seal();
			DataStore[18].ClassName = @"Module";
			DataStore[18].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[1];
			DataStore[18].Description = @"Metadefinition Object for Modules.";
			DataStore[18].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[10],
});
			DataStore[18].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[22],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[23],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[24],
});
			DataStore[18].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[18].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[44],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[82],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[42],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[43],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[179],
});
			DataStore[18].BaseObjectClass = null;
			DataStore[18].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[9];
			DataStore[18].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[32];
			DataStore[18].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[18].IsFrozenObject = true;
			DataStore[18].IsSimpleObject = false;
			DataStore[18].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[18].TableName = @"Modules";
			DataStore[18].Seal();
			DataStore[19].ClassName = @"Auftrag";
			DataStore[19].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[8];
			DataStore[19].Description = null;
			DataStore[19].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[19],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[11],
});
			DataStore[19].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[3],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[25],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[26],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[27],
});
			DataStore[19].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[19].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[49],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[50],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[51],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[64],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[65],
});
			DataStore[19].BaseObjectClass = null;
			DataStore[19].DefaultModel = null;
			DataStore[19].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[19].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[19].IsFrozenObject = false;
			DataStore[19].IsSimpleObject = false;
			DataStore[19].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[19].TableName = @"Auftraege";
			DataStore[19].Seal();
			DataStore[20].ClassName = @"Zeitkonto";
			DataStore[20].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[20].Description = @"en:TimeAccount; Ein Konto für die Leistungserfassung. Es können nicht mehr als MaxStunden auf ein Konto gebucht werden.";
			DataStore[20].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[12],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[45],
});
			DataStore[20].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[28],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[29],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[30],
});
			DataStore[20].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[20].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[86],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[52],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[89],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[90],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[237],
});
			DataStore[20].BaseObjectClass = null;
			DataStore[20].DefaultModel = null;
			DataStore[20].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[20].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[20].IsFrozenObject = false;
			DataStore[20].IsSimpleObject = false;
			DataStore[20].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(2) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[23],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[21],
});
			DataStore[20].TableName = @"Zeitkonten";
			DataStore[20].Seal();
			DataStore[21].ClassName = @"Kostenstelle";
			DataStore[21].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[10];
			DataStore[21].Description = null;
			DataStore[21].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[21].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[21].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[21].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[21].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[20];
			DataStore[21].DefaultModel = null;
			DataStore[21].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[21].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[21].IsFrozenObject = false;
			DataStore[21].IsSimpleObject = false;
			DataStore[21].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[21].TableName = @"Kostenstellen";
			DataStore[21].Seal();
			DataStore[23].ClassName = @"Kostentraeger";
			DataStore[23].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[3];
			DataStore[23].Description = null;
			DataStore[23].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[23].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[23].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[23].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[53],
});
			DataStore[23].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[20];
			DataStore[23].DefaultModel = null;
			DataStore[23].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[23].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[23].IsFrozenObject = false;
			DataStore[23].IsSimpleObject = false;
			DataStore[23].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[23].TableName = @"Kostentraeger";
			DataStore[23].Seal();
			DataStore[26].ClassName = @"Kunde";
			DataStore[26].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[6];
			DataStore[26].Description = null;
			DataStore[26].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[14],
});
			DataStore[26].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[34],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[35],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[36],
});
			DataStore[26].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[2];
			DataStore[26].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(6) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[59],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[60],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[61],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[62],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[63],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[85],
});
			DataStore[26].BaseObjectClass = null;
			DataStore[26].DefaultModel = null;
			DataStore[26].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[26].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[26].IsFrozenObject = false;
			DataStore[26].IsSimpleObject = false;
			DataStore[26].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[26].TableName = @"Kunden";
			DataStore[26].Seal();
			DataStore[27].ClassName = @"Icon";
			DataStore[27].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[4];
			DataStore[27].Description = null;
			DataStore[27].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[15],
});
			DataStore[27].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[37],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[38],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[39],
});
			DataStore[27].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[27].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[68],
});
			DataStore[27].BaseObjectClass = null;
			DataStore[27].DefaultModel = null;
			DataStore[27].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[27].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[27].IsFrozenObject = true;
			DataStore[27].IsSimpleObject = true;
			DataStore[27].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[27].TableName = @"Icons";
			DataStore[27].Seal();
			DataStore[29].ClassName = @"Assembly";
			DataStore[29].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[13];
			DataStore[29].Description = null;
			DataStore[29].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[16],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[111],
});
			DataStore[29].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[40],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[41],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[42],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[155],
});
			DataStore[29].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[29].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(3) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[70],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[71],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[83],
});
			DataStore[29].BaseObjectClass = null;
			DataStore[29].DefaultModel = null;
			DataStore[29].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[29].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[29].IsFrozenObject = true;
			DataStore[29].IsSimpleObject = false;
			DataStore[29].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[29].TableName = @"Assemblies";
			DataStore[29].Seal();
			DataStore[30].ClassName = @"MethodInvocation";
			DataStore[30].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[2];
			DataStore[30].Description = @"Metadefinition Object for a MethodInvocation on a Method of a DataType.";
			DataStore[30].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[6],
});
			DataStore[30].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[43],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[44],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[45],
});
			DataStore[30].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[30].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[74],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[78],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[79],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[77],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[208],
});
			DataStore[30].BaseObjectClass = null;
			DataStore[30].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[41];
			DataStore[30].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[31];
			DataStore[30].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[30].IsFrozenObject = true;
			DataStore[30].IsSimpleObject = false;
			DataStore[30].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[30].TableName = @"MethodInvocations";
			DataStore[30].Seal();
			DataStore[31].ClassName = @"TaetigkeitsArt";
			DataStore[31].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[8];
			DataStore[31].Description = null;
			DataStore[31].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[44],
});
			DataStore[31].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[71],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[72],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[73],
});
			DataStore[31].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[3];
			DataStore[31].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[87],
});
			DataStore[31].BaseObjectClass = null;
			DataStore[31].DefaultModel = null;
			DataStore[31].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[31].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[31].IsFrozenObject = false;
			DataStore[31].IsSimpleObject = false;
			DataStore[31].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[31].TableName = @"TaetigkeitsArten";
			DataStore[31].Seal();
			DataStore[33].ClassName = @"DataType";
			DataStore[33].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[11];
			DataStore[33].Description = @"Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.";
			DataStore[33].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(5) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[5],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[74],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[75],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[72],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[73],
});
			DataStore[33].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(5) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[74],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[75],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[76],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[120],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[121],
});
			DataStore[33].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[33].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(7) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[1],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[7],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[31],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[45],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[69],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[80],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[175],
});
			DataStore[33].BaseObjectClass = null;
			DataStore[33].DefaultModel = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[11];
			DataStore[33].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[30];
			DataStore[33].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[33].IsFrozenObject = true;
			DataStore[33].IsSimpleObject = false;
			DataStore[33].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(4) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[45],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[44],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[2],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[62],
});
			DataStore[33].TableName = @"DataTypes";
			DataStore[33].Seal();
			DataStore[36].ClassName = @"BaseParameter";
			DataStore[36].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[36].Description = @"Metadefinition Object for Parameter. This class is abstract.";
			DataStore[36].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(4) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[62],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[49],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[76],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[77],
});
			DataStore[36].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(5) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[79],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[80],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[81],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[82],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[123],
});
			DataStore[36].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[36].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[91],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[92],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[94],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[95],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[177],
});
			DataStore[36].BaseObjectClass = null;
			DataStore[36].DefaultModel = null;
			DataStore[36].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[36].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[36].IsFrozenObject = true;
			DataStore[36].IsSimpleObject = false;
			DataStore[36].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(7) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[40],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[43],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[41],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[39],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[38],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[42],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[37],
});
			DataStore[36].TableName = @"BaseParameters";
			DataStore[36].Seal();
			DataStore[37].ClassName = @"StringParameter";
			DataStore[37].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[37].Description = @"Metadefinition Object for String Parameter.";
			DataStore[37].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[47],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[50],
});
			DataStore[37].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[37].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[37].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[37].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[37].DefaultModel = null;
			DataStore[37].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[37].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[37].IsFrozenObject = false;
			DataStore[37].IsSimpleObject = false;
			DataStore[37].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[37].TableName = @"StringParameters";
			DataStore[37].Seal();
			DataStore[38].ClassName = @"IntParameter";
			DataStore[38].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[38].Description = @"Metadefinition Object for Int Parameter.";
			DataStore[38].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[48],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[51],
});
			DataStore[38].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[38].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[38].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[38].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[38].DefaultModel = null;
			DataStore[38].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[38].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[38].IsFrozenObject = false;
			DataStore[38].IsSimpleObject = false;
			DataStore[38].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[38].TableName = @"IntParameters";
			DataStore[38].Seal();
			DataStore[39].ClassName = @"DoubleParameter";
			DataStore[39].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[39].Description = @"Metadefinition Object for Double Parameter.";
			DataStore[39].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[52],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[53],
});
			DataStore[39].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[39].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[39].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[39].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[39].DefaultModel = null;
			DataStore[39].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[39].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[39].IsFrozenObject = false;
			DataStore[39].IsSimpleObject = false;
			DataStore[39].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[39].TableName = @"DoubleParameters";
			DataStore[39].Seal();
			DataStore[40].ClassName = @"BoolParameter";
			DataStore[40].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[40].Description = @"Metadefinition Object for Bool Parameter.";
			DataStore[40].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[55],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[56],
});
			DataStore[40].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[40].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[40].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[40].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[40].DefaultModel = null;
			DataStore[40].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[40].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[40].IsFrozenObject = false;
			DataStore[40].IsSimpleObject = false;
			DataStore[40].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[40].TableName = @"BoolParameters";
			DataStore[40].Seal();
			DataStore[41].ClassName = @"DateTimeParameter";
			DataStore[41].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[41].Description = @"Metadefinition Object for DateTime Parameter.";
			DataStore[41].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[54],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[57],
});
			DataStore[41].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[41].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[41].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[41].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[41].DefaultModel = null;
			DataStore[41].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[41].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[41].IsFrozenObject = false;
			DataStore[41].IsSimpleObject = false;
			DataStore[41].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[41].TableName = @"DateTimeParameters";
			DataStore[41].Seal();
			DataStore[42].ClassName = @"ObjectParameter";
			DataStore[42].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[42].Description = @"Metadefinition Object for Object Parameter.";
			DataStore[42].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(4) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[58],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[59],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[78],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[79],
});
			DataStore[42].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[42].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[42].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[97],
});
			DataStore[42].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[42].DefaultModel = null;
			DataStore[42].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[42].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[42].IsFrozenObject = false;
			DataStore[42].IsSimpleObject = false;
			DataStore[42].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[42].TableName = @"ObjectParameters";
			DataStore[42].Seal();
			DataStore[43].ClassName = @"CLRObjectParameter";
			DataStore[43].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[12];
			DataStore[43].Description = @"Metadefinition Object for CLR Object Parameter.";
			DataStore[43].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[60],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[61],
});
			DataStore[43].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[43].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[43].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(2) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[98],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[99],
});
			DataStore[43].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[36];
			DataStore[43].DefaultModel = null;
			DataStore[43].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[43].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[43].IsFrozenObject = false;
			DataStore[43].IsSimpleObject = false;
			DataStore[43].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[43].TableName = @"CLRObjectParameters";
			DataStore[43].Seal();
			DataStore[44].ClassName = @"Interface";
			DataStore[44].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[11];
			DataStore[44].Description = @"Metadefinition Object for Interfaces.";
			DataStore[44].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[44].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[44].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[44].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[44].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[33];
			DataStore[44].DefaultModel = null;
			DataStore[44].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[44].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[44].IsFrozenObject = false;
			DataStore[44].IsSimpleObject = false;
			DataStore[44].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[44].TableName = @"Interfaces";
			DataStore[44].Seal();
			DataStore[45].ClassName = @"Enumeration";
			DataStore[45].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[8];
			DataStore[45].Description = @"Metadefinition Object for Enumerations.";
			DataStore[45].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[66],
});
			DataStore[45].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[84],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[85],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[86],
});
			DataStore[45].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[45].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[103],
});
			DataStore[45].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[33];
			DataStore[45].DefaultModel = null;
			DataStore[45].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[45].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[45].IsFrozenObject = false;
			DataStore[45].IsSimpleObject = false;
			DataStore[45].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[45].TableName = @"Enumerations";
			DataStore[45].Seal();
			DataStore[46].ClassName = @"EnumerationEntry";
			DataStore[46].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[8];
			DataStore[46].Description = @"Metadefinition Object for an Enumeration Entry.";
			DataStore[46].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[67],
});
			DataStore[46].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[87],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[88],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[89],
});
			DataStore[46].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[46].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(4) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[100],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[135],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[136],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[178],
});
			DataStore[46].BaseObjectClass = null;
			DataStore[46].DefaultModel = null;
			DataStore[46].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[46].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[46].IsFrozenObject = true;
			DataStore[46].IsSimpleObject = true;
			DataStore[46].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[46].TableName = @"EnumerationEntries";
			DataStore[46].Seal();
			DataStore[47].ClassName = @"EnumerationProperty";
			DataStore[47].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[47].Description = @"Metadefinition Object for Enumeration Properties.";
			DataStore[47].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[64],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[65],
});
			DataStore[47].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[47].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[47].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[104],
});
			DataStore[47].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[8];
			DataStore[47].DefaultModel = null;
			DataStore[47].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[47].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[47].IsFrozenObject = false;
			DataStore[47].IsSimpleObject = false;
			DataStore[47].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[47].TableName = @"EnumerationProperties";
			DataStore[47].Seal();
			DataStore[51].ClassName = @"TestObjClass";
			DataStore[51].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[5];
			DataStore[51].Description = null;
			DataStore[51].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[51].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[91],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[92],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[93],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[95],
});
			DataStore[51].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[51].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(4) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[112],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[109],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[110],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[126],
});
			DataStore[51].BaseObjectClass = null;
			DataStore[51].DefaultModel = null;
			DataStore[51].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[51].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(1) {
Kistl.App.Base.Interface__Implementation__Frozen.DataStore[48],
});
			DataStore[51].IsFrozenObject = false;
			DataStore[51].IsSimpleObject = false;
			DataStore[51].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[51].TableName = @"TestObjClasses";
			DataStore[51].Seal();
			DataStore[54].ClassName = @"ControlInfo";
			DataStore[54].DefaultIcon = null;
			DataStore[54].Description = null;
			DataStore[54].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[94],
});
			DataStore[54].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[141],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[142],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[143],
});
			DataStore[54].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[54].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[114],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[115],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[116],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[117],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[118],
});
			DataStore[54].BaseObjectClass = null;
			DataStore[54].DefaultModel = null;
			DataStore[54].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[54].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[54].IsFrozenObject = false;
			DataStore[54].IsSimpleObject = false;
			DataStore[54].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[54].TableName = @"ControlInfos";
			DataStore[54].Seal();
			DataStore[58].ClassName = @"TestCustomObject";
			DataStore[58].DefaultIcon = null;
			DataStore[58].Description = null;
			DataStore[58].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[58].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[106],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[107],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[108],
});
			DataStore[58].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[58].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(4) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[130],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[131],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[132],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[133],
});
			DataStore[58].BaseObjectClass = null;
			DataStore[58].DefaultModel = null;
			DataStore[58].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[58].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[58].IsFrozenObject = false;
			DataStore[58].IsSimpleObject = false;
			DataStore[58].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[58].TableName = @"TestCustomObjects";
			DataStore[58].Seal();
			DataStore[59].ClassName = @"Muhblah";
			DataStore[59].DefaultIcon = null;
			DataStore[59].Description = null;
			DataStore[59].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[59].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[109],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[110],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[111],
});
			DataStore[59].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[59].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[59].BaseObjectClass = null;
			DataStore[59].DefaultModel = null;
			DataStore[59].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[59].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[59].IsFrozenObject = false;
			DataStore[59].IsSimpleObject = false;
			DataStore[59].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[59].TableName = @"Muhblas";
			DataStore[59].Seal();
			DataStore[60].ClassName = @"AnotherTest";
			DataStore[60].DefaultIcon = null;
			DataStore[60].Description = null;
			DataStore[60].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[60].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[112],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[113],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[114],
});
			DataStore[60].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[60].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[60].BaseObjectClass = null;
			DataStore[60].DefaultModel = null;
			DataStore[60].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[60].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[60].IsFrozenObject = false;
			DataStore[60].IsSimpleObject = false;
			DataStore[60].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[60].TableName = @"AnotherTests";
			DataStore[60].Seal();
			DataStore[61].ClassName = @"LastTest";
			DataStore[61].DefaultIcon = null;
			DataStore[61].Description = null;
			DataStore[61].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[61].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[115],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[116],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[117],
});
			DataStore[61].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[5];
			DataStore[61].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[61].BaseObjectClass = null;
			DataStore[61].DefaultModel = null;
			DataStore[61].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[61].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[61].IsFrozenObject = false;
			DataStore[61].IsSimpleObject = false;
			DataStore[61].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[61].TableName = @"LastTests";
			DataStore[61].Seal();
			DataStore[62].ClassName = @"Struct";
			DataStore[62].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[11];
			DataStore[62].Description = @"Metadefinition Object for Structs.";
			DataStore[62].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[62].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[62].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[62].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[62].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[33];
			DataStore[62].DefaultModel = null;
			DataStore[62].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[62].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[62].IsFrozenObject = false;
			DataStore[62].IsSimpleObject = false;
			DataStore[62].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[62].TableName = @"Structs";
			DataStore[62].Seal();
			DataStore[64].ClassName = @"StructProperty";
			DataStore[64].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[9];
			DataStore[64].Description = @"Metadefinition Object for Struct Properties.";
			DataStore[64].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[82],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[83],
});
			DataStore[64].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[64].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[64].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(1) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[129],
});
			DataStore[64].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[7];
			DataStore[64].DefaultModel = null;
			DataStore[64].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[64].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[64].IsFrozenObject = false;
			DataStore[64].IsSimpleObject = false;
			DataStore[64].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[64].TableName = @"StructProperties";
			DataStore[64].Seal();
			DataStore[66].ClassName = @"PresenterInfo";
			DataStore[66].DefaultIcon = null;
			DataStore[66].Description = null;
			DataStore[66].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[101],
});
			DataStore[66].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[126],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[127],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[128],
});
			DataStore[66].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[66].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[137],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[138],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[139],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[147],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[148],
});
			DataStore[66].BaseObjectClass = null;
			DataStore[66].DefaultModel = null;
			DataStore[66].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[66].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[66].IsFrozenObject = false;
			DataStore[66].IsSimpleObject = false;
			DataStore[66].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[66].TableName = @"PresenterInfos";
			DataStore[66].Seal();
			DataStore[67].ClassName = @"Visual";
			DataStore[67].DefaultIcon = null;
			DataStore[67].Description = null;
			DataStore[67].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[103],
});
			DataStore[67].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[129],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[130],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[131],
});
			DataStore[67].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[67].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(6) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[151],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[152],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[153],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[164],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[149],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[150],
});
			DataStore[67].BaseObjectClass = null;
			DataStore[67].DefaultModel = null;
			DataStore[67].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[67].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[67].IsFrozenObject = false;
			DataStore[67].IsSimpleObject = false;
			DataStore[67].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[67].TableName = @"Visuals";
			DataStore[67].Seal();
			DataStore[68].ClassName = @"Template";
			DataStore[68].DefaultIcon = null;
			DataStore[68].Description = null;
			DataStore[68].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[102],
});
			DataStore[68].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[132],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[133],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[134],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[144],
});
			DataStore[68].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[68].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(5) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[155],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[163],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[165],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[154],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[162],
});
			DataStore[68].BaseObjectClass = null;
			DataStore[68].DefaultModel = null;
			DataStore[68].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[68].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[68].IsFrozenObject = false;
			DataStore[68].IsSimpleObject = false;
			DataStore[68].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[68].TableName = @"Templates";
			DataStore[68].Seal();
			DataStore[69].ClassName = @"Constraint";
			DataStore[69].DefaultIcon = null;
			DataStore[69].Description = null;
			DataStore[69].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[87],
});
			DataStore[69].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(5) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[135],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[136],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[137],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[138],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[139],
});
			DataStore[69].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[69].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(2) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[167],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[171],
});
			DataStore[69].BaseObjectClass = null;
			DataStore[69].DefaultModel = null;
			DataStore[69].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[69].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[69].IsFrozenObject = true;
			DataStore[69].IsSimpleObject = false;
			DataStore[69].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(5) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[71],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[75],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[74],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[70],
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[73],
});
			DataStore[69].TableName = @"Constraints";
			DataStore[69].Seal();
			DataStore[70].ClassName = @"NotNullableConstraint";
			DataStore[70].DefaultIcon = null;
			DataStore[70].Description = null;
			DataStore[70].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[88],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[90],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[89],
});
			DataStore[70].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[70].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[70].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[70].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[69];
			DataStore[70].DefaultModel = null;
			DataStore[70].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[70].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[70].IsFrozenObject = false;
			DataStore[70].IsSimpleObject = false;
			DataStore[70].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[70].TableName = @"NotNullableConstraints";
			DataStore[70].Seal();
			DataStore[71].ClassName = @"IntegerRangeConstraint";
			DataStore[71].DefaultIcon = null;
			DataStore[71].Description = null;
			DataStore[71].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[93],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[91],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[92],
});
			DataStore[71].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[71].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[71].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(2) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[168],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[169],
});
			DataStore[71].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[69];
			DataStore[71].DefaultModel = null;
			DataStore[71].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[71].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[71].IsFrozenObject = false;
			DataStore[71].IsSimpleObject = false;
			DataStore[71].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[71].TableName = @"IntegerRangeConstraints";
			DataStore[71].Seal();
			DataStore[73].ClassName = @"StringRangeConstraint";
			DataStore[73].DefaultIcon = null;
			DataStore[73].Description = null;
			DataStore[73].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[97],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[95],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[96],
});
			DataStore[73].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[73].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[73].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(2) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[172],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[173],
});
			DataStore[73].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[69];
			DataStore[73].DefaultModel = null;
			DataStore[73].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[73].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[73].IsFrozenObject = false;
			DataStore[73].IsSimpleObject = false;
			DataStore[73].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[73].TableName = @"StringRangeConstraints";
			DataStore[73].Seal();
			DataStore[74].ClassName = @"MethodInvocationConstraint";
			DataStore[74].DefaultIcon = null;
			DataStore[74].Description = null;
			DataStore[74].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[100],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[99],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[98],
});
			DataStore[74].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[74].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[74].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[74].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[69];
			DataStore[74].DefaultModel = null;
			DataStore[74].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[74].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[74].IsFrozenObject = false;
			DataStore[74].IsSimpleObject = false;
			DataStore[74].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[74].TableName = @"MethodInvocationConstraints";
			DataStore[74].Seal();
			DataStore[75].ClassName = @"IsValidIdentifierConstraint";
			DataStore[75].DefaultIcon = null;
			DataStore[75].Description = null;
			DataStore[75].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(3) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[104],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[106],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[105],
});
			DataStore[75].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[75].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[75].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[75].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[69];
			DataStore[75].DefaultModel = null;
			DataStore[75].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[75].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[75].IsFrozenObject = false;
			DataStore[75].IsSimpleObject = false;
			DataStore[75].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(1) {
Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[76],
});
			DataStore[75].TableName = @"IsValidIdentifierConstraints";
			DataStore[75].Seal();
			DataStore[76].ClassName = @"IsValidNamespaceConstraint";
			DataStore[76].DefaultIcon = null;
			DataStore[76].Description = null;
			DataStore[76].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[107],
});
			DataStore[76].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(0) {
});
			DataStore[76].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[76].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(0) {
});
			DataStore[76].BaseObjectClass = Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[75];
			DataStore[76].DefaultModel = null;
			DataStore[76].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[76].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[76].IsFrozenObject = false;
			DataStore[76].IsSimpleObject = false;
			DataStore[76].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[76].TableName = @"IsValidNamespaceConstraints";
			DataStore[76].Seal();
			DataStore[77].ClassName = @"Relation";
			DataStore[77].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[11];
			DataStore[77].Description = @"Describes a Relation between two Object Classes";
			DataStore[77].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[114],
});
			DataStore[77].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[145],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[146],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[147],
});
			DataStore[77].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[77].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(4) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[183],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[184],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[213],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[214],
});
			DataStore[77].BaseObjectClass = null;
			DataStore[77].DefaultModel = null;
			DataStore[77].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[77].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[77].IsFrozenObject = false;
			DataStore[77].IsSimpleObject = false;
			DataStore[77].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[77].TableName = @"Relations";
			DataStore[77].Seal();
			DataStore[79].ClassName = @"TypeRef";
			DataStore[79].DefaultIcon = Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[2];
			DataStore[79].Description = @"This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.";
			DataStore[79].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(2) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[109],
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[108],
});
			DataStore[79].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(4) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[148],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[149],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[150],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[151],
});
			DataStore[79].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[79].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(4) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[205],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[206],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[207],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[235],
});
			DataStore[79].BaseObjectClass = null;
			DataStore[79].DefaultModel = null;
			DataStore[79].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[79].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[79].IsFrozenObject = true;
			DataStore[79].IsSimpleObject = false;
			DataStore[79].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[79].TableName = @"TypeRefs";
			DataStore[79].Seal();
			DataStore[82].ClassName = @"RelationEnd";
			DataStore[82].DefaultIcon = null;
			DataStore[82].Description = @"Describes one end of a relation between two object classes";
			DataStore[82].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(1) {
Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[113],
});
			DataStore[82].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[157],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[158],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[159],
});
			DataStore[82].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[82].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(8) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[215],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[216],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[217],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[218],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[219],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[220],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[223],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[224],
});
			DataStore[82].BaseObjectClass = null;
			DataStore[82].DefaultModel = null;
			DataStore[82].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[82].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[82].IsFrozenObject = false;
			DataStore[82].IsSimpleObject = false;
			DataStore[82].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[82].TableName = @"RelationEnds";
			DataStore[82].Seal();
			DataStore[83].ClassName = @"ViewDescriptor";
			DataStore[83].DefaultIcon = null;
			DataStore[83].Description = null;
			DataStore[83].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[83].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[160],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[161],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[162],
});
			DataStore[83].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[83].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(4) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[226],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[227],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[228],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[229],
});
			DataStore[83].BaseObjectClass = null;
			DataStore[83].DefaultModel = null;
			DataStore[83].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[83].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[83].IsFrozenObject = true;
			DataStore[83].IsSimpleObject = false;
			DataStore[83].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[83].TableName = @"ViewDescriptors";
			DataStore[83].Seal();
			DataStore[85].ClassName = @"PresentableModelDescriptor";
			DataStore[85].DefaultIcon = null;
			DataStore[85].Description = null;
			DataStore[85].MethodInvocations = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.MethodInvocation>(new List<Kistl.App.Base.MethodInvocation>(0) {
});
			DataStore[85].Methods = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Method>(new List<Kistl.App.Base.Method>(3) {
Kistl.App.Base.Method__Implementation__Frozen.DataStore[163],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[164],
Kistl.App.Base.Method__Implementation__Frozen.DataStore[165],
});
			DataStore[85].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[85].Properties = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Property>(new List<Kistl.App.Base.Property>(3) {
Kistl.App.Base.Property__Implementation__Frozen.DataStore[231],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[232],
Kistl.App.Base.Property__Implementation__Frozen.DataStore[233],
});
			DataStore[85].BaseObjectClass = null;
			DataStore[85].DefaultModel = null;
			DataStore[85].DefaultPresentableModelDescriptor = Kistl.App.GUI.PresentableModelDescriptor__Implementation__Frozen.DataStore[7];
			DataStore[85].ImplementsInterfaces = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Interface>(new List<Kistl.App.Base.Interface>(0) {
});
			DataStore[85].IsFrozenObject = true;
			DataStore[85].IsSimpleObject = false;
			DataStore[85].SubClasses = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.ObjectClass>(new List<Kistl.App.Base.ObjectClass>(0) {
});
			DataStore[85].TableName = @"PresentableModelDescriptors";
			DataStore[85].Seal();
	
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