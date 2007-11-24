using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.API.Client
{
    public static class ClientExtensionHelper
    {
        public static T GetObject<T>(this IDataObject obj, string propName) where T : class, IDataObject
        {
            IClientObject client = ClientObjectFactory.GetClientObject(new ObjectType(typeof(T).FullName));
            int? id = (int?)obj.GetType().GetProperty("fk_" + propName).GetValue(obj, null);
            if (id == null) return null;

            return (T)client.GetObjectGeneric(id.Value);
        }
    }
}
