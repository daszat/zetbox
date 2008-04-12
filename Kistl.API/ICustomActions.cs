using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Interface for a Custom Action Manager. Every Client and Server host must provide a Custom Action Manager.
    /// </summary>
    public interface ICustomActionsManager
    {
        /// <summary>
        /// Should Attach using Metadata.
        /// Detaching is done through the Garbage Collector.
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// </summary>
        /// <param name="obj"></param>
        void AttachEvents(IDataObject obj);
        /// <summary>
        /// Should load Metadata, create an Instance and cache Metadata for future use.
        /// </summary>
        void Init();
    }

    /// <summary>
    /// CustomActionsManager Factory/Singleton. Must be initialised by each of the Server and Client Hosts.
    /// </summary>
    public sealed class CustomActionsManagerFactory
    {
        /// <summary>
        /// Singelton for ICustomActionsManager.
        /// </summary>
        private static ICustomActionsManager _manager = null;

        /// <summary>
        /// Initializes a Custom Actions Manager. Throws a InvalidOperationException if invoked twice.
        /// </summary>
        /// <param name="manager">A Custom Actions Manager Implementation</param>
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
        /// Current CustomActionsManager. Throws a InvalidOperationException if Init() was not called.
        /// </summary>
        public static ICustomActionsManager Current
        {
            get
            {
                if (_manager == null) throw new InvalidOperationException("ICustomActionsManager.Init() was not called");
                return _manager;
            }
        }

        /// <summary>
        /// prevent this class from being instantiated
        /// </summary>
        private CustomActionsManagerFactory() { }
    }


}
