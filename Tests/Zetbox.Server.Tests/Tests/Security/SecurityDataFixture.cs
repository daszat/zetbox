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

namespace Zetbox.Server.Tests.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Projekte;

    public abstract class SecurityDataFixture : AbstractServerTestFixture
    {
        protected ZetboxConfig config;

        protected Identity admin;
        protected Identity identity1;
        protected Identity identity2;
        protected Identity identity3_low;

        protected Mitarbeiter ma1;
        protected Mitarbeiter ma2;
        protected Mitarbeiter ma3_low;

        protected Projekt prj1;
        protected Projekt prj2;
        protected Projekt prjCommon;

        int prj1ID, prjCommonID, prj2ID;

        protected IZetboxServerContext srvCtx;
        protected IZetboxContext id1Ctx;
        protected IZetboxContext id2Ctx;
        protected IZetboxContext id3Ctx_low;

        protected static readonly int id1ProjectCount = 2;
        protected static readonly int id2ProjectCount = 2;
        protected static readonly int projectCount = 3;
        protected static readonly int task_projectCount = 2;

        protected Group grpAdmin;
        protected Group grpEveryOne;

        private void CreateTestData()
        {
            {
                srvCtx = scope.Resolve<IZetboxServerContext>();

                var grpAdmin = Zetbox.NamedObjects.Base.Groups.Administrator.Find(srvCtx);
                var grpEveryOne = Zetbox.NamedObjects.Base.Groups.Everyone.Find(srvCtx);

                // Create Identities
                admin = srvCtx.Create<Identity>();
                admin.DisplayName = "Administrator";
                admin.UserName = "<TestDomain>\\Administrator";
                admin.Groups.Add(grpAdmin);
                admin.Groups.Add(grpEveryOne);

                identity1 = srvCtx.Create<Identity>();
                identity1.DisplayName = "User 1";
                identity1.UserName = "<TestDomain>\\User1";
                identity1.Groups.Add(grpEveryOne);

                identity2 = srvCtx.Create<Identity>();
                identity2.DisplayName = "User 2";
                identity2.UserName = "<TestDomain>\\User2";
                identity2.Groups.Add(grpEveryOne);

                identity3_low = srvCtx.Create<Identity>();
                identity3_low.DisplayName = "User 3 with low privileges";
                identity3_low.UserName = "<TestDomain>\\User3";

                ma1 = srvCtx.Create<Mitarbeiter>();
                ma1.Name = identity1.DisplayName;
                ma1.Identity = identity1;

                ma2 = srvCtx.Create<Mitarbeiter>();
                ma2.Name = identity2.DisplayName;
                ma2.Identity = identity2;

                ma3_low = srvCtx.Create<Mitarbeiter>();
                ma3_low.Name = identity3_low.DisplayName;
                ma3_low.Identity = identity3_low;

                srvCtx.SubmitChanges();
            }


            {
                // Create 3 identity context
                var ctx = scope.Resolve<ServerZetboxContextFactory>().Invoke(identity1);

                // Create TestData with Identity 1
                prj1 = ctx.Create<Projekt>();
                prj1.Name = "Project User 1";
                prj1.Mitarbeiter.Add(ctx.Find<Mitarbeiter>(ma1.ID));
                CreateTasks(ctx, prj1);

                // Create TestData with Identity 1, common
                prjCommon = ctx.Create<Projekt>();
                prjCommon.Name = "Project Common";
                prjCommon.Mitarbeiter.Add(ctx.Find<Mitarbeiter>(ma1.ID));
                prjCommon.Mitarbeiter.Add(ctx.Find<Mitarbeiter>(ma2.ID));
                CreateTasks(ctx, prjCommon);

                ctx.SubmitChanges();

                prj1ID = prj1.ID;
                prjCommonID = prjCommon.ID;
            }

            {
                var ctx = scope.Resolve<ServerZetboxContextFactory>().Invoke(identity2);

                // Create TestData with Identity 2
                prj2 = ctx.Create<Projekt>();
                prj2.Name = "Project User 2";
                prj2.Mitarbeiter.Add(ctx.Find<Mitarbeiter>(ma2.ID));
                CreateTasks(ctx, prj2);
                ctx.SubmitChanges();

                prj2ID = prj2.ID;
            }

            id1Ctx = scope.Resolve<ServerZetboxContextFactory>().Invoke(identity1);
            id2Ctx = scope.Resolve<ServerZetboxContextFactory>().Invoke(identity2);
            id3Ctx_low = scope.Resolve<ServerZetboxContextFactory>().Invoke(identity3_low);

            prj1 = id1Ctx.Find<Projekt>(prj1ID);
            prjCommon = id1Ctx.Find<Projekt>(prjCommonID);
            prj2 = id2Ctx.Find<Projekt>(prj2ID);


            // Fix security tables
            // Own test checks if this works during object modifications too
            var connectionString = config.Server.GetConnectionString(Helper.ZetboxConnectionStringKey);
            using (var db = scope.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider))
            {
                db.Open(connectionString.ConnectionString);
                db.ExecRefreshRightsOnProcedure(db.GetProcedureName("projekte", "RefreshRightsOn_Projekte"));
                db.ExecRefreshRightsOnProcedure(db.GetProcedureName("projekte", "RefreshRightsOn_Tasks"));
                db.ExecRefreshRightsOnProcedure(db.GetProcedureName("projekte", "RefreshRightsOn_Auftraege"));
            }
        }

        private void CreateTasks(IZetboxContext ctx, Projekt p)
        {
            for (int i = 0; i < task_projectCount; i++)
            {
                var t = ctx.Create<Task>();
                t.Name = "Task " + i;
                p.Tasks.Add(t);
            }
        }

        public void DeleteData()
        {
            var ctx = scope.Resolve<IZetboxServerContext>();
            ctx.GetQuery<Task>().ForEach(obj => ctx.Delete(obj));
            ctx.SubmitChanges();

            ctx = scope.Resolve<IZetboxServerContext>();
            ctx.GetQuery<Projekt>().ForEach(obj => { obj.Mitarbeiter.Clear(); ctx.Delete(obj); });
            ctx.SubmitChanges();

            ctx = scope.Resolve<IZetboxServerContext>();
            ctx.GetQuery<Mitarbeiter>().ForEach(obj => ctx.Delete(obj));
            ctx.GetQuery<Kunde>().ForEach(obj => ctx.Delete(obj));
            ctx.SubmitChanges();

            ctx = scope.Resolve<IZetboxServerContext>();
            if (identity1 != null) { var id = ctx.Find<Identity>(identity1.ID); id.Groups.Clear(); ctx.Delete(id); }
            if (identity2 != null) { var id = ctx.Find<Identity>(identity2.ID); id.Groups.Clear(); ctx.Delete(id); }
            if (identity3_low != null) { var id = ctx.Find<Identity>(identity3_low.ID); id.Groups.Clear(); ctx.Delete(id); }

            identity1 = null;
            identity2 = null;
            identity3_low = null;
            ctx.SubmitChanges();
        }

        public override void TearDown()
        {
            DeleteData();
            base.TearDown();
        }

        public override void SetUp()
        {
            base.SetUp();
            config = scope.Resolve<ZetboxConfig>();
            DeleteData();
            CreateTestData();
        }
    }
}
