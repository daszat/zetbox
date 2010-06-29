using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.Memory.Tests
{
    public class TestDeploymentRestrictor : IDeploymentRestrictor
    {
        public bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.None;
        }
    }
}
