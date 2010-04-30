// <autogenerated/>


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
    using Kistl.DalProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Constraint")]
    [System.Diagnostics.DebuggerDisplay("Constraint")]
    public class Constraint__Implementation__ : BaseServerDataObject_EntityFramework, Constraint, Kistl.API.IExportableInternal
    {
    
		public Constraint__Implementation__()
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
        /// Identity which changed this object
        /// </summary>
    /*
    Relation: FK_Constraint_was_ChangedBy
    A: ZeroOrMore Constraint as Constraint
    B: ZeroOrOne Identity as ChangedBy
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Identity ChangedBy
        {
            get
            {
                return ChangedBy__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                ChangedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__)value;
            }
        }
        
        private int? _fk_ChangedBy;
        private Guid? _fk_guid_ChangedBy = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Constraint_was_ChangedBy", "ChangedBy")]
        public Kistl.App.Base.Identity__Implementation__ ChangedBy__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Identity__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Identity__Implementation__>(
                        "Model.FK_Constraint_was_ChangedBy",
                        "ChangedBy");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnChangedBy_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Identity>(__value);
					OnChangedBy_Getter(this, e);
					__value = (Kistl.App.Base.Identity__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Identity__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Identity__Implementation__>(
                        "Model.FK_Constraint_was_ChangedBy",
                        "ChangedBy");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Identity __oldValue = (Kistl.App.Base.Identity)r.Value;
                Kistl.App.Base.Identity __newValue = (Kistl.App.Base.Identity)value;

                if(OnChangedBy_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnChangedBy_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Identity__Implementation__)__newValue;
                if(OnChangedBy_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnChangedBy_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Identity> OnChangedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Identity> OnChangedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Identity> OnChangedBy_PostSetter;

        /// <summary>
        /// Date and time where this object was changed
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? ChangedOn
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ChangedOn;
                if (OnChangedOn_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnChangedOn_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ChangedOn != value)
                {
                    var __oldValue = _ChangedOn;
                    var __newValue = value;
                    if(OnChangedOn_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnChangedOn_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ChangedOn", __oldValue, __newValue);
                    _ChangedOn = __newValue;
                    NotifyPropertyChanged("ChangedOn", __oldValue, __newValue);
                    if(OnChangedOn_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnChangedOn_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _ChangedOn;
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, DateTime?> OnChangedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, DateTime?> OnChangedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, DateTime?> OnChangedOn_PostSetter;

        /// <summary>
        /// The property to be constrained
        /// </summary>
    /*
    Relation: FK_ConstrainedProperty_has_Constraints
    A: One Property as ConstrainedProperty
    B: ZeroOrMore Constraint as Constraints
    Preferred Storage: MergeIntoB
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property ConstrainedProperty
        {
            get
            {
                return ConstrainedProperty__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                ConstrainedProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        private int? _fk_ConstrainedProperty;
        private Guid? _fk_guid_ConstrainedProperty = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ConstrainedProperty_has_Constraints", "ConstrainedProperty")]
        public Kistl.App.Base.Property__Implementation__ ConstrainedProperty__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_ConstrainedProperty_has_Constraints",
                        "ConstrainedProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnConstrainedProperty_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Property>(__value);
					OnConstrainedProperty_Getter(this, e);
					__value = (Kistl.App.Base.Property__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_ConstrainedProperty_has_Constraints",
                        "ConstrainedProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Property __oldValue = (Kistl.App.Base.Property)r.Value;
                Kistl.App.Base.Property __newValue = (Kistl.App.Base.Property)value;

                if(OnConstrainedProperty_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnConstrainedProperty_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Property__Implementation__)__newValue;
                if(OnConstrainedProperty_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnConstrainedProperty_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Property> OnConstrainedProperty_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Property> OnConstrainedProperty_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Property> OnConstrainedProperty_PostSetter;

        /// <summary>
        /// Identity which created this object
        /// </summary>
    /*
    Relation: FK_Constraint_was_CreatedBy
    A: ZeroOrMore Constraint as Constraint
    B: ZeroOrOne Identity as CreatedBy
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Identity CreatedBy
        {
            get
            {
                return CreatedBy__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                CreatedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__)value;
            }
        }
        
        private int? _fk_CreatedBy;
        private Guid? _fk_guid_CreatedBy = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Constraint_was_CreatedBy", "CreatedBy")]
        public Kistl.App.Base.Identity__Implementation__ CreatedBy__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Identity__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Identity__Implementation__>(
                        "Model.FK_Constraint_was_CreatedBy",
                        "CreatedBy");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnCreatedBy_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Identity>(__value);
					OnCreatedBy_Getter(this, e);
					__value = (Kistl.App.Base.Identity__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Identity__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Identity__Implementation__>(
                        "Model.FK_Constraint_was_CreatedBy",
                        "CreatedBy");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Identity __oldValue = (Kistl.App.Base.Identity)r.Value;
                Kistl.App.Base.Identity __newValue = (Kistl.App.Base.Identity)value;

                if(OnCreatedBy_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnCreatedBy_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Identity__Implementation__)__newValue;
                if(OnCreatedBy_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Identity>(__oldValue, __newValue);
					OnCreatedBy_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Identity> OnCreatedBy_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Identity> OnCreatedBy_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, Kistl.App.Base.Identity> OnCreatedBy_PostSetter;

        /// <summary>
        /// Date and time where this object was created
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual DateTime? CreatedOn
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _CreatedOn;
                if (OnCreatedOn_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<DateTime?>(__result);
                    OnCreatedOn_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_CreatedOn != value)
                {
                    var __oldValue = _CreatedOn;
                    var __newValue = value;
                    if(OnCreatedOn_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnCreatedOn_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("CreatedOn", __oldValue, __newValue);
                    _CreatedOn = __newValue;
                    NotifyPropertyChanged("CreatedOn", __oldValue, __newValue);
                    if(OnCreatedOn_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<DateTime?>(__oldValue, __newValue);
                        OnCreatedOn_PostSetter(this, __e);
                    }
                }
            }
        }
        private DateTime? _CreatedOn;
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, DateTime?> OnCreatedOn_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, DateTime?> OnCreatedOn_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, DateTime?> OnCreatedOn_PostSetter;

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
                    var __p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("8da6d02c-9d9e-4db8-91ee-24a3fd1c74e1"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        __result = this._ExportGuid = (Guid)__p.DefaultValue.GetDefaultValue();
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Constraint.ExportGuid'");
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
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, Guid> OnExportGuid_PostSetter;

        /// <summary>
        /// The reason of this constraint
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Reason
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Reason;
                if (OnReason_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnReason_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Reason != value)
                {
                    var __oldValue = _Reason;
                    var __newValue = value;
                    if(OnReason_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnReason_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Reason", __oldValue, __newValue);
                    _Reason = __newValue;
                    NotifyPropertyChanged("Reason", __oldValue, __newValue);
                    if(OnReason_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnReason_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Reason;
		public static event PropertyGetterHandler<Kistl.App.Base.Constraint, string> OnReason_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.Constraint, string> OnReason_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.Constraint, string> OnReason_PostSetter;

        /// <summary>
        /// 
        /// </summary>
		[EventBasedMethod("OnGetErrorText_Constraint")]
		public virtual string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_Constraint != null)
            {
                OnGetErrorText_Constraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.GetErrorText");
            }
            return e.Result;
        }
		public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, System.Object constrainedObject, System.Object constrainedValue);
		public static event GetErrorText_Handler<Constraint> OnGetErrorText_Constraint;



        /// <summary>
        /// 
        /// </summary>
		[EventBasedMethod("OnIsValid_Constraint")]
		public virtual bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_Constraint != null)
            {
                OnIsValid_Constraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.IsValid");
            }
            return e.Result;
        }
		public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, System.Object constrainedObject, System.Object constrainedValue);
		public static event IsValid_Handler<Constraint> OnIsValid_Constraint;



		public override Type GetImplementedInterface()
		{
			return typeof(Constraint);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Constraint)obj;
			var otherImpl = (Constraint__Implementation__)obj;
			var me = (Constraint)this;

			me.ChangedOn = other.ChangedOn;
			me.CreatedOn = other.CreatedOn;
			me.ExportGuid = other.ExportGuid;
			me.Reason = other.Reason;
			this._fk_ChangedBy = otherImpl._fk_ChangedBy;
			this._fk_ConstrainedProperty = otherImpl._fk_ConstrainedProperty;
			this._fk_CreatedBy = otherImpl._fk_CreatedBy;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Constraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Constraint != null)
            {
                OnToString_Constraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Constraint> OnToString_Constraint;

        [EventBasedMethod("OnPreSave_Constraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Constraint != null) OnPreSave_Constraint(this);
        }
        public static event ObjectEventHandler<Constraint> OnPreSave_Constraint;

        [EventBasedMethod("OnPostSave_Constraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Constraint != null) OnPostSave_Constraint(this);
        }
        public static event ObjectEventHandler<Constraint> OnPostSave_Constraint;

        [EventBasedMethod("OnCreated_Constraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Constraint != null) OnCreated_Constraint(this);
        }
        public static event ObjectEventHandler<Constraint> OnCreated_Constraint;

        [EventBasedMethod("OnDeleting_Constraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Constraint != null) OnDeleting_Constraint(this);
        }
        public static event ObjectEventHandler<Constraint> OnDeleting_Constraint;


		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, Kistl.App.Base.Identity>(
				new Guid("b7d3d6d2-6c34-4599-846d-2df3dbf8eda8"),
				"ChangedBy",
				null,
				obj => obj.ChangedBy,
				(obj, val) => obj.ChangedBy = val),
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, DateTime?>(
				new Guid("90d7ec21-a775-46f0-8a30-ef25088dd5eb"),
				"ChangedOn",
				null,
				obj => obj.ChangedOn,
				(obj, val) => obj.ChangedOn = val),
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, Kistl.App.Base.Property>(
				new Guid("438b9307-fb40-4afe-a66f-a5762c41e14b"),
				"ConstrainedProperty",
				null,
				obj => obj.ConstrainedProperty,
				(obj, val) => obj.ConstrainedProperty = val),
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, Kistl.App.Base.Identity>(
				new Guid("51cb0dfc-4156-4dcf-a409-57d0029b4cbb"),
				"CreatedBy",
				null,
				obj => obj.CreatedBy,
				(obj, val) => obj.CreatedBy = val),
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, DateTime?>(
				new Guid("a24ba1ea-4ad5-4ffd-bf24-c0f2df4b8e0c"),
				"CreatedOn",
				null,
				obj => obj.CreatedOn,
				(obj, val) => obj.CreatedOn = val),
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, Guid>(
				new Guid("8da6d02c-9d9e-4db8-91ee-24a3fd1c74e1"),
				"ExportGuid",
				null,
				obj => obj.ExportGuid,
				(obj, val) => obj.ExportGuid = val),
			// else
			new CustomPropertyDescriptor<Constraint__Implementation__, string>(
				new Guid("49f759b3-de60-4cee-be06-c712e901c24e"),
				"Reason",
				null,
				obj => obj.Reason,
				(obj, val) => obj.Reason = val),
			// rel: Constraint was CreatedBy (f7eab863-c425-4819-b435-10394cf1ca5a)
			// rel: Constraint was ChangedBy (6b1b0216-b3cf-4a5f-ae59-e6f46fda9331)
			// rel: ConstrainedProperty has Constraints (6fa271a3-e365-4b8d-9cb1-575d7a3b5d6a)
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

			if (_fk_guid_ChangedBy.HasValue)
				ChangedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Identity>(_fk_guid_ChangedBy.Value);
			else if (_fk_ChangedBy.HasValue)
				ChangedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__)Context.Find<Kistl.App.Base.Identity>(_fk_ChangedBy.Value);
			else
				ChangedBy__Implementation__ = null;

			if (_fk_guid_ConstrainedProperty.HasValue)
				ConstrainedProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Property>(_fk_guid_ConstrainedProperty.Value);
			else if (_fk_ConstrainedProperty.HasValue)
				ConstrainedProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.Find<Kistl.App.Base.Property>(_fk_ConstrainedProperty.Value);
			else
				ConstrainedProperty__Implementation__ = null;

			if (_fk_guid_CreatedBy.HasValue)
				CreatedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Identity>(_fk_guid_CreatedBy.Value);
			else if (_fk_CreatedBy.HasValue)
				CreatedBy__Implementation__ = (Kistl.App.Base.Identity__Implementation__)Context.Find<Kistl.App.Base.Identity>(_fk_CreatedBy.Value);
			else
				CreatedBy__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(ChangedBy != null ? ChangedBy.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._ChangedOn, binStream);
            BinarySerializer.ToStream(ConstrainedProperty != null ? ConstrainedProperty.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(ConstrainedProperty);
			}
            BinarySerializer.ToStream(CreatedBy != null ? CreatedBy.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._CreatedOn, binStream);
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this._Reason, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ChangedBy, binStream);
            BinarySerializer.FromStream(out this._ChangedOn, binStream);
            BinarySerializer.FromStream(out this._fk_ConstrainedProperty, binStream);
            BinarySerializer.FromStream(out this._fk_CreatedBy, binStream);
            BinarySerializer.FromStream(out this._CreatedOn, binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._Reason, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(ChangedBy != null ? ChangedBy.ID : (int?)null, xml, "ChangedBy", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.ToStream(ConstrainedProperty != null ? ConstrainedProperty.ID : (int?)null, xml, "ConstrainedProperty", "Kistl.App.Base");
            XmlStreamer.ToStream(CreatedBy != null ? CreatedBy.ID : (int?)null, xml, "CreatedBy", "Kistl.App.Base");
            XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_ChangedBy, xml, "ChangedBy", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_ConstrainedProperty, xml, "ConstrainedProperty", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_CreatedBy, xml, "CreatedBy", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.FromStream(ref this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(ConstrainedProperty != null ? ConstrainedProperty.ExportGuid : (Guid?)null, xml, "ConstrainedProperty", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
    
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ChangedOn, xml, "ChangedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_ConstrainedProperty, xml, "ConstrainedProperty", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._CreatedOn, xml, "CreatedOn", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            this._isExportGuidSet = true;
            XmlStreamer.FromStream(ref this._Reason, xml, "Reason", "Kistl.App.Base");
        }

#endregion

    }


}