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
    [EdmEntityType(NamespaceName="Model", Name="DefaultPropertyValue")]
    [System.Diagnostics.DebuggerDisplay("DefaultPropertyValue")]
    public class DefaultPropertyValue__Implementation__ : BaseServerDataObject_EntityFramework, DefaultPropertyValue, Kistl.API.IExportableInternal
    {
    
		public DefaultPropertyValue__Implementation__()
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
		public event PropertyGetterHandler<Kistl.App.Base.DefaultPropertyValue, Guid> OnExportGuid_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.DefaultPropertyValue, Guid> OnExportGuid_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.DefaultPropertyValue, Guid> OnExportGuid_PostSetter;
        /// <summary>
        /// Property where the default value is set
        /// </summary>
    /*
    Relation: FK_Property_has_DefaultPropertyValue
    A: One Property as Property
    B: ZeroOrOne DefaultPropertyValue as DefaultValue
    Preferred Storage: MergeIntoB
    */
        // object reference property
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.ObjectReferencePropertyTemplate
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property Property
        {
            get
            {
                return Property__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                Property__Implementation__ = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        private int? _fk_Property;
        private Guid? _fk_guid_Property = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Property_has_DefaultPropertyValue", "Property")]
        public Kistl.App.Base.Property__Implementation__ Property__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Property_has_DefaultPropertyValue",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                var __value = r.Value;
				if(OnProperty_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Property>(__value);
					OnProperty_Getter(this, e);
					__value = (Kistl.App.Base.Property__Implementation__)e.Result;
				}
                return __value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Property_has_DefaultPropertyValue",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                Kistl.App.Base.Property __oldValue = (Kistl.App.Base.Property)r.Value;
                Kistl.App.Base.Property __newValue = (Kistl.App.Base.Property)value;

                if(OnProperty_PreSetter != null)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnProperty_PreSetter(this, e);
					__newValue = e.Result;
                }
                r.Value = (Kistl.App.Base.Property__Implementation__)__newValue;
                if(OnProperty_PostSetter != null)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnProperty_PostSetter(this, e);
                }
                                
            }
        }
        
        
		public event PropertyGetterHandler<Kistl.App.Base.DefaultPropertyValue, Kistl.App.Base.Property> OnProperty_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Base.DefaultPropertyValue, Kistl.App.Base.Property> OnProperty_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Base.DefaultPropertyValue, Kistl.App.Base.Property> OnProperty_PostSetter;
        /// <summary>
        /// GetDefaultValue
        /// </summary>

		public virtual System.Object GetDefaultValue() 
        {
            var e = new MethodReturnEventArgs<System.Object>();
            if (OnGetDefaultValue_DefaultPropertyValue != null)
            {
                OnGetDefaultValue_DefaultPropertyValue(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on DefaultPropertyValue.GetDefaultValue");
            }
            return e.Result;
        }
		public delegate void GetDefaultValue_Handler<T>(T obj, MethodReturnEventArgs<System.Object> ret);
		public event GetDefaultValue_Handler<DefaultPropertyValue> OnGetDefaultValue_DefaultPropertyValue;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(DefaultPropertyValue));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (DefaultPropertyValue)obj;
			var otherImpl = (DefaultPropertyValue__Implementation__)obj;
			var me = (DefaultPropertyValue)this;

			me.ExportGuid = other.ExportGuid;
			this._fk_Property = otherImpl._fk_Property;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DefaultPropertyValue != null)
            {
                OnToString_DefaultPropertyValue(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DefaultPropertyValue> OnToString_DefaultPropertyValue;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DefaultPropertyValue != null) OnPreSave_DefaultPropertyValue(this);
        }
        public event ObjectEventHandler<DefaultPropertyValue> OnPreSave_DefaultPropertyValue;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DefaultPropertyValue != null) OnPostSave_DefaultPropertyValue(this);
        }
        public event ObjectEventHandler<DefaultPropertyValue> OnPostSave_DefaultPropertyValue;

        public override void NotifyCreated()
        {
            try
            {
				this.ExportGuid = (Guid)FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("e672de1a-e0f4-4613-9d1f-121ba543c2ec")).DefaultValue.GetDefaultValue();
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
            if (OnCreated_DefaultPropertyValue != null) OnCreated_DefaultPropertyValue(this);
        }
        public event ObjectEventHandler<DefaultPropertyValue> OnCreated_DefaultPropertyValue;

        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_DefaultPropertyValue != null) OnDeleting_DefaultPropertyValue(this);
        }
        public event ObjectEventHandler<DefaultPropertyValue> OnDeleting_DefaultPropertyValue;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("e672de1a-e0f4-4613-9d1f-121ba543c2ec")).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Property":
				{
					var errors = FrozenContext.Single.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("a2451b2f-2430-4de4-81a6-3d5ac9f0138f")).Constraints
						.Where(c => !c.IsValid(this, this.Property))
						.Select(c => c.GetErrorText(this, this.Property))
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

			if (_fk_guid_Property.HasValue)
				Property__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Property>(_fk_guid_Property.Value);
			else if (_fk_Property.HasValue)
				Property__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.Find<Kistl.App.Base.Property>(_fk_Property.Value);
			else
				Property__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
            BinarySerializer.ToStream(this._ExportGuid, binStream);
            BinarySerializer.ToStream(Property != null ? Property.ID : (int?)null, binStream);
			if (auxObjects != null) {
				auxObjects.Add(Property);
			}
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._ExportGuid, binStream);
            BinarySerializer.FromStream(out this._fk_Property, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.ToStream(Property != null ? Property.ID : (int?)null, xml, "Property", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Property, xml, "Property", "Kistl.App.Base");
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
			xml.WriteAttributeString("ExportGuid", this.ExportGuid.ToString());
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Property != null ? Property.ExportGuid : (Guid?)null, xml, "Property", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_Property, xml, "Property", "Kistl.App.Base");
        }

#endregion

    }


}