
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

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Taetigkeit")]
    public class Taetigkeit__Implementation__ : BaseClientDataObject, Taetigkeit
    {
    
		public Taetigkeit__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Datum
        /// </summary>
        // value type property
        public virtual DateTime Datum
        {
            get
            {
                return _Datum;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Datum != value)
                {
					var __oldValue = _Datum;
                    NotifyPropertyChanging("Datum", __oldValue, value);
                    _Datum = value;
                    NotifyPropertyChanged("Datum", __oldValue, value);
                }
            }
        }
        private DateTime _Datum;

        /// <summary>
        /// Dauer in Stunden
        /// </summary>
        // value type property
        public virtual double Dauer
        {
            get
            {
                return _Dauer;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Dauer != value)
                {
					var __oldValue = _Dauer;
                    NotifyPropertyChanging("Dauer", __oldValue, value);
                    _Dauer = value;
                    NotifyPropertyChanged("Dauer", __oldValue, value);
                }
            }
        }
        private double _Dauer;

        /// <summary>
        /// Mitarbeiter
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
        /// Art der Tätigkeit
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.TaetigkeitsArt TaetigkeitsArt
        {
            get
            {
                if (fk_TaetigkeitsArt.HasValue)
                    return Context.Find<Kistl.App.Zeiterfassung.TaetigkeitsArt>(fk_TaetigkeitsArt.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_TaetigkeitsArt == null)
					return;
                else if (value != null && value.ID == _fk_TaetigkeitsArt)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = TaetigkeitsArt;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("TaetigkeitsArt", oldValue, value);
                
				// next, set the local reference
                _fk_TaetigkeitsArt = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("TaetigkeitsArt", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_TaetigkeitsArt
        {
            get
            {
                return _fk_TaetigkeitsArt;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_TaetigkeitsArt != value)
                {
					var __oldValue = _fk_TaetigkeitsArt;
                    NotifyPropertyChanging("TaetigkeitsArt", __oldValue, value);
                    _fk_TaetigkeitsArt = value;
                    NotifyPropertyChanged("TaetigkeitsArt", __oldValue, value);
                }
            }
        }
        private int? _fk_TaetigkeitsArt;

        /// <summary>
        /// Zeitkonto
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.Zeitkonto Zeitkonto
        {
            get
            {
                if (fk_Zeitkonto.HasValue)
                    return Context.Find<Kistl.App.Zeiterfassung.Zeitkonto>(fk_Zeitkonto.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Zeitkonto == null)
					return;
                else if (value != null && value.ID == _fk_Zeitkonto)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Zeitkonto;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Zeitkonto", oldValue, value);
                
				// next, set the local reference
                _fk_Zeitkonto = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.Taetigkeiten as OneNRelationCollection<Kistl.App.Zeiterfassung.Taetigkeit>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Taetigkeiten as OneNRelationCollection<Kistl.App.Zeiterfassung.Taetigkeit>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Zeitkonto", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Zeitkonto
        {
            get
            {
                return _fk_Zeitkonto;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Zeitkonto != value)
                {
					var __oldValue = _fk_Zeitkonto;
                    NotifyPropertyChanging("Zeitkonto", __oldValue, value);
                    _fk_Zeitkonto = value;
                    NotifyPropertyChanged("Zeitkonto", __oldValue, value);
                }
            }
        }
        private int? _fk_Zeitkonto;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Taetigkeit));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Taetigkeit)obj;
			var otherImpl = (Taetigkeit__Implementation__)obj;
			var me = (Taetigkeit)this;

			me.Datum = other.Datum;
			me.Dauer = other.Dauer;
			this.fk_Mitarbeiter = otherImpl.fk_Mitarbeiter;
			this.fk_TaetigkeitsArt = otherImpl.fk_TaetigkeitsArt;
			this.fk_Zeitkonto = otherImpl.fk_Zeitkonto;
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
            if (OnToString_Taetigkeit != null)
            {
                OnToString_Taetigkeit(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Taetigkeit> OnToString_Taetigkeit;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Taetigkeit != null) OnPreSave_Taetigkeit(this);
        }
        public event ObjectEventHandler<Taetigkeit> OnPreSave_Taetigkeit;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Taetigkeit != null) OnPostSave_Taetigkeit(this);
        }
        public event ObjectEventHandler<Taetigkeit> OnPostSave_Taetigkeit;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Mitarbeiter":
                    fk_Mitarbeiter = id;
                    break;
                case "TaetigkeitsArt":
                    fk_TaetigkeitsArt = id;
                    break;
                case "Zeitkonto":
                    fk_Zeitkonto = id;
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
            BinarySerializer.ToStream(this._Datum, binStream);
            BinarySerializer.ToStream(this._Dauer, binStream);
            BinarySerializer.ToStream(this._fk_Mitarbeiter, binStream);
            BinarySerializer.ToStream(this._fk_TaetigkeitsArt, binStream);
            BinarySerializer.ToStream(this._fk_Zeitkonto, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Datum, binStream);
            BinarySerializer.FromStream(out this._Dauer, binStream);
            BinarySerializer.FromStream(out this._fk_Mitarbeiter, binStream);
            BinarySerializer.FromStream(out this._fk_TaetigkeitsArt, binStream);
            BinarySerializer.FromStream(out this._fk_Zeitkonto, binStream);
        }

#endregion

    }


}