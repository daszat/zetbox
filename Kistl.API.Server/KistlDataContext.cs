using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Kistl.API.Configuration;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

namespace Kistl.API.Server
{
    public class KistlDataContext : ObjectContext, IKistlContext, IDisposable
    {
        [ThreadStatic]
        private static KistlDataContext _Current = null;
        private static string connectionString = "";

        public static KistlDataContext Current
        {
            get
            {
                if (_Current == null)
                {
                    throw new InvalidOperationException("No Session");
                }
                return _Current;
            }
        }

        public static KistlDataContext InitSession()
        {
            if (_Current != null) throw new InvalidOperationException("Session already set");
            _Current = GetContext();
            return _Current;
        }

        public static KistlDataContext GetContext()
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
            return new KistlDataContext(connectionString);
        }

        #region IDisposable Members
        // Special code to dispose of ThreadStatic instance
        void IDisposable.Dispose()
        {
            if (_Current != null) _Current.Dispose();
            base.Dispose();
            _Current = null;
        }

        #endregion


        private KistlDataContext(string connectionString) :
            base(connectionString, "Entities")
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
        public IQueryable<T> GetTable<T>()
        {
            if (!_table.ContainsKey(typeof(T)))
            {
                Type t = GetRootType(typeof(T));
                _table[typeof(T)] = this.CreateQuery<T>("[" + t.Name + "]");
            }

            return ((ObjectQuery<T>)_table[typeof(T)]).OfType<T>();
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

        public void Attach(IDataObject obj)
        {
            // Do nothing yet
        }

        public void Dettach(IDataObject obj)
        {
            // Do nothing yet
        }

        #endregion
    }
}
