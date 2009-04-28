
namespace Kistl.App.TimeRecords
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
    [System.Diagnostics.DebuggerDisplay("PresenceRecord")]
    public class PresenceRecord__Implementation__ : BaseClientDataObject_ClientObjects, PresenceRecord
    {
    
		public PresenceRecord__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Point in time when the presence started.
        /// </summary>
        // value type property
        public virtual DateTime From
        {
            get
            {
                return _From;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_From != value)
                {
					var __oldValue = _From;
                    NotifyPropertyChanging("From", __oldValue, value);
                    _From = value;
                    NotifyPropertyChanged("From", __oldValue, value);
                }
            }
        }
        private DateTime _From;

        /// <summary>
        /// Which employee was present.
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
        /// Point in time (inclusive) when the presence ended.
        /// </summary>
        // value type property
        public virtual DateTime Thru
        {
            get
            {
                return _Thru;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Thru != value)
                {
					var __oldValue = _Thru;
                    NotifyPropertyChanging("Thru", __oldValue, value);
                    _Thru = value;
                    NotifyPropertyChanged("Thru", __oldValue, value);
                }
            }
        }
        private DateTime _Thru;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresenceRecord));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PresenceRecord)obj;
			var otherImpl = (PresenceRecord__Implementation__)obj;
			var me = (PresenceRecord)this;

			me.From = other.From;
			me.Thru = other.Thru;
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
            if (OnToString_PresenceRecord != null)
            {
                OnToString_PresenceRecord(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresenceRecord> OnToString_PresenceRecord;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresenceRecord != null) OnPreSave_PresenceRecord(this);
        }
        public event ObjectEventHandler<PresenceRecord> OnPreSave_PresenceRecord;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresenceRecord != null) OnPostSave_PresenceRecord(this);
        }
        public event ObjectEventHandler<PresenceRecord> OnPostSave_PresenceRecord;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "From":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(238).Constraints
						.Where(c => !c.IsValid(this, this.From))
						.Select(c => c.GetErrorText(this, this.From))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(244).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Thru":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(239).Constraints
						.Where(c => !c.IsValid(this, this.Thru))
						.Select(c => c.GetErrorText(this, this.Thru))
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
            BinarySerializer.ToStream(this._From, binStream);
            BinarySerializer.ToStream(this._fk_Mitarbeiter, binStream);
            BinarySerializer.ToStream(this._Thru, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._From, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._Thru, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._From, xml, "From", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._fk_Mitarbeiter, xml, "Mitarbeiter", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Thru, xml, "Thru", "Kistl.App.TimeRecords");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._From, xml, "From", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._fk_Mitarbeiter, xml, "Mitarbeiter", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Thru, xml, "Thru", "Kistl.App.TimeRecords");
        }

#endregion

    }


}