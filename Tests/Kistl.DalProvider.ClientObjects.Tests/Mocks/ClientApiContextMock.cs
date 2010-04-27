
namespace Kistl.DalProvider.ClientObjects.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client;

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

    //public class ClientApiContextMock
    //{
    //    public ClientApiContextMock()
    //        : base(KistlConfig.FromFile("Kistl.DalProvider.ClientObjects.Tests.Config.xml"))
    //    {
    //    }
    //}
}
