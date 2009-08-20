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
    /// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="TypeRef")]
    [System.Diagnostics.DebuggerDisplay("TypeRef")]
    public class TypeRef__Implementation__ : BaseServerDataObject_EntityFramework, Kistl.API.IExportableInternal, TypeRef
    {
    
		public TypeRef__Implementation__()
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
        /// The assembly containing the referenced Type.
        /// </summary>
    /*
    Relation: FK_TypeRef_has_Assembly
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return Assembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        private int? _fk_Assembly;
        private Guid? _fk_guid_Assembly = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_has_Assembly", "Assembly")]
        public Kistl.App.Base.Assembly__Implementation__ Assembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_TypeRef_has_Assembly",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnAssembly_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Assembly>(__value);
					OnAssembly_Getter(this, e);
					__value = (Kistl.App.Base.Assembly__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_TypeRef_has_Assembly",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Assembly __oldValue = (Kistl.App.Base.Assembly)r.Value;
                Kistl.App.Base.Assembly __newValue = (Kistl.App.Base.Assembly)value;

                if(OnAssembly_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Assembly>(__oldValue, __newValue);
					OnAssembly_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)__newValue;
                if(OnAssembly_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Assembly>(__oldValue, __newValue);
					OnAssembly_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Base.TypeRef, Kistl.App.Base.Assembly> OnAssembly_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.TypeRef, Kistl.App.Base.Assembly> OnAssembly_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.TypeRef, Kistl.App.Base.Assembly> OnAssembly_PostSetter;
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
		public event PropertyGetterHandler<Kistl.App.Base.TypeRef, Guid> OnExportGuid_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.TypeRef, Guid> OnExportGuid_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.TypeRef, Guid> OnExportGuid_PostSetter;
        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string FullName
        {
            get
            {
				var __value = _FullName;
				if(OnFullName_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnFullName_Getter(this, e);
					__value = e.Result;
				}
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_FullName != value)
                {
					var __oldValue = _FullName;
					var __newValue = value;
                    if(OnFullName_PreSetter != null)
                    {
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnFullName_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("FullName", __oldValue, __newValue);
                    _FullName = __newValue;
                    NotifyPropertyChanged("FullName", __oldValue, __newValue);

                    if(OnFullName_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnFullName_PostSetter(this, e);
                    }
                }
            }
        }
        private string _FullName;
		public event PropertyGetterHandler<Kistl.App.Base.TypeRef, string> OnFullName_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.TypeRef, string> OnFullName_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.TypeRef, string> OnFullName_PostSetter;
        /// <summary>
        /// list of type arguments
        /// </summary>
    /*
    Relation: FK_TypeRef_hasGenericArguments_TypeRef
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrMore TypeRef as GenericArguments
    Preferred Storage: Separate
    */
        // collection reference property
		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Base.TypeRef> GenericArguments
        {
            get
            {
                if (_GenericArgumentsWrapper == null)
                {
                    _GenericArgumentsWrapper = new EntityRelationBSideListWrapper<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__>(
                            this,
                            GenericArguments__Implementation__);
                }
                return _GenericArgumentsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_hasGenericArguments_TypeRef_TypeRef", "CollectionEntry")]
        public EntityCollection<Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__> GenericArguments__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__>(
                        "Model.FK_TypeRef_hasGenericArguments_TypeRef_TypeRef",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideListWrapper<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry__Implementation__> _GenericArgumentsWrapper;


        /// <summary>
        /// The TypeRef of the BaseClass of the referenced Type
        /// </summary>
    /*
    Relation: FK_TypeRef_has_TypeRef
    A: ZeroOrMore TypeRef as Child
    B: One TypeRef as Parent
    Preferred Storage: MergeIntoA
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef Parent
        {
            get
            {
                return Parent__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                Parent__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        private int? _fk_Parent;
        private Guid? _fk_guid_Parent = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_has_TypeRef", "Parent")]
        public Kistl.App.Base.TypeRef__Implementation__ Parent__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_TypeRef_has_TypeRef",
                        "Parent");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnParent_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.TypeRef>(__value);
					OnParent_Getter(this, e);
					__value = (Kistl.App.Base.TypeRef__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_TypeRef_has_TypeRef",
                        "Parent");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.TypeRef __oldValue = (Kistl.App.Base.TypeRef)r.Value;
                Kistl.App.Base.TypeRef __newValue = (Kistl.App.Base.TypeRef)value;

                if(OnParent_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnParent_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)__newValue;
                if(OnParent_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef>(__oldValue, __newValue);
					OnParent_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef> OnParent_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef> OnParent_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef> OnParent_PostSetter;
        /// <summary>
        /// get the referenced <see cref="System.Type"/>
        /// </summary>
		[EventBasedMethod("OnAsType_TypeRef")]
		public virtual System.Type AsType(System.Boolean throwOnError) 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnAsType_TypeRef != null)
            {
                OnAsType_TypeRef(this, e, throwOnError);
            }
            else
            {
                throw new NotImplementedException("No handler registered on TypeRef.AsType");
            }
            return e.Result;
        }
		public delegate void AsType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret, System.Boolean throwOnError);
		public event AsType_Handler<TypeRef> OnAsType_TypeRef;



        /// <summary>
        /// Update the Parent property to the currently loaded assemblies' state
        /// </summary>
		[EventBasedMethod("OnUpdateParent_TypeRef")]
		public virtual void UpdateParent() 
		{
            // base.UpdateParent();
            if (OnUpdateParent_TypeRef != null)
            {
				OnUpdateParent_TypeRef(this);
			}
			else
			{
                throw new NotImplementedException("No handler registered on TypeRef.UpdateParent");
			}
        }
		public delegate void UpdateParent_Handler<T>(T obj);
		public event UpdateParent_Handler<TypeRef> OnUpdateParent_TypeRef;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TypeRef));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (TypeRef)obj;
			var otherImpl = (TypeRef__Implementation__)obj;
			var me = (TypeRef)this;

			me.ExportGuid = other.ExportGuid;
			me.FullName = other.FullName;
			this._fk_Assembly = otherImpl._fk_Assembly;
			this._fk_Parent = otherImpl._fk_Parent;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_TypeRef")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TypeRef != null)
            {
                OnToString_TypeRef(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TypeRef> OnToString_TypeRef;

        [EventBasedMethod("OnPreSave_TypeRef")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TypeRef != null) OnPreSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPreSave_TypeRef;

        [EventBasedMethod("OnPostSave_TypeRef")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TypeRef != null) OnPostSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPostSave_TypeRef;

        [EventBasedMethod("OnCreated_TypeRef")]
        public override void NotifyCreated()
        {
            try
            {
				Kistl.App.Base.Property p = null;
				p = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("48430be7-e17f-48ad-ac8b-7f9cb5341318"));
				if(p != null && p.DefaultValue != null) { this.ExportGuid = (Guid)p.DefaultValue.GetDefaultValue(); } else { System.Diagnostics.Trace.TraceWarning("Unable to get default value for property 'TypeRef.ExportGuid'"); }
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
            if (OnCreated_TypeRef != null) OnCreated_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnCreated_TypeRef;

        [EventBasedMethod("OnDeleting_TypeRef")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_TypeRef != null) OnDeleting_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnDeleting_TypeRef;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Assembly":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("885bfa97-3d43-48bb-a0aa-1049298714ff")).Constraints
						.Where(c => !c.IsValid(this, this.Assembly))
						.Select(c => c.GetErrorText(this, this.Assembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("48430be7-e17f-48ad-ac8b-7f9cb5341318")).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "FullName":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("e418e513-e623-4a8f-bcbd-8572a29b7c82")).Constraints
						.Where(c => !c.IsValid(this, this.FullName))
						.Select(c => c.GetErrorText(this, this.FullName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "GenericArguments":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("443e3370-b1f4-46e8-9779-1a8d9ba1c8a6")).Constraints
						.Where(c => !c.IsValid(this, this.GenericArguments))
						.Select(c => c.GetErrorText(this, this.GenericArguments))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Parent":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("f7ed21a0-9a41-40eb-b3ab-b35591f2edd7")).Constraints
						.Where(c => !c.IsValid(this, this.Parent))
						.Select(c => c.GetErrorText(this, this.Parent))
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

			if (_fk_guid_Assembly.HasValue)
				Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Assembly>(_fk_guid_Assembly.Value);
			else if (_fk_Assembly.HasValue)
				Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.Find<Kistl.App.Base.Assembly>(_fk_Assembly.Value);
			else
				Assembly__Implementation__ = null;

			if (_fk_guid_Parent.HasValue)
				Parent__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.TypeRef>(_fk_guid_Parent.Value);
			else if (_fk_Parent.HasValue)
				Parent__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_Parent.Value);
			else
				Parent__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(Assembly != null ? Assembly.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(Assembly);
			}
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(this._FullName, binStream);
			{
				foreach(var obj in GenericArguments__Implementation__)
				{
					if (auxObjects != null) {
						auxObjects.Add(obj);
					}
				}
			}
            BinarySerializer.ToStream(Parent != null ? Parent.ID : (int?)null, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._ExportGuid, binStream);
            BinarySerializer.FromStream(out this._FullName, binStream);
            BinarySerializer.FromStream(out this._fk_Parent, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(Assembly != null ? Assembly.ID : (int?)null, xml, "Assembly", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.ToStream(this._FullName, xml, "FullName", "Kistl.App.Base");
            XmlStreamer.ToStream(Parent != null ? Parent.ID : (int?)null, xml, "Parent", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_Assembly, xml, "Assembly", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._FullName, xml, "FullName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Parent, xml, "Parent", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Assembly != null ? Assembly.ExportGuid : (Guid?)null, xml, "Assembly", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._FullName, xml, "FullName", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Parent != null ? Parent.ExportGuid : (Guid?)null, xml, "Parent", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._fk_guid_Assembly, xml, "Assembly", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._FullName, xml, "FullName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_Parent, xml, "Parent", "Kistl.App.Base");
        }

#endregion

    }


}