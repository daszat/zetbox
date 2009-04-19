
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
    /// Metadefinition Object for Enumeration Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumerationProperty")]
    public class EnumerationProperty__Implementation__ : Kistl.App.Base.ValueTypeProperty__Implementation__, EnumerationProperty
    {
    
		public EnumerationProperty__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Enumeration der Eigenschaft
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                if (fk_Enumeration.HasValue)
                    return Context.Find<Kistl.App.Base.Enumeration>(fk_Enumeration.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Enumeration == null)
					return;
                else if (value != null && value.ID == _fk_Enumeration)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Enumeration;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Enumeration", oldValue, value);
                
				// next, set the local reference
                _fk_Enumeration = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Enumeration", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Enumeration
        {
            get
            {
                return _fk_Enumeration;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Enumeration != value)
                {
					var __oldValue = _fk_Enumeration;
                    NotifyPropertyChanging("Enumeration", __oldValue, value);
                    _fk_Enumeration = value;
                    NotifyPropertyChanged("Enumeration", __oldValue, value);
                }
            }
        }
        private int? _fk_Enumeration;

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_EnumerationProperty != null)
            {
                OnGetPropertyType_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<EnumerationProperty> OnGetPropertyType_EnumerationProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_EnumerationProperty != null)
            {
                OnGetPropertyTypeString_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(EnumerationProperty));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (EnumerationProperty)obj;
			var otherImpl = (EnumerationProperty__Implementation__)obj;
			var me = (EnumerationProperty)this;

			this.fk_Enumeration = otherImpl.fk_Enumeration;
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
            if (OnToString_EnumerationProperty != null)
            {
                OnToString_EnumerationProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<EnumerationProperty> OnToString_EnumerationProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationProperty != null) OnPreSave_EnumerationProperty(this);
        }
        public event ObjectEventHandler<EnumerationProperty> OnPreSave_EnumerationProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationProperty != null) OnPostSave_EnumerationProperty(this);
        }
        public event ObjectEventHandler<EnumerationProperty> OnPostSave_EnumerationProperty;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Enumeration":
                    fk_Enumeration = id;
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
            BinarySerializer.ToStream(this._fk_Enumeration, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Enumeration, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._fk_Enumeration, xml, "Enumeration", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._fk_Enumeration, xml, "Enumeration", "http://dasz.at/Kistl");
        }

#endregion

    }


}