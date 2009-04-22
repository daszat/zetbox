
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
    public class PresentableModelDescriptor__Implementation__ : BaseServerDataObject_EntityFramework, PresentableModelDescriptor
    {
    
		public PresentableModelDescriptor__Implementation__()
		{
            {
            }
        }

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
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// The default visual type used for this PresentableModel
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.VisualType PresentableModelDescriptor.DefaultVisualType
        {
            get
            {
                return _DefaultVisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultVisualType != value)
                {
					var __oldValue = _DefaultVisualType;
                    NotifyPropertyChanging("DefaultVisualType", __oldValue, value);
                    _DefaultVisualType = value;
                    NotifyPropertyChanged("DefaultVisualType", __oldValue, value);
                }
            }
        }
        
        /// <summary>backing store for DefaultVisualType</summary>
        private Kistl.App.GUI.VisualType _DefaultVisualType;
        
        /// <summary>EF sees only this property, for DefaultVisualType</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int DefaultVisualType
        {
            get
            {
                return (int)((PresentableModelDescriptor)this).DefaultVisualType;
            }
            set
            {
                ((PresentableModelDescriptor)this).DefaultVisualType = (Kistl.App.GUI.VisualType)value;
            }
        }
        

        /// <summary>
        /// describe this PresentableModel
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// The described CLR class' reference
        /// </summary>
    /*
    Relation: FK_PresentableModelDescriptor_TypeRef_Descriptor_77
    A: ZeroOrMore PresentableModelDescriptor as Descriptor
    B: One TypeRef as PresentableModelRef
    Preferred Storage: Left
    */
        // object reference property
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
                if (IsReadonly) throw new ReadOnlyObjectException();
                PresentableModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_PresentableModelRef
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && PresentableModelRef != null)
                {
                    _fk_PresentableModelRef = PresentableModelRef.ID;
                }
                return _fk_PresentableModelRef;
            }
            set
            {
                _fk_PresentableModelRef = value;
            }
        }
        private int? _fk_PresentableModelRef;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_PresentableModelDescriptor_TypeRef_Descriptor_77", "PresentableModelRef")]
        public Kistl.App.Base.TypeRef__Implementation__ PresentableModelRef__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_PresentableModelDescriptor_TypeRef_Descriptor_77",
                        "PresentableModelRef");
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
                        "Model.FK_PresentableModelDescriptor_TypeRef_Descriptor_77",
                        "PresentableModelRef");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        

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
			this.fk_PresentableModelRef = otherImpl.fk_PresentableModelRef;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
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

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresentableModelDescriptor != null) OnPreSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPreSave_PresentableModelDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresentableModelDescriptor != null) OnPostSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPostSave_PresentableModelDescriptor;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "DefaultVisualType":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(233).Constraints
						.Where(c => !c.IsValid(this, this.DefaultVisualType))
						.Select(c => c.GetErrorText(this, this.DefaultVisualType))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Description":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(232).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresentableModelRef":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(231).Constraints
						.Where(c => !c.IsValid(this, this.PresentableModelRef))
						.Select(c => c.GetErrorText(this, this.PresentableModelRef))
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
			if (_fk_PresentableModelRef.HasValue)
				PresentableModelRef__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)Context.Find<Kistl.App.Base.TypeRef>(_fk_PresentableModelRef.Value);
			else
				PresentableModelRef__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream((int)((PresentableModelDescriptor)this).DefaultVisualType, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_PresentableModelRef, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => ((PresentableModelDescriptor)this).DefaultVisualType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_PresentableModelRef;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_PresentableModelRef = tmp;
            }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            // TODO: Add XML Serializer here
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(this.fk_PresentableModelRef, xml, "PresentableModelRef", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            {
                var tmp = this.fk_PresentableModelRef;
                XmlStreamer.FromStream(ref tmp, xml, "PresentableModelRef", "http://dasz.at/Kistl");
                this.fk_PresentableModelRef = tmp;
            }
        }

#endregion

    }


}