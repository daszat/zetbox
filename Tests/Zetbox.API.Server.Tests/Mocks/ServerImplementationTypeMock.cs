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

namespace Zetbox.API.Server.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;

    public sealed class ServerImplementationTypeMock
        : ImplementationType
    {
        public ServerImplementationTypeMock(Type t, InterfaceType.Factory iftFactory)
            : base(t, iftFactory, new Zetbox.API.Mocks.MockImplementationTypeChecker())
        {
        }

        public override InterfaceType ToInterfaceType()
        {
            var ifTypeName = Type.FullName.Replace(Helper.ImplementationSuffix, String.Empty);
            if (ifTypeName.Contains("Mock"))
            {
                return IftFactory(Type.GetType(ifTypeName, true));
            }
            else
            {
                return IftFactory(Type.GetType(ifTypeName + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
