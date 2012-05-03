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
using Kistl.API.Utils;

namespace Kistl.DalProvider.Ef
{
    internal interface IEntityStateObject : IEntityWithKey
    {
        System.Data.EntityState EntityState { get; }
    }

    internal interface IEntityFrameworkNotifyingObject
    {
        void ReportEfPropertyChanging(string efProperty);
        void ReportEfPropertyChanged(string efProperty);
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

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            // EF keeps tabs for us, so we do nothing.
        }

        #region IEntityFrameworkNotifyingObject
        public void ReportEfPropertyChanging(string efProperty)
        {
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(efProperty);
            }
        }

        public void ReportEfPropertyChanged(string efProperty)
        {
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanged(efProperty);
            }
        }
        #endregion
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

        // Case: 668
        public ObjectContext GetEFContext()
        {
            return _relationships.GetPrivatePropertyValue<ObjectContext>("Context");
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            // EF keeps tabs for us, so we do nothing.
        }

        #region IEntityFrameworkNotifyingObject
        public void ReportEfPropertyChanging(string efProperty)
        {
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(efProperty);
            }
        }

        public void ReportEfPropertyChanged(string efProperty)
        {
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanged(efProperty);
            }
        }
        #endregion
    }

    /// <summary>
    /// Implementing a change tracker is not required because CompoundObjects are attached to their 
    /// parent objects. Every change will notify the parent also.
    /// </summary>
    public abstract class BaseServerCompoundObject_EntityFramework
        : BaseServerCompoundObject, IEntityFrameworkNotifyingObject
    {
        protected BaseServerCompoundObject_EntityFramework(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        #region IEntityFrameworkNotifyingObject
        public void ReportEfPropertyChanging(string efProperty)
        {
            if (ParentObject != null)
            {
                ((IEntityFrameworkNotifyingObject)ParentObject)
                    .ReportEfPropertyChanging(this.ParentProperty + Kistl.API.Helper.ImplementationSuffix);
            }
        }

        public void ReportEfPropertyChanged(string efProperty)
        {
            if (ParentObject != null)
            {
                ((IEntityFrameworkNotifyingObject)ParentObject)
                    .ReportEfPropertyChanged(this.ParentProperty + Kistl.API.Helper.ImplementationSuffix);
            }
        }
        #endregion
    }
}
