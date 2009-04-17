
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
    [System.Diagnostics.DebuggerDisplay("PresentableModelDescriptor")]
    public class PresentableModelDescriptor__Implementation__ : BaseClientDataObject, PresentableModelDescriptor
    {
    
		public PresentableModelDescriptor__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The default visual type used for this PresentableModel
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType DefaultVisualType
        {
            get
            {
                return _DefaultVisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultVisualType != value)
                {
					var __oldValue = _DefaultVisualType;
                    NotifyPropertyChanging("DefaultVisualType", __oldValue, value);
                    _DefaultVisualType = value;
                    NotifyPropertyChanged("DefaultVisualType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _DefaultVisualType;

        /// <summary>
        /// describe this PresentableModel
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
        /// The described CLR class' reference
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef PresentableModelRef
        {
            get
            {
                if (fk_PresentableModelRef.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_PresentableModelRef.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_PresentableModelRef == null)
					return;
                else if (value != null && value.ID == _fk_PresentableModelRef)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = PresentableModelRef;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("PresentableModelRef", oldValue, value);
                
				// next, set the local reference
                _fk_PresentableModelRef = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("PresentableModelRef", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_PresentableModelRef
        {
            get
            {
                return _fk_PresentableModelRef;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_PresentableModelRef != value)
                {
					var __oldValue = _fk_PresentableModelRef;
                    NotifyPropertyChanging("PresentableModelRef", __oldValue, value);
                    _fk_PresentableModelRef = value;
                    NotifyPropertyChanged("PresentableModelRef", __oldValue, value);
                }
            }
        }
        private int? _fk_PresentableModelRef;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresentableModelDescriptor));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (PresentableModelDescriptor)obj;
			var otherImpl = (PresentableModelDescriptor__Implementation__)obj;
			var me = (PresentableModelDescriptor)this;

			me.DefaultVisualType = other.DefaultVisualType;
			me.Description = other.Description;
			this.fk_PresentableModelRef = otherImpl.fk_PresentableModelRef;
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
            if (OnToString_PresentableModelDescriptor != null)
            {
                OnToString_PresentableModelDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresentableModelDescriptor> OnToString_PresentableModelDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresentableModelDescriptor != null) OnPreSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPreSave_PresentableModelDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresentableModelDescriptor != null) OnPostSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPostSave_PresentableModelDescriptor;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "PresentableModelRef":
                    fk_PresentableModelRef = id;
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
            BinarySerializer.ToStream((int)((PresentableModelDescriptor)this).DefaultVisualType, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._fk_PresentableModelRef, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStreamConverter(v => ((PresentableModelDescriptor)this).DefaultVisualType = (Kistl.App.GUI.VisualType)v, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._fk_PresentableModelRef, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            // TODO: Add XML Serializer here
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_PresentableModelRef, xml, "fk_PresentableModelRef", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}