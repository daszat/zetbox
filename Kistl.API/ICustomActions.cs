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
    public interface ICustomActionsManager
    {
        /// <summary>
        /// Should Attach using Metadata
        /// Detaching is done through the Garbage Collector
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// </summary>
        /// <param name="obj"></param>
        void AttachEvents(IDataObject obj);
        /// <summary>
        /// Should load Metadata, create an Instance and save
        /// </summary>
        void Init();
    }

    /// <summary>
    /// Verwaltet ein CustomActionsManager Objekt. Muss vom Client bzw. Server initialisiert werden.
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
            using (TraceClient.TraceHelper.TraceMethodCall("CustomActionsManagerFactory of {0}", manager.GetType().Name))
            {
                if (_manager != null) throw new InvalidOperationException("ICustomActionsManager.Init() was called twice");
                _manager = manager;
                _manager.Init();
            }
        }

        /// <summary>
        /// Gibt den aktuellen CustomActionsManager zurück
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
