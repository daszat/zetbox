using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

using Autofac;
using Kistl.API;

namespace Kistl.App.Extensions
{
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

        public ServiceControlManager(Autofac.ILifetimeScope container, IFrozenContext frozenCtx)
        {
            this._container = container;
            this._frozenCtx = frozenCtx;
        }

        #region IServiceControlManager Members

        public void Start()
        {
            foreach (var s in _frozenCtx.GetQuery<ServiceDescriptor>())
            {
                Start(s);
            }
        }

        public void Stop()
        {
            foreach (var s in _frozenCtx.GetQuery<ServiceDescriptor>())
            {
                Stop(s);
            }
        }

        public void Start(ServiceDescriptor descr)
        {
            if (descr == null) throw new ArgumentNullException("descr");

            var service = GetInstance(descr);
            service.Start();
        }

        public void Stop(ServiceDescriptor descr)
        {
            if (descr == null) throw new ArgumentNullException("descr");

            var service = GetInstance(descr);
            service.Stop();
        }

        private IService GetInstance(ServiceDescriptor descr)
        {
            var type = descr.TypeRef.AsType(true);
            return (IService)_container.Resolve(type);
        }
        #endregion
    }

    public class ServiceControlManagerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ServiceControlManager>()
                .As<IServiceControlManager>()
                .SingleInstance();
        }
    }
}
