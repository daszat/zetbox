using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using Kistl.Client;
using Kistl.API;
using Kistl.API.Configuration;
using Kistl.App.Extensions;
using Kistl.App.Base;

namespace Kistl.DalProvider.ClientObjects.Mocks
{
    public class MyCustomActionsManagerClient : BaseCustomActionsManager
    {
        public MyCustomActionsManagerClient()
            : base(String.Empty, ApplicationContext.Current.ImplementationAssembly)
        {
        }

        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ClientOnly || r == (int)DeploymentRestriction.None;
        }
    }

    public class ClientApiContextMock : ClientApiContext
    {
        public ClientApiContextMock()
            : base(KistlConfig.FromFile("Kistl.DalProvider.ClientObjects.Tests.Config.xml"))
        {
        }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            var fam = new FrozenActionsManagerClient();
            fam.Init(ctx);
        }
    }
}
