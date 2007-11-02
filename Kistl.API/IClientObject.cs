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
        IDataObject CreateNew();
    }

    public delegate void ClientObjectHandler<T>(T obj) where T : class, IDataObject, new();

    public class ClientObject<T> : IClientObject where T : class, IDataObject, new()
    {
        public ClientObject()
        {
            API.ObjectBrokerFactory.Current.AttachEvents(this);
        }

        /// <summary>
        /// Generic Helper Method
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IEnumerable GetArrayFromXML(string xml)
        {
            IEnumerable result = xml.FromXmlString<List<T>>();
            return result;
        }

        /// <summary>
        /// Generic Helper Method
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IDataObject GetObjectFromXML(string xml)
        {
            T obj = xml.FromXmlString<T>();
            return obj;
        }

        /// <summary>
        /// Generic Helper Method
        /// </summary>
        /// <returns></returns>
        public IDataObject CreateNew()
        {
            T obj = new T();
            return obj;
        }
    }
}
