
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for ObjectClasses.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ObjectClass")]
    [System.Diagnostics.DebuggerDisplay("ObjectClass")]
    public class ObjectClass__Implementation__ : Kistl.App.Base.DataType__Implementation__, ObjectClass
    {


        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
    /*
    NewRelation: FK_ObjectClass_ObjectClass_BaseObjectClass_4 
    A: ZeroOrOne ObjectClass as BaseObjectClass (site: A, from relation ID = 4)
    B: ZeroOrMore ObjectClass as SubClasses (site: B, from relation ID = 4)
    Preferred Storage: MergeB
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                return BaseObjectClass__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                BaseObjectClass__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_BaseObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && BaseObjectClass != null)
                {
                    _fk_BaseObjectClass = BaseObjectClass.ID;
                }
                return _fk_BaseObjectClass;
            }
            set
            {
                _fk_BaseObjectClass = value;
            }
        }
        private int _fk_BaseObjectClass;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_4", "BaseObjectClass")]
        public Kistl.App.Base.ObjectClass__Implementation__ BaseObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_ObjectClass_BaseObjectClass_4",
                        "BaseObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_ObjectClass_BaseObjectClass_4",
                        "BaseObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The default model to use for the UI
        /// </summary>
    /*
    NewRelation: FK_ObjectClass_TypeRef_ObjectClass_50 
    A: ZeroOrMore ObjectClass as ObjectClass (site: A, no Relation, prop ID=212)
    B: ZeroOrOne TypeRef as DefaultModel (site: B, no Relation, prop ID=212)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef DefaultModel
        {
            get
            {
                return DefaultModel__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                DefaultModel__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_DefaultModel
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && DefaultModel != null)
                {
                    _fk_DefaultModel = DefaultModel.ID;
                }
                return _fk_DefaultModel;
            }
            set
            {
                _fk_DefaultModel = value;
            }
        }
        private int _fk_DefaultModel;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_TypeRef_ObjectClass_50", "DefaultModel")]
        public Kistl.App.Base.TypeRef__Implementation__ DefaultModel__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ObjectClass_TypeRef_ObjectClass_50",
                        "DefaultModel");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ObjectClass_TypeRef_ObjectClass_50",
                        "DefaultModel");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
    /*
    NewRelation: FK_ObjectClass_Interface_ObjectClass_29 
    A: ZeroOrMore ObjectClass as ObjectClass (site: A, no Relation, prop ID=105)
    B: ZeroOrMore Interface as ImplementsInterfaces (site: B, no Relation, prop ID=105)
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
        {
            get
            {
                if (_ImplementsInterfacesWrapper == null)
                {
                    _ImplementsInterfacesWrapper = new EntityCollectionBSideWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.Interface, Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__>(
                            this,
                            ImplementsInterfaces__Implementation__);
                }
                return _ImplementsInterfacesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_Interface_ObjectClass_29", "CollectionEntry")]
        public EntityCollection<Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__> ImplementsInterfaces__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__>(
                        "Model.FK_ObjectClass_Interface_ObjectClass_29",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionBSideWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.Interface, Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry__Implementation__> _ImplementsInterfacesWrapper;
        

        /// <summary>
        /// if true then all Instances appear in FozenContext.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsFrozenObject
        {
            get
            {
                return _IsFrozenObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsFrozenObject != value)
                {
                    NotifyPropertyChanging("IsFrozenObject");
                    _IsFrozenObject = value;
                    NotifyPropertyChanged("IsFrozenObject");
                }
            }
        }
        private bool _IsFrozenObject;

        /// <summary>
        /// Setting this to true marks the instances of this class as "simple." At first this will only mean that they'll be displayed inline.
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsSimpleObject
        {
            get
            {
                return _IsSimpleObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsSimpleObject != value)
                {
                    NotifyPropertyChanging("IsSimpleObject");
                    _IsSimpleObject = value;
                    NotifyPropertyChanged("IsSimpleObject");
                }
            }
        }
        private bool _IsSimpleObject;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
    /*
    NewRelation: FK_ObjectClass_ObjectClass_BaseObjectClass_4 
    A: ZeroOrOne ObjectClass as BaseObjectClass (site: A, from relation ID = 4)
    B: ZeroOrMore ObjectClass as SubClasses (site: B, from relation ID = 4)
    Preferred Storage: MergeB
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                if (_SubClassesWrapper == null)
                {
                    _SubClassesWrapper = new EntityCollectionWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.ObjectClass__Implementation__>(
                            SubClasses__Implementation__);
                }
                return _SubClassesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_4", "SubClasses")]
        public EntityCollection<Kistl.App.Base.ObjectClass__Implementation__> SubClasses__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_ObjectClass_BaseObjectClass_4",
                        "SubClasses");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.ObjectClass__Implementation__> _SubClassesWrapper;



        /// <summary>
        /// Tabellenname in der Datenbank
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TableName != value)
                {
                    NotifyPropertyChanging("TableName");
                    _TableName = value;
                    NotifyPropertyChanged("TableName");
                }
            }
        }
        private string _TableName;

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_ObjectClass != null)
            {
                OnGetDataType_ObjectClass(this, e);
            };
            return e.Result;
        }
		public event GetDataType_Handler<ObjectClass> OnGetDataType_ObjectClass;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_ObjectClass != null)
            {
                OnGetDataTypeString_ObjectClass(this, e);
            };
            return e.Result;
        }
		public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual Kistl.App.Base.TypeRef GetDefaultModelRef() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.TypeRef>();
            if (OnGetDefaultModelRef_ObjectClass != null)
            {
                OnGetDefaultModelRef_ObjectClass(this, e);
            };
            return e.Result;
        }
		public delegate void GetDefaultModelRef_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.TypeRef> ret);
		public event GetDefaultModelRef_Handler<ObjectClass> OnGetDefaultModelRef_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual IList<Kistl.App.Base.Method> GetInheritedMethods() 
        {
            var e = new MethodReturnEventArgs<IList<Kistl.App.Base.Method>>();
            if (OnGetInheritedMethods_ObjectClass != null)
            {
                OnGetInheritedMethods_ObjectClass(this, e);
            };
            return e.Result;
        }
		public delegate void GetInheritedMethods_Handler<T>(T obj, MethodReturnEventArgs<IList<Kistl.App.Base.Method>> ret);
		public event GetInheritedMethods_Handler<ObjectClass> OnGetInheritedMethods_ObjectClass;



		public override Type GetInterfaceType()
		{
			return typeof(ObjectClass);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectClass != null)
            {
                OnToString_ObjectClass(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectClass> OnToString_ObjectClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectClass != null) OnPreSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPreSave_ObjectClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectClass != null) OnPostSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPostSave_ObjectClass;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_BaseObjectClass, binStream);
            BinarySerializer.ToStream(this._fk_DefaultModel, binStream);
            BinarySerializer.ToStreamCollectionEntries(this.ImplementsInterfaces__Implementation__, binStream);
            BinarySerializer.ToStream(this._IsFrozenObject, binStream);
            BinarySerializer.ToStream(this._IsSimpleObject, binStream);
            BinarySerializer.ToStream(this._TableName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_BaseObjectClass, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultModel, binStream);
            BinarySerializer.FromStreamCollectionEntries(this.ImplementsInterfaces__Implementation__, binStream);
            BinarySerializer.FromStream(out this._IsFrozenObject, binStream);
            BinarySerializer.FromStream(out this._IsSimpleObject, binStream);
            BinarySerializer.FromStream(out this._TableName, binStream);
        }

#endregion

    }


}