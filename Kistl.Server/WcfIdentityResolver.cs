
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    public class WcfIdentityResolver
        : IIdentityResolver
    {
        private readonly IKistlServerContext _resolverCtx;

        public WcfIdentityResolver(IKistlServerContext resolverCtx)
        {
            _resolverCtx = resolverCtx;
        }

        public Identity GetCurrent()
        {
            return Resolve(Thread.CurrentPrincipal.Identity);
        }

        public Identity Resolve(IIdentity identity)
        {
            string id = identity.Name.ToLower();
            return _resolverCtx.GetQuery<Identity>().Where(i => i.WCFAccount.ToLower() == id).FirstOrDefault();
        }
    }
}
