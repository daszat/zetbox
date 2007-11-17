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

        public void SubmitChanges()
        {
            /*ChangeSet c = this.ObjectStateManager.GetChangeSet();
            List<IDataObject> saveList = new List<IDataObject>();

            foreach(IDataObject obj in c.AddedEntities)
            {
                saveList.Add(obj);
            }

            foreach (IDataObject obj in c.ModifiedEntities)
            {
                saveList.Add(obj);
            }

            foreach (IDataObject obj in saveList)
            {
                obj.NotifyPreSave();
            }*/

            base.SaveChanges();

            /*
            foreach (IDataObject obj in saveList)
            {
                obj.NotifyPostSave();
            }*/

        }
    }
}
