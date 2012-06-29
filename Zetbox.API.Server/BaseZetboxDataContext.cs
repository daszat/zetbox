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

namespace Zetbox.API.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public delegate IZetboxContext ServerZetboxContextFactory(Identity identity);

    public abstract class BaseZetboxDataContext
        : IZetboxServerContext, IZetboxContextInternals, IDisposable
    {
        protected readonly Identity identityStore;
        protected readonly IMetaDataResolver metaDataResolver;
        protected readonly FuncCache<Type, InterfaceType> iftFactoryCache;
        protected readonly InterfaceType.Factory iftFactory;
        protected readonly Func<IFrozenContext> lazyCtx;
        protected readonly ZetboxConfig config;

        /// <summary>
        /// Initializes a new instance of the BaseZetboxDataContext class using the specified <see cref="Identity"/>.
        /// </summary>
        /// <param name="metaDataResolver">the IMetaDataResolver for this context.</param>
        /// <param name="identity">the identity of this context. if this is null, the context does no security checks</param>
        /// <param name="config"></param>
        /// <param name="lazyCtx"></param>
        /// <param name="iftFactory"></param>
        protected BaseZetboxDataContext(IMetaDataResolver metaDataResolver, Identity identity, ZetboxConfig config, Func<IFrozenContext> lazyCtx, InterfaceType.Factory iftFactory)
        {
            if (metaDataResolver == null) { throw new ArgumentNullException("metaDataResolver"); }
            if (config == null) { throw new ArgumentNullException("config"); }
            if (iftFactory == null) { throw new ArgumentNullException("iftFactory"); }

            this.metaDataResolver = metaDataResolver;
            this.identityStore = identity;
            this.config = config;
            this.iftFactoryCache = new FuncCache<Type, InterfaceType>(r => iftFactory(r));
            this.iftFactory = t => iftFactoryCache.Invoke(t);
            this.lazyCtx = lazyCtx;
        }

        /// <summary>
        /// Fired when the Context is beeing disposed.
        /// </summary>
        public event GenericEventHandler<IReadOnlyZetboxContext> Disposing;

        // TODO: implement proper IDisposable pattern
        public virtual void Dispose()
        {
            GenericEventHandler<IReadOnlyZetboxContext> temp = Disposing;
            if (temp != null)
            {
                temp(this, new GenericEventArgs<IReadOnlyZetboxContext>() { Data = this });
            }
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        /// <summary>
        /// Is true after Dispose() was called.
        /// </summary>
        public bool IsDisposed { get; private set; }

        protected void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("Context already disposed");
            }
        }

        public bool IsReadonly { get { return false; } }

        int IZetboxContextInternals.IdentityID { get { return Identity != null ? Identity.ID : Helper.INVALIDID; } }

        public Zetbox.API.AccessRights GetGroupAccessRights(InterfaceType ifType)
        {
            if (Identity == null || !ifType.Type.IsIDataObject()) return Zetbox.API.AccessRights.Full;

            // Identity is a Administrator - is allowed to do everything
            if (Identity.IsAdmininistrator()) return Zetbox.API.AccessRights.Full;

            // Case #1363: May return NULL during initialization
            var objClass = metaDataResolver.GetObjectClass(ifType);
            if (objClass == null) return Zetbox.API.AccessRights.Full;

            // Only ACL's on Root classes are allowed
            var rootClass = objClass.GetRootClass();

            // No AccessControlList - full rights
            if (!rootClass.HasAccessControlList()) return Zetbox.API.AccessRights.Full;

            var rights = rootClass.GetGroupAccessRights(Identity);
            if (rights.HasValue)
            {
                return rights.Value;
            }
            else
            {
                return rootClass.NeedsRightsTable() ? Zetbox.API.AccessRights.None : Zetbox.API.AccessRights.Full;
            }
        }

        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check required.
        /// </summary>
        /// <param name="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        public virtual IPersistenceObject Attach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.ObjectState != DataObjectState.Detached && obj.ObjectState != DataObjectState.New)
            {
                throw new ArgumentOutOfRangeException("obj", String.Format("Cannot attach object unless it is New or Detached. obj.ObjectState == {0}", obj.ObjectState));
            }

            CheckCreateRights(obj);

            // call Attach on Subitems
            obj.AttachToContext(this);

            OnChanged();

            return obj;
        }

        void IZetboxContextInternals.AttachAsNew(IPersistenceObject obj)
        {
            // delegate to protected virtual method below
            this.AttachAsNew(obj);
        }

        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check required.
        /// </summary>
        /// <param name="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        protected virtual void AttachAsNew(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (obj.ObjectState != DataObjectState.Detached)
            {
                throw new ArgumentOutOfRangeException("obj", String.Format("Cannot attach object as new unless it is Detached. obj.ObjectState == {0}", obj.ObjectState));
            }

            CheckCreateRights(obj);

            ((BaseServerPersistenceObject)obj).SetNew();

            // call Attach on Subitems
            obj.AttachToContext(this);

            OnChanged();
        }

        private void CheckCreateRights(IPersistenceObject obj)
        {
            // Do not only check in IZetboxContext.Create for creation rights, also here
            // Object might be created by SerializableType
            if (obj is IDataObject && obj.ObjectState == DataObjectState.New)
            {
                var ifType = GetInterfaceType(obj);
                var cls = metaDataResolver.GetObjectClass(ifType);
                if (cls == null)
                {
                    Logging.Log.WarnFormat("obj=[{0}] ifType=[{1}]", obj.GetType().AssemblyQualifiedName, ifType.Type.AssemblyQualifiedName);
                    Logging.Log.WarnFormat("metaDataResolver=[{0}] => [{1}]", metaDataResolver.GetType().AssemblyQualifiedName, metaDataResolver.ToString());
                    throw new ApplicationException("Unexpected failure from metadata resolver");
                }
                cls = cls.GetRootClass();
                if (identityStore != null && cls.HasAccessControlList() && !cls.GetGroupAccessRights(identityStore).HasCreateRights())
                {
                    throw new System.Security.SecurityException(string.Format("The current identity has no rights to create an Object of type '{0}'", ifType.Type.FullName));
                }
            }
        }

        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        public virtual void Detach(IPersistenceObject obj)
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }

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
            if (obj == null) { throw new ArgumentNullException("obj"); }

            if (!obj.CurrentAccessRights.HasDeleteRights())
            {
                throw new System.Security.SecurityException(string.Format("The current identity has no rights to delete this Object: {0}({1})", GetInterfaceType(obj).Type.FullName, obj.ID));
            }

            IsModified = true;
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyDeleting();
            }
            
            DoDeleteObject(obj);
         
            OnObjectDeleted(obj);
        }

        protected virtual void DoDeleteObject(IPersistenceObject obj)
        {
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public abstract IQueryable<T> GetQuery<T>() where T : class, IDataObject;

        public List<IDataObject> GetAllHack<T>()
            where T : class, IDataObject
        {
            // The query translator cannot properly handle the IDataObject cast:
            // return GetQuery<T>().Cast<IDataObject>();

            var result = new List<IDataObject>();
            foreach (var o in GetQuery<T>())
            {
                result.Add(o);
            }
            return result;
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public List<IDataObject> GetAll(InterfaceType t)
        {
            var mi = this.GetType().FindGenericMethod("GetAllHack", new[] { t.Type }, new Type[0]);
            return (List<IDataObject>)mi.Invoke(this, new object[0]);
        }

        /// <summary>
        /// Returns a PersistenceObject Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IQueryable<T> GetPersistenceObjectQuery<T>() where T : class, IPersistenceObject;

        /// <summary>
        /// Returns the List referenced by the given Name.
        /// </summary>
        /// <typeparam name="T">List Type of the ObjectReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the ObjectReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the ObjectReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public virtual List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : class, IDataObject
        {
            CheckDisposed();
            if (obj == null) { throw new ArgumentNullException("obj"); }

            return obj.GetPropertyValue<IEnumerable>(propertyName).Cast<T>().ToList();
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IList<T> FetchRelation<T>(Guid relationId, RelationEndRole endRole, IDataObject parent) where T : class, IRelationEntry;

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public abstract IPersistenceObject ContainsObject(InterfaceType type, int ID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IEnumerable<IPersistenceObject> AttachedObjects
        {
            get;
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public abstract int SubmitChanges();

        /// <summary>
        /// Submits the changes and returns the number of affected Objects.
        /// This method does not fire any events or methods on added/changed objects. 
        /// It also does not change any IChanged property.
        /// </summary>
        /// <remarks>
        /// Only IDataObjects are counded.
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        public abstract int SubmitRestore();

        Identity localIdentity = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiedObjects">All changed, added and deleted objects</param>
        protected virtual void NotifyChanging(IEnumerable<IDataObject> modifiedObjects)
        {
            var now = DateTime.Now;

            foreach (IDataObject obj in modifiedObjects)
            {
                var state = obj.ObjectState;
                var rights = obj.CurrentAccessRights;

                // Blob check
                if (obj is Zetbox.App.Base.Blob && state == DataObjectState.Modified)
                {
                    throw new InvalidOperationException("Modifying a Zetbox.App.Base.Blob is not allowed. Upload a new Blob instead.");
                }

                // last rights check
                if (state == DataObjectState.Modified && !rights.HasWriteRights())
                {
                    throw new System.Security.SecurityException(string.Format("The current identity has no rights to modify an Object of type '{0}'", this.GetInterfaceType(obj).Type.FullName));
                }
                else if (state == DataObjectState.Deleted && !rights.HasDeleteRights())
                {
                    throw new System.Security.SecurityException(string.Format("The current identity has no rights to delete an Object of type '{0}'", this.GetInterfaceType(obj).Type.FullName));
                }
                else if (state == DataObjectState.New)
                {
                    CheckCreateRights(obj);
                }

                // Update IChangedBy 
                if (obj is Zetbox.App.Base.IChangedBy && state != DataObjectState.Deleted)
                {
                    var cb = (Zetbox.App.Base.IChangedBy)obj;
                    if (obj.ObjectState == DataObjectState.New)
                    {
                        cb.CreatedOn = now;
                    }
                    cb.ChangedOn = now;

                    if (this.identityStore != null)
                    {
                        if (localIdentity == null)
                        {
                            localIdentity = this.identityStore.Context == this ? this.identityStore : this.GetQuery<Identity>().First(id => id.ID == this.identityStore.ID);
                        }

                        if (obj.ObjectState == DataObjectState.New)
                        {
                            cb.CreatedBy = localIdentity;
                        }
                        cb.ChangedBy = localIdentity;
                    }
                }

                // Save notification
                if (state != DataObjectState.Deleted)
                {
                    obj.NotifyPreSave();
                }
            }
        }

        protected virtual void NotifyChanged(IEnumerable<IDataObject> changedOrAdded)
        {
            changedOrAdded.ForEach(obj => obj.NotifyPostSave());
        }

        /// <summary>
        /// Creates an unattached instance of the specified interface type. This is used by various public methods to create objects.
        /// </summary>
        /// <param name="ifType">the requested type</param>
        /// <returns>a newly initialised provider-specific object of the specified type, which is not yet attached</returns>
        protected abstract object CreateUnattachedInstance(InterfaceType ifType);

        private IPersistenceObject CreateInternal(InterfaceType ifType)
        {
            var obj = (BaseServerPersistenceObject)CreateUnattachedInstance(ifType);
            AttachAsNew(obj);
            IsModified = true;
            OnObjectCreated(obj);
            if (obj is IDataObject)
            {
                ((IDataObject)obj).NotifyCreated();
            }
            return obj;
        }

        /// <summary>
        /// Creates a new IPersistenceObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="ifType">System.Type of the new IPersistenceObject</param>
        /// <returns>A new IPersistenceObject</returns>
        public virtual IDataObject Create(InterfaceType ifType)
        {
            CheckDisposed();
            if (ifType.Type == typeof(Zetbox.App.Base.Blob))
                throw new InvalidOperationException("Creating a Blob is not supported. Use CreateBlob() instead");

            ObjectClass cls = metaDataResolver.GetObjectClass(ifType).GetRootClass();
            if (identityStore != null && cls.HasAccessControlList() && !cls.GetGroupAccessRights(identityStore).HasCreateRights())
            {
                throw new System.Security.SecurityException(string.Format("The current identity has no rights to create an Object of type '{0}'", ifType.Type.FullName));
            }
            return (IDataObject)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public virtual T Create<T>() where T : class, IDataObject
        {
            CheckDisposed();
            return (T)Create(iftFactory(typeof(T)));
        }

        /// <inheritdoc />
        public IPersistenceObject CreateUnattached(InterfaceType ifType)
        {
            CheckDisposed();
            return (IPersistenceObject)CreateUnattachedInstance(ifType);
        }

        /// <inheritdoc />
        public T CreateUnattached<T>()
            where T : class, IPersistenceObject
        {
            CheckDisposed();
            return (T)CreateUnattachedInstance(iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new IPersistenceObject by System.Type.
        /// </summary>
        /// <param name="ifType">Interface type of the new IPersistenceObject</param>
        /// <returns>A new IPersistenceObject</returns>
        public virtual IRelationEntry CreateRelationCollectionEntry(InterfaceType ifType)
        {
            CheckDisposed();
            return (IRelationEntry)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates a new IPersistenceObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IPersistenceObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public virtual T CreateRelationCollectionEntry<T>() where T : IRelationEntry
        {
            CheckDisposed();
            return (T)CreateRelationCollectionEntry(iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new IPersistenceObject by System.Type.
        /// </summary>
        /// <param name="ifType">Interface type of the new IPersistenceObject</param>
        /// <returns>A new IPersistenceObject</returns>
        public virtual IValueCollectionEntry CreateValueCollectionEntry(InterfaceType ifType)
        {
            CheckDisposed();
            return (IValueCollectionEntry)CreateInternal(ifType);
        }

        /// <summary>
        /// Creates a new IPersistenceObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IPersistenceObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public virtual T CreateValueCollectionEntry<T>() where T : IValueCollectionEntry
        {
            CheckDisposed();
            return (T)CreateValueCollectionEntry(iftFactory(typeof(T)));
        }

        /// <summary>
        /// Creates a new CompoundObject by Type
        /// </summary>
        /// <param name="ifType">Type of the new CompoundObject</param>
        /// <returns>A new CompoundObject</returns>
        public virtual ICompoundObject CreateCompoundObject(InterfaceType ifType)
        {
            CheckDisposed();
            return (ICompoundObject)CreateUnattachedInstance(ifType);
        }

        /// <summary>
        /// Creates a new CompoundObject.
        /// </summary>
        /// <typeparam name="T">Type of the new CompoundObject</typeparam>
        /// <returns>A new CompoundObject</returns>
        public virtual T CreateCompoundObject<T>() where T : ICompoundObject
        {
            CheckDisposed();
            return (T)CreateCompoundObject(iftFactory(typeof(T)));
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public abstract IDataObject Find(InterfaceType ifType, int ID);

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IZetboxContext Methods.
        /// This could be moved to a common abstract IZetboxContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public abstract T Find<T>(int ID) where T : class, IDataObject;

        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID);
        /// <summary>
        /// Find the Persistence Object of the given type by ID
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract T FindPersistenceObject<T>(int ID) where T : class, IPersistenceObject;

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid);
        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract T FindPersistenceObject<T>(Guid exportGuid) where T : class, IPersistenceObject;

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids);
        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public abstract IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids) where T : class, IPersistenceObject;

        public int CreateBlob(Stream s, string filename, string mimetype)
        {
            CheckDisposed();
            if (s == null)
                throw new ArgumentNullException("s");
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");
            if (string.IsNullOrEmpty(mimetype))
                throw new ArgumentNullException("mimetype");

            var blob = (Zetbox.App.Base.Blob)this.CreateInternal(iftFactory(typeof(Zetbox.App.Base.Blob)));
            blob.OriginalName = filename;
            blob.MimeType = mimetype;
            blob.StoragePath = this.Internals().StoreBlobStream(s, blob.ExportGuid, DateTime.Today /* but should be blob.CreatedOn. Around midnight the path may differ */, filename);

            return blob.ID;
        }

        string IZetboxContextInternals.StoreBlobStream(Stream s, Guid exportGuid, DateTime timestamp, string filename)
        {
            if (exportGuid == Guid.Empty) throw new ArgumentOutOfRangeException("exportGuid", "exportGuid cannot be empty");
            if (timestamp == DateTime.MinValue) throw new ArgumentOutOfRangeException("timestamp", "timestamp cannot be empty");

            var storagePath = BuildStoragePath(exportGuid, timestamp, filename);
            string path = Path.Combine(config.Server.DocumentStore, storagePath);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }

            using (var file = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                file.SetLength(0);
                s.CopyTo(file);
            }
            File.SetAttributes(path, FileAttributes.ReadOnly);
            return storagePath;
        }

        private static string BuildStoragePath(Guid exportGuid, DateTime timestamp, string filename)
        {
            var storagePath = Helper.PathCombine(timestamp.Year.ToString("0000"), timestamp.Month.ToString("00"), timestamp.Day.ToString("00"), String.Format("({0}) - {1}", exportGuid, filename));
            return storagePath;
        }

        public int CreateBlob(FileInfo fi, string mimetype)
        {
            CheckDisposed();
            if (fi == null)
                throw new ArgumentNullException("fi");
            using (var s = fi.OpenRead())
            {
                return CreateBlob(s, fi.Name, mimetype);
            }
        }

        public Stream GetStream(int ID)
        {
            CheckDisposed();
            return GetFileInfo(ID).OpenRead();
        }

        public FileInfo GetFileInfo(int ID)
        {
            CheckDisposed();
            var blob = this.Find<Zetbox.App.Base.Blob>(ID);
            var storagePath = BuildStoragePath(blob.ExportGuid, blob.CreatedOn, blob.OriginalName);
            string path = Path.Combine(config.Server.DocumentStore, storagePath);
            return new FileInfo(path);
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

        public InterfaceType GetInterfaceType(IPersistenceObject obj)
        {
            CheckDisposed();
            return iftFactory(((BasePersistenceObject)obj).GetImplementedInterface());
        }

        public InterfaceType GetInterfaceType(ICompoundObject obj)
        {
            CheckDisposed();
            return iftFactory(((BaseCompoundObject)obj).GetImplementedInterface());
        }

        public InterfaceType GetInterfaceType(Type t)
        {
            CheckDisposed();
            return iftFactory(t);
        }

        public InterfaceType GetInterfaceType(string typeName)
        {
            CheckDisposed();
            return iftFactory(Type.GetType(typeName + "," + typeof(ObjectClass).Assembly.FullName, true));
        }

        public abstract ImplementationType GetImplementationType(Type t);
        public abstract ImplementationType ToImplementationType(InterfaceType t);

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

        /// <summary>
        /// Indicates that the Zetbox Context has some modified, added or deleted items
        /// </summary>
        public bool IsModified { get; private set; }

        /// <summary>
        /// Is fires when <see cref="IsModified"/> was changed
        /// </summary>
        public event EventHandler IsModifiedChanged;

        #region IZetboxContextInternals Members

        void IZetboxContextInternals.SetModified(IPersistenceObject obj)
        {
            CheckDisposed();
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

        #endregion

        #region SequenceNumber
        private Sequence GetSequence(Guid sequenceGuid)
        {
            if (sequenceGuid == Guid.Empty) throw new ArgumentNullException("sequenceGuid");
            var s = lazyCtx().FindPersistenceObject<Sequence>(sequenceGuid);
            if (s == null) throw new ArgumentOutOfRangeException("sequenceGuid");
            return s;
        }

        protected abstract int ExecGetSequenceNumber(Guid sequenceGuid);
        protected abstract int ExecGetContinuousSequenceNumber(Guid sequenceGuid);

        public virtual int GetSequenceNumber(Guid sequenceGuid)
        {
            var s = GetSequence(sequenceGuid);
            if (s.IsContinuous) throw new InvalidOperationException("Sequence is a continuous sequence. use GetContinuousSequenceNumber instead.");

            bool isItMyTransaction = !IsTransactionRunning;
            if (isItMyTransaction)
                BeginTransaction();

            try
            {
                return ExecGetSequenceNumber(sequenceGuid);
            }
            finally
            {
                if (isItMyTransaction)
                    CommitTransaction();
            }
        }

        public virtual int GetContinuousSequenceNumber(Guid sequenceGuid)
        {
            var s = GetSequence(sequenceGuid);
            if (!s.IsContinuous) throw new InvalidOperationException("Sequence is no continuous sequence. use GetSequenceNumber instead.");
            if (!IsTransactionRunning) throw new InvalidOperationException("No transaction is running");

            return ExecGetContinuousSequenceNumber(sequenceGuid);
        }
        #endregion

        #region Transaction/Connection management
        protected abstract bool IsTransactionRunning { get; }

        public abstract void BeginTransaction();
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();
        #endregion

        public Identity Identity
        {
            get { return this.identityStore; }
        }

        public void SetElevatedMode(bool elevatedMode)
        {
        }
        public bool IsElevatedMode { get { return true; } }
        public event EventHandler IsElevatedModeChanged { add { } remove { } }
    }
}
