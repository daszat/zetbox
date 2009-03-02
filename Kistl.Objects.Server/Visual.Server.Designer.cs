
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
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
    /*
    Relation: FK_Visual_Visual_Visual_55
    A: 3 Visual as Visual
    B: 3 Visual as Children
    Preferred Storage: 4
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
                    _ChildrenWrapper = new EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__>(
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
        private EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_Children55CollectionEntry__Implementation__> _ChildrenWrapper;
        

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
    /*
    Relation: FK_Visual_Visual_Visual_60
    A: 3 Visual as Visual
    B: 3 Visual as ContextMenu
    Preferred Storage: 4
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
                    _ContextMenuWrapper = new EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__>(
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
        private EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_ContextMenu60CollectionEntry__Implementation__> _ContextMenuWrapper;
        

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
                    NotifyPropertyChanging("ControlType");
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType");
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
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }
        private string _Description;

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
    /*
    Relation: FK_Visual_Method_Visual_57
    A: 3 Visual as Visual
    B: 1 Method as Method
    Preferred Storage: 1
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
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? Method_pos
        {
            get
            {
                return _Method_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method_pos != value)
                {
                    NotifyPropertyChanging("Method_pos");
                    _Method_pos = value;
                    NotifyPropertyChanged("Method_pos");
                }
            }
        }
        private int? _Method_pos;
        

        /// <summary>
        /// The Property to display
        /// </summary>
    /*
    Relation: FK_Visual_BaseProperty_Visual_56
    A: 3 Visual as Visual
    B: 1 BaseProperty as Property
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.BaseProperty Property
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
                Property__Implementation__ = (Kistl.App.Base.BaseProperty__Implementation__)value;
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
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_BaseProperty_Visual_56", "Property")]
        public Kistl.App.Base.BaseProperty__Implementation__ Property__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.BaseProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.BaseProperty__Implementation__>(
                        "Model.FK_Visual_BaseProperty_Visual_56",
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
                EntityReference<Kistl.App.Base.BaseProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.BaseProperty__Implementation__>(
                        "Model.FK_Visual_BaseProperty_Visual_56",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.BaseProperty__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? Property_pos
        {
            get
            {
                return _Property_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Property_pos != value)
                {
                    NotifyPropertyChanging("Property_pos");
                    _Property_pos = value;
                    NotifyPropertyChanged("Property_pos");
                }
            }
        }
        private int? _Property_pos;
        

		public override Type GetInterfaceType()
		{
			return typeof(Visual);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
			// collections have to be loaded separately for now
            // BinarySerializer.ToStreamCollectionEntries(this.Children__Implementation__, binStream);
			// collections have to be loaded separately for now
            // BinarySerializer.ToStreamCollectionEntries(this.ContextMenu__Implementation__, binStream);
            BinarySerializer.ToStream((int)((Visual)this).ControlType, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_Method, binStream);
            BinarySerializer.ToStream(this._Method_pos, binStream);
            BinarySerializer.ToStream(this.fk_Property, binStream);
            BinarySerializer.ToStream(this._Property_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
			// collections have to be loaded separately for now
            // BinarySerializer.FromStreamCollectionEntries(this.Children__Implementation__, binStream);
			// collections have to be loaded separately for now
            // BinarySerializer.FromStreamCollectionEntries(this.ContextMenu__Implementation__, binStream);
            BinarySerializer.FromStreamConverter(v => ((Visual)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_Method;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Method = tmp;
            }
            BinarySerializer.FromStream(out this._Method_pos, binStream);
            {
                var tmp = this.fk_Property;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Property = tmp;
            }
            BinarySerializer.FromStream(out this._Property_pos, binStream);
        }

#endregion

    }


}