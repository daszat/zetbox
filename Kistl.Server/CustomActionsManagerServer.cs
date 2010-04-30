
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
        internal CustomActionsManagerServer(IAssemblyConfiguration assemblyConfiguration)
            : base(String.Empty, assemblyConfiguration)
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
        public FrozenActionsManagerServer(IAssemblyConfiguration aCfg)
            : base(aCfg)
        {
        }

        /// <inheritdoc/>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
