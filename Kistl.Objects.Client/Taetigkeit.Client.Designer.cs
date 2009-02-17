
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
    [System.Diagnostics.DebuggerDisplay("Taetigkeit")]
    public class Taetigkeit__Implementation__ : BaseClientDataObject, Taetigkeit
    {


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
        /// Mitarbeiter
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Mitarbeiter Mitarbeiter
        {
            get
            {
                if (fk_Mitarbeiter.HasValue)
                    return Context.Find<Kistl.App.Projekte.Mitarbeiter>(fk_Mitarbeiter.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Mitarbeiter
        {
            get
            {
                return _fk_Mitarbeiter;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Mitarbeiter != value)
                {
                    NotifyPropertyChanging("Mitarbeiter");
                    _fk_Mitarbeiter = value;
                    NotifyPropertyChanging("Mitarbeiter");
                }
            }
        }
        private int? _fk_Mitarbeiter;

        /// <summary>
        /// Art der Tätigkeit
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.TaetigkeitsArt TaetigkeitsArt
        {
            get
            {
                if (fk_TaetigkeitsArt.HasValue)
                    return Context.Find<Kistl.App.Zeiterfassung.TaetigkeitsArt>(fk_TaetigkeitsArt.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_TaetigkeitsArt
        {
            get
            {
                return _fk_TaetigkeitsArt;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_TaetigkeitsArt != value)
                {
                    NotifyPropertyChanging("TaetigkeitsArt");
                    _fk_TaetigkeitsArt = value;
                    NotifyPropertyChanging("TaetigkeitsArt");
                }
            }
        }
        private int? _fk_TaetigkeitsArt;

        /// <summary>
        /// Zeitkonto
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.Zeitkonto Zeitkonto
        {
            get
            {
                if (fk_Zeitkonto.HasValue)
                    return Context.Find<Kistl.App.Zeiterfassung.Zeitkonto>(fk_Zeitkonto.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = Zeitkonto;
                if (value != null && value.ID != fk_Zeitkonto)
                {
                    oldValue.Taetigkeiten.Remove(this);
                    fk_Zeitkonto = value.ID;
                    value.Taetigkeiten.Add(this);
                }
                else
                {
                    oldValue.Taetigkeiten.Remove(this);
                    fk_Zeitkonto = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Zeitkonto
        {
            get
            {
                return _fk_Zeitkonto;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Zeitkonto != value)
                {
                    NotifyPropertyChanging("Zeitkonto");
                    _fk_Zeitkonto = value;
                    NotifyPropertyChanging("Zeitkonto");
                }
            }
        }
        private int? _fk_Zeitkonto;

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