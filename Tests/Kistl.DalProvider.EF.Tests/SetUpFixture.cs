
namespace Kistl.DalProvider.Ef.Tests
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Kistl.API;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Projekte;
    using Kistl.DalProvider.Ef.Mocks;
    using Kistl.Server;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture
        : AbstractSetUpFixture
    {

        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Server.ServerApiModule());
            builder.RegisterModule(new Kistl.Server.ServerModule());
            builder.RegisterModule(new Kistl.DalProvider.Ef.EfProvider());
            builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Kistl.Objects.InterfaceModule());
            builder.RegisterModule(new Kistl.Objects.EfModule());
            builder.RegisterModule(new Kistl.Objects.MemoryModule());
            builder.RegisterModule(new Kistl.Tests.Utilities.MsSql.UtilityModule());
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
            ResetDatabase(container);

            PropertyEfImpl.OnToString_Property
                += (obj, args) => { args.Result = String.Format("Prop, [{0}]", obj.Description); };
            MitarbeiterEfImpl.OnToString_Mitarbeiter
                += (obj, args) => { args.Result = String.Format("MA, [{0}]", obj.Name); };
            ProjektEfImpl.OnToString_Projekt
                += (obj, args) => { args.Result = String.Format("Proj, [{0}]", obj.Name); };
        }

        protected override string GetConfigFile()
        {
            return "Kistl.DalProvider.Ef.Tests.Config{0}.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
