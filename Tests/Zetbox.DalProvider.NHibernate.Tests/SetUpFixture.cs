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

namespace Zetbox.DalProvider.NHibernate.Tests
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Npgsql;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Projekte;
    using Zetbox.Server;
    using stt = System.Threading.Tasks;

    [SetUpFixture]
    public class SetUpFixture
        : AbstractSetUpFixture
    {
        protected override void SetupBuilder(ContainerBuilder builder)
        {
            builder.RegisterModule(new Zetbox.Server.ServerModule());
            builder.RegisterModule(new Zetbox.DalProvider.NHibernate.NHibernateProvider());

            // load overrides after loading the default modules
            base.SetupBuilder(builder);
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);

            PropertyNHibernateImpl.OnToString_Property
                += (obj, args) => { args.Result = String.Format("Prop, [{0}]", obj.Description); return stt.Task.CompletedTask; };
            MitarbeiterNHibernateImpl.OnToString_Mitarbeiter
                += (obj, args) => { args.Result = String.Format("MA, [{0}]", obj.Name); return stt.Task.CompletedTask; };
            ProjektNHibernateImpl.OnToString_Projekt
                += (obj, args) => { args.Result = String.Format("Proj, [{0}]", obj.Name); return stt.Task.CompletedTask; };
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
