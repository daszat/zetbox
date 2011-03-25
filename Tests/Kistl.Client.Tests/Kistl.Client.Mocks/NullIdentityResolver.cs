
namespace Kistl.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using Kistl.API.Common;
    using Kistl.App.Base;

    public sealed class NullIdentityResolver : IIdentityResolver
    {
        public Identity GetCurrent()
        {
            return null;
        }

        public Identity Resolve(IIdentity identity)
        {
            return null;
        }
    }
}
