
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
    public class Relation__Implementation__Frozen : BaseFrozenDataObject, Relation
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


        internal Relation__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Relation__Implementation__Frozen> DataStore = new Dictionary<int, Relation__Implementation__Frozen>(17);
		internal static void CreateInstances()
		{
			DataStore[1] = new Relation__Implementation__Frozen(1);

			DataStore[2] = new Relation__Implementation__Frozen(2);

			DataStore[3] = new Relation__Implementation__Frozen(3);

			DataStore[4] = new Relation__Implementation__Frozen(4);

			DataStore[5] = new Relation__Implementation__Frozen(5);

			DataStore[6] = new Relation__Implementation__Frozen(6);

			DataStore[8] = new Relation__Implementation__Frozen(8);

			DataStore[9] = new Relation__Implementation__Frozen(9);

			DataStore[10] = new Relation__Implementation__Frozen(10);

			DataStore[11] = new Relation__Implementation__Frozen(11);

			DataStore[12] = new Relation__Implementation__Frozen(12);

			DataStore[13] = new Relation__Implementation__Frozen(13);

			DataStore[14] = new Relation__Implementation__Frozen(14);

			DataStore[15] = new Relation__Implementation__Frozen(15);

			DataStore[16] = new Relation__Implementation__Frozen(16);

			DataStore[17] = new Relation__Implementation__Frozen(17);

			DataStore[18] = new Relation__Implementation__Frozen(18);

		}

		internal static void FillDataStore() {
			DataStore[1].Storage = StorageType.Left;
			DataStore[1].Description = null;
			DataStore[1].Seal();
			DataStore[2].Storage = StorageType.Left;
			DataStore[2].Description = null;
			DataStore[2].Seal();
			DataStore[3].Storage = StorageType.Right;
			DataStore[3].Description = null;
			DataStore[3].Seal();
			DataStore[4].Storage = StorageType.Left;
			DataStore[4].Description = null;
			DataStore[4].Seal();
			DataStore[5].Storage = StorageType.Left;
			DataStore[5].Description = null;
			DataStore[5].Seal();
			DataStore[6].Storage = StorageType.Left;
			DataStore[6].Description = null;
			DataStore[6].Seal();
			DataStore[8].Storage = StorageType.Left;
			DataStore[8].Description = null;
			DataStore[8].Seal();
			DataStore[9].Storage = StorageType.Left;
			DataStore[9].Description = null;
			DataStore[9].Seal();
			DataStore[10].Storage = StorageType.Left;
			DataStore[10].Description = null;
			DataStore[10].Seal();
			DataStore[11].Storage = StorageType.Left;
			DataStore[11].Description = null;
			DataStore[11].Seal();
			DataStore[12].Storage = StorageType.Left;
			DataStore[12].Description = null;
			DataStore[12].Seal();
			DataStore[13].Storage = StorageType.Left;
			DataStore[13].Description = null;
			DataStore[13].Seal();
			DataStore[14].Storage = StorageType.Left;
			DataStore[14].Description = null;
			DataStore[14].Seal();
			DataStore[15].Storage = StorageType.Left;
			DataStore[15].Description = null;
			DataStore[15].Seal();
			DataStore[16].Storage = StorageType.Left;
			DataStore[16].Description = null;
			DataStore[16].Seal();
			DataStore[17].Storage = StorageType.Left;
			DataStore[17].Description = null;
			DataStore[17].Seal();
			DataStore[18].Storage = StorageType.Left;
			DataStore[18].Description = null;
			DataStore[18].Seal();
	
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