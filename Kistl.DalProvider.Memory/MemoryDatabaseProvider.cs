
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using Kistl.App.Packaging;

    public sealed class MemoryDatabaseProvider
        : Autofac.Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory");
        private readonly static object _lock = new object();

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register(c =>
                {
                    lock (_lock)
                    {
                        var result = new MemoryContext(
                            c.Resolve<InterfaceType.Factory>(),
                            c.Resolve<Func<IFrozenContext>>(),
                            c.Resolve<MemoryImplementationType.MemoryFactory>()
                            );

                        return result;
                    }
                })
                .As<IReadOnlyKistlContext>()
                .As<IKistlContext>()
                .OnActivated(args =>
                {
                    var config = args.Context.Resolve<KistlConfig>();
                    Importer.LoadFromXml(args.Instance, config.Server.ConnectionString);

                    var manager = args.Context.Resolve<IMemoryActionsManager>();
                    manager.Init(args.Instance);
                })
                .InstancePerDependency();
        }
    }
}
