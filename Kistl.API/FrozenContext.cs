
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
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
        /// A value indicating whether loading the frozen context was already tried.
        /// </summary>
        private static bool _haveTriedLoading = false;

        /// <summary>
        /// Gets the FrozenContext singleton. This is loaded on demand.
        /// </summary>
        public static IKistlContext Single
        {
            get
            {
                if (!_haveTriedLoading)
                {
                    TryInit(true);
                    if (_single == null)
                    {
                        // something strange happened: no context loaded AND no exception thrown
                        throw new TypeLoadException("Unable to load frozen context");
                    }
                }
                return _single;
            }
        }

        /// <summary>
        /// Tries to initialise the FrozenContext singleton.
        /// </summary>
        /// <param name="shouldThrow">whether or not the method should throw an exception</param>
        /// <returns>the initialised frozen context or null, if shouldThrow is false and the FrozenContext could not be initialised</returns>
        /// <exception cref="TypeLoadException">if shouldThrow is true and the FrozenContext could not be loaded</exception>
        public static IKistlContext TryInit(bool shouldThrow)
        {
            if (!_haveTriedLoading)
            {
                try
                {
                    _haveTriedLoading = true;
                    Type t = Type.GetType("Kistl.DalProvider.Frozen.FrozenContextImplementation, Kistl.Objects.Frozen", true);
                    _single = (IKistlContext)Activator.CreateInstance(t);
                }
                catch (Exception ex)
                {
                    if (shouldThrow)
                    {
                        throw new TypeLoadException("Unable to load FrozenContext", ex);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            if (shouldThrow && _single == null)
            {
                throw new TypeLoadException("Unable to load FrozenContext");
            }
            else
            {
                return _single;
            }
        }
    }
}
