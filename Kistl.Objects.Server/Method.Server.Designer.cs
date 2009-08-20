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
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for Methods.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Method")]
    [System.Diagnostics.DebuggerDisplay("Method")]
    public class Method__Implementation__ : BaseServerDataObject_EntityFramework, Kistl.API.IExportableInternal, Method
    {
    
		public Method__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
				return _ID;
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
        /// Description of this Method
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string Description
        {
            get
            {
				var __value = _Description;
				if(OnDescription_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnDescription_Getter(this, e);
					__value = e.Result;
				}
                return __value;
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
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnDescription_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    _Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);

                    if(OnDescription_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnDescription_PostSetter(this, e);
                    }
                }
            }
        }
        private string _Description;
		public event PropertyGetterHandler<Kistl.App.Base.Method, string> OnDescription_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.Method, string> OnDescription_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.Method, string> OnDescription_PostSetter;
        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual Guid ExportGuid
        {
            get
            {
				var __value = _ExportGuid;
				if(OnExportGuid_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Guid>(__value);
					OnExportGuid_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ExportGuid != value)
                {
					var __oldValue = _ExportGuid;
					var __newValue = value;
                    if(OnExportGuid_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
						OnExportGuid_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    _ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);

                    if(OnExportGuid_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
						OnExportGuid_PostSetter(this, e);
                    }
                }
            }
        }
        private Guid _ExportGuid;
		public event PropertyGetterHandler<Kistl.App.Base.Method, Guid> OnExportGuid_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.Method, Guid> OnExportGuid_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.Method, Guid> OnExportGuid_PostSetter;
        /// <summary>
        /// Shows this Method in th GUI
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual bool IsDisplayable
        {
            get
            {
				var __value = _IsDisplayable;
				if(OnIsDisplayable_Getter != null)
				{
					var e = new PropertyGetterEventArgs<bool>(__value);
					OnIsDisplayable_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_IsDisplayable != value)
                {
					var __oldValue = _IsDisplayable;
					var __newValue = value;
                    if(OnIsDisplayable_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
						OnIsDisplayable_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("IsDisplayable", __oldValue, __newValue);
                    _IsDisplayable = __newValue;
                    NotifyPropertyChanged("IsDisplayable", __oldValue, __newValue);

                    if(OnIsDisplayable_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
						OnIsDisplayable_PostSetter(this, e);
                    }
                }
            }
        }
        private bool _IsDisplayable;
		public event PropertyGetterHandler<Kistl.App.Base.Method, bool> OnIsDisplayable_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.Method, bool> OnIsDisplayable_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.Method, bool> OnIsDisplayable_PostSetter;
        /// <summary>
        /// Methodenaufrufe implementiert in dieser Objekt Klasse
        /// </summary>
    /*
    Relation: FK_Method_has_MethodInvocation
    A: One Method as Method
    B: ZeroOrMore MethodInvocation as MethodInvokations
    Preferred Storage: MergeIntoB
    */
        // object list property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectListProperty
	    // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.MethodInvocation> MethodInvokations
        {
            get
            {
                if (_MethodInvokationsWrapper == null)
                {
                    _MethodInvokationsWrapper = new EntityCollectionWrapper<Kistl.App.Base.MethodInvocation, Kistl.App.Base.MethodInvocation__Implementation__>(
                            this.Context, MethodInvokations__Implementation__);
                }
                return _MethodInvokationsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Method_has_MethodInvocation", "MethodInvokations")]
        public EntityCollection<Kistl.App.Base.MethodInvocation__Implementation__> MethodInvokations__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.MethodInvocation__Implementation__>(
                        "Model.FK_Method_has_MethodInvocation",
                        "MethodInvokations");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.MethodInvocation, Kistl.App.Base.MethodInvocation__Implementation__> _MethodInvokationsWrapper;



        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string MethodName
        {
            get
            {
				var __value = _MethodName;
				if(OnMethodName_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnMethodName_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MethodName != value)
                {
					var __oldValue = _MethodName;
					var __newValue = value;
                    if(OnMethodName_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnMethodName_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("MethodName", __oldValue, __newValue);
                    _MethodName = __newValue;
                    NotifyPropertyChanged("MethodName", __oldValue, __newValue);

                    if(OnMethodName_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnMethodName_PostSetter(this, e);
                    }
                }
            }
        }
        private string _MethodName;
		public event PropertyGetterHandler<Kistl.App.Base.Method, string> OnMethodName_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.Method, string> OnMethodName_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.Method, string> OnMethodName_PostSetter;
        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
    /*
    Relation: FK_Method_has_Module
    A: ZeroOrMore Method as Method
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
        [EdmRelationshipNavigationProperty("Model", "FK_Method_has_Module", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_Method_has_Module",
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
                        "Model.FK_Method_has_Module",
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
        
        
		public event PropertyGetterHandler<Kistl.App.Base.Method, Kistl.App.Base.Module> OnModule_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.Method, Kistl.App.Base.Module> OnModule_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.Method, Kistl.App.Base.Module> OnModule_PostSetter;
        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_DataType_has_Method
    A: One DataType as ObjectClass
    B: ZeroOrMore Method as Methods
    Preferred Storage: MergeIntoB
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                return ObjectClass__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                ObjectClass__Implementation__ = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        private int? _fk_ObjectClass;
        private Guid? _fk_guid_ObjectClass = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_has_Method", "ObjectClass")]
        public Kistl.App.Base.DataType__Implementation__ ObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_has_Method",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnObjectClass_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.DataType>(__value);
					OnObjectClass_Getter(this, e);
					__value = (Kistl.App.Base.DataType__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_has_Method",
                        "ObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.DataType __oldValue = (Kistl.App.Base.DataType)r.Value;
                Kistl.App.Base.DataType __newValue = (Kistl.App.Base.DataType)value;

                if(OnObjectClass_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.DataType>(__oldValue, __newValue);
					OnObjectClass_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.DataType__Implementation__)__newValue;
                if(OnObjectClass_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.DataType>(__oldValue, __newValue);
					OnObjectClass_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Base.Method, Kistl.App.Base.DataType> OnObjectClass_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.Method, Kistl.App.Base.DataType> OnObjectClass_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.Method, Kistl.App.Base.DataType> OnObjectClass_PostSetter;
        /// <summary>
        /// Parameter der Methode
        /// </summary>
    /*
    Relation: FK_Method_has_BaseParameter
    A: One Method as Method
    B: ZeroOrMore BaseParameter as Parameter
    Preferred Storage: MergeIntoB
    */
        // object list property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectListProperty
	    // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Base.BaseParameter> Parameter
        {
            get
            {
                if (_ParameterWrapper == null)
                {
                    _ParameterWrapper = new EntityListWrapper<Kistl.App.Base.BaseParameter, Kistl.App.Base.BaseParameter__Implementation__>(
                            this.Context, Parameter__Implementation__, "Method");
                }
                return _ParameterWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Method_has_BaseParameter", "Parameter")]
        public EntityCollection<Kistl.App.Base.BaseParameter__Implementation__> Parameter__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.BaseParameter__Implementation__>(
                        "Model.FK_Method_has_BaseParameter",
                        "Parameter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityListWrapper<Kistl.App.Base.BaseParameter, Kistl.App.Base.BaseParameter__Implementation__> _ParameterWrapper;



        /// <summary>
        /// 
        /// </summary>
		[EventBasedMethod("OnCreateMethodInvocation_Method")]
		public virtual Kistl.App.Base.MethodInvocation CreateMethodInvocation() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.MethodInvocation>();
            if (OnCreateMethodInvocation_Method != null)
            {
                OnCreateMethodInvocation_Method(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Method.CreateMethodInvocation");
            }
            return e.Result;
        }
		public delegate void CreateMethodInvocation_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.MethodInvocation> ret);
		public event CreateMethodInvocation_Handler<Method> OnCreateMethodInvocation_Method;



        /// <summary>
        /// Returns the Return Parameter Meta Object of this Method Meta Object.
        /// </summary>
		[EventBasedMethod("OnGetReturnParameter_Method")]
		public virtual Kistl.App.Base.BaseParameter GetReturnParameter() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.BaseParameter>();
            if (OnGetReturnParameter_Method != null)
            {
                OnGetReturnParameter_Method(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Method.GetReturnParameter");
            }
            return e.Result;
        }
		public delegate void GetReturnParameter_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.BaseParameter> ret);
		public event GetReturnParameter_Handler<Method> OnGetReturnParameter_Method;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Method));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Method)obj;
			var otherImpl = (Method__Implementation__)obj;
			var me = (Method)this;

			me.Description = other.Description;
			me.ExportGuid = other.ExportGuid;
			me.IsDisplayable = other.IsDisplayable;
			me.MethodName = other.MethodName;
			this._fk_Module = otherImpl._fk_Module;
			this._fk_ObjectClass = otherImpl._fk_ObjectClass;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Method")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Method != null)
            {
                OnToString_Method(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Method> OnToString_Method;

        [EventBasedMethod("OnPreSave_Method")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Method != null) OnPreSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPreSave_Method;

        [EventBasedMethod("OnPostSave_Method")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Method != null) OnPostSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPostSave_Method;

        [EventBasedMethod("OnCreated_Method")]
        public override void NotifyCreated()
        {
            try
            {
				Kistl.App.Base.Property p = null;
				p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("842eb3fc-3c8f-47d6-a59f-225c75ec2439"));
				if(p != null && p.DefaultValue != null) { this.ExportGuid = (Guid)p.DefaultValue.GetDefaultValue(); } else { System.Diagnostics.Trace.TraceWarning("Unable to get default value for property 'Method.ExportGuid'"); }
            }
            catch (TypeLoadException)
            {
                // TODO: Find a better way to ignore bootstrap errors.
                // During bootstrapping no MethodInvocation is registred
            }
            catch (NotImplementedException)
            {
                // TODO: Find a better way to ignore bootstrap errors.
                // During bootstrapping no MethodInvocation is registred
            }
            base.NotifyCreated();
            if (OnCreated_Method != null) OnCreated_Method(this);
        }
        public event ObjectEventHandler<Method> OnCreated_Method;

        [EventBasedMethod("OnDeleting_Method")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Method != null) OnDeleting_Method(this);
        }
        public event ObjectEventHandler<Method> OnDeleting_Method;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Description":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("cbf27789-e98f-4d9f-88e9-f3ff89e8c952")).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("842eb3fc-3c8f-47d6-a59f-225c75ec2439")).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsDisplayable":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("5ac29d6a-9dec-4d88-8f66-59ee7a139f4d")).Constraints
						.Where(c => !c.IsValid(this, this.IsDisplayable))
						.Select(c => c.GetErrorText(this, this.IsDisplayable))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MethodInvokations":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("dc2bd380-6e63-4a44-bcc3-192780f80606")).Constraints
						.Where(c => !c.IsValid(this, this.MethodInvokations))
						.Select(c => c.GetErrorText(this, this.MethodInvokations))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MethodName":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("88de8421-488e-452e-8289-33074054b22f")).Constraints
						.Where(c => !c.IsValid(this, this.MethodName))
						.Select(c => c.GetErrorText(this, this.MethodName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Module":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("51640f6f-b2ae-4f26-915e-fda5a2c060a6")).Constraints
						.Where(c => !c.IsValid(this, this.Module))
						.Select(c => c.GetErrorText(this, this.Module))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ObjectClass":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("9afc74a4-4eeb-4c39-879c-eacc8f369fa7")).Constraints
						.Where(c => !c.IsValid(this, this.ObjectClass))
						.Select(c => c.GetErrorText(this, this.ObjectClass))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Parameter":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("8dace0a9-6db1-458d-b054-ace4a3d906c2")).Constraints
						.Where(c => !c.IsValid(this, this.Parameter))
						.Select(c => c.GetErrorText(this, this.Parameter))
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

			if (_fk_guid_Module.HasValue)
				Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Module>(_fk_guid_Module.Value);
			else if (_fk_Module.HasValue)
				Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)Context.Find<Kistl.App.Base.Module>(_fk_Module.Value);
			else
				Module__Implementation__ = null;

			if (_fk_guid_ObjectClass.HasValue)
				ObjectClass__Implementation__ = (Kistl.App.Base.DataType__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.DataType>(_fk_guid_ObjectClass.Value);
			else if (_fk_ObjectClass.HasValue)
				ObjectClass__Implementation__ = (Kistl.App.Base.DataType__Implementation__)Context.Find<Kistl.App.Base.DataType>(_fk_ObjectClass.Value);
			else
				ObjectClass__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(this._IsDisplayable, binStream);
            BinarySerializer.ToStream(this._MethodName, binStream);
            BinarySerializer.ToStream(Module != null ? Module.ID : (int?)null, binStream);
            BinarySerializer.ToStream(ObjectClass != null ? ObjectClass.ID : (int?)null, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._ExportGuid, binStream);
            BinarySerializer.FromStream(out this._IsDisplayable, binStream);
            BinarySerializer.FromStream(out this._MethodName, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectClass, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.ToStream(this._IsDisplayable, xml, "IsDisplayable", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._MethodName, xml, "MethodName", "Kistl.App.Base");
            XmlStreamer.ToStream(Module != null ? Module.ID : (int?)null, xml, "Module", "Kistl.App.Base");
            XmlStreamer.ToStream(ObjectClass != null ? ObjectClass.ID : (int?)null, xml, "ObjectClass", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsDisplayable, xml, "IsDisplayable", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._MethodName, xml, "MethodName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_ObjectClass, xml, "ObjectClass", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this._IsDisplayable, xml, "IsDisplayable", "Kistl.App.GUI");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._MethodName, xml, "MethodName", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Module != null ? Module.ExportGuid : (Guid?)null, xml, "Module", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(ObjectClass != null ? ObjectClass.ExportGuid : (Guid?)null, xml, "ObjectClass", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsDisplayable, xml, "IsDisplayable", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._MethodName, xml, "MethodName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_Module, xml, "Module", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_ObjectClass, xml, "ObjectClass", "Kistl.App.Base");
        }

#endregion

    }


}