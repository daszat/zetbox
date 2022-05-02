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

namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.App.Packaging;

    // Not a feature, will be loaded by ApiCommon module
    [Description("Memory provider")]
    public class MemoryProvider
        : Autofac.Module
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MemoryProvider));

        public static readonly string ContextClassName = "Zetbox.Objects.Memory.MemoryContext";
        public static readonly string GeneratedAssemblyName = "Zetbox.Objects.MemoryImpl";

        private static readonly SemaphoreSlim _initLock = new SemaphoreSlim(1, 1);

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<MemoryImplementationType>()
                .As<MemoryImplementationType>()
                .As<ImplementationType>()
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<MemoryContext>()
                .As<BaseMemoryContext>()
                .OnActivated(async args =>
                {
                    var manager = args.Context.Resolve<IMemoryActionsManager>();
                    await manager.Init(args.Context.Resolve<IFrozenContext>());

                    ZetboxContextEventListenerHelper.OnCreated(args.Context.Resolve<IEnumerable<IZetboxContextEventListener>>(), args.Instance);
                })
                .InstancePerDependency();

            moduleBuilder.RegisterModule((Autofac.Module)Activator.CreateInstance(Type.GetType("Zetbox.Objects.MemoryModule, Zetbox.Objects.MemoryImpl", true)));

            try
            {
                // TODO: Move to MemoryModule
                var generatedAssembly = Assembly.Load(GeneratedAssemblyName);
                moduleBuilder
                    .Register(c =>
                    {
                        FrozenMemoryContext memCtx = null;
                        memCtx = new FrozenMemoryContext(
                            c.Resolve<InterfaceType.Factory>(),
                            () => memCtx,
                            c.Resolve<MemoryImplementationType.MemoryFactory>(),
                            c.Resolve<IEnumerable<IZetboxContextEventListener>>());
                        
                        return memCtx;
                    })
                    .As<IFrozenContext>()
                    .OnActivated(async args =>
                    {
                        await _initLock.WaitAsync();
                        var manager = args.Context.Resolve<IMemoryActionsManager>();
                        var listener = args.Context.Resolve<IEnumerable<IZetboxContextEventListener>>();

                        await Importer.LoadFromXml(args.Instance, generatedAssembly.GetManifestResourceStream("Zetbox.Objects.MemoryImpl.FrozenObjects.xml"), "FrozenContext XML from Assembly");
                        args.Instance.Seal();

                        await manager.Init(args.Instance);

                        ZetboxContextEventListenerHelper.OnCreated(listener, args.Instance);
                        _initLock.Release();
                    })
                    .SingleInstance();

                // Make the Func<IFrozenContext> a single instance
                moduleBuilder.Register<Func<IFrozenContext>>(c =>
                {
                    var frozenCtx = c.Resolve<IFrozenContext>();
                    return () => frozenCtx;
                })
                .SingleInstance();

            }
            catch (FileNotFoundException ex)
            {
                Log.Warn("Could not load memory context", ex);
            }
            catch (Exception ex)
            {
                Log.Error("Error while loading memory context", ex);
                throw;
            }
        }
    }
}
