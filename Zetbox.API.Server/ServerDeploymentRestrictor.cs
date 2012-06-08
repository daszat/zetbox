
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;

    internal sealed class ServerDeploymentRestrictor
        : IDeploymentRestrictor
    {
        internal ServerDeploymentRestrictor() { }
        
        public bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
