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
    using Autofac;
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
        private readonly object _lock = new object();

        private readonly ILifetimeScope _parentScope;
        private readonly Dictionary<string, Identity> _cache;

        private ILifetimeScope _currentScope;
        private IReadOnlyZetboxContext _resolverCtx;

        protected BaseIdentityResolver(ILifetimeScope parentScope)
        {
            if (parentScope == null) throw new ArgumentNullException("parentScope");
            _parentScope = parentScope;
            _cache = new Dictionary<string, Identity>();
        }

        private void CheckScope()
        {
            // for now, just cache indefinitely. the service will have to be recycled regularily
            if (_currentScope == null)
            {
                Logging.Log.Info("(Re-)Initialising BaseIdentityResolver's cache");
                _cache.Clear();
                if (_currentScope != null) _currentScope.Dispose();
                _currentScope = _parentScope.BeginLifetimeScope();
                _resolverCtx = _currentScope.Resolve<IReadOnlyZetboxContext>();
                _scopeCreated = DateTime.Now;
            }
        }

        public abstract Identity GetCurrent();

        public Identity Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            return Resolve(identity.Name);
        }

        protected Identity Resolve(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            string id = name.ToLower();

            Identity result = null;

            lock (_lock)
            {
                CheckScope();

                if (_cache.ContainsKey(id))
                {
                    result = _cache[id];
                }
                else
                {
                    try
                    {
                        result = _cache[id] = _resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();
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
            }

            return result;
        }
    }
}
