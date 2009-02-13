
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
    [System.Diagnostics.DebuggerDisplay("Mitarbeiter")]
    public class Mitarbeiter__Implementation__ : BaseFrozenDataObject, Mitarbeiter
    {


        /// <summary>
        /// Vorname Nachname
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
                    NotifyPropertyChanged("Name");;
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Projekte des Mitarbeiters für die er Verantwortlich ist
        /// </summary>
        // object reference list property
        public virtual IList<Kistl.App.Projekte.Projekt> Projekte
        {
            get
            {
                if (_Projekte == null)
                    _Projekte = new List<Kistl.App.Projekte.Projekt>();
                return _Projekte;
            }
        }
        private IList<Kistl.App.Projekte.Projekt> _Projekte;

        /// <summary>
        /// Herzlichen Glückwunsch zum Geburtstag
        /// </summary>
        // value type property
        public virtual DateTime? Geburtstag
        {
            get
            {
                return _Geburtstag;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Geburtstag != value)
                {
                    NotifyPropertyChanging("Geburtstag");
                    _Geburtstag = value;
                    NotifyPropertyChanged("Geburtstag");;
                }
            }
        }
        private DateTime? _Geburtstag;

        /// <summary>
        /// NNNN TTMMYY
        /// </summary>
        // value type property
        public virtual string SVNr
        {
            get
            {
                return _SVNr;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_SVNr != value)
                {
                    NotifyPropertyChanging("SVNr");
                    _SVNr = value;
                    NotifyPropertyChanged("SVNr");;
                }
            }
        }
        private string _SVNr;

        /// <summary>
        /// +43 123 12345678
        /// </summary>
        // value type property
        public virtual string TelefonNummer
        {
            get
            {
                return _TelefonNummer;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TelefonNummer != value)
                {
                    NotifyPropertyChanging("TelefonNummer");
                    _TelefonNummer = value;
                    NotifyPropertyChanged("TelefonNummer");;
                }
            }
        }
        private string _TelefonNummer;

        /// <summary>
        /// 
        /// </summary>

		public virtual DateTime TestMethodForParameter(System.String TestString, System.Int32 TestInt, System.Double TestDouble, System.Boolean TestBool, System.DateTime TestDateTime, Kistl.App.Projekte.Auftrag TestObjectParameter, System.Guid TestCLRObjectParameter) 
        {
            var e = new MethodReturnEventArgs<DateTime>();
            if (OnTestMethodForParameter_Mitarbeiter != null)
            {
                OnTestMethodForParameter_Mitarbeiter(this, e, TestString, TestInt, TestDouble, TestBool, TestDateTime, TestObjectParameter, TestCLRObjectParameter);
            };
            return e.Result;
        }
		public delegate void TestMethodForParameter_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret, System.String TestString, System.Int32 TestInt, System.Double TestDouble, System.Boolean TestBool, System.DateTime TestDateTime, Kistl.App.Projekte.Auftrag TestObjectParameter, System.Guid TestCLRObjectParameter);
		public event TestMethodForParameter_Handler<Mitarbeiter> OnTestMethodForParameter_Mitarbeiter;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Mitarbeiter != null)
            {
                OnToString_Mitarbeiter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Mitarbeiter> OnToString_Mitarbeiter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Mitarbeiter != null) OnPreSave_Mitarbeiter(this);
        }
        public event ObjectEventHandler<Mitarbeiter> OnPreSave_Mitarbeiter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Mitarbeiter != null) OnPostSave_Mitarbeiter(this);
        }
        public event ObjectEventHandler<Mitarbeiter> OnPostSave_Mitarbeiter;


        internal Mitarbeiter__Implementation__(FrozenContext ctx, int id)
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