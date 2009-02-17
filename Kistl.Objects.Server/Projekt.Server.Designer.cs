
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
    [EdmEntityType(NamespaceName="Model", Name="Projekt")]
    [System.Diagnostics.DebuggerDisplay("Projekt")]
    public class Projekt__Implementation__ : BaseServerDataObject_EntityFramework, Projekt
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
                    NotifyPropertyChanged("ID");;
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Aufträge
        /// </summary>
    /*
    NewRelation: FK_Projekt_Auftrag_Projekt_10 
    A: ZeroOrOne Projekt as Projekt (site: A, from relation ID = 10)
    B: ZeroOrMore Auftrag as Auftraege (site: B, from relation ID = 10)
    Preferred Storage: MergeB
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Auftrag> Auftraege
        {
            get
            {
                if (_AuftraegeWrapper == null)
                {
                    _AuftraegeWrapper = new EntityCollectionWrapper<Kistl.App.Projekte.Auftrag, Kistl.App.Projekte.Auftrag__Implementation__>(
                            Auftraege__Implementation__);
                }
                return _AuftraegeWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Auftrag_Projekt_10", "Auftraege")]
        public EntityCollection<Kistl.App.Projekte.Auftrag__Implementation__> Auftraege__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Projekte.Auftrag__Implementation__>(
                        "Model.FK_Projekt_Auftrag_Projekt_10",
                        "Auftraege");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Projekte.Auftrag, Kistl.App.Projekte.Auftrag__Implementation__> _AuftraegeWrapper;



        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual double? AufwandGes
        {
            get
            {
                return _AufwandGes;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AufwandGes != value)
                {
                    NotifyPropertyChanging("AufwandGes");
                    _AufwandGes = value;
                    NotifyPropertyChanged("AufwandGes");;
                }
            }
        }
        private double? _AufwandGes;

        /// <summary>
        /// Kostenträger
        /// </summary>
    /*
    NewRelation: FK_Projekt_Kostentraeger_Projekt_11 
    A: One Projekt as Projekt (site: A, from relation ID = 9)
    B: ZeroOrMore Kostentraeger as Kostentraeger (site: B, from relation ID = 9)
    Preferred Storage: MergeB
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Zeiterfassung.Kostentraeger> Kostentraeger
        {
            get
            {
                if (_KostentraegerWrapper == null)
                {
                    _KostentraegerWrapper = new EntityCollectionWrapper<Kistl.App.Zeiterfassung.Kostentraeger, Kistl.App.Zeiterfassung.Kostentraeger__Implementation__>(
                            Kostentraeger__Implementation__);
                }
                return _KostentraegerWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Kostentraeger_Projekt_11", "Kostentraeger")]
        public EntityCollection<Kistl.App.Zeiterfassung.Kostentraeger__Implementation__> Kostentraeger__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Zeiterfassung.Kostentraeger__Implementation__>(
                        "Model.FK_Projekt_Kostentraeger_Projekt_11",
                        "Kostentraeger");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Zeiterfassung.Kostentraeger, Kistl.App.Zeiterfassung.Kostentraeger__Implementation__> _KostentraegerWrapper;



        /// <summary>
        /// Bitte geben Sie den Kundennamen ein
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Kundenname
        {
            get
            {
                return _Kundenname;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Kundenname != value)
                {
                    NotifyPropertyChanging("Kundenname");
                    _Kundenname = value;
                    NotifyPropertyChanged("Kundenname");;
                }
            }
        }
        private string _Kundenname;

        /// <summary>
        /// 
        /// </summary>
    /*
    NewRelation: FK_Projekt_Mitarbeiter_Projekte_3 
    A: ZeroOrMore Projekt as Projekte (site: A, from relation ID = 3)
    B: ZeroOrMore Mitarbeiter as Mitarbeiter (site: B, from relation ID = 3)
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (_MitarbeiterWrapper == null)
                {
                    _MitarbeiterWrapper = new EntityListBSideWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry__Implementation__>(
                            this,
                            Mitarbeiter__Implementation__);
                }
                return _MitarbeiterWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Mitarbeiter_Projekte_3", "CollectionEntry")]
        public EntityCollection<Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry__Implementation__> Mitarbeiter__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry__Implementation__>(
                        "Model.FK_Projekt_Mitarbeiter_Projekte_3",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityListBSideWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry__Implementation__> _MitarbeiterWrapper;
        

        /// <summary>
        /// Projektname
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");;
                }
            }
        }
        private string _Name;

        /// <summary>
        /// 
        /// </summary>
    /*
    NewRelation: FK_Projekt_Task_Projekt_2 
    A: ZeroOrOne Projekt as Projekt (site: A, from relation ID = 2)
    B: ZeroOrMore Task as Tasks (site: B, from relation ID = 2)
    Preferred Storage: MergeB
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Task> Tasks
        {
            get
            {
                if (_TasksWrapper == null)
                {
                    _TasksWrapper = new EntityCollectionWrapper<Kistl.App.Projekte.Task, Kistl.App.Projekte.Task__Implementation__>(
                            Tasks__Implementation__);
                }
                return _TasksWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Task_Projekt_2", "Tasks")]
        public EntityCollection<Kistl.App.Projekte.Task__Implementation__> Tasks__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Projekte.Task__Implementation__>(
                        "Model.FK_Projekt_Task_Projekt_2",
                        "Tasks");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Projekte.Task, Kistl.App.Projekte.Task__Implementation__> _TasksWrapper;



		public override Type GetInterfaceType()
		{
			return typeof(Projekt);
		}

        // tail template

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
        public event ToStringHandler<Projekt> OnToString_Projekt;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Projekt != null) OnPreSave_Projekt(this);
        }
        public event ObjectEventHandler<Projekt> OnPreSave_Projekt;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Projekt != null) OnPostSave_Projekt(this);
        }
        public event ObjectEventHandler<Projekt> OnPostSave_Projekt;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AufwandGes, binStream);
            BinarySerializer.ToStream(this._Kundenname, binStream);
            BinarySerializer.ToStreamCollectionEntries(this.Mitarbeiter__Implementation__, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AufwandGes, binStream);
            BinarySerializer.FromStream(out this._Kundenname, binStream);
            BinarySerializer.FromStreamCollectionEntries(this.Mitarbeiter__Implementation__, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

#endregion

    }


}