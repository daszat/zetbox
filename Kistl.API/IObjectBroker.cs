using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Das ist noch gemischt - sollte aber bald getrennt werden.
    /// Client & Server implementieren jeweils einen ObjektBroker, der allen Objekten
    /// Events attachen kann (Custom Actions).
    /// </summary>
    public interface IObjectBroker
    {
        void AttachEvents(IDataObject obj);
        void AttachEvents(IClientObject obj);
        void AttachEvents(IServerObject obj);
    }

    /// <summary>
    /// Verwaltet ein ObjectBroker Objekt. Muss vom Client bzw. Server initialisiert werden.
    /// </summary>
    public class ObjectBrokerFactory
    {
        private static IObjectBroker _broker = null;

        /// <summary>
        /// Inititalisierung des Brokers.
        /// </summary>
        /// <param name="broker">Broker</param>
        public static void Init(IObjectBroker broker)
        {
            if (_broker != null) throw new InvalidOperationException("ObjectBrokerFactory.Init() was called twice");
            _broker = broker;
        }

        /// <summary>
        /// Gibt den aktuellen Broker zurück
        /// </summary>
        public static IObjectBroker Current
        {
            get
            {
                if (_broker == null) throw new InvalidOperationException("ObjectBrokerFactory.Init() was not called");
                return _broker;
            }
        }
    }
}
