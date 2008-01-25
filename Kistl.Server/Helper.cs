using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.API.Server;
using System.ServiceModel;

namespace Kistl.Server
{
    /// <summary>
    /// Server Helper
    /// </summary>
    public class Helper
    {

        public static void HandleError(Exception ex)
        {
            HandleError(ex, false);
        }

        /// <summary>
        /// Handles an Error
        /// </summary>
        /// <param name="ex">Expeption to handle</param>
        public static void HandleError(Exception ex, bool throwFault)
        {
            System.Diagnostics.Trace.TraceError(ex.ToString());
            if (throwFault)
            {
                if (ex is ApplicationException)
                {
                    throw new FaultException<ApplicationException>(ex as ApplicationException, ex.Message);
                }
                else if (ex is System.Data.UpdateException && ex.InnerException != null)
                {
                    throw new FaultException(ex.InnerException.Message);
                }
                else
                {
#if DEBUG
                    throw new FaultException(ex.Message);
#else
                    throw new FaultException("Generic Error");
#endif
                }
            }
        }

        /// <summary>
        /// Path to temp. location for Code Generation.
        /// TODO: Hardcoded yet
        /// </summary>
        public static string CodeGenPath
        {
            get
            {
                return @"c:\temp\KistlCodeGen";
            }
        }

        public static List<ObjectClass> GetObjectHierarchie(KistlDataContext ctx, ObjectClass objClass)
        {
            List<ObjectClass> result = new List<ObjectClass>();
            while (objClass != null)
            {
                result.Add(objClass);
                objClass = objClass.BaseObjectClass;
            }

            result.Reverse();
            return result;
        }
    }
}
