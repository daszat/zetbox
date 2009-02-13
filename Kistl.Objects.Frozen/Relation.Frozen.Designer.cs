
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// Describes a Relation between two Object Classes
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Relation")]
    public class Relation__Implementation__ : BaseFrozenDataObject, Relation
    {


        /// <summary>
        /// Left Part of the Relation
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.ObjectReferenceProperty LeftPart
        {
            get
            {
                return _LeftPart;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_LeftPart != value)
                {
                    NotifyPropertyChanging("LeftPart");
                    _LeftPart = value;
                    NotifyPropertyChanged("LeftPart");;
                }
            }
        }
        private Kistl.App.Base.ObjectReferenceProperty _LeftPart;

        /// <summary>
        /// Right Part of the Relation
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.ObjectReferenceProperty RightPart
        {
            get
            {
                return _RightPart;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_RightPart != value)
                {
                    NotifyPropertyChanging("RightPart");
                    _RightPart = value;
                    NotifyPropertyChanged("RightPart");;
                }
            }
        }
        private Kistl.App.Base.ObjectReferenceProperty _RightPart;

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


        internal Relation__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }
		internal Dictionary<int, Relation> DataStore = new Dictionary<int, Relation>(0);
		static Relation__Implementation__()
		{
		}

#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}