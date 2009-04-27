
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
    /// Eine definierte Leistung eines Mitarbeiters, die auf ein Zeitkonto gebucht worden ist.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("LeistungsEintrag")]
    public class LeistungsEintrag__Implementation__ : BaseClientDataObject_ClientObjects, LeistungsEintrag
    {
    
		public LeistungsEintrag__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Wann diese Leistung begonnen wurde
        /// </summary>
        // value type property
        public virtual DateTime Anfang
        {
            get
            {
                return _Anfang;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Anfang != value)
                {
					var __oldValue = _Anfang;
                    NotifyPropertyChanging("Anfang", __oldValue, value);
                    _Anfang = value;
                    NotifyPropertyChanged("Anfang", __oldValue, value);
                }
            }
        }
        private DateTime _Anfang;

        /// <summary>
        /// Eine kurze Überschrift, was gemacht wurde.
        /// </summary>
        // value type property
        public virtual string Bezeichnung
        {
            get
            {
                return _Bezeichnung;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Bezeichnung != value)
                {
					var __oldValue = _Bezeichnung;
                    NotifyPropertyChanging("Bezeichnung", __oldValue, value);
                    _Bezeichnung = value;
                    NotifyPropertyChanged("Bezeichnung", __oldValue, value);
                }
            }
        }
        private string _Bezeichnung;

        /// <summary>
        /// Wann diese Leistung beendet wurde.
        /// </summary>
        // value type property
        public virtual DateTime Ende
        {
            get
            {
                return _Ende;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Ende != value)
                {
					var __oldValue = _Ende;
                    NotifyPropertyChanging("Ende", __oldValue, value);
                    _Ende = value;
                    NotifyPropertyChanged("Ende", __oldValue, value);
                }
            }
        }
        private DateTime _Ende;

        /// <summary>
        /// Der Mitarbeiter der diese Leistung erbracht hat.
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
                
                // shortcut noops
                if (value == null && _fk_Mitarbeiter == null)
					return;
                else if (value != null && value.ID == _fk_Mitarbeiter)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Mitarbeiter;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Mitarbeiter", oldValue, value);
                
				// next, set the local reference
                _fk_Mitarbeiter = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Mitarbeiter", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Mitarbeiter
        {
            get
            {
                return _fk_Mitarbeiter;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Mitarbeiter != value)
                {
					var __oldValue = _fk_Mitarbeiter;
                    NotifyPropertyChanging("Mitarbeiter", __oldValue, value);
                    _fk_Mitarbeiter = value;
                    NotifyPropertyChanged("Mitarbeiter", __oldValue, value);
                }
            }
        }
        private int? _fk_Mitarbeiter;

        /// <summary>
        /// Notizen zu dieser Leistung
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
			return new InterfaceType(typeof(LeistungsEintrag));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (LeistungsEintrag)obj;
			var otherImpl = (LeistungsEintrag__Implementation__)obj;
			var me = (LeistungsEintrag)this;

			me.Anfang = other.Anfang;
			me.Bezeichnung = other.Bezeichnung;
			me.Ende = other.Ende;
			me.Notizen = other.Notizen;
			this.fk_Mitarbeiter = otherImpl.fk_Mitarbeiter;
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
            if (OnToString_LeistungsEintrag != null)
            {
                OnToString_LeistungsEintrag(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<LeistungsEintrag> OnToString_LeistungsEintrag;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_LeistungsEintrag != null) OnPreSave_LeistungsEintrag(this);
        }
        public event ObjectEventHandler<LeistungsEintrag> OnPreSave_LeistungsEintrag;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_LeistungsEintrag != null) OnPostSave_LeistungsEintrag(this);
        }
        public event ObjectEventHandler<LeistungsEintrag> OnPostSave_LeistungsEintrag;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Anfang":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(247).Constraints
						.Where(c => !c.IsValid(this, this.Anfang))
						.Select(c => c.GetErrorText(this, this.Anfang))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Bezeichnung":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(245).Constraints
						.Where(c => !c.IsValid(this, this.Bezeichnung))
						.Select(c => c.GetErrorText(this, this.Bezeichnung))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Ende":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(248).Constraints
						.Where(c => !c.IsValid(this, this.Ende))
						.Select(c => c.GetErrorText(this, this.Ende))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(249).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Notizen":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(246).Constraints
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
                case "Mitarbeiter":
                    fk_Mitarbeiter = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Anfang, binStream);
            BinarySerializer.ToStream(this._Bezeichnung, binStream);
            BinarySerializer.ToStream(this._Ende, binStream);
            BinarySerializer.ToStream(this._fk_Mitarbeiter, binStream);
            BinarySerializer.ToStream(this._Notizen, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Anfang, binStream);
            BinarySerializer.FromStream(out this._Bezeichnung, binStream);
            BinarySerializer.FromStream(out this._Ende, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._Notizen, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Anfang, xml, "Anfang", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._Bezeichnung, xml, "Bezeichnung", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._Ende, xml, "Ende", "Kistl.App.Zeiterfassung");
            XmlStreamer.ToStream(this._fk_Mitarbeiter, xml, "Mitarbeiter", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Notizen, xml, "Notizen", "Kistl.App.Zeiterfassung");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Anfang, xml, "Anfang", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._Bezeichnung, xml, "Bezeichnung", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._Ende, xml, "Ende", "Kistl.App.Zeiterfassung");
            XmlStreamer.FromStream(ref this._fk_Mitarbeiter, xml, "Mitarbeiter", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Notizen, xml, "Notizen", "Kistl.App.Zeiterfassung");
        }

#endregion

    }


}