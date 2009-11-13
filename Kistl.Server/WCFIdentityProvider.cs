using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.Server
{
    public class WCFIdentityProvider : IIdentityProvider
    {
        public Kistl.App.Base.Identity LoadIdentity(IQueryable<Kistl.App.Base.Identity> query, System.Security.Principal.IIdentity identity)
        {
            string id = System.Threading.Thread.CurrentPrincipal.Identity.Name.ToLower();
            return query.Where(i => i.WCFAccount.ToLower() == id).FirstOrDefault();
        }
    }
}
