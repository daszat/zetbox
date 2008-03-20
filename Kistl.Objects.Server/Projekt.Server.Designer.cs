//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter), "B_Projekt_MitarbeiterCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_MitarbeiterCollectionEntry))]
[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Projekt_MitarbeiterCollectionEntry_Projekt", "A_Projekt", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt), "B_Projekt_MitarbeiterCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt_MitarbeiterCollectionEntry))]

namespace Kistl.App.Projekte
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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Projekt")]
    public class Projekt : BaseServerDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private string _Name;
        
        private System.Double? _AufwandGes;
        
        private string _Kundenname;
        
        public Projekt()
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
        
        public override string EntitySetName
        {
            get
            {
                return "Projekt";
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                NotifyPropertyChanging("Name"); 
                _Name = value; 
                NotifyPropertyChanged("Name");;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Task_Projekt", "B_Task")]
        public EntityCollection<Kistl.App.Projekte.Task> Tasks
        {
            get
            {
                EntityCollection<Kistl.App.Projekte.Task> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Projekte.Task>("Model.FK_Task_Projekt", "B_Task");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Projekt_MitarbeiterCollectionEntry_Projekt", "B_Projekt_MitarbeiterCollectionEntry")]
        public EntityCollection<Projekt_MitarbeiterCollectionEntry> Mitarbeiter
        {
            get
            {
                EntityCollection<Projekt_MitarbeiterCollectionEntry> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Projekt_MitarbeiterCollectionEntry>("Model.FK_Projekt_MitarbeiterCollectionEntry_Projekt", "B_Projekt_MitarbeiterCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.Double? AufwandGes
        {
            get
            {
                return _AufwandGes;
            }
            set
            {
                NotifyPropertyChanging("AufwandGes"); 
                _AufwandGes = value; 
                NotifyPropertyChanged("AufwandGes");;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Kundenname
        {
            get
            {
                return _Kundenname;
            }
            set
            {
                NotifyPropertyChanging("Kundenname"); 
                _Kundenname = value; 
                NotifyPropertyChanged("Kundenname");;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Kostentraeger_Projekt", "B_Kostentraeger")]
        public EntityCollection<Kistl.App.Zeiterfassung.Kostentraeger> Kostentraeger
        {
            get
            {
                EntityCollection<Kistl.App.Zeiterfassung.Kostentraeger> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Zeiterfassung.Kostentraeger>("Model.FK_Kostentraeger_Projekt", "B_Kostentraeger");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Auftrag_Projekt", "B_Auftrag")]
        public EntityCollection<Kistl.App.Projekte.Auftrag> Auftraege
        {
            get
            {
                EntityCollection<Kistl.App.Projekte.Auftrag> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Projekte.Auftrag>("Model.FK_Auftrag_Projekt", "B_Auftrag");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        public event ToStringHandler<Projekt> OnToString_Projekt;
        
        public event ObjectEventHandler<Projekt> OnPreSave_Projekt;
        
        public event ObjectEventHandler<Projekt> OnPostSave_Projekt;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Projekt != null)
            {
                OnToString_Projekt(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Projekt != null) OnPreSave_Projekt(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Projekt != null) OnPostSave_Projekt(this);
        }
        
        public override object Clone()
        {
            Projekt obj = new Projekt();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((Projekt)obj)._Name = this._Name;
            ((Projekt)obj)._AufwandGes = this._AufwandGes;
            ((Projekt)obj)._Kundenname = this._Kundenname;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Name, sw);
            BinarySerializer.ToBinary(this.Mitarbeiter, sw);
            BinarySerializer.ToBinary(this._AufwandGes, sw);
            BinarySerializer.ToBinary(this._Kundenname, sw);
        }
        
        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
            BinarySerializer.FromBinary(out this._Name, sr);
            BinarySerializer.FromBinaryCollectionEntries(this.Mitarbeiter, sr, ctx);
            BinarySerializer.FromBinary(out this._AufwandGes, sr);
            BinarySerializer.FromBinary(out this._Kundenname, sr);
        }
    }
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Projekt_MitarbeiterCollectionEntry")]
    public class Projekt_MitarbeiterCollectionEntry : Kistl.API.Server.BaseServerCollectionEntry
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
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter Value
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter>("Model.FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter>("Model.FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        [XmlIgnore()]
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Projekt_MitarbeiterCollectionEntry_Projekt", "A_Projekt")]
        public Projekt Parent
        {
            get
            {
                EntityReference<Projekt> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Projekt>("Model.FK_Projekt_MitarbeiterCollectionEntry_Projekt", "A_Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Projekt> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Projekt>("Model.FK_Projekt_MitarbeiterCollectionEntry_Projekt", "A_Projekt");
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
        
        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
            BinarySerializer.FromBinary(out this._fk_Value, sr);
            BinarySerializer.FromBinary(out this._fk_Parent, sr);
        }
        
        public override void CopyTo(Kistl.API.ICollectionEntry obj)
        {
            base.CopyTo(obj);
            ((Projekt_MitarbeiterCollectionEntry)obj)._fk_Value = this.fk_Value;
            ((Projekt_MitarbeiterCollectionEntry)obj)._fk_Parent = this.fk_Parent;
        }
    }
}
