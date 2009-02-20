
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
    [System.Diagnostics.DebuggerDisplay("Mitarbeiter")]
    public class Mitarbeiter__Implementation__ : BaseClientDataObject, Mitarbeiter
    {


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
                    NotifyPropertyChanged("Geburtstag");
                }
            }
        }
        private DateTime? _Geburtstag;

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
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Projekte des Mitarbeiters für die er Verantwortlich ist
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Projekte.Projekt> Projekte
        {
            get
            {
                if (_ProjekteWrapper == null)
                {
                    List<Kistl.App.Projekte.Projekt> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Projekt>(this, "Projekte");
                    else
                        serverList = new List<Kistl.App.Projekte.Projekt>();
                        
                    _ProjekteWrapper = new BackReferenceCollection<Kistl.App.Projekte.Projekt>(
                        "Mitarbeiter",
                        this,
                        serverList);
                }
                return _ProjekteWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Projekte.Projekt> _ProjekteWrapper;

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
                    NotifyPropertyChanged("SVNr");
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
                    NotifyPropertyChanged("TelefonNummer");
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



		public override Type GetInterfaceType()
		{
			return typeof(Mitarbeiter);
		}

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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Geburtstag, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._SVNr, binStream);
            BinarySerializer.ToStream(this._TelefonNummer, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Geburtstag, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._SVNr, binStream);
            BinarySerializer.FromStream(out this._TelefonNummer, binStream);
        }

#endregion

    }


}