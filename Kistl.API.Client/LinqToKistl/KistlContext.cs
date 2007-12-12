using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public class KistlContext : IDisposable
    {
        public KistlContextQuery<BaseClientDataObject> GetQuery(ObjectType type)
        {
            return new KistlContextQuery<BaseClientDataObject>(this, type);
        }

        public KistlContextQuery<T> GetQuery<T>() where T : BaseClientDataObject
        {
            return new KistlContextQuery<T>(this, new ObjectType(typeof(T)));
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
