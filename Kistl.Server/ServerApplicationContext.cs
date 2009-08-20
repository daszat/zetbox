
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
            var interfaceAssembly = Assembly.Load("Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (interfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly, no Entity Framework Metadata will be loaded");
            var serverAssembly = Assembly.Load("Kistl.Objects.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (serverAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly, no Entity Framework Metadata will be loaded");

            // force-load a few assemblies to the reflection-only context so the DAL provider can find them
            // this uses the AssemblyLoader directly because Assembly.ReflectionOnlyLoad doesn't go through all 
            // the moves of resolving AssemblyNames to files. See http://stackoverflow.com/questions/570117/
            var reflectedInterfaceAssembly = AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects");
            if (reflectedInterfaceAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects Assembly for reflection, no Entity Framework Metadata will be loaded");
            var reflectedServerAssembly = AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects.Server");
            if (reflectedServerAssembly == null)
                throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly for reflection, no Entity Framework Metadata will be loaded");

            ServerApplicationContext.Current = this;
        }

        internal void LoadDefaultActionsManager()
        {
            var frozenCtx = FrozenContext.TryInit(false);
            if (frozenCtx != null)
            {
                var fam = new FrozenActionsManagerServer();
                fam.Init(frozenCtx);
                var cams = new CustomActionsManagerServer();
                cams.Init(frozenCtx);
                this.SetCustomActionsManager(cams);
            }
        }

        internal void LoadActionsManager(IKistlContext ctx)
        {
            var cams = new CustomActionsManagerServer();
            cams.Init(ctx);
            this.SetCustomActionsManager(cams);
        }

        internal void LoadNoopActionsManager(IKistlContext ctx)
        {
            this.SetCustomActionsManager(new NoopActionsManager());
        }
    }
}
