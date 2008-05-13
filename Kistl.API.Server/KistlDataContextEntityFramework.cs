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
    public class KistlDataContextEntityFramework : ObjectContext, IKistlContext, IDisposable
    {
        private static string connectionString = "";

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

        /// For Clean Up Session
        void IDisposable.Dispose()
        {
            base.Dispose();
            KistlDataContext.ClearSession(this);
            GC.SuppressFinalize(this);
        }


        internal KistlDataContextEntityFramework() :
            base(GetConnectionString(), "Entities")
        {
        }

        private Dictionary<Type, object> _table = new Dictionary<Type, object>();

        private Type GetRootType(Type t)
        {
            while (t != null && t.BaseType != typeof(BaseServerDataObject) && t.BaseType != typeof(BaseServerCollectionEntry))
            {
                t = t.BaseType;
            }

            return t;
        }

        private string GetEntityName(Type type)
        {
            Type rootType = GetRootType(type);
            return rootType.Name;
        }

        /// <summary>
        /// Return IQueryable to make it possible to use alternative LINQ Provider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            Type type = typeof(T);

            if (!_table.ContainsKey(type))
            {
                _table[type] = this.CreateQuery<T>("[" + GetEntityName(type) + "]");
            }
            return ((ObjectQuery<T>)_table[type]).OfType<T>();
        }

        public IQueryable<IDataObject> GetQuery(ObjectType objType)
        {
            throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>().");
            /*Type type = Type.GetType(objType.FullNameDataObject);
            return this.CreateQuery<IDataObject>("[" + GetEntityName(type) + "]");*/
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName) where T : IDataObject
        {
            return obj.GetPropertyValue<IEnumerable>(propertyName).OfType<T>().ToList();
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName) where T : IDataObject
        {
            IDataObject obj = (IDataObject)this.GetQuery(type).First(o => ((BaseServerDataObject)o).ID == ID);
            return GetListOf<T>(obj, propertyName);
        }

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
        /// The EntityFramework guarantees the all Objects are unique. No check requiered.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IPersistenceObject Attach(IPersistenceObject obj)
        {
            // Attach/Detach then Attach item
            string entityName = GetEntityName(obj.GetType());
            if (obj.ObjectState == DataObjectState.New)
            {
                /// http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2232129&SiteID=1
                /// Another way to solve your dilemma would be to go with your second approach 
                /// where you attach the whole graph, then detach the added things, 
                /// *set the key to null*, and then add them back.  
                /// Technically this would work, but it's less efficient than avoiding attaching 
                /// the things which are new in the first place.
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

            // Then call Attach on Subitems
            obj.AttachToContext(this);

            return obj;
        }

        public void Detach(IPersistenceObject obj)
        {
            base.Detach(obj);
            obj.DetachFromContext(this);
        }

        public void Delete(IPersistenceObject obj)
        {
            base.DeleteObject(obj);
        }

        public Kistl.API.IDataObject Create(Type type)
        {
            return Create(new ObjectType(type));
        }

        public Kistl.API.IDataObject Create(ObjectType type)
        {
            Kistl.API.IDataObject obj = type.NewDataObject();
            Attach(obj);
            return obj;
        }

        public T Create<T>() where T : Kistl.API.IDataObject, new()
        {
            T obj = new T();
            Attach(obj);
            return obj;
        }

        // TODO: This is quite redundant here as it only uses other IKistlContext Methods.
        // This could be moved to a common abstract IKistlContextBase
        public IDataObject Find(ObjectType type, int ID)
        {
            throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>()");
            // return GetQuery(type).First(o => o.ID == ID);
        }

        public T Find<T>(int ID)
            where T : IDataObject
        {
            throw new NotSupportedException("Entity Framework does not support queries on Interfaces. Please use GetQuery<T>()");
            // return GetQuery<T>().First(o => o.ID == ID);
        }

    }
}
