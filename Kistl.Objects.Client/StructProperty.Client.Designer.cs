
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

    /// <summary>
    /// Metadefinition Object for Struct Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("StructProperty")]
    public class StructProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, StructProperty
    {
    
		public StructProperty__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Definition of this Struct
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Struct StructDefinition
        {
            get
            {
                if (fk_StructDefinition.HasValue)
                    return Context.Find<Kistl.App.Base.Struct>(fk_StructDefinition.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_StructDefinition == null)
					return;
                else if (value != null && value.ID == _fk_StructDefinition)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = StructDefinition;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("StructDefinition", oldValue, value);
                
				// next, set the local reference
                _fk_StructDefinition = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("StructDefinition", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_StructDefinition
        {
            get
            {
                return _fk_StructDefinition;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_StructDefinition != value)
                {
					var __oldValue = _fk_StructDefinition;
                    NotifyPropertyChanging("StructDefinition", __oldValue, value);
                    _fk_StructDefinition = value;
                    NotifyPropertyChanged("StructDefinition", __oldValue, value);
                }
            }
        }
        private int? _fk_StructDefinition;

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_StructProperty != null)
            {
                OnGetPropertyType_StructProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<StructProperty> OnGetPropertyType_StructProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_StructProperty != null)
            {
                OnGetPropertyTypeString_StructProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<StructProperty> OnGetPropertyTypeString_StructProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(StructProperty));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (StructProperty)obj;
			var otherImpl = (StructProperty__Implementation__)obj;
			var me = (StructProperty)this;

			this.fk_StructDefinition = otherImpl.fk_StructDefinition;
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
            if (OnToString_StructProperty != null)
            {
                OnToString_StructProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StructProperty> OnToString_StructProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StructProperty != null) OnPreSave_StructProperty(this);
        }
        public event ObjectEventHandler<StructProperty> OnPreSave_StructProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StructProperty != null) OnPostSave_StructProperty(this);
        }
        public event ObjectEventHandler<StructProperty> OnPostSave_StructProperty;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "StructDefinition":
                    fk_StructDefinition = id;
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
            BinarySerializer.ToStream(this._fk_StructDefinition, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_StructDefinition, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_StructDefinition, xml, "fk_StructDefinition", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
        }

#endregion

    }


}