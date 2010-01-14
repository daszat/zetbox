
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    using Kistl.API.Server;
    using Kistl.App.Base;

    public class WCFIdentityProvider
        : IIdentityProvider
    {
        public Identity LoadIdentity(IQueryable<Identity> query, IIdentity identity)
        {
            string id = Thread.CurrentPrincipal.Identity.Name.ToLower();
            return query.Where(i => i.WCFAccount.ToLower() == id).FirstOrDefault();
        }
    }
}
