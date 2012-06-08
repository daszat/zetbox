
namespace Zetbox.DalProvider.Memory.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    public class TestDeploymentRestrictor 
        : IDeploymentRestrictor
    {
        public bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
