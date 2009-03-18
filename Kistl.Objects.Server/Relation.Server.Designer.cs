
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
    A: ZeroOrMore Relation as Relation
    B: ZeroOrOne RelationEnd as A
    Preferred Storage: Left
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
        
        

        /// <summary>
        /// The B-side of this Relation.
        /// </summary>
    /*
    Relation: FK_Relation_RelationEnd_Relation_72
    A: ZeroOrMore Relation as Relation
    B: ZeroOrOne RelationEnd as B
    Preferred Storage: Left
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
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Relation));
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
            BinarySerializer.ToStream(this.fk_B, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
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
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStreamConverter(v => ((Relation)this).Storage = (Kistl.App.Base.StorageType)v, binStream);
        }

#endregion

    }


}