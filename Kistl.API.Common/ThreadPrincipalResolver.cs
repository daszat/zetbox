
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

    public sealed class ThreadPrincipalResolver
        : BaseIdentityResolver
    {
        public ThreadPrincipalResolver(Func<IReadOnlyKistlContext> resolverCtxFactory)
            : base(resolverCtxFactory)
        {
        }

        public override Identity GetCurrent()
        {
            if (!string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
                return Resolve(Thread.CurrentPrincipal.Identity);
            else
                return Resolve(WindowsIdentity.GetCurrent());
        }
    }
}
