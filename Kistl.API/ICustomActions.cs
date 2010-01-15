
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Interface for a Custom Action Manager. Every Client and Server host must provide a Custom Action Manager.
    /// </summary>
    public interface ICustomActionsManager
    {
        /// <summary>
        /// Should load Metadata, create an Instance and cache Metadata for future use.
        /// </summary>
        /// <param name="ctx">the context to use for looking up MethodInvocations</param>
        void Init(IReadOnlyKistlContext ctx);
    }

    /// <summary>
    /// A noop implementation of the ICustomActionsManager
    /// </summary>
    public sealed class NoopActionsManager
        : ICustomActionsManager
    {
        #region ICustomActionsManager Members

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="ctx">ignored</param>
        public void Init(IReadOnlyKistlContext ctx) { }

        #endregion
    }
}
