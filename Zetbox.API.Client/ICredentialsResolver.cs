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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Net;

namespace Zetbox.API.Client
{
    public interface ICredentialsResolver
    {
        /// <summary>
        /// Ensures that credentials are initialized
        /// </summary>
        void EnsureCredentials();

        /// <summary>
        /// Initializes the given ClientCredentials
        /// </summary>
        /// <param name="c">ClientCredentials to initialize</param>
        void SetCredentialsTo(ClientCredentials c);

        /// <summary>
        /// Initializes the given WebRequest
        /// </summary>
        /// <param name="req">WebRequest to initialize</param>
        void SetCredentialsTo(WebRequest req);

        /// <summary>
        /// Called by the using class to report invalid credentials.
        /// Implementors should reset their internal state and rerequest credentials from the user.
        /// </summary>
        void InvalidCredentials();

        /// <summary>
        /// After calling this function, invalidating the credentials will cause a complete abort.
        /// This can be used to protect identity and ACL caches.
        /// </summary>
        void Freeze();
    }
}
