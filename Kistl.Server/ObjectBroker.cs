using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des Serverseitigen ObjectBrokers
    /// </summary>
    internal class ObjectBrokerServer : API.IObjectBroker
    {
        /// <summary>
        /// Helper Method for generic object access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static API.IServerObject GetServerObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            API.IServerObject obj = Activator.CreateInstance(t) as API.IServerObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }

        #region IObjectBroker Members

        /// <summary>
        /// Attach lt. Metadaten
        /// Und damit kann man dann auch security machen :-)
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(Kistl.API.IDataObject obj)
        {
            // TODO: lt. Metadaten
            API.ICustomServerActions actions = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomServerActions, Kistl.App.Projekte")) as API.ICustomServerActions;
            actions.Attach(obj);
        }

        /// <summary>
        /// TODO: evtl. doch entfernen :-)
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(Kistl.API.IClientObject obj)
        {
            throw new InvalidOperationException("Wrong Object broker - I'm a Server Object Broker");
        }

        /// <summary>
        /// Attach lt. Metadaten
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(Kistl.API.IServerObject obj)
        {
            // TODO: Lt. Metadaten dynamisch laden
            Kistl.API.ICustomServerActions customActions = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomServerActions, Kistl.App.Projekte")) as API.ICustomServerActions;
            customActions.Attach(obj);
        }

        #endregion
    }
}
