//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Mitarbeiter")]
    public class Mitarbeiter : BaseServerDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private string _Name;
        
        private System.DateTime? _Geburtstag;
        
        private string _SVNr;
        
        private string _TelefonNummer;
        
        public Mitarbeiter()
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
                return "Mitarbeiter";
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
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter", "B_Projekt_MitarbeiterCollectionEntry")]
        public EntityCollection<Kistl.App.Projekte.Projekt_MitarbeiterCollectionEntry> Projekte
        {
            get
            {
                EntityCollection<Kistl.App.Projekte.Projekt_MitarbeiterCollectionEntry> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Projekte.Projekt_MitarbeiterCollectionEntry>("Model.FK_Projekt_MitarbeiterCollectionEntry_Mitarbeiter", "B_Projekt_MitarbeiterCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.DateTime? Geburtstag
        {
            get
            {
                return _Geburtstag;
            }
            set
            {
                NotifyPropertyChanging("Geburtstag"); 
                _Geburtstag = value; 
                NotifyPropertyChanged("Geburtstag");;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string SVNr
        {
            get
            {
                return _SVNr;
            }
            set
            {
                NotifyPropertyChanging("SVNr"); 
                _SVNr = value; 
                NotifyPropertyChanged("SVNr");;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string TelefonNummer
        {
            get
            {
                return _TelefonNummer;
            }
            set
            {
                NotifyPropertyChanging("TelefonNummer"); 
                _TelefonNummer = value; 
                NotifyPropertyChanged("TelefonNummer");;
            }
        }
        
        public event ToStringHandler<Mitarbeiter> OnToString_Mitarbeiter;
        
        public event ObjectEventHandler<Mitarbeiter> OnPreSave_Mitarbeiter;
        
        public event ObjectEventHandler<Mitarbeiter> OnPostSave_Mitarbeiter;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Mitarbeiter != null)
            {
                OnToString_Mitarbeiter(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Mitarbeiter != null) OnPreSave_Mitarbeiter(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Mitarbeiter != null) OnPostSave_Mitarbeiter(this);
        }
        
        public override object Clone()
        {
            Mitarbeiter obj = new Mitarbeiter();
            CopyTo(obj);
            return obj;
        }
        
        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((Mitarbeiter)obj)._Name = this._Name;
            ((Mitarbeiter)obj)._Geburtstag = this._Geburtstag;
            ((Mitarbeiter)obj)._SVNr = this._SVNr;
            ((Mitarbeiter)obj)._TelefonNummer = this._TelefonNummer;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Name, sw);
            BinarySerializer.ToBinary(this._Geburtstag, sw);
            BinarySerializer.ToBinary(this._SVNr, sw);
            BinarySerializer.ToBinary(this._TelefonNummer, sw);
        }
        
        public override void FromStream(Kistl.API.IKistlContext ctx, System.IO.BinaryReader sr)
        {
            base.FromStream(ctx, sr);
            BinarySerializer.FromBinary(out this._Name, sr);
            BinarySerializer.FromBinary(out this._Geburtstag, sr);
            BinarySerializer.FromBinary(out this._SVNr, sr);
            BinarySerializer.FromBinary(out this._TelefonNummer, sr);
        }
    }
}
