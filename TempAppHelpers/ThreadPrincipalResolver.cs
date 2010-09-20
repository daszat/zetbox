
namespace Kistl.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    using Kistl.API;
    using Kistl.App.Base;

    public class ThreadPrincipalResolver
        : IIdentityResolver
    {
        private readonly IReadOnlyKistlContext _resolverCtx;

        public ThreadPrincipalResolver(IReadOnlyKistlContext resolverCtx)
        {
            _resolverCtx = resolverCtx;
        }

        public Identity GetCurrent()
        {
            if (!string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
                return Resolve(Thread.CurrentPrincipal.Identity);
            else
                return Resolve(WindowsIdentity.GetCurrent());
        }

        public Identity Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");

            string id = identity.Name.ToLower();
            return _resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();
        }
    }
}
