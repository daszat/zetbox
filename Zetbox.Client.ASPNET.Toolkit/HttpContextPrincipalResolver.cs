using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Http;
using Zetbox.API;
using Zetbox.API.Common;

namespace Zetbox.Client.ASPNET.Toolkit
{
    public class HttpContextPrincipalResolver : BasePrincipalResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextPrincipalResolver(ILifetimeScope parentScope, IHttpContextAccessor httpContextAccessor) : base(parentScope)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ZetboxPrincipal GetCurrent()
        {
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.User?.Identity?.Name))
                return Resolve(_httpContextAccessor.HttpContext?.User?.Identity);
            else
                return null;
        }
    }
}
