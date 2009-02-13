
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
    [System.Diagnostics.DebuggerDisplay("Taetigkeit")]
    public class Taetigkeit__Implementation__ : BaseFrozenDataObject, Taetigkeit
    {


        /// <summary>
        /// Mitarbeiter
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
        /// Zeitkonto
        /// </summary>
        // object reference property
        public virtual Kistl.App.Zeiterfassung.Zeitkonto Zeitkonto
        {
            get
            {
                return _Zeitkonto;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Zeitkonto != value)
                {
                    NotifyPropertyChanging("Zeitkonto");
                    _Zeitkonto = value;
                    NotifyPropertyChanged("Zeitkonto");;
                }
            }
        }
        private Kistl.App.Zeiterfassung.Zeitkonto _Zeitkonto;

        /// <summary>
        /// Datum
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Datum");;
                }
            }
        }
        private DateTime _Datum;

        /// <summary>
        /// Dauer in Stunden
        /// </summary>
        // value type property
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
                    NotifyPropertyChanged("Dauer");;
                }
            }
        }
        private double _Dauer;

        /// <summary>
        /// Art der Tätigkeit
        /// </summary>
        // object reference property
        public virtual Kistl.App.Zeiterfassung.TaetigkeitsArt TaetigkeitsArt
        {
            get
            {
                return _TaetigkeitsArt;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TaetigkeitsArt != value)
                {
                    NotifyPropertyChanging("TaetigkeitsArt");
                    _TaetigkeitsArt = value;
                    NotifyPropertyChanged("TaetigkeitsArt");;
                }
            }
        }
        private Kistl.App.Zeiterfassung.TaetigkeitsArt _TaetigkeitsArt;

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


        internal Taetigkeit__Implementation__(FrozenContext ctx, int id)
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