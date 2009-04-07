
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
    [System.Diagnostics.DebuggerDisplay("Template")]
    public class Template__Implementation__ : BaseClientDataObject, Template
    {
    
		public Template__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly DisplayedTypeAssembly
        {
            get
            {
                if (fk_DisplayedTypeAssembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_DisplayedTypeAssembly.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_DisplayedTypeAssembly == null)
					return;
                else if (value != null && value.ID == _fk_DisplayedTypeAssembly)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = DisplayedTypeAssembly;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DisplayedTypeAssembly", oldValue, value);
                
				// next, set the local reference
                _fk_DisplayedTypeAssembly = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DisplayedTypeAssembly", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DisplayedTypeAssembly
        {
            get
            {
                return _fk_DisplayedTypeAssembly;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DisplayedTypeAssembly != value)
                {
					var __oldValue = _fk_DisplayedTypeAssembly;
                    NotifyPropertyChanging("DisplayedTypeAssembly", __oldValue, value);
                    _fk_DisplayedTypeAssembly = value;
                    NotifyPropertyChanged("DisplayedTypeAssembly", __oldValue, value);
                }
            }
        }
        private int? _fk_DisplayedTypeAssembly;

        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
        // value type property
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
					var __oldValue = _DisplayedTypeFullName;
                    NotifyPropertyChanging("DisplayedTypeFullName", __oldValue, value);
                    _DisplayedTypeFullName = value;
                    NotifyPropertyChanged("DisplayedTypeFullName", __oldValue, value);
                }
            }
        }
        private string _DisplayedTypeFullName;

        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
        // value type property
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
					var __oldValue = _DisplayName;
                    NotifyPropertyChanging("DisplayName", __oldValue, value);
                    _DisplayName = value;
                    NotifyPropertyChanged("DisplayName", __oldValue, value);
                }
            }
        }
        private string _DisplayName;

        /// <summary>
        /// The main menu for this Template
        /// </summary>
        // collection reference property

		public ICollection<Kistl.App.GUI.Visual> Menu
		{
			get
			{
				if (_Menu == null)
				{
					_Menu 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Template_Menu61CollectionEntry__Implementation__>(
							this, 
							Context.FetchRelation<Template_Menu61CollectionEntry__Implementation__>(61, RelationEndRole.A, this));
				}
				return _Menu;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.GUI.Template, Kistl.App.GUI.Visual, Template_Menu61CollectionEntry__Implementation__> _Menu;

        /// <summary>
        /// The visual representation of this Template
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual VisualTree
        {
            get
            {
                if (fk_VisualTree.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_VisualTree.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_VisualTree == null)
					return;
                else if (value != null && value.ID == _fk_VisualTree)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = VisualTree;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("VisualTree", oldValue, value);
                
				// next, set the local reference
                _fk_VisualTree = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("VisualTree", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_VisualTree
        {
            get
            {
                return _fk_VisualTree;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_VisualTree != value)
                {
					var __oldValue = _fk_VisualTree;
                    NotifyPropertyChanging("VisualTree", __oldValue, value);
                    _fk_VisualTree = value;
                    NotifyPropertyChanged("VisualTree", __oldValue, value);
                }
            }
        }
        private int? _fk_VisualTree;

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



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Template));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Template)obj;
			var otherImpl = (Template__Implementation__)obj;
			var me = (Template)this;

			me.DisplayedTypeFullName = other.DisplayedTypeFullName;
			me.DisplayName = other.DisplayName;
			this.fk_DisplayedTypeAssembly = otherImpl.fk_DisplayedTypeAssembly;
			this.fk_VisualTree = otherImpl.fk_VisualTree;
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "DisplayedTypeAssembly":
                    fk_DisplayedTypeAssembly = id;
                    break;
                case "VisualTree":
                    fk_VisualTree = id;
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
            BinarySerializer.ToStream(this._fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.ToStream(this._DisplayedTypeFullName, binStream);
            BinarySerializer.ToStream(this._DisplayName, binStream);
            BinarySerializer.ToStream(this._fk_VisualTree, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.FromStream(out this._DisplayedTypeFullName, binStream);
            BinarySerializer.FromStream(out this._DisplayName, binStream);
            BinarySerializer.FromStream(out this._fk_VisualTree, binStream);
        }

#endregion

    }


}