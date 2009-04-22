
namespace Kistl.App.Base
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
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// Describes one end of a relation between two object classes
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RelationEnd")]
    public class RelationEnd__Implementation__ : BaseClientDataObject_ClientObjects, RelationEnd
    {
    
		public RelationEnd__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The Relation using this RelationEnd as A
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation AParent
        {
            get
            {
                if (fk_AParent.HasValue)
                    return Context.Find<Kistl.App.Base.Relation>(fk_AParent.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_AParent == null)
					return;
                else if (value != null && value.ID == _fk_AParent)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = AParent;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("AParent", oldValue, value);
                
				// next, set the local reference
                _fk_AParent = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// unset old reference
					oldValue.A = null;
				}

                if (value != null)
                {
					// set new reference
                    value.A = this;
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("AParent", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_AParent
        {
            get
            {
                return _fk_AParent;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_AParent != value)
                {
					var __oldValue = _fk_AParent;
                    NotifyPropertyChanging("AParent", __oldValue, value);
                    _fk_AParent = value;
                    NotifyPropertyChanged("AParent", __oldValue, value);
                }
            }
        }
        private int? _fk_AParent;

        /// <summary>
        /// The Relation using this RelationEnd as B
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation BParent
        {
            get
            {
                if (fk_BParent.HasValue)
                    return Context.Find<Kistl.App.Base.Relation>(fk_BParent.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_BParent == null)
					return;
                else if (value != null && value.ID == _fk_BParent)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = BParent;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("BParent", oldValue, value);
                
				// next, set the local reference
                _fk_BParent = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// unset old reference
					oldValue.B = null;
				}

                if (value != null)
                {
					// set new reference
                    value.B = this;
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("BParent", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_BParent
        {
            get
            {
                return _fk_BParent;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_BParent != value)
                {
					var __oldValue = _fk_BParent;
                    NotifyPropertyChanging("BParent", __oldValue, value);
                    _fk_BParent = value;
                    NotifyPropertyChanged("BParent", __oldValue, value);
                }
            }
        }
        private int? _fk_BParent;

        /// <summary>
        /// Is true, if this RelationEnd persists the order of its elements
        /// </summary>
        // value type property
        public virtual bool HasPersistentOrder
        {
            get
            {
                return _HasPersistentOrder;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_HasPersistentOrder != value)
                {
					var __oldValue = _HasPersistentOrder;
                    NotifyPropertyChanging("HasPersistentOrder", __oldValue, value);
                    _HasPersistentOrder = value;
                    NotifyPropertyChanged("HasPersistentOrder", __oldValue, value);
                }
            }
        }
        private bool _HasPersistentOrder;

        /// <summary>
        /// Specifies how many instances may occur on this end of the relation.
        /// </summary>
        // enumeration property
        public virtual Kistl.App.Base.Multiplicity Multiplicity
        {
            get
            {
                return _Multiplicity;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Multiplicity != value)
                {
					var __oldValue = _Multiplicity;
                    NotifyPropertyChanging("Multiplicity", __oldValue, value);
                    _Multiplicity = value;
                    NotifyPropertyChanged("Multiplicity", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Multiplicity _Multiplicity;

        /// <summary>
        /// The ORP to navigate FROM this end of the relation. MAY be null.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty Navigator
        {
            get
            {
                if (fk_Navigator.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectReferenceProperty>(fk_Navigator.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Navigator == null)
					return;
                else if (value != null && value.ID == _fk_Navigator)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Navigator;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Navigator", oldValue, value);
                
				// next, set the local reference
                _fk_Navigator = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// unset old reference
					oldValue.RelationEnd = null;
				}

                if (value != null)
                {
					// set new reference
                    value.RelationEnd = this;
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Navigator", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Navigator
        {
            get
            {
                return _fk_Navigator;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Navigator != value)
                {
					var __oldValue = _fk_Navigator;
                    NotifyPropertyChanging("Navigator", __oldValue, value);
                    _fk_Navigator = value;
                    NotifyPropertyChanged("Navigator", __oldValue, value);
                }
            }
        }
        private int? _fk_Navigator;

        /// <summary>
        /// Which RelationEndRole this End has
        /// </summary>
        // value type property
        public virtual int Role
        {
            get
            {
                return _Role;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Role != value)
                {
					var __oldValue = _Role;
                    NotifyPropertyChanging("Role", __oldValue, value);
                    _Role = value;
                    NotifyPropertyChanged("Role", __oldValue, value);
                }
            }
        }
        private int _Role;

        /// <summary>
        /// This end's role name in the relation
        /// </summary>
        // value type property
        public virtual string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_RoleName != value)
                {
					var __oldValue = _RoleName;
                    NotifyPropertyChanging("RoleName", __oldValue, value);
                    _RoleName = value;
                    NotifyPropertyChanged("RoleName", __oldValue, value);
                }
            }
        }
        private string _RoleName;

        /// <summary>
        /// Specifies which type this End of the relation has. MUST NOT be null.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass Type
        {
            get
            {
                if (fk_Type.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectClass>(fk_Type.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Type == null)
					return;
                else if (value != null && value.ID == _fk_Type)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Type;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Type", oldValue, value);
                
				// next, set the local reference
                _fk_Type = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Type", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Type
        {
            get
            {
                return _fk_Type;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Type != value)
                {
					var __oldValue = _fk_Type;
                    NotifyPropertyChanging("Type", __oldValue, value);
                    _fk_Type = value;
                    NotifyPropertyChanged("Type", __oldValue, value);
                }
            }
        }
        private int? _fk_Type;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(RelationEnd));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (RelationEnd)obj;
			var otherImpl = (RelationEnd__Implementation__)obj;
			var me = (RelationEnd)this;

			me.HasPersistentOrder = other.HasPersistentOrder;
			me.Multiplicity = other.Multiplicity;
			me.Role = other.Role;
			me.RoleName = other.RoleName;
			this.fk_AParent = otherImpl.fk_AParent;
			this.fk_BParent = otherImpl.fk_BParent;
			this.fk_Navigator = otherImpl.fk_Navigator;
			this.fk_Type = otherImpl.fk_Type;
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
            if (OnToString_RelationEnd != null)
            {
                OnToString_RelationEnd(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<RelationEnd> OnToString_RelationEnd;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_RelationEnd != null) OnPreSave_RelationEnd(this);
        }
        public event ObjectEventHandler<RelationEnd> OnPreSave_RelationEnd;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_RelationEnd != null) OnPostSave_RelationEnd(this);
        }
        public event ObjectEventHandler<RelationEnd> OnPostSave_RelationEnd;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "AParent":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(224).Constraints
						.Where(c => !c.IsValid(this, this.AParent))
						.Select(c => c.GetErrorText(this, this.AParent))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "BParent":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(223).Constraints
						.Where(c => !c.IsValid(this, this.BParent))
						.Select(c => c.GetErrorText(this, this.BParent))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "HasPersistentOrder":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(220).Constraints
						.Where(c => !c.IsValid(this, this.HasPersistentOrder))
						.Select(c => c.GetErrorText(this, this.HasPersistentOrder))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Multiplicity":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(219).Constraints
						.Where(c => !c.IsValid(this, this.Multiplicity))
						.Select(c => c.GetErrorText(this, this.Multiplicity))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Navigator":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(218).Constraints
						.Where(c => !c.IsValid(this, this.Navigator))
						.Select(c => c.GetErrorText(this, this.Navigator))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Role":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(217).Constraints
						.Where(c => !c.IsValid(this, this.Role))
						.Select(c => c.GetErrorText(this, this.Role))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "RoleName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(216).Constraints
						.Where(c => !c.IsValid(this, this.RoleName))
						.Select(c => c.GetErrorText(this, this.RoleName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Type":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(215).Constraints
						.Where(c => !c.IsValid(this, this.Type))
						.Select(c => c.GetErrorText(this, this.Type))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Navigator":
                    fk_Navigator = id;
                    break;
                case "Type":
                    fk_Type = id;
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
            BinarySerializer.ToStream(this._fk_AParent, binStream);
            BinarySerializer.ToStream(this._fk_BParent, binStream);
            BinarySerializer.ToStream(this._HasPersistentOrder, binStream);
            BinarySerializer.ToStream((int)((RelationEnd)this).Multiplicity, binStream);
            BinarySerializer.ToStream(this._fk_Navigator, binStream);
            BinarySerializer.ToStream(this._Role, binStream);
            BinarySerializer.ToStream(this._RoleName, binStream);
            BinarySerializer.ToStream(this._fk_Type, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_AParent, binStream);
            BinarySerializer.FromStream(out this._fk_BParent, binStream);
            BinarySerializer.FromStream(out this._HasPersistentOrder, binStream);
            BinarySerializer.FromStreamConverter(v => ((RelationEnd)this).Multiplicity = (Kistl.App.Base.Multiplicity)v, binStream);
            BinarySerializer.FromStream(out this._fk_Navigator, binStream);
            BinarySerializer.FromStream(out this._Role, binStream);
            BinarySerializer.FromStream(out this._RoleName, binStream);
            BinarySerializer.FromStream(out this._fk_Type, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_AParent, xml, "AParent", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_BParent, xml, "BParent", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            // TODO: Add XML Serializer here
            XmlStreamer.ToStream(this._fk_Navigator, xml, "Navigator", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Role, xml, "Role", "Kistl.App.Base");
            XmlStreamer.ToStream(this._RoleName, xml, "RoleName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_Type, xml, "Type", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_AParent, xml, "AParent", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._fk_BParent, xml, "BParent", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            // TODO: Add XML Serializer here
            XmlStreamer.FromStream(ref this._fk_Navigator, xml, "Navigator", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Role, xml, "Role", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._RoleName, xml, "RoleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Type, xml, "Type", "http://dasz.at/Kistl");
        }

#endregion

    }


}