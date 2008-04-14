using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Kistl.API.Configuration;
using System.Data.Objects.DataClasses;

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

        #region IDisposable Members
        // Special code to dispose of ThreadStatic instance
        void IDisposable.Dispose()
        {
            base.Dispose();
            KistlDataContext.ClearSession(this);
        }

        #endregion


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
        public IQueryable<T> GetTable<T>() where T : IDataObject
        {
            if (!_table.ContainsKey(typeof(T)))
            {
                Type t = GetRootType(typeof(T));
                _table[typeof(T)] = this.CreateQuery<T>("[" + t.Name + "]");
            }

            return ((ObjectQuery<T>)_table[typeof(T)]).OfType<T>();
        }

        public IQueryable<T> GetQuery<T>() where T : IDataObject
        {
            return GetTable<T>();
        }

        public IQueryable<IDataObject> GetQuery(ObjectType type)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(IDataObject obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOf<T>(ObjectType type, int ID, string propertyName)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Überschreiben, damit ich meine eigene Feuerlogik einbauen kann
        /// & hoffen, dass die Microsofties das bald als virtuel markieren
        /// </summary>
        /// <returns></returns>
        public new int SaveChanges()
        {
            return SubmitChanges();
        }

        /// <summary>
        /// Überschreiben, damit ich meine eigene Feuerlogik einbauen kann
        /// & hoffen, dass die Microsofties das bald als virtuel markieren
        /// </summary>
        /// <returns></returns>
        public new int SaveChanges(bool acceptChanges)
        {
            return SubmitChanges();
        }

        public int SubmitChanges()
        {
            List<IDataObject> saveList = new List<IDataObject>();
            this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added)
                .ToList().ForEach(e => { if (e.Entity is IDataObject) saveList.Add(e.Entity as IDataObject); });
            this.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified)
                .ToList().ForEach(e => { if (e.Entity is IDataObject) saveList.Add(e.Entity as IDataObject); });

            foreach (IDataObject obj in saveList)
            {
                obj.NotifyPreSave();
            }

            int result = base.SaveChanges();

            foreach (IDataObject obj in saveList)
            {
                obj.NotifyPostSave();
            }

            return result;
        }

        #region IKistlContext Members

        public void Attach(IDataObject o)
        {
            BaseServerDataObject obj = (BaseServerDataObject)o;
            if (obj.ObjectState == DataObjectState.New)
            {
                this.AddObject(obj.EntitySetName, obj);
            }
            else
            {
                this.AttachTo(obj.EntitySetName, obj);
            }
        }

        public void Detach(IDataObject o)
        {
            throw new NotSupportedException();
        }

        public void Delete(IDataObject obj)
        {
            this.DeleteObject(obj);
        }

        public void Attach(ICollectionEntry e)
        {
            throw new NotSupportedException();
        }

        public void Detach(ICollectionEntry e)
        {
            throw new NotSupportedException();
        }

        public void Delete(ICollectionEntry e)
        {
            this.DeleteObject(e);
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

        #endregion
    }
}
