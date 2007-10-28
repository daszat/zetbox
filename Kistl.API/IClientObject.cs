using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kistl.API
{
    public interface IClientObjectFactory
    {
        IClientObject GetClientObject();
    }

    public interface IClientObject
    {
        IEnumerable GetArrayFromXML(string xml);
        IDataObject GetObjectFromXML(string xml);
    }

    public delegate void ClientObjectHandler<T>(T obj) where T : class, IDataObject, new();

    public class ClientObject<T> : IClientObject where T : class, IDataObject, new()
    {
        public IEnumerable GetArrayFromXML(string xml)
        {
            IEnumerable result = xml.FromXmlString<List<T>>();
            foreach(T obj in result)
            {
                // TODO: To be continued
            }
            return result;
        }

        public IDataObject GetObjectFromXML(string xml)
        {
            return xml.FromXmlString<T>();
        }
    }
}
