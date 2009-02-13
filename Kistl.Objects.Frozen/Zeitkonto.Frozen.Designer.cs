
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Zeitkonto")]
    public class Zeitkonto__Implementation__ : BaseFrozenDataObject, Zeitkonto
    {


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
        /// Tätigkeiten
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Zeiterfassung.Taetigkeit> Taetigkeiten
        {
            get
            {
                if (_Taetigkeiten == null)
                    _Taetigkeiten = new List<Kistl.App.Zeiterfassung.Taetigkeit>();
                return _Taetigkeiten;
            }
        }
        private ICollection<Kistl.App.Zeiterfassung.Taetigkeit> _Taetigkeiten;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (_Mitarbeiter == null)
                    _Mitarbeiter = new List<Kistl.App.Projekte.Mitarbeiter>();
                return _Mitarbeiter;
            }
        }
        private ICollection<Kistl.App.Projekte.Mitarbeiter> _Mitarbeiter;

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


        internal Zeitkonto__Implementation__(FrozenContext ctx, int id)
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