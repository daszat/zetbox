using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.API;

namespace Kistl.Server
{
    /// <summary>
    /// Server Helper
    /// </summary>
    public sealed class Helper
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

        /// <summary>
        /// prevent this class from being instantiated
        /// </summary>
        private Helper() { }
    }

    /// <summary>
    /// C# Extensions
    /// </summary>
    public static class ExtensionHelpers
    {
        public static ICollection<ObjectClass> GetObjectHierarchie(this ObjectClass objClass, KistlDataContext ctx)
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

        public static ObjectType GetObjectType(this DataType objClass)
        {
            return new ObjectType(objClass.Module.Namespace, objClass.ClassName);
        }

        public static ObjectClass GetObjectClass(this ObjectType objType, KistlDataContext ctx)
        {
            return ctx.GetTable<ObjectClass>().First(o => o.Module.Namespace == objType.Namespace && o.ClassName == objType.Classname);
        }

        public static BaseProperty GetProperty(this ObjectClass c, KistlDataContext ctx, string property)
        {
            ObjectClass objClass = c;
            while (objClass != null)
            {
                BaseProperty prop = objClass.Properties.SingleOrDefault(p => p.PropertyName == property);
                if (prop != null)
                {
                    return prop;
                }
                objClass = objClass.BaseObjectClass;
            }

            return null;
        }
    }
}
