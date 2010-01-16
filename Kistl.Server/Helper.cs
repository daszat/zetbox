
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    using Kistl.API.Utils;

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
            Logging.Log.Error("Handling exception", ex);
            if (throwFault)
            {
#if DEBUG
                if (ex is System.Data.UpdateException && ex.InnerException != null)
                {
                    throw new FaultException(ex.InnerException.Message);
                }
                else
                {
                    throw new FaultException(ex.Message);
                }
#else
                throw new FaultException("An error ocurred while processing this request.");
#endif
            }
        }
    }
}
