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

    using Kistl.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class Visual__Implementation__Memory : BaseMemoryDataObject, Visual
    {
        [Obsolete]
        public Visual__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public Visual__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        // collection reference property
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> Children
		{
			get
			{
				if (_Children == null)
				{
					Context.FetchRelation<Visual_contains_Visual_RelationEntry__Implementation__Memory>(new Guid("4d4e1ffd-f362-40e2-9fe1-0711ded83241"), RelationEndRole.A, this);
					_Children 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_contains_Visual_RelationEntry__Implementation__Memory>(
							this, 
							new RelationshipFilterASideCollection<Visual_contains_Visual_RelationEntry__Implementation__Memory>(this.Context, this));
				}
				return _Children;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_contains_Visual_RelationEntry__Implementation__Memory> _Children;

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
        // collection reference property
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> ContextMenu
		{
			get
			{
				if (_ContextMenu == null)
				{
					Context.FetchRelation<Visual_hasContextMenu_Visual_RelationEntry__Implementation__Memory>(new Guid("358c14b9-fef5-495d-8d44-04e84186830e"), RelationEndRole.A, this);
					_ContextMenu 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_hasContextMenu_Visual_RelationEntry__Implementation__Memory>(
							this, 
							new RelationshipFilterASideCollection<Visual_hasContextMenu_Visual_RelationEntry__Implementation__Memory>(this.Context, this));
				}
				return _ContextMenu;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_hasContextMenu_Visual_RelationEntry__Implementation__Memory> _ContextMenu;

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        // value type property
           // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Description
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Description;
                if (OnDescription_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDescription_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
                    var __oldValue = _Description;
                    var __newValue = value;
                    if(OnDescription_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    _Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);
                    if(OnDescription_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Description;
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, string> OnDescription_PostSetter;

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Method
		// rel(A): Visual has Method
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
				Kistl.App.Base.Method __value;
                if (_fk_Method.HasValue)
                    __value = Context.Find<Kistl.App.Base.Method>(_fk_Method.Value);
                else
                    __value = null;

				if(OnMethod_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Method>(__value);
					OnMethod_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_Method == null)
					return;
                else if (value != null && value.ID == _fk_Method)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = Method;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Method", __oldValue, __newValue);
				
                if(OnMethod_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Method>(__oldValue, __newValue);
					OnMethod_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_Method = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Method", __oldValue, __newValue);

                if(OnMethod_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Method>(__oldValue, __newValue);
					OnMethod_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_Method;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Method
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_PostSetter;

        /// <summary>
        /// The Property to display
        /// </summary>
        // object reference property
		// BEGIN Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Property
		// rel(A): Visual has Property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property Property
        {
            get
            {
				Kistl.App.Base.Property __value;
                if (_fk_Property.HasValue)
                    __value = Context.Find<Kistl.App.Base.Property>(_fk_Property.Value);
                else
                    __value = null;

				if(OnProperty_Getter != null)
				{
					var e = new PropertyGetterEventArgs<Kistl.App.Base.Property>(__value);
					OnProperty_Getter(this, e);
					__value = e.Result;
				}
                    
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if(value != null && value.Context != this.Context) throw new WrongKistlContextException();
                
                // shortcut noops
                if (value == null && _fk_Property == null)
					return;
                else if (value != null && value.ID == _fk_Property)
					return;
			           
	            // cache old value to remove inverse references later
                var __oldValue = Property;
				var __newValue = value;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Property", __oldValue, __newValue);
				
                if(OnProperty_PreSetter != null && IsAttached)
                {
					var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnProperty_PreSetter(this, e);
					__newValue = e.Result;
                }
                
				// next, set the local reference
                _fk_Property = __newValue == null ? (int?)null : __newValue.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Property", __oldValue, __newValue);

                if(OnProperty_PostSetter != null && IsAttached)
                {
					var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
					OnProperty_PostSetter(this, e);
                }
                
            }
        }
        
        private int? _fk_Property;
		// END Kistl.DalProvider.Memory.Generator.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for Property
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_PostSetter;

        public override Type GetImplementedInterface()
        {
            return typeof(Visual);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Visual)obj;
			var otherImpl = (Visual__Implementation__Memory)obj;
			var me = (Visual)this;

			me.Description = other.Description;
			this._fk_Method = otherImpl._fk_Method;
			this._fk_Property = otherImpl._fk_Property;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Visual")]
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
        public static event ToStringHandler<Visual> OnToString_Visual;

        [EventBasedMethod("OnPreSave_Visual")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Visual != null) OnPreSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnPreSave_Visual;

        [EventBasedMethod("OnPostSave_Visual")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Visual != null) OnPostSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnPostSave_Visual;

        [EventBasedMethod("OnCreated_Visual")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Visual != null) OnCreated_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnCreated_Visual;

        [EventBasedMethod("OnDeleting_Visual")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Visual != null) OnDeleting_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnDeleting_Visual;


		private static readonly object _propertiesLock = new object();
		private static System.ComponentModel.PropertyDescriptor[] _properties;
		
		private void _InitializePropertyDescriptors(Func<IReadOnlyKistlContext> lazyCtx)
		{
			if (_properties != null) return;
			lock (_propertiesLock)
			{
				// recheck for a lost race after aquiring the lock
				if (_properties != null) return;
				
				_properties = new System.ComponentModel.PropertyDescriptor[] {
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<Visual__Implementation__Memory, ICollection<Kistl.App.GUI.Visual>>(
						lazyCtx,
						new Guid("9f69c3bd-e274-4639-b30c-8d2a9599917b"),
						"Children",
						null,
						obj => obj.Children,
						null), // lists are read-only properties
					// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
					new CustomPropertyDescriptor<Visual__Implementation__Memory, ICollection<Kistl.App.GUI.Visual>>(
						lazyCtx,
						new Guid("7b18f26e-0f3f-4554-b469-1029bd4ca10b"),
						"ContextMenu",
						null,
						obj => obj.ContextMenu,
						null), // lists are read-only properties
					// else
					new CustomPropertyDescriptor<Visual__Implementation__Memory, string>(
						lazyCtx,
						new Guid("8d3b7c91-2bbf-4dcf-bc37-318dc0fda92d"),
						"Description",
						null,
						obj => obj.Description,
						(obj, val) => obj.Description = val),
					// else
					new CustomPropertyDescriptor<Visual__Implementation__Memory, Kistl.App.Base.Method>(
						lazyCtx,
						new Guid("0b55b2ba-3ac0-4631-8a73-1e8846c8e9b1"),
						"Method",
						null,
						obj => obj.Method,
						(obj, val) => obj.Method = val),
					// else
					new CustomPropertyDescriptor<Visual__Implementation__Memory, Kistl.App.Base.Property>(
						lazyCtx,
						new Guid("a432e3ff-61ed-4726-8559-f34516181065"),
						"Property",
						null,
						obj => obj.Property,
						(obj, val) => obj.Property = val),
					// rel: Template has VisualTree (299a4cf9-3f3e-4b89-b6ba-6b163b4e5dc0)
					// rel: Visual has Method (304c9a1e-7365-45ee-a685-348fd76f10e7)
					// rel: Visual has Property (73178882-7f93-444b-bf93-75db193904cf)
				};
			}
		}
		
		protected override void CollectProperties(Func<IReadOnlyKistlContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			_InitializePropertyDescriptors(lazyCtx);
			props.AddRange(_properties);
		}
	

		public override void UpdateParent(string propertyName, int? id)
		{
			int? __oldValue, __newValue = id;
			
			switch(propertyName)
			{
                case "Method":
                    __oldValue = _fk_Method;
                    NotifyPropertyChanging("Method", __oldValue, __newValue);
                    _fk_Method = __newValue;
                    NotifyPropertyChanged("Method", __oldValue, __newValue);
                    break;
                case "Property":
                    __oldValue = _fk_Property;
                    NotifyPropertyChanging("Property", __oldValue, __newValue);
                    _fk_Property = __newValue;
                    NotifyPropertyChanged("Property", __oldValue, __newValue);
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStream(this._fk_Property, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._fk_Property, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(this._fk_Method, xml, "Method", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_Property, xml, "Property", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_Method, xml, "Method", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._fk_Property, xml, "Property", "http://dasz.at/Kistl");
        }

#endregion

    }


}