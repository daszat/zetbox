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

        public override void NotifyPropertyChanging(string property)
        {
            base.NotifyPropertyChanging(property);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(property);
            }
        }

        public override void NotifyPropertyChanged(string property)
        {
            base.NotifyPropertyChanged(property);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanged(property);
            }
        }
    }

    public abstract class BaseServerCollectionEntry_EntityFramework
        : BaseServerCollectionEntry, IEntityWithKey, IEntityWithRelationships, IEntityWithChangeTracker, IEntityStateObject
    {
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

        public override void NotifyPropertyChanging(string property)
        {
            base.NotifyPropertyChanging(property);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(property);
            }
        }

        public override void NotifyPropertyChanged(string property)
        {
            base.NotifyPropertyChanged(property);
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
    /// See BaseServerStructObject.AttachToObject and BaseServerStructObject.NotifyPropertyChanged
    /// </summary>
    public abstract class BaseServerStructObject_EntityFramework : BaseServerStructObject
    {
    }
}
