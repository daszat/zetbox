using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Server;
using Kistl.API.Utils;
using Kistl.App.Extensions;
using Kistl.App.Base;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

namespace Kistl.DalProvider.EF
{
    internal class EFObjectContext : ObjectContext
    {
        /// <summary>
        /// Private Connectionstring
        /// </summary>
        private static string connectionString = String.Empty;

        public EFObjectContext(KistlConfig config)
            : base(GetConnectionString(config), "Entities")
        {
        }

        /// <summary>
        /// Creates the Connectionstring.
        /// <remarks>Format is: metadata=res://*;provider={provider};provider connection string='{Provider Connectionstring}'</remarks>
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString(KistlConfig config)
        {
            // Build connectionString
            // metadata=res://*;provider=System.Data.SqlClient;provider connection string='Data Source=.\SQLEXPRESS;Initial Catalog=Kistl;Integrated Security=True;MultipleActiveResultSets=true;'
            if (string.IsNullOrEmpty(connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("metadata=res://*;");
                sb.AppendFormat("provider={0};", config.Server.DatabaseProvider);
                sb.AppendFormat("provider connection string='{0}'", config.Server.ConnectionString);

                connectionString = sb.ToString();
            }
            return connectionString;
        }
    }

    /// <summary>
    /// Entityframework IKistlContext implementation
    /// </summary>
    public sealed class KistlDataContext : BaseKistlDataContext, IKistlContext, IDisposable
    {
        private readonly EFObjectContext _ctx;
        private readonly IMetaDataResolver _metaDataResolver;

        /// <summary>
        /// For Clean Up Session
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            if (_ctx != null) { _ctx.Dispose(); }
        }

        /// <summary>
        /// Internal Constructor
        /// </summary>
        public KistlDataContext(KistlConfig config, IMetaDataResolver metaDataResolver, Identity identity)
            : base(identity)
        {
            if (metaDataResolver == null) { throw new ArgumentNullException("metaDataResolver"); }
            
            _metaDataResolver = metaDataResolver;
            _ctx = new EFObjectContext(config);
        }

        internal ObjectContext ObjectContext { get { return _ctx; } }

        /// <summary>
        /// Type/Query cache
        /// </summary>
        private Dictionary<Type, object> _table = new Dictionary<Type, object>();

        /// <summary>
        /// Returns the Root Type of a given Type.
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>Root Type of the given Type</returns>
        private Type GetRootType(Type t)
        {
            while (t != null &&
                t.BaseType != typeof(BaseServerDataObject) &&
                t.BaseType != typeof(BaseServerDataObject_EntityFramework) &&
                t.BaseType != typeof(BaseServerCollectionEntry) &&
                t.BaseType != typeof(BaseServerCollectionEntry_EntityFramework)
                )
            {
                t = t.BaseType;
            }

            return t;
        }

