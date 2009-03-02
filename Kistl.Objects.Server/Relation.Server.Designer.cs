
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
    /// Describes a Relation between two Object Classes
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Relation")]
    [System.Diagnostics.DebuggerDisplay("Relation")]
    public class Relation__Implementation__ : BaseServerDataObject_EntityFramework, Relation
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
        /// The A-side of this Relation.
        /// </summary>
    /*
    Relation: FK_Relation_RelationEnd_Relation_71
    A: 3 Relation as Relation
    B: 1 RelationEnd as A
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.RelationEnd A
        {
            get
            {
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.Base.RelationEnd__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_RelationEnd_Relation_71", "A")]
        public Kistl.App.Base.RelationEnd__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.RelationEnd__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.RelationEnd__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_71",
                        "A");
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
                EntityReference<Kistl.App.Base.RelationEnd__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.RelationEnd__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_71",
                        "A");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.RelationEnd__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? A_pos
        {
            get
            {
                return _A_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_A_pos != value)
                {
                    NotifyPropertyChanging("A_pos");
                    _A_pos = value;
                    NotifyPropertyChanged("A_pos");
                }
            }
        }
        private int? _A_pos;
        

        /// <summary>
        /// The B-side of this Relation.
        /// </summary>
    /*
    Relation: FK_Relation_RelationEnd_Relation_72
    A: 3 Relation as Relation
    B: 1 RelationEnd as B
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.RelationEnd B
        {
            get
            {
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.Base.RelationEnd__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_RelationEnd_Relation_72", "B")]
        public Kistl.App.Base.RelationEnd__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.RelationEnd__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.RelationEnd__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_72",
                        "B");
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
                EntityReference<Kistl.App.Base.RelationEnd__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.RelationEnd__Implementation__>(
                        "Model.FK_Relation_RelationEnd_Relation_72",
                        "B");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.RelationEnd__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? B_pos
        {
            get
            {
                return _B_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_B_pos != value)
                {
                    NotifyPropertyChanging("B_pos");
                    _B_pos = value;
                    NotifyPropertyChanged("B_pos");
                }
            }
        }
        private int? _B_pos;
        

        /// <summary>
        /// Description of this Relation
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
        /// Left Part of the Relation
        /// </summary>
    /*
    Relation: FK_Relation_ObjectReferenceProperty_LeftOf_63
    A: 1 Relation as LeftOf
    B: 2 ObjectReferenceProperty as LeftPart
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty LeftPart
        {
            get
            {
                return LeftPart__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                LeftPart__Implementation__ = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_LeftPart
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && LeftPart != null)
                {
                    _fk_LeftPart = LeftPart.ID;
                }
                return _fk_LeftPart;
            }
            set
            {
                _fk_LeftPart = value;
            }
        }
        private int? _fk_LeftPart;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_ObjectReferenceProperty_LeftOf_63", "LeftPart")]
        public Kistl.App.Base.ObjectReferenceProperty__Implementation__ LeftPart__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_LeftOf_63",
                        "LeftPart");
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
                        "Model.FK_Relation_ObjectReferenceProperty_LeftOf_63",
                        "LeftPart");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? LeftPart_pos
        {
            get
            {
                return _LeftPart_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_LeftPart_pos != value)
                {
                    NotifyPropertyChanging("LeftPart_pos");
                    _LeftPart_pos = value;
                    NotifyPropertyChanged("LeftPart_pos");
                }
            }
        }
        private int? _LeftPart_pos;
        

        /// <summary>
        /// Right Part of the Relation
        /// </summary>
    /*
    Relation: FK_Relation_ObjectReferenceProperty_RightOf_64
    A: 1 Relation as RightOf
    B: 2 ObjectReferenceProperty as RightPart
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty RightPart
        {
            get
            {
                return RightPart__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                RightPart__Implementation__ = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_RightPart
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && RightPart != null)
                {
                    _fk_RightPart = RightPart.ID;
                }
                return _fk_RightPart;
            }
            set
            {
                _fk_RightPart = value;
            }
        }
        private int? _fk_RightPart;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_ObjectReferenceProperty_RightOf_64", "RightPart")]
        public Kistl.App.Base.ObjectReferenceProperty__Implementation__ RightPart__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_RightOf_64",
                        "RightPart");
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
                        "Model.FK_Relation_ObjectReferenceProperty_RightOf_64",
                        "RightPart");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? RightPart_pos
        {
            get
            {
                return _RightPart_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_RightPart_pos != value)
                {
                    NotifyPropertyChanging("RightPart_pos");
                    _RightPart_pos = value;
                    NotifyPropertyChanged("RightPart_pos");
                }
            }
        }
        private int? _RightPart_pos;
        

        /// <summary>
        /// Storagetype for 1:1 Relations. Must be null for non 1:1 Relations.
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.Base.StorageType? Relation.Storage
        {
            get
            {
                return _Storage;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Storage != value)
                {
                    NotifyPropertyChanging("Storage");
                    _Storage = value;
                    NotifyPropertyChanged("Storage");
                }
            }
        }
        
        /// <summary>backing store for Storage</summary>
        private Kistl.App.Base.StorageType? _Storage;
        
        /// <summary>EF sees only this property, for Storage</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int Storage
        {
            get
            {
                return (int)((Relation)this).Storage;
            }
            set
            {
                ((Relation)this).Storage = (Kistl.App.Base.StorageType?)value;
            }
        }
        

		public override Type GetInterfaceType()
		{
			return typeof(Relation);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Relation != null)
            {
                OnToString_Relation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Relation> OnToString_Relation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Relation != null) OnPreSave_Relation(this);
        }
        public event ObjectEventHandler<Relation> OnPreSave_Relation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Relation != null) OnPostSave_Relation(this);
        }
        public event ObjectEventHandler<Relation> OnPostSave_Relation;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_LeftPart, binStream);
            BinarySerializer.ToStream(this._LeftPart_pos, binStream);
            BinarySerializer.ToStream(this.fk_RightPart, binStream);
            BinarySerializer.ToStream(this._RightPart_pos, binStream);
            BinarySerializer.ToStream((int)((Relation)this).Storage, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            BinarySerializer.FromStream(out this._A_pos, binStream);
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
            BinarySerializer.FromStream(out this._B_pos, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_LeftPart;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_LeftPart = tmp;
            }
            BinarySerializer.FromStream(out this._LeftPart_pos, binStream);
            {
                var tmp = this.fk_RightPart;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_RightPart = tmp;
            }
            BinarySerializer.FromStream(out this._RightPart_pos, binStream);
            BinarySerializer.FromStreamConverter(v => ((Relation)this).Storage = (Kistl.App.Base.StorageType)v, binStream);
        }

#endregion

    }


}