// <autogenerated/>


namespace Kistl.App.GUI
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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="PresentableModelDescriptor")]
    [System.Diagnostics.DebuggerDisplay("PresentableModelDescriptor")]
    public class PresentableModelDescriptor__Implementation__ : BaseServerDataObject_EntityFramework, Kistl.API.IExportableInternal, PresentableModelDescriptor
    {
    
		public PresentableModelDescriptor__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var __oldValue = _ID;
                    var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// The default ControlKind to use for this Presentable.
        /// </summary>
    /*
    Relation: FK_PresentableModelDescriptor_has_ControlKind
    A: ZeroOrOne PresentableModelDescriptor as Presentable
    B: ZeroOrOne ControlKind as DefaultKind
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.ControlKind DefaultKind
        {
            get
            {
                return DefaultKind__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                DefaultKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)value;
            }
        }
        
        private int? _fk_DefaultKind;
        private Guid? _fk_guid_DefaultKind = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresentableModelDescriptor_has_ControlKind", "DefaultKind")]
        public Kistl.App.GUI.ControlKind__Implementation__ DefaultKind__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.ControlKind__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.ControlKind__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_has_ControlKind",
                        "DefaultKind");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnDefaultKind_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.GUI.ControlKind>(__value);
					OnDefaultKind_Getter(this, e);
					__value = (Kistl.App.GUI.ControlKind__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.ControlKind__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.ControlKind__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_has_ControlKind",
                        "DefaultKind");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.GUI.ControlKind __oldValue = (Kistl.App.GUI.ControlKind)r.Value;
                Kistl.App.GUI.ControlKind __newValue = (Kistl.App.GUI.ControlKind)value;

                if(OnDefaultKind_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultKind_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.GUI.ControlKind__Implementation__)__newValue;
                if(OnDefaultKind_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultKind_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_Getter;
		public event PropertyPreSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_PostSetter;
        /// <summary>
        /// The default visual type used for this PresentableModel
        /// </summary>
        // enumeration property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.EnumerationPropertyTemplate
        // implement the user-visible interface
        public Kistl.App.GUI.VisualType DefaultVisualType
        {
            get
            {
				var __value = _DefaultVisualType;
				if(OnDefaultVisualType_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.GUI.VisualType>(__value);
					OnDefaultVisualType_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultVisualType != value)
                {
					var __oldValue = _DefaultVisualType;
					var __newValue = value;
                    if(OnDefaultVisualType_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.VisualType>(__oldValue, __newValue);
						OnDefaultVisualType_PreSetter(this, e);
						__newValue = e.Result;
                    }
					
                    NotifyPropertyChanging("DefaultVisualType", "DefaultVisualType__Implementation__", __oldValue, __newValue);
                    _DefaultVisualType = value;
                    NotifyPropertyChanged("DefaultVisualType", "DefaultVisualType__Implementation__", __oldValue, __newValue);
                    if(OnDefaultVisualType_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.VisualType>(__oldValue, __newValue);
						OnDefaultVisualType_PostSetter(this, e);
                    }
                    
                }
            }
        }
        
        /// <summary>backing store for DefaultVisualType</summary>
        private Kistl.App.GUI.VisualType _DefaultVisualType;
        
        /// <summary>EF sees only this property, for DefaultVisualType</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int DefaultVisualType__Implementation__
        {
            get
            {
                return (int)this.DefaultVisualType;
            }
            set
            {
                this.DefaultVisualType = (Kistl.App.GUI.VisualType)value;
            }
        }
        
		public event PropertyGetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.VisualType> OnDefaultVisualType_Getter;
		public event PropertyPreSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.VisualType> OnDefaultVisualType_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.VisualType> OnDefaultVisualType_PostSetter;
        /// <summary>
        /// describe this PresentableModel
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Description
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Description;
                if (OnDescription_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDescription_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
                    var __oldValue = _Description;
                    var __newValue = value;
                    if(OnDescription_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    _Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);
                    if(OnDescription_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Description;
		public event PropertyGetterHandler<Kistl.App.GUI.PresentableModelDescriptor, string> OnDescription_Getter;
		public event PropertyPreSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, string> OnDescription_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, string> OnDescription_PostSetter;
        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        private bool _isExportGuidSet = false;
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual Guid ExportGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ExportGuid;
                if (!_isExportGuidSet) {
                    var __p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("77ce1e5b-f244-4279-af13-b3e75b55f933"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        __result = this._ExportGuid = (Guid)__p.DefaultValue.GetDefaultValue();
                    } else {
                        System.Diagnostics.Trace.TraceWarning("Unable to get default value for property 'PresentableModelDescriptor.ExportGuid'");
                    }
                }
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ExportGuid != value)
                {
                    var __oldValue = _ExportGuid;
                    var __newValue = value;
                    _isExportGuidSet = true;
                    if(OnExportGuid_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    _ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if(OnExportGuid_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
            }
        }
        private Guid _ExportGuid;
		public event PropertyGetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Guid> OnExportGuid_Getter;
		public event PropertyPreSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Guid> OnExportGuid_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Guid> OnExportGuid_PostSetter;
        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_PresentableModelDescriptor_has_Module
    A: ZeroOrMore PresentableModelDescriptor as PresentableModelDescriptor
    B: ZeroOrOne Module as Module
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                return Module__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        private int? _fk_Module;
        private Guid? _fk_guid_Module = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresentableModelDescriptor_has_Module", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_has_Module",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnModule_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Module>(__value);
					OnModule_Getter(this, e);
					__value = (Kistl.App.Base.Module__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_has_Module",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Module __oldValue = (Kistl.App.Base.Module)r.Value;
                Kistl.App.Base.Module __newValue = (Kistl.App.Base.Module)value;

                if(OnModule_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Module>(__oldValue, __newValue);
					OnModule_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Module__Implementation__)__newValue;
                if(OnModule_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Module>(__oldValue, __newValue);
					OnModule_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.Base.Module> OnModule_Getter;
		public event PropertyPreSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.Base.Module> OnModule_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.Base.Module> OnModule_PostSetter;
        /// <summary>
        /// The described CLR class&apos; reference
        /// </summary>
    /*
    Relation: FK_PresentableModelDescriptor_has_TypeRef
    A: ZeroOrMore PresentableModelDescriptor as Descriptor
    B: One TypeRef as PresentableModelRef
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef PresentableModelRef
        {
            get
            {
                return PresentableModelRef__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                PresentableModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        private int? _fk_PresentableModelRef;
        private Guid? _fk_guid_PresentableModelRef = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresentableModelDescriptor_has_TypeRef", "PresentableModelRef")]
        public Kistl.App.Base.TypeRef__Implementation__ PresentableModelRef__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_has_TypeRef",
                        "PresentableModelRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnPresentableModelRef_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.TypeRef>(__value);
					OnPresentableModelRef_Getter(this, e);
					__value = (Kistl.App.Base.TypeRef__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_has_TypeRef",
                        "PresentableModelRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.TypeRef __oldValue = (Kistl.App.Base.TypeRef)r.Value;
                Kistl.App.Base.TypeRef __newValue = (Kistl.App.Base.TypeRef)value;

                if(OnPresentableModelRef_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnPresentableModelRef_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)__newValue;
                if(OnPresentableModelRef_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnPresentableModelRef_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.Base.TypeRef> OnPresentableModelRef_Getter;
		public event PropertyPreSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.Base.TypeRef> OnPresentableModelRef_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.Base.TypeRef> OnPresentableModelRef_PostSetter;
        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_PresentableModelDescriptor_displayedBy_ControlKind
    A: ZeroOrMore PresentableModelDescriptor as Presentable
    B: ZeroOrMore ControlKind as SecondaryControlKinds
    Preferred Storage: Separate
    */
        // collection reference property
		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.ControlKind> SecondaryControlKinds
        {
            get
            {
                if (_SecondaryControlKindsWrapper == null)
                {
                    _SecondaryControlKindsWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.ControlKind, Kistl.App.GUI.PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__>(
                            this,
                            SecondaryControlKinds__Implementation__);
                }
                return _SecondaryControlKindsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_PresentableModelDescriptor_displayedBy_ControlKind_Presentable", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__> SecondaryControlKinds__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_displayedBy_ControlKind_Presentable",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.GUI.PresentableModelDescriptor, Kistl.App.GUI.ControlKind, Kistl.App.GUI.PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__> _SecondaryControlKindsWrapper;


		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresentableModelDescriptor));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PresentableModelDescriptor)obj;
			var otherImpl = (PresentableModelDescriptor__Implementation__)obj;
			var me = (PresentableModelDescriptor)this;

			me.DefaultVisualType = other.DefaultVisualType;
			me.Description = other.Description;
			me.ExportGuid = other.ExportGuid;
			this._fk_DefaultKind = otherImpl._fk_DefaultKind;
			this._fk_Module = otherImpl._fk_Module;
			this._fk_PresentableModelRef = otherImpl._fk_PresentableModelRef;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_PresentableModelDescriptor")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PresentableModelDescriptor != null)
            {
                OnToString_PresentableModelDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresentableModelDescriptor> OnToString_PresentableModelDescriptor;

        [EventBasedMethod("OnPreSave_PresentableModelDescriptor")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresentableModelDescriptor != null) OnPreSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPreSave_PresentableModelDescriptor;

        [EventBasedMethod("OnPostSave_PresentableModelDescriptor")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresentableModelDescriptor != null) OnPostSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPostSave_PresentableModelDescriptor;

        [EventBasedMethod("OnCreated_PresentableModelDescriptor")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_PresentableModelDescriptor != null) OnCreated_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnCreated_PresentableModelDescriptor;

        [EventBasedMethod("OnDeleting_PresentableModelDescriptor")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_PresentableModelDescriptor != null) OnDeleting_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnDeleting_PresentableModelDescriptor;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "DefaultKind":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("b535115c-b847-479d-bdea-a7994ae6eeca")).Constraints
						.Where(c => !c.IsValid(this, this.DefaultKind))
						.Select(c => c.GetErrorText(this, this.DefaultKind))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "DefaultVisualType":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("2ab3364a-561c-40f3-a83a-731ce0f1e2de")).Constraints
						.Where(c => !c.IsValid(this, this.DefaultVisualType))
						.Select(c => c.GetErrorText(this, this.DefaultVisualType))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Description":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("93e25648-50f9-40d8-8753-e5dadab68e1d")).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("77ce1e5b-f244-4279-af13-b3e75b55f933")).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Module":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("0b7135d3-dedc-4091-a0c4-690c1b4a2b6d")).Constraints
						.Where(c => !c.IsValid(this, this.Module))
						.Select(c => c.GetErrorText(this, this.Module))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresentableModelRef":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("554288d1-f5f4-4b22-908b-01525a1d0f9b")).Constraints
						.Where(c => !c.IsValid(this, this.PresentableModelRef))
						.Select(c => c.GetErrorText(this, this.PresentableModelRef))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "SecondaryControlKinds":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("5e2e007c-2e90-4ba6-9c9d-46e62b662ff9")).Constraints
						.Where(c => !c.IsValid(this, this.SecondaryControlKinds))
						.Select(c => c.GetErrorText(this, this.SecondaryControlKinds))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references

			if (_fk_guid_DefaultKind.HasValue)
				DefaultKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)Context.FindPersistenceObject<Kistl.App.GUI.ControlKind>(_fk_guid_DefaultKind.Value);
			else if (_fk_DefaultKind.HasValue)
				DefaultKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)Context.Find<Kistl.App.GUI.ControlKind>(_fk_DefaultKind.Value);
			else
				DefaultKind__Implementation__ = null;

			if (_fk_guid_Module.HasValue)
				Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Module>(_fk_guid_Module.Value);
			else if (_fk_Module.HasValue)
				Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)Context.Find<Kistl.App.Base.Module>(_fk_Module.Value);
			else
				Module__Implementation__ = null;

			if (_fk_guid_PresentableModelRef.HasValue)
				PresentableModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.TypeRef>(_fk_guid_PresentableModelRef.Value);
			else if (_fk_PresentableModelRef.HasValue)
				PresentableModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_PresentableModelRef.Value);
			else
				PresentableModelRef__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(DefaultKind != null ? DefaultKind.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(DefaultKind);
			}
            BinarySerializer.ToStream((int)((PresentableModelDescriptor)this).DefaultVisualType, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(Module != null ? Module.ID : (int?)null, binStream);
            BinarySerializer.ToStream(PresentableModelRef != null ? PresentableModelRef.ID : (int?)null, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_DefaultKind, binStream);
            BinarySerializer.FromStreamConverter(v => ((PresentableModelDescriptor)this).DefaultVisualType = (Kistl.App.GUI.VisualType)v, binStream);
            {
                var tmp = this._Description;
                BinarySerializer.FromStream(out tmp, binStream);
                this._Description = tmp;
            }
            {
                var tmp = this._ExportGuid;
                BinarySerializer.FromStream(out tmp, binStream);
                this._ExportGuid = tmp;
                this._isExportGuidSet = true;
            }
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_PresentableModelRef, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(DefaultKind != null ? DefaultKind.ID : (int?)null, xml, "DefaultKind", "Kistl.App.GUI");
            XmlStreamer.ToStream((int)this.DefaultVisualType, xml, "DefaultVisualType", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            XmlStreamer.ToStream(Module != null ? Module.ID : (int?)null, xml, "Module", "Kistl.App.GUI");
            XmlStreamer.ToStream(PresentableModelRef != null ? PresentableModelRef.ID : (int?)null, xml, "PresentableModelRef", "Kistl.App.GUI");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_DefaultKind, xml, "DefaultKind", "Kistl.App.GUI");
            XmlStreamer.FromStreamConverter(v => ((PresentableModelDescriptor)this).DefaultVisualType = (Kistl.App.GUI.VisualType)v, xml, "DefaultVisualType", "Kistl.App.GUI");
            {
                var tmp = this._Description;
                XmlStreamer.FromStream(ref tmp, xml, "Description", "Kistl.App.GUI");
                this._Description = tmp;
            }
            {
                var tmp = this._ExportGuid;
                XmlStreamer.FromStream(ref tmp, xml, "ExportGuid", "Kistl.App.GUI");
                this._ExportGuid = tmp;
                this._isExportGuidSet = true;
            }
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_PresentableModelRef, xml, "PresentableModelRef", "Kistl.App.GUI");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(DefaultKind != null ? DefaultKind.ExportGuid : (Guid?)null, xml, "DefaultKind", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream((int)this.DefaultVisualType, xml, "DefaultVisualType", "Kistl.App.GUI");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(Module != null ? Module.ExportGuid : (Guid?)null, xml, "Module", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(PresentableModelRef != null ? PresentableModelRef.ExportGuid : (Guid?)null, xml, "PresentableModelRef", "Kistl.App.GUI");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._fk_guid_DefaultKind, xml, "DefaultKind", "Kistl.App.GUI");
            XmlStreamer.FromStreamConverter(v => ((PresentableModelDescriptor)this).DefaultVisualType = (Kistl.App.GUI.VisualType)v, xml, "DefaultVisualType", "Kistl.App.GUI");
            {
                var tmp = this._Description;
                XmlStreamer.FromStream(ref tmp, xml, "Description", "Kistl.App.GUI");
                this._Description = tmp;
            }
            {
                var tmp = this._ExportGuid;
                XmlStreamer.FromStream(ref tmp, xml, "ExportGuid", "Kistl.App.GUI");
                this._ExportGuid = tmp;
                this._isExportGuidSet = true;
            }
            XmlStreamer.FromStream(ref this._fk_guid_Module, xml, "Module", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_guid_PresentableModelRef, xml, "PresentableModelRef", "Kistl.App.GUI");
        }

#endregion

    }


}