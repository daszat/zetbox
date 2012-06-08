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
using Zetbox.API.Utils;

namespace Zetbox.API
{
    public class ServiceControlManager : IServiceControlManager
    {
        private readonly IEnumerable<IService> _services;

        public ServiceControlManager(IEnumerable<IService> services)
        {
            if (services == null) throw new ArgumentNullException("services");

            this._services = services;
        }

        #region IServiceControlManager Members

        public void Start()
        {
            foreach (var s in _services)
            {
                Start(s);
            }
        }

        public void Stop()
        {
            foreach (var s in _services)
            {
                Stop(s);
            }
        }

        private void Start(IService service)
        {
            if (service == null) throw new ArgumentNullException("service");

            try
            {
                Logging.Log.InfoFormat("Starting service {0}", service.DisplayName);

                service.Start();

                Logging.Log.InfoFormat("Service {0} started successfully", service.DisplayName);
            }
            catch (Exception ex)
            {
                Logging.Log.Error(string.Format("Failed starting service {0}", service.DisplayName), ex);
            }
        }

        private void Stop(IService service)
        {
            if (service == null) throw new ArgumentNullException("service");

            try
            {
                Logging.Log.InfoFormat("Stopping service {0}", service.DisplayName);

                service.Stop();

                Logging.Log.InfoFormat("Service {0} stopped successfully", service.DisplayName);
            }
            catch (Exception ex)
            {
                Logging.Log.Error(string.Format("Failed stopping service {0}", service.DisplayName), ex);
            }
        }
        #endregion
    }
}
