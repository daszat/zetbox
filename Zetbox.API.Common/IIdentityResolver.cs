
namespace Kistl.API.Common
{
    using System.Security.Principal;
    using Kistl.App.Base;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Kistl.API.Utils;

    public interface IIdentityResolver
    {
        Identity GetCurrent();
        Identity Resolve(IIdentity identity);
    }

    [Serializable]
    public class UnresolvableIdentityException : Exception
    {
        public UnresolvableIdentityException()
            : this(string.Empty)
        {
        }

        public UnresolvableIdentityException(string userName)
            : base(string.Format("Unable to resolve identity '{0}'", userName))
        {
        }

        public UnresolvableIdentityException(string userName, Exception inner)
            : base(string.Format("Unable to resolve identity '{0}'", userName), inner)
        {
        }

        protected UnresolvableIdentityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public abstract class BaseIdentityResolver : IIdentityResolver
    {
        private readonly Func<IReadOnlyKistlContext> resolverCtxFactory;
        private IReadOnlyKistlContext _resolverCtx;
        protected readonly Dictionary<string, Identity> cache;

        protected BaseIdentityResolver(Func<IReadOnlyKistlContext> resolverCtxFactory)
        {
            if (resolverCtxFactory == null) throw new ArgumentNullException("resolverCtxFactory");
            this.resolverCtxFactory = resolverCtxFactory;
            cache = new Dictionary<string, Identity>();
        }

        protected IReadOnlyKistlContext ResolverCtx
        {
            get
            {
                if (_resolverCtx == null)
                {
                    _resolverCtx = resolverCtxFactory();
                }
                return _resolverCtx;
            }
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

            Identity result;

            if (cache.ContainsKey(id))
            {
                result = cache[id];
            }
            else
            {
                result = cache[id] = ResolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();

                if (result == null)
                {
                    Logging.Log.WarnFormat("Unable to resolve Identity {0}", name);
                }
            }


            return result;
        }
    }
}
