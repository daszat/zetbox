
namespace Kistl.API.Server
{
    using System.Security.Principal;
    using Kistl.App.Base;

    public interface IIdentityResolver
    {
        Identity GetCurrent();
        Identity Resolve(IIdentity identity);
    }
}
