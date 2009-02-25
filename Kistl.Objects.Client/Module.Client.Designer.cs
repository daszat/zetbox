
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

    using Kistl.API.Client;

    /// <summary>
    /// Metadefinition Object for Modules.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Module")]
    public class Module__Implementation__ : BaseClientDataObject, Module
    {


        /// <summary>
        /// Assemblies des Moduls
        /// </summary>
        // object list property

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Assembly> Assemblies
        {
            get
            {
                if (_AssembliesWrapper == null)
                {
                    List<Kistl.App.Base.Assembly> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.Assembly>(this, "Assemblies");
                    else
                        serverList = new List<Kistl.App.Base.Assembly>();
                        
                    _AssembliesWrapper = new BackReferenceCollection<Kistl.App.Base.Assembly>(
                        "Module",
                        this,
                        serverList);
                }
                return _AssembliesWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.Assembly> _AssembliesWrapper;

        /// <summary>
        /// Datentypendes Modules
        /// </summary>
        // object list property

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.DataType> DataTypes
        {
            get
            {
                if (_DataTypesWrapper == null)
                {
                    List<Kistl.App.Base.DataType> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.DataType>(this, "DataTypes");
                    else
                        serverList = new List<Kistl.App.Base.DataType>();
                        
                    _DataTypesWrapper = new BackReferenceCollection<Kistl.App.Base.DataType>(
                        "Module",
                        this,
                        serverList);
                }
                return _DataTypesWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.DataType> _DataTypesWrapper;

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
                    NotifyPropertyChanged("Description");
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
                    NotifyPropertyChanging("ModuleName");
                    _ModuleName = value;
                    NotifyPropertyChanged("ModuleName");
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
                    NotifyPropertyChanging("Namespace");
                    _Namespace = value;
                    NotifyPropertyChanged("Namespace");
                }
            }
        }
        private string _Namespace;

		public override Type GetInterfaceType()
		{
			return typeof(Module);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._ModuleName, binStream);
            BinarySerializer.ToStream(this._Namespace, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._ModuleName, binStream);
            BinarySerializer.FromStream(out this._Namespace, binStream);
        }

#endregion

    }


}