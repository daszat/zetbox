
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
					_Children 
						= new ClientCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_Children35CollectionEntry__Implementation__>(
							this, 
							(ICollection<Visual_Children35CollectionEntry__Implementation__>)Context.FetchRelation<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_Children35CollectionEntry__Implementation__>(RelationEndRole.A, this));
				}
				return _Children;
			}
		}

		private ClientCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_Children35CollectionEntry__Implementation__> _Children;

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
					_ContextMenu 
						= new ClientCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_ContextMenu40CollectionEntry__Implementation__>(
							this, 
							(ICollection<Visual_ContextMenu40CollectionEntry__Implementation__>)Context.FetchRelation<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_ContextMenu40CollectionEntry__Implementation__>(RelationEndRole.A, this));
				}
				return _ContextMenu;
			}
		}

		private ClientCollectionBSideWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Visual_ContextMenu40CollectionEntry__Implementation__> _ContextMenu;

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
                    NotifyPropertyChanging("ControlType");
                    _ControlType = value;
                    NotifyPropertyChanged("ControlType");
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
                fk_Method = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                return _fk_Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Method != value)
                {
                    NotifyPropertyChanging("Method");
                    _fk_Method = value;
                    NotifyPropertyChanging("Method");
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
        public Kistl.App.Base.BaseProperty Property
        {
            get
            {
                if (fk_Property.HasValue)
                    return Context.Find<Kistl.App.Base.BaseProperty>(fk_Property.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                fk_Property = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Property
        {
            get
            {
                return _fk_Property;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Property != value)
                {
                    NotifyPropertyChanging("Property");
                    _fk_Property = value;
                    NotifyPropertyChanging("Property");
                }
            }
        }
        private int? _fk_Property;

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

#endregion

    }


}