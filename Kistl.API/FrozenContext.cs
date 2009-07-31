
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
        /// Gets the FrozenContext singleton. This is loaded on demand.
        /// </summary>
        public static IKistlContext Single
        {
            get
            {
                if (_single == null)
                {
                    try
                    {
                        Type t = Type.GetType("Kistl.DalProvider.Frozen.FrozenContextImplementation, Kistl.Objects.Frozen", true);
                        _single = (IKistlContext)Activator.CreateInstance(t);
                    }
                    catch (Exception ex)
                    {
                        throw new TypeLoadException("Unable to load FrozenContext", ex);
                    }
                    if (_single == null)
                    {
                        // something strange happened.
                        throw new TypeLoadException("Unable to load frozen context");
                    }
                }
                return _single;
            }
        }
    }
}
