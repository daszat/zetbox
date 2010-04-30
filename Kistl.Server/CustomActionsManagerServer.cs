
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
            : base(String.Empty, Kistl.API.Helper.ServerAssembly)
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
        public FrozenActionsManagerServer()
            : base()
        {
        }

        /// <inheritdoc/>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
