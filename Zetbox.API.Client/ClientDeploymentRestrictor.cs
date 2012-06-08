
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;

    internal sealed class ClientDeploymentRestrictor 
        : IDeploymentRestrictor
    {
        internal ClientDeploymentRestrictor() { }

        public bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ClientOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
