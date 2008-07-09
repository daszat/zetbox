using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Kistl.API.Configuration;
using System.Data.Objects.DataClasses;
using System.Collections;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

namespace Kistl.API.Server
{
    /// <summary>
    /// Entityframework IKistlContext implementation
    /// </summary>
    public class KistlDataContextEntityFramework : ObjectContext, IKistlContext, IDisposable
    {
        /// <summary>
        /// Private Connectionstring
        /// </summary>
        private static string connectionString = "";

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
                sb.AppendFormat("provider={0};", KistlConfig.Current.Server.DatabaseProvider);
                sb.AppendFormat("provider connection string='{0}'", KistlConfig.Current.Server.ConnectionString);

                connectionString = sb.ToString();
            }
            return connectionString;
        }

        /// <summary>
        /// For Clean Up Session
        /// </summary>
        void IDisposable.Dispose()
        {
            base.Dispose();
            KistlDataContext.ClearSession(this);
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        /// <summary>
        /// Is true after Dispose() was called.
        /// </summary>
        public bool IsDisposed { get; private set; }


        /// <summary>
        /// Internal Constructor
        /// </summary>
        internal KistlDataContextEntityFramework() :
            base(GetConnectionString(), "Entities")
        {
        }

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
            while (t != null && t.BaseType != typeof(BaseServerDataObject) && t.BaseType != typeof(BaseServerCollectionEntry))
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
            return rootType.Name;
        }

        /// <summary>
        /// Returns a Query by T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>IQueryable</returns>
        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            Type type = typeof(T);

            if (!_table.ContainsKey(type))
            {
                _table[type] = this.CreateQuery<T>("[" + GetEntityName(type) + "]");
            }
            return ((ObjectQuery<T>)_table[type]).OfType<T>();
        }

        /// <summary>
        /// Returns a Query by System.Type.
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;().</remarks>
        /// </summary>
        /// <param name="objType">System.Type</param>
        /// <returns>IQueryable</returns>
        public IQueryable<IDataObject> GetQuery(Type objType)
        {
            throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>().");
        }

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="obj">Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            return obj.GetPropertyValue<IEnumerable>(propertyName).OfType<T>().ToList();
        }

        /// <summary>
        /// Returns the List of a BackReferenceProperty by the given Type, ID and PropertyName.
        /// </summary>
        /// <typeparam name="T">List Type of the BackReferenceProperty</typeparam>
        /// <param name="type">Type of the Object which holds the BackReferenceProperty</param>
        /// <param name="ID">ID of the Object which holds the BackReferenceProperty</param>
        /// <param name="propertyName">Propertyname which holds the BackReferenceProperty</param>
        /// <returns>A List of Objects</returns>
        public List<T> GetListOf<T>(Type type, int ID, string propertyName) where T : IDataObject
        {
            IDataObject obj = (IDataObject)this.GetQuery(type).First(o => o.ID == ID);
            return GetListOf<T>(obj, propertyName);
        }

        /// <summary>
        /// Submits the changes and returns the number of affected Objects. Note: only IDataObjects are counted.
        /// </summary>
        /// <returns>Number of affected Objects</returns>
        public int SubmitChanges()
        {
            List<IDataObject> saveList = new List<IDataObject>();

            saveList.AddRange(this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
                .Select(e => e.Entity).OfType<IDataObject>());
            saveList.AddRange(this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified)
                .Select(e => e.Entity).OfType<IDataObject>());

            saveList.ForEach(obj => obj.NotifyPreSave());

            int result = base.SaveChanges();

            saveList.ForEach(obj => obj.NotifyPostSave());

            return result;
        }

        /// <summary>
        /// Attach an IPersistenceObject. The EntityFramework guarantees the all Objects are unique. No check requiered.
        /// </summary>
        /// <param name="obj">Object to Attach</param>
        /// <returns>Object Attached</returns>
        public IPersistenceObject Attach(IPersistenceObject obj)
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
                EntityObject entityObj = (EntityObject)obj;
                if (entityObj.EntityState != System.Data.EntityState.Detached)
                {
                    base.Detach(entityObj);
                    entityObj.EntityKey = null;
                }

                base.AddObject(entityName, obj);
            }
            else if (obj.ObjectState == DataObjectState.Deleted)
            {
                base.DeleteObject(obj);
            }
            else
            {
                base.AttachTo(entityName, obj);
            }

            // then call Attach on Subitems
            obj.AttachToContext(this);

            return obj;
        }

        /// <summary>
        /// Detach an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IDataObject</param>
        public void Detach(IPersistenceObject obj)
        {
            base.Detach(obj);
            obj.DetachFromContext(this);
        }

        /// <summary>
        /// Delete an IPersistenceObject.
        /// </summary>
        /// <param name="obj">IPersistenceObject</param>
        public void Delete(IPersistenceObject obj)
        {
            base.DeleteObject(obj);
        }

        /// <summary>
        /// Creates a new IDataObject by System.Type. Note - this Method is depricated!
        /// </summary>
        /// <param name="type">System.Type of the new IDataObject</param>
        /// <returns>A new IDataObject</returns>
        public Kistl.API.IDataObject Create(Type type)
        {
            Kistl.API.IDataObject obj = (Kistl.API.IDataObject)Activator.CreateInstance(type);
            Attach(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new IDataObject.
        /// </summary>
        /// <typeparam name="T">Type of the new IDataObject</typeparam>
        /// <returns>A new IDataObject</returns>
        public T Create<T>() where T : Kistl.API.IDataObject, new()
        {
            T obj = new T();
            Attach(obj);
            return obj;
        }

        /// <summary>
        /// Find the Object of the given type by ID
        /// TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        /// This could be moved to a common abstract IKistlContextBase
        /// <remarks>Note: This Method is depricated.</remarks>
        /// <remarks>Entity Framework does not support queries on Interfaces. Please use GetQuery&lt;T&gt;()</remarks>
        /// </summary>
        /// <param name="type">Object Type of the Object to find.</param>
        /// <param name="ID">ID of the Object to find.</param>
        /// <returns>IDataObject. If the Object is not found, a Exception is thrown.</returns>
        public IDataObject Find(Type type, int ID)
        {
            throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>()");
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
        public T Find<T>(int ID)
            where T : IDataObject
        {
            throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>()");
        }

    }
}
