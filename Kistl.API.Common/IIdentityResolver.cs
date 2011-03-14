
namespace Kistl.API.Common
{
    using System.Security.Principal;
    using Kistl.App.Base;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public interface IIdentityResolver
    {
        Identity GetCurrent();
        Identity Resolve(IIdentity identity);
    }

    public abstract class BaseIdentityResolver : IIdentityResolver
    {
        protected readonly IReadOnlyKistlContext resolverCtx;
        protected readonly Dictionary<string, Identity> cache;

        protected BaseIdentityResolver(IReadOnlyKistlContext resolverCtx)
        {
            if (resolverCtx == null) throw new ArgumentNullException("resolverCtx");
            this.resolverCtx = resolverCtx;
            cache = new Dictionary<string, Identity>();
        }

        public abstract Identity GetCurrent();

        public virtual Identity Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            return Resolve(identity.Name);
        }

        protected virtual Identity Resolve(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            string id = name.ToLower();

            if (cache.ContainsKey(id))
            {
                return cache[id];
            }
            else
            {
                return cache[id] = resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();
            }
        }
    }
}
