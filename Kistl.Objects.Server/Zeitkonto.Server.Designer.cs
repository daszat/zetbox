
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
    [EdmEntityType(NamespaceName="Model", Name="Zeitkonto")]
    [System.Diagnostics.DebuggerDisplay("Zeitkonto")]
    public class Zeitkonto__Implementation__ : BaseServerDataObject_EntityFramework, Zeitkonto
    {
    
		public Zeitkonto__Implementation__()
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
        /// Aktuell gebuchte Stunden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual double? AktuelleStunden
        {
            get
            {
                return _AktuelleStunden;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AktuelleStunden != value)
                {
					var __oldValue = _AktuelleStunden;
                    NotifyPropertyChanging("AktuelleStunden", __oldValue, value);
                    _AktuelleStunden = value;
                    NotifyPropertyChanged("AktuelleStunden", __oldValue, value);
                }
            }
        }
        private double? _AktuelleStunden;

        /// <summary>
        /// Name des Zeiterfassungskontos
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Kontoname
        {
            get
            {
                return _Kontoname;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Kontoname != value)
                {
					var __oldValue = _Kontoname;
                    NotifyPropertyChanging("Kontoname", __oldValue, value);
                    _Kontoname = value;
                    NotifyPropertyChanged("Kontoname", __oldValue, value);
                }
            }
        }
        private string _Kontoname;

        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual double? MaxStunden
        {
            get
            {
                return _MaxStunden;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MaxStunden != value)
                {
					var __oldValue = _MaxStunden;
                    NotifyPropertyChanging("MaxStunden", __oldValue, value);
                    _MaxStunden = value;
                    NotifyPropertyChanged("MaxStunden", __oldValue, value);
                }
            }
        }
        private double? _MaxStunden;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
    /*
    Relation: FK_Zeitkonto_Mitarbeiter_Zeitkonto_42
    A: ZeroOrMore Zeitkonto as Zeitkonto
    B: ZeroOrMore Mitarbeiter as Mitarbeiter
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (_MitarbeiterWrapper == null)
                {
                    _MitarbeiterWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__>(
                            this,
                            Mitarbeiter__Implementation__);
                }
                return _MitarbeiterWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Mitarbeiter_Zeitkonto_42", "CollectionEntry")]
        public EntityCollection<Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__> Mitarbeiter__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__>(
                        "Model.FK_Zeitkonto_Mitarbeiter_Zeitkonto_42",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__> _MitarbeiterWrapper;
        

        /// <summary>
        /// Tätigkeiten
        /// </summary>
    /*
    Relation: FK_Zeitkonto_Taetigkeit_Zeitkonto_33
    A: One Zeitkonto as Zeitkonto
    B: ZeroOrMore Taetigkeit as Taetigkeiten
    Preferred Storage: Right
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Zeiterfassung.Taetigkeit> Taetigkeiten
        {
            get
            {
                if (_TaetigkeitenWrapper == null)
                {
                    _TaetigkeitenWrapper = new EntityCollectionWrapper<Kistl.App.Zeiterfassung.Taetigkeit, Kistl.App.Zeiterfassung.Taetigkeit__Implementation__>(
                            this.Context, Taetigkeiten__Implementation__);
                }
                return _TaetigkeitenWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Taetigkeit_Zeitkonto_33", "Taetigkeiten")]
        public EntityCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__> Taetigkeiten__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__>(
                        "Model.FK_Zeitkonto_Taetigkeit_Zeitkonto_33",
                        "Taetigkeiten");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Zeiterfassung.Taetigkeit, Kistl.App.Zeiterfassung.Taetigkeit__Implementation__> _TaetigkeitenWrapper;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Zeitkonto));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Zeitkonto)obj;
			var otherImpl = (Zeitkonto__Implementation__)obj;
			var me = (Zeitkonto)this;

			me.AktuelleStunden = other.AktuelleStunden;
			me.Kontoname = other.Kontoname;
			me.MaxStunden = other.MaxStunden;
		}

        // tail template

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
        public event ToStringHandler<Zeitkonto> OnToString_Zeitkonto;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Zeitkonto != null) OnPreSave_Zeitkonto(this);
        }
        public event ObjectEventHandler<Zeitkonto> OnPreSave_Zeitkonto;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Zeitkonto != null) OnPostSave_Zeitkonto(this);
        }
        public event ObjectEventHandler<Zeitkonto> OnPostSave_Zeitkonto;



		public override void ReloadReferences()
		{
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AktuelleStunden, binStream);
            BinarySerializer.ToStream(this._Kontoname, binStream);
            BinarySerializer.ToStream(this._MaxStunden, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AktuelleStunden, binStream);
            BinarySerializer.FromStream(out this._Kontoname, binStream);
            BinarySerializer.FromStream(out this._MaxStunden, binStream);
        }

#endregion

    }


}