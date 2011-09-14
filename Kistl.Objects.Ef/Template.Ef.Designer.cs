// <autogenerated/>

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
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Template")]
    [System.Diagnostics.DebuggerDisplay("Template")]
    public class TemplateEfImpl : BaseServerDataObject_EntityFramework, Template
    {
        [Obsolete]
        public TemplateEfImpl()
            : base(null)
        {
        }

        public TemplateEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
    /*
    Relation: FK_Template_has_DisplayedTypeAssembly
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Assembly as DisplayedTypeAssembly
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DisplayedTypeAssembly
        // fkBackingName=_fk_DisplayedTypeAssembly; fkGuidBackingName=_fk_guid_DisplayedTypeAssembly;
        // referencedInterface=Kistl.App.Base.Assembly; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly DisplayedTypeAssembly
        {
            get { return DisplayedTypeAssemblyImpl; }
            set { DisplayedTypeAssemblyImpl = (Kistl.App.Base.AssemblyEfImpl)value; }
        }

        private int? _fk_DisplayedTypeAssembly;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Template_has_DisplayedTypeAssembly", "DisplayedTypeAssembly")]
        public Kistl.App.Base.AssemblyEfImpl DisplayedTypeAssemblyImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.AssemblyEfImpl __value;
                EntityReference<Kistl.App.Base.AssemblyEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.AssemblyEfImpl>(
                        "Model.FK_Template_has_DisplayedTypeAssembly",
                        "DisplayedTypeAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnDisplayedTypeAssembly_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Assembly>(__value);
                    OnDisplayedTypeAssembly_Getter(this, e);
                    __value = (Kistl.App.Base.AssemblyEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Base.AssemblyEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.AssemblyEfImpl>(
                        "Model.FK_Template_has_DisplayedTypeAssembly",
                        "DisplayedTypeAssembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Base.AssemblyEfImpl __oldValue = (Kistl.App.Base.AssemblyEfImpl)r.Value;
                Kistl.App.Base.AssemblyEfImpl __newValue = (Kistl.App.Base.AssemblyEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("DisplayedTypeAssembly", null, __oldValue, __newValue);

                if (OnDisplayedTypeAssembly_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Assembly>(__oldValue, __newValue);
                    OnDisplayedTypeAssembly_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.AssemblyEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Base.AssemblyEfImpl)__newValue;

                if (OnDisplayedTypeAssembly_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Assembly>(__oldValue, __newValue);
                    OnDisplayedTypeAssembly_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("DisplayedTypeAssembly", null, __oldValue, __newValue);
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DisplayedTypeAssembly
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, Kistl.App.Base.Assembly> OnDisplayedTypeAssembly_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, Kistl.App.Base.Assembly> OnDisplayedTypeAssembly_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, Kistl.App.Base.Assembly> OnDisplayedTypeAssembly_PostSetter;

        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string DisplayedTypeFullName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DisplayedTypeFullName;
                if (OnDisplayedTypeFullName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDisplayedTypeFullName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayedTypeFullName != value)
                {
                    var __oldValue = _DisplayedTypeFullName;
                    var __newValue = value;
                    if (OnDisplayedTypeFullName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayedTypeFullName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DisplayedTypeFullName", __oldValue, __newValue);
                    _DisplayedTypeFullName = __newValue;
                    NotifyPropertyChanged("DisplayedTypeFullName", __oldValue, __newValue);
                    if (OnDisplayedTypeFullName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayedTypeFullName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _DisplayedTypeFullName;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, string> OnDisplayedTypeFullName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, string> OnDisplayedTypeFullName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, string> OnDisplayedTypeFullName_PostSetter;

        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string DisplayName
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DisplayName;
                if (OnDisplayName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDisplayName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayName != value)
                {
                    var __oldValue = _DisplayName;
                    var __newValue = value;
                    if (OnDisplayName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DisplayName", __oldValue, __newValue);
                    _DisplayName = __newValue;
                    NotifyPropertyChanged("DisplayName", __oldValue, __newValue);
                    if (OnDisplayName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _DisplayName;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, string> OnDisplayName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, string> OnDisplayName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, string> OnDisplayName_PostSetter;

        /// <summary>
        /// The main menu for this Template
        /// </summary>
    /*
    Relation: FK_Template_hasMenu_Menu
    A: ZeroOrMore Template as Template
    B: ZeroOrMore Visual as Menu
    Preferred Storage: Separate
    */
        // collection reference property
        // Kistl.DalProvider.Ef.Generator.Templates.Properties.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.Visual> Menu
        {
            get
            {
                if (_Menu == null)
                {
                    _Menu = new BSideCollectionWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl, EntityCollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl>>(
                            this,
                            MenuImpl);
                }
                return _Menu;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Template_hasMenu_Menu_A", "CollectionEntry")]
        public EntityCollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl> MenuImpl
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl>(
                        "Model.FK_Template_hasMenu_Menu_A",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                c.ForEach(i => i.AttachToContext(Context));
                return c;
            }
        }
        private BSideCollectionWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl, EntityCollection<Kistl.App.GUI.Template_hasMenu_Visual_RelationEntryEfImpl>> _Menu;

        /// <summary>
        /// The visual representation of this Template
        /// </summary>
    /*
    Relation: FK_Template_has_VisualTree
    A: ZeroOrMore Template as Template
    B: ZeroOrOne Visual as VisualTree
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for VisualTree
        // fkBackingName=_fk_VisualTree; fkGuidBackingName=_fk_guid_VisualTree;
        // referencedInterface=Kistl.App.GUI.Visual; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual VisualTree
        {
            get { return VisualTreeImpl; }
            set { VisualTreeImpl = (Kistl.App.GUI.VisualEfImpl)value; }
        }

        private int? _fk_VisualTree;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Template_has_VisualTree", "VisualTree")]
        public Kistl.App.GUI.VisualEfImpl VisualTreeImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.GUI.VisualEfImpl __value;
                EntityReference<Kistl.App.GUI.VisualEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.VisualEfImpl>(
                        "Model.FK_Template_has_VisualTree",
                        "VisualTree");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnVisualTree_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.GUI.Visual>(__value);
                    OnVisualTree_Getter(this, e);
                    __value = (Kistl.App.GUI.VisualEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.GUI.VisualEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.VisualEfImpl>(
                        "Model.FK_Template_has_VisualTree",
                        "VisualTree");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.GUI.VisualEfImpl __oldValue = (Kistl.App.GUI.VisualEfImpl)r.Value;
                Kistl.App.GUI.VisualEfImpl __newValue = (Kistl.App.GUI.VisualEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("VisualTree", null, __oldValue, __newValue);

                if (OnVisualTree_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.GUI.Visual>(__oldValue, __newValue);
                    OnVisualTree_PreSetter(this, e);
                    __newValue = (Kistl.App.GUI.VisualEfImpl)e.Result;
                }

                r.Value = (Kistl.App.GUI.VisualEfImpl)__newValue;

                if (OnVisualTree_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.GUI.Visual>(__oldValue, __newValue);
                    OnVisualTree_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("VisualTree", null, __oldValue, __newValue);
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for VisualTree
		public static event PropertyGetterHandler<Kistl.App.GUI.Template, Kistl.App.GUI.Visual> OnVisualTree_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Template, Kistl.App.GUI.Visual> OnVisualTree_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Template, Kistl.App.GUI.Visual> OnVisualTree_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnPrepareDefault_Template")]
        public virtual void PrepareDefault(Kistl.App.Base.ObjectClass cls)
        {
            // base.PrepareDefault();
            if (OnPrepareDefault_Template != null)
            {
                OnPrepareDefault_Template(this, cls);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method Template.PrepareDefault");
            }
        }
        public delegate void PrepareDefault_Handler<T>(T obj, Kistl.App.Base.ObjectClass cls);
        public static event PrepareDefault_Handler<Template> OnPrepareDefault_Template;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(Template);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Template)obj;
            var otherImpl = (TemplateEfImpl)obj;
            var me = (Template)this;

            me.DisplayedTypeFullName = other.DisplayedTypeFullName;
            me.DisplayName = other.DisplayName;
            this._fk_DisplayedTypeAssembly = otherImpl._fk_DisplayedTypeAssembly;
            this._fk_VisualTree = otherImpl._fk_VisualTree;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

		public override void UpdateParent(string propertyName, int? id)
		{
			int? __oldValue, __newValue = id;
			
			switch(propertyName)
			{
                case "DisplayedTypeAssembly":
                    __oldValue = _fk_DisplayedTypeAssembly;
                    NotifyPropertyChanging("DisplayedTypeAssembly", __oldValue, __newValue);
                    _fk_DisplayedTypeAssembly = __newValue;
                    NotifyPropertyChanged("DisplayedTypeAssembly", __oldValue, __newValue);
                    break;
                case "VisualTree":
                    __oldValue = _fk_VisualTree;
                    NotifyPropertyChanging("VisualTree", __oldValue, __newValue);
                    _fk_VisualTree = __newValue;
                    NotifyPropertyChanged("VisualTree", __oldValue, __newValue);
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_DisplayedTypeAssembly.HasValue)
                DisplayedTypeAssemblyImpl = (Kistl.App.Base.AssemblyEfImpl)Context.Find<Kistl.App.Base.Assembly>(_fk_DisplayedTypeAssembly.Value);
            else
                DisplayedTypeAssemblyImpl = null;

            if (_fk_VisualTree.HasValue)
                VisualTreeImpl = (Kistl.App.GUI.VisualEfImpl)Context.Find<Kistl.App.GUI.Visual>(_fk_VisualTree.Value);
            else
                VisualTreeImpl = null;
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // else
                    new PropertyDescriptorEfImpl<TemplateEfImpl, Kistl.App.Base.Assembly>(
                        lazyCtx,
                        new Guid("c81105da-97e4-4685-af88-792c68e55a17"),
                        "DisplayedTypeAssembly",
                        null,
                        obj => obj.DisplayedTypeAssembly,
                        (obj, val) => obj.DisplayedTypeAssembly = val),
                    // else
                    new PropertyDescriptorEfImpl<TemplateEfImpl, string>(
                        lazyCtx,
                        new Guid("4b683aa1-45a9-4c5e-80e7-0ff30f5b798c"),
                        "DisplayedTypeFullName",
                        null,
                        obj => obj.DisplayedTypeFullName,
                        (obj, val) => obj.DisplayedTypeFullName = val),
                    // else
                    new PropertyDescriptorEfImpl<TemplateEfImpl, string>(
                        lazyCtx,
                        new Guid("4fc51781-b0fe-495c-91a1-90e484345515"),
                        "DisplayName",
                        null,
                        obj => obj.DisplayName,
                        (obj, val) => obj.DisplayName = val),
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorEfImpl<TemplateEfImpl, ICollection<Kistl.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("5e9612d5-019a-416b-a2e2-dfc9674a50f6"),
                        "Menu",
                        null,
                        obj => obj.Menu,
                        null), // lists are read-only properties
                    // else
                    new PropertyDescriptorEfImpl<TemplateEfImpl, Kistl.App.GUI.Visual>(
                        lazyCtx,
                        new Guid("5d2880a4-716a-4bdc-aaa9-379c006e7ed4"),
                        "VisualTree",
                        null,
                        obj => obj.VisualTree,
                        (obj, val) => obj.VisualTree = val),
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Template")]
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
        public static event ToStringHandler<Template> OnToString_Template;

        [EventBasedMethod("OnPreSave_Template")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Template != null) OnPreSave_Template(this);
        }
        public static event ObjectEventHandler<Template> OnPreSave_Template;

        [EventBasedMethod("OnPostSave_Template")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Template != null) OnPostSave_Template(this);
        }
        public static event ObjectEventHandler<Template> OnPostSave_Template;

        [EventBasedMethod("OnCreated_Template")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Template != null) OnCreated_Template(this);
        }
        public static event ObjectEventHandler<Template> OnCreated_Template;

        [EventBasedMethod("OnDeleting_Template")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Template != null) OnDeleting_Template(this);
        }
        public static event ObjectEventHandler<Template> OnDeleting_Template;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
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
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.AssemblyEfImpl>("Model.FK_Template_has_DisplayedTypeAssembly", "DisplayedTypeAssembly").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
            BinarySerializer.ToStream(this._DisplayedTypeFullName, binStream);
            BinarySerializer.ToStream(this._DisplayName, binStream);
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.GUI.VisualEfImpl>("Model.FK_Template_has_VisualTree", "VisualTree").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.FromStream(out this._DisplayedTypeFullName, binStream);
            BinarySerializer.FromStream(out this._DisplayName, binStream);
            BinarySerializer.FromStream(out this._fk_VisualTree, binStream);
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.AssemblyEfImpl>("Model.FK_Template_has_DisplayedTypeAssembly", "DisplayedTypeAssembly").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "DisplayedTypeAssembly", "Kistl.App.GUI");
            }
            XmlStreamer.ToStream(this._DisplayedTypeFullName, xml, "DisplayedTypeFullName", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._DisplayName, xml, "DisplayName", "Kistl.App.GUI");
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.GUI.VisualEfImpl>("Model.FK_Template_has_VisualTree", "VisualTree").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "VisualTree", "Kistl.App.GUI");
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_DisplayedTypeAssembly, xml, "DisplayedTypeAssembly", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._DisplayedTypeFullName, xml, "DisplayedTypeFullName", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._DisplayName, xml, "DisplayName", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_VisualTree, xml, "VisualTree", "Kistl.App.GUI");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}