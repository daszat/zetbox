
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
                    NotifyPropertyChanged("ID");;
                }
            }
        }
        private int _ID;

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
    /*
    NewRelation: FK_Visual_Visual_Visual_35 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=151)
    B: ZeroOrMore Visual as Children (site: B, no Relation, prop ID=151)
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
                    _ChildrenWrapper = new EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_Children35CollectionEntry__Implementation__>(
                            this,
                            Children__Implementation__);
                }
                return _ChildrenWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Visual_35", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.Visual_Children35CollectionEntry__Implementation__> Children__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.Visual_Children35CollectionEntry__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_35",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_Children35CollectionEntry__Implementation__> _ChildrenWrapper;
        

        /// <summary>
        /// The Property to display
        /// </summary>
    /*
    NewRelation: FK_Visual_BaseProperty_Visual_36 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=152)
    B: ZeroOrOne BaseProperty as Property (site: B, no Relation, prop ID=152)
    Preferred Storage: MergeA
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
        public int fk_Property
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
        private int _fk_Property;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_BaseProperty_Visual_36", "Property")]
        public Kistl.App.Base.BaseProperty__Implementation__ Property__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.BaseProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.BaseProperty__Implementation__>(
                        "Model.FK_Visual_BaseProperty_Visual_36",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.BaseProperty__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.BaseProperty__Implementation__>(
                        "Model.FK_Visual_BaseProperty_Visual_36",
                        "Property");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.BaseProperty__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
    /*
    NewRelation: FK_Visual_Method_Visual_37 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=153)
    B: ZeroOrOne Method as Method (site: B, no Relation, prop ID=153)
    Preferred Storage: MergeA
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
        public int fk_Method
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
        private int _fk_Method;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Method_Visual_37", "Method")]
        public Kistl.App.Base.Method__Implementation__ Method__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Visual_Method_Visual_37",
                        "Method");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Visual_Method_Visual_37",
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
        /// The context menu for this Visual
        /// </summary>
    /*
    NewRelation: FK_Visual_Visual_Visual_40 
    A: ZeroOrMore Visual as Visual (site: A, no Relation, prop ID=164)
    B: ZeroOrMore Visual as ContextMenu (site: B, no Relation, prop ID=164)
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
                    _ContextMenuWrapper = new EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_ContextMenu40CollectionEntry__Implementation__>(
                            this,
                            ContextMenu__Implementation__);
                }
                return _ContextMenuWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Visual_Visual_Visual_40", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.Visual_ContextMenu40CollectionEntry__Implementation__> ContextMenu__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.Visual_ContextMenu40CollectionEntry__Implementation__>(
                        "Model.FK_Visual_Visual_Visual_40",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_ContextMenu40CollectionEntry__Implementation__> _ContextMenuWrapper;
        

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
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Which visual is represented here
        /// </summary>
        // enumeration property
        // implement the user-visible interface
        public Kistl.App.GUI.VisualType ControlType
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
        public int ControlType__Implementation__
        {
            get
            {
                return (int)ControlType;
            }
            set
            {
                ControlType = (Kistl.App.GUI.VisualType)value;
            }
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
            BinarySerializer.ToStreamCollectionEntries(this.Children__Implementation__, binStream);
            BinarySerializer.ToStream(this._fk_Property, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStreamCollectionEntries(this.ContextMenu__Implementation__, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream((int)this.ControlType, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamCollectionEntries(this.Children__Implementation__, binStream);
            BinarySerializer.FromStream(out this._fk_Property, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStreamCollectionEntries(this.ContextMenu__Implementation__, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStreamConverter(v => this.ControlType = (Kistl.App.GUI.VisualType)v, binStream);
        }

#endregion

    }


}