
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Auftrag")]
    public class Auftrag__Implementation__ : BaseFrozenDataObject, Auftrag
    {


        /// <summary>
        /// 
        /// </summary>
        // object reference property
        public virtual Kistl.App.Projekte.Mitarbeiter Mitarbeiter
        {
            get
            {
                return _Mitarbeiter;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Mitarbeiter != value)
                {
                    NotifyPropertyChanging("Mitarbeiter");
                    _Mitarbeiter = value;
                    NotifyPropertyChanged("Mitarbeiter");;
                }
            }
        }
        private Kistl.App.Projekte.Mitarbeiter _Mitarbeiter;

        /// <summary>
        /// Bitte füllen Sie einen sprechenden Auftragsnamen aus
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Auftragsname");;
                }
            }
        }
        private string _Auftragsname;

        /// <summary>
        /// Projekt zum Auftrag
        /// </summary>
        // object reference property
        public virtual Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                return _Projekt;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Projekt != value)
                {
                    NotifyPropertyChanging("Projekt");
                    _Projekt = value;
                    NotifyPropertyChanged("Projekt");;
                }
            }
        }
        private Kistl.App.Projekte.Projekt _Projekt;

        /// <summary>
        /// Kunde des Projektes
        /// </summary>
        // object reference property
        public virtual Kistl.App.Projekte.Kunde Kunde
        {
            get
            {
                return _Kunde;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Kunde != value)
                {
                    NotifyPropertyChanging("Kunde");
                    _Kunde = value;
                    NotifyPropertyChanged("Kunde");;
                }
            }
        }
        private Kistl.App.Projekte.Kunde _Kunde;

        /// <summary>
        /// Wert in EUR des Auftrages
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Auftragswert");;
                }
            }
        }
        private double? _Auftragswert;

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


        internal Auftrag__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}