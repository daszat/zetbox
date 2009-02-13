
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
    [System.Diagnostics.DebuggerDisplay("Kunde")]
    public class Kunde__Implementation__ : BaseFrozenDataObject, Kunde
    {


        /// <summary>
        /// Name des Kunden
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
        /// Adresse & Hausnummer
        /// </summary>
        // value type property
        public virtual string Adresse
        {
            get
            {
                return _Adresse;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Adresse != value)
                {
                    NotifyPropertyChanging("Adresse");
                    _Adresse = value;
                    NotifyPropertyChanged("Adresse");;
                }
            }
        }
        private string _Adresse;

        /// <summary>
        /// Postleitzahl
        /// </summary>
        // value type property
        public virtual string PLZ
        {
            get
            {
                return _PLZ;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PLZ != value)
                {
                    NotifyPropertyChanging("PLZ");
                    _PLZ = value;
                    NotifyPropertyChanged("PLZ");;
                }
            }
        }
        private string _PLZ;

        /// <summary>
        /// Ort
        /// </summary>
        // value type property
        public virtual string Ort
        {
            get
            {
                return _Ort;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Ort != value)
                {
                    NotifyPropertyChanging("Ort");
                    _Ort = value;
                    NotifyPropertyChanged("Ort");;
                }
            }
        }
        private string _Ort;

        /// <summary>
        /// Land
        /// </summary>
        // value type property
        public virtual string Land
        {
            get
            {
                return _Land;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Land != value)
                {
                    NotifyPropertyChanging("Land");
                    _Land = value;
                    NotifyPropertyChanged("Land");;
                }
            }
        }
        private string _Land;

        /// <summary>
        /// EMails des Kunden - können mehrere sein
        /// </summary>
        // value list property
        public virtual ICollection<System.String> EMails
        {
            get
            {
                if (_EMails == null)
                    _EMails = new List<System.String>();
                return _EMails;
            }
        }
        private ICollection<System.String> _EMails;

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Kunde != null)
            {
                OnToString_Kunde(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Kunde> OnToString_Kunde;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Kunde != null) OnPreSave_Kunde(this);
        }
        public event ObjectEventHandler<Kunde> OnPreSave_Kunde;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Kunde != null) OnPostSave_Kunde(this);
        }
        public event ObjectEventHandler<Kunde> OnPostSave_Kunde;


        internal Kunde__Implementation__(FrozenContext ctx, int id)
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