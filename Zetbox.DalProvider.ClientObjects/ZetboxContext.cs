// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.DalProvider.Base;

    public interface IZetboxClientContextInternals
    {
        Task<object> InvokeServerMethod<T>(T obj, string name, Type retValType, IEnumerable<Type> parameterTypes, params object[] parameter) where T : class, IDataObject;
    }

    /// <summary>
    /// TODO: Remove that class when Case #1763 is solved
    /// </summary>
    public static class ZetboxClientContextExtensions
    {
        public static IZetboxClientContextInternals ClientInternals(this IZetboxContext ctx)
        {
            return (IZetboxClientContextInternals)ctx;
        }
    }

    /// <summary>
    /// Linq to Zetbox Context Implementation
    /// </summary>
    internal class ZetboxContextImpl
        : IDebuggingZetboxContext, IZetboxContextInternals, IZetboxClientContextInternals, IDisposable
    {
        private readonly static object _lock = new object();
        private readonly ZetboxConfig config;
        private readonly IProxy proxy;
        private readonly string _ClientImplementationAssembly;
        private readonly Func<IFrozenContext> _lazyCtx;
        private readonly InterfaceType.Factory _iftFactory;
        private readonly ClientImplementationType.ClientFactory _implTypeFactory;
        private readonly UnattachedObjectFactory _unattachedObjectFactory;
        private readonly ContextIsolationLevel _clientIsolationLevel;
        private readonly IPerfCounter _perfCounter;
        private readonly long _startTime;
        private readonly IPrincipalResolver _identityResolver;
        private readonly IEnumerable<IZetboxContextEventListener> _eventListeners;

        /// <summary>
        /// List of Objects (IDataObject and ICollectionEntry) in this Context.
        /// </summary>
        private ContextCache<int> _objects;

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        public ZetboxContextImpl(ContextIsolationLevel il, ZetboxConfig config, IProxy proxy, string clientImplementationAssembly, Func<IFrozenContext> lazyCtx, InterfaceType.Factory iftFactory, ClientImplementationType.ClientFactory implTypeFactory, UnattachedObjectFactory unattachedObjectFactory, IPerfCounter perfCounter, IPrincipalResolver identityResolver, IEnumerable<IZetboxContextEventListener> eventListeners)
        {
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");
            this._perfCounter = perfCounter;
            _startTime = _perfCounter.IncrementZetboxContext();

            this._clientIsolationLevel = il;
            this.config = config;
            this.proxy = proxy;
            this._ClientImplementationAssembly = clientImplementationAssembly;
            this._objects = new ContextCache<int>(this, item => item.ID);
            this._lazyCtx = lazyCtx;
            this._iftFactory = iftFactory;
            this._implTypeFactory = implTypeFactory;
            this._unattachedObjectFactory = unattachedObjectFactory;
            this._identityResolver = identityResolver;
            this._eventListeners = eventListeners;

            CreatedAt = new StackTrace(true);
            ZetboxContextDebuggerSingleton.Created(this);
        }

        public event GenericEventHandler<IReadOnlyZetboxContext> Disposing;
        public event GenericEventHandler<IReadOnlyZetboxContext> Disposed;

        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Clarifies intent of variable")]
        private bool disposed = false;
        /// <summary>
        /// Dispose this Context.
        /// TODO: use correct Dispose implementation pattern
        /// </summary>
        public void Dispose()
        {
            var temp = Disposing;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IReadOnlyZetboxContext>() { Data = this });
            }

            lock (_lock)
            {
                if (!disposed)
                {
                    proxy.Dispose();
                    DisposedAt = new StackTrace(true);
                    _perfCounter.DecrementZetboxContext(_startTime);
                }
                disposed = true;
            }

            temp = Disposed;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IReadOnlyZetboxContext>() { Data = this });
            }
            ZetboxContextEventListenerHelper.OnDisposed(_eventListeners, this);
        }

        public bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        public bool IsReadonly
        {
            get
            {
                return _clientIsolationLevel != ContextIsolationLevel.PreferContextCache;
            }
        }

        /// <summary>
        /// Allways full, managed on the server side
        /// </summary>
        /// <param name="ifType"></param>
        /// <returns></returns>
        public AccessRights GetGroupAccessRights(InterfaceType ifType) { return AccessRights.Full; }

        /// <summary>
        /// Throws an Exception when this Context has been disposed
        /// </summary>
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ZetboxContextDisposedException();
            }
        }


        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            if (ID == Helper.INVALIDID)
                throw new ArgumentException("ID cannot be invalid", "ID");
            return _objects.Lookup(type, ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerDisplay("Count = {_objects.Count}")]
        public IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                return _objects;
            }
        }

        /// <summary>
        /// Returns a Query by System.Type
        /// </summary>
        /// <param name="ifType">System.Type</param>
        /// <returns>IQueryable</returns>
        public IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            CheckDisposed();
            return new ZetboxContextQuery<IDataObject>(this, ifType, proxy, _perfCounter);
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public IQueryable<T> GetQuery<T>() where T : class, IDataObject
        {
            CheckDisposed();
            return new ZetboxContextQuery<T>(this, _iftFactory(typeof(T)), proxy, _perfCounter);
        }

        /// <summary>
        /// Returns a PersistenceObject Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject
        {
            CheckDisposed();
            return new ZetboxContextQuery<T>(this, _iftFactory(typeof(T)), proxy, _perfCounter);
        }

        /// <summary>
        /// Returns a PersistenceObject Query by InterfaceType
        /// </summary>
        /// <param name="ifType">the interface to look for</param>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            CheckDisposed();
            return new ZetboxContextQuery<IPersistenceObject>(this, ifType, proxy, _perfCounter);
        }

        /// <summary>
        /// Returns the List referenced by the given Name.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public async Task<List<T>> GetListOfAsync<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            CheckDisposed();
            if (obj.CurrentAccessRights.HasNoRights()) return new List<T>();

            ZetboxContextQuery<T> query = new ZetboxContextQuery<T>(this, GetInterfaceType(obj), proxy, _perfCounter);
            var task = await ((ZetboxContextProvider)query.Provider).GetListOfCallAsync(obj.ID, propertyName);
            if (IsDisposed) return new List<T>();
            return task.Cast<T>().ToList();
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            var t = GetListOfAsync<T>(obj, propertyName);
            return t.Result;
        }

        public async Task<IList<T>> FetchRelationAsync<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry
        {
            var parentId = container.ID;
            var parentIfType = GetInterfaceType(container);
            var serverList = await proxy.FetchRelation<T>(relationId, role, parentId, parentIfType);

            if (IsDisposed) return new List<T>();
            RecordNotifications();
            try
            {
                serverList.Item2.Cast<IPersistenceObject>().ForEach(obj => this.AttachRespectingIsolationLevel(obj));
                return serverList.Item1.Select(obj => (T)this.AttachRespectingIsolationLevel(obj)).ToList();
            }
            finally
            {
                PlaybackNotifications();
            }
        }

        public IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry
        {
            var t = FetchRelationAsync<T>(relationId, role, container);
            return t.Result;
        }

        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public T Create<T>() where T : class, IDataObject
        {
            return (T)Create(_iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new IDataObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="ifType">System.Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        public IDataObject Create(InterfaceType ifType)
        {
            return (IDataObject)CreateInternal(ifType);
        }

        /// <inheritdoc />
        public IPersistenceObject CreateUnattached(InterfaceType ifType)
        {
            return (IPersistenceObject)CreateUnattachedInstance(ifType);
        }

        /// <inheritdoc />
        public T CreateUnattached<T>() where T : class, IPersistenceObject
        {
            return (T)CreateUnattachedInstance(_iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new IRelationEntry by Type
        /// </summary>
        /// <typeparam name="T">Type of the new IRelationEntry</typeparam>
        /// <returns>A new IRelationEntry</returns>
        public T CreateRelationCollectionEntry<T>() where T : IRelationEntry
        {
            return (T)CreateRelationCollectionEntry(_iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new IRelationEntry.
        /// </summary>
        /// <returns>A new IRelationEntry</returns>
        public IRelationEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            return (IRelationEntry)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates a new IValueCollectionEntry by Type
        /// </summary>
        /// <typeparam name="T">Type of the new ICollectionEntry</typeparam>
        /// <returns>A new IValueCollectionEntry</returns>
        public T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            return (T)CreateValueCollectionEntry(_iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new IValueCollectionEntry.
        /// </summary>
        /// <returns>A new IValueCollectionEntry</returns>
        public IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            return (IValueCollectionEntry)CreateInternal(ifType);
        }

        private object CreateUnattachedInstance(InterfaceType ifType)
        {
            return _unattachedObjectFactory(ifType);
        }

        private IPersistenceObject CreateInternal(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType.Type == typeof(Zetbox.App.Base.Blob))
                throw new InvalidOperationException("Creating a Blob is not supported. Use CreateBlob() instead");

            IPersistenceObject obj = (IPersistenceObject)CreateUnattachedInstance(ifType);
            Attach(obj);
            IsModified = true;

            OnObjectCreated(obj);
            obj.NotifyCreated();
            return obj;
        }

        /// <summary>
        /// Creates a new CompoundObject by Type
        /// </summary>
        /// <param name="ifType">Type of the new CompoundObject</param>
        /// <returns>A new CompoundObject</returns>
        public ICompoundObject CreateCompoundObject(InterfaceType ifType)
        {
            CheckDisposed();
            ICompoundObject obj = (ICompoundObject)CreateUnattachedInstance(ifType);
            return obj;
        }
        /// <summary>
        /// Creates a new CompoundObject.
        /// </summary>
        /// <typeparam name="T">Type of the new CompoundObject</typeparam>
        /// <returns>A new CompoundObject</returns>
        public T CreateCompoundObject<T>() where T : ICompoundObject
        {
            return (T)CreateCompoundObject(_iftFactory(typeof(T)));
        }

        internal IPersistenceObject AttachRespectingIsolationLevel(IPersistenceObject obj)
        {
            var localobj = this.Attach(obj);

            if (_clientIsolationLevel == ContextIsolationLevel.MergeQueryData && obj != localobj)
            {
                ((BasePersistenceObject)localobj).RecordNotifications();
                localobj.ApplyChangesFrom(obj);
                // reset ObjectState to new truth
                ((IClientObject)localobj).SetUnmodified();
            }

            return localobj;
        }

        private int _notificationsCounter = 0;
        internal void RecordNotifications()
        {
            AttachedObjects.ForEach(obj => ((BasePersistenceObject)obj).RecordNotifications());
            _notificationsCounter += 1;
        }

        internal void PlaybackNotifications()
        {
            _notificationsCounter -= 1;

            if (_notificationsCounter == 0)
                AttachedObjects.ForEach(obj => ((BasePersistenceObject)obj).PlaybackNotifications());
        }

        /// <summary>
        /// Attach an IPersistenceObject. This Method checks, if the Object is already in that Context. 
        /// If so, it returns the Object in that Context.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        /// <returns>The Object in already Context or obj if not</returns>
        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null)
                throw new ArgumentNullException("obj");

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                checked
                {
                    ((PersistenceObjectBaseImpl)obj).ID = --_newIDCounter;
                }
            }
            else
            {
                // Check if Object is already in this Context
                var attachedObj = ContainsObject(GetInterfaceType(obj), obj.ID);
                if (attachedObj != null)
                {
                    // already attached, nothing to do
                    return attachedObj;
                }

                // Check ID <-> newIDCounter
                if (obj.ID < _newIDCounter)
                {
                    _newIDCounter = obj.ID;
                }
            }

            // Attach & set Objectstate to Unmodified
            _objects.Add(obj);
            ((IClientObject)obj).SetUnmodified();
            obj.AttachToContext(this, _lazyCtx);

            OnChanged();

            return obj;
        }

        /// <summary>
        /// Attach an IPersistenceObject. This Method checks, if the Object is already in that Context. 
        /// If so, it returns the Object in that Context.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        /// <returns>The Object in already Context or obj if not</returns>
        public void AttachAsNew(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null)
                throw new ArgumentNullException("obj");

            // Handle created Objects
            if (obj.ID == Helper.INVALIDID)
            {
                checked
                {
                    ((PersistenceObjectBaseImpl)obj).ID = --_newIDCounter;
                }
            }
            else if (obj.ID < _newIDCounter)// Check ID <-> newIDCounter
            {
                _newIDCounter = obj.ID;
            }

            // Attach & set Objectstate to Unmodified
            _objects.Add(obj);
            ((IClientObject)obj).SetNew();
            obj.AttachToContext(this, _lazyCtx);

            OnChanged();
        }
        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        public void Detach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (!_objects.Contains(obj))
                throw new InvalidOperationException("This Object does not belong to this context");

            _objects.Remove(obj);
            obj.DetachFromContext(this);
            OnChanged();
        }

        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IPersistenceObject</param>
        public void Delete(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.Context != this)
                throw new InvalidOperationException("The Object does not belong to the current Context");

            ((IClientObject)obj).SetDeleted();

            IsModified = true;
            obj.NotifyDeleting();
            OnObjectDeleted(obj);
        }

        private abstract class ExchangeObjectsHandler
        {
            public async Task<int> ExchangeObjects(ZetboxContextImpl ctx)
            {
                // TODO: ExchangeObjectsHandler is also used for method calls 
                NotifyPreSave(ctx);

                var objectsToSubmit = new List<IPersistenceObject>();
                var objectsToAdd = new List<IPersistenceObject>();
                var objectsToDetach = new List<IPersistenceObject>();

                // Added Objects
                foreach (var obj in ctx._objects.OfType<PersistenceObjectBaseImpl>()
                    .Where(o => o.ObjectState == DataObjectState.New))
                {
                    objectsToSubmit.Add(obj);
                    objectsToAdd.Add(obj);
                }
                // Changed objects
                foreach (var obj in ctx._objects.OfType<PersistenceObjectBaseImpl>()
                    .Where(o => o.ObjectState == DataObjectState.Modified))
                {
                    if (!obj.CurrentAccessRights.HasWriteRights()) throw new System.Security.SecurityException(string.Format("Inconsistent security/rights state detected while changing {0}({1})", ctx.GetInterfaceType(obj).Type.FullName, obj.ID));
                    objectsToSubmit.Add(obj);
                }
                // Deleted Objects
                foreach (var obj in ctx._objects.OfType<PersistenceObjectBaseImpl>()
                    .Where(o => o.ObjectState == DataObjectState.Deleted))
                {
                    if (!obj.CurrentAccessRights.HasDeleteRights()) throw new System.Security.SecurityException(string.Format("Inconsistent security/rights state detected while deleting {0}({1})", ctx.GetInterfaceType(obj).Type.FullName, obj.ID));
                    // Submit only persisted objects
                    if (Helper.IsPersistedObject(obj))
                    {
                        objectsToSubmit.Add(obj);
                    }
                    objectsToDetach.Add(obj);
                }

                var notificationRequests = ctx.AttachedObjects
                        .ToLookup(o => ctx.GetInterfaceType(o))
                        .Select(g => new ObjectNotificationRequest() { Type = g.Key.ToSerializableType(), IDs = g.Select(o => o.ID).ToArray() });

                // Submit to server
                var objectsFromServer = await this.ExecuteServerCall(ctx, objectsToSubmit, notificationRequests);

                ctx.RecordNotifications();
                try
                {
                    // Apply Changes
                    int counter = 0;
                    var changedObjects = new List<IPersistenceObject>();
                    foreach (var objFromServer in objectsFromServer)
                    {
                        IClientObject obj;
                        IPersistenceObject underlyingObject;

                        if (counter < objectsToAdd.Count)
                        {
                            obj = (IClientObject)objectsToAdd[counter++];
                            underlyingObject = obj.UnderlyingObject;

                            // remove object from cache, since index by ID may change.
                            // will be re-inserted on attach later
                            ctx._objects.Remove(underlyingObject);
                        }
                        else
                        {
                            underlyingObject = ctx.ContainsObject(ctx.GetInterfaceType(objFromServer), objFromServer.ID) ?? objFromServer;
                            obj = (IClientObject)underlyingObject;
                        }

                        ((BasePersistenceObject)underlyingObject).RecordNotifications();
                        if (obj != objFromServer)
                        {
                            underlyingObject.ApplyChangesFrom(objFromServer);
                        }

                        // reset ObjectState to new truth
                        switch (objFromServer.ObjectState)
                        {
                            case DataObjectState.Deleted:
                                obj.SetDeleted();
                                break;
                            case DataObjectState.Unmodified:
                                obj.SetUnmodified();
                                break;
                            case DataObjectState.New:
                                FixObjStateNew(obj);
                                break;
                            case DataObjectState.Modified:
                                FixObjStateModified(obj);
                                break;
                            case DataObjectState.NotDeserialized:
                            case DataObjectState.Detached:
                                throw new InvalidOperationException(string.Format("Invalid state received from server: {0}", objFromServer.ObjectState));
                            default:
                                throw new InvalidOperationException(string.Format("Unknown state received from server: {0}", objFromServer.ObjectState));
                        }

                        changedObjects.Add(underlyingObject);
                    }

                    objectsToDetach.Except(changedObjects).ForEach(obj => ctx.Detach(obj));
                    changedObjects.ForEach(obj => ctx.Attach(obj));

                    this.UpdateModifiedState(ctx);
                }
                finally
                {
                    ctx.PlaybackNotifications();
                }
                NotifyPostSave();

                return objectsToSubmit.Count;
            }

            protected virtual void FixObjStateNew(IClientObject obj)
            {
                // do nothing
            }

            protected virtual void FixObjStateModified(IClientObject obj)
            {
                // do nothing
            }

            protected virtual void NotifyPreSave(ZetboxContextImpl ctx)
            {
                // do nothing
            }

            protected virtual void NotifyPostSave()
            {
                // do nothing
            }

            protected virtual void UpdateModifiedState(ZetboxContextImpl ctx)
            {
                // Do nothing!
            }

            protected abstract Task<IEnumerable<IPersistenceObject>> ExecuteServerCall(ZetboxContextImpl ctx, IEnumerable<IPersistenceObject> objectsToSubmit, IEnumerable<ObjectNotificationRequest> notificationRequests);
        }

        private class SubmitChangesHandler : ExchangeObjectsHandler
        {
            protected override async Task<IEnumerable<IPersistenceObject>> ExecuteServerCall(ZetboxContextImpl ctx, IEnumerable<IPersistenceObject> objectsToSubmit, IEnumerable<ObjectNotificationRequest> notificationRequests)
            {
                return await ctx.proxy.SetObjects(
                    objectsToSubmit,
                    notificationRequests);
            }

            protected override void FixObjStateNew(IClientObject obj)
            {
                throw new InvalidOperationException(string.Format("received at least one object from server that is new - this can't be after a submit changes: {0}#{1}", obj.UnderlyingObject.GetType(), obj.UnderlyingObject.ID));
            }

            protected override void FixObjStateModified(IClientObject obj)
            {
                // Bad hack due to apply changes implementation
                obj.SetUnmodified();
            }

            protected override void UpdateModifiedState(ZetboxContextImpl ctx)
            {
                // Before Notifications & PostSave events. They could change data
                ctx.IsModified = false;
            }

            private HashSet<IDataObject> notifiedObjects;
            protected override void NotifyPreSave(ZetboxContextImpl ctx)
            {
                int iterations = 1;
                notifiedObjects = new HashSet<IDataObject>();
                for (; ; )
                {
                    var notified = false;
                    foreach (var obj in ctx._objects.OfType<IDataObject>().Where(o => o.ObjectState.In(DataObjectState.New, DataObjectState.Modified) && !notifiedObjects.Contains(o)))
                    {
                        notifiedObjects.Add(obj);
                        obj.NotifyPreSave();
                        notified = true;
                    }

                    if (!notified)
                        break;

                    if (iterations++ == 10)
                    {
                        Logging.Facade.Warn("Long iteration when trying to NotifyPreSave");
                    }
                }
            }

            protected override void NotifyPostSave()
            {
                // Fire PostSave
                notifiedObjects.ForEach(o => o.NotifyPostSave());

                // avoid reusing list
                notifiedObjects = null;
            }
        }

        private SubmitChangesHandler _submitChangesHandler = null;
        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public async Task<int> SubmitChanges()
        {
            CheckDisposed();
            if (IsReadonly)
                throw new ReadOnlyContextException();

            int result = 0;
            var ticks = _perfCounter.IncrementSubmitChanges();
            try
            {
                // TODO: Add a better Cache Refresh Strategie
                // CacheController<IDataObject>.Current.Clear();

                var added = new List<IDataObject>();
                var modified = new List<IDataObject>();
                var deleted = new List<IDataObject>();
                foreach (var ido in AttachedObjects.OfType<IDataObject>())
                {
                    switch (ido.ObjectState)
                    {
                        case DataObjectState.New:
                            added.Add(ido);
                            break;
                        case DataObjectState.Modified:
                            modified.Add(ido);
                            break;
                        case DataObjectState.Deleted:
                            deleted.Add(ido);
                            break;
                    }
                }

                if (_submitChangesHandler == null) _submitChangesHandler = new SubmitChangesHandler();
                result = await _submitChangesHandler.ExchangeObjects(this);

                ZetboxContextEventListenerHelper.OnSubmitted(_eventListeners, this, added, modified, deleted);
            }
            finally
            {
                _perfCounter.DecrementSubmitChanges(result, ticks);
            }
            return result;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <param name="ifType">Interface Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public Task<IDataObject> FindAsync(InterfaceType ifType, int ID)
        {
            CheckDisposed();

            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            // return GetQuery(type).Single(o => o.ID == ID);

            return (Task<IDataObject>)this.GetType().FindGenericMethod("FindAsyncGenericHelper",
                new Type[] { ifType.Type },
                new Type[] { typeof(int) },
                isPrivate: true)
                .Invoke(this, new object[] { ID });
        }

        private async Task<IDataObject> FindAsyncGenericHelper<T>(int id)
            where T : class, IDataObject
        {
            var result = (IDataObject)await FindAsync<T>(id);
            return result;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <param name="ifType">Interface Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public IDataObject Find(InterfaceType ifType, int ID)
        {
            try
            {
                var t = FindAsync(ifType, ID);
                return t.Result;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                // unwrap "business" exception
                throw ex.StripTargetInvocationExceptions();
            }
        }

        private T MakeAccessDeniedProxy<T>(int id)
            where T : class, IPersistenceObject
        {
            var result = CreateUnattached<T>();
            (result as BasePersistenceObject).ID = id;
            Attach(result);
            ((IClientObject)result).MakeAccessDeniedProxy();
            return result;
        }

        private T MakeAccessDeniedProxy<T>(Guid exportGuid)
            where T : class, IPersistenceObject
        {
            var result = CreateUnattached<T>();
            checked
            {
                // Fake a ID, when a guid is given, the ID is unknown
                (result as BasePersistenceObject).ID = --_newIDCounter;
            }
            ((Zetbox.App.Base.IExportable)result).ExportGuid = exportGuid;
            Attach(result);
            ((IClientObject)result).MakeAccessDeniedProxy();
            return result;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public async Task<T> FindAsync<T>(int ID)
            where T : class, IDataObject
        {
            CheckDisposed();
            IPersistenceObject cacheHit = _objects.Lookup(_iftFactory(typeof(T)), ID);
            if (cacheHit != null)
                return (T)cacheHit;

            var result = await GetQuery<T>()
                .WithDeactivated()
                .SingleOrDefaultAsync(o => o.ID == ID);

            if (IsDisposed) return null;
            if (result == null) result = MakeAccessDeniedProxy<T>(ID);
            return result;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public T Find<T>(int ID)
            where T : class, IDataObject
        {
            try
            {
                var t = FindAsync<T>(ID);
                return t.Result;
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                // unwrap "business" exception
                throw ex.StripTargetInvocationExceptions();
            }
        }

        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            CheckDisposed();

            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject",
                new Type[] { ifType.Type },
                new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
        }

        /// <summary>
        /// Find the Persistence Object of the given type by ID.
        /// Note: This method is not supported on the client
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject
        {
            CheckDisposed();
            IPersistenceObject cacheHit = _objects.Lookup(_iftFactory(typeof(T)), ID);
            if (cacheHit != null)
            {
                return (T)cacheHit;
            }
            else
            {
                return GetPersistenceObjectQuery<T>()
                    .WithDeactivated()
                    .SingleOrDefault(o => o.ID == ID)
                    ?? MakeAccessDeniedProxy<T>(ID);
            }
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// Note: This method is not supported on the client yet
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject",
                new Type[] { ifType.Type },
                new Type[] { typeof(Guid) })
                .Invoke(this, new object[] { exportGuid });
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject
        {
            return GetPersistenceObjectQuery<T>()
                .WithDeactivated()
                .SingleOrDefault(o => ((Zetbox.App.Base.IExportable)o).ExportGuid == exportGuid)
                ?? MakeAccessDeniedProxy<T>(exportGuid);
        }

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            //return GetQuery(type).Single(o => o.ID == ID);

            return (IEnumerable<IPersistenceObject>)this.GetType().FindGenericMethod("FindPersistenceObjects",
                new Type[] { ifType.Type },
                new Type[] { typeof(IEnumerable<Guid>) })
                .Invoke(this, new object[] { exportGuids });
        }
        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject
        {
            return GetPersistenceObjectQuery<T>()
                .WithDeactivated()
                .Where(o => exportGuids.Contains(((Zetbox.App.Base.IExportable)o).ExportGuid));
        }

        /// <inheritdoc />
        public event GenericEventHandler<IZetboxContext> Changed;
        protected virtual void OnChanged()
        {
            GenericEventHandler<IZetboxContext> temp = Changed;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IZetboxContext>() { Data = this });
            }
        }

        public event GenericEventHandler<IPersistenceObject> ObjectCreated;

        protected virtual void OnObjectCreated(IPersistenceObject obj)
        {
            if (ObjectCreated != null)
            {
                ObjectCreated(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        public event GenericEventHandler<IPersistenceObject> ObjectDeleted;

        protected virtual void OnObjectDeleted(IPersistenceObject obj)
        {
            if (ObjectDeleted != null)
            {
                ObjectDeleted(this, new GenericEventArgs<IPersistenceObject>() { Data = obj });
            }
        }

        /// <summary>
        /// Create and store a Blob.
        /// </summary>
        /// <remarks>In contrast to the servers's implementation, this does submit the blob immediately. This may cause orphaned blobs in the database.</remarks>        
        /// <returns>the ID of the created Blob</returns>
        public async Task<int> CreateBlob(Stream s, string filename, string mimetype)
        {
            var blob = await proxy.SetBlobStream(s, filename, mimetype);
            Attach(blob);
            return blob.ID;
        }

        public Task<int> CreateBlob(FileInfo fi, string mimetype)
        {
            using (var s = fi.OpenRead())
            {
                return CreateBlob(s, fi.Name, mimetype);
            }
        }

        private string DocumentCache
        {
            get
            {
                return Path.Combine(config.TempFolder, "cache");
            }
        }

        public Stream GetStream(int ID)
        {
            return GetFileInfo(ID).OpenRead();
        }

        public async Task<Stream> GetStreamAsync(int ID)
        {
            var task = await GetFileInfoAsync(ID);
            if (IsDisposed) return null;
            return task.OpenRead();
        }

        public FileInfo GetFileInfo(int ID)
        {
            return GetFileInfoAsync(ID).Result;
        }

        private static readonly object _cacheFileLock = new object();
        public async System.Threading.Tasks.Task<FileInfo> GetFileInfoAsync(int ID)
        {
            var blobTask = await this.FindAsync<Zetbox.App.Base.Blob>(ID);
            string path = Path.Combine(DocumentCache, blobTask.StoragePath.ToLocalPath());
            if (path.Length >= 256)
            {
                var dir = Path.GetDirectoryName(path);
                if (dir.Length >= 256 - 41 - 4)
                {
                    throw new PathTooLongException("DocumentCache path is far too long. Should be less then 256 - 41 for guid and 4 for extension. Path includes DocumentStore\\year\\month\\day. FullPath: " + path);
                }
                var name = Path.GetFileNameWithoutExtension(path);
                var ext = Path.GetExtension(path);
                name = name.Substring(0, 255 - dir.Length - ext.Length);
                path = Path.Combine(dir, name + ext);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            //lock (_cacheFileLock)
            {
                if (!File.Exists(path))
                {
                    using (var stream = await proxy.GetBlobStream(ID))
                    using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        file.SetLength(0);
                        stream.CopyAllTo(file);
                    }
                    File.SetAttributes(path, FileAttributes.ReadOnly);
                }
            }

            return new FileInfo(path);
        }

        #region IDebuggingZetboxContext Members

        public StackTrace CreatedAt { get; private set; }

        public StackTrace DisposedAt { get; private set; }

        #endregion

        #region IReadOnlyZetboxContext Members

        public InterfaceType GetInterfaceType(Type t)
        {
            return _iftFactory(t);
        }

        public InterfaceType GetInterfaceType(string typeName)
        {
            return _iftFactory(Type.GetType(typeName + "," + typeof(Zetbox.App.Base.ObjectClass).Assembly.FullName, true));
        }

        public InterfaceType GetInterfaceType(IPersistenceObject obj)
        {
            return _iftFactory(((BasePersistenceObject)obj).GetImplementedInterface());
        }

        public InterfaceType GetInterfaceType(ICompoundObject obj)
        {
            return _iftFactory(((BaseCompoundObject)obj).GetImplementedInterface());
        }

        public ImplementationType ToImplementationType(InterfaceType t)
        {
            return GetImplementationType(Type.GetType(t.Type.FullName + "Client" + Zetbox.API.Helper.ImplementationSuffix + "," + _ClientImplementationAssembly, true));
        }

        public ImplementationType GetImplementationType(Type t)
        {
            return _implTypeFactory(t);
        }

        [NonSerialized]
        private Dictionary<object, object> _transientState;
        /// <inheritdoc />
        [XmlIgnore]
        public IDictionary<object, object> TransientState
        {
            get
            {
                CheckDisposed();
                if (_transientState == null)
                {
                    _transientState = new Dictionary<object, object>();
                }
                return _transientState;
            }
        }
        #endregion

        /// <summary>
        /// Indicates that the Zetbox Context has some modified, added or deleted items
        /// </summary>
        private bool _isModified = false;
        public bool IsModified
        {
            get
            {
                return _isModified;
            }
            private set
            {
                if (!IsReadonly && _isModified != value)
                {
                    _isModified = value;

                    EventHandler temp = IsModifiedChanged;
                    if (temp != null)
                    {
                        temp(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Is fires when <see cref="IsModified"/> was changed
        /// </summary>
        public event EventHandler IsModifiedChanged;

        #region IZetboxContextInternals Members
        /// <summary>
        /// TODO: Not supported yet
        /// </summary>
        int IZetboxContextInternals.IdentityID { get { return Helper.INVALIDID; } }

        void IZetboxContextInternals.SetModified(IPersistenceObject obj)
        {
            if (obj.ObjectState.In(DataObjectState.Deleted, DataObjectState.Modified, DataObjectState.New))
            {
                IsModified = true;
            }
        }
        string IZetboxContextInternals.StoreBlobStream(Stream s, Guid exportGuid, DateTime timestamp, string filename)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IZetboxClientContextInternals Members

        private class InvokeServerMethodHandler<T> : ExchangeObjectsHandler where T : class, IDataObject
        {
            public InvokeServerMethodHandler(T obj, string name, Type retValType, IEnumerable<Type> parameterTypes, params object[] parameter)
            {
                this.obj = obj;
                this.name = name;
                this.retValType = retValType;
                this.parameterTypes = parameterTypes;
                this.parameter = parameter;
            }

            private readonly T obj;
            private readonly string name;
            private readonly Type retValType;
            private readonly IEnumerable<Type> parameterTypes;
            private readonly object[] parameter;

            protected override async Task<IEnumerable<IPersistenceObject>> ExecuteServerCall(ZetboxContextImpl ctx, IEnumerable<IPersistenceObject> objectsToSubmit, IEnumerable<ObjectNotificationRequest> notificationRequests)
            {
                IEnumerable<IPersistenceObject> changedObjects;
                List<IStreamable> auxObjects;
                var response = await ctx.proxy.InvokeServerMethod(
                    ctx.GetInterfaceType(obj),
                    obj.ID,
                    name,
                    retValType,
                    parameterTypes,
                    parameter,
                    objectsToSubmit,
                    notificationRequests);

                Result = response.Item1;
                changedObjects = response.Item2;
                auxObjects = response.Item3;

                if (Result != null && Result.GetType().IsIPersistenceObject())
                {
                    Result = ctx.AttachRespectingIsolationLevel((IPersistenceObject)Result);
                }
                else if (Result != null && Result.GetType().IsIList() && Result.GetType().FindElementTypes().Any(t => t.IsIPersistenceObject()))
                {
                    var lst = (IList)Result;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        lst[i] = ctx.AttachRespectingIsolationLevel((IPersistenceObject)lst[i]);
                    }
                }

                if (auxObjects != null)
                {
                    foreach (IPersistenceObject auxObj in auxObjects)
                    {
                        ctx.AttachRespectingIsolationLevel(auxObj);
                    }
                }

                return changedObjects;
            }

            public object Result { get; private set; }
        }

        public async Task<object> InvokeServerMethod<T>(T obj, string name, Type retValType, IEnumerable<Type> parameterTypes, params object[] parameter) where T : class, IDataObject
        {
            CheckDisposed();
            InvokeServerMethodHandler<T> handler = new InvokeServerMethodHandler<T>(obj, name, retValType, parameterTypes, parameter);
            await handler.ExchangeObjects(this);
            return handler.Result;
        }

        public ContextIsolationLevel IsolationLevel { get { return _clientIsolationLevel; } }

        #endregion

        public int GetSequenceNumber(Guid sequenceGuid)
        {
            throw new NotSupportedException();
        }

        public int GetContinuousSequenceNumber(Guid sequenceGuid)
        {
            throw new NotSupportedException();
        }

        public void BeginTransaction()
        {
            throw new NotSupportedException();
        }

        public void CommitTransaction()
        {
            throw new NotSupportedException();
        }

        public void RollbackTransaction()
        {
            // Allways allowed
        }

        public List<IDataObject> GetAll(InterfaceType t)
        {
            throw new NotSupportedException("Use GetQuery<T>().Take(x) instead");
        }

        private bool _elevatedMode = false;
        public async Task SetElevatedMode(bool elevatedMode)
        {
            if (!(await _identityResolver.GetCurrent()).IsAdministrator()) throw new System.Security.SecurityException("You have no rights to enter elevated mode");
            if (_elevatedMode != elevatedMode)
            {
                _elevatedMode = elevatedMode;
                var temp = IsElevatedModeChanged;
                if (temp != null)
                {
                    temp(this, EventArgs.Empty);
                }
            }
        }
        public bool IsElevatedMode { get { return _elevatedMode; } }
        public event EventHandler IsElevatedModeChanged;
    }
}
