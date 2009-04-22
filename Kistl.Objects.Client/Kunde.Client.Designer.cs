
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
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Kunde")]
    public class Kunde__Implementation__ : BaseClientDataObject_ClientObjects, Kunde
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
					var __oldValue = _Adresse;
                    NotifyPropertyChanging("Adresse", __oldValue, value);
                    _Adresse = value;
                    NotifyPropertyChanged("Adresse", __oldValue, value);
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
				        = new ClientValueCollectionWrapper<Kunde, string, Kunde_EMailsCollectionEntry__Implementation__, IList<Kunde_EMailsCollectionEntry__Implementation__>>(
							this.Context,
				            this, 
				            _EMails);
				}
				return _EMailsWrapper;
			}
		}

		private ClientValueCollectionWrapper<Kunde, string, Kunde_EMailsCollectionEntry__Implementation__, IList<Kunde_EMailsCollectionEntry__Implementation__>> _EMailsWrapper;
		private IList<Kunde_EMailsCollectionEntry__Implementation__> _EMails = new List<Kunde_EMailsCollectionEntry__Implementation__>();

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
					var __oldValue = _Kundenname;
                    NotifyPropertyChanging("Kundenname", __oldValue, value);
                    _Kundenname = value;
                    NotifyPropertyChanged("Kundenname", __oldValue, value);
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
					var __oldValue = _Land;
                    NotifyPropertyChanging("Land", __oldValue, value);
                    _Land = value;
                    NotifyPropertyChanged("Land", __oldValue, value);
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
					var __oldValue = _Ort;
                    NotifyPropertyChanging("Ort", __oldValue, value);
                    _Ort = value;
                    NotifyPropertyChanged("Ort", __oldValue, value);
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
					var __oldValue = _PLZ;
                    NotifyPropertyChanging("PLZ", __oldValue, value);
                    _PLZ = value;
                    NotifyPropertyChanged("PLZ", __oldValue, value);
                }
            }
        }
        private string _PLZ;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Kunde));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Kunde)obj;
			var otherImpl = (Kunde__Implementation__)obj;
			var me = (Kunde)this;

			me.Adresse = other.Adresse;
			me.Kundenname = other.Kundenname;
			me.Land = other.Land;
			me.Ort = other.Ort;
			me.PLZ = other.PLZ;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
			_EMails.ForEach<IValueCollectionEntry>(i => ctx.Attach(i));
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Adresse":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(60).Constraints
						.Where(c => !c.IsValid(this, this.Adresse))
						.Select(c => c.GetErrorText(this, this.Adresse))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "EMails":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(85).Constraints
						.Where(c => !c.IsValid(this, this.EMails))
						.Select(c => c.GetErrorText(this, this.EMails))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Kundenname":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(59).Constraints
						.Where(c => !c.IsValid(this, this.Kundenname))
						.Select(c => c.GetErrorText(this, this.Kundenname))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Land":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(63).Constraints
						.Where(c => !c.IsValid(this, this.Land))
						.Select(c => c.GetErrorText(this, this.Land))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Ort":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(62).Constraints
						.Where(c => !c.IsValid(this, this.Ort))
						.Select(c => c.GetErrorText(this, this.Ort))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PLZ":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(61).Constraints
						.Where(c => !c.IsValid(this, this.PLZ))
						.Select(c => c.GetErrorText(this, this.PLZ))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

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

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Adresse, xml, "Adresse", "Kistl.App.Projekte");
            XmlStreamer.ToStreamCollectionEntries(this._EMails, xml, "EMails", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Kundenname, xml, "Kundenname", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Land, xml, "Land", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Ort, xml, "Ort", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._PLZ, xml, "PLZ", "Kistl.App.Projekte");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Adresse, xml, "Adresse", "Kistl.App.Projekte");
            XmlStreamer.FromStreamCollectionEntries(this._EMails, xml, "EMails", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Kundenname, xml, "Kundenname", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Land, xml, "Land", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Ort, xml, "Ort", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._PLZ, xml, "PLZ", "Kistl.App.Projekte");
        }

#endregion

    }


}