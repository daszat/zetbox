
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
    
		public ObjectClass__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
    /*
    Relation: FK_ObjectClass_ObjectClass_BaseObjectClass_24
    A: ZeroOrOne ObjectClass as BaseObjectClass
    B: ZeroOrMore ObjectClass as SubClasses
    Preferred Storage: Right
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
        public int? fk_BaseObjectClass
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
        private int? _fk_BaseObjectClass;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_24", "BaseObjectClass")]
        public Kistl.App.Base.ObjectClass__Implementation__ BaseObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_ObjectClass_BaseObjectClass_24",
                        "BaseObjectClass");
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
                        "Model.FK_ObjectClass_ObjectClass_BaseObjectClass_24",
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
    Relation: FK_ObjectClass_TypeRef_ObjectClass_70
    A: ZeroOrMore ObjectClass as ObjectClass
    B: ZeroOrOne TypeRef as DefaultModel
    Preferred Storage: Left
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
        public int? fk_DefaultModel
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
        private int? _fk_DefaultModel;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_TypeRef_ObjectClass_70", "DefaultModel")]
        public Kistl.App.Base.TypeRef__Implementation__ DefaultModel__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_ObjectClass_TypeRef_ObjectClass_70",
                        "DefaultModel");
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
                        "Model.FK_ObjectClass_TypeRef_ObjectClass_70",
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
        /// The default PresentableModel to use for this ObjectClass
        /// </summary>
    /*
    Relation: FK_ObjectClass_PresentableModelDescriptor_Presentable_78
    A: ZeroOrMore ObjectClass as Presentable
    B: One PresentableModelDescriptor as DefaultPresentableModelDescriptor
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.PresentableModelDescriptor DefaultPresentableModelDescriptor
        {
            get
            {
                return DefaultPresentableModelDescriptor__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                DefaultPresentableModelDescriptor__Implementation__ = (Kistl.App.GUI.PresentableModelDescriptor__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DefaultPresentableModelDescriptor
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && DefaultPresentableModelDescriptor != null)
                {
                    _fk_DefaultPresentableModelDescriptor = DefaultPresentableModelDescriptor.ID;
                }
                return _fk_DefaultPresentableModelDescriptor;
            }
            set
            {
                _fk_DefaultPresentableModelDescriptor = value;
            }
        }
        private int? _fk_DefaultPresentableModelDescriptor;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_PresentableModelDescriptor_Presentable_78", "DefaultPresentableModelDescriptor")]
        public Kistl.App.GUI.PresentableModelDescriptor__Implementation__ DefaultPresentableModelDescriptor__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__>(
                        "Model.FK_ObjectClass_PresentableModelDescriptor_Presentable_78",
                        "DefaultPresentableModelDescriptor");
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
                EntityReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.PresentableModelDescriptor__Implementation__>(
                        "Model.FK_ObjectClass_PresentableModelDescriptor_Presentable_78",
                        "DefaultPresentableModelDescriptor");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.PresentableModelDescriptor__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
    /*
    Relation: FK_ObjectClass_Interface_ObjectClass_49
    A: ZeroOrMore ObjectClass as ObjectClass
    B: ZeroOrMore Interface as ImplementsInterfaces
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
                    _ImplementsInterfacesWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.Interface, Kistl.App.Base.ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__>(
                            this,
                            ImplementsInterfaces__Implementation__);
                }
                return _ImplementsInterfacesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_Interface_ObjectClass_49", "CollectionEntry")]
        public EntityCollection<Kistl.App.Base.ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__> ImplementsInterfaces__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__>(
                        "Model.FK_ObjectClass_Interface_ObjectClass_49",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.Interface, Kistl.App.Base.ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__> _ImplementsInterfacesWrapper;
        

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
					var __oldValue = _IsFrozenObject;
                    NotifyPropertyChanging("IsFrozenObject", __oldValue, value);
                    _IsFrozenObject = value;
                    NotifyPropertyChanged("IsFrozenObject", __oldValue, value);
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
					var __oldValue = _IsSimpleObject;
                    NotifyPropertyChanging("IsSimpleObject", __oldValue, value);
                    _IsSimpleObject = value;
                    NotifyPropertyChanged("IsSimpleObject", __oldValue, value);
                }
            }
        }
        private bool _IsSimpleObject;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
    /*
    Relation: FK_ObjectClass_ObjectClass_BaseObjectClass_24
    A: ZeroOrOne ObjectClass as BaseObjectClass
    B: ZeroOrMore ObjectClass as SubClasses
    Preferred Storage: Right
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
                            this.Context, SubClasses__Implementation__);
                }
                return _SubClassesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectClass_ObjectClass_BaseObjectClass_24", "SubClasses")]
        public EntityCollection<Kistl.App.Base.ObjectClass__Implementation__> SubClasses__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectClass_ObjectClass_BaseObjectClass_24",
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
					var __oldValue = _TableName;
                    NotifyPropertyChanging("TableName", __oldValue, value);
                    _TableName = value;
                    NotifyPropertyChanged("TableName", __oldValue, value);
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
            }
            else
            {
                e.Result = base.GetDataType();
            }
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
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual IList<Kistl.App.Base.Method> GetInheritedMethods() 
        {
            var e = new MethodReturnEventArgs<IList<Kistl.App.Base.Method>>();
            if (OnGetInheritedMethods_ObjectClass != null)
            {
                OnGetInheritedMethods_ObjectClass(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ObjectClass.GetInheritedMethods");
            }
            return e.Result;
        }
		public delegate void GetInheritedMethods_Handler<T>(T obj, MethodReturnEventArgs<IList<Kistl.App.Base.Method>> ret);
		public event GetInheritedMethods_Handler<ObjectClass> OnGetInheritedMethods_ObjectClass;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectClass));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ObjectClass)obj;
			var otherImpl = (ObjectClass__Implementation__)obj;
			var me = (ObjectClass)this;

			me.IsFrozenObject = other.IsFrozenObject;
			me.IsSimpleObject = other.IsSimpleObject;
			me.TableName = other.TableName;
			this.fk_BaseObjectClass = otherImpl.fk_BaseObjectClass;
			this.fk_DefaultModel = otherImpl.fk_DefaultModel;
			this.fk_DefaultPresentableModelDescriptor = otherImpl.fk_DefaultPresentableModelDescriptor;
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "BaseObjectClass":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(25).Constraints
						.Where(c => !c.IsValid(this, this.BaseObjectClass))
						.Select(c => c.GetErrorText(this, this.BaseObjectClass))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DefaultModel":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(212).Constraints
						.Where(c => !c.IsValid(this, this.DefaultModel))
						.Select(c => c.GetErrorText(this, this.DefaultModel))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DefaultPresentableModelDescriptor":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(234).Constraints
						.Where(c => !c.IsValid(this, this.DefaultPresentableModelDescriptor))
						.Select(c => c.GetErrorText(this, this.DefaultPresentableModelDescriptor))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ImplementsInterfaces":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(105).Constraints
						.Where(c => !c.IsValid(this, this.ImplementsInterfaces))
						.Select(c => c.GetErrorText(this, this.ImplementsInterfaces))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsFrozenObject":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(174).Constraints
						.Where(c => !c.IsValid(this, this.IsFrozenObject))
						.Select(c => c.GetErrorText(this, this.IsFrozenObject))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsSimpleObject":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(119).Constraints
						.Where(c => !c.IsValid(this, this.IsSimpleObject))
						.Select(c => c.GetErrorText(this, this.IsSimpleObject))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "SubClasses":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(27).Constraints
						.Where(c => !c.IsValid(this, this.SubClasses))
						.Select(c => c.GetErrorText(this, this.SubClasses))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "TableName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(3).Constraints
						.Where(c => !c.IsValid(this, this.TableName))
						.Select(c => c.GetErrorText(this, this.TableName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			base.ReloadReferences();
			
			// fix direct object references
			if (_fk_BaseObjectClass.HasValue)
				BaseObjectClass__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)Context.Find<Kistl.App.Base.ObjectClass>(_fk_BaseObjectClass.Value);
			else
				BaseObjectClass__Implementation__ = null;
			if (_fk_DefaultModel.HasValue)
				DefaultModel__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_DefaultModel.Value);
			else
				DefaultModel__Implementation__ = null;
			if (_fk_DefaultPresentableModelDescriptor.HasValue)
				DefaultPresentableModelDescriptor__Implementation__ = (Kistl.App.GUI.PresentableModelDescriptor__Implementation__)Context.Find<Kistl.App.GUI.PresentableModelDescriptor>(_fk_DefaultPresentableModelDescriptor.Value);
			else
				DefaultPresentableModelDescriptor__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_BaseObjectClass, binStream);
            BinarySerializer.ToStream(this.fk_DefaultModel, binStream);
            BinarySerializer.ToStream(this.fk_DefaultPresentableModelDescriptor, binStream);
            BinarySerializer.ToStream(this._IsFrozenObject, binStream);
            BinarySerializer.ToStream(this._IsSimpleObject, binStream);
            BinarySerializer.ToStream(this._TableName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_BaseObjectClass;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_BaseObjectClass = tmp;
            }
            {
                var tmp = this.fk_DefaultModel;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_DefaultModel = tmp;
            }
            {
                var tmp = this.fk_DefaultPresentableModelDescriptor;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_DefaultPresentableModelDescriptor = tmp;
            }
            BinarySerializer.FromStream(out this._IsFrozenObject, binStream);
            BinarySerializer.FromStream(out this._IsSimpleObject, binStream);
            BinarySerializer.FromStream(out this._TableName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this.fk_BaseObjectClass, xml, "BaseObjectClass", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_DefaultModel, xml, "DefaultModel", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_DefaultPresentableModelDescriptor, xml, "DefaultPresentableModelDescriptor", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._IsFrozenObject, xml, "IsFrozenObject", "Kistl.App.Base");
            XmlStreamer.ToStream(this._IsSimpleObject, xml, "IsSimpleObject", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._TableName, xml, "TableName", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            {
                var tmp = this.fk_BaseObjectClass;
                XmlStreamer.FromStream(ref tmp, xml, "BaseObjectClass", "http://dasz.at/Kistl");
                this.fk_BaseObjectClass = tmp;
            }
            {
                var tmp = this.fk_DefaultModel;
                XmlStreamer.FromStream(ref tmp, xml, "DefaultModel", "http://dasz.at/Kistl");
                this.fk_DefaultModel = tmp;
            }
            {
                var tmp = this.fk_DefaultPresentableModelDescriptor;
                XmlStreamer.FromStream(ref tmp, xml, "DefaultPresentableModelDescriptor", "http://dasz.at/Kistl");
                this.fk_DefaultPresentableModelDescriptor = tmp;
            }
            XmlStreamer.FromStream(ref this._IsFrozenObject, xml, "IsFrozenObject", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsSimpleObject, xml, "IsSimpleObject", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._TableName, xml, "TableName", "Kistl.App.Base");
        }

#endregion

    }


}