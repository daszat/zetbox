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
        object InvokeServerMethod<T>(T obj, string name, Type retValType, IEnumerable<Type> parameterTypes, params object[] parameter) where T : class, IDataObject;
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
        private readonly ClientIsolationLevel _clientIsolationLevel;
        private readonly IPerfCounter _perfCounter;
        private readonly IIdentityResolver _identityResolver;

        /// <summary>
        /// List of Objects (IDataObject and ICollectionEntry) in this Context.
        /// </summary>
        private ContextCache<int> _objects;

        /// <summary>
        /// Counter for newly created Objects to give them a valid ID
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Uses global constant")]
        private int _newIDCounter = Helper.INVALIDID;

        public ZetboxContextImpl(ClientIsolationLevel il, ZetboxConfig config, IProxy proxy, string clientImplementationAssembly, Func<IFrozenContext> lazyCtx, InterfaceType.Factory iftFactory, ClientImplementationType.ClientFactory implTypeFactory, UnattachedObjectFactory unattachedObjectFactory, IPerfCounter perfCounter, IIdentityResolver identityResolver)
        {
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");
            this._clientIsolationLevel = il;
            this.config = config;
            this.proxy = proxy;
            this._ClientImplementationAssembly = clientImplementationAssembly;
            this._objects = new ContextCache<int>(this, item => item.ID);
            this._lazyCtx = lazyCtx;
            this._iftFactory = iftFactory;
            this._implTypeFactory = implTypeFactory;
            this._unattachedObjectFactory = unattachedObjectFactory;
            this._perfCounter = perfCounter;
            this._identityResolver = identityResolver;

            CreatedAt = new StackTrace(true);
            ZetboxContextDebuggerSingleton.Created(this);
        }

        public event GenericEventHandler<IReadOnlyZetboxContext> Disposing;

        [SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Clarifies intent of variable")]
        private bool disposed = false;
        /// <summary>
        /// Dispose this Context.
        /// </summary>
        public void Dispose()
        {
            GenericEventHandler<IReadOnlyZetboxContext> temp = Disposing;
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
                }
                disposed = true;
            }
            // TODO: use correct Dispose implementation pattern
            GC.SuppressFinalize(this);
        }

        public bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        public bool IsReadonly { get { return false; } }

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
        public ZbTask<List<T>> GetListOfAsync<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            CheckDisposed();
            if (obj.CurrentAccessRights.HasNoRights()) return new ZbTask<List<T>>(ZbTask.Synchron, () => new List<T>());

            ZetboxContextQuery<T> query = new ZetboxContextQuery<T>(this, GetInterfaceType(obj), proxy, _perfCounter);
            var task = ((ZetboxContextProvider)query.Provider).GetListOfCallAsync(obj.ID, propertyName);
            return new ZbTask<List<T>>(task)
            .OnResult(t =>
            {
                t.Result = task.Result.Cast<T>().ToList();
            });
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            var t = GetListOfAsync<T>(obj, propertyName);
            t.Wait();
            return t.Result;
        }

        public ZbTask<IList<T>> FetchRelationAsync<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry
        {
            var parentId = container.ID;
            var parentIfType = GetInterfaceType(container);
            var fetchTask = new ZbTask<Tuple<IEnumerable<T>, List<IStreamable>>>(() =>
            {
                List<IStreamable> auxObjects;
                var serverList = proxy.FetchRelation<T>(relationId, role, parentId, parentIfType, out auxObjects);
                return new Tuple<IEnumerable<T>, List<IStreamable>>(serverList, auxObjects);
            });

            return new ZbTask<IList<T>>(fetchTask)
                .OnResult(t =>
                {
                    foreach (IPersistenceObject obj in fetchTask.Result.Item2)
                    {
                        this.AttachRespectingIsolationLevel(obj);
                    }

                    t.Result = new List<T>();
                    foreach (IPersistenceObject obj in fetchTask.Result.Item1)
                    {
                        var localobj = this.AttachRespectingIsolationLevel(obj);
                        t.Result.Add((T)localobj);
                    }
                    PlaybackNotifications();
                });
        }

        public IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject container) where T : class, IRelationEntry
        {
            var t = FetchRelationAsync<T>(relationId, role, container);
            t.Wait();
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
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyCreated();
            }
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

            if (_clientIsolationLevel == ClientIsolationLevel.MergeServerData && obj != localobj)
            {
                RecordNotifications(localobj);
                localobj.ApplyChangesFrom(obj);
                // reset ObjectState to new truth
                ((IClientObject)localobj).SetUnmodified();
            }

            return localobj;
        }

        private List<BasePersistenceObject> _objectsToPlayBackNotifications = null;

        internal void RecordNotifications(IPersistenceObject obj)
        {
            if (_objectsToPlayBackNotifications == null)
            {
                _objectsToPlayBackNotifications = new List<BasePersistenceObject>();
            }
            var bpo = (BasePersistenceObject)obj;
            bpo.RecordNotifications();
            _objectsToPlayBackNotifications.Add(bpo);
        }

        internal void PlaybackNotifications()
        {
            if (_objectsToPlayBackNotifications == null)
                return;
            _objectsToPlayBackNotifications.ForEach(obj => obj.PlaybackNotifications());
            _objectsToPlayBackNotifications = null;
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
            OnObjectDeleted(obj);

            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyDeleting();
            }
        }

        private abstract class ExchangeObjectsHandler
        {
            public int ExchangeObjects(ZetboxContextImpl ctx)
            {
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

                var notifySaveList = objectsToSubmit.OfType<IDataObject>().Where(o => o.ObjectState.In(DataObjectState.New, DataObjectState.Modified));

                // Fire PreSave
                notifySaveList.ForEach(o => o.NotifyPreSave());

                var notificationRequests = ctx.AttachedObjects
                        .ToLookup(o => ctx.GetInterfaceType(o))
                        .Select(g => new ObjectNotificationRequest() { Type = g.Key.ToSerializableType(), IDs = g.Select(o => o.ID).ToArray() });

                // Submit to server
                var objectsFromServer = this.ExecuteServerCall(ctx, objectsToSubmit, notificationRequests);

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

                    ctx.RecordNotifications(underlyingObject);
                    if (obj != objFromServer)
                    {
                        underlyingObject.ApplyChangesFrom(objFromServer);
                    }

                    if (objFromServer.ObjectState == DataObjectState.Deleted)
                    {
                        // deleted on server
                        obj.SetDeleted();
                    }
                    else
                    {
                        // reset ObjectState to new truth
                        obj.SetUnmodified();
                    }

                    changedObjects.Add(underlyingObject);
                }

                objectsToDetach.Except(changedObjects).ForEach(obj => ctx.Detach(obj));
                changedObjects.ForEach(obj => ctx.Attach(obj));

                this.UpdateModifiedState(ctx);

                ctx.PlaybackNotifications();

                // Fire PostSave
                notifySaveList.ForEach(o => o.NotifyPostSave());

                return objectsToSubmit.Count;
            }

            protected abstract IEnumerable<IPersistenceObject> ExecuteServerCall(ZetboxContextImpl ctx, IEnumerable<IPersistenceObject> objectsToSubmit, IEnumerable<ObjectNotificationRequest> notificationRequests);
            protected abstract void UpdateModifiedState(ZetboxContextImpl ctx);
        }

        private class SubmitChangesHandler : ExchangeObjectsHandler
        {
            protected override IEnumerable<IPersistenceObject> ExecuteServerCall(ZetboxContextImpl ctx, IEnumerable<IPersistenceObject> objectsToSubmit, IEnumerable<ObjectNotificationRequest> notificationRequests)
            {
                return ctx.proxy.SetObjects(
                    objectsToSubmit,
                    notificationRequests);
            }

            protected override void UpdateModifiedState(ZetboxContextImpl ctx)
            {
                // Before Notifications & PostSave events. They could change data
                ctx.IsModified = false;
            }
        }

        private SubmitChangesHandler _submitChangesHandler = null;
        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public int SubmitChanges()
        {
            CheckDisposed();
            int result = 0;
            var ticks = _perfCounter.IncrementSubmitChanges();
            try
            {
                // TODO: Add a better Cache Refresh Strategie
                // CacheController<IDataObject>.Current.Clear();

                if (_submitChangesHandler == null) _submitChangesHandler = new SubmitChangesHandler();
                result = _submitChangesHandler.ExchangeObjects(this);
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
        public ZbTask<IDataObject> FindAsync(InterfaceType ifType, int ID)
        {
            CheckDisposed();

            // TODO: should be able to pass "type" unmodified, like this
            // See Case 552
            // return GetQuery(type).Single(o => o.ID == ID);

            return (ZbTask<IDataObject>)this.GetType().FindGenericMethod(true, "FindAsyncGenericHelper",
                new Type[] { ifType.Type },
                new Type[] { typeof(int) })
                .Invoke(this, new object[] { ID });
        }

        private ZbTask<IDataObject> FindAsyncGenericHelper<T>(int id)
            where T : class, IDataObject
        {
            var nestedTask = FindAsync<T>(id);
            return new ZbTask<IDataObject>(nestedTask)
                .OnResult(t => { t.Result = nestedTask.Result; });
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
            var t = FindAsync(ifType, ID);
            t.Wait();
            return t.Result;
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

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public ZbTask<T> FindAsync<T>(int ID)
            where T : class, IDataObject
        {
            CheckDisposed();
            IPersistenceObject cacheHit = _objects.Lookup(_iftFactory(typeof(T)), ID);
            if (cacheHit != null)
                return new ZbTask<T>(ZbTask.Synchron, () => (T)cacheHit);

            return GetQuery<T>()
                    .SingleOrDefaultAsync(o => o.ID == ID)
                    .OnResult(t =>
                    {
                        if (t.Result == null) t.Result = MakeAccessDeniedProxy<T>(ID);
                    });
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
            var t = FindAsync<T>(ID);
            t.Wait();
            return t.Result;
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
                return GetPersistenceObjectQuery<T>().SingleOrDefault(o => o.ID == ID) ?? MakeAccessDeniedProxy<T>(ID);
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
            return GetPersistenceObjectQuery<T>().Single(o => ((Zetbox.App.Base.IExportable)o).ExportGuid == exportGuid);
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
            return GetPersistenceObjectQuery<T>().Where(o => exportGuids.Contains(((Zetbox.App.Base.IExportable)o).ExportGuid));
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

        public int CreateBlob(Stream s, string filename, string mimetype)
        {
            var blob = proxy.SetBlobStream(s, filename, mimetype);
            Attach(blob);
            return blob.ID;
        }

        public int CreateBlob(FileInfo fi, string mimetype)
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

        public ZbTask<Stream> GetStreamAsync(int ID)
        {
            var task = GetFileInfoAsync(ID);
            return new ZbTask<Stream>(task).OnResult(t => t.Result = task.Result.OpenRead());
        }

        public FileInfo GetFileInfo(int ID)
        {
            return GetFileInfoAsync(ID).Result;
        }

        private static readonly object _cacheFileLock = new object();
        public ZbTask<FileInfo> GetFileInfoAsync(int ID)
        {
            var blobTask = this.FindAsync<Zetbox.App.Base.Blob>(ID);
            return new ZbTask<FileInfo>(blobTask)
                .ContinueWith(t =>
                {
                    string path = Path.Combine(DocumentCache, blobTask.Result.StoragePath);
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

                    lock (_cacheFileLock)
                    {
                        if (!File.Exists(path))
                        {
                            using (var stream = proxy.GetBlobStream(ID))
                            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                            {
                                file.SetLength(0);
                                stream.CopyAllTo(file);
                            }
                            File.SetAttributes(path, FileAttributes.ReadOnly);
                        }
                    }

                    t.Result = new FileInfo(path);
                });
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

        private IDictionary<object, object> _TransientState = null;
        /// <inheritdoc />
        public IDictionary<object, object> TransientState
        {
            get
            {
                CheckDisposed();
                if (_TransientState == null)
                {
                    _TransientState = new Dictionary<object, object>();
                }
                return _TransientState;
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
                _isModified = value;
                // Allways invoke event, as others are temporary interessed in seeing changes on individual objects
                EventHandler temp = IsModifiedChanged;
                if (temp != null)
                {
                    temp(this, EventArgs.Empty);
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

            protected override IEnumerable<IPersistenceObject> ExecuteServerCall(ZetboxContextImpl ctx, IEnumerable<IPersistenceObject> objectsToSubmit, IEnumerable<ObjectNotificationRequest> notificationRequests)
            {
                IEnumerable<IPersistenceObject> changedObjects;
                List<IStreamable> auxObjects;
                Result = ctx.proxy.InvokeServerMethod(
                    ctx.GetInterfaceType(obj),
                    obj.ID,
                    name,
                    retValType,
                    parameterTypes,
                    parameter,
                    objectsToSubmit,
                    notificationRequests,
                    out changedObjects,
                    out auxObjects);

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

            protected override void UpdateModifiedState(ZetboxContextImpl ctx)
            {
                // Do nothing!
            }
        }

        public object InvokeServerMethod<T>(T obj, string name, Type retValType, IEnumerable<Type> parameterTypes, params object[] parameter) where T : class, IDataObject
        {
            CheckDisposed();
            InvokeServerMethodHandler<T> handler = new InvokeServerMethodHandler<T>(obj, name, retValType, parameterTypes, parameter);
            handler.ExchangeObjects(this);
            return handler.Result;
        }

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
        public void SetElevatedMode(bool elevatedMode)
        {
            if (!_identityResolver.GetCurrent().IsAdmininistrator()) throw new System.Security.SecurityException("You have no rights to enter elevated mode");
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
