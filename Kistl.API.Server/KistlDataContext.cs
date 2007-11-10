using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kistl.API.Server
{
    public class KistlDataContext : System.Data.Linq.DataContext, IDisposable
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
            _Current = new KistlDataContext("Data Source=localhost\\sqlexpress; Initial Catalog=Kistl;Integrated Security=true");
            return _Current;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.Dispose();
            _Current = null;
        }

        #endregion


        private KistlDataContext(string fileorconnectionstring) : base(fileorconnectionstring)
        {
        }

        public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        {
            ChangeSet c = this.GetChangeSet();
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
            }

            base.SubmitChanges(failureMode);

            foreach (IDataObject obj in saveList)
            {
                obj.NotifyPostSave();
            }

        }
    }

    
}
