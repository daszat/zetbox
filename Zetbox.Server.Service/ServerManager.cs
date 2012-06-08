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
        private IZetboxAppDomain wcfServer;

        public void Start(ZetboxConfig config)
        {
            if (container != null) { throw new InvalidOperationException("already started"); }
            
            Logging.Configure();
            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

            container = Program.CreateMasterContainer(config);

            wcfServer = container.Resolve<IZetboxAppDomain>();
            wcfServer.Start(config);
        }

        public void Stop()
        {
            if (wcfServer == null) { throw new InvalidOperationException("not yet started"); }
            Dispose();
        }

        public void Dispose()
        {
            if (wcfServer != null) { wcfServer.Stop(); }
            if (container != null) { container.Dispose(); }
        }
    }
}
