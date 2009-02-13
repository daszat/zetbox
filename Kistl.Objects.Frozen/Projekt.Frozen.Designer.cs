
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
    [System.Diagnostics.DebuggerDisplay("Projekt")]
    public class Projekt__Implementation__ : BaseFrozenDataObject, Projekt
    {


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
                    NotifyPropertyChanged("Name");;
                }
            }
        }
        private string _Name;

        /// <summary>
        /// 
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Projekte.Task> Tasks
        {
            get
            {
                if (_Tasks == null)
                    _Tasks = new List<Kistl.App.Projekte.Task>();
                return _Tasks;
            }
        }
        private ICollection<Kistl.App.Projekte.Task> _Tasks;

        /// <summary>
        /// 
        /// </summary>
        // object reference list property
        public virtual IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get
            {
                if (_Mitarbeiter == null)
                    _Mitarbeiter = new List<Kistl.App.Projekte.Mitarbeiter>();
                return _Mitarbeiter;
            }
        }
        private IList<Kistl.App.Projekte.Mitarbeiter> _Mitarbeiter;

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
                    NotifyPropertyChanged("AufwandGes");;
                }
            }
        }
        private double? _AufwandGes;

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
                    NotifyPropertyChanged("Kundenname");;
                }
            }
        }
        private string _Kundenname;

        /// <summary>
        /// Kostenträger
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Zeiterfassung.Kostentraeger> Kostentraeger
        {
            get
            {
                if (_Kostentraeger == null)
                    _Kostentraeger = new List<Kistl.App.Zeiterfassung.Kostentraeger>();
                return _Kostentraeger;
            }
        }
        private ICollection<Kistl.App.Zeiterfassung.Kostentraeger> _Kostentraeger;

        /// <summary>
        /// Aufträge
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Projekte.Auftrag> Auftraege
        {
            get
            {
                if (_Auftraege == null)
                    _Auftraege = new List<Kistl.App.Projekte.Auftrag>();
                return _Auftraege;
            }
        }
        private ICollection<Kistl.App.Projekte.Auftrag> _Auftraege;

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


        internal Projekt__Implementation__(FrozenContext ctx, int id)
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