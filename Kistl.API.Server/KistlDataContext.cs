using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

namespace Kistl.API.Server
{
    public class KistlDataContext : ObjectContext, IDisposable
    {
        [ThreadStatic]
        private static KistlDataContext _Current = null;

        public static KistlDataContext Current
        {
            get
            {
                if (_Current == null)
                {
                }
                return _Current;
            }
        }

        public static KistlDataContext InitSession()
        {
            _Current = new KistlDataContext();
            return _Current;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.Dispose();
            _Current = null;
        }

        #endregion


        private KistlDataContext() :
            base("name=KistlContext", "Entities")
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
            Type t = GetRootType(typeof(T));
            if (!_table.ContainsKey(t))
            {
                _table[t] = this.CreateQuery<T>("[" + t.Name + "]");
            }

            return (_table[t] as ObjectQuery<T>).OfType<T>();

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
                .ToList().ForEach(e => { if(e.Entity is IDataObject) saveList.Add(e.Entity as IDataObject); });

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
    }
}
