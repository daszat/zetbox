
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

    public class ThreadPrincipalResolver
        : IIdentityResolver
    {
        private readonly IKistlServerContext _resolverCtx;

        public ThreadPrincipalResolver(IKistlServerContext resolverCtx)
        {
            _resolverCtx = resolverCtx;
        }

        public Identity GetCurrent()
        {
            return Resolve(Thread.CurrentPrincipal.Identity);
        }

        public Identity Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");

            string id = identity.Name.ToLower();
            return _resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();
        }
    }
}
