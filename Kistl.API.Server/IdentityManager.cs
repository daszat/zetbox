
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    using Kistl.App.Base;

    public interface IIdentityProvider
    {
        Identity LoadIdentity(IQueryable<Identity> query, IIdentity identity);
    }

    public static class IdentityManager
    {
        private readonly static object _lock = new object();
        private static Type _ProviderType = null;
        private static Dictionary<string, Identity> _IdentityCache = new Dictionary<string, Identity>();

        static IdentityManager()
        {
            // This ensures that the current thread has a valid Principal during startup
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        }

        public static bool IsAuthenticated
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.IsAuthenticated &&
                    !string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name);
            }
        }

        public static Identity Current
        {
            get
            {
                lock (_lock)
                {
                    if (!IsAuthenticated) return null;
                    var name = Thread.CurrentPrincipal.Identity.Name;
                    if (!_IdentityCache.ContainsKey(name))
                    {
                        using (IKistlContext ctx = KistlContext.GetContext())
                        {
                            var identity = GetProvider().LoadIdentity(ctx.GetQuery<Identity>(), Thread.CurrentPrincipal.Identity);
                            if (identity == null) return null;
                            _IdentityCache[name] = identity;
                        }
                    }
                    return _IdentityCache[name];
                }
            }
        }

        public static Identity LoadIdentity(IKistlContext ctx)
        {
            if (!IsAuthenticated) return null;
            return GetProvider().LoadIdentity(ctx.GetQuery<Identity>(), Thread.CurrentPrincipal.Identity);
        }


        /// <summary>
        /// Creates a new <see cref="IIdentityProvider"/> which is loaded from the current configuration.
        /// </summary>
        /// <returns>A new IIdentityProvider.</returns>
        private static IIdentityProvider GetProvider()
        {
            lock (_lock)
            {
                if (_ProviderType == null)
                {
                    _ProviderType = Type.GetType(ApplicationContext.Current.Configuration.Server.IdentityProviderType);
                    if (_ProviderType == null)
                    {
                        throw new Configuration.ConfigurationException(string.Format("Unable to load Type '{0}' for IdentityProvider. Check your Configuration '/Server/IdentityProviderType'.", ApplicationContext.Current.Configuration.Server.IdentityProviderType));
                    }
                }
            }
            object obj = Activator.CreateInstance(_ProviderType);
            if (!(obj is IIdentityProvider))
            {
                throw new Configuration.ConfigurationException(string.Format("Type '{0}' is not a IIdentityProvider object. Check your Configuration '/Server/IdentityProviderType'.", ApplicationContext.Current.Configuration.Server.IdentityProviderType));
            }
            return (IIdentityProvider)obj;
        }
    }
}
