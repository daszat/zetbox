
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
    /// Metadefinition Object for Modules.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Module")]
    public class Module__Implementation__Frozen : BaseFrozenDataObject, Module
    {
    
		public Module__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Assemblies des Moduls
        /// </summary>
        // object list property
        public virtual ICollection<Kistl.App.Base.Assembly> Assemblies
        {
            get
            {
                if (_Assemblies == null)
                    _Assemblies = new ReadOnlyCollection<Kistl.App.Base.Assembly>(new List<Kistl.App.Base.Assembly>(0));
                return _Assemblies;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _Assemblies = (ReadOnlyCollection<Kistl.App.Base.Assembly>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.Assembly> _Assemblies;

        /// <summary>
        /// Datentypendes Modules
        /// </summary>
        // object list property
        public virtual ICollection<Kistl.App.Base.DataType> DataTypes
        {
            get
            {
                if (_DataTypes == null)
                    _DataTypes = new ReadOnlyCollection<Kistl.App.Base.DataType>(new List<Kistl.App.Base.DataType>(0));
                return _DataTypes;
            }
            internal set
            {
                if (IsReadonly)
                {
                    throw new ReadOnlyObjectException();
                }
                _DataTypes = (ReadOnlyCollection<Kistl.App.Base.DataType>)value;
            }
        }
        private ReadOnlyCollection<Kistl.App.Base.DataType> _DataTypes;

        /// <summary>
        /// Description of this Module
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
        /// Name des Moduls
        /// </summary>
        // value type property
        public virtual string ModuleName
        {
            get
            {
                return _ModuleName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ModuleName != value)
                {
					var __oldValue = _ModuleName;
                    NotifyPropertyChanging("ModuleName", __oldValue, value);
                    _ModuleName = value;
                    NotifyPropertyChanged("ModuleName", __oldValue, value);
                }
            }
        }
        private string _ModuleName;

        /// <summary>
        /// CLR Namespace des Moduls
        /// </summary>
        // value type property
        public virtual string Namespace
        {
            get
            {
                return _Namespace;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Namespace != value)
                {
					var __oldValue = _Namespace;
                    NotifyPropertyChanging("Namespace", __oldValue, value);
                    _Namespace = value;
                    NotifyPropertyChanged("Namespace", __oldValue, value);
                }
            }
        }
        private string _Namespace;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Module));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Module != null)
            {
                OnToString_Module(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Module> OnToString_Module;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Module != null) OnPreSave_Module(this);
        }
        public event ObjectEventHandler<Module> OnPreSave_Module;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Module != null) OnPostSave_Module(this);
        }
        public event ObjectEventHandler<Module> OnPostSave_Module;


        internal Module__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Module__Implementation__Frozen> DataStore = new Dictionary<int, Module__Implementation__Frozen>(5);
		internal static void CreateInstances()
		{
			DataStore[1] = new Module__Implementation__Frozen(1);

			DataStore[2] = new Module__Implementation__Frozen(2);

			DataStore[3] = new Module__Implementation__Frozen(3);

			DataStore[4] = new Module__Implementation__Frozen(4);

			DataStore[5] = new Module__Implementation__Frozen(5);

		}

		internal static void FillDataStore() {
			DataStore[1].Assemblies = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Assembly>(new List<Kistl.App.Base.Assembly>(2) {
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[1],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[2],
});
			DataStore[1].DataTypes = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.DataType>(new List<Kistl.App.Base.DataType>(40) {
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[8],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[79],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[64],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[62],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[73],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[9],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[37],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[82],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[77],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[7],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[14],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[42],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[2],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[70],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[18],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[74],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[30],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[10],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[76],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[75],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[11],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[38],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[44],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[71],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[47],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[46],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[45],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[13],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[39],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[15],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[41],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[33],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[69],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[43],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[12],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[40],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[36],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[29],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[78],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[81],
});
			DataStore[1].Description = null;
			DataStore[1].ModuleName = @"KistlBase";
			DataStore[1].Namespace = @"Kistl.App.Base";
			DataStore[1].Seal();
			DataStore[2].Assemblies = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Assembly>(new List<Kistl.App.Base.Assembly>(0) {
});
			DataStore[2].DataTypes = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.DataType>(new List<Kistl.App.Base.DataType>(5) {
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[4],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[3],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[6],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[26],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[19],
});
			DataStore[2].Description = null;
			DataStore[2].ModuleName = @"Projekte";
			DataStore[2].Namespace = @"Kistl.App.Projekte";
			DataStore[2].Seal();
			DataStore[3].Assemblies = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Assembly>(new List<Kistl.App.Base.Assembly>(0) {
});
			DataStore[3].DataTypes = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.DataType>(new List<Kistl.App.Base.DataType>(5) {
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[20],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[31],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[25],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[23],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[21],
});
			DataStore[3].Description = null;
			DataStore[3].ModuleName = @"Zeiterfassung";
			DataStore[3].Namespace = @"Kistl.App.Zeiterfassung";
			DataStore[3].Seal();
			DataStore[4].Assemblies = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Assembly>(new List<Kistl.App.Base.Assembly>(8) {
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[3],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[4],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[13],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[14],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[15],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[16],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[17],
Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[18],
});
			DataStore[4].DataTypes = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.DataType>(new List<Kistl.App.Base.DataType>(10) {
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[67],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[83],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[68],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[66],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[85],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[27],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[54],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[55],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[53],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[52],
});
			DataStore[4].Description = null;
			DataStore[4].ModuleName = @"GUI";
			DataStore[4].Namespace = @"Kistl.App.GUI";
			DataStore[4].Seal();
			DataStore[5].Assemblies = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.Assembly>(new List<Kistl.App.Base.Assembly>(0) {
});
			DataStore[5].DataTypes = new System.Collections.ObjectModel.ReadOnlyCollection<Kistl.App.Base.DataType>(new List<Kistl.App.Base.DataType>(8) {
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[51],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[58],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[59],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[61],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[60],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[63],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[50],
Kistl.App.Base.DataType__Implementation__Frozen.DataStore[48],
});
			DataStore[5].Description = null;
			DataStore[5].ModuleName = @"TestModule";
			DataStore[5].Namespace = @"Kistl.App.Test";
			DataStore[5].Seal();
	
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