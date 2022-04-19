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
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    /// <summary>
    /// Resolve the client's identity.
    /// </summary>
    public interface IPrincipalResolver
    {
        /// <summary>
        /// Retrieves the zetbox identity of the current user. The Identity is member of it's own resolver data context
        /// </summary>
        /// <returns>a Identity or null if none was found.</returns>
        Task<ZetboxPrincipal> GetCurrent();

        /// <summary>
        /// Retrieves the zetbox identity of the specified security principal.The Identity is member of it's own resolver data context
        /// </summary>
        /// <param name="identity">a security principal</param>
        /// <returns>a Identity or null if none was found.</returns>
        Task<ZetboxPrincipal> Resolve(IIdentity identity);

        /// <summary>
        /// Clear the cache
        /// </summary>
        void ClearCache();
    }

    [Serializable]
    public class UnresolvablePrincipalException : Exception
    {
        public UnresolvablePrincipalException()
            : this(string.Empty)
        {
        }

        public UnresolvablePrincipalException(string userName)
            : base(string.Format("Unable to resolve identity '{0}'", userName))
        {
        }

        public UnresolvablePrincipalException(string userName, Exception inner)
            : base(string.Format("Unable to resolve identity '{0}'", userName), inner)
        {
        }

        protected UnresolvablePrincipalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public abstract class BasePrincipalResolver : IPrincipalResolver
    {
        private readonly object _lock = new object();

        private readonly ILifetimeScope _parentScope;
        private readonly Dictionary<string, ZetboxPrincipal> _cache;
        private DateTime _clearTime = DateTime.MinValue;

        private ILifetimeScope _currentScope;
        private IReadOnlyZetboxContext _resolverCtx;

        protected BasePrincipalResolver(ILifetimeScope parentScope)
        {
            if (parentScope == null) throw new ArgumentNullException("parentScope");
            _parentScope = parentScope;
            _cache = new Dictionary<string, ZetboxPrincipal>();
        }

        private void CheckScope()
        {
            // for now, clear the cache every hour
            if (_currentScope == null || _clearTime < DateTime.Now)
            {
                Logging.Log.Info("(Re-)Initialising BaseIdentityResolver's cache");
                _cache.Clear();
                if (_currentScope != null) _currentScope.Dispose();
                _currentScope = _parentScope.BeginLifetimeScope();
                _resolverCtx = _currentScope.Resolve<IReadOnlyZetboxContext>();
                _clearTime = DateTime.Now.AddHours(1);
            }
        }

        public abstract Task<ZetboxPrincipal> GetCurrent();

        public async Task<ZetboxPrincipal> Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            return await Resolve(identity.Name);
        }

        protected async Task<ZetboxPrincipal> Resolve(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            string id = name.ToLower();

            ZetboxPrincipal result = null;

            lock (_lock)
            {
                CheckScope();

                if (_cache.ContainsKey(id))
                {
                    return _cache[id];
                }
            }

            try
            {
                var identity = await _resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefaultAsync();
                if (identity != null && identity.IsDeactivated == false)
                {
                    result = new ZetboxPrincipal(id: identity.ID, userName: identity.UserName, displayName: identity.DisplayName, groups: identity.Groups.Select(g => new ZetboxPrincipalGroup(id: g.ID, name: g.Name, exportGuid: g.ExportGuid)));
                    lock(_lock)
                    {
                        _cache[id] = result;
                    }
                }
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

            return result;
        }

        void IPrincipalResolver.ClearCache()
        {
            lock (_lock)
            {
                _clearTime = DateTime.MinValue;
            }
        }
    }
}
