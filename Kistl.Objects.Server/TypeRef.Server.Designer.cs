
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="TypeRef")]
    [System.Diagnostics.DebuggerDisplay("TypeRef")]
    public class TypeRef__Implementation__ : BaseServerDataObject_EntityFramework, TypeRef
    {
    
		public TypeRef__Implementation__()
		{
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// The assembly containing the referenced Type.
        /// </summary>
    /*
    Relation: FK_TypeRef_Assembly_TypeRef_65
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return Assembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Assembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Assembly != null)
                {
                    _fk_Assembly = Assembly.ID;
                }
                return _fk_Assembly;
            }
            set
            {
                _fk_Assembly = value;
            }
        }
        private int? _fk_Assembly;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_Assembly_TypeRef_65", "Assembly")]
        public Kistl.App.Base.Assembly__Implementation__ Assembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_TypeRef_Assembly_TypeRef_65",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_TypeRef_Assembly_TypeRef_65",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
    /*
    Relation: FK_TypeRef_TypeRef_TypeRef_66
    A: ZeroOrMore TypeRef as TypeRef
    B: ZeroOrMore TypeRef as GenericArguments
    Preferred Storage: Separate
    */
        // collection reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Base.TypeRef> GenericArguments
        {
            get
            {
                if (_GenericArgumentsWrapper == null)
                {
                    _GenericArgumentsWrapper = new EntityRelationBSideListWrapper<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef_GenericArguments66CollectionEntry__Implementation__>(
                            this,
                            GenericArguments__Implementation__);
                }
                return _GenericArgumentsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_TypeRef_TypeRef_TypeRef_66", "CollectionEntry")]
        public EntityCollection<Kistl.App.Base.TypeRef_GenericArguments66CollectionEntry__Implementation__> GenericArguments__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.TypeRef_GenericArguments66CollectionEntry__Implementation__>(
                        "Model.FK_TypeRef_TypeRef_TypeRef_66",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityRelationBSideListWrapper<Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef, Kistl.App.Base.TypeRef_GenericArguments66CollectionEntry__Implementation__> _GenericArgumentsWrapper;
        

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



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Assembly.HasValue)
				Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.Find<Kistl.App.Base.Assembly>(_fk_Assembly.Value);
			else
				Assembly__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_Assembly, binStream);
            BinarySerializer.ToStream(this._FullName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_Assembly;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Assembly = tmp;
            }
            BinarySerializer.FromStream(out this._FullName, binStream);
        }

#endregion

    }


}