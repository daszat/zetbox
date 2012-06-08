
namespace Zetbox.DalProvider.NHibernate.Tests
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Projekte;
    using Zetbox.Server;
    using Npgsql;
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
            builder.RegisterModule(new Zetbox.Objects.InterfaceModule());
            builder.RegisterModule(new Zetbox.Objects.NHibernateModule());
            builder.RegisterModule(new Zetbox.Objects.MemoryModule());
            builder.RegisterModule(new Zetbox.DalProvider.NHibernate.NHibernateProvider());
            builder.RegisterModule(new Zetbox.DalProvider.Memory.MemoryProvider());

            // load DB Utility from config
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
            ResetDatabase(container);

            PropertyNHibernateImpl.OnToString_Property
                += (obj, args) => { args.Result = String.Format("Prop, [{0}]", obj.Description); };
            MitarbeiterNHibernateImpl.OnToString_Mitarbeiter
                += (obj, args) => { args.Result = String.Format("MA, [{0}]", obj.Name); };
            ProjektNHibernateImpl.OnToString_Projekt
                += (obj, args) => { args.Result = String.Format("Proj, [{0}]", obj.Name); };
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.DalProvider.NHibernate.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }

        public override void TearDown()
        {
            base.TearDown();
            // always kill connections to the db
            NpgsqlConnection.ClearAllPools();
        }
    }
}
