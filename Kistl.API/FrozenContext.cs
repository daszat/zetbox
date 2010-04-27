
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using Kistl.API.Utils;

    /// <summary>
    /// This exception is thrown when someone tries to modify a read only context.
    /// </summary>
    [Serializable]
    public class ReadOnlyContextException
        : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        public ReadOnlyContextException()
            : base("This context is readonly")
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        public ReadOnlyContextException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        /// <param name="inner">The exception that caused this exception.</param>
        public ReadOnlyContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        protected ReadOnlyContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// This exception is thrown when someone tries to modify a read only object.
    /// </summary>
    [Serializable]
    public class ReadOnlyObjectException
        : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        public ReadOnlyObjectException()
            : base("This object is readonly")
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        public ReadOnlyObjectException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        /// <param name="inner">The exception that caused this exception.</param>
        public ReadOnlyObjectException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        protected ReadOnlyObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// A static class to provide access to the FrozenContext singleton.
    /// </summary>
    public static class FrozenContext
    {
        /// <summary>
        /// The private backing store for the Single property.
        /// </summary>
        private static IReadOnlyKistlContext _single = null;

        /// <summary>
        /// A fallback context, if the Frozen assembly is not available.
        /// </summary>
        private static IReadOnlyKistlContext _fallback = null;

        /// <summary>
        /// A value indicating whether loading the frozen context was already tried.
        /// </summary>
        private static bool _haveTriedLoading = false;

        /// <summary>
        /// Gets the FrozenContext singleton. This is loaded on demand. If no 
        /// frozen context provider/assembly is available an optionally 
        /// registered fallback is used instead. If neither is available, an 
        /// InvalidOperationException is thrown.
        /// </summary>
        public static IReadOnlyKistlContext Single
        {
            get
            {
                if (!_haveTriedLoading)
                {
                    TryInit();
                }
                var result = _single ?? _fallback;
                if (result == null)
                {
                    // Now we're in a bind: we've got no frozen context or 
                    // fallback, but need to finish initialisation. Therefore 
                    // we use the current database as stand-in for the 
                    // frozen context. Hopefully we have data there and 
                    // performance isn't tooooo bad :)
                    Logging.Log.Warn("Frozen.Single called without frozen context assembly or fallback.");
                    Logging.Log.Warn("Falling back to direct database access. Expect poor performance.");
                    // TODO: Case 1211: Move KistlContext.GetContext from Kistl.Api.{Client,Server} into the API proper and use the config file to find the right context.
                }
                return result;
            }
        }

        /// <summary>
        /// Tries to initialise the FrozenContext singleton.
        /// </summary>
        /// <returns>the initialised frozen context or null</returns>
        private static IReadOnlyKistlContext TryInit()
        {
            using (Logging.Log.DebugTraceMethodCall())
            {
                string frozenAssemblyName = "Kistl.Objects.Frozen.FrozenContextImplementation, " + Kistl.API.Helper.FrozenAssembly;
                if (!_haveTriedLoading)
                {
                    try
                    {
                        _haveTriedLoading = true;
                        Type t = Type.GetType(frozenAssemblyName, true);
                        _single = (IReadOnlyKistlContext)Activator.CreateInstance(t);
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Error(String.Format("Error when trying to load frozen context: [{0}]", frozenAssemblyName), ex);
                        return null;
                    }
                }
            }

            return _single;
        }

        /// <summary>
        /// Registers a IKistlContext as fallback which can be used if the frozen context assembly is not available. This is especially useful while bootstrapping.
        /// </summary>
        /// <param name="ctx">the context to use as fallback</param>
        public static void RegisterFallback(IReadOnlyKistlContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }
            if (_fallback != null)
            {
                Logging.Log.Warn("Replacing FrozenContext fallback.");
            }
            _fallback = ctx;
        }
    }
}
