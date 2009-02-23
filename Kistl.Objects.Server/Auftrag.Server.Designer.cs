
namespace Kistl.App.Projekte
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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Auftrag")]
    [System.Diagnostics.DebuggerDisplay("Auftrag")]
    public class Auftrag__Implementation__ : BaseServerDataObject_EntityFramework, Auftrag
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
        /// Bitte füllen Sie einen sprechenden Auftragsnamen aus
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Auftragsname
        {
            get
            {
                return _Auftragsname;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Auftragsname != value)
                {
                    NotifyPropertyChanging("Auftragsname");
                    _Auftragsname = value;
                    NotifyPropertyChanged("Auftragsname");
                }
            }
        }
        private string _Auftragsname;

        /// <summary>
        /// Wert in EUR des Auftrages
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual double? Auftragswert
        {
            get
            {
                return _Auftragswert;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Auftragswert != value)
                {
                    NotifyPropertyChanging("Auftragswert");
                    _Auftragswert = value;
                    NotifyPropertyChanged("Auftragswert");
                }
            }
        }
        private double? _Auftragswert;

        /// <summary>
        /// Kunde des Projektes
        /// </summary>
    /*
    NewRelation: FK_Auftrag_Kunde_Auftrag_14 
    A: ZeroOrMore Auftrag as Auftrag (site: A, no Relation, prop ID=64)
    B: ZeroOrOne Kunde as Kunde (site: B, no Relation, prop ID=64)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Kunde Kunde
        {
            get
            {
                return Kunde__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Kunde__Implementation__ = (Kistl.App.Projekte.Kunde__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Kunde
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Kunde != null)
                {
                    _fk_Kunde = Kunde.ID;
                }
                return _fk_Kunde;
            }
            set
            {
                _fk_Kunde = value;
            }
        }
        private int? _fk_Kunde;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Auftrag_Kunde_Auftrag_14", "Kunde")]
        public Kistl.App.Projekte.Kunde__Implementation__ Kunde__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Kunde__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Kunde__Implementation__>(
                        "Model.FK_Auftrag_Kunde_Auftrag_14",
                        "Kunde");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Kunde__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Kunde__Implementation__>(
                        "Model.FK_Auftrag_Kunde_Auftrag_14",
                        "Kunde");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Kunde__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>
    /*
    NewRelation: FK_Auftrag_Mitarbeiter_Auftrag_9 
    A: ZeroOrMore Auftrag as Auftrag (site: A, no Relation, prop ID=49)
    B: ZeroOrOne Mitarbeiter as Mitarbeiter (site: B, no Relation, prop ID=49)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Mitarbeiter Mitarbeiter
        {
            get
            {
                return Mitarbeiter__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Mitarbeiter
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Mitarbeiter != null)
                {
                    _fk_Mitarbeiter = Mitarbeiter.ID;
                }
                return _fk_Mitarbeiter;
            }
            set
            {
                _fk_Mitarbeiter = value;
            }
        }
        private int? _fk_Mitarbeiter;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Auftrag_Mitarbeiter_Auftrag_9", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ Mitarbeiter__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Auftrag_Mitarbeiter_Auftrag_9",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Auftrag_Mitarbeiter_Auftrag_9",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Projekt zum Auftrag
        /// </summary>
    /*
    NewRelation: FK_Projekt_Auftrag_Projekt_10 
    A: ZeroOrOne Projekt as Projekt (site: A, from relation ID = 10)
    B: ZeroOrMore Auftrag as Auftraege (site: B, from relation ID = 10)
    Preferred Storage: MergeB
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                return Projekt__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Projekt
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Projekt != null)
                {
                    _fk_Projekt = Projekt.ID;
                }
                return _fk_Projekt;
            }
            set
            {
                _fk_Projekt = value;
            }
        }
        private int? _fk_Projekt;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Auftrag_Projekt_10", "Projekt")]
        public Kistl.App.Projekte.Projekt__Implementation__ Projekt__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Auftrag_Projekt_10",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Auftrag_Projekt_10",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Testmethode zum Erstellen von Rechnungen mit Word
        /// </summary>

		public virtual void RechnungErstellen() 
		{
            // base.RechnungErstellen();
            if (OnRechnungErstellen_Auftrag != null)
            {
				OnRechnungErstellen_Auftrag(this);
			}
        }
		public delegate void RechnungErstellen_Handler<T>(T obj);
		public event RechnungErstellen_Handler<Auftrag> OnRechnungErstellen_Auftrag;



		public override Type GetInterfaceType()
		{
			return typeof(Auftrag);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Auftrag != null)
            {
                OnToString_Auftrag(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Auftrag> OnToString_Auftrag;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Auftrag != null) OnPreSave_Auftrag(this);
        }
        public event ObjectEventHandler<Auftrag> OnPreSave_Auftrag;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Auftrag != null) OnPostSave_Auftrag(this);
        }
        public event ObjectEventHandler<Auftrag> OnPostSave_Auftrag;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Auftragsname, binStream);
            BinarySerializer.ToStream(this._Auftragswert, binStream);
            BinarySerializer.ToStream(this._fk_Kunde, binStream);
            BinarySerializer.ToStream(this._fk_Mitarbeiter, binStream);
            BinarySerializer.ToStream(this._fk_Projekt, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Auftragsname, binStream);
            BinarySerializer.FromStream(out this._Auftragswert, binStream);
            BinarySerializer.FromStream(out this._fk_Kunde, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._fk_Projekt, binStream);
        }

#endregion

    }


}