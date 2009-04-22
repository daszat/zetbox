
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
    /// Metadefinition Object for ObjectReference Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectReferenceProperty")]
    public class ObjectReferenceProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, ObjectReferenceProperty
    {
    
		public ObjectReferenceProperty__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Pointer zur Objektklasse
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass ReferenceObjectClass
        {
            get
            {
                if (fk_ReferenceObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectClass>(fk_ReferenceObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_ReferenceObjectClass == null)
					return;
                else if (value != null && value.ID == _fk_ReferenceObjectClass)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = ReferenceObjectClass;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ReferenceObjectClass", oldValue, value);
                
				// next, set the local reference
                _fk_ReferenceObjectClass = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ReferenceObjectClass", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ReferenceObjectClass
        {
            get
            {
                return _fk_ReferenceObjectClass;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ReferenceObjectClass != value)
                {
					var __oldValue = _fk_ReferenceObjectClass;
                    NotifyPropertyChanging("ReferenceObjectClass", __oldValue, value);
                    _fk_ReferenceObjectClass = value;
                    NotifyPropertyChanged("ReferenceObjectClass", __oldValue, value);
                }
            }
        }
        private int? _fk_ReferenceObjectClass;

        /// <summary>
        /// The RelationEnd describing this Property
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.RelationEnd RelationEnd
        {
            get
            {
                if (fk_RelationEnd.HasValue)
                    return Context.Find<Kistl.App.Base.RelationEnd>(fk_RelationEnd.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_RelationEnd == null)
					return;
                else if (value != null && value.ID == _fk_RelationEnd)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = RelationEnd;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("RelationEnd", oldValue, value);
                
				// next, set the local reference
                _fk_RelationEnd = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// unset old reference
					oldValue.Navigator = null;
				}

                if (value != null)
                {
					// set new reference
                    value.Navigator = this;
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("RelationEnd", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_RelationEnd
        {
            get
            {
                return _fk_RelationEnd;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_RelationEnd != value)
                {
					var __oldValue = _fk_RelationEnd;
                    NotifyPropertyChanging("RelationEnd", __oldValue, value);
                    _fk_RelationEnd = value;
                    NotifyPropertyChanged("RelationEnd", __oldValue, value);
                }
            }
        }
        private int? _fk_RelationEnd;

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_ObjectReferenceProperty != null)
            {
                OnGetPropertyType_ObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<ObjectReferenceProperty> OnGetPropertyType_ObjectReferenceProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ObjectReferenceProperty != null)
            {
                OnGetPropertyTypeString_ObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<ObjectReferenceProperty> OnGetPropertyTypeString_ObjectReferenceProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectReferenceProperty));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ObjectReferenceProperty)obj;
			var otherImpl = (ObjectReferenceProperty__Implementation__)obj;
			var me = (ObjectReferenceProperty)this;

			this.fk_ReferenceObjectClass = otherImpl.fk_ReferenceObjectClass;
			this.fk_RelationEnd = otherImpl.fk_RelationEnd;
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
            if (OnToString_ObjectReferenceProperty != null)
            {
                OnToString_ObjectReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectReferenceProperty> OnToString_ObjectReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectReferenceProperty != null) OnPreSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPreSave_ObjectReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectReferenceProperty != null) OnPostSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPostSave_ObjectReferenceProperty;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ReferenceObjectClass":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(46).Constraints
						.Where(c => !c.IsValid(this, this.ReferenceObjectClass))
						.Select(c => c.GetErrorText(this, this.ReferenceObjectClass))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "RelationEnd":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(222).Constraints
						.Where(c => !c.IsValid(this, this.RelationEnd))
						.Select(c => c.GetErrorText(this, this.RelationEnd))
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
                case "ReferenceObjectClass":
                    fk_ReferenceObjectClass = id;
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
            BinarySerializer.ToStream(this._fk_ReferenceObjectClass, binStream);
            BinarySerializer.ToStream(this._fk_RelationEnd, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ReferenceObjectClass, binStream);
            BinarySerializer.FromStream(out this._fk_RelationEnd, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_ReferenceObjectClass, xml, "ReferenceObjectClass", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_RelationEnd, xml, "RelationEnd", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_ReferenceObjectClass, xml, "ReferenceObjectClass", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._fk_RelationEnd, xml, "RelationEnd", "http://dasz.at/Kistl");
        }

#endregion

    }


}