using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using Kistl.API;
using Kistl.API.Configuration;

namespace Kistl.Server.Service
{
    public class ServerManager
        : MarshalByRefObject, IKistlAppDomain, IDisposable
    {
        private IContainer container;
        private IKistlAppDomain wcfServer;

        public void Start(KistlConfig config)
        {
            if (container != null) { throw new InvalidOperationException("already started"); }
            container = Program.CreateMasterContainer(config);

            var appCtx = new ServerApplicationContext(config);

            Program.DefaultInitialisation();

            wcfServer = container.Resolve<IKistlAppDomain>();
            wcfServer.Start(config);
        }

        public void Stop()
        {
            if (wcfServer != null) { throw new InvalidOperationException("not yet started"); }
            wcfServer.Stop();
        }

        public void Dispose()
        {
            if (container != null) { container.Dispose(); }
        }
    }
}
