//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter), "B_Zeitkonto_MitarbeiterCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto", "A_Zeitkonto", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Zeiterfassung.Zeitkonto), "B_Zeitkonto_MitarbeiterCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Zeiterfassung.Zeitkonto_MitarbeiterCollectionEntry))]

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
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Zeitkonto")]
    public class Zeitkonto : BaseServerDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private string _Kontoname;
        
        private System.Double? _MaxStunden;
        
        private System.Double? _AktuelleStunden;
        
        public Zeitkonto()
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
                NotifyPropertyChanging("Kontoname"); 
                _Kontoname = value; 
                NotifyPropertyChanged("Kontoname");;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Taetigkeit_Zeitkonto", "B_Taetigkeit")]
        public EntityCollection<Kistl.App.Zeiterfassung.Taetigkeit> Taetigkeiten
        {
            get
            {
                EntityCollection<Kistl.App.Zeiterfassung.Taetigkeit> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Zeiterfassung.Taetigkeit>("Model.FK_Taetigkeit_Zeitkonto", "B_Taetigkeit");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto", "B_Zeitkonto_MitarbeiterCollectionEntry")]
        public EntityCollection<Zeitkonto_MitarbeiterCollectionEntry> Mitarbeiter
        {
            get
            {
                EntityCollection<Zeitkonto_MitarbeiterCollectionEntry> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Zeitkonto_MitarbeiterCollectionEntry>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto", "B_Zeitkonto_MitarbeiterCollectionEntry");
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
                NotifyPropertyChanging("MaxStunden"); 
                _MaxStunden = value; 
                NotifyPropertyChanged("MaxStunden");;
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
                NotifyPropertyChanging("AktuelleStunden"); 
                _AktuelleStunden = value; 
                NotifyPropertyChanged("AktuelleStunden");;
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
        
        public override object Clone()
        {
            Zeitkonto obj = new Zeitkonto();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((Zeitkonto)obj)._Kontoname = this._Kontoname;
            ((Zeitkonto)obj)._MaxStunden = this._MaxStunden;
            ((Zeitkonto)obj)._AktuelleStunden = this._AktuelleStunden;
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            Mitarbeiter.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i));
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Kontoname, sw);
            BinarySerializer.ToBinary(this.Mitarbeiter, sw);
            BinarySerializer.ToBinary(this._MaxStunden, sw);
            BinarySerializer.ToBinary(this._AktuelleStunden, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Kontoname, sr);
            BinarySerializer.FromBinaryCollectionEntries(this.Mitarbeiter, sr);
            BinarySerializer.FromBinary(out this._MaxStunden, sr);
            BinarySerializer.FromBinary(out this._AktuelleStunden, sr);
        }
    }
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Zeitkonto_MitarbeiterCollectionEntry")]
    public class Zeitkonto_MitarbeiterCollectionEntry : Kistl.API.Server.BaseServerCollectionEntry
    {
        
        private int _ID = Helper.INVALIDID;
        
        private int _fk_Value = Helper.INVALIDID;
        
        private int _fk_Parent = Helper.INVALIDID;
        
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
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter Value
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto", "A_Zeitkonto")]
        public Zeitkonto Parent
        {
            get
            {
                EntityReference<Zeitkonto> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zeitkonto>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto", "A_Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Zeitkonto> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zeitkonto>("Model.FK_Zeitkonto_MitarbeiterCollectionEntry_Zeitkonto", "A_Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_Value
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Value == Helper.INVALIDID && Value != null)
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
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Parent == Helper.INVALIDID && Parent != null)
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
        
        public override void CopyTo(Kistl.API.ICollectionEntry obj)
        {
            base.CopyTo(obj);
            ((Zeitkonto_MitarbeiterCollectionEntry)obj)._fk_Value = this.fk_Value;
            ((Zeitkonto_MitarbeiterCollectionEntry)obj)._fk_Parent = this.fk_Parent;
        }
    }
}
