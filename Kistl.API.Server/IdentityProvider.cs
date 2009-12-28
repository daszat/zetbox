using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    public interface IIdentityProvider
    {
        Kistl.App.Base.Identity LoadIdentity(IQueryable<Kistl.App.Base.Identity> query, System.Security.Principal.IIdentity identity);
    }

    public static class IdentityProvider
    {
        private readonly static object _lock = new object();
        private static Type _ProviderType = null;

        /// <summary>
        /// Creates a new <see cref="IIdentityProvider"/> which is loaded from the current configuration.
        /// </summary>
        /// <returns>A new IIdentityProvider.</returns>
        public static IIdentityProvider GetProvider()
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
