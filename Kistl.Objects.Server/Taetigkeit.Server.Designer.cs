
namespace Kistl.App.Zeiterfassung
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
    [EdmEntityType(NamespaceName="Model", Name="Taetigkeit")]
    [System.Diagnostics.DebuggerDisplay("Taetigkeit")]
    public class Taetigkeit__Implementation__ : BaseServerDataObject_EntityFramework, Taetigkeit
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
        /// Datum
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime Datum
        {
            get
            {
                return _Datum;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Datum != value)
                {
                    NotifyPropertyChanging("Datum");
                    _Datum = value;
                    NotifyPropertyChanged("Datum");
                }
            }
        }
        private DateTime _Datum;

        /// <summary>
        /// Dauer in Stunden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual double Dauer
        {
            get
            {
                return _Dauer;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Dauer != value)
                {
                    NotifyPropertyChanging("Dauer");
                    _Dauer = value;
                    NotifyPropertyChanged("Dauer");
                }
            }
        }
        private double _Dauer;

        /// <summary>
        /// Mitarbeiter
        /// </summary>
    /*
    NewRelation: FK_Taetigkeit_Mitarbeiter_Taetigkeit_12 
    A: ZeroOrMore Taetigkeit as Taetigkeit (site: A, no Relation, prop ID=54)
    B: ZeroOrOne Mitarbeiter as Mitarbeiter (site: B, no Relation, prop ID=54)
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
        public int fk_Mitarbeiter
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
        private int _fk_Mitarbeiter;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Taetigkeit_Mitarbeiter_Taetigkeit_12", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ Mitarbeiter__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Taetigkeit_Mitarbeiter_Taetigkeit_12",
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
                        "Model.FK_Taetigkeit_Mitarbeiter_Taetigkeit_12",
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
        /// Art der Tätigkeit
        /// </summary>
    /*
    NewRelation: FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_23 
    A: ZeroOrMore Taetigkeit as Taetigkeit (site: A, no Relation, prop ID=88)
    B: ZeroOrOne TaetigkeitsArt as TaetigkeitsArt (site: B, no Relation, prop ID=88)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.TaetigkeitsArt TaetigkeitsArt
        {
            get
            {
                return TaetigkeitsArt__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                TaetigkeitsArt__Implementation__ = (Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_TaetigkeitsArt
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && TaetigkeitsArt != null)
                {
                    _fk_TaetigkeitsArt = TaetigkeitsArt.ID;
                }
                return _fk_TaetigkeitsArt;
            }
            set
            {
                _fk_TaetigkeitsArt = value;
            }
        }
        private int _fk_TaetigkeitsArt;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_23", "TaetigkeitsArt")]
        public Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__ TaetigkeitsArt__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__>(
                        "Model.FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_23",
                        "TaetigkeitsArt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__>(
                        "Model.FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_23",
                        "TaetigkeitsArt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Zeitkonto
        /// </summary>
    /*
    NewRelation: FK_Zeitkonto_Taetigkeit_Zeitkonto_13 
    A: One Zeitkonto as Zeitkonto (site: A, from relation ID = 8)
    B: ZeroOrMore Taetigkeit as Taetigkeiten (site: B, from relation ID = 8)
    Preferred Storage: MergeB
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.Zeitkonto Zeitkonto
        {
            get
            {
                return Zeitkonto__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Zeitkonto__Implementation__ = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_Zeitkonto
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Zeitkonto != null)
                {
                    _fk_Zeitkonto = Zeitkonto.ID;
                }
                return _fk_Zeitkonto;
            }
            set
            {
                _fk_Zeitkonto = value;
            }
        }
        private int _fk_Zeitkonto;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Taetigkeit_Zeitkonto_13", "Zeitkonto")]
        public Kistl.App.Zeiterfassung.Zeitkonto__Implementation__ Zeitkonto__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__>(
                        "Model.FK_Zeitkonto_Taetigkeit_Zeitkonto_13",
                        "Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__>(
                        "Model.FK_Zeitkonto_Taetigkeit_Zeitkonto_13",
                        "Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)value;
            }
        }
        
        

		public override Type GetInterfaceType()
		{
			return typeof(Taetigkeit);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Taetigkeit != null)
            {
                OnToString_Taetigkeit(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Taetigkeit> OnToString_Taetigkeit;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Taetigkeit != null) OnPreSave_Taetigkeit(this);
        }
        public event ObjectEventHandler<Taetigkeit> OnPreSave_Taetigkeit;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Taetigkeit != null) OnPostSave_Taetigkeit(this);
        }
        public event ObjectEventHandler<Taetigkeit> OnPostSave_Taetigkeit;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Datum, binStream);
            BinarySerializer.ToStream(this._Dauer, binStream);
            BinarySerializer.ToStream(this._fk_Mitarbeiter, binStream);
            BinarySerializer.ToStream(this._fk_TaetigkeitsArt, binStream);
            BinarySerializer.ToStream(this._fk_Zeitkonto, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Datum, binStream);
            BinarySerializer.FromStream(out this._Dauer, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._fk_TaetigkeitsArt, binStream);
            BinarySerializer.FromStream(out this._fk_Zeitkonto, binStream);
        }

#endregion

    }


}