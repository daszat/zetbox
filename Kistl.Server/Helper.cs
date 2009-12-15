using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.API;
using System.Diagnostics;
using Kistl.API.Utils;

namespace Kistl.Server
{
    /// <summary>
    /// Server Helper
    /// </summary>
    public static class Helper
    {

        public static void HandleError(Exception ex)
        {
            HandleError(ex, false);
        }

        /// <summary>
        /// Handles an Error
        /// </summary>
        /// <param name="ex">Exception to handle</param>
        /// <param name="throwFault">whether or not to throw a <see cref="FaultException"/></param>
        public static void HandleError(Exception ex, bool throwFault)
        {
            Logging.Log.Error(ex.ToString());
            if (throwFault)
            {
                if (ex is System.Data.UpdateException && ex.InnerException != null)
                {
                    throw new FaultException(ex.InnerException.Message);
                }
                else
                {
#if DEBUG
                    throw new FaultException(ex.Message);
#else
                    throw new FaultException("An Error ocurred while processing this request.");
#endif
                }
            }
        }
    }
}
