using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

namespace Kistl.App.Base
{
    [EdmEntityType(NamespaceName="Model", Name="ObjectClass_ImplementsInterfaces49CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__")]
    public class ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, ObjectClass_ImplementsInterfaces49CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_Interface_ObjectClass_49", "ObjectClass")]
        public Kistl.App.Base.ObjectClass__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_Interface_ObjectClass_49",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_Interface_ObjectClass_49",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.Base.Interface__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_Interface_ImplementsInterfaces_49", "ImplementsInterfaces")]
        public Kistl.App.Base.Interface__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Interface__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Interface__Implementation__>(
                        "Model.FK_ObjectClass_Interface_ImplementsInterfaces_49",
                        "ImplementsInterfaces");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Interface__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Interface__Implementation__>(
                        "Model.FK_ObjectClass_Interface_ImplementsInterfaces_49",
                        "ImplementsInterfaces");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Interface__Implementation__)value;
            }
        }
        
        

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectClass_ImplementsInterfaces49CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)Context.Find<Kistl.App.Base.ObjectClass>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.Base.Interface__Implementation__)Context.Find<Kistl.App.Base.Interface>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.Projekte
{
    [EdmEntityType(NamespaceName="Model", Name="Projekt_Mitarbeiter23CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("Projekt_Mitarbeiter23CollectionEntry__Implementation__")]
    public class Projekt_Mitarbeiter23CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, Projekt_Mitarbeiter23CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Mitarbeiter_Projekte_23", "Projekte")]
        public Kistl.App.Projekte.Projekt__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Mitarbeiter_Projekte_23",
                        "Projekte");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Mitarbeiter_Projekte_23",
                        "Projekte");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Mitarbeiter_Mitarbeiter_23", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Projekt_Mitarbeiter_Mitarbeiter_23",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Projekt_Mitarbeiter_Mitarbeiter_23",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            BinarySerializer.FromStream(out this._A_pos, binStream);
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
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
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)Context.Find<Kistl.App.Projekte.Projekt>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)Context.Find<Kistl.App.Projekte.Mitarbeiter>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.GUI
{
    [EdmEntityType(NamespaceName="Model", Name="Template_Menu61CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("Template_Menu61CollectionEntry__Implementation__")]
    public class Template_Menu61CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, Template_Menu61CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.GUI.Template__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Template_Visual_Template_61", "Template")]
        public Kistl.App.GUI.Template__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Template__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Template__Implementation__>(
                        "Model.FK_Template_Visual_Template_61",
                        "Template");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.Template__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Template__Implementation__>(
                        "Model.FK_Template_Visual_Template_61",
                        "Template");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Template__Implementation__)value;
            }
        }
        
        
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Template_Visual_Menu_61", "Menu")]
        public Kistl.App.GUI.Visual__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Template_Visual_Menu_61",
                        "Menu");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Template_Visual_Menu_61",
                        "Menu");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Template_Menu61CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.GUI.Template__Implementation__)Context.Find<Kistl.App.GUI.Template>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)Context.Find<Kistl.App.GUI.Visual>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.Base
{
    [EdmEntityType(NamespaceName="Model", Name="TypeRef_GenericArguments66CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("TypeRef_GenericArguments66CollectionEntry__Implementation__")]
    public class TypeRef_GenericArguments66CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, TypeRef_GenericArguments66CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_TypeRef_TypeRef_66", "TypeRef")]
        public Kistl.App.Base.TypeRef__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_TypeRef_TypeRef_TypeRef_66",
                        "TypeRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_TypeRef_TypeRef_TypeRef_66",
                        "TypeRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_TypeRef_GenericArguments_66", "GenericArguments")]
        public Kistl.App.Base.TypeRef__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_TypeRef_TypeRef_GenericArguments_66",
                        "GenericArguments");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_TypeRef_TypeRef_GenericArguments_66",
                        "GenericArguments");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
            BinarySerializer.ToStream(this._A_pos, binStream);
            BinarySerializer.ToStream(this._B_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            BinarySerializer.FromStream(out this._A_pos, binStream);
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
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
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.GUI
{
    [EdmEntityType(NamespaceName="Model", Name="Visual_Children55CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("Visual_Children55CollectionEntry__Implementation__")]
    public class Visual_Children55CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, Visual_Children55CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Visual_55", "Visual")]
        public Kistl.App.GUI.Visual__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_55",
                        "Visual");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_55",
                        "Visual");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Children_55", "Children")]
        public Kistl.App.GUI.Visual__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_Children_55",
                        "Children");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_Children_55",
                        "Children");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Visual_Children55CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)Context.Find<Kistl.App.GUI.Visual>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)Context.Find<Kistl.App.GUI.Visual>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.GUI
{
    [EdmEntityType(NamespaceName="Model", Name="Visual_ContextMenu60CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("Visual_ContextMenu60CollectionEntry__Implementation__")]
    public class Visual_ContextMenu60CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, Visual_ContextMenu60CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Visual_60", "Visual")]
        public Kistl.App.GUI.Visual__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_60",
                        "Visual");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_60",
                        "Visual");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_ContextMenu_60", "ContextMenu")]
        public Kistl.App.GUI.Visual__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_ContextMenu_60",
                        "ContextMenu");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Visual_Visual_ContextMenu_60",
                        "ContextMenu");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Visual_ContextMenu60CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)Context.Find<Kistl.App.GUI.Visual>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)Context.Find<Kistl.App.GUI.Visual>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.Zeiterfassung
{
	using Kistl.App.Projekte;
    [EdmEntityType(NamespaceName="Model", Name="Zeitkonto_Mitarbeiter42CollectionEntry")]
    [System.Diagnostics.DebuggerDisplay("Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__")]
    public class Zeitkonto_Mitarbeiter42CollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, Zeitkonto_Mitarbeiter42CollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

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
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Mitarbeiter_Zeitkonto_42", "Zeitkonto")]
        public Kistl.App.Zeiterfassung.Zeitkonto__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__>(
                        "Model.FK_Zeitkonto_Mitarbeiter_Zeitkonto_42",
                        "Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Zeiterfassung.Zeitkonto__Implementation__>(
                        "Model.FK_Zeitkonto_Mitarbeiter_Zeitkonto_42",
                        "Zeitkonto");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)value;
            }
        }
        
        
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
                return B__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                B__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_B
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && B != null)
                {
                    _fk_B = B.ID;
                }
                return _fk_B;
            }
            set
            {
                _fk_B = value;
            }
        }
        private int? _fk_B;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Zeitkonto_Mitarbeiter_Mitarbeiter_42", "Mitarbeiter")]
        public Kistl.App.Projekte.Mitarbeiter__Implementation__ B__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Zeitkonto_Mitarbeiter_Mitarbeiter_42",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter__Implementation__>(
                        "Model.FK_Zeitkonto_Mitarbeiter_Mitarbeiter_42",
                        "Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Mitarbeiter__Implementation__)value;
            }
        }
        
        

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_A, binStream);
            BinarySerializer.ToStream(this.fk_B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_A;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_A = tmp;
            }
            {
                var tmp = this.fk_B;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_B = tmp;
            }
        }

