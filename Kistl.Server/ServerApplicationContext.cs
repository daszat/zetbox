
namespace Kistl.Server
{
    using System;
    using System.Reflection;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;

    public class ServerApplicationContext : ServerApiContext
    {
        public static new ServerApplicationContext Current { get; private set; }

        public ServerApplicationContext(KistlConfig config)
            : base(config)
        {
            ServerApplicationContext.Current = this;
        }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            // nothing to do
        }
    }
}
