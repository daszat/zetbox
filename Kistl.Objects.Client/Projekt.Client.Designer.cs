
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

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Projekt")]
    public class Projekt__Implementation__ : BaseClientDataObject, Projekt
    {


        /// <summary>
        /// Aufträge
        /// </summary>
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
                    List<Kistl.App.Projekte.Auftrag> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Auftrag>(this, "Auftraege");
                    else
                        serverList = new List<Kistl.App.Projekte.Auftrag>();
                        
                    _AuftraegeWrapper = new BackReferenceCollection<Kistl.App.Projekte.Auftrag>(
                        "Projekt",
                        this,
                        serverList);
                }
                return _AuftraegeWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Projekte.Auftrag> _AuftraegeWrapper;

        /// <summary>
        /// 
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("AufwandGes");
                }
            }
        }
        private double? _AufwandGes;

        /// <summary>
        /// Kostenträger
        /// </summary>
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
                    List<Kistl.App.Zeiterfassung.Kostentraeger> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Zeiterfassung.Kostentraeger>(this, "Kostentraeger");
                    else
                        serverList = new List<Kistl.App.Zeiterfassung.Kostentraeger>();
                        
                    _KostentraegerWrapper = new BackReferenceCollection<Kistl.App.Zeiterfassung.Kostentraeger>(
                        "Projekt",
                        this,
                        serverList);
                }
                return _KostentraegerWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Zeiterfassung.Kostentraeger> _KostentraegerWrapper;

        /// <summary>
        /// Bitte geben Sie den Kundennamen ein
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Kundenname");
                }
            }
        }
        private string _Kundenname;

        /// <summary>
        /// 
        /// </summary>
        // collection reference property

		public IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
		{
			get
			{
				if (_Mitarbeiter == null)
				{
					_Mitarbeiter 
						= new ClientListBSideWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__>(
							this, 
							(ICollection<Projekt_Mitarbeiter23CollectionEntry__Implementation__>)Context.FetchRelation<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__>(RelationEndRole.A, this));
				}
				return _Mitarbeiter;
			}
		}

		private ClientListBSideWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__> _Mitarbeiter;

        /// <summary>
        /// Projektname
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private string _Name;

        /// <summary>
        /// 
        /// </summary>
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
                    List<Kistl.App.Projekte.Task> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Task>(this, "Tasks");
                    else
                        serverList = new List<Kistl.App.Projekte.Task>();
                        
                    _TasksWrapper = new BackReferenceCollection<Kistl.App.Projekte.Task>(
                        "Projekt",
                        this,
                        serverList);
                }
                return _TasksWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Projekte.Task> _TasksWrapper;

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
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AufwandGes, binStream);
            BinarySerializer.FromStream(out this._Kundenname, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

#endregion

    }


}