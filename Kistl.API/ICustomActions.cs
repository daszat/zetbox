
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

    public interface IDeploymentRestrictor
    {
        /// <summary>
        /// Override this method to modify the acceptable DeploymentRestrictions.
        /// </summary>
        /// <param name="r">the restriction to check (This parameter is int because DeploymentRestriction might not yet be loaded)</param>
        /// <returns>whether or not the given deployment restriction is acceptable for the environment</returns>
        bool IsAcceptableDeploymentRestriction(int r);
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class Implementor : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class Invocation : Attribute
    {
    }
}
