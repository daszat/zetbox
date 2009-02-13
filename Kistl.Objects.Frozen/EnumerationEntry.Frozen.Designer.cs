
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
    /// Metadefinition Object for an Enumeration Entry.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumerationEntry")]
    public class EnumerationEntry__Implementation__ : BaseFrozenDataObject, EnumerationEntry
    {


        /// <summary>
        /// Übergeordnete Enumeration
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                return _Enumeration;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Enumeration != value)
                {
                    NotifyPropertyChanging("Enumeration");
                    _Enumeration = value;
                    NotifyPropertyChanged("Enumeration");;
                }
            }
        }
        private Kistl.App.Base.Enumeration _Enumeration;

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
                    NotifyPropertyChanging("Value");
                    _Value = value;
                    NotifyPropertyChanged("Value");;
                }
            }
        }
        private int _Value;

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
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");;
                }
            }
        }
        private string _Name;

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


        internal EnumerationEntry__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }
		internal Dictionary<int, EnumerationEntry> DataStore = new Dictionary<int, EnumerationEntry>(0);
		static EnumerationEntry__Implementation__()
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