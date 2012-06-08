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
