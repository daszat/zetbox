
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
    /// Metadefinition Object for a MethodInvocation on a Method of a DataType.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MethodInvocation")]
    public class MethodInvocation__Implementation__ : BaseClientDataObject, MethodInvocation
    {
    
		public MethodInvocation__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The Type implementing this invocation
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef Implementor
        {
            get
            {
                if (fk_Implementor.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_Implementor.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Implementor == null)
					return;
                else if (value != null && value.ID == _fk_Implementor)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Implementor;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Implementor", oldValue, value);
                
				// next, set the local reference
                _fk_Implementor = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Implementor", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Implementor
        {
            get
            {
                return _fk_Implementor;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Implementor != value)
                {
					var __oldValue = _fk_Implementor;
                    NotifyPropertyChanging("Implementor", __oldValue, value);
                    _fk_Implementor = value;
                    NotifyPropertyChanged("Implementor", __oldValue, value);
                }
            }
        }
        private int? _fk_Implementor;

        /// <summary>
        /// In dieser Objektklasse implementieren
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType InvokeOnObjectClass
        {
            get
            {
                if (fk_InvokeOnObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.DataType>(fk_InvokeOnObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_InvokeOnObjectClass == null)
					return;
                else if (value != null && value.ID == _fk_InvokeOnObjectClass)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = InvokeOnObjectClass;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("InvokeOnObjectClass", oldValue, value);
                
				// next, set the local reference
                _fk_InvokeOnObjectClass = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.MethodInvocations as OneNRelationCollection<Kistl.App.Base.MethodInvocation>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.MethodInvocations as OneNRelationCollection<Kistl.App.Base.MethodInvocation>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("InvokeOnObjectClass", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_InvokeOnObjectClass
        {
            get
            {
                return _fk_InvokeOnObjectClass;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_InvokeOnObjectClass != value)
                {
					var __oldValue = _fk_InvokeOnObjectClass;
                    NotifyPropertyChanging("InvokeOnObjectClass", __oldValue, value);
                    _fk_InvokeOnObjectClass = value;
                    NotifyPropertyChanged("InvokeOnObjectClass", __oldValue, value);
                }
            }
        }
        private int? _fk_InvokeOnObjectClass;

        /// <summary>
        /// Name des implementierenden Members
        /// </summary>
        // value type property
        public virtual string MemberName
        {
            get
            {
                return _MemberName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MemberName != value)
                {
					var __oldValue = _MemberName;
                    NotifyPropertyChanging("MemberName", __oldValue, value);
                    _MemberName = value;
                    NotifyPropertyChanged("MemberName", __oldValue, value);
                }
            }
        }
        private string _MemberName;

        /// <summary>
        /// Methode, die Aufgerufen wird
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
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.MethodInvokations as OneNRelationCollection<Kistl.App.Base.MethodInvocation>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.MethodInvokations as OneNRelationCollection<Kistl.App.Base.MethodInvocation>).AddWithoutSetParent(this);
                }
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
        /// Zugehörig zum Modul
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                if (fk_Module.HasValue)
                    return Context.Find<Kistl.App.Base.Module>(fk_Module.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Module == null)
					return;
                else if (value != null && value.ID == _fk_Module)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Module;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Module", oldValue, value);
                
				// next, set the local reference
                _fk_Module = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Module", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                return _fk_Module;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Module != value)
                {
					var __oldValue = _fk_Module;
                    NotifyPropertyChanging("Module", __oldValue, value);
                    _fk_Module = value;
                    NotifyPropertyChanged("Module", __oldValue, value);
                }
            }
        }
        private int? _fk_Module;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(MethodInvocation));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (MethodInvocation)obj;
			var otherImpl = (MethodInvocation__Implementation__)obj;
			var me = (MethodInvocation)this;

			me.MemberName = other.MemberName;
			this.fk_Implementor = otherImpl.fk_Implementor;
			this.fk_InvokeOnObjectClass = otherImpl.fk_InvokeOnObjectClass;
			this.fk_Method = otherImpl.fk_Method;
			this.fk_Module = otherImpl.fk_Module;
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
            if (OnToString_MethodInvocation != null)
            {
                OnToString_MethodInvocation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<MethodInvocation> OnToString_MethodInvocation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocation != null) OnPreSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPreSave_MethodInvocation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocation != null) OnPostSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPostSave_MethodInvocation;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Implementor":
                    fk_Implementor = id;
                    break;
                case "InvokeOnObjectClass":
                    fk_InvokeOnObjectClass = id;
                    break;
                case "Method":
                    fk_Method = id;
                    break;
                case "Module":
                    fk_Module = id;
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
            BinarySerializer.ToStream(this._fk_Implementor, binStream);
            BinarySerializer.ToStream(this._fk_InvokeOnObjectClass, binStream);
            BinarySerializer.ToStream(this._MemberName, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Implementor, binStream);
            BinarySerializer.FromStream(out this._fk_InvokeOnObjectClass, binStream);
            BinarySerializer.FromStream(out this._MemberName, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
        }

#endregion

    }


}