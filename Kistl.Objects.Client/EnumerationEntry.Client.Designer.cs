
namespace Kistl.App.Base
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
    /// Metadefinition Object for an Enumeration Entry.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumerationEntry")]
    public class EnumerationEntry__Implementation__ : BaseClientDataObject, EnumerationEntry
    {
    
		public EnumerationEntry__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Description of this Enumeration Entry
        /// </summary>
        // value type property
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Übergeordnete Enumeration
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                if (fk_Enumeration.HasValue)
                    return Context.Find<Kistl.App.Base.Enumeration>(fk_Enumeration.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Enumeration == null)
					return;
                else if (value != null && value.ID == _fk_Enumeration)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Enumeration;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Enumeration", oldValue, value);
                
				// next, set the local reference
                _fk_Enumeration = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.EnumerationEntries as OneNRelationCollection<Kistl.App.Base.EnumerationEntry>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.EnumerationEntries as OneNRelationCollection<Kistl.App.Base.EnumerationEntry>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Enumeration", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Enumeration
        {
            get
            {
                return _fk_Enumeration;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Enumeration != value)
                {
					var __oldValue = _fk_Enumeration;
                    NotifyPropertyChanging("Enumeration", __oldValue, value);
                    _fk_Enumeration = value;
                    NotifyPropertyChanged("Enumeration", __oldValue, value);
                }
            }
        }
        private int? _fk_Enumeration;

        /// <summary>
        /// CLR name of this entry
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
        /// The CLR value of this entry
        /// </summary>
        // value type property
        public virtual int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Value != value)
                {
					var __oldValue = _Value;
                    NotifyPropertyChanging("Value", __oldValue, value);
                    _Value = value;
                    NotifyPropertyChanged("Value", __oldValue, value);
                }
            }
        }
        private int _Value;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(EnumerationEntry));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (EnumerationEntry)obj;
			var otherImpl = (EnumerationEntry__Implementation__)obj;
			var me = (EnumerationEntry)this;

			me.Description = other.Description;
			me.Name = other.Name;
			me.Value = other.Value;
			this.fk_Enumeration = otherImpl.fk_Enumeration;
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
            if (OnToString_EnumerationEntry != null)
            {
                OnToString_EnumerationEntry(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<EnumerationEntry> OnToString_EnumerationEntry;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationEntry != null) OnPreSave_EnumerationEntry(this);
        }
        public event ObjectEventHandler<EnumerationEntry> OnPreSave_EnumerationEntry;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationEntry != null) OnPostSave_EnumerationEntry(this);
        }
        public event ObjectEventHandler<EnumerationEntry> OnPostSave_EnumerationEntry;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Enumeration":
                    fk_Enumeration = id;
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
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_Enumeration, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._Value, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_Enumeration, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._Value, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_Enumeration, xml, "fk_Enumeration", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Name, xml, "Name", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Value, xml, "Value", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}