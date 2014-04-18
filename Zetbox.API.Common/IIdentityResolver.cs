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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Principal;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    /// <summary>
    /// Resolve the client's identity.
    /// </summary>
    public interface IIdentityResolver
    {
        /// <summary>
        /// Retrieves the zetbox identity of the current user. The Identity is member of it's own resolver data context
        /// </summary>
        /// <returns>a Identity or null if none was found.</returns>
        Identity GetCurrent();

        /// <summary>
        /// Retrieves the zetbox identity of the specified security principal.The Identity is member of it's own resolver data context
        /// </summary>
        /// <param name="identity">a security principal</param>
        /// <returns>a Identity or null if none was found.</returns>
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

            Identity result = null;

            if (cache.ContainsKey(id))
            {
                result = cache[id];
            }
            else
            {
                try
                {
                    result = cache[id] = ResolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    var inner = ex.StripTargetInvocationExceptions();
                    if (inner is InvalidZetboxGeneratedVersionException || inner is System.IO.IOException)
                    {
                        throw inner;
                    }
                    Logging.Log.Warn("Exception while resolving Identity", ex);
                }
                if (result == null)
                {
                    Logging.Log.WarnFormat("Unable to resolve Identity {0}", name);
                }
            }


            return result;
        }
    }
}
