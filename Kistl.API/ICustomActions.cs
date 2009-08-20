
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
        /// Should Attach using Metadata.
        /// Detaching is done through the Garbage Collector.
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// </summary>
        /// <param name="obj"></param>
        void AttachEvents(IDataObject obj);

        /// <summary>
        /// Should load Metadata, create an Instance and cache Metadata for future use.
        /// </summary>
        /// <param name="ctx">the context to use for looking up MethodInvocations</param>
        void Init(IKistlContext ctx);
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
        /// <param name="obj">ignored</param>
        public void AttachEvents(IDataObject obj) { }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="ctx">ignored</param>
        public void Init(IKistlContext ctx) { }

        #endregion
    }
}
