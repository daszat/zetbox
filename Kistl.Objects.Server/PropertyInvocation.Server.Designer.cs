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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="PropertyInvocation")]
    [System.Diagnostics.DebuggerDisplay("PropertyInvocation")]
    public class PropertyInvocation__Implementation__ : BaseServerDataObject_EntityFramework, PropertyInvocation, Kistl.API.IExportableInternal
    {
    
		public PropertyInvocation__Implementation__()
		{
            {
            }
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
		public event PropertyGetterHandler<Kistl.App.Base.PropertyInvocation, Guid> OnExportGuid_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.PropertyInvocation, Guid> OnExportGuid_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.PropertyInvocation, Guid> OnExportGuid_PostSetter;
        /// <summary>
        /// The Type implementing this invocation
        /// </summary>
    /*
    Relation: FK_PropertyInvocation_has_TypeRef
    A: ZeroOrMore PropertyInvocation as PropertyInvocation
    B: One TypeRef as Implementor
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef Implementor
        {
            get
            {
                return Implementor__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                Implementor__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        private int? _fk_Implementor;
        private Guid? _fk_guid_Implementor = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PropertyInvocation_has_TypeRef", "Implementor")]
        public Kistl.App.Base.TypeRef__Implementation__ Implementor__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_PropertyInvocation_has_TypeRef",
                        "Implementor");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnImplementor_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.TypeRef>(__value);
					OnImplementor_Getter(this, e);
					__value = (Kistl.App.Base.TypeRef__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_PropertyInvocation_has_TypeRef",
                        "Implementor");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.TypeRef __oldValue = (Kistl.App.Base.TypeRef)r.Value;
                Kistl.App.Base.TypeRef __newValue = (Kistl.App.Base.TypeRef)value;

                if(OnImplementor_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnImplementor_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)__newValue;
                if(OnImplementor_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnImplementor_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.TypeRef> OnImplementor_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.TypeRef> OnImplementor_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.TypeRef> OnImplementor_PostSetter;
        /// <summary>
        /// 
        /// </summary>
        // enumeration property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.EnumerationPropertyTemplate
        // implement the user-visible interface
        public Kistl.App.Base.PropertyInvocationType InvocationType
        {
            get
            {
				var __value = _InvocationType;
				if(OnInvocationType_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.PropertyInvocationType>(__value);
					OnInvocationType_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (_InvocationType != value)
                {
					var __oldValue = _InvocationType;
					var __newValue = value;
                    if(OnInvocationType_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<Kistl.App.Base.PropertyInvocationType>(__oldValue, __newValue);
						OnInvocationType_PreSetter(this, e);
						__newValue = e.Result;
                    }
					
                    NotifyPropertyChanging("InvocationType", "InvocationType__Implementation__", __oldValue, __newValue);
                    _InvocationType = value;
                    NotifyPropertyChanged("InvocationType", "InvocationType__Implementation__", __oldValue, __newValue);
                    if(OnInvocationType_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<Kistl.App.Base.PropertyInvocationType>(__oldValue, __newValue);
						OnInvocationType_PostSetter(this, e);
                    }
                    
                }
            }
        }
        
        /// <summary>backing store for InvocationType</summary>
        private Kistl.App.Base.PropertyInvocationType _InvocationType;
        
        /// <summary>EF sees only this property, for InvocationType</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int InvocationType__Implementation__
        {
            get
            {
                return (int)this.InvocationType;
            }
            set
            {
                this.InvocationType = (Kistl.App.Base.PropertyInvocationType)value;
            }
        }
        
		public event PropertyGetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.PropertyInvocationType> OnInvocationType_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.PropertyInvocationType> OnInvocationType_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.PropertyInvocationType> OnInvocationType_PostSetter;
        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Property_has_PropertyInvocation
    A: One Property as InvokeOnProperty
    B: ZeroOrMore PropertyInvocation as Invocations
    Preferred Storage: MergeIntoB
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property InvokeOnProperty
        {
            get
            {
                return InvokeOnProperty__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                InvokeOnProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        private int? _fk_InvokeOnProperty;
        private Guid? _fk_guid_InvokeOnProperty = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Property_has_PropertyInvocation", "InvokeOnProperty")]
        public Kistl.App.Base.Property__Implementation__ InvokeOnProperty__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Property_has_PropertyInvocation",
                        "InvokeOnProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnInvokeOnProperty_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Property>(__value);
					OnInvokeOnProperty_Getter(this, e);
					__value = (Kistl.App.Base.Property__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Property_has_PropertyInvocation",
                        "InvokeOnProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Property __oldValue = (Kistl.App.Base.Property)r.Value;
                Kistl.App.Base.Property __newValue = (Kistl.App.Base.Property)value;

                if(OnInvokeOnProperty_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnInvokeOnProperty_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Property__Implementation__)__newValue;
                if(OnInvokeOnProperty_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnInvokeOnProperty_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.Property> OnInvokeOnProperty_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.Property> OnInvokeOnProperty_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.PropertyInvocation, Kistl.App.Base.Property> OnInvokeOnProperty_PostSetter;
        /// <summary>
        /// Name des implementierenden Members
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string MemberName
        {
            get
            {
				var __value = _MemberName;
				if(OnMemberName_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnMemberName_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MemberName != value)
                {
					var __oldValue = _MemberName;
					var __newValue = value;
                    if(OnMemberName_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnMemberName_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("MemberName", __oldValue, __newValue);
                    _MemberName = __newValue;
                    NotifyPropertyChanged("MemberName", __oldValue, __newValue);

                    if(OnMemberName_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnMemberName_PostSetter(this, e);
                    }
                }
            }
        }
        private string _MemberName;
		public event PropertyGetterHandler<Kistl.App.Base.PropertyInvocation, string> OnMemberName_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.PropertyInvocation, string> OnMemberName_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.PropertyInvocation, string> OnMemberName_PostSetter;
        /// <summary>
        /// 
        /// </summary>

		public virtual string GetCodeTemplate() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetCodeTemplate_PropertyInvocation != null)
            {
                OnGetCodeTemplate_PropertyInvocation(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on PropertyInvocation.GetCodeTemplate");
            }
            return e.Result;
        }
		public delegate void GetCodeTemplate_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetCodeTemplate_Handler<PropertyInvocation> OnGetCodeTemplate_PropertyInvocation;



        /// <summary>
        /// 
        /// </summary>

		public virtual string GetMemberName() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetMemberName_PropertyInvocation != null)
            {
                OnGetMemberName_PropertyInvocation(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on PropertyInvocation.GetMemberName");
            }
            return e.Result;
        }
		public delegate void GetMemberName_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetMemberName_Handler<PropertyInvocation> OnGetMemberName_PropertyInvocation;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PropertyInvocation));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PropertyInvocation)obj;
			var otherImpl = (PropertyInvocation__Implementation__)obj;
			var me = (PropertyInvocation)this;

			me.ExportGuid = other.ExportGuid;
			me.InvocationType = other.InvocationType;
			me.MemberName = other.MemberName;
			this._fk_Implementor = otherImpl._fk_Implementor;
			this._fk_InvokeOnProperty = otherImpl._fk_InvokeOnProperty;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PropertyInvocation != null)
            {
                OnToString_PropertyInvocation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PropertyInvocation> OnToString_PropertyInvocation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PropertyInvocation != null) OnPreSave_PropertyInvocation(this);
        }
        public event ObjectEventHandler<PropertyInvocation> OnPreSave_PropertyInvocation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PropertyInvocation != null) OnPostSave_PropertyInvocation(this);
        }
        public event ObjectEventHandler<PropertyInvocation> OnPostSave_PropertyInvocation;

        public override void NotifyCreated()
        {
            try
            {
				this.ExportGuid = (Guid)FrozenContext.Single.Find<Kistl.App.Base.Property>(128).DefaultValue.GetDefaultValue();
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
            if (OnCreated_PropertyInvocation != null) OnCreated_PropertyInvocation(this);
        }
        public event ObjectEventHandler<PropertyInvocation> OnCreated_PropertyInvocation;

        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_PropertyInvocation != null) OnDeleting_PropertyInvocation(this);
        }
        public event ObjectEventHandler<PropertyInvocation> OnDeleting_PropertyInvocation;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(128).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Implementor":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(193).Constraints
						.Where(c => !c.IsValid(this, this.Implementor))
						.Select(c => c.GetErrorText(this, this.Implementor))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "InvocationType":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(196).Constraints
						.Where(c => !c.IsValid(this, this.InvocationType))
						.Select(c => c.GetErrorText(this, this.InvocationType))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "InvokeOnProperty":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(20).Constraints
						.Where(c => !c.IsValid(this, this.InvokeOnProperty))
						.Select(c => c.GetErrorText(this, this.InvokeOnProperty))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "MemberName":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(194).Constraints
						.Where(c => !c.IsValid(this, this.MemberName))
						.Select(c => c.GetErrorText(this, this.MemberName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// fix direct object references

			if (_fk_guid_Implementor.HasValue)
				Implementor__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.TypeRef>(_fk_guid_Implementor.Value);
			else if (_fk_Implementor.HasValue)
				Implementor__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_Implementor.Value);
			else
				Implementor__Implementation__ = null;

			if (_fk_guid_InvokeOnProperty.HasValue)
				InvokeOnProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Property>(_fk_guid_InvokeOnProperty.Value);
			else if (_fk_InvokeOnProperty.HasValue)
				InvokeOnProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.Find<Kistl.App.Base.Property>(_fk_InvokeOnProperty.Value);
			else
				InvokeOnProperty__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(Implementor != null ? Implementor.ID : (int?)null, binStream);
            BinarySerializer.ToStream((int)((PropertyInvocation)this).InvocationType, binStream);
            BinarySerializer.ToStream(InvokeOnProperty != null ? InvokeOnProperty.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._MemberName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._ExportGuid, binStream);
            BinarySerializer.FromStream(out this._fk_Implementor, binStream);
            BinarySerializer.FromStreamConverter(v => ((PropertyInvocation)this).InvocationType = (Kistl.App.Base.PropertyInvocationType)v, binStream);
            BinarySerializer.FromStream(out this._fk_InvokeOnProperty, binStream);
            BinarySerializer.FromStream(out this._MemberName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.ToStream(Implementor != null ? Implementor.ID : (int?)null, xml, "Implementor", "Kistl.App.Base");
            XmlStreamer.ToStream((int)this.InvocationType, xml, "InvocationType", "Kistl.App.Base");
            XmlStreamer.ToStream(InvokeOnProperty != null ? InvokeOnProperty.ID : (int?)null, xml, "InvokeOnProperty", "Kistl.App.Base");
            XmlStreamer.ToStream(this._MemberName, xml, "MemberName", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Implementor, xml, "Implementor", "Kistl.App.Base");
            XmlStreamer.FromStreamConverter(v => ((PropertyInvocation)this).InvocationType = (Kistl.App.Base.PropertyInvocationType)v, xml, "InvocationType", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_InvokeOnProperty, xml, "InvokeOnProperty", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._MemberName, xml, "MemberName", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Implementor != null ? Implementor.ExportGuid : (Guid?)null, xml, "Implementor", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream((int)this.InvocationType, xml, "InvocationType", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(InvokeOnProperty != null ? InvokeOnProperty.ExportGuid : (Guid?)null, xml, "InvokeOnProperty", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._MemberName, xml, "MemberName", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_Implementor, xml, "Implementor", "Kistl.App.Base");
            XmlStreamer.FromStreamConverter(v => ((PropertyInvocation)this).InvocationType = (Kistl.App.Base.PropertyInvocationType)v, xml, "InvocationType", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_InvokeOnProperty, xml, "InvokeOnProperty", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._MemberName, xml, "MemberName", "Kistl.App.Base");
        }

#endregion

    }


}