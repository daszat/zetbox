
namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    using Zetbox.API.Utils;
    using Zetbox.API;

    /// <summary>
    /// Server Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Handles an Error
        /// </summary>
        /// <param name="ex">Exception to handle</param>
        public static void ThrowFaultException(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            Logging.Log.Error("Error in Facade: " + ex.Message, ex);

            if (ex is ConcurrencyException)
            {
                throw new FaultException<ConcurrencyException>((ConcurrencyException)ex);
            }
            else if (ex is InvalidZetboxGeneratedVersionException)
            {
                throw new FaultException<InvalidZetboxGeneratedVersionException>((InvalidZetboxGeneratedVersionException)ex);
            }
            else
            {
#if DEBUG
                if (ex is System.Data.DataException && ex.InnerException != null)
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
