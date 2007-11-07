using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Interface für Server Custom Actions. Jedes Custom Actions Objekt muss selbst die 
    /// gewünschten Events attachen
    /// Getrennt, damit man ja nicht Server & Clientaktionen vermischt
    /// </summary>
    public interface ICustomServerActions
    {
        void Attach(IServerObject server);
        void Attach(IDataObject obj);
    }

    /// <summary>
    /// Interface für Client Custom Actions. Jedes Custom Actions Objekt muss selbst die 
    /// gewünschten Events attachen
    /// Getrennt, damit man ja nicht Server & Clientaktionen vermischt
    /// </summary>
    public interface ICustomClientActions
    {
        void Attach(IClientObject client);
        void Attach(IDataObject obj);
    }

    /// <summary>
    /// Das ist noch gemischt - sollte aber bald getrennt werden.
    /// Client & Server implementieren jeweils einen ObjektBroker, der allen Objekten
    /// Events attachen kann (Custom Actions).
    /// </summary>
    public interface ICustomActionsManager
    {
        void AttachEvents(IDataObject obj);
        void AttachEvents(IClientObject obj);
        void AttachEvents(IServerObject obj);
    }

    /// <summary>
    /// Verwaltet ein ObjectBroker Objekt. Muss vom Client bzw. Server initialisiert werden.
    /// </summary>
    public class CustomActionsManagerFactory
    {
        private static ICustomActionsManager _manager = null;

        /// <summary>
        /// Inititalisierung des Custom Actions Manager.
        /// </summary>
        /// <param name="broker">Broker</param>
        public static void Init(ICustomActionsManager manager)
        {
            if (_manager != null) throw new InvalidOperationException("ICustomActionsManager.Init() was called twice");
            _manager = manager;
        }

        /// <summary>
        /// Gibt den aktuellen Broker zurück
        /// </summary>
        public static ICustomActionsManager Current
        {
            get
            {
                if (_manager == null) throw new InvalidOperationException("ICustomActionsManager.Init() was not called");
                return _manager;
            }
        }
    }


}
