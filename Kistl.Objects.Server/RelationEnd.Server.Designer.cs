
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
    /// Describes one end of a relation between two object classes
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="RelationEnd")]
    [System.Diagnostics.DebuggerDisplay("RelationEnd")]
    public class RelationEnd__Implementation__ : BaseServerDataObject_EntityFramework, RelationEnd
    {
    
		public RelationEnd__Implementation__()
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
        /// The Relation using this RelationEnd as A
        /// </summary>
    /*
    Relation: FK_Relation_RelationEnd_Relation_71
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as A
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation AParent
        {
            get
            {
                return AParent__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                AParent__Implementation__ = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_AParent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && AParent != null)
                {
                    _fk_AParent = AParent.ID;
                }
                return _fk_AParent;
            }
            set
            {
                _fk_AParent = value;
            }
        }
        private int? _fk_AParent;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_RelationEnd_Relation_71", "Relation")]
        public Kistl.App.Base.Relation__Implementation__ AParent__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_71",
                        "Relation");
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
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_71",
                        "Relation");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The Relation using this RelationEnd as B
        /// </summary>
    /*
    Relation: FK_Relation_RelationEnd_Relation_72
    A: ZeroOrOne Relation as Relation
    B: ZeroOrOne RelationEnd as B
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation BParent
        {
            get
            {
                return BParent__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                BParent__Implementation__ = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_BParent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && BParent != null)
                {
                    _fk_BParent = BParent.ID;
                }
                return _fk_BParent;
            }
            set
            {
                _fk_BParent = value;
            }
        }
        private int? _fk_BParent;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_RelationEnd_Relation_72", "Relation")]
        public Kistl.App.Base.Relation__Implementation__ BParent__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_72",
                        "Relation");
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
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_72",
                        "Relation");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Is true, if this RelationEnd persists the order of its elements
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool HasPersistentOrder
        {
            get
            {
                return _HasPersistentOrder;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_HasPersistentOrder != value)
                {
					var __oldValue = _HasPersistentOrder;
                    NotifyPropertyChanging("HasPersistentOrder", __oldValue, value);
                    _HasPersistentOrder = value;
                    NotifyPropertyChanged("HasPersistentOrder", __oldValue, value);
                }
            }
        }
        private bool _HasPersistentOrder;

        /// <summary>
        /// Specifies how many instances may occur on this end of the relation.
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.Base.Multiplicity RelationEnd.Multiplicity
        {
            get
            {
                return _Multiplicity;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Multiplicity != value)
                {
					var __oldValue = _Multiplicity;
                    NotifyPropertyChanging("Multiplicity", __oldValue, value);
                    _Multiplicity = value;
                    NotifyPropertyChanged("Multiplicity", __oldValue, value);
                }
            }
        }
        
        /// <summary>backing store for Multiplicity</summary>
        private Kistl.App.Base.Multiplicity _Multiplicity;
        
        /// <summary>EF sees only this property, for Multiplicity</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int Multiplicity
        {
            get
            {
                return (int)((RelationEnd)this).Multiplicity;
            }
            set
            {
                ((RelationEnd)this).Multiplicity = (Kistl.App.Base.Multiplicity)value;
            }
        }
        

        /// <summary>
        /// The ORP to navigate FROM this end of the relation. MAY be null.
        /// </summary>
    /*
    Relation: FK_RelationEnd_ObjectReferenceProperty_RelationEnd_74
    A: ZeroOrOne RelationEnd as RelationEnd
    B: ZeroOrOne ObjectReferenceProperty as Navigator
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty Navigator
        {
            get
            {
                return Navigator__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Navigator__Implementation__ = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Navigator
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Navigator != null)
                {
                    _fk_Navigator = Navigator.ID;
                }
                return _fk_Navigator;
            }
            set
            {
                _fk_Navigator = value;
            }
        }
        private int? _fk_Navigator;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_RelationEnd_ObjectReferenceProperty_RelationEnd_74", "Navigator")]
        public Kistl.App.Base.ObjectReferenceProperty__Implementation__ Navigator__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_RelationEnd_ObjectReferenceProperty_RelationEnd_74",
                        "Navigator");
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
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_RelationEnd_ObjectReferenceProperty_RelationEnd_74",
                        "Navigator");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Which RelationEndRole this End has
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int Role
        {
            get
            {
                return _Role;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Role != value)
                {
					var __oldValue = _Role;
                    NotifyPropertyChanging("Role", __oldValue, value);
                    _Role = value;
                    NotifyPropertyChanged("Role", __oldValue, value);
                }
            }
        }
        private int _Role;

        /// <summary>
        /// This end's role name in the relation
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_RoleName != value)
                {
					var __oldValue = _RoleName;
                    NotifyPropertyChanging("RoleName", __oldValue, value);
                    _RoleName = value;
                    NotifyPropertyChanged("RoleName", __oldValue, value);
                }
            }
        }
        private string _RoleName;

        /// <summary>
        /// Specifies which type this End of the relation has. MUST NOT be null.
        /// </summary>
    /*
    Relation: FK_RelationEnd_ObjectClass_RelationEnd_73
    A: ZeroOrMore RelationEnd as RelationEnd
    B: ZeroOrOne ObjectClass as Type
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass Type
        {
            get
            {
                return Type__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Type__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Type
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Type != null)
                {
                    _fk_Type = Type.ID;
                }
                return _fk_Type;
            }
            set
            {
                _fk_Type = value;
            }
        }
        private int? _fk_Type;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_RelationEnd_ObjectClass_RelationEnd_73", "Type")]
        public Kistl.App.Base.ObjectClass__Implementation__ Type__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_RelationEnd_ObjectClass_RelationEnd_73",
                        "Type");
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
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_RelationEnd_ObjectClass_RelationEnd_73",
                        "Type");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(RelationEnd));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (RelationEnd)obj;
			var otherImpl = (RelationEnd__Implementation__)obj;
			var me = (RelationEnd)this;

			me.HasPersistentOrder = other.HasPersistentOrder;
			me.Multiplicity = other.Multiplicity;
			me.Role = other.Role;
			me.RoleName = other.RoleName;
			this.fk_AParent = otherImpl.fk_AParent;
			this.fk_BParent = otherImpl.fk_BParent;
			this.fk_Navigator = otherImpl.fk_Navigator;
			this.fk_Type = otherImpl.fk_Type;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RelationEnd != null)
            {
                OnToString_RelationEnd(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<RelationEnd> OnToString_RelationEnd;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_RelationEnd != null) OnPreSave_RelationEnd(this);
        }
        public event ObjectEventHandler<RelationEnd> OnPreSave_RelationEnd;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_RelationEnd != null) OnPostSave_RelationEnd(this);
        }
        public event ObjectEventHandler<RelationEnd> OnPostSave_RelationEnd;



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Type.HasValue)
				Type__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)Context.Find<Kistl.App.Base.ObjectClass>(_fk_Type.Value);
			else
				Type__Implementation__ = null;
			if (_fk_Navigator.HasValue)
				Navigator__Implementation__ = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)Context.Find<Kistl.App.Base.ObjectReferenceProperty>(_fk_Navigator.Value);
			else
				Navigator__Implementation__ = null;
			if (_fk_BParent.HasValue)
				BParent__Implementation__ = (Kistl.App.Base.Relation__Implementation__)Context.Find<Kistl.App.Base.Relation>(_fk_BParent.Value);
			else
				BParent__Implementation__ = null;
			if (_fk_AParent.HasValue)
				AParent__Implementation__ = (Kistl.App.Base.Relation__Implementation__)Context.Find<Kistl.App.Base.Relation>(_fk_AParent.Value);
			else
				AParent__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_AParent, binStream);
            BinarySerializer.ToStream(this.fk_BParent, binStream);
            BinarySerializer.ToStream(this._HasPersistentOrder, binStream);
            BinarySerializer.ToStream((int)((RelationEnd)this).Multiplicity, binStream);
            BinarySerializer.ToStream(this.fk_Navigator, binStream);
            BinarySerializer.ToStream(this._Role, binStream);
            BinarySerializer.ToStream(this._RoleName, binStream);
            BinarySerializer.ToStream(this.fk_Type, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_AParent;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_AParent = tmp;
            }
            {
                var tmp = this.fk_BParent;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_BParent = tmp;
            }
            BinarySerializer.FromStream(out this._HasPersistentOrder, binStream);
            BinarySerializer.FromStreamConverter(v => ((RelationEnd)this).Multiplicity = (Kistl.App.Base.Multiplicity)v, binStream);
            {
                var tmp = this.fk_Navigator;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Navigator = tmp;
            }
            BinarySerializer.FromStream(out this._Role, binStream);
            BinarySerializer.FromStream(out this._RoleName, binStream);
            {
                var tmp = this.fk_Type;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Type = tmp;
            }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this.fk_AParent, xml, "fk_AParent", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_BParent, xml, "fk_BParent", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "http://dasz.at/Kistl");
            // TODO: Add XML Serializer here
            XmlStreamer.ToStream(this.fk_Navigator, xml, "fk_Navigator", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Role, xml, "Role", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._RoleName, xml, "RoleName", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_Type, xml, "fk_Type", "http://dasz.at/Kistl");
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
        }

#endregion

    }


}