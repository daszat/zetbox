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
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;

    public sealed class ThreadPrincipalResolver
        : BasePrincipalResolver
    {
        public ThreadPrincipalResolver(ILifetimeScope parentScope)
            : base(parentScope)
        {
        }

        public override Task<ZetboxPrincipal> GetCurrent()
        {
            if (!string.IsNullOrEmpty(Thread.CurrentPrincipal?.Identity?.Name))
                return Resolve(Thread.CurrentPrincipal.Identity);
            else
                return Resolve("ARTHUR-P15\\Arthur Zaczek"); // TODO: Fix this!
        }
    }
}
