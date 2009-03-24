
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
    /// Describes a Relation between two Object Classes
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Relation")]
    public class Relation__Implementation__ : BaseClientDataObject, Relation
    {


        /// <summary>
        /// The A-side of this Relation.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.RelationEnd A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.Base.RelationEnd>(fk_A.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_A == null)
					return;
                else if (value != null && value.ID == _fk_A)
					return;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("A");
				           
				// next, set the local reference
                _fk_A = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("A");
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanged("A");
                }
            }
        }
        private int? _fk_A;

        /// <summary>
        /// The B-side of this Relation.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.RelationEnd B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.Base.RelationEnd>(fk_B.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_B == null)
					return;
                else if (value != null && value.ID == _fk_B)
					return;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("B");
				           
				// next, set the local reference
                _fk_B = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("B");
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanged("B");
                }
            }
        }
        private int? _fk_B;

        /// <summary>
        /// Description of this Relation
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
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Storagetype for 1:1 Relations. Must be null for non 1:1 Relations.
        /// </summary>
        // enumeration property
        public virtual Kistl.App.Base.StorageType? Storage
        {
            get
            {
                return _Storage;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Storage != value)
                {
                    NotifyPropertyChanging("Storage");
                    _Storage = value;
                    NotifyPropertyChanged("Storage");
                }
            }
        }
        private Kistl.App.Base.StorageType? _Storage;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Relation));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Relation != null)
            {
                OnToString_Relation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Relation> OnToString_Relation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Relation != null) OnPreSave_Relation(this);
        }
        public event ObjectEventHandler<Relation> OnPreSave_Relation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Relation != null) OnPostSave_Relation(this);
        }
        public event ObjectEventHandler<Relation> OnPostSave_Relation;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "A":
                    fk_A = id;
                    break;
                case "B":
                    fk_B = id;
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
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream((int)((Relation)this).Storage, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStreamConverter(v => ((Relation)this).Storage = (Kistl.App.Base.StorageType)v, binStream);
        }

#endregion

    }


}