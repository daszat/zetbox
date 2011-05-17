
namespace Kistl.Server.Tests.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.App.Projekte;
    using NUnit.Framework;

    public abstract class SecurityDataFixture : AbstractServerTestFixture
    {
        protected KistlConfig config;

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

        protected IKistlServerContext srvCtx;
        protected IKistlContext id1Ctx;
        protected IKistlContext id2Ctx;
        protected IKistlContext id3Ctx_low;

        protected static readonly int id1ProjectCount = 2;
        protected static readonly int id2ProjectCount = 2;
        protected static readonly int projectCount = 3;
        protected static readonly int task_projectCount = 2;

        protected Group grpAdmin;
        protected Group grpEveryOne;

        private void CreateTestData()
        {
            srvCtx = scope.Resolve<IKistlServerContext>();

            var grpAdmin = srvCtx.FindPersistenceObject<Group>(new Guid("9C46F2B1-09D9-46B8-A7BF-812850921030"));
            var grpEveryOne = srvCtx.FindPersistenceObject<Group>(new Guid("76D43CF2-4DDF-4A3A-9AD6-28CABFDDDFF1"));

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

            // Create 3 identity context
            id1Ctx = scope.Resolve<ServerKistlContextFactory>().Invoke(identity1);
            id2Ctx = scope.Resolve<ServerKistlContextFactory>().Invoke(identity2);
            id3Ctx_low = scope.Resolve<ServerKistlContextFactory>().Invoke(identity3_low);

            // Create TestData with Identity 1
            prj1 = id1Ctx.Create<Projekt>();
            prj1.Name = "Project User 1";
            prj1.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma1.ID));
            CreateTasks(id1Ctx, prj1);

            // Create TestData with Identity 2
            prj2 = id2Ctx.Create<Projekt>();
            prj2.Name = "Project User 2";
            prj2.Mitarbeiter.Add(id2Ctx.Find<Mitarbeiter>(ma2.ID));
            CreateTasks(id2Ctx, prj2);

            // Create TestData with Identity 1, common
            prjCommon = id1Ctx.Create<Projekt>();
            prjCommon.Name = "Project Common";
            prjCommon.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma1.ID));
            prjCommon.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma2.ID));
            CreateTasks(id1Ctx, prjCommon);

            id1Ctx.SubmitChanges();
            id2Ctx.SubmitChanges();

            // Fix security tables
            // Own test checks if this works during object modifications too
            var connectionString = config.Server.GetConnectionString(Helper.KistlConnectionStringKey);
            using (var db = scope.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider))
            {
                db.Open(connectionString.ConnectionString);
                db.ExecRefreshRightsOnProcedure(db.GetQualifiedProcedureName("RefreshRightsOn_Projekte"));
                db.ExecRefreshRightsOnProcedure(db.GetQualifiedProcedureName("RefreshRightsOn_Tasks"));
                db.ExecRefreshRightsOnProcedure(db.GetQualifiedProcedureName("RefreshRightsOn_Auftraege"));
            }
        }

        private void CreateTasks(IKistlContext ctx, Projekt p)
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
            using (var ctx = scope.Resolve<IKistlServerContext>())
            {
                ctx.GetQuery<Task>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();

                if (identity1 != null) { var id = ctx.Find<Identity>(identity1.ID); id.Groups.Clear(); ctx.Delete(id); }
                if (identity2 != null) { var id = ctx.Find<Identity>(identity2.ID); id.Groups.Clear(); ctx.Delete(id); }
                if (identity3_low != null) { var id = ctx.Find<Identity>(identity3_low.ID); id.Groups.Clear(); ctx.Delete(id); }

                ctx.GetQuery<Kunde>().ForEach(obj => ctx.Delete(obj));
                ctx.GetQuery<Auftrag>().ForEach(obj => ctx.Delete(obj));
                ctx.GetQuery<Mitarbeiter>().ForEach(obj => ctx.Delete(obj));
                ctx.GetQuery<Projekt>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        public override void SetUp()
        {
            base.SetUp();
            config = scope.Resolve<KistlConfig>();
            DeleteData();
            CreateTestData();
        }
    }
}