        /// <summary>
        /// Returns the EntiySet Name of the given Type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>EntitySet Name</returns>
        private string GetEntityName(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            Type rootType = GetRootType(type);
            if (rootType == null) throw new ArgumentOutOfRangeException("type", string.Format("Unable to find root type of '{0}'", type.FullName));

            return rootType.Name.Remove(rootType.Name.Length - Kistl.API.Helper.ImplementationSuffix.Length, Kistl.API.Helper.ImplementationSuffix.Length);
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public override IQueryable<T> GetQuery<T>()
        {
            return GetPersistenceObjectQuery<T>();
        }


        /// <summary>
        /// Returns a Query by System.Type.
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;().</remarks>
        /// </summary>
        /// <param name="ifType">the interface type to query for</param>
        /// <returns>IQueryable</returns>
        public override IQueryable<IDataObject> GetQuery(InterfaceType ifType)
        {
            MethodInfo mi = this.GetType().FindGenericMethod("GetListHack", new Type[] { ifType.Type }, new Type[] { });
            // See Case 552
            var result = (System.Collections.IList)mi.Invoke(this, new object[] { });
            return result.AsQueryable().Cast<IDataObject>();
            // use OfType instead of "safe" cast because of 
            // http://social.msdn.microsoft.com/Forums/en-US/adodotnetentityframework/thread/b3537995-2441-423d-8485-ee285cf2f4ba/
            //return result.OfType<IDataObject>();

            //throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>().");

            // Des geht a net...
            //Type type = objType.ToImplementationType();

            //// Unable to cache - cannot cast from/to IQueryable<IDataObject> <-> IQueryable<T>
            //IQueryable<IDataObject> query = new QueryTranslator<IDataObject>(
            //    this.CreateQuery<BaseServerDataObject>("[" + GetEntityName(type) + "]"));
            //// This doesn't work without "OfType"
            //// The reason is that "GetEntityName" returns a Query to the baseobject 
            //// but maybe a derived object is asked. OfType will filter this.
            //// return (ObjectQuery<T>)_table[type];
            //return query.AddOfType<IDataObject>(objType);
        }

        /// <summary>
        /// Returns a PersistenceObject Query by InterfaceType
        /// </summary>
        /// <param name="ifType">the interface to look for</param>
        /// <returns>IQueryable</returns>
        public override IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            MethodInfo mi = this.GetType().FindGenericMethod("GetListHack", new Type[] { ifType.Type }, new Type[] { });
            // See Case 552
            var result = (System.Collections.IList)mi.Invoke(this, new object[] { });
            return result.AsQueryable().Cast<IPersistenceObject>();
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public override IQueryable<T> GetPersistenceObjectQuery<T>()
        {
            Type type = typeof(T).ToImplementationType();

            if (!_table.ContainsKey(type))
            {
                _table[type] = new QueryTranslator<T>(
                    _metaDataResolver, this.identity,
                    _ctx.CreateQuery<BaseServerDataObject_EntityFramework>("[" + GetEntityName(type) + "]"), this);
            }

            // This doesn't work without "OfType"
            // The reason is that "GetEntityName" returns a Query to the baseobject 
            // but maybe a derived object is asked. OfType will filter this.
            // return (ObjectQuery<T>)_table[type];
            return ((IQueryable<T>)_table[type]).OfType<T>();
        }


        public System.Collections.IList GetListHack<T>()
        {
            Type type = typeof(T).ToImplementationType();

            if (!_table.ContainsKey(type))
            {
                _table[type] = new QueryTranslator<T>(
                    _metaDataResolver, this.identity,
                    _ctx.CreateQuery<BaseServerDataObject_EntityFramework>("[" + GetEntityName(type) + "]"), this);
            }

            // This doesn't work without "OfType"
            // The reason is that "GetEntityName" returns a Query to the baseobject 
            // but maybe a derived object is asked. OfType will filter this.
            // return (ObjectQuery<T>)_table[type];
            return ((IQueryable<T>)_table[type]).OfType<T>().ToList();
        }

        /// <summary>
        /// TODO: Create new override
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relationId"></param>
        /// <param name="role"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent)
        {
            if (parent == null)
            {
                return this.GetPersistenceObjectQuery<T>().ToList();
            }
            else
            {
                throw new NotImplementedException();
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
        public override IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            return AttachedObjects.Where(obj => obj.GetInterfaceType() == type && obj.ID == ID).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistenceObject> AttachedObjects
        {
            get
            {
                // Must use OfType -> ObjectStateManager also contains RelationshipEntities
                return _ctx.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted | EntityState.Unchanged)
                    .Select(e => e.Entity).OfType<IPersistenceObject>();
            }
        }


        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public override int SubmitChanges()
        {
            DebugTraceChangedObjects();

            var notifySaveList = _ctx.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Select(e => e.Entity)
                .OfType<IDataObject>()
                .ToList();

            var addedList = _ctx.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added)
                .Select(e => e.Entity)
                .OfType<IDataObject>()
                .ToList();

            NotifyChanging(notifySaveList);

            int result = 0;
            try
            {
                result = _ctx.SaveChanges();
            }
            catch (UpdateException updex)
            {
                Logging.Log.Error("Error during SubmitChanges", updex);
                throw updex.InnerException;
            }

            NotifyChanged(notifySaveList);

            return result;
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects.
        /// This method does not fire any events or methods on added/changed objects. 
        /// It also does not change any IChanged property.
        /// </summary>
        /// <remarks>
        /// Only IDataObjects are counded.
        /// </remarks>
        /// <returns>Number of affected Objects</returns>
        public override int SubmitRestore()
        {
            DebugTraceChangedObjects();

            try
            {
                return _ctx.SaveChanges();
            }
            catch (UpdateException updex)
            {
                Logging.Log.Error("Error during SubmitChanges", updex);
                throw updex.InnerException;
            }
        }

        private void DebugTraceChangedObjects()
        {
            if (Logging.Log.IsDebugEnabled)
            {
                if (_ctx.ObjectStateManager
                    .GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted)
                    .Count() > 0)
                {
                    Logging.Log.Debug("************************* >>>> Submit Changes ******************************");

                    #region Details
                    //foreach (var msgPair in new[]{
                    //    new{State = EntityState.Added,      Label="Insert" },
                    //    new{State = EntityState.Modified,   Label="Update" },
                    //    new{State = EntityState.Deleted,    Label="Delete" },
                    //    // new{State = EntityState.Unchanged,  Label="Unchanged" },
                    //})
                    //{
                    //    var debugList = _ctx.ObjectStateManager
                    //        .GetObjectStateEntries(msgPair.State)
                    //        .Where(e => e.Entity != null)
                    //        .Select(e => e.Entity);
                    //    foreach (var i in debugList)
                    //    {
                    //        Logging.Log.InfoFormat("{0}: {1} -> {2}", msgPair.Label, i.GetType(), i.ToString());
                    //    }
                    //}
                    // Logging.Log.Info(String.Empty);
                    #endregion

                    Logging.Log.Debug("  Added: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Count());
                    Logging.Log.Debug("  Modified: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Count());
                    Logging.Log.Debug("  Deleted: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted).Count());
                    Logging.Log.Debug("  Unchanged: " + _ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged).Count());

                    Logging.Log.Debug("************************* Submit Changes <<<< ******************************");
                }
                else
                {
                    Logging.Log.Debug("**** >>>> Empty Submit Changes <<<< ****");
                }
            }
            else
            {
                Logging.Log.Info("Changes submitted successfully");
            }
        }


        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check requiered.
        /// </summary>
        /// <param name="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        public override IPersistenceObject Attach(IPersistenceObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            var serverObj = (BaseServerPersistenceObject)obj;
            string entityName = GetEntityName(obj.GetType());

            if (serverObj.ClientObjectState == DataObjectState.New
                || serverObj.ClientObjectState == DataObjectState.NotDeserialized)
            {
                _ctx.AddObject(entityName, obj);
            }
            else if (serverObj.ClientObjectState == DataObjectState.Deleted)
            {
                _ctx.AttachTo(entityName, obj);
                _ctx.DeleteObject(obj);
            }
            else
            {
                _ctx.AttachTo(entityName, obj);
            }

            return base.Attach(obj);
        }

        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        public override void Detach(IPersistenceObject obj)
        {
            _ctx.Detach(obj);
            base.Detach(obj);
        }

        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IPersistenceObject</param>
        public override void Delete(IPersistenceObject obj)
        {
            _ctx.DeleteObject(obj);
            base.Delete(obj);
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override IDataObject Find(InterfaceType ifType, int ID)
        {
            try
            {
                // See Case 552
                return (IDataObject)this.GetType().FindGenericMethod("Find", new Type[] { ifType.Type }, new Type[] { typeof(int) }).Invoke(this, new object[] { ID });
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override T Find<T>(int ID)
        {
            try
            {
                return AttachedObjects.OfType<T>().SingleOrDefault(o => o.ID == ID)
                    ?? GetQuery<T>().First(o => o.ID == ID);
            }
            catch (InvalidOperationException)
            {
                // if the IOEx happened because there is no such object, we 
                // want to report this with an ArgumentOutOfRangeException, 
                // else, we just want to pass the exception on.
                // Since we do not want to rely on the exception string, 
                // we have to check whether there is _any_ object with that ID
                if (GetQuery<T>().Count(o => o.ID == ID) == 0)
                {
                    throw new ArgumentOutOfRangeException("ID", ID, "No such object");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            try
            {
                // See Case 552
                return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject", new Type[] { ifType.Type }, new Type[] { typeof(int) }).Invoke(this, new object[] { ID });
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override T FindPersistenceObject<T>(int ID)
        {
            try
            {
                return AttachedObjects.OfType<T>().SingleOrDefault(o => o.ID == ID)
                    ?? GetPersistenceObjectQuery<T>().First(o => o.ID == ID);
            }
            catch (InvalidOperationException)
            {
                // if the IOEx happened because there is no such object, we 
                // want to report this with an ArgumentOutOfRangeException, 
                // else, we just want to pass the exception on.
                // Since we do not want to rely on the exception string, 
                // we have to check whether there is _any_ object with that ID
                if (GetPersistenceObjectQuery<T>().Count(o => o.ID == ID) == 0)
                {
                    throw new ArgumentOutOfRangeException("ID", ID, "No such object");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            try
            {
                // See Case 552
                return (IPersistenceObject)this.GetType().FindGenericMethod("FindPersistenceObject", new Type[] { ifType.Type }, new Type[] { typeof(Guid) }).Invoke(this, new object[] { exportGuid });
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find the Persistence Object of the given type by an ExportGuid
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuid">ExportGuid of the Object to find.</param>
        /// <returns>IPersistenceObject or null if the Object was not found.</returns>
        public override T FindPersistenceObject<T>(Guid exportGuid)
        {
            T result = (T)AttachedObjects.OfType<T>().OfType<IExportableInternal>().SingleOrDefault(o => o.ExportGuid == exportGuid);
            if (result == null)
            {
                // TODO: Case #1174
                var tmp = GetPersistenceObjectQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
                string sql = string.Format("SELECT VALUE e FROM Entities.{0} AS e WHERE e.ExportGuid = @guid", GetEntityName(typeof(T).ToImplementationType()));
                result = _ctx.CreateQuery<T>(sql, new System.Data.Objects.ObjectParameter("guid", exportGuid)).FirstOrDefault();
                if (result != null) result.AttachToContext(this);
            }
            return result;
        }

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <param name="ifType">Object Type of the Object to find.</param>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public override IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            try
            {
                // See Case 552
                return ((IEnumerable)this.GetType().FindGenericMethod("FindPersistenceObjects", new Type[] { ifType.Type }, new Type[] { typeof(IEnumerable<Guid>) })
                    .Invoke(this, new object[] { exportGuids })).Cast<IPersistenceObject>();
            }
            catch (TargetInvocationException tiex)
            {
                // unwrap "business" exception
                throw tiex.InnerException;
            }
        }

        /// <summary>
        /// Find Persistence Objects of the given type by ExportGuids
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="exportGuids">ExportGuids of the Objects to find.</param>
        /// <returns>A List of IPersistenceObject.</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public override IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids)
        {
            if (exportGuids.Count() == 0) return new List<T>();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT VALUE e FROM Entities.{0} AS e WHERE e.ExportGuid IN {{", GetEntityName(typeof(T).ToImplementationType()));
            foreach (Guid g in exportGuids)
            {
                sql.AppendFormat("Guid'{0}',", g);
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append("}");
            IEnumerable<T> result = _ctx.CreateQuery<T>(sql.ToString()).ToList();
            foreach (IPersistenceObject obj in result)
            {
                obj.AttachToContext(this);
            }
            return result;
        }
    }
}
