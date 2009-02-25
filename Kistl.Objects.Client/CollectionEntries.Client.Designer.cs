using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
    using Kistl.API.Client;

namespace Kistl.App.Base
{
    [System.Diagnostics.DebuggerDisplay("ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__")]
    public class ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<ObjectClass, Interface>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
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

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<ObjectClass, Interface>);
	}


    }
}

namespace Kistl.App.Base
{
    [System.Diagnostics.DebuggerDisplay("TypeRef_GenericArguments46CollectionEntry__Implementation__")]
    public class TypeRef_GenericArguments46CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewListEntry<TypeRef, TypeRef>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
                }
            }
        }
        private int? _fk_B;



        /// <summary>
        /// Index into the A-side list of this relation
        /// </summary>
public int? AIndex { get { return A_pos; } set { A_pos = value; } }
        /// <summary>
        /// Index into the B-side list of this relation
        /// </summary>
/// <summary>ignored implementation for INewListEntry</summary>
public int? BIndex { get { return null; } set { } }
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_A, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._fk_B, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_A, binStream);
            BinarySerializer.FromStream(out this._A_pos, binStream);
            BinarySerializer.FromStream(out this._fk_B, binStream);
            BinarySerializer.FromStream(out this._A_pos, binStream);
        }

#endregion

	public override Type GetInterfaceType()
	{
		return typeof(INewListEntry<TypeRef, TypeRef>);
	}


    }
}

namespace Kistl.App.GUI
{
    [System.Diagnostics.DebuggerDisplay("Template_Menu41CollectionEntry__Implementation__")]
    public class Template_Menu41CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<Template, Visual>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
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

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<Template, Visual>);
	}


    }
}

namespace Kistl.App.GUI
{
    [System.Diagnostics.DebuggerDisplay("Visual_Children35CollectionEntry__Implementation__")]
    public class Visual_Children35CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<Visual, Visual>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
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

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<Visual, Visual>);
	}


    }
}

namespace Kistl.App.GUI
{
    [System.Diagnostics.DebuggerDisplay("Visual_ContextMenu40CollectionEntry__Implementation__")]
    public class Visual_ContextMenu40CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<Visual, Visual>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
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

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<Visual, Visual>);
	}


    }
}

namespace Kistl.App.Projekte
{
    [System.Diagnostics.DebuggerDisplay("Projekt_Mitarbeiter3CollectionEntry__Implementation__")]
    public class Projekt_Mitarbeiter3CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewListEntry<Projekt, Mitarbeiter>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
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

	public override Type GetInterfaceType()
	{
		return typeof(INewListEntry<Projekt, Mitarbeiter>);
	}


    }
}

namespace Kistl.App.Zeiterfassung
{
	using Kistl.App.Projekte;
    [System.Diagnostics.DebuggerDisplay("Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__")]
    public class Zeitkonto_Mitarbeiter22CollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<Zeitkonto, Mitarbeiter>
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
                fk_A = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                return _fk_A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_A != value)
                {
                    NotifyPropertyChanging("A");
                    _fk_A = value;
                    NotifyPropertyChanging("A");
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
                fk_B = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                return _fk_B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_B != value)
                {
                    NotifyPropertyChanging("B");
                    _fk_B = value;
                    NotifyPropertyChanging("B");
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

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<Zeitkonto, Mitarbeiter>);
	}


    }
}

namespace Kistl.App.Projekte
{
    [System.Diagnostics.DebuggerDisplay("Kunde_EMailsCollectionEntry__Implementation__")]
    public class Kunde_EMailsCollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<Kunde, System.String>
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // parent property
        public virtual Kunde A
        {
            get
            {
                return _A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_A != value)
                {
                    NotifyPropertyChanging("A");
                    _A = value;
                    NotifyPropertyChanged("A");
                }
            }
        }
        private Kunde _A;

        // proxy to A.ID
        public int fk_A
        {
            get { return A.ID; }
            set { A = this.Context.Find<Kunde>(value); }
        }


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
            BinarySerializer.ToStream(this._B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._B, binStream);
        }

#endregion

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<Kunde, System.String>);
	}


    }
}
