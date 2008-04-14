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

        /// For Clearing Session
        void IDisposable.Dispose()
        {
            base.Dispose();
            KistlDataContext.ClearSession(this);
        }


        internal KistlDataContextEntityFramework() :
            base(GetConnectionString(), "Entities")
        {
        }

        private Dictionary<Type, object> _table = new Dictionary<Type, object>();

        private Type GetRootType(Type t)
        {
            while (t != null && t.BaseType != typeof(BaseServerDataObject))
            {
                t = t.BaseType;
            }

            return t;
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
                Type rootType = GetRootType(type);
                _table[type] = this.CreateQuery<T>("[" + rootType.Name + "]");
            }
            return ((ObjectQuery<T>)_table[type]).OfType<T>();
        }

        public IQueryable<IDataObject> GetQuery(ObjectType objType)
        {
            Type type = Type.GetType(objType.FullNameDataObject);
            Type rootType = GetRootType(type);
            
            return this.CreateQuery<IDataObject>("[" + rootType.Name + "]");
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName)
        {
            return obj.GetPropertyValue<IEnumerable>(propertyName).OfType<T>().ToList();
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName)
        {
            return GetListOf<T>((IDataObject)this.GetQuery(type).First(o => ((BaseServerDataObject)o).ID == ID), propertyName);
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

        public void Attach(IDataObject o)
        {
            BaseServerDataObject obj = (BaseServerDataObject)o;
            if (obj.ObjectState == DataObjectState.New)
            {
                base.AddObject(obj.EntitySetName, obj);
            }
            else
            {
                base.AttachTo(obj.EntitySetName, obj);
            }

            o.AttachToContext(this);
        }

        public void Detach(IDataObject o)
        {
            base.Detach(o);
            o.DetachFromContext(this);
        }

        public void Delete(IDataObject obj)
        {
            base.DeleteObject(obj);
        }

        public void Attach(ICollectionEntry e)
        {
            throw new NotSupportedException("Attaching and Detaching an ICollectionEntry is done automatically by the Context through attaching/detaching an IDataObject");
        }

        public void Detach(ICollectionEntry e)
        {
            throw new NotSupportedException("Attaching and Detaching an ICollectionEntry is done automatically by the Context through attaching/detaching an IDataObject");
        }

        public void Delete(ICollectionEntry e)
        {
            base.DeleteObject(e);
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
    }
}
