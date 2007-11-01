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
        /// <summary>
        /// Adds Eventhandler
        /// </summary>
        /// <param name="obj"></param>
        protected void AttachClientEvents(IDataObject obj)
        {
            // TODO: lt. Metadaten
            API.ICustomClientActions actions = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomClientActions, Kistl.App.Projekte")) as API.ICustomClientActions;
            actions.Attach(obj);
        }

        /// <summary>
        /// Adds Evenhandler
        /// </summary>
        /// <param name="list"></param>
        protected void AttachClientEvents(IEnumerable list)
        {
            foreach (IDataObject obj in list)
            {
                // Add Eventhandler
                AttachClientEvents(obj);
            }
        }

        public IEnumerable GetArrayFromXML(string xml)
        {
            IEnumerable result = xml.FromXmlString<List<T>>();
            AttachClientEvents(result);
            return result;
        }

        public IDataObject GetObjectFromXML(string xml)
        {
            T obj = xml.FromXmlString<T>();
            AttachClientEvents(obj);
            return obj;
        }

        public IDataObject CreateNew()
        {
            T obj = new T();
            AttachClientEvents(obj);
            return obj;
        }
    }
}