#endregion

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Zeitkonto_Mitarbeiter42CollectionEntry));
		}
	
		public override void ReloadReferences()
		{
			if (_fk_A.HasValue)
				A__Implementation__ = (Kistl.App.Zeiterfassung.Zeitkonto__Implementation__)Context.Find<Kistl.App.Zeiterfassung.Zeitkonto>(_fk_A.Value);
			else
				A__Implementation__ = null;

			if (_fk_B.HasValue)
				B__Implementation__ = (Kistl.App.Projekte.Mitarbeiter__Implementation__)Context.Find<Kistl.App.Projekte.Mitarbeiter>(_fk_B.Value);
			else
				B__Implementation__ = null;
	
		}


    }
}

namespace Kistl.App.Projekte
{
    [EdmEntityType(NamespaceName="Model", Name="Kunde_EMailsCollectionEntry")]    [System.Diagnostics.DebuggerDisplay("Kunde_EMailsCollectionEntry__Implementation__")]
    public class Kunde_EMailsCollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, Kunde_EMailsCollectionEntry
    {
    
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kunde A
        {
            get
            {
                return A__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                A__Implementation__ = (Kunde__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_A
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && A != null)
                {
                    _fk_A = A.ID;
                }
                return _fk_A;
            }
            set
            {
                _fk_A = value;
            }
        }
        private int? _fk_A;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Kunde_String_EMails", "Kunde")]
        public Kunde__Implementation__ A__Implementation__
        {
            get
            {
                EntityReference<Kunde__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kunde__Implementation__>(
                        "Model.FK_Kunde_String_EMails",
                        "Kunde");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kunde__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kunde__Implementation__>(
                        "Model.FK_Kunde_String_EMails",
                        "Kunde");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kunde__Implementation__)value;
            }
        }
        
        
        /// <summary>
        /// the B-side value of this CollectionEntry
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Kunde_EMailsCollectionEntry));
		}
	
		public override void ReloadReferences()
		{
			if (_fk_A.HasValue)
				A__Implementation__ = (Kunde__Implementation__)Context.Find<Kunde>(_fk_A.Value);
			else
				A__Implementation__ = null;
	
		}


    }
}
