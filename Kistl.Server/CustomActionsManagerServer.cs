using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des Serverseitigen ObjectBrokers
    /// </summary>
    internal class CustomActionsManagerServer : API.ICustomActionsManager
    {
        #region ICustomActionsManager Members

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
