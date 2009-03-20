using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
    using Kistl.API.Client;

namespace Kistl.App.Base
{
    [System.Diagnostics.DebuggerDisplay("ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__")]
    public class ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__ : BaseClientCollectionEntry, ObjectClass_ImplementsInterfaces49CollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectClass>(fk_A.Value);
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
        /// the B-side value of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Interface B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.Base.Interface>(fk_B.Value);
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

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectClass_ImplementsInterfaces49CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.Projekte
{
    [System.Diagnostics.DebuggerDisplay("Projekt_Mitarbeiter23CollectionEntry__Implementation__")]
    public class Projekt_Mitarbeiter23CollectionEntry__Implementation__ : BaseClientCollectionEntry, Projekt_Mitarbeiter23CollectionEntry
    {
    
// ID is inherited

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
                    NotifyPropertyChanging("A_pos");
                    _A_pos = value;
                    NotifyPropertyChanged("A_pos");
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
                    NotifyPropertyChanging("B_pos");
                    _B_pos = value;
                    NotifyPropertyChanged("B_pos");
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
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._A_pos, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
            BinarySerializer.FromStream(out this._B_pos, binStream);
            BinarySerializer.FromStream(out this._A_pos, binStream);
            BinarySerializer.FromStream(out this._B_pos, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Projekt_Mitarbeiter23CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.GUI
{
    [System.Diagnostics.DebuggerDisplay("Template_Menu61CollectionEntry__Implementation__")]
    public class Template_Menu61CollectionEntry__Implementation__ : BaseClientCollectionEntry, Template_Menu61CollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Template A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.GUI.Template>(fk_A.Value);
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
        /// the B-side value of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_B.Value);
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

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Template_Menu61CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.Base
{
    [System.Diagnostics.DebuggerDisplay("TypeRef_GenericArguments66CollectionEntry__Implementation__")]
    public class TypeRef_GenericArguments66CollectionEntry__Implementation__ : BaseClientCollectionEntry, TypeRef_GenericArguments66CollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_A.Value);
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
                    NotifyPropertyChanging("A_pos");
                    _A_pos = value;
                    NotifyPropertyChanged("A_pos");
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
        public Kistl.App.Base.TypeRef B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_B.Value);
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
                    NotifyPropertyChanging("B_pos");
                    _B_pos = value;
                    NotifyPropertyChanged("B_pos");
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
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._A_pos, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
            BinarySerializer.FromStream(out this._B_pos, binStream);
            BinarySerializer.FromStream(out this._A_pos, binStream);
            BinarySerializer.FromStream(out this._B_pos, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TypeRef_GenericArguments66CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.GUI
{
    [System.Diagnostics.DebuggerDisplay("Visual_Children55CollectionEntry__Implementation__")]
    public class Visual_Children55CollectionEntry__Implementation__ : BaseClientCollectionEntry, Visual_Children55CollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_A.Value);
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
        /// the B-side value of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_B.Value);
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

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Visual_Children55CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.GUI
{
    [System.Diagnostics.DebuggerDisplay("Visual_ContextMenu60CollectionEntry__Implementation__")]
    public class Visual_ContextMenu60CollectionEntry__Implementation__ : BaseClientCollectionEntry, Visual_ContextMenu60CollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_A.Value);
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
        /// the B-side value of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual B
        {
            get
            {
                if (fk_B.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_B.Value);
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

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Visual_ContextMenu60CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.Zeiterfassung
{
	using Kistl.App.Projekte;
    [System.Diagnostics.DebuggerDisplay("Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__")]
    public class Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__ : BaseClientCollectionEntry, Zeitkonto_Mitarbeiter42CollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Zeiterfassung.Zeitkonto A
        {
            get
            {
                if (fk_A.HasValue)
                    return Context.Find<Kistl.App.Zeiterfassung.Zeitkonto>(fk_A.Value);
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

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Zeitkonto_Mitarbeiter42CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}

namespace Kistl.App.Projekte
{
    [System.Diagnostics.DebuggerDisplay("Kunde_EMailsCollectionEntry__Implementation__")]
    public class Kunde_EMailsCollectionEntry__Implementation__ : BaseClientCollectionEntry, Kunde_EMailsCollectionEntry
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>

        public Kunde A
        {
            get
            {
                if (_ACache != null && _ACache.ID == _fk_A)
                    return _ACache;

                if (_fk_A.HasValue)
                    _ACache = this.Context.Find<Kunde>(_fk_A.Value);
                else
                    _ACache = null;

                return _ACache;
            }
            set
            {
                if (value == null && !_fk_A.HasValue)
                    return;
                if (value != null && _fk_A.HasValue && value.ID == _fk_A.Value)
                    return;

                _ACache = value;
                if (value != null)
					fk_A = value.ID;
				else
					fk_A = null;
            }
        }
        private Kunde _ACache;

        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanged("A");
                }
            }
        }

        // backing store for serialization
        private int? _fk_A;
        
        /// <summary>
        /// the B-side value of this CollectionEntry
        /// </summary>
        // value type property
        public virtual string B
        {
            get
            {
                return _B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_B != value)
                {
                    NotifyPropertyChanging("B");
                    _B = value;
                    NotifyPropertyChanged("B");
                }
            }
        }
        private string _B;

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._B, binStream);
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Kunde_EMailsCollectionEntry));
		}
	
		public override void ReloadReferences()
		{
	
		}


    }
}
