
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
    [System.Diagnostics.DebuggerDisplay("ViewDescriptor")]
    public class ViewDescriptor__Implementation__ : BaseClientDataObject, ViewDescriptor
    {
    
		public ViewDescriptor__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The control implementing this View
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef ControlRef
        {
            get
            {
                if (fk_ControlRef.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_ControlRef.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_ControlRef == null)
					return;
                else if (value != null && value.ID == _fk_ControlRef)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = ControlRef;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ControlRef", oldValue, value);
                
				// next, set the local reference
                _fk_ControlRef = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ControlRef", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ControlRef
        {
            get
            {
                return _fk_ControlRef;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ControlRef != value)
                {
					var __oldValue = _fk_ControlRef;
                    NotifyPropertyChanging("ControlRef", __oldValue, value);
                    _fk_ControlRef = value;
                    NotifyPropertyChanged("ControlRef", __oldValue, value);
                }
            }
        }
        private int? _fk_ControlRef;

        /// <summary>
        /// The PresentableModel usable by this View
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.PresentableModelDescriptor PresentedModelDescriptor
        {
            get
            {
                if (fk_PresentedModelDescriptor.HasValue)
                    return Context.Find<Kistl.App.GUI.PresentableModelDescriptor>(fk_PresentedModelDescriptor.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_PresentedModelDescriptor == null)
					return;
                else if (value != null && value.ID == _fk_PresentedModelDescriptor)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = PresentedModelDescriptor;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("PresentedModelDescriptor", oldValue, value);
                
				// next, set the local reference
                _fk_PresentedModelDescriptor = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("PresentedModelDescriptor", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_PresentedModelDescriptor
        {
            get
            {
                return _fk_PresentedModelDescriptor;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_PresentedModelDescriptor != value)
                {
					var __oldValue = _fk_PresentedModelDescriptor;
                    NotifyPropertyChanging("PresentedModelDescriptor", __oldValue, value);
                    _fk_PresentedModelDescriptor = value;
                    NotifyPropertyChanged("PresentedModelDescriptor", __oldValue, value);
                }
            }
        }
        private int? _fk_PresentedModelDescriptor;

        /// <summary>
        /// Which toolkit provides this View
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.Toolkit Toolkit
        {
            get
            {
                return _Toolkit;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Toolkit != value)
                {
					var __oldValue = _Toolkit;
                    NotifyPropertyChanging("Toolkit", __oldValue, value);
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Toolkit;

        /// <summary>
        /// The visual type of this View
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType VisualType
        {
            get
            {
                return _VisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_VisualType != value)
                {
					var __oldValue = _VisualType;
                    NotifyPropertyChanging("VisualType", __oldValue, value);
                    _VisualType = value;
                    NotifyPropertyChanged("VisualType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _VisualType;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ViewDescriptor));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ViewDescriptor)obj;
			var otherImpl = (ViewDescriptor__Implementation__)obj;
			var me = (ViewDescriptor)this;

			me.Toolkit = other.Toolkit;
			me.VisualType = other.VisualType;
			this.fk_ControlRef = otherImpl.fk_ControlRef;
			this.fk_PresentedModelDescriptor = otherImpl.fk_PresentedModelDescriptor;
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
            if (OnToString_ViewDescriptor != null)
            {
                OnToString_ViewDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ViewDescriptor> OnToString_ViewDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ViewDescriptor != null) OnPreSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPreSave_ViewDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ViewDescriptor != null) OnPostSave_ViewDescriptor(this);
        }
        public event ObjectEventHandler<ViewDescriptor> OnPostSave_ViewDescriptor;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "ControlRef":
                    fk_ControlRef = id;
                    break;
                case "PresentedModelDescriptor":
                    fk_PresentedModelDescriptor = id;
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
            BinarySerializer.ToStream(this._fk_ControlRef, binStream);
            BinarySerializer.ToStream(this._fk_PresentedModelDescriptor, binStream);
            BinarySerializer.ToStream((int)((ViewDescriptor)this).Toolkit, binStream);
            BinarySerializer.ToStream((int)((ViewDescriptor)this).VisualType, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ControlRef, binStream);
            BinarySerializer.FromStream(out this._fk_PresentedModelDescriptor, binStream);
            BinarySerializer.FromStreamConverter(v => ((ViewDescriptor)this).Toolkit = (Kistl.App.GUI.Toolkit)v, binStream);
            BinarySerializer.FromStreamConverter(v => ((ViewDescriptor)this).VisualType = (Kistl.App.GUI.VisualType)v, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_ControlRef, xml, "fk_ControlRef", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_PresentedModelRef, xml, "fk_PresentedModelRef", "http://dasz.at/Kistl");
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}