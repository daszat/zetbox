
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
                    NotifyPropertyChanging("AktuelleStunden");
                    _AktuelleStunden = value;
                    NotifyPropertyChanged("AktuelleStunden");
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
                    NotifyPropertyChanging("Kontoname");
                    _Kontoname = value;
                    NotifyPropertyChanged("Kontoname");
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
                    NotifyPropertyChanging("MaxStunden");
                    _MaxStunden = value;
                    NotifyPropertyChanged("MaxStunden");
                }
            }
        }
        private double? _MaxStunden;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
    /*
    NewRelation: FK_Zeitkonto_Mitarbeiter_Zeitkonto_22 
    A: ZeroOrMore Zeitkonto as Zeitkonto (site: A, no Relation, prop ID=86)
    B: ZeroOrMore Mitarbeiter as Mitarbeiter (site: B, no Relation, prop ID=86)
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
                    _MitarbeiterWrapper = new EntityCollectionBSideWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__>(
                            this,
                            Mitarbeiter__Implementation__);
                }
                return _MitarbeiterWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Mitarbeiter_Zeitkonto_22", "CollectionEntry")]
        public EntityCollection<Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__> Mitarbeiter__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__>(
                        "Model.FK_Zeitkonto_Mitarbeiter_Zeitkonto_22",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionBSideWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__> _MitarbeiterWrapper;
        

        /// <summary>
        /// Tätigkeiten
        /// </summary>
    /*
    NewRelation: FK_Zeitkonto_Taetigkeit_Zeitkonto_13 
    A: One Zeitkonto as Zeitkonto (site: A, from relation ID = 8)
    B: ZeroOrMore Taetigkeit as Taetigkeiten (site: B, from relation ID = 8)
    Preferred Storage: MergeB
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
                            Taetigkeiten__Implementation__);
                }
                return _TaetigkeitenWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Taetigkeit_Zeitkonto_13", "Taetigkeiten")]
        public EntityCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__> Taetigkeiten__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Zeiterfassung.Taetigkeit__Implementation__>(
                        "Model.FK_Zeitkonto_Taetigkeit_Zeitkonto_13",
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



		public override Type GetInterfaceType()
		{
			return typeof(Zeitkonto);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AktuelleStunden, binStream);
            BinarySerializer.ToStream(this._Kontoname, binStream);
            BinarySerializer.ToStream(this._MaxStunden, binStream);
            BinarySerializer.ToStreamCollectionEntries(this.Mitarbeiter__Implementation__, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AktuelleStunden, binStream);
            BinarySerializer.FromStream(out this._Kontoname, binStream);
            BinarySerializer.FromStream(out this._MaxStunden, binStream);
            BinarySerializer.FromStreamCollectionEntries(this.Mitarbeiter__Implementation__, binStream);
        }

#endregion

    }


}