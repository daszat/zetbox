
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This exception is thrown when someone tries to modify a read only context.
    /// </summary>
    public class ReadOnlyContextException : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        public ReadOnlyContextException()
            : base("This context is readonly")
        {
        }
    }

    /// <summary>
    /// This exception is thrown when someone tries to modify a read only object.
    /// </summary>
    public class ReadOnlyObjectException : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        public ReadOnlyObjectException()
            : base("This object is readonly")
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
        private static IKistlContext _single = null;

        /// <summary>
        /// A fallback context, if the Frozen assembly is not available.
        /// </summary>
        private static IKistlContext _fallback = null;

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
        public static IKistlContext Single
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
                    const string errorMessage = "Frozen.Single called without frozen context assembly or fallback";
                    Trace.TraceError(errorMessage);
                    Debug.Assert(false, errorMessage);
                    throw new InvalidOperationException(errorMessage);
                }
                return result;
            }
        }

        /// <summary>
        /// Tries to initialise the FrozenContext singleton.
        /// </summary>
        /// <returns>the initialised frozen context or null</returns>
        private static IKistlContext TryInit()
        {
            const string frozenAssemblyName="Kistl.DalProvider.Frozen.FrozenContextImplementation, Kistl.Objects.Frozen";
            if (!_haveTriedLoading)
            {
                try
                {
                    _haveTriedLoading = true;
                    Type t = Type.GetType(frozenAssemblyName, true);
                    _single = (IKistlContext)Activator.CreateInstance(t);
                    ApplicationContext.Current.LoadFrozenActions(_single);
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning("Error when trying to load frozen context: {0}: {1}", frozenAssemblyName, ex.Message);
                    return null;
                }
            }

            return _single;
        }

        /// <summary>
        /// Registeres a IKistlContext as fallback which can be used if the frozen context assembly is not available. This is especially useful while bootstrapping.
        /// </summary>
        /// <param name="ctx">the context to use as fallback</param>
        public static void RegisterFallback(IKistlContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }
            if (_fallback != null)
            {
                Trace.TraceWarning("Replacing FrozenContext fallback.");
            }
            _fallback = ctx;
        }
    }
}
