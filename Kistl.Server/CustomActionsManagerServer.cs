
namespace Kistl.Server
{
    using System;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// Implementation of the server-side ICustomActionsManager
    /// </summary>
    internal class CustomActionsManagerServer
        : BaseCustomActionsManager
    {
        internal CustomActionsManagerServer()
            : base(String.Empty, ApplicationContext.Current.ImplementationAssembly)
        {
        }

        /// <inheritdoc/>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }
    }

    public class FrozenActionsManagerServer
          : FrozenActionsManager
    {
        /// <inheritdoc/>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
