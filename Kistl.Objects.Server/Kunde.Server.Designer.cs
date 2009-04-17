
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Kunde")]
    [System.Diagnostics.DebuggerDisplay("Kunde")]
    public class Kunde__Implementation__ : BaseServerDataObject_EntityFramework, Kunde
    {
    
		public Kunde__Implementation__()
		{
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Adresse & Hausnummer
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<string> EMails
        {
            get
            {
                if (_EMailsWrapper == null)
                {
                    _EMailsWrapper = new EFValueCollectionWrapper<Kunde, string, Kunde_EMailsCollectionEntry__Implementation__, EntityCollection<Kunde_EMailsCollectionEntry__Implementation__>>(
						this.Context,
                        this,
                        EMails__Implementation__);
                }
                return _EMailsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Kunde_String_EMails", "CollectionEntry")]
        public EntityCollection<Kunde_EMailsCollectionEntry__Implementation__> EMails__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kunde_EMailsCollectionEntry__Implementation__>(
                        "Model.FK_Kunde_String_EMails",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EFValueCollectionWrapper<Kunde, string, Kunde_EMailsCollectionEntry__Implementation__, EntityCollection<Kunde_EMailsCollectionEntry__Implementation__>> _EMailsWrapper;

        /// <summary>
        /// Name des Kunden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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



		public override void ReloadReferences()
		{
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Adresse, binStream);
            BinarySerializer.ToStreamCollectionEntries(this.EMails__Implementation__, binStream);
            BinarySerializer.ToStream(this._Kundenname, binStream);
            BinarySerializer.ToStream(this._Land, binStream);
            BinarySerializer.ToStream(this._Ort, binStream);
            BinarySerializer.ToStream(this._PLZ, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Adresse, binStream);
            BinarySerializer.FromStreamCollectionEntries(this.EMails__Implementation__, binStream);
            BinarySerializer.FromStream(out this._Kundenname, binStream);
            BinarySerializer.FromStream(out this._Land, binStream);
            BinarySerializer.FromStream(out this._Ort, binStream);
            BinarySerializer.FromStream(out this._PLZ, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Adresse, xml, "Adresse", "http://dasz.at/Kistl");
            XmlStreamer.ToStreamCollectionEntries(this.EMails__Implementation__, xml, "EMails__Implementation__", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Kundenname, xml, "Kundenname", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Land, xml, "Land", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Ort, xml, "Ort", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._PLZ, xml, "PLZ", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            XmlStreamer.FromStreamCollectionEntries(this.EMails__Implementation__, xml, "EMails__Implementation__", "http://dasz.at/Kistl");
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}