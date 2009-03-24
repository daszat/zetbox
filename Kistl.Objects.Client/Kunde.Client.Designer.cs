
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
    [System.Diagnostics.DebuggerDisplay("Kunde")]
    public class Kunde__Implementation__ : BaseClientDataObject, Kunde
    {
    
		public Kunde__Implementation__()
		{
            {
            }
        }


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
                    NotifyPropertyChanged("Adresse");
                }
            }
        }
        private string _Adresse;

        /// <summary>
        /// EMails des Kunden - können mehrere sein
        /// </summary>
        // value list property

		public ICollection<string> EMails
		{
			get
			{
				if (_EMailsWrapper == null)
				{
				    _EMailsWrapper 
				        = new ClientCollectionBSideWrapper<Kunde, string, Kunde_EMailsCollectionEntry__Implementation__>(
				            this, 
				            _EMails);
				}
				return _EMailsWrapper;
			}
		}

		private ClientCollectionBSideWrapper<Kunde, string, Kunde_EMailsCollectionEntry__Implementation__> _EMailsWrapper;
		private ICollection<Kunde_EMailsCollectionEntry__Implementation__> _EMails = new List<Kunde_EMailsCollectionEntry__Implementation__>();

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
                    NotifyPropertyChanged("Kundenname");
                }
            }
        }
        private string _Kundenname;

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
                    NotifyPropertyChanged("Land");
                }
            }
        }
        private string _Land;

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
                    NotifyPropertyChanged("Ort");
                }
            }
        }
        private string _Ort;

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
                    NotifyPropertyChanged("PLZ");
                }
            }
        }
        private string _PLZ;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Kunde));
		}

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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}


#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Adresse, binStream);
            BinarySerializer.ToStreamCollectionEntries(this._EMails, binStream);
            BinarySerializer.ToStream(this._Kundenname, binStream);
            BinarySerializer.ToStream(this._Land, binStream);
            BinarySerializer.ToStream(this._Ort, binStream);
            BinarySerializer.ToStream(this._PLZ, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Adresse, binStream);
            BinarySerializer.FromStreamCollectionEntries(this._EMails, binStream);
            BinarySerializer.FromStream(out this._Kundenname, binStream);
            BinarySerializer.FromStream(out this._Land, binStream);
            BinarySerializer.FromStream(out this._Ort, binStream);
            BinarySerializer.FromStream(out this._PLZ, binStream);
        }

#endregion

    }


}