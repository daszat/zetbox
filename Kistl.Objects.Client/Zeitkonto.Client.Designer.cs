
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
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// en:TimeAccount; Ein Konto für die Leistungserfassung. Es können nicht mehr als MaxStunden auf ein Konto gebucht werden.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Zeitkonto")]
    public class Zeitkonto__Implementation__ : BaseClientDataObject_ClientObjects, Zeitkonto
    {
    
		public Zeitkonto__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
        // value type property
        public virtual double? AktuelleStunden
        {
            get
            {
                return _AktuelleStunden;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AktuelleStunden != value)
                {
					var __oldValue = _AktuelleStunden;
                    NotifyPropertyChanging("AktuelleStunden", __oldValue, value);
                    _AktuelleStunden = value;
                    NotifyPropertyChanged("AktuelleStunden", __oldValue, value);
                }
            }
        }
        private double? _AktuelleStunden;

        /// <summary>
        /// Name des Zeiterfassungskontos
        /// </summary>
        // value type property
        public virtual string Kontoname
        {
            get
            {
                return _Kontoname;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Kontoname != value)
                {
					var __oldValue = _Kontoname;
                    NotifyPropertyChanging("Kontoname", __oldValue, value);
                    _Kontoname = value;
                    NotifyPropertyChanged("Kontoname", __oldValue, value);
                }
            }
        }
        private string _Kontoname;

        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
        // value type property
        public virtual double? MaxStunden
        {
            get
            {
                return _MaxStunden;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MaxStunden != value)
                {
					var __oldValue = _MaxStunden;
                    NotifyPropertyChanging("MaxStunden", __oldValue, value);
                    _MaxStunden = value;
                    NotifyPropertyChanged("MaxStunden", __oldValue, value);
                }
            }
        }
        private double? _MaxStunden;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
        // collection reference property

		public ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
		{
			get
			{
				if (_Mitarbeiter == null)
				{
					Context.FetchRelation<Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__>(42, RelationEndRole.A, this);
					_Mitarbeiter 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__>(
							this, 
							new RelationshipFilterASideCollection<Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__>(this.Context, this));
				}
				return _Mitarbeiter;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.Zeiterfassung.Zeitkonto, Kistl.App.Projekte.Mitarbeiter, Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__> _Mitarbeiter;

        /// <summary>
        /// Platz für Notizen
        /// </summary>
        // value type property
        public virtual string Notizen
        {
            get
            {
                return _Notizen;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Notizen != value)
                {
					var __oldValue = _Notizen;
                    NotifyPropertyChanging("Notizen", __oldValue, value);
                    _Notizen = value;
                    NotifyPropertyChanged("Notizen", __oldValue, value);
                }
            }
        }
        private string _Notizen;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Zeitkonto));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Zeitkonto)obj;
			var otherImpl = (Zeitkonto__Implementation__)obj;
			var me = (Zeitkonto)this;

			me.AktuelleStunden = other.AktuelleStunden;
			me.Kontoname = other.Kontoname;
			me.MaxStunden = other.MaxStunden;
			me.Notizen = other.Notizen;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Zeitkonto != null)
            {
                OnToString_Zeitkonto(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Zeitkonto> OnToString_Zeitkonto;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Zeitkonto != null) OnPreSave_Zeitkonto(this);
        }
        public event ObjectEventHandler<Zeitkonto> OnPreSave_Zeitkonto;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Zeitkonto != null) OnPostSave_Zeitkonto(this);
        }
        public event ObjectEventHandler<Zeitkonto> OnPostSave_Zeitkonto;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "AktuelleStunden":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(90).Constraints
						.Where(c => !c.IsValid(this, this.AktuelleStunden))
						.Select(c => c.GetErrorText(this, this.AktuelleStunden))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Kontoname":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(52).Constraints
						.Where(c => !c.IsValid(this, this.Kontoname))
						.Select(c => c.GetErrorText(this, this.Kontoname))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MaxStunden":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(89).Constraints
						.Where(c => !c.IsValid(this, this.MaxStunden))
						.Select(c => c.GetErrorText(this, this.MaxStunden))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(86).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Notizen":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(237).Constraints
						.Where(c => !c.IsValid(this, this.Notizen))
						.Select(c => c.GetErrorText(this, this.Notizen))
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
            BinarySerializer.ToStream(this._AktuelleStunden, binStream);
            BinarySerializer.ToStream(this._Kontoname, binStream);
            BinarySerializer.ToStream(this._MaxStunden, binStream);
            BinarySerializer.ToStream(this._Notizen, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AktuelleStunden, binStream);
            BinarySerializer.FromStream(out this._Kontoname, binStream);
            BinarySerializer.FromStream(out this._MaxStunden, binStream);
            BinarySerializer.FromStream(out this._Notizen, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._AktuelleStunden, xml, "AktuelleStunden", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._Kontoname, xml, "Kontoname", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._MaxStunden, xml, "MaxStunden", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._Notizen, xml, "Notizen", "Kistl.App.Zeiterfassung");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._AktuelleStunden, xml, "AktuelleStunden", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._Kontoname, xml, "Kontoname", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._MaxStunden, xml, "MaxStunden", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._Notizen, xml, "Notizen", "Kistl.App.Zeiterfassung");
        }

#endregion

    }


}