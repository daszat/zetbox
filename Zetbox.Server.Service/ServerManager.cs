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

using Autofac;
using Zetbox.API;
using Zetbox.API.Configuration;
using Zetbox.API.Utils;

namespace Zetbox.Server.Service
{
    /// <summary>
    /// Manages starting a WCF Server in a new AppDomain which has only the AssemblyLoader initialized.
    /// </summary>
    public class ServerManager
        : MarshalByRefObject, IZetboxAppDomain, IDisposable
    {
        private IContainer container;

        public void Start(ZetboxConfig config)
        {
            if (config == null) { throw new ArgumentNullException("config"); }
            if (container != null) { throw new InvalidOperationException("already started"); }

            Logging.Configure();
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            container = Program.CreateMasterContainer(config);

            IServiceControlManager scm = null;
            if (container.TryResolve<IServiceControlManager>(out scm))
            {
                Logging.Log.Info("Starting Zetbox Services");
                scm.Start();
            }
            else
            {
                Logging.Log.Info("Service control manager not registered");
            }
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (container != null) { container.Dispose(); }
        }
    }
}
