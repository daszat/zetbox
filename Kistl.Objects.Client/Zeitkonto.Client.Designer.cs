
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

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Zeitkonto")]
    public class Zeitkonto__Implementation__ : BaseClientDataObject, Zeitkonto
    {


        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("AktuelleStunden");;
                }
            }
        }
        private double? _AktuelleStunden;

        /// <summary>
        /// Name des Zeiterfassungskontos
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Kontoname");;
                }
            }
        }
        private string _Kontoname;

        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("MaxStunden");;
                }
            }
        }
        private double? _MaxStunden;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (_MitarbeiterWrapper == null)
                {
                    List<Kistl.App.Projekte.Mitarbeiter> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Mitarbeiter>(this, "Mitarbeiter");
                    else
                        serverList = new List<Kistl.App.Projekte.Mitarbeiter>();
                        
                    _MitarbeiterWrapper = new BackReferenceCollection<Kistl.App.Projekte.Mitarbeiter>(
                        "Zeitkonto",
                        this,
                        serverList);
                }
                return _MitarbeiterWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Projekte.Mitarbeiter> _MitarbeiterWrapper;

        /// <summary>
        /// Tätigkeiten
        /// </summary>
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
                    List<Kistl.App.Zeiterfassung.Taetigkeit> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Zeiterfassung.Taetigkeit>(this, "Taetigkeiten");
                    else
                        serverList = new List<Kistl.App.Zeiterfassung.Taetigkeit>();
                        
                    _TaetigkeitenWrapper = new BackReferenceCollection<Kistl.App.Zeiterfassung.Taetigkeit>(
                        "Zeitkonto",
                        this,
                        serverList);
                }
                return _TaetigkeitenWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Zeiterfassung.Taetigkeit> _TaetigkeitenWrapper;

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