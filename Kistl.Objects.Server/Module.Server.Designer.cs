// <autogenerated/>


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
    public class Module__Implementation__ : BaseServerDataObject_EntityFramework, Module, Kistl.API.IExportableInternal
    {
    
		public Module__Implementation__()
		{
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Assemblies des Moduls
        /// </summary>
    /*
    Relation: FK_Module_has_Assembly
    A: One Module as Module
    B: ZeroOrMore Assembly as Assemblies
    Preferred Storage: MergeIntoB
    */
        // object list property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectListProperty
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
        
        [EdmRelationshipNavigationProperty("Model", "FK_Module_has_Assembly", "Assemblies")]
        public EntityCollection<Kistl.App.Base.Assembly__Implementation__> Assemblies__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_Module_has_Assembly",
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
    Relation: FK_Module_has_DataType
    A: One Module as Module
    B: ZeroOrMore DataType as DataTypes
    Preferred Storage: MergeIntoB
    */
        // object list property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectListProperty
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
        
        [EdmRelationshipNavigationProperty("Model", "FK_Module_has_DataType", "DataTypes")]
        public EntityCollection<Kistl.App.Base.DataType__Implementation__> DataTypes__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_Module_has_DataType",
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
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
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
        /// Export Guid
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual Guid ExportGuid
        {
            get
            {
                return _ExportGuid;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ExportGuid != value)
                {
					var __oldValue = _ExportGuid;
                    NotifyPropertyChanging("ExportGuid", __oldValue, value);
                    _ExportGuid = value;
                    NotifyPropertyChanged("ExportGuid", __oldValue, value);
                }
            }
        }
        private Guid _ExportGuid;

        /// <summary>
        /// Name des Moduls
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string ModuleName
        {
            get
            {
                return _ModuleName;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
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
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string Namespace
        {
            get
            {
                return _Namespace;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
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

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Module)obj;
			var otherImpl = (Module__Implementation__)obj;
			var me = (Module)this;

			me.Description = other.Description;
			me.ExportGuid = other.ExportGuid;
			me.ModuleName = other.ModuleName;
			me.Namespace = other.Namespace;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

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

        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Module != null) OnCreated_Module(this);
        }
        public event ObjectEventHandler<Module> OnCreated_Module;

        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Module != null) OnDeleting_Module(this);
        }
        public event ObjectEventHandler<Module> OnDeleting_Module;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Assemblies":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(53).Constraints
						.Where(c => !c.IsValid(this, this.Assemblies))
						.Select(c => c.GetErrorText(this, this.Assemblies))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DataTypes":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(51).Constraints
						.Where(c => !c.IsValid(this, this.DataTypes))
						.Select(c => c.GetErrorText(this, this.DataTypes))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Description":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(92).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(19).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ModuleName":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(102).Constraints
						.Where(c => !c.IsValid(this, this.ModuleName))
						.Select(c => c.GetErrorText(this, this.ModuleName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Namespace":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(98).Constraints
						.Where(c => !c.IsValid(this, this.Namespace))
						.Select(c => c.GetErrorText(this, this.Namespace))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(this._ModuleName, binStream);
            BinarySerializer.ToStream(this._Namespace, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._ExportGuid, binStream);
            BinarySerializer.FromStream(out this._ModuleName, binStream);
            BinarySerializer.FromStream(out this._Namespace, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ModuleName, xml, "ModuleName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ModuleName, xml, "ModuleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ModuleName, xml, "ModuleName", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ModuleName, xml, "ModuleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Namespace, xml, "Namespace", "Kistl.App.Base");
        }

#endregion

    }


}