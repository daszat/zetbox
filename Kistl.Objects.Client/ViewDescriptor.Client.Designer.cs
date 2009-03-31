
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
        /// 
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef LayoutRef
        {
            get
            {
                if (fk_LayoutRef.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_LayoutRef.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_LayoutRef == null)
					return;
                else if (value != null && value.ID == _fk_LayoutRef)
					return;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("LayoutRef");
				           
				// next, set the local reference
                _fk_LayoutRef = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("LayoutRef");
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_LayoutRef
        {
            get
            {
                return _fk_LayoutRef;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_LayoutRef != value)
                {
                    NotifyPropertyChanging("LayoutRef");
                    _fk_LayoutRef = value;
                    NotifyPropertyChanged("LayoutRef");
                }
            }
        }
        private int? _fk_LayoutRef;

        /// <summary>
        /// 
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
                    NotifyPropertyChanging("Toolkit");
                    _Toolkit = value;
                    NotifyPropertyChanged("Toolkit");
                }
            }
        }
        private Kistl.App.GUI.Toolkit _Toolkit;

        /// <summary>
        /// 
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef ViewRef
        {
            get
            {
                if (fk_ViewRef.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_ViewRef.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_ViewRef == null)
					return;
                else if (value != null && value.ID == _fk_ViewRef)
					return;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ViewRef");
				           
				// next, set the local reference
                _fk_ViewRef = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ViewRef");
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ViewRef
        {
            get
            {
                return _fk_ViewRef;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ViewRef != value)
                {
                    NotifyPropertyChanging("ViewRef");
                    _fk_ViewRef = value;
                    NotifyPropertyChanged("ViewRef");
                }
            }
        }
        private int? _fk_ViewRef;

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
			this.fk_LayoutRef = otherImpl.fk_LayoutRef;
			this.fk_ViewRef = otherImpl.fk_ViewRef;
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
                case "LayoutRef":
                    fk_LayoutRef = id;
                    break;
                case "ViewRef":
                    fk_ViewRef = id;
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
            BinarySerializer.ToStream(this._fk_LayoutRef, binStream);
            BinarySerializer.ToStream((int)((ViewDescriptor)this).Toolkit, binStream);
            BinarySerializer.ToStream(this._fk_ViewRef, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_LayoutRef, binStream);
            BinarySerializer.FromStreamConverter(v => ((ViewDescriptor)this).Toolkit = (Kistl.App.GUI.Toolkit)v, binStream);
            BinarySerializer.FromStream(out this._fk_ViewRef, binStream);
        }

#endregion

    }


}