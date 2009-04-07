using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using System.Xml.Serialization;
using Kistl.API;

namespace Kistl.App.Projekte
{
    [System.Diagnostics.DebuggerDisplay("Projekt_Mitarbeiter23CollectionEntry__Implementation__")]
    public class Projekt_Mitarbeiter23CollectionEntry__Implementation__ : BaseClientCollectionEntry, Projekt_Mitarbeiter23CollectionEntry
    {

        // ID is inherited
        public int RelationID { get { return 23; } }
        public IDataObject AObject
        {
            get
            {
                return A;
            }
            set
            {
                A = (Projekt)value;
            }
        }

        public IDataObject BObject
        {
            get
            {
                return B;
            }
            set
            {
                B = (Mitarbeiter)value;
            }
        }

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Projekt A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.Projekte.Projekt>(fk_A.Value);
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

                // cache old value to remove inverse references later
                var oldValue = A;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("A", oldValue, value);

                // next, set the local reference
                _fk_A = value == null ? (int?)null : value.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("A", oldValue, value);
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
                    var __oldValue = _fk_A;
                    NotifyPropertyChanging("A", __oldValue, value);
                    _fk_A = value;
                    NotifyPropertyChanged("A", __oldValue, value);
                }
            }
        }
        private int? _fk_A;
        public virtual int? A_pos
        {
            get
            {
                return _A_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_A_pos != value)
                {
                    var __oldValue = _A_pos;
                    NotifyPropertyChanging("A_pos", __oldValue, value);
                    _A_pos = value;
                    NotifyPropertyChanged("A_pos", __oldValue, value);
                }
            }
        }
        private int? _A_pos;
        /// <summary>
        /// the B-side value of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Mitarbeiter B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.Projekte.Mitarbeiter>(fk_B.Value);
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

                // cache old value to remove inverse references later
                var oldValue = B;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("B", oldValue, value);

                // next, set the local reference
                _fk_B = value == null ? (int?)null : value.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("B", oldValue, value);
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
                    var __oldValue = _fk_B;
                    NotifyPropertyChanging("B", __oldValue, value);
                    _fk_B = value;
                    NotifyPropertyChanged("B", __oldValue, value);
                }
            }
        }
        private int? _fk_B;
        public virtual int? B_pos
        {
            get
            {
                return _B_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_B_pos != value)
                {
                    var __oldValue = _B_pos;
                    NotifyPropertyChanging("B_pos", __oldValue, value);
                    _B_pos = value;
                    NotifyPropertyChanged("B_pos", __oldValue, value);
                }
            }
        }
        private int? _B_pos;



        /// <summary>
        /// Index into the A-side list of this relation
        /// </summary>
        public int? AIndex { get { return A_pos; } set { A_pos = value; } }
        /// <summary>
        /// Index into the B-side list of this relation
        /// </summary>
        public int? BIndex { get { return B_pos; } set { B_pos = value; } }
        public override Kistl.API.InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(Projekt_Mitarbeiter23CollectionEntry));
        }
    }
}
