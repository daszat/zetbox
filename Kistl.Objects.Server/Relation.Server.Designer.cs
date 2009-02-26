
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
    NewRelation: FK_Relation_ObjectReferenceProperty_LeftOf_43 
    A: ZeroOrOne Relation as LeftOf (site: A, from relation ID = 17)
    B: One ObjectReferenceProperty as LeftPart (site: B, from relation ID = 17)
    Preferred Storage: MergeA
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
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_ObjectReferenceProperty_LeftOf_43", "LeftPart")]
        public Kistl.App.Base.ObjectReferenceProperty__Implementation__ LeftPart__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_LeftOf_43",
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
                        "Model.FK_Relation_ObjectReferenceProperty_LeftOf_43",
                        "LeftPart");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Right Part of the Relation
        /// </summary>
    /*
    NewRelation: FK_Relation_ObjectReferenceProperty_RightOf_44 
    A: ZeroOrOne Relation as RightOf (site: A, from relation ID = 18)
    B: One ObjectReferenceProperty as RightPart (site: B, from relation ID = 18)
    Preferred Storage: MergeA
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
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_ObjectReferenceProperty_RightOf_44", "RightPart")]
        public Kistl.App.Base.ObjectReferenceProperty__Implementation__ RightPart__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectReferenceProperty__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_RightOf_44",
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
                        "Model.FK_Relation_ObjectReferenceProperty_RightOf_44",
                        "RightPart");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectReferenceProperty__Implementation__)value;
            }
        }
        
        

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
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_LeftPart, binStream);
            BinarySerializer.ToStream(this.fk_RightPart, binStream);
            BinarySerializer.ToStream((int)((Relation)this).Storage, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_LeftPart;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_LeftPart = tmp;
            }
            {
                var tmp = this.fk_RightPart;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_RightPart = tmp;
            }
            BinarySerializer.FromStreamConverter(v => ((Relation)this).Storage = (Kistl.App.Base.StorageType)v, binStream);
        }

#endregion

    }


}