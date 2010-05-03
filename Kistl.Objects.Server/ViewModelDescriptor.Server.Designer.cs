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
    using Kistl.DalProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ViewModelDescriptor")]
    [System.Diagnostics.DebuggerDisplay("ViewModelDescriptor")]
    public class ViewModelDescriptor__Implementation__ : BaseServerDataObject_EntityFramework, Kistl.API.IExportableInternal, ViewModelDescriptor
    {
    
		public ViewModelDescriptor__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.IdProperty
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
        /// The default ControlKind for displaying this model in a GridCell
        /// </summary>
    /*
    Relation: FK_ViewModelDescriptor_displayedInGridBy_DefaultGridCellKind
    A: ZeroOrOne ViewModelDescriptor as ViewModelDescriptor
    B: ZeroOrOne ControlKind as DefaultGridCellKind
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.ControlKind DefaultGridCellKind
        {
            get
            {
                return DefaultGridCellKind__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                DefaultGridCellKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)value;
            }
        }
        
        private int? _fk_DefaultGridCellKind;
        private Guid? _fk_guid_DefaultGridCellKind = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ViewModelDescriptor_displayedInGridBy_DefaultGridCellKind", "DefaultGridCellKind")]
        public Kistl.App.GUI.ControlKind__Implementation__ DefaultGridCellKind__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.ControlKind__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.ControlKind__Implementation__>(
                        "Model.FK_ViewModelDescriptor_displayedInGridBy_DefaultGridCellKind",
                        "DefaultGridCellKind");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnDefaultGridCellKind_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.GUI.ControlKind>(__value);
					OnDefaultGridCellKind_Getter(this, e);
					__value = (Kistl.App.GUI.ControlKind__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.GUI.ControlKind__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.ControlKind__Implementation__>(
                        "Model.FK_ViewModelDescriptor_displayedInGridBy_DefaultGridCellKind",
                        "DefaultGridCellKind");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.GUI.ControlKind __oldValue = (Kistl.App.GUI.ControlKind)r.Value;
                Kistl.App.GUI.ControlKind __newValue = (Kistl.App.GUI.ControlKind)value;

                if(OnDefaultGridCellKind_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultGridCellKind_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.GUI.ControlKind__Implementation__)__newValue;
                if(OnDefaultGridCellKind_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.ControlKind>(__oldValue, __newValue);
					OnDefaultGridCellKind_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultGridCellKind_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultGridCellKind_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultGridCellKind_PostSetter;

        /// <summary>
        /// The default ControlKind to use for this Presentable.
        /// </summary>
    /*
    Relation: FK_Presentable_has_DefaultKind
    A: ZeroOrOne ViewModelDescriptor as Presentable
    B: ZeroOrOne ControlKind as DefaultKind
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
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
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                DefaultKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)value;
            }
        }
        
        private int? _fk_DefaultKind;
        private Guid? _fk_guid_DefaultKind = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Presentable_has_DefaultKind", "DefaultKind")]
        public Kistl.App.GUI.ControlKind__Implementation__ DefaultKind__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.ControlKind__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.ControlKind__Implementation__>(
                        "Model.FK_Presentable_has_DefaultKind",
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
                        "Model.FK_Presentable_has_DefaultKind",
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
        
        
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind> OnDefaultKind_PostSetter;

        /// <summary>
        /// describe this ViewModel
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
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
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, string> OnDescription_PostSetter;

        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        private bool _isExportGuidSet = false;
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
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
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'ViewModelDescriptor.ExportGuid'");
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
                _isExportGuidSet = true;
                if (_ExportGuid != value)
                {
                    var __oldValue = _ExportGuid;
                    var __newValue = value;
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
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_ViewModelDescriptor_has_Module
    A: ZeroOrMore ViewModelDescriptor as ViewModelDescriptor
    B: One Module as Module
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
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
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        private int? _fk_Module;
        private Guid? _fk_guid_Module = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ViewModelDescriptor_has_Module", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_ViewModelDescriptor_has_Module",
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
                        "Model.FK_ViewModelDescriptor_has_Module",
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
        
        
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.Module> OnModule_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.Module> OnModule_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.Module> OnModule_PostSetter;

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Presentable_displayedBy_SecondaryControlKinds
    A: ZeroOrMore ViewModelDescriptor as Presentable
    B: ZeroOrMore ControlKind as SecondaryControlKinds
    Preferred Storage: Separate
    */
        // collection reference property
		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.ControlKind> SecondaryControlKinds
        {
            get
            {
                if (_SecondaryControlKindsWrapper == null)
                {
                    _SecondaryControlKindsWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind, Kistl.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__>(
                            this,
                            SecondaryControlKinds__Implementation__);
                }
                return _SecondaryControlKindsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Presentable_displayedBy_SecondaryControlKinds_A", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__> SecondaryControlKinds__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__>(
                        "Model.FK_Presentable_displayedBy_SecondaryControlKinds_A",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                    c.ForEach(i => i.AttachToContext(Context));
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.GUI.ControlKind, Kistl.App.GUI.ViewModelDescriptor_displayedBy_ControlKind_RelationEntry__Implementation__> _SecondaryControlKindsWrapper;


        /// <summary>
        /// The described CLR class&apos; reference
        /// </summary>
    /*
    Relation: FK_Descriptor_has_ViewModelRef
    A: ZeroOrMore ViewModelDescriptor as Descriptor
    B: One TypeRef as ViewModelRef
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef ViewModelRef
        {
            get
            {
                return ViewModelRef__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                ViewModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        private int? _fk_ViewModelRef;
        private Guid? _fk_guid_ViewModelRef = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Descriptor_has_ViewModelRef", "ViewModelRef")]
        public Kistl.App.Base.TypeRef__Implementation__ ViewModelRef__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_Descriptor_has_ViewModelRef",
                        "ViewModelRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnViewModelRef_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.TypeRef>(__value);
					OnViewModelRef_Getter(this, e);
					__value = (Kistl.App.Base.TypeRef__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_Descriptor_has_ViewModelRef",
                        "ViewModelRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.TypeRef __oldValue = (Kistl.App.Base.TypeRef)r.Value;
                Kistl.App.Base.TypeRef __newValue = (Kistl.App.Base.TypeRef)value;

                if(OnViewModelRef_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnViewModelRef_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)__newValue;
                if(OnViewModelRef_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnViewModelRef_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public static event PropertyGetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.TypeRef> OnViewModelRef_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.TypeRef> OnViewModelRef_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.ViewModelDescriptor, Kistl.App.Base.TypeRef> OnViewModelRef_PostSetter;

		public override Type GetImplementedInterface()
		{
			return typeof(ViewModelDescriptor);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ViewModelDescriptor)obj;
			var otherImpl = (ViewModelDescriptor__Implementation__)obj;
			var me = (ViewModelDescriptor)this;

			me.Description = other.Description;
			me.ExportGuid = other.ExportGuid;
			this._fk_DefaultGridCellKind = otherImpl._fk_DefaultGridCellKind;
			this._fk_DefaultKind = otherImpl._fk_DefaultKind;
			this._fk_Module = otherImpl._fk_Module;
			this._fk_ViewModelRef = otherImpl._fk_ViewModelRef;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ViewModelDescriptor")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ViewModelDescriptor != null)
            {
                OnToString_ViewModelDescriptor(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ViewModelDescriptor> OnToString_ViewModelDescriptor;

        [EventBasedMethod("OnPreSave_ViewModelDescriptor")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ViewModelDescriptor != null) OnPreSave_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnPreSave_ViewModelDescriptor;

        [EventBasedMethod("OnPostSave_ViewModelDescriptor")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ViewModelDescriptor != null) OnPostSave_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnPostSave_ViewModelDescriptor;

        [EventBasedMethod("OnCreated_ViewModelDescriptor")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_ViewModelDescriptor != null) OnCreated_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnCreated_ViewModelDescriptor;

        [EventBasedMethod("OnDeleting_ViewModelDescriptor")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_ViewModelDescriptor != null) OnDeleting_ViewModelDescriptor(this);
        }
        public static event ObjectEventHandler<ViewModelDescriptor> OnDeleting_ViewModelDescriptor;


		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {
			// else
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, Kistl.App.GUI.ControlKind>(
				new Guid("6c744476-35e0-4cef-a221-f02abc81566c"),
				"DefaultGridCellKind",
				null,
				obj => obj.DefaultGridCellKind,
				(obj, val) => obj.DefaultGridCellKind = val),
			// else
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, Kistl.App.GUI.ControlKind>(
				new Guid("b535115c-b847-479d-bdea-a7994ae6eeca"),
				"DefaultKind",
				null,
				obj => obj.DefaultKind,
				(obj, val) => obj.DefaultKind = val),
			// else
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, string>(
				new Guid("93e25648-50f9-40d8-8753-e5dadab68e1d"),
				"Description",
				null,
				obj => obj.Description,
				(obj, val) => obj.Description = val),
			// else
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, Guid>(
				new Guid("77ce1e5b-f244-4279-af13-b3e75b55f933"),
				"ExportGuid",
				null,
				obj => obj.ExportGuid,
				(obj, val) => obj.ExportGuid = val),
			// else
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, Kistl.App.Base.Module>(
				new Guid("0b7135d3-dedc-4091-a0c4-690c1b4a2b6d"),
				"Module",
				null,
				obj => obj.Module,
				(obj, val) => obj.Module = val),
			// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, ICollection<Kistl.App.GUI.ControlKind>>(
				new Guid("5e2e007c-2e90-4ba6-9c9d-46e62b662ff9"),
				"SecondaryControlKinds",
				null,
				obj => obj.SecondaryControlKinds,
				null), // lists are read-only properties
			// else
			new CustomPropertyDescriptor<ViewModelDescriptor__Implementation__, Kistl.App.Base.TypeRef>(
				new Guid("554288d1-f5f4-4b22-908b-01525a1d0f9b"),
				"ViewModelRef",
				null,
				obj => obj.ViewModelRef,
				(obj, val) => obj.ViewModelRef = val),
			// rel: Presentable has DefaultViewModelDescriptor (1ae94c81-3359-45e8-b97a-b61add91abba)
			// rel: Property has ValueModelDescriptor (3437ea5d-d926-4a0b-a848-9dafedf7ad6a)
			// rel: ViewModelDescriptor has Module (557dbc1c-2a38-4c77-8544-264a95307980)
			// rel: Descriptor has ViewModelRef (9d771d87-3b28-4e5e-be33-ea71028e1720)
		};
		
		protected override void CollectProperties(List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			props.AddRange(_properties);
		}
	

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references

			if (_fk_guid_DefaultGridCellKind.HasValue)
				DefaultGridCellKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)Context.FindPersistenceObject<Kistl.App.GUI.ControlKind>(_fk_guid_DefaultGridCellKind.Value);
			else if (_fk_DefaultGridCellKind.HasValue)
				DefaultGridCellKind__Implementation__ = (Kistl.App.GUI.ControlKind__Implementation__)Context.Find<Kistl.App.GUI.ControlKind>(_fk_DefaultGridCellKind.Value);
			else
				DefaultGridCellKind__Implementation__ = null;

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

			if (_fk_guid_ViewModelRef.HasValue)
				ViewModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.TypeRef>(_fk_guid_ViewModelRef.Value);
			else if (_fk_ViewModelRef.HasValue)
				ViewModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_ViewModelRef.Value);
			else
				ViewModelRef__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(DefaultGridCellKind != null ? DefaultGridCellKind.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(DefaultGridCellKind);
			}
            BinarySerializer.ToStream(DefaultKind != null ? DefaultKind.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(DefaultKind);
			}
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(Module != null ? Module.ID : (int?)null, binStream);
            BinarySerializer.ToStream(ViewModelRef != null ? ViewModelRef.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(ViewModelRef);
			}
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_DefaultGridCellKind, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultKind, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_ViewModelRef, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(DefaultGridCellKind != null ? DefaultGridCellKind.ID : (int?)null, xml, "DefaultGridCellKind", "Kistl.App.GUI");
            XmlStreamer.ToStream(DefaultKind != null ? DefaultKind.ID : (int?)null, xml, "DefaultKind", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.GUI");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            }
            XmlStreamer.ToStream(Module != null ? Module.ID : (int?)null, xml, "Module", "Kistl.App.GUI");
            XmlStreamer.ToStream(ViewModelRef != null ? ViewModelRef.ID : (int?)null, xml, "ViewModelRef", "Kistl.App.GUI");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_DefaultGridCellKind, xml, "DefaultGridCellKind", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_DefaultKind, xml, "DefaultKind", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.GUI");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            }
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_ViewModelRef, xml, "ViewModelRef", "Kistl.App.GUI");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(DefaultGridCellKind != null ? DefaultGridCellKind.ExportGuid : (Guid?)null, xml, "DefaultGridCellKind", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(DefaultKind != null ? DefaultKind.ExportGuid : (Guid?)null, xml, "DefaultKind", "Kistl.App.GUI");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(Module != null ? Module.ExportGuid : (Guid?)null, xml, "Module", "Kistl.App.GUI");
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(ViewModelRef != null ? ViewModelRef.ExportGuid : (Guid?)null, xml, "ViewModelRef", "Kistl.App.GUI");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._fk_guid_DefaultGridCellKind, xml, "DefaultGridCellKind", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_guid_DefaultKind, xml, "DefaultKind", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.GUI");
            this._isExportGuidSet = true;
            XmlStreamer.FromStream(ref this._fk_guid_Module, xml, "Module", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_guid_ViewModelRef, xml, "ViewModelRef", "Kistl.App.GUI");
        }

#endregion

    }


}