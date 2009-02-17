
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
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Left Part of the Relation
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty LeftPart
        {
            get
            {
                if (fk_LeftPart.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectReferenceProperty>(fk_LeftPart.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = LeftPart;
                if (value != null && value.ID != fk_LeftPart)
                {
                    fk_LeftPart = value.ID;
                    value.LeftOf = this;
                }
                else
                {
                    fk_LeftPart = null;
                    value.LeftOf = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_LeftPart
        {
            get
            {
                return _fk_LeftPart;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_LeftPart != value)
                {
                    NotifyPropertyChanging("LeftPart");
                    _fk_LeftPart = value;
                    NotifyPropertyChanging("LeftPart");
                }
            }
        }
        private int? _fk_LeftPart;

        /// <summary>
        /// Right Part of the Relation
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty RightPart
        {
            get
            {
                if (fk_RightPart.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectReferenceProperty>(fk_RightPart.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = RightPart;
                if (value != null && value.ID != fk_RightPart)
                {
                    fk_RightPart = value.ID;
                    value.RightOf = this;
                }
                else
                {
                    fk_RightPart = null;
                    value.RightOf = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_RightPart
        {
            get
            {
                return _fk_RightPart;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_RightPart != value)
                {
                    NotifyPropertyChanging("RightPart");
                    _fk_RightPart = value;
                    NotifyPropertyChanging("RightPart");
                }
            }
        }
        private int? _fk_RightPart;

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
                    NotifyPropertyChanged("Storage");;
                }
            }
        }
        private Kistl.App.Base.StorageType? _Storage;

		public override Type GetInterfaceType()
		{
			return typeof(Relation);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_LeftPart, binStream);
            BinarySerializer.ToStream(this._fk_RightPart, binStream);
            BinarySerializer.ToStream((int)this.Storage, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_LeftPart, binStream);
            BinarySerializer.FromStream(out this._fk_RightPart, binStream);
            BinarySerializer.FromStreamConverter(v => this.Storage = (Kistl.App.Base.StorageType)v, binStream);
        }

#endregion

    }


}