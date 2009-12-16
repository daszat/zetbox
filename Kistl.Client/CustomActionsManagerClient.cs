
namespace Kistl.Client
{
    using System;
    using System.Linq;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// Implementation of the client-side CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerClient
        : BaseCustomActionsManager
    {
        internal CustomActionsManagerClient()
            : base(String.Empty, ApplicationContext.Current.ImplementationAssembly)
        {
        }

        /// <summary>
        /// Decides whether or no a deployment restriction is acceptable
        /// </summary>
        /// <param name="r">the restriction to check</param>
        /// <returns>true if the restriction is None or ClientOnly</returns>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ClientOnly || r == (int)DeploymentRestriction.None;
        }
    }

    public class FrozenActionsManagerClient
        : FrozenActionsManager
    {
        /// <inheritdoc/>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ClientOnly || r == (int)DeploymentRestriction.None;
        }
    }

}
