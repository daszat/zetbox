using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class KistlDataContext : System.Data.Linq.DataContext
    {
        public KistlDataContext(string fileorconnectionstring) : base(fileorconnectionstring)
        {
        }

        public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        {
            base.SubmitChanges(failureMode);
        }
    }

    
}
