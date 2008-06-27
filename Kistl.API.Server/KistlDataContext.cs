using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    /// <summary>
    /// Factory for Context Objects.
    /// </summary>
    public static class KistlDataContext
    {
        [ThreadStatic]
        private static IKistlContext _Current = null;

        /// <summary>
        /// Returns the current Context of the current Thread. 
        /// If InitSession was not called, an InvalidOperationException will be thrown.
        /// </summary>
        public static IKistlContext Current
        {
            get
            {
                if (_Current == null)
                {
                    throw new InvalidOperationException("No Session");
                }
                return _Current;
            }
        }

        /// <summary>
        /// Initializes a Context on the current Thread.
        /// Throws an InvalidOperationException if a Session was already initialized.
        /// </summary>
        /// <returns>a new Context</returns>
        public static IKistlContext InitSession()
        {
            if (_Current != null) throw new InvalidOperationException("Session already set");
            _Current = GetContext();
            return _Current;
        }

        /// <summary>
        /// Clears the current Session.
        /// </summary>
        /// <param name="ctx">Context to clear from Session.</param>
        public static void ClearSession(IKistlContext ctx)
        {
            if (_Current == ctx)
            {
                _Current = null;
            }
        }

        /// <summary>
        /// Creates a new Context.
        /// </summary>
        /// <returns>A new Context.</returns>
        public static IKistlContext GetContext()
        {
            return new KistlDataContextEntityFramework();
        }
    }
}
