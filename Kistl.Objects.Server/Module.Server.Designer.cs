
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for Modules.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Module")]
    [System.Diagnostics.DebuggerDisplay("Module")]
    public class Module__Implementation__ : BaseServerDataObject_EntityFramework, Module
    {

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Assemblies des Moduls
        /// </summary>
    /*
    Relation: FK_Module_Assembly_Module_36
    A: One Module as Module
    B: ZeroOrMore Assembly as Assemblies
    Preferred Storage: Right
    */
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
                    _AssembliesWrapper = new EntityCollectionWrapper<Kistl.App.Base.Assembly, Kistl.App.Base.Assembly__Implementation__>(
                            this.Context, Assemblies__Implementation__);
                }
                return _AssembliesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Module_Assembly_Module_36", "Assemblies")]
        public EntityCollection<Kistl.App.Base.Assembly__Implementation__> Assemblies__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_Module_Assembly_Module_36",
                        "Assemblies");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.Assembly, Kistl.App.Base.Assembly__Implementation__> _AssembliesWrapper;



        /// <summary>
        /// Datentypendes Modules
        /// </summary>
    /*
    Relation: FK_Module_DataType_Module_26
    A: One Module as Module
    B: ZeroOrMore DataType as DataTypes
    Preferred Storage: Right
    */
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
                    _DataTypesWrapper = new EntityCollectionWrapper<Kistl.App.Base.DataType, Kistl.App.Base.DataType__Implementation__>(
                            this.Context, DataTypes__Implementation__);
                }
                return _DataTypesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Module_DataType_Module_26", "DataTypes")]
        public EntityCollection<Kistl.App.Base.DataType__Implementation__> DataTypes__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_Module_DataType_Module_26",
                        "DataTypes");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.DataType, Kistl.App.Base.DataType__Implementation__> _DataTypesWrapper;



        /// <summary>
        /// Description of this Module
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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