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

        public ObjectQuery<T> GetTable<T>()
        {
            Type t = typeof(T);
            if (!_table.ContainsKey(t))
            {
                _table[t] = this.CreateQuery<T>("[" + t.Name + "]");
            }

            return _table[t] as ObjectQuery<T>;

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
