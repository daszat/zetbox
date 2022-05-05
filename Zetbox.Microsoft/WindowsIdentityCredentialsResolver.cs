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
namespace Zetbox.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;

    public class WindowsIdentityCredentialsResolver : ICredentialsResolver
    {
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .Register<WindowsIdentityCredentialsResolver>(c => new WindowsIdentityCredentialsResolver())
                    .As<ICredentialsResolver>()
                    .SingleInstance();
            }
        }

        public void EnsureCredentials()
        {
            // Gracefully do nothing
            // Using Windows Credentials, they are already set by the operating system
        }

        public void SetCredentialsTo(HttpClient req)
        {
            if (req == null) throw new ArgumentNullException("req");

            // req.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( CredentialCache.DefaultCredentials;
        }

        public void InvalidCredentials()
        {
            string name;

            if (!string.IsNullOrEmpty(Thread.CurrentPrincipal?.Identity?.Name))
                name = Thread.CurrentPrincipal.Identity.Name;
            else
                name = WindowsIdentity.GetCurrent()?.Name ?? string.Empty;

            throw new AuthenticationException(string.Format("You are not authorized to access this application. (username={0})",
                !string.IsNullOrWhiteSpace(name) ? name : "<empty>"));
        }

        public void Freeze()
        {
            // doesn't need to do anything.
        }
    }
}
