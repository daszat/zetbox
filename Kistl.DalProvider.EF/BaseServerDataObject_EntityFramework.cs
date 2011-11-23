using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.DalProvider.Ef
{
    internal interface IEntityStateObject : IEntityWithKey
    {
        System.Data.EntityState EntityState { get; }
    }

    internal interface IEntityFrameworkNotifyingObject
    {
        void NotifyPropertyChanged(string property, string efProperty, object oldValue, object newValue);
        void NotifyPropertyChanging(string property, string efProperty, object oldValue, object newValue);
    }

    public abstract class BaseServerDataObject_EntityFramework
        : BaseServerDataObject, IEntityWithKey, IEntityWithRelationships, IEntityWithChangeTracker, IEntityStateObject, IEntityFrameworkNotifyingObject
    {
        protected BaseServerDataObject_EntityFramework(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        #region IEntityWithKey Members
        private System.Data.EntityKey _entityKey = null;
        public System.Data.EntityKey EntityKey
        {
            get
            {
                return _entityKey;
            }
            set
            {
                if (_changeTracker != null)
                {
                    _changeTracker.EntityMemberChanging(StructuralObject.EntityKeyPropertyName);
                    _entityKey = value;
                    _changeTracker.EntityMemberChanged(StructuralObject.EntityKeyPropertyName);
                }
                else
                {
                    _entityKey = value;
                }
            }
        }
        #endregion

        #region IEntityWithRelationships Members
        private RelationshipManager _relationships = null;

        // Define a relationship manager for the class.
        public RelationshipManager RelationshipManager
        {
            get
            {
                if (null == _relationships)
                    _relationships = RelationshipManager.Create(this);
                return _relationships;
            }
        }

        #endregion

        #region IEntityChangeTracker
        IEntityChangeTracker _changeTracker = null;

        public virtual void SetChangeTracker(IEntityChangeTracker changeTracker)
        {
            _changeTracker = changeTracker;
            // Set CompoundObject change tracker
        }
        #endregion

        #region EntityState
        public System.Data.EntityState EntityState
        {
            get
            {
                if (_changeTracker == null) return System.Data.EntityState.Detached;
                return _changeTracker.EntityState;
            }
        }
        #endregion

        public override bool IsAttached
        {
            get
            {
                return this.EntityState != System.Data.EntityState.Detached;
            }
        }

        public override void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            if (property == "ObjectState")
            {
                base.NotifyPropertyChanging(property, oldValue, newValue);
            }
            else
            {
                NotifyPropertyChanging(property, property, oldValue, newValue);
            }
        }

        public override void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (property == "ObjectState")
            {
                base.NotifyPropertyChanged(property, oldValue, newValue);
            }
            else
            {
                NotifyPropertyChanged(property, property, oldValue, newValue);
            }
        }

        /// <summary>
        /// Special NotifyPropertyChanged implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        /// <param baseDir="oldValue">the old value of the property</param>
        /// <param baseDir="newValue">the new value of the property</param>
        public void NotifyPropertyChanged(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanged(property, oldValue, newValue);
            if (_changeTracker != null && !String.IsNullOrEmpty(efProperty))
            {
                try
                {
                    _changeTracker.EntityMemberChanged(efProperty);
                }
                catch (ArgumentException)
                {
                    // TODO: Sorry, but this public method is sometimes called
                    // for calculated properties. EF doesn't know about them
                    // so it throws this ArgumentException
                }
                catch (InvalidOperationException)
                {
                    // TODO: Sorry, but this public method is sometimes called
                    // for calculated properties. EF doesn't know about them
                    // so it throws this InvalidOperationException
                }
            }
        }

        /// <summary>
        /// Special NotifyPropertyChanging implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        /// <param baseDir="oldValue">the old value of the property</param>
        /// <param baseDir="newValue">the new value of the property</param>
        public void NotifyPropertyChanging(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanging(property, oldValue, newValue);
            if (_changeTracker != null && !String.IsNullOrEmpty(efProperty))
            {
                try
                {
                    _changeTracker.EntityMemberChanging(efProperty);
                }
                catch (ArgumentException)
                {
                    // TODO: Sorry, but this public method is sometimes called
                    // for calculated properties. EF doesn't know about them
                    // so it throws this ArgumentException
                }
                catch (InvalidOperationException)
                {
                    // TODO: Sorry, but this public method is sometimes called
                    // for calculated properties. EF doesn't know about them
                    // so it throws this InvalidOperationException
                }
            }
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            // EF keeps tabs for us, so we do nothing.
        }
    }

    public abstract class BaseServerCollectionEntry_EntityFramework
        : BaseServerCollectionEntry, IEntityWithKey, IEntityWithRelationships, IEntityWithChangeTracker, IEntityStateObject, IEntityFrameworkNotifyingObject
    {
        protected BaseServerCollectionEntry_EntityFramework(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        #region IEntityWithKey Members
        private System.Data.EntityKey _entityKey = null;
        public System.Data.EntityKey EntityKey
        {
            get
            {
                return _entityKey;
            }
            set
            {
                if (_changeTracker != null)
                {
                    _changeTracker.EntityMemberChanging(StructuralObject.EntityKeyPropertyName);
                    _entityKey = value;
                    _changeTracker.EntityMemberChanged(StructuralObject.EntityKeyPropertyName);
                }
                else
                {
                    _entityKey = value;
                }
            }
        }
        #endregion

        #region IEntityWithRelationships Members
        private RelationshipManager _relationships = null;

        // Define a relationship manager for the class.
        public RelationshipManager RelationshipManager
        {
            get
            {
                if (null == _relationships)
                    _relationships = RelationshipManager.Create(this);
                return _relationships;
            }
        }

        #endregion

        #region IEntityChangeTracker

        IEntityChangeTracker _changeTracker = null;

        public virtual void SetChangeTracker(IEntityChangeTracker changeTracker)
        {
            _changeTracker = changeTracker;
            // Set struct change tracker
        }

        #endregion

        #region IEntityStateObject

        public System.Data.EntityState EntityState
        {
            get
            {
                if (_changeTracker == null) return System.Data.EntityState.Detached;
                return _changeTracker.EntityState;
            }
        }

        #endregion

        public override bool IsAttached
        {
            get
            {
                return this.EntityState != System.Data.EntityState.Detached;
            }
        }

        public override void NotifyPropertyChanging(string property, object oldValue, object newValue)
        {
            if (property == "ObjectState")
            {
                base.NotifyPropertyChanging(property, oldValue, newValue);
            }
            else
            {
                NotifyPropertyChanging(property, property, oldValue, newValue);
            }
        }

        public override void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            if (property == "ObjectState")
            {
                base.NotifyPropertyChanged(property, oldValue, newValue);
            }
            else
            {
                NotifyPropertyChanged(property, property, oldValue, newValue);
            }
        }

        /// <summary>
        /// Special NotifyPropertyChanging implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        public void NotifyPropertyChanging(string property, string efProperty)
        {
            NotifyPropertyChanging(property, efProperty, null, null);
        }

        /// <summary>
        /// Special NotifyPropertyChanged implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        /// <param baseDir="oldValue">the old value of the property</param>
        /// <param baseDir="newValue">the new value of the property</param>
        public void NotifyPropertyChanged(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanged(property, oldValue, newValue);
            if (_changeTracker != null && !String.IsNullOrEmpty(efProperty))
            {
                _changeTracker.EntityMemberChanged(efProperty);
            }
        }

        /// <summary>
        /// Special NotifyPropertyChanging implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        /// <param baseDir="oldValue">the old value of the property</param>
        /// <param baseDir="newValue">the new value of the property</param>
        public void NotifyPropertyChanging(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanging(property, oldValue, newValue);
            if (_changeTracker != null && !String.IsNullOrEmpty(efProperty))
            {
                _changeTracker.EntityMemberChanging(efProperty);
            }
        }

        // Case: 668
        public ObjectContext GetEFContext()
        {
            return _relationships.GetPrivatePropertyValue<ObjectContext>("Context");
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            // EF keeps tabs for us, so we do nothing.
        }
    }

    /// <summary>
    /// Implementing a change tracker is not required because CompoundObjects are attached to their 
    /// parent objects. Every change will notify the parent also.
    /// </summary>
    public abstract class BaseServerCompoundObject_EntityFramework
        : BaseServerCompoundObject
    {
        protected BaseServerCompoundObject_EntityFramework(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanging(property, oldValue, newValue);
            if (ParentObject != null)
            {
                ((IEntityFrameworkNotifyingObject)ParentObject)
                    .NotifyPropertyChanging(this.ParentProperty + "." + property, this.ParentProperty + Kistl.API.Helper.ImplementationSuffix, null, null);
            }
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            if (ParentObject != null)
            {
                ((IEntityFrameworkNotifyingObject)ParentObject)
                    .NotifyPropertyChanged(this.ParentProperty + "." + property, this.ParentProperty + Kistl.API.Helper.ImplementationSuffix, null, null);
            }
        }

        /// <summary>
        /// Special NotifyPropertyChanged implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        /// <param baseDir="oldValue">the old value of the property</param>
        /// <param baseDir="newValue">the new value of the property</param>
        public void NotifyPropertyChanged(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanged(property, oldValue, newValue);
        }

        /// <summary>
        /// Special NotifyPropertyChanging implementation. Use this if the 
        /// underlying EF-Property baseDir doesn't match the public property
        /// baseDir.
        /// </summary>
        /// <param baseDir="property">the official baseDir of the property</param>
        /// <param baseDir="efProperty">the EF baseDir of the property</param>
        /// <param baseDir="oldValue">the old value of the property</param>
        /// <param baseDir="newValue">the new value of the property</param>
        public void NotifyPropertyChanging(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanging(property, oldValue, newValue);
        }
    }
}
