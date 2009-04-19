
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
    [EdmEntityType(NamespaceName="Model", Name="Visual")]
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class Visual__Implementation__ : BaseServerDataObject_EntityFramework, Visual
    {
    
		public Visual__Implementation__()
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
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
    /*
    Relation: FK_Visual_Visual_Visual_55
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as Children
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.Visual> Children
        {
            get
            {
                if (_ChildrenWrapper == null)
                {
                    _ChildrenWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__>(
                            this,
                            Children__Implementation__);
                }
                return _ChildrenWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Visual_55", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__> Children__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_55",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__> _ChildrenWrapper;
        

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
    /*
    Relation: FK_Visual_Visual_Visual_60
    A: ZeroOrMore Visual as Visual
    B: ZeroOrMore Visual as ContextMenu
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.Visual> ContextMenu
        {
            get
            {
                if (_ContextMenuWrapper == null)
                {
                    _ContextMenuWrapper = new EntityRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__>(
                            this,
                            ContextMenu__Implementation__);
                }
                return _ContextMenuWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Visual_60", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__> ContextMenu__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_60",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__> _ContextMenuWrapper;
        

        /// <summary>
        /// Which visual is represented here
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        Kistl.App.GUI.VisualType Visual.ControlType
        {
            get
            {
                return _ControlType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ControlType != value)
                {
					var __oldValue = _ControlType;
                    NotifyPropertyChanging("ControlType", __oldValue, value);
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType", __oldValue, value);
                }
            }
        }
        
        /// <summary>backing store for ControlType</summary>
        private Kistl.App.GUI.VisualType _ControlType;
        
        /// <summary>EF sees only this property, for ControlType</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int ControlType
        {
            get
            {
                return (int)((Visual)this).ControlType;
            }
            set
            {
                ((Visual)this).ControlType = (Kistl.App.GUI.VisualType)value;
            }
        }
        

        /// <summary>
        /// A short description of the utility of this visual
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
        /// The Method whose return value shoud be displayed
        /// </summary>
    /*
    Relation: FK_Visual_Method_Visual_57
    A: ZeroOrMore Visual as Visual
    B: ZeroOrOne Method as Method
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
                return Method__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Method__Implementation__ = (Kistl.App.Base.Method__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Method != null)
                {
                    _fk_Method = Method.ID;
                }
                return _fk_Method;
            }
            set
            {
                _fk_Method = value;
            }
        }
        private int? _fk_Method;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Method_Visual_57", "Method")]
        public Kistl.App.Base.Method__Implementation__ Method__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Visual_Method_Visual_57",
                        "Method");
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
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Visual_Method_Visual_57",
                        "Method");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Method__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The Property to display
        /// </summary>
    /*
    Relation: FK_Visual_Property_Visual_56
    A: ZeroOrMore Visual as Visual
    B: ZeroOrOne Property as Property
    Preferred Storage: Left
    */
        // object reference property
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
                if (IsReadonly) throw new ReadOnlyObjectException();
                Property__Implementation__ = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Property
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Property != null)
                {
                    _fk_Property = Property.ID;
                }
                return _fk_Property;
            }
            set
            {
                _fk_Property = value;
            }
        }
        private int? _fk_Property;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Property_Visual_56", "Property")]
        public Kistl.App.Base.Property__Implementation__ Property__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Visual_Property_Visual_56",
                        "Property");
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
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Visual_Property_Visual_56",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Visual));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Visual)obj;
			var otherImpl = (Visual__Implementation__)obj;
			var me = (Visual)this;

			me.ControlType = other.ControlType;
			me.Description = other.Description;
			this.fk_Method = otherImpl.fk_Method;
			this.fk_Property = otherImpl.fk_Property;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Visual != null)
            {
                OnToString_Visual(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Visual> OnToString_Visual;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Visual != null) OnPreSave_Visual(this);
        }
        public event ObjectEventHandler<Visual> OnPreSave_Visual;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Visual != null) OnPostSave_Visual(this);
        }
        public event ObjectEventHandler<Visual> OnPostSave_Visual;



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Property.HasValue)
				Property__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.Find<Kistl.App.Base.Property>(_fk_Property.Value);
			else
				Property__Implementation__ = null;
			if (_fk_Method.HasValue)
				Method__Implementation__ = (Kistl.App.Base.Method__Implementation__)Context.Find<Kistl.App.Base.Method>(_fk_Method.Value);
			else
				Method__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream((int)((Visual)this).ControlType, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_Method, binStream);
            BinarySerializer.ToStream(this.fk_Property, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => ((Visual)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_Method;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Method = tmp;
            }
            {
                var tmp = this.fk_Property;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Property = tmp;
            }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            // TODO: Add XML Serializer here
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_Method, xml, "Method", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_Property, xml, "Property", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "http://dasz.at/Kistl");
            {
                var tmp = this.fk_Method;
                XmlStreamer.FromStream(ref tmp, xml, "Method", "http://dasz.at/Kistl");
                this.fk_Method = tmp;
            }
            {
                var tmp = this.fk_Property;
                XmlStreamer.FromStream(ref tmp, xml, "Property", "http://dasz.at/Kistl");
                this.fk_Property = tmp;
            }
        }

#endregion

    }


}