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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;

    public interface IAppDomainInitializer
    {
        void Initialize(ILifetimeScope container);
    }

    public static class AppDomainInitializer
    {
        /// <summary>
        /// Call all registered IAppDomainInitializer implementors.
        /// </summary>
        public static void InitializeFrom(IContainer container)
        {
            foreach (var initializer in container.Resolve<IEnumerable<IAppDomainInitializer>>())
            {
                initializer.Initialize(container);
            }
        }
    }
}
