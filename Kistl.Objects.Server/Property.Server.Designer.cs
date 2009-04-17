
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
    /// Metadefinition Object for Properties. This class is abstract.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Property")]
    [System.Diagnostics.DebuggerDisplay("Property")]
    public class Property__Implementation__ : BaseServerDataObject_EntityFramework, Property
    {
    
		public Property__Implementation__()
		{
            {
            }
        }

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
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string AltText
        {
            get
            {
                return _AltText;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AltText != value)
                {
					var __oldValue = _AltText;
                    NotifyPropertyChanging("AltText", __oldValue, value);
                    _AltText = value;
                    NotifyPropertyChanged("AltText", __oldValue, value);
                }
            }
        }
        private string _AltText;

        /// <summary>
        /// A space separated list of category names containing this Property
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string CategoryTags
        {
            get
            {
                return _CategoryTags;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_CategoryTags != value)
                {
					var __oldValue = _CategoryTags;
                    NotifyPropertyChanging("CategoryTags", __oldValue, value);
                    _CategoryTags = value;
                    NotifyPropertyChanged("CategoryTags", __oldValue, value);
                }
            }
        }
        private string _CategoryTags;

        /// <summary>
        /// The list of constraints applying to this Property
        /// </summary>
    /*
    Relation: FK_Property_Constraint_ConstrainedProperty_62
    A: One Property as ConstrainedProperty
    B: ZeroOrMore Constraint as Constraints
    Preferred Storage: Right
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Constraint> Constraints
        {
            get
            {
                if (_ConstraintsWrapper == null)
                {
                    _ConstraintsWrapper = new EntityCollectionWrapper<Kistl.App.Base.Constraint, Kistl.App.Base.Constraint__Implementation__>(
                            this.Context, Constraints__Implementation__);
                }
                return _ConstraintsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Property_Constraint_ConstrainedProperty_62", "Constraints")]
        public EntityCollection<Kistl.App.Base.Constraint__Implementation__> Constraints__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.Constraint__Implementation__>(
                        "Model.FK_Property_Constraint_ConstrainedProperty_62",
                        "Constraints");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.Constraint, Kistl.App.Base.Constraint__Implementation__> _ConstraintsWrapper;



        /// <summary>
        /// Description of this Property
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
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Whether or not a list-valued property has a index
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsIndexed
        {
            get
            {
                return _IsIndexed;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsIndexed != value)
                {
					var __oldValue = _IsIndexed;
                    NotifyPropertyChanging("IsIndexed", __oldValue, value);
                    _IsIndexed = value;
                    NotifyPropertyChanged("IsIndexed", __oldValue, value);
                }
            }
        }
        private bool _IsIndexed;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsList
        {
            get
            {
                return _IsList;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsList != value)
                {
					var __oldValue = _IsList;
                    NotifyPropertyChanging("IsList", __oldValue, value);
                    _IsList = value;
                    NotifyPropertyChanged("IsList", __oldValue, value);
                }
            }
        }
        private bool _IsList;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsNullable
        {
            get
            {
                return _IsNullable;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsNullable != value)
                {
					var __oldValue = _IsNullable;
                    NotifyPropertyChanging("IsNullable", __oldValue, value);
                    _IsNullable = value;
                    NotifyPropertyChanged("IsNullable", __oldValue, value);
                }
            }
        }
        private bool _IsNullable;

        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
    /*
    Relation: FK_Property_Module_BaseProperty_37
    A: ZeroOrMore Property as BaseProperty
    B: ZeroOrOne Module as Module
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                return Module__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Module != null)
                {
                    _fk_Module = Module.ID;
                }
                return _fk_Module;
            }
            set
            {
                _fk_Module = value;
            }
        }
        private int? _fk_Module;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Property_Module_BaseProperty_37", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_Property_Module_BaseProperty_37",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_Property_Module_BaseProperty_37",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_DataType_Property_ObjectClass_19
    A: One DataType as ObjectClass
    B: ZeroOrMore Property as Properties
    Preferred Storage: Right
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                return ObjectClass__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                ObjectClass__Implementation__ = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && ObjectClass != null)
                {
                    _fk_ObjectClass = ObjectClass.ID;
                }
                return _fk_ObjectClass;
            }
            set
            {
                _fk_ObjectClass = value;
            }
        }
        private int? _fk_ObjectClass;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_Property_ObjectClass_19", "ObjectClass")]
        public Kistl.App.Base.DataType__Implementation__ ObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_Property_ObjectClass_19",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_Property_ObjectClass_19",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PropertyName != value)
                {
					var __oldValue = _PropertyName;
                    NotifyPropertyChanging("PropertyName", __oldValue, value);
                    _PropertyName = value;
                    NotifyPropertyChanged("PropertyName", __oldValue, value);
                }
            }
        }
        private string _PropertyName;

        /// <summary>
        /// 
        /// </summary>

		public virtual string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_Property != null)
            {
                OnGetGUIRepresentation_Property(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Property.GetGUIRepresentation");
            }
            return e.Result;
        }
		public delegate void GetGUIRepresentation_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetGUIRepresentation_Handler<Property> OnGetGUIRepresentation_Property;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public virtual System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_Property != null)
            {
                OnGetPropertyType_Property(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Property.GetPropertyType");
            }
            return e.Result;
        }
		public delegate void GetPropertyType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetPropertyType_Handler<Property> OnGetPropertyType_Property;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public virtual string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_Property != null)
            {
                OnGetPropertyTypeString_Property(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Property.GetPropertyTypeString");
            }
            return e.Result;
        }
		public delegate void GetPropertyTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetPropertyTypeString_Handler<Property> OnGetPropertyTypeString_Property;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Property));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Property)obj;
			var otherImpl = (Property__Implementation__)obj;
			var me = (Property)this;

			me.AltText = other.AltText;
			me.CategoryTags = other.CategoryTags;
			me.Description = other.Description;
			me.IsIndexed = other.IsIndexed;
			me.IsList = other.IsList;
			me.IsNullable = other.IsNullable;
			me.PropertyName = other.PropertyName;
			this.fk_Module = otherImpl.fk_Module;
			this.fk_ObjectClass = otherImpl.fk_ObjectClass;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Property != null)
            {
                OnToString_Property(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Property> OnToString_Property;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Property != null) OnPreSave_Property(this);
        }
        public event ObjectEventHandler<Property> OnPreSave_Property;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Property != null) OnPostSave_Property(this);
        }
        public event ObjectEventHandler<Property> OnPostSave_Property;



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_ObjectClass.HasValue)
				ObjectClass__Implementation__ = (Kistl.App.Base.DataType__Implementation__)Context.Find<Kistl.App.Base.DataType>(_fk_ObjectClass.Value);
			else
				ObjectClass__Implementation__ = null;
			if (_fk_Module.HasValue)
				Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)Context.Find<Kistl.App.Base.Module>(_fk_Module.Value);
			else
				Module__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AltText, binStream);
            BinarySerializer.ToStream(this._CategoryTags, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._IsIndexed, binStream);
            BinarySerializer.ToStream(this._IsList, binStream);
            BinarySerializer.ToStream(this._IsNullable, binStream);
            BinarySerializer.ToStream(this.fk_Module, binStream);
            BinarySerializer.ToStream(this.fk_ObjectClass, binStream);
            BinarySerializer.ToStream(this._PropertyName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AltText, binStream);
            BinarySerializer.FromStream(out this._CategoryTags, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._IsIndexed, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
            BinarySerializer.FromStream(out this._IsNullable, binStream);
            {
                var tmp = this.fk_Module;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Module = tmp;
            }
            {
                var tmp = this.fk_ObjectClass;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_ObjectClass = tmp;
            }
            BinarySerializer.FromStream(out this._PropertyName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._AltText, xml, "AltText", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._CategoryTags, xml, "CategoryTags", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsIndexed, xml, "IsIndexed", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsList, xml, "IsList", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsNullable, xml, "IsNullable", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_Module, xml, "fk_Module", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_ObjectClass, xml, "fk_ObjectClass", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PropertyName, xml, "PropertyName", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}