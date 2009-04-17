
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
    /// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TypeRef")]
    public class TypeRef__Implementation__ : BaseClientDataObject, TypeRef
    {
    
		public TypeRef__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The assembly containing the referenced Type.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                if (fk_Assembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_Assembly.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Assembly == null)
					return;
                else if (value != null && value.ID == _fk_Assembly)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Assembly;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Assembly", oldValue, value);
                
				// next, set the local reference
                _fk_Assembly = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Assembly", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Assembly
        {
            get
            {
                return _fk_Assembly;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Assembly != value)
                {
					var __oldValue = _fk_Assembly;
                    NotifyPropertyChanging("Assembly", __oldValue, value);
                    _fk_Assembly = value;
                    NotifyPropertyChanged("Assembly", __oldValue, value);
                }
            }
        }
        private int? _fk_Assembly;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_FullName != value)
                {
					var __oldValue = _FullName;
                    NotifyPropertyChanging("FullName", __oldValue, value);
                    _FullName = value;
                    NotifyPropertyChanged("FullName", __oldValue, value);
                }
            }
        }
        private string _FullName;

        /// <summary>
        /// list of type arguments
        /// </summary>
        // collection reference property

		public IList<Kistl.App.Base.TypeRef> GenericArguments
		{
			get
			{
				if (_GenericArguments == null)
				{
					Context.FetchRelation<TypeRef_GenericArguments66CollectionEntry__Implementation__>(66, RelationEndRole.A, this);
					_GenericArguments 
						= new ClientRelationBSideListWrapper<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef, TypeRef_GenericArguments66CollectionEntry__Implementation__>(
							this, 
							new RelationshipFilterASideCollection<TypeRef_GenericArguments66CollectionEntry__Implementation__>(this.Context, this));
				}
				return _GenericArguments;
			}
		}

		private ClientRelationBSideListWrapper<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef, TypeRef_GenericArguments66CollectionEntry__Implementation__> _GenericArguments;

        /// <summary>
        /// The TypeRef of the BaseClass of the referenced Type
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef Parent
        {
            get
            {
                if (fk_Parent.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_Parent.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Parent == null)
					return;
                else if (value != null && value.ID == _fk_Parent)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Parent;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Parent", oldValue, value);
                
				// next, set the local reference
                _fk_Parent = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Parent", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Parent
        {
            get
            {
                return _fk_Parent;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Parent != value)
                {
					var __oldValue = _fk_Parent;
                    NotifyPropertyChanging("Parent", __oldValue, value);
                    _fk_Parent = value;
                    NotifyPropertyChanged("Parent", __oldValue, value);
                }
            }
        }
        private int? _fk_Parent;

        /// <summary>
        /// get the referenced <see cref="System.Type"/>
        /// </summary>

		public virtual System.Type AsType(System.Boolean throwOnError) 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnAsType_TypeRef != null)
            {
                OnAsType_TypeRef(this, e, throwOnError);
            }
            else
            {
                throw new NotImplementedException("No handler registered on TypeRef.AsType");
            }
            return e.Result;
        }
		public delegate void AsType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret, System.Boolean throwOnError);
		public event AsType_Handler<TypeRef> OnAsType_TypeRef;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TypeRef));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (TypeRef)obj;
			var otherImpl = (TypeRef__Implementation__)obj;
			var me = (TypeRef)this;

			me.FullName = other.FullName;
			this.fk_Assembly = otherImpl.fk_Assembly;
			this.fk_Parent = otherImpl.fk_Parent;
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
            if (OnToString_TypeRef != null)
            {
                OnToString_TypeRef(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TypeRef> OnToString_TypeRef;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TypeRef != null) OnPreSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPreSave_TypeRef;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TypeRef != null) OnPostSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPostSave_TypeRef;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Assembly":
                    fk_Assembly = id;
                    break;
                case "Parent":
                    fk_Parent = id;
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
            BinarySerializer.ToStream(this._fk_Assembly, binStream);
            BinarySerializer.ToStream(this._FullName, binStream);
            BinarySerializer.ToStream(this._fk_Parent, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._FullName, binStream);
            BinarySerializer.FromStream(out this._fk_Parent, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_Assembly, xml, "fk_Assembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._FullName, xml, "FullName", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._fk_Parent, xml, "fk_Parent", "http://dasz.at/Kistl");
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