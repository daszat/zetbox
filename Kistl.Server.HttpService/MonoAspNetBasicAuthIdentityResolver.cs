
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Web;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.App.Base;

    public class MonoAspNetBasicAuthIdentityResolver
        : IIdentityResolver
    {
        private readonly IReadOnlyKistlContext _resolverCtx;

        public MonoAspNetBasicAuthIdentityResolver(IReadOnlyKistlContext resolverCtx, ILifetimeScope scope)
        {
            _resolverCtx = resolverCtx;
        }

        public Identity GetCurrent()
        {
            var decodedBytes = Convert.FromBase64String(HttpContext.Current.Request.Headers.Get("Authorization"));
            var parts = Encoding.UTF8.GetString(decodedBytes).Split(":".ToCharArray(), 2);
            return parts.Length > 0
                ? Resolve(parts[0])
                : null;
        }

        public Identity Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");

            return Resolve(identity.Name.ToLower());
        }

        public Identity Resolve(string identity)
        {
            if (String.IsNullOrEmpty(identity)) throw new ArgumentNullException("identity");

            return _resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == identity).FirstOrDefault();
        }
    }
}
