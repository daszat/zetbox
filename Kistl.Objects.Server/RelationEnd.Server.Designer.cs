
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
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                    NotifyPropertyChanging("HasPersistentOrder");
                    _HasPersistentOrder = value;
                    NotifyPropertyChanged("HasPersistentOrder");
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
                    NotifyPropertyChanging("Multiplicity");
                    _Multiplicity = value;
                    NotifyPropertyChanged("Multiplicity");
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
    Relation: FK_RelationEnd_Property_RelationEnd_74
    A: ZeroOrOne RelationEnd as RelationEnd
    B: ZeroOrOne Property as Navigator
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property Navigator
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
                Navigator__Implementation__ = (Kistl.App.Base.Property__Implementation__)value;
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
        [EdmRelationshipNavigationProperty("Model", "FK_RelationEnd_Property_RelationEnd_74", "Navigator")]
        public Kistl.App.Base.Property__Implementation__ Navigator__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_RelationEnd_Property_RelationEnd_74",
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
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_RelationEnd_Property_RelationEnd_74",
                        "Navigator");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Property__Implementation__)value;
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
                    NotifyPropertyChanging("Role");
                    _Role = value;
                    NotifyPropertyChanged("Role");
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
                    NotifyPropertyChanging("RoleName");
                    _RoleName = value;
                    NotifyPropertyChanged("RoleName");
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
				Navigator__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.Find<Kistl.App.Base.Property>(_fk_Navigator.Value);
			else
				Navigator__Implementation__ = null;
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
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

#endregion

    }


}