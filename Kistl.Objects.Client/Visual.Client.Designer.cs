
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

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class Visual__Implementation__ : BaseClientDataObject, Visual
    {
    
		public Visual__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        // collection reference property

		public ICollection<Kistl.App.GUI.Visual> Children
		{
			get
			{
				if (_Children == null)
				{
					Context.FetchRelation<Visual_Children55CollectionEntry__Implementation__>(55, RelationEndRole.A, this);
					_Children 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_Children55CollectionEntry__Implementation__>(
							this, 
							new RelationshipFilterASideCollection<Visual_Children55CollectionEntry__Implementation__>(this.Context, this));
				}
				return _Children;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_Children55CollectionEntry__Implementation__> _Children;

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
        // collection reference property

		public ICollection<Kistl.App.GUI.Visual> ContextMenu
		{
			get
			{
				if (_ContextMenu == null)
				{
					Context.FetchRelation<Visual_ContextMenu60CollectionEntry__Implementation__>(60, RelationEndRole.A, this);
					_ContextMenu 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_ContextMenu60CollectionEntry__Implementation__>(
							this, 
							new RelationshipFilterASideCollection<Visual_ContextMenu60CollectionEntry__Implementation__>(this.Context, this));
				}
				return _ContextMenu;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_ContextMenu60CollectionEntry__Implementation__> _ContextMenu;

        /// <summary>
        /// Which visual is represented here
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType ControlType
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
        private Kistl.App.GUI.VisualType _ControlType;

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        // value type property
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
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
                if (fk_Method.HasValue)
                    return Context.Find<Kistl.App.Base.Method>(fk_Method.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Method == null)
					return;
                else if (value != null && value.ID == _fk_Method)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Method;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Method", oldValue, value);
                
				// next, set the local reference
                _fk_Method = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Method", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                return _fk_Method;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Method != value)
                {
					var __oldValue = _fk_Method;
                    NotifyPropertyChanging("Method", __oldValue, value);
                    _fk_Method = value;
                    NotifyPropertyChanged("Method", __oldValue, value);
                }
            }
        }
        private int? _fk_Method;

        /// <summary>
        /// The Property to display
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property Property
        {
            get
            {
                if (fk_Property.HasValue)
                    return Context.Find<Kistl.App.Base.Property>(fk_Property.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Property == null)
					return;
                else if (value != null && value.ID == _fk_Property)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Property;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Property", oldValue, value);
                
				// next, set the local reference
                _fk_Property = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Property", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Property
        {
            get
            {
                return _fk_Property;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Property != value)
                {
					var __oldValue = _fk_Property;
                    NotifyPropertyChanging("Property", __oldValue, value);
                    _fk_Property = value;
                    NotifyPropertyChanged("Property", __oldValue, value);
                }
            }
        }
        private int? _fk_Property;

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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Method":
                    fk_Method = id;
                    break;
                case "Property":
                    fk_Property = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream((int)((Visual)this).ControlType, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStream(this._fk_Property, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => ((Visual)this).ControlType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._fk_Property, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            // TODO: Add XML Serializer here
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._fk_Method, xml, "Method", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._fk_Property, xml, "Property", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Description, xml, "Description", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._fk_Method, xml, "Method", "http://dasz.at/Kistl");
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._fk_Property, xml, "Property", "http://dasz.at/Kistl");
        }

#endregion

    }


}