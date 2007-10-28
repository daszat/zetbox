using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kistl.API
{
    public interface IClientObject
    {
        IEnumerable GetArrayFromXML(string xml);
        IDataObject GetObjectFromXML(string xml);
    }

    public class ClientObjectHelper
    {
        public static IClientObject GetClientObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            IClientObject obj = Activator.CreateInstance(t) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }
}
