using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.API.Client
{
    public class ClientHelper
    {
        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static BaseClientDataObject NewBaseClientDataObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameClientDataObject)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type.FullNameClientDataObject);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            BaseClientDataObject obj = Activator.CreateInstance(t) as BaseClientDataObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }

    public static class ClientExtensionHelper
    {
    }
}
