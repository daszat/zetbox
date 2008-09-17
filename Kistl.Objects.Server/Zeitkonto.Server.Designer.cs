//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_Mitarbeiter", "A_Mitarbeiter", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter__Implementation__), "B_Zeitkonto_MitarbeiterCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry__Implementation__))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Parent", "A_Zeitkonto", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.Zeitkonto__Implementation__), "B_Zeitkonto_MitarbeiterCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry__Implementation__))]

namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Kistl.DALProvider.EF;
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Zeitkonto")]
    public class Zeitkonto__Implementation__ : BaseServerDataObject_EntityFramework, Zeitkonto
    {
        
        private int _ID;
        
        private string _Kontoname;
        
        private EntityCollectionWrapper<Kistl.App.Zeiterfassung.Taetigkeit, Kistl.App.Zeiterfassung.Taetigkeit__Implementation__> TaetigkeitenWrapper;
        
        private EntityCollectionEntryValueWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry__Implementation__> MitarbeiterWrapper;
        
        private System.Double? _MaxStunden;
        
        private System.Double? _AktuelleStunden;
        
        public Zeitkonto__Implementation__()
        {
        }
        
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Kontoname
        {
            get
            {
                return _Kontoname;
            }
            set
            {
                if (Kontoname != value)
                {
                    NotifyPropertyChanging("Kontoname"); 
                    _Kontoname = value;
                    NotifyPropertyChanged("Kontoname");;
                }
            }
        }
        
        [XmlIgnore()]
        public ICollection<Kistl.App.Zeiterfassung.Taetigkeit> Taetigkeiten
        {
            get
            {
                if (TaetigkeitenWrapper == null) TaetigkeitenWrapper = new EntityCollectionWrapper<Kistl.App.Zeiterfassung.Taetigkeit, Kistl.App.Zeiterfassung.Taetigkeit__Implementation__>(Taetigkeiten__Implementation__);
                return TaetigkeitenWrapper;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Taetigkeit_Zeitkonto_Zeitkonto", "B_Taetigkeit")]
        public EntityCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__> Taetigkeiten__Implementation__
        {
            get
            {
                EntityCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__>("Model.FK_Taetigkeit_Zeitkonto_Zeitkonto", "B_Taetigkeit");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        public IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (MitarbeiterWrapper == null) MitarbeiterWrapper = new EntityCollectionEntryValueWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry__Implementation__>(this, Mitarbeiter__Implementation__);
                return MitarbeiterWrapper;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Parent", "B_Zeitkonto_MitarbeiterCollectionEntry")]
        public EntityCollection<Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry__Implementation__> Mitarbeiter__Implementation__
        {
            get
            {
                EntityCollection<Zeitkonto_MitarbeiterCollectionEntry__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Zeitkonto_MitarbeiterCollectionEntry__Implementation__>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Parent", "B_Zeitkonto_MitarbeiterCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.Double? MaxStunden
        {
            get
            {
                return _MaxStunden;
            }
            set
            {
                if (MaxStunden != value)
                {
                    NotifyPropertyChanging("MaxStunden"); 
                    _MaxStunden = value;
                    NotifyPropertyChanged("MaxStunden");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.Double? AktuelleStunden
        {
            get
            {
                return _AktuelleStunden;
            }
            set
            {
                if (AktuelleStunden != value)
                {
                    NotifyPropertyChanging("AktuelleStunden"); 
                    _AktuelleStunden = value;
                    NotifyPropertyChanged("AktuelleStunden");;
                }
            }
        }
        
        public event ToStringHandler<Zeitkonto> OnToString_Zeitkonto;
        
        public event ObjectEventHandler<Zeitkonto> OnPreSave_Zeitkonto;
        
        public event ObjectEventHandler<Zeitkonto> OnPostSave_Zeitkonto;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Zeitkonto != null)
            {
                OnToString_Zeitkonto(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Zeitkonto != null) OnPreSave_Zeitkonto(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Zeitkonto != null) OnPostSave_Zeitkonto(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            /// Use ToList before using foreach - the collection will change in the KistContext.Attach() Method because EntityFramework will need a Trick to attach CollectionEntries correctly
            Mitarbeiter__Implementation__.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i));
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Kontoname, sw);
            BinarySerializer.ToBinary(this.Mitarbeiter__Implementation__, sw);
            BinarySerializer.ToBinary(this._MaxStunden, sw);
            BinarySerializer.ToBinary(this._AktuelleStunden, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Kontoname, sr);
            BinarySerializer.FromBinaryCollectionEntries(this.Mitarbeiter__Implementation__, sr);
            BinarySerializer.FromBinary(out this._MaxStunden, sr);
            BinarySerializer.FromBinary(out this._AktuelleStunden, sr);
        }
    }
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Zeitkonto_MitarbeiterCollectionEntry")]
    public class Zeitkonto_MitarbeiterCollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, ICollectionEntry<Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto>
    {
        
        private int _ID;
        
        private int _fk_Value;
        
        private int _fk_Parent;
        
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        
        [XmlIgnore()]
        public Kistl.App.Projekte.Mitarbeiter Value
        {
            get
            {
                return ValueImpl;
            }
            set
            {
                ValueImpl = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        public Kistl.App.Zeiterfassung.Zeitkonto Parent
        {
            get
            {
                return ParentImpl;
            }
            set
            {
                ParentImpl = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)value;
            }
        }
        
        public int fk_Value
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Value != null)
                {
                    _fk_Value = Value.ID;
                }
                return _fk_Value;
            }
            set
            {
                _fk_Value = value;
            }
        }
        
        public int fk_Parent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Parent != null)
                {
                    _fk_Parent = Parent.ID;
                }
                return _fk_Parent;
            }
            set
            {
                _fk_Parent = value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_Mitarbeiter", "A_Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ ValueImpl
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Parent", "A_Zeitkonto")]
        public Kistl.App.Zeiterfassung.Zeitkonto__Implementation__ ParentImpl
        {
            get
            {
                EntityReference<Zeitkonto__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zeitkonto__Implementation__>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Parent", "A_Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Zeitkonto__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zeitkonto__Implementation__>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto_fk_Parent", "A_Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = (Zeitkonto__Implementation__)value;
            }
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.fk_Value, sw);
            BinarySerializer.ToBinary(this.fk_Parent, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._fk_Value, sr);
            BinarySerializer.FromBinary(out this._fk_Parent, sr);
        }
    }
}
