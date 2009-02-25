using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Kistl.API.Configuration;
using System.Data.Objects.DataClasses;
using System.Collections;
using Kistl.API;
using Kistl.API.Server;
using System.Reflection;
using System.Diagnostics;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

namespace Kistl.DALProvider.EF
{
    internal class EFObjectContext : ObjectContext
    {
        /// <summary>
        /// Private Connectionstring
        /// </summary>
        private static string connectionString = "";

        public EFObjectContext()
            : base(GetConnectionString(), "Entities")
        {
        }

        /// <summary>
        /// Creates the Connectionstring.
        /// <remarks>Format is: metadata=res://*;provider={provider};provider connection string='{Provider Connectionstring}'</remarks>
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            // Build connectionString
            // metadata=res://*;provider=System.Data.SqlClient;provider connection string='Data Source=.\SQLEXPRESS;Initial Catalog=Kistl;Integrated Security=True;MultipleActiveResultSets=true;'
            if (string.IsNullOrEmpty(connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("metadata=res://*;");
                sb.AppendFormat("provider={0};", ApplicationContext.Current.Configuration.Server.DatabaseProvider);
                sb.AppendFormat("provider connection string='{0}'", ApplicationContext.Current.Configuration.Server.ConnectionString);

                connectionString = sb.ToString();
            }
            return connectionString;
        }
    }

    /// <summary>
    /// Entityframework IKistlContext implementation
    /// </summary>
    public class KistlDataContext : BaseKistlDataContext, IKistlContext, IDisposable
    {        
        private EFObjectContext _ctx;

        /// <summary>
        /// For Clean Up Session
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            if (_ctx != null) _ctx.Dispose();
            _ctx = null;
        }

        /// <summary>
        /// Internal Constructor
        /// </summary>
        public KistlDataContext()
        {
            _ctx = new EFObjectContext();
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
            Type rootType = GetRootType(type);
            return rootType.Name.Remove(rootType.Name.Length - Kistl.API.Helper.ImplementationSuffix.Length, Kistl.API.Helper.ImplementationSuffix.Length); // ImplementationSuffix
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public override IQueryable<T> GetQuery<T>()
        {
            Type type = typeof(T).ToImplementationType();

            if (!_table.ContainsKey(type))
            {
                _table[type] = new QueryTranslator<T>(
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
                    _ctx.CreateQuery<BaseServerDataObject_EntityFramework>("[" + GetEntityName(type) + "]"), this);
            }

            // This doesn't work without "OfType"
            // The reason is that "GetEntityName" returns a Query to the baseobject 
            // but maybe a derived object is asked. OfType will filter this.
            // return (ObjectQuery<T>)_table[type];
            return ((IQueryable<T>)_table[type]).OfType<T>().ToList();
        }

        /// <summary>
        /// Returns a Query by System.Type.
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;().</remarks>
        /// </summary>
        /// <param name="objType">System.Type</param>
        /// <returns>IQueryable</returns>
        public override IQueryable<IDataObject> GetQuery(Type interfaceType)
        {
            MethodInfo mi = this.GetType().FindGenericMethod("GetListHack", new Type[] { interfaceType }, new Type[] { });
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

        public override ICollection<T> FetchRelation<A, B, T>(string collectionEntryClassName, RelationEndRole role, IDataObject parent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the given Object is already in that Context.
        /// </summary>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">ID</param>
        /// <returns>If ID is InvalidID (Object is not inititalized) then an Exception will be thrown.
        /// If the Object is already in that Context, the Object Instace is returned.
        /// If the Object is not in that Context, null is returned.</returns>
        public override IPersistenceObject ContainsObject(Type type, int ID)
        {
            return AttachedObjects.Where(obj => obj.GetType() == type && obj.ID == ID).SingleOrDefault();
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
                    .GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Modified | System.Data.EntityState.Deleted | System.Data.EntityState.Unchanged)
                    .Select(e => e.Entity).OfType<IPersistenceObject>();
            }
        }


        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public override int SubmitChanges()
        {
            List<IDataObject> saveList = _ctx.ObjectStateManager
                .GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Modified)
                .Select(e => e.Entity).OfType<IDataObject>().ToList();

#if DEBUG
            Debug.WriteLine("************************* >>>> Submit Changes ******************************");

            var submitList = _ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added);
            submitList.Select(e => e.Entity).OfType<IPersistenceObject>()
                .ForEach<object>(i => Debug.WriteLine(string.Format("Insert: {0} -> {1}", i.GetType(), i.ToString())));

            submitList = _ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified);
            submitList.Select(e => e.Entity).OfType<IPersistenceObject>()
                .ForEach<object>(i => Debug.WriteLine(string.Format("Update: {0} -> {1}", i.GetType(), i.ToString())));

            submitList = _ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Deleted);
            submitList.Select(e => e.Entity).OfType<IPersistenceObject>()
                .ForEach<object>(i => Debug.WriteLine(string.Format("Delete: {0} -> {1}", i.GetType(), i.ToString())));

            submitList = _ctx.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Unchanged);
            submitList.Select(e => e.Entity).OfType<IPersistenceObject>()
                .ForEach<object>(i => Debug.WriteLine(string.Format("Unchanged: {0} -> {1}", i.GetType(), i.ToString())));

            Debug.WriteLine("************************* Submit Changes <<<< ******************************");
#endif

            saveList.ForEach(obj => obj.NotifyPreSave());

            int result = _ctx.SaveChanges();

            saveList.ForEach(obj => obj.NotifyPostSave());

            return result;
        }

        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check requiered.
        /// </summary>
        /// <param name="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        public override IPersistenceObject Attach(IPersistenceObject obj)
        {
            // Fist Attach/Detach
            string entityName = GetEntityName(obj.GetType());
            if (obj.ObjectState == DataObjectState.New)
            {
                // http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2232129&SiteID=1
                // Another way to solve your dilemma would be to go with your second approach 
                // where you attach the whole graph, then detach the added things, 
                // *set the key to null*, and then add them back.  
                // Technically this would work, but it's less efficient than avoiding attaching 
                // the things which are new in the first place.
                IEntityStateObject entityObj = (IEntityStateObject)obj;
                if (entityObj.EntityState != System.Data.EntityState.Added)
                {
                    if (entityObj.EntityState != System.Data.EntityState.Detached)
                    {
                        _ctx.Detach(entityObj);
                        entityObj.EntityKey = null;
                    }

                    _ctx.AddObject(entityName, obj);
                }
            }
            else if (obj.ObjectState == DataObjectState.Deleted)
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
        /// <param name="type">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override IDataObject Find(Type type, int ID)
        {
            try
            {
                // See Case 552
                return (IDataObject)this.GetType().FindGenericMethod("Find", new Type[] { type }, new Type[] { typeof(int) }).Invoke(this, new object[] { ID });
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
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <typeparam name="T">Object Type of the Object to find.</typeparam>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public override T Find<T>(int ID)
        {
            try
            {
                return GetQuery<T>().First(o => o.ID == ID);
            }
            catch (InvalidOperationException)
            {
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
    }
}
