using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kistl.API
{
    public class KistlDataContext : System.Data.Linq.DataContext
    {
        public KistlDataContext(string fileorconnectionstring) : base(fileorconnectionstring)
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
                obj.NotifyPreSave(this);
            }

            base.SubmitChanges(failureMode);

            foreach (IDataObject obj in saveList)
            {
                obj.NotifyPostSave(this);
            }

        }
    }

    
}
