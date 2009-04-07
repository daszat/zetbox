
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
    /// Metadefinition Object for ObjectClasses.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectClass")]
    public class ObjectClass__Implementation__ : Kistl.App.Base.DataType__Implementation__, ObjectClass
    {
    
		public ObjectClass__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass BaseObjectClass
        {
            get
            {
                if (fk_BaseObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectClass>(fk_BaseObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_BaseObjectClass == null)
					return;
                else if (value != null && value.ID == _fk_BaseObjectClass)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = BaseObjectClass;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("BaseObjectClass", oldValue, value);
                
				// next, set the local reference
                _fk_BaseObjectClass = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.SubClasses as OneNRelationCollection<Kistl.App.Base.ObjectClass>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.SubClasses as OneNRelationCollection<Kistl.App.Base.ObjectClass>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("BaseObjectClass", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_BaseObjectClass
        {
            get
            {
                return _fk_BaseObjectClass;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_BaseObjectClass != value)
                {
					var __oldValue = _fk_BaseObjectClass;
                    NotifyPropertyChanging("BaseObjectClass", __oldValue, value);
                    _fk_BaseObjectClass = value;
                    NotifyPropertyChanged("BaseObjectClass", __oldValue, value);
                }
            }
        }
        private int? _fk_BaseObjectClass;

        /// <summary>
        /// The default model to use for the UI
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef DefaultModel
        {
            get
            {
                if (fk_DefaultModel.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_DefaultModel.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_DefaultModel == null)
					return;
                else if (value != null && value.ID == _fk_DefaultModel)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = DefaultModel;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("DefaultModel", oldValue, value);
                
				// next, set the local reference
                _fk_DefaultModel = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("DefaultModel", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DefaultModel
        {
            get
            {
                return _fk_DefaultModel;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DefaultModel != value)
                {
					var __oldValue = _fk_DefaultModel;
                    NotifyPropertyChanging("DefaultModel", __oldValue, value);
                    _fk_DefaultModel = value;
                    NotifyPropertyChanged("DefaultModel", __oldValue, value);
                }
            }
        }
        private int? _fk_DefaultModel;

        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>
        // collection reference property

		public ICollection<Kistl.App.Base.Interface> ImplementsInterfaces
		{
			get
			{
				if (_ImplementsInterfaces == null)
				{
					_ImplementsInterfaces 
						= new ClientCollectionBSideWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.Interface, ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__>(
							this, 
							Context.FetchRelation<ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__>(49, RelationEndRole.A, this));
				}
				return _ImplementsInterfaces;
			}
		}

		private ClientCollectionBSideWrapper<Kistl.App.Base.ObjectClass, Kistl.App.Base.Interface, ObjectClass_ImplementsInterfaces49CollectionEntry__Implementation__> _ImplementsInterfaces;

        /// <summary>
        /// if true then all Instances appear in FozenContext.
        /// </summary>
        // value type property
        public virtual bool IsFrozenObject
        {
            get
            {
                return _IsFrozenObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsFrozenObject != value)
                {
					var __oldValue = _IsFrozenObject;
                    NotifyPropertyChanging("IsFrozenObject", __oldValue, value);
                    _IsFrozenObject = value;
                    NotifyPropertyChanged("IsFrozenObject", __oldValue, value);
                }
            }
        }
        private bool _IsFrozenObject;

        /// <summary>
        /// Setting this to true marks the instances of this class as "simple." At first this will only mean that they'll be displayed inline.
        /// </summary>
        // value type property
        public virtual bool IsSimpleObject
        {
            get
            {
                return _IsSimpleObject;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsSimpleObject != value)
                {
					var __oldValue = _IsSimpleObject;
                    NotifyPropertyChanging("IsSimpleObject", __oldValue, value);
                    _IsSimpleObject = value;
                    NotifyPropertyChanged("IsSimpleObject", __oldValue, value);
                }
            }
        }
        private bool _IsSimpleObject;

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.ObjectClass> SubClasses
        {
            get
            {
                if (_SubClassesWrapper == null)
                {
                    List<Kistl.App.Base.ObjectClass> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.ObjectClass>(this, "SubClasses");
                    else
                        serverList = new List<Kistl.App.Base.ObjectClass>();
                        
                    _SubClassesWrapper = new OneNRelationCollection<Kistl.App.Base.ObjectClass>(
                        "BaseObjectClass",
                        this,
                        serverList);
                }
                return _SubClassesWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.ObjectClass> _SubClassesWrapper;

        /// <summary>
        /// Tabellenname in der Datenbank
        /// </summary>
        // value type property
        public virtual string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TableName != value)
                {
					var __oldValue = _TableName;
                    NotifyPropertyChanging("TableName", __oldValue, value);
                    _TableName = value;
                    NotifyPropertyChanged("TableName", __oldValue, value);
                }
            }
        }
        private string _TableName;

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_ObjectClass != null)
            {
                OnGetDataType_ObjectClass(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
		public event GetDataType_Handler<ObjectClass> OnGetDataType_ObjectClass;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_ObjectClass != null)
            {
                OnGetDataTypeString_ObjectClass(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<ObjectClass> OnGetDataTypeString_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual Kistl.App.Base.TypeRef GetDefaultModelRef() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.TypeRef>();
            if (OnGetDefaultModelRef_ObjectClass != null)
            {
                OnGetDefaultModelRef_ObjectClass(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ObjectClass.GetDefaultModelRef");
            }
            return e.Result;
        }
		public delegate void GetDefaultModelRef_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.TypeRef> ret);
		public event GetDefaultModelRef_Handler<ObjectClass> OnGetDefaultModelRef_ObjectClass;



        /// <summary>
        /// 
        /// </summary>

		public virtual IList<Kistl.App.Base.Method> GetInheritedMethods() 
        {
            var e = new MethodReturnEventArgs<IList<Kistl.App.Base.Method>>();
            if (OnGetInheritedMethods_ObjectClass != null)
            {
                OnGetInheritedMethods_ObjectClass(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ObjectClass.GetInheritedMethods");
            }
            return e.Result;
        }
		public delegate void GetInheritedMethods_Handler<T>(T obj, MethodReturnEventArgs<IList<Kistl.App.Base.Method>> ret);
		public event GetInheritedMethods_Handler<ObjectClass> OnGetInheritedMethods_ObjectClass;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectClass));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ObjectClass)obj;
			var otherImpl = (ObjectClass__Implementation__)obj;
			var me = (ObjectClass)this;

			me.IsFrozenObject = other.IsFrozenObject;
			me.IsSimpleObject = other.IsSimpleObject;
			me.TableName = other.TableName;
			this.fk_BaseObjectClass = otherImpl.fk_BaseObjectClass;
			this.fk_DefaultModel = otherImpl.fk_DefaultModel;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectClass != null)
            {
                OnToString_ObjectClass(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectClass> OnToString_ObjectClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectClass != null) OnPreSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPreSave_ObjectClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectClass != null) OnPostSave_ObjectClass(this);
        }
        public event ObjectEventHandler<ObjectClass> OnPostSave_ObjectClass;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "BaseObjectClass":
                    fk_BaseObjectClass = id;
                    break;
                case "DefaultModel":
                    fk_DefaultModel = id;
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
            BinarySerializer.ToStream(this._fk_BaseObjectClass, binStream);
            BinarySerializer.ToStream(this._fk_DefaultModel, binStream);
            BinarySerializer.ToStream(this._IsFrozenObject, binStream);
            BinarySerializer.ToStream(this._IsSimpleObject, binStream);
            BinarySerializer.ToStream(this._TableName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_BaseObjectClass, binStream);
            BinarySerializer.FromStream(out this._fk_DefaultModel, binStream);
            BinarySerializer.FromStream(out this._IsFrozenObject, binStream);
            BinarySerializer.FromStream(out this._IsSimpleObject, binStream);
            BinarySerializer.FromStream(out this._TableName, binStream);
        }

#endregion

    }


}