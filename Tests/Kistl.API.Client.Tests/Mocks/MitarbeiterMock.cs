using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.App.Projekte
{
    public class Mitarbeiter__Implementation__ : Kistl.API.Client.Mocks.BaseClientDataObjectMock__Implementation__, Kistl.App.Projekte.Mitarbeiter
    {
        #region Mitarbeiter Members

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
                    NotifyPropertyChanging("Geburtstag", null, null);
                    _Geburtstag = value;
                    NotifyPropertyChanged("Geburtstag", null, null);
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
                    NotifyPropertyChanging("Name", null, null);
                    _Name = value;
                    NotifyPropertyChanged("Name", null, null);
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Projekte des Mitarbeiters für die er Verantwortlich ist
        /// </summary>
        // collection reference property

        public IList<Kistl.App.Projekte.Projekt> Projekte
        {
            get
            {
                if (_Projekte == null)
                {
                    _Projekte
                        = new ClientRelationASideListWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__>(
                            this,
                            Context.FetchRelation<Projekt_Mitarbeiter23CollectionEntry__Implementation__>(23, RelationEndRole.B, this));
                }
                return _Projekte;
            }
        }

        private ClientRelationASideListWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__> _Projekte;

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
                    NotifyPropertyChanging("SVNr", null, null);
                    _SVNr = value;
                    NotifyPropertyChanged("SVNr", null, null);
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
                    NotifyPropertyChanging("TelefonNummer", null, null);
                    _TelefonNummer = value;
                    NotifyPropertyChanged("TelefonNummer", null, null);
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
            }
            else
            {
                throw new NotImplementedException("No handler registered on Mitarbeiter.TestMethodForParameter");
            }
            return e.Result;
        }
        public delegate void TestMethodForParameter_Handler<T>(T obj, MethodReturnEventArgs<DateTime> ret, System.String TestString, System.Int32 TestInt, System.Double TestDouble, System.Boolean TestBool, System.DateTime TestDateTime, Kistl.App.Projekte.Auftrag TestObjectParameter, System.Guid TestCLRObjectParameter);
        public event TestMethodForParameter_Handler<Mitarbeiter> OnTestMethodForParameter_Mitarbeiter;

        #endregion
    }
}
