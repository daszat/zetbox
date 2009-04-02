
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
    
		public Taetigkeit__Implementation__()
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
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
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
					var __oldValue = _Datum;
                    NotifyPropertyChanging("Datum", __oldValue, value);
                    _Datum = value;
                    NotifyPropertyChanged("Datum", __oldValue, value);
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
					var __oldValue = _Dauer;
                    NotifyPropertyChanging("Dauer", __oldValue, value);
                    _Dauer = value;
                    NotifyPropertyChanged("Dauer", __oldValue, value);
                }
            }
        }
        private double _Dauer;

        /// <summary>
        /// Mitarbeiter
        /// </summary>
    /*
    Relation: FK_Taetigkeit_Mitarbeiter_Taetigkeit_32
    A: ZeroOrMore Taetigkeit as Taetigkeit
    B: ZeroOrOne Mitarbeiter as Mitarbeiter
    Preferred Storage: Left
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
        [EdmRelationshipNavigationProperty("Model", "FK_Taetigkeit_Mitarbeiter_Taetigkeit_32", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ Mitarbeiter__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Taetigkeit_Mitarbeiter_Taetigkeit_32",
                        "Mitarbeiter");
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
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Taetigkeit_Mitarbeiter_Taetigkeit_32",
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
    Relation: FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_43
    A: ZeroOrMore Taetigkeit as Taetigkeit
    B: ZeroOrOne TaetigkeitsArt as TaetigkeitsArt
    Preferred Storage: Left
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
        public int? fk_TaetigkeitsArt
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
        private int? _fk_TaetigkeitsArt;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_43", "TaetigkeitsArt")]
        public Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__ TaetigkeitsArt__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__>(
                        "Model.FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_43",
                        "TaetigkeitsArt");
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
                EntityReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__>(
                        "Model.FK_Taetigkeit_TaetigkeitsArt_Taetigkeit_43",
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
    Relation: FK_Zeitkonto_Taetigkeit_Zeitkonto_33
    A: One Zeitkonto as Zeitkonto
    B: ZeroOrMore Taetigkeit as Taetigkeiten
    Preferred Storage: Right
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
        public int? fk_Zeitkonto
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
        private int? _fk_Zeitkonto;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Taetigkeit_Zeitkonto_33", "Zeitkonto")]
        public Kistl.App.Zeiterfassung.Zeitkonto__Implementation__ Zeitkonto__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__>(
                        "Model.FK_Zeitkonto_Taetigkeit_Zeitkonto_33",
                        "Zeitkonto");
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
                EntityReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__>(
                        "Model.FK_Zeitkonto_Taetigkeit_Zeitkonto_33",
                        "Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)value;
            }
        }
        
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Taetigkeit));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Taetigkeit)obj;
			var otherImpl = (Taetigkeit__Implementation__)obj;
			var me = (Taetigkeit)this;

			me.Datum = other.Datum;
			me.Dauer = other.Dauer;
			this.fk_Mitarbeiter = otherImpl.fk_Mitarbeiter;
			this.fk_TaetigkeitsArt = otherImpl.fk_TaetigkeitsArt;
			this.fk_Zeitkonto = otherImpl.fk_Zeitkonto;
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



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Mitarbeiter.HasValue)
				Mitarbeiter__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)Context.Find<Kistl.App.Projekte.Mitarbeiter>(_fk_Mitarbeiter.Value);
			else
				Mitarbeiter__Implementation__ = null;
			if (_fk_Zeitkonto.HasValue)
				Zeitkonto__Implementation__ = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)Context.Find<Kistl.App.Zeiterfassung.Zeitkonto>(_fk_Zeitkonto.Value);
			else
				Zeitkonto__Implementation__ = null;
			if (_fk_TaetigkeitsArt.HasValue)
				TaetigkeitsArt__Implementation__ = (Kistl.App.Zeiterfassung.TaetigkeitsArt__Implementation__)Context.Find<Kistl.App.Zeiterfassung.TaetigkeitsArt>(_fk_TaetigkeitsArt.Value);
			else
				TaetigkeitsArt__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Datum, binStream);
            BinarySerializer.ToStream(this._Dauer, binStream);
            BinarySerializer.ToStream(this.fk_Mitarbeiter, binStream);
            BinarySerializer.ToStream(this.fk_TaetigkeitsArt, binStream);
            BinarySerializer.ToStream(this.fk_Zeitkonto, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Datum, binStream);
            BinarySerializer.FromStream(out this._Dauer, binStream);
            {
                var tmp = this.fk_Mitarbeiter;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Mitarbeiter = tmp;
            }
            {
                var tmp = this.fk_TaetigkeitsArt;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_TaetigkeitsArt = tmp;
            }
            {
                var tmp = this.fk_Zeitkonto;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Zeitkonto = tmp;
            }
        }

#endregion

    }


}