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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;

    [Implementor]
    public class IdentityActions
    {
        private static IPrincipalResolver _principalResolver;

        public IdentityActions(IPrincipalResolver principalResolver)
        {
            _principalResolver = principalResolver;
        }

        [Invocation]
        public static void NotifyPostSave(Identity obj)
        {
            _principalResolver.ClearCache();
        }

        [Invocation]
        public static void ToString(Identity obj, MethodReturnEventArgs<string> e)
        {
            e.Result = (obj.DisplayName ?? string.Empty) + " (" + (obj.UserName ?? string.Empty) + ")";

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void CreateLoginToken(Identity obj)
        {
            obj.LoginToken = Guid.NewGuid();
        }

        [Invocation]
        public static void ClearLoginToken(Identity obj)
        {
            obj.LoginToken = null;
        }

        [Invocation]
        public static void SetPassword(Identity obj, string plainTextPassword)
        {
            if (!string.IsNullOrEmpty(plainTextPassword))
            {
                obj.Password = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
            }
            else
            {
                // null or empty is a valid argument as this means, that there is no local account (local password) and it is a pure OpenID account (or something)
                obj.Password = null;
            }
        }
    }
}
