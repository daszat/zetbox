using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public class MockImplementationTypeChecker
        : IImplementationTypeChecker
    {
        public bool IsImplementationType(Type t)
        {
            return true;
        }
    }

    public class MockImplementationType 
        : ImplementationType
    {
        public MockImplementationType(Type t, InterfaceType.Factory iftFactory)
            : base(t, iftFactory, new MockImplementationTypeChecker())
        {
        }

        public override InterfaceType ToInterfaceType()
        {
            throw new NotImplementedException();
        }
    }

    public class TestKistlContext : IKistlContext, IZBoxContextInternals
    {
        private readonly InterfaceType.Factory _iftFactory;
        public TestKistlContext(InterfaceType.Factory iftFactory)
        {
            _iftFactory = iftFactory;
        }

        #region IKistlContext Members

        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            obj.AttachToContext(this);
            return obj;
        }

        public void Detach(IPersistenceObject obj)
        {
            obj.DetachFromContext(this);
        }

        public void Delete(IPersistenceObject obj)
        {

        }

        public IQueryable<T> GetQuery<T>() where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }


        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent) where T : class, IRelationEntry
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> AttachedObjects
        {
            get { throw new NotImplementedException(); }
        }

        public int SubmitChanges()
        {
            throw new NotImplementedException();
        }

        public bool IsDisposed
        {
            get { throw new NotImplementedException(); }
        }

        public IDataObject Create(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T Create<T>() where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject CreateUnattached(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateUnattached<T>() where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public IRelationEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateRelationCollectionEntry<T>() where T : IRelationEntry
        {
            throw new NotImplementedException();
        }

        public IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            throw new NotImplementedException();
        }


        public ICompoundObject CreateCompoundObject(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public T CreateCompoundObject<T>() where T : ICompoundObject
        {
            throw new NotImplementedException();
        }

        public IDataObject Find(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            throw new NotImplementedException();
        }

        public T Find<T>(int ID) where T : class, IDataObject
        {
            throw new NotImplementedException();
        }

        public bool IsReadonly { get { return false; } }

        public void NotifyObjectCreated()
        {
            if (ObjectCreated != null) ObjectCreated(this, new GenericEventArgs<IPersistenceObject>());
        }

        public void NotifyObjectDeleted()
        {
            if (ObjectDeleted != null) ObjectDeleted(this, new GenericEventArgs<IPersistenceObject>());
        }

        /// <inheritdoc />
        public event GenericEventHandler<IKistlContext> Changed;
        protected virtual void OnChanged()
        {
            GenericEventHandler<IKistlContext> temp = Changed;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IKistlContext>() { Data = this });
            }
        }

        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        #endregion

        #region IDisposable Members
        public event GenericEventHandler<IReadOnlyKistlContext> Disposing;

        public void Dispose()
        {
            GenericEventHandler<IReadOnlyKistlContext> temp = Disposing;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IReadOnlyKistlContext>() { Data = this });
            }
        }

        #endregion

        #region IKistlContext Members


        public int CreateBlob(System.IO.Stream s, string filename, string mimetype)
        {
            throw new NotImplementedException();
        }

        public int CreateBlob(System.IO.FileInfo fi, string mimetype)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IReadOnlyKistlContext Members


        public System.IO.Stream GetStream(int ID)
        {
            throw new NotImplementedException();
        }

        public System.IO.FileInfo GetFileInfo(int ID)
        {
            throw new NotImplementedException();
        }

        public InterfaceType GetInterfaceType(Type t)
        {
            return _iftFactory(t);
        }

        public InterfaceType GetInterfaceType(string typeName)
        {
            return _iftFactory(Type.GetType(typeName + ",Kistl.Objects", true));
        }

        public InterfaceType GetInterfaceType(IPersistenceObject obj)
        {
            Type ifType;
            if (obj is TestCollectionEntry)
            {
                ifType = ((TestCollectionEntry)obj).GetImplementedInterface();
            }
            else if (obj is TestDataObjectImpl)
            {
                ifType = ((TestDataObjectImpl)obj).GetImplementedInterface();
            }
            else
            {
                throw new NotImplementedException("unable to get the implemented interfacetype of the given object");
            }
            return _iftFactory(ifType);
        }

        public InterfaceType GetInterfaceType(ICompoundObject obj)
        {
            throw new NotImplementedException();
        }

        public ImplementationType GetImplementationType(Type t)
        {
            return new MockImplementationType(t, _iftFactory);
        }

        public ImplementationType ToImplementationType(InterfaceType t)
        {
            return GetImplementationType(Type.GetType(t.Type.FullName + Kistl.API.Helper.ImplementationSuffix + "," + typeof(TestKistlContext).Assembly.FullName, true));
        }
        private IDictionary<object, object> _TransientState = null;
        /// <inheritdoc />
        public IDictionary<object, object> TransientState
        {
            get
            {
                if (_TransientState == null)
                {
                    _TransientState = new Dictionary<object, object>();
                }
                return _TransientState;
            }
        }
        #endregion

        /// <summary>
        /// Indicates that the ZBox Context has some modified, added or deleted items
        /// </summary>
        public bool IsModified { get; private set; }

        /// <summary>
        /// Is fires when <see cref="IsModified"/> was changed
        /// </summary>
        public event EventHandler IsModifiedChanged;

        #region IZBoxContextInternals Members
        int IZBoxContextInternals.IdentityID { get { return Helper.INVALIDID; } }

        void IZBoxContextInternals.SetModified(IPersistenceObject obj)
        {
            if (obj.ObjectState.In(DataObjectState.Deleted, DataObjectState.Modified, DataObjectState.New))
            {
                IsModified = true;
                EventHandler temp = IsModifiedChanged;
                if (temp != null)
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }
        string IZBoxContextInternals.StoreBlobStream(System.IO.Stream s, Guid exportGuid, DateTime timestamp, string filename)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IKistlContext Members


        public int GetSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
        }

        public int GetContinuousSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            // Allways allowed
        }

        #endregion


        public List<IDataObject> GetAll(InterfaceType t)
        {
            throw new NotImplementedException();
        }


        public void AttachAsNew(IPersistenceObject obj)
        {
            throw new NotImplementedException();
        }

        public AccessRights GetGroupAccessRights(InterfaceType ifType) { return AccessRights.Full; }
    }
}
