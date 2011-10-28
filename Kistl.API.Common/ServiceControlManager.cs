
namespace Kistl.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    
    public interface IServiceControlManager
    {
        void Start();
        void Stop();

        void Start(ServiceDescriptor descr);
        void Stop(ServiceDescriptor descr);
    }

    public class ServiceControlManager : IServiceControlManager
    {
        private readonly Autofac.ILifetimeScope _container;
        private readonly IFrozenContext _frozenCtx;
        private readonly IDeploymentRestrictor _restrictor;

        public ServiceControlManager(Autofac.ILifetimeScope container, IFrozenContext frozenCtx, IDeploymentRestrictor restrictor)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (restrictor == null) throw new ArgumentNullException("restrictor");

            this._container = container;
            this._frozenCtx = frozenCtx;
            this._restrictor = restrictor;
        }

        #region IServiceControlManager Members

        public void Start()
        {
            foreach (var s in GetServiceDescriptors())
            {
                Start(s);
            }
        }

        public void Stop()
        {
            foreach (var s in GetServiceDescriptors())
            {
                Stop(s);
            }
        }

        public void Start(ServiceDescriptor descr)
        {
            if (descr == null) throw new ArgumentNullException("descr");

            try
            {
                Logging.Log.InfoFormat("Starting service {0}", descr.Description);

                var service = GetInstance(descr);
                if(service != null)
                    service.Start();
                else
                    Logging.Log.Warn("Service has not been registered");

                Logging.Log.Info("Service started successfully");
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Failed starting service", ex);
            }
        }

        public void Stop(ServiceDescriptor descr)
        {
            if (descr == null) throw new ArgumentNullException("descr");

            try
            {
                Logging.Log.InfoFormat("Stopping service {0}", descr.Description);

                var service = GetInstance(descr);
                if (service != null)
                    service.Stop();
                else
                    Logging.Log.Warn("Service has not been registered");

                Logging.Log.Info("Service stopped successfully");
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Failed stopping service", ex);
            }
        }
        #endregion

        private IService GetInstance(ServiceDescriptor descr)
        {
            var type = descr.TypeRef.AsType(true);
            object service;
            _container.TryResolve(type, out service);
            return service as IService;
        }

        private IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            foreach (var s in _frozenCtx.GetQuery<ServiceDescriptor>())
            {
                if (_restrictor.IsAcceptableDeploymentRestriction((int)s.DeploymentRestriction))
                    yield return s;
            }
        }
    }
}
