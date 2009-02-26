
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
    [EdmEntityType(NamespaceName="Model", Name="Template")]
    [System.Diagnostics.DebuggerDisplay("Template")]
    public class Template__Implementation__ : BaseServerDataObject_EntityFramework, Template
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
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
    /*
    NewRelation: FK_Template_Assembly_Template_39 
    A: ZeroOrMore Template as Template (site: A, no Relation, prop ID=163)
    B: ZeroOrOne Assembly as DisplayedTypeAssembly (site: B, no Relation, prop ID=163)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly DisplayedTypeAssembly
        {
            get
            {
                return DisplayedTypeAssembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                DisplayedTypeAssembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DisplayedTypeAssembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && DisplayedTypeAssembly != null)
                {
                    _fk_DisplayedTypeAssembly = DisplayedTypeAssembly.ID;
                }
                return _fk_DisplayedTypeAssembly;
            }
            set
            {
                _fk_DisplayedTypeAssembly = value;
            }
        }
        private int? _fk_DisplayedTypeAssembly;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Template_Assembly_Template_39", "DisplayedTypeAssembly")]
        public Kistl.App.Base.Assembly__Implementation__ DisplayedTypeAssembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_Template_Assembly_Template_39",
                        "DisplayedTypeAssembly");
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
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_Template_Assembly_Template_39",
                        "DisplayedTypeAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string DisplayedTypeFullName
        {
            get
            {
                return _DisplayedTypeFullName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayedTypeFullName != value)
                {
                    NotifyPropertyChanging("DisplayedTypeFullName");
                    _DisplayedTypeFullName = value;
                    NotifyPropertyChanged("DisplayedTypeFullName");
                }
            }
        }
        private string _DisplayedTypeFullName;

        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayName != value)
                {
                    NotifyPropertyChanging("DisplayName");
                    _DisplayName = value;
                    NotifyPropertyChanged("DisplayName");
                }
            }
        }
        private string _DisplayName;

        /// <summary>
        /// The main menu for this Template
        /// </summary>
    /*
    NewRelation: FK_Template_Visual_Template_41 
    A: ZeroOrMore Template as Template (site: A, no Relation, prop ID=165)
    B: ZeroOrMore Visual as Menu (site: B, no Relation, prop ID=165)
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.Visual> Menu
        {
            get
            {
                if (_MenuWrapper == null)
                {
                    _MenuWrapper = new EntityCollectionBSideWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Kistl.App.GUI.Template_Menu41CollectionEntry__Implementation__>(
                            this,
                            Menu__Implementation__);
                }
                return _MenuWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Template_Visual_Template_41", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.Template_Menu41CollectionEntry__Implementation__> Menu__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.Template_Menu41CollectionEntry__Implementation__>(
                        "Model.FK_Template_Visual_Template_41",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionBSideWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Kistl.App.GUI.Template_Menu41CollectionEntry__Implementation__> _MenuWrapper;
        

        /// <summary>
        /// The visual representation of this Template
        /// </summary>
    /*
    NewRelation: FK_Template_Visual_Template_38 
    A: ZeroOrMore Template as Template (site: A, no Relation, prop ID=155)
    B: ZeroOrOne Visual as VisualTree (site: B, no Relation, prop ID=155)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual VisualTree
        {
            get
            {
                return VisualTree__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                VisualTree__Implementation__ = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_VisualTree
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && VisualTree != null)
                {
                    _fk_VisualTree = VisualTree.ID;
                }
                return _fk_VisualTree;
            }
            set
            {
                _fk_VisualTree = value;
            }
        }
        private int? _fk_VisualTree;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Template_Visual_Template_38", "VisualTree")]
        public Kistl.App.GUI.Visual__Implementation__ VisualTree__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Template_Visual_Template_38",
                        "VisualTree");
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
                EntityReference<Kistl.App.GUI.Visual__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Visual__Implementation__>(
                        "Model.FK_Template_Visual_Template_38",
                        "VisualTree");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Visual__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>

		public virtual void PrepareDefault(Kistl.App.Base.ObjectClass cls) 
		{
            // base.PrepareDefault();
            if (OnPrepareDefault_Template != null)
            {
				OnPrepareDefault_Template(this, cls);
			}
			else
			{
                throw new NotImplementedException("No handler registered on Template.PrepareDefault");
			}
        }
		public delegate void PrepareDefault_Handler<T>(T obj, Kistl.App.Base.ObjectClass cls);
		public event PrepareDefault_Handler<Template> OnPrepareDefault_Template;



		public override Type GetInterfaceType()
		{
			return typeof(Template);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Template != null)
            {
                OnToString_Template(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Template> OnToString_Template;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Template != null) OnPreSave_Template(this);
        }
        public event ObjectEventHandler<Template> OnPreSave_Template;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Template != null) OnPostSave_Template(this);
        }
        public event ObjectEventHandler<Template> OnPostSave_Template;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.ToStream(this._DisplayedTypeFullName, binStream);
            BinarySerializer.ToStream(this._DisplayName, binStream);
			// collections have to be loaded separately for now
            // BinarySerializer.ToStreamCollectionEntries(this.Menu__Implementation__, binStream);
            BinarySerializer.ToStream(this.fk_VisualTree, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_DisplayedTypeAssembly;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_DisplayedTypeAssembly = tmp;
            }
            BinarySerializer.FromStream(out this._DisplayedTypeFullName, binStream);
            BinarySerializer.FromStream(out this._DisplayName, binStream);
			// collections have to be loaded separately for now
            // BinarySerializer.FromStreamCollectionEntries(this.Menu__Implementation__, binStream);
            {
                var tmp = this.fk_VisualTree;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_VisualTree = tmp;
            }
        }

#endregion

    }


}