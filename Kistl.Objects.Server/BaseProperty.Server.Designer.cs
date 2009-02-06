
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
    [EdmEntityType(NamespaceName="Model", Name="BaseProperty")]
    [System.Diagnostics.DebuggerDisplay("BaseProperty")]
    public class BaseProperty__Implementation__ : BaseServerDataObject_EntityFramework, BaseProperty
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
                    NotifyPropertyChanged("ID");;
                }
            }
        }
        private int _ID;

        /// <summary>
        /// 
        /// </summary>
    /*
    NewRelation: FK_DataType_BaseProperty_ObjectClass_1 
    A: One DataType as ObjectClass (site: A, from relation ID = 1)
    B: ZeroOrMore BaseProperty as Properties (site: B, from relation ID = 1)
    Preferred Storage: MergeB
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
        public int fk_ObjectClass
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
        private int _fk_ObjectClass;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_BaseProperty_ObjectClass_1", "ObjectClass")]
        public Kistl.App.Base.DataType__Implementation__ ObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_BaseProperty_ObjectClass_1",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_BaseProperty_ObjectClass_1",
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
        /// Zugehörig zum Modul
        /// </summary>
    /*
    NewRelation: FK_BaseProperty_Module_BaseProperty_17 
    A: ZeroOrMore BaseProperty as BaseProperty (site: A, no Relation, prop ID=72)
    B: ZeroOrOne Module as Module (site: B, no Relation, prop ID=72)
    Preferred Storage: MergeA
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
        public int fk_Module
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
        private int _fk_Module;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_BaseProperty_Module_BaseProperty_17", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_BaseProperty_Module_BaseProperty_17",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_BaseProperty_Module_BaseProperty_17",
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
        /// The list of constraints applying to this Property
        /// </summary>
    /*
    NewRelation: FK_BaseProperty_Constraint_ConstrainedProperty_42 
    A: One BaseProperty as ConstrainedProperty (site: A, from relation ID = 16)
    B: ZeroOrMore Constraint as Constraints (site: B, from relation ID = 16)
    Preferred Storage: MergeB
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
                            Constraints__Implementation__);
                }
                return _ConstraintsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_BaseProperty_Constraint_ConstrainedProperty_42", "Constraints")]
        public EntityCollection<Kistl.App.Base.Constraint__Implementation__> Constraints__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.Constraint__Implementation__>(
                        "Model.FK_BaseProperty_Constraint_ConstrainedProperty_42",
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
                    NotifyPropertyChanging("PropertyName");
                    _PropertyName = value;
                    NotifyPropertyChanged("PropertyName");;
                }
            }
        }
        private string _PropertyName;

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
                    NotifyPropertyChanging("AltText");
                    _AltText = value;
                    NotifyPropertyChanged("AltText");;
                }
            }
        }
        private string _AltText;

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
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public virtual string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BaseProperty != null)
            {
                OnGetPropertyTypeString_BaseProperty(this, e);
            };
            return e.Result;
        }
		public delegate void GetPropertyTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetPropertyTypeString_Handler<BaseProperty> OnGetPropertyTypeString_BaseProperty;



        /// <summary>
        /// 
        /// </summary>

		public virtual string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_BaseProperty != null)
            {
                OnGetGUIRepresentation_BaseProperty(this, e);
            };
            return e.Result;
        }
		public delegate void GetGUIRepresentation_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetGUIRepresentation_Handler<BaseProperty> OnGetGUIRepresentation_BaseProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public virtual System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BaseProperty != null)
            {
                OnGetPropertyType_BaseProperty(this, e);
            };
            return e.Result;
        }
		public delegate void GetPropertyType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetPropertyType_Handler<BaseProperty> OnGetPropertyType_BaseProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BaseProperty != null)
            {
                OnToString_BaseProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BaseProperty> OnToString_BaseProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BaseProperty != null) OnPreSave_BaseProperty(this);
        }
        public event ObjectEventHandler<BaseProperty> OnPreSave_BaseProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BaseProperty != null) OnPostSave_BaseProperty(this);
        }
        public event ObjectEventHandler<BaseProperty> OnPostSave_BaseProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_ObjectClass, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._PropertyName, binStream);
            BinarySerializer.ToStream(this._AltText, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ObjectClass, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._PropertyName, binStream);
            BinarySerializer.FromStream(out this._AltText, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
        }

#endregion

    }


}