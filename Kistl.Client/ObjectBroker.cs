using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client
{
    /// <summary>
    /// Implementierung des Clientseitigen ObjectBrokers
    /// </summary>
    internal class ObjectBrokerClient : API.IObjectBroker
    {
        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IClientObject GetClientObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            IClientObject obj = Activator.CreateInstance(t) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }

        #region IObjectBroker Members

        /// <summary>
        /// Attach lt. Metadaten
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(IDataObject obj)
        {
            // TODO: lt. Metadaten
            API.ICustomClientActions actions = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomClientActions, Kistl.App.Projekte")) as API.ICustomClientActions;
            actions.Attach(obj);
        }

        /// <summary>
        /// Attach lt. Metadaten
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(IClientObject obj)
        {
            // TODO: Lt. Metadaten dynamisch laden
            API.ICustomClientActions actions = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomClientActions, Kistl.App.Projekte")) as API.ICustomClientActions;
            customActions.Attach(obj);
        }

        /// <summary>
        /// TODO: evtl. doch entfernen :-)
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(IServerObject obj)
        {
            throw new InvalidOperationException("Wrong Object broker - I'm a Client Object Broker");
        }

        #endregion
    }
}
