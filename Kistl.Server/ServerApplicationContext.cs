using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Server;

namespace Kistl.Server
{
    public class ServerApplicationContext : ServerApiContext
    {
        public static new ServerApplicationContext Current { get; private set; }

        public ServerApplicationContext(KistlConfig config)
            : base(config)
        {
            ServerApplicationContext.Current = this;

            // Preload Kistl.Objects.Server.dll so the Mapping Resources will be loaded
            // Console.WriteLine(typeof(Kistl.App.Base.ObjectClass).FullName);

            Assembly a = Kistl.API.AssemblyLoader.Load("Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (a == null) throw new InvalidOperationException("Unable to load Kistl.Objects Assembly, no Entity Framework Metadata will be loaded");
            Kistl.API.AssemblyLoader.Load("Kistl.Objects.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            if (a == null) throw new InvalidOperationException("Unable to load Kistl.Objects.Server Assembly, no Entity Framework Metadata will be loaded");

            Kistl.API.AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects");
            Kistl.API.AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects.Server");

            SetCustomActionsManager(new CustomActionsManagerServer());
        }

    }
}
