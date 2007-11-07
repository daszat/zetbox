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
    internal class CustomActionsManagerClient : API.ICustomActionsManager
    {
        #region ICustomActionsManager Members

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

        #endregion
    }
}
