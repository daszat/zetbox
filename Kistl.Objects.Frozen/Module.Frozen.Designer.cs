
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
                    NotifyPropertyChanging("Namespace");
                    _Namespace = value;
                    NotifyPropertyChanged("Namespace");;
                }
            }
        }
        private string _Namespace;

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
                    NotifyPropertyChanging("ModuleName");
                    _ModuleName = value;
                    NotifyPropertyChanged("ModuleName");;
                }
            }
        }
        private string _ModuleName;

        /// <summary>
        /// Datentypendes Modules
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.DataType> DataTypes
        {
            get
            {
                if (_DataTypes == null)
                    _DataTypes = new List<Kistl.App.Base.DataType>();
                return _DataTypes;
            }
        }
        private ICollection<Kistl.App.Base.DataType> _DataTypes;

        /// <summary>
        /// Assemblies des Moduls
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.Assembly> Assemblies
        {
            get
            {
                if (_Assemblies == null)
                    _Assemblies = new List<Kistl.App.Base.Assembly>();
                return _Assemblies;
            }
        }
        private ICollection<Kistl.App.Base.Assembly> _Assemblies;

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
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

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


        internal Module__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }



/*
DTS: 
NS: Kistl.App.Base
CN: Module
*/


		internal Dictionary<int, Module> DataStore = new Dictionary<int, Module>(0);
		static Module__Implementation__Frozen()
		{
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