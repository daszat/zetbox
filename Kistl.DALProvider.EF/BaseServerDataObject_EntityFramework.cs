using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

namespace Kistl.DALProvider.EF
{
    internal interface IEntityStateObject : IEntityWithKey
    {
        System.Data.EntityState EntityState { get; }
    }

    public abstract class BaseServerDataObject_EntityFramework
        : BaseServerDataObject, IEntityWithKey, IEntityWithRelationships, IEntityWithChangeTracker, IEntityStateObject
    {
        public override DataObjectState ObjectState
        {
            get
            {
                switch (EntityState)
                {
                    case System.Data.EntityState.Added:
                        return DataObjectState.New;
                    case System.Data.EntityState.Modified:
                        return DataObjectState.Modified;
                    case System.Data.EntityState.Unchanged:
                        return DataObjectState.Unmodified;
                    case System.Data.EntityState.Deleted:
                        return DataObjectState.Deleted;
                    default:
                        throw new InvalidOperationException("Invalid Entity Object State: " + EntityState.ToString());
                }
            }
        }

        protected override void SetModified()
        {
            // EF will do that for us
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
            NotifyPropertyChanging(property, property, oldValue, newValue);
        }

        public override void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            NotifyPropertyChanged(property, property, oldValue, newValue);
        }

        /// <summary>
        /// Special NotifyPropertyChanging implementation. Use this if the 
        /// underlying EF-Property name doesn't match the public property
        /// name.
        /// </summary>
        /// <param name="property">the official name of the property</param>
        /// <param name="efProperty">the EF name of the property</param>
        public void NotifyPropertyChanging(string property, string efProperty)
        {
            NotifyPropertyChanging(property, efProperty, null, null);
        }

        /// <summary>
        /// Special NotifyPropertyChanged implementation. Use this if the 
        /// underlying EF-Property name doesn't match the public property
        /// name.
        /// </summary>
        /// <param name="property">the official name of the property</param>
        /// <param name="efProperty">the EF name of the property</param>
        public void NotifyPropertyChanged(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanged(property, oldValue, newValue);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanged(efProperty);
            }
        }

        /// <summary>
        /// Special NotifyPropertyChanging implementation. Use this if the 
        /// underlying EF-Property name doesn't match the public property
        /// name.
        /// </summary>
        /// <param name="property">the official name of the property</param>
        /// <param name="efProperty">the EF name of the property</param>
        public void NotifyPropertyChanging(string property, string efProperty, object oldValue, object newValue)
        {
            base.NotifyPropertyChanging(property, oldValue, newValue);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(efProperty);
            }
        }
    }

    public abstract class BaseServerCollectionEntry_EntityFramework
        : BaseServerCollectionEntry, IEntityWithKey, IEntityWithRelationships, IEntityWithChangeTracker, IEntityStateObject
    {
        public override DataObjectState ObjectState
        {
            get
            {
                switch (EntityState)
                {
                    case System.Data.EntityState.Added:
                        return DataObjectState.New;
                    case System.Data.EntityState.Modified:
                        return DataObjectState.Modified;
                    case System.Data.EntityState.Unchanged:
                        return DataObjectState.Unmodified;
                    case System.Data.EntityState.Deleted:
                        return DataObjectState.Deleted;
                    default:
                        throw new InvalidOperationException("Invalid Entity Object State: " + EntityState.ToString());
                }
            }
        }

        protected override void SetModified()
        {
            // EF will do that for us
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
            base.NotifyPropertyChanging(property, oldValue, newValue);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(property);
            }
        }

        public override void NotifyPropertyChanged(string property, object oldValue, object newValue)
        {
            base.NotifyPropertyChanged(property, oldValue, newValue);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanged(property);
            }
        }

        // Case: 668
        public ObjectContext GetEFContext()
        {
            return _relationships.GetPrivatePropertyValue<ObjectContext>("Context");
        }

    }

    /// <summary>
    /// Implementing a change tracker is not required because Structs are attached to their 
    /// parent objects. Every change will notify the parent also.
    /// </summary>
    public abstract class BaseServerStructObject_EntityFramework
        : BaseServerStructObject
    {
        protected override void OnPropertyChanging(string property, object oldValue, object newValue)
        {
            //base.OnNotifyPropertyChanging(property);
            if (ParentObject != null)
                (ParentObject as BaseServerDataObject_EntityFramework)
                    .NotifyPropertyChanging(this.ParentProperty, this.ParentProperty + Kistl.API.Helper.ImplementationSuffix, null, null);
        }

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            //base.OnNotifyPropertyChanged(property);
            if (ParentObject != null)
                (ParentObject as BaseServerDataObject_EntityFramework)
                    .NotifyPropertyChanged(this.ParentProperty, this.ParentProperty + Kistl.API.Helper.ImplementationSuffix, null, null);
        }

    }
}
