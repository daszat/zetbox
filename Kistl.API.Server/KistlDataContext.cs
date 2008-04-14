using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    public static class KistlDataContext
    {
        [ThreadStatic]
        private static IKistlContext _Current = null;

        public static IKistlContext Current
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

        public static IKistlContext InitSession()
        {
            if (_Current != null) throw new InvalidOperationException("Session already set");
            _Current = GetContext();
            return _Current;
        }

        public static void ClearSession(IKistlContext ctx)
        {
            if (_Current == ctx)
            {
                _Current = null;
            }
        }

        public static IKistlContext GetContext()
        {
            return new KistlDataContextEntityFramework();
        }
    }
}
