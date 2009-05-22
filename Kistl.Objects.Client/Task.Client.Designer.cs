// <autogenerated/>


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
    [System.Diagnostics.DebuggerDisplay("Task")]
    public class Task__Implementation__ : BaseClientDataObject_ClientObjects, Task
    {
    
		public Task__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Aufwand in Stunden
        /// </summary>
        // value type property
        public virtual double? Aufwand
        {
            get
            {
                return _Aufwand;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Aufwand != value)
                {
					var __oldValue = _Aufwand;
                    NotifyPropertyChanging("Aufwand", __oldValue, value);
                    _Aufwand = value;
                    NotifyPropertyChanged("Aufwand", __oldValue, value);
                }
            }
        }
        private double? _Aufwand;

        /// <summary>
        /// Enddatum
        /// </summary>
        // value type property
        public virtual DateTime? DatumBis
        {
            get
            {
                return _DatumBis;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumBis != value)
                {
					var __oldValue = _DatumBis;
                    NotifyPropertyChanging("DatumBis", __oldValue, value);
                    _DatumBis = value;
                    NotifyPropertyChanged("DatumBis", __oldValue, value);
                }
            }
        }
        private DateTime? _DatumBis;

        /// <summary>
        /// Start Datum
        /// </summary>
        // value type property
        public virtual DateTime? DatumVon
        {
            get
            {
                return _DatumVon;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumVon != value)
                {
					var __oldValue = _DatumVon;
                    NotifyPropertyChanging("DatumVon", __oldValue, value);
                    _DatumVon = value;
                    NotifyPropertyChanged("DatumVon", __oldValue, value);
                }
            }
        }
        private DateTime? _DatumVon;

        /// <summary>
        /// Taskname
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
					var __oldValue = _Name;
                    NotifyPropertyChanging("Name", __oldValue, value);
                    _Name = value;
                    NotifyPropertyChanged("Name", __oldValue, value);
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Verknüpfung zum Projekt
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                if (_fk_Projekt.HasValue)
                    return Context.Find<Kistl.App.Projekte.Projekt>(_fk_Projekt.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Projekt == null)
					return;
                else if (value != null && value.ID == _fk_Projekt)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Projekt;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Projekt", oldValue, value);
                
				// next, set the local reference
                _fk_Projekt = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.Tasks as OneNRelationCollection<Kistl.App.Projekte.Task>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Tasks as OneNRelationCollection<Kistl.App.Projekte.Task>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Projekt", oldValue, value);
            }
        }
        
        private int? _fk_Projekt;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Task));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Task)obj;
			var otherImpl = (Task__Implementation__)obj;
			var me = (Task)this;

			me.Aufwand = other.Aufwand;
			me.DatumBis = other.DatumBis;
			me.DatumVon = other.DatumVon;
			me.Name = other.Name;
			this._fk_Projekt = otherImpl._fk_Projekt;
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
            if (OnToString_Task != null)
            {
                OnToString_Task(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Task> OnToString_Task;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Task != null) OnPreSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPreSave_Task;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Task != null) OnPostSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPostSave_Task;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Aufwand":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(18).Constraints
						.Where(c => !c.IsValid(this, this.Aufwand))
						.Select(c => c.GetErrorText(this, this.Aufwand))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DatumBis":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(17).Constraints
						.Where(c => !c.IsValid(this, this.DatumBis))
						.Select(c => c.GetErrorText(this, this.DatumBis))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DatumVon":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(16).Constraints
						.Where(c => !c.IsValid(this, this.DatumVon))
						.Select(c => c.GetErrorText(this, this.DatumVon))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Name":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(15).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Projekt":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(19).Constraints
						.Where(c => !c.IsValid(this, this.Projekt))
						.Select(c => c.GetErrorText(this, this.Projekt))
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
                case "Projekt":
                    _fk_Projekt = id;
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
            BinarySerializer.ToStream(this._Aufwand, binStream);
            BinarySerializer.ToStream(this._DatumBis, binStream);
            BinarySerializer.ToStream(this._DatumVon, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._fk_Projekt, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Aufwand, binStream);
            BinarySerializer.FromStream(out this._DatumBis, binStream);
            BinarySerializer.FromStream(out this._DatumVon, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._fk_Projekt, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Aufwand, xml, "Aufwand", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._DatumBis, xml, "DatumBis", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._DatumVon, xml, "DatumVon", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._fk_Projekt, xml, "Projekt", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Aufwand, xml, "Aufwand", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._DatumBis, xml, "DatumBis", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._DatumVon, xml, "DatumVon", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._fk_Projekt, xml, "Projekt", "http://dasz.at/Kistl");
        }

#endregion

    }


}