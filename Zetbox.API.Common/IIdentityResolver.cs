// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Common
{
    using System.Security.Principal;
    using Zetbox.App.Base;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Zetbox.API.Utils;

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
        private readonly Func<IReadOnlyZetboxContext> resolverCtxFactory;
        private IReadOnlyZetboxContext _resolverCtx;
        protected readonly Dictionary<string, Identity> cache;

        protected BaseIdentityResolver(Func<IReadOnlyZetboxContext> resolverCtxFactory)
        {
            if (resolverCtxFactory == null) throw new ArgumentNullException("resolverCtxFactory");
            this.resolverCtxFactory = resolverCtxFactory;
            cache = new Dictionary<string, Identity>();
        }

        protected IReadOnlyZetboxContext ResolverCtx
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
