
namespace Zetbox.DalProvider.Ef.Tests
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Projekte;
    using Zetbox.DalProvider.Ef.Mocks;
    using Zetbox.Server;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture
        : AbstractSetUpFixture
    {

        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Zetbox.API.ApiModule());
            builder.RegisterModule(new Zetbox.API.Common.ApiCommonModule());
            builder.RegisterModule(new Zetbox.API.Server.ServerApiModule());
            builder.RegisterModule(new Zetbox.Server.ServerModule());
            builder.RegisterModule(new Zetbox.DalProvider.Ef.EfProvider());
            builder.RegisterModule(new Zetbox.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Zetbox.Objects.InterfaceModule());
            builder.RegisterModule(new Zetbox.Objects.EfModule());
            builder.RegisterModule(new Zetbox.Objects.MemoryModule());

            // load DB Utility from config
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
            return "Zetbox.DalProvider.Ef.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
