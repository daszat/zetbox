using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Autofac;
using Kistl.App.Base;
using Kistl.App.Projekte;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Kistl.Server.Tests.Security
{
    public abstract class SecurityDataFixture
    {
        protected IContainer container;

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
            srvCtx = container.Resolve<IKistlServerContext>();

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
            identity3_low.UserName = "<TestDomain>\\User2";

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
            id1Ctx = container.GetKistlContext(identity1);
            id2Ctx = container.GetKistlContext(identity2);
            id3Ctx_low = container.GetKistlContext(identity3_low);

            // Create TestData with Identitiy 1
            prj1 = id1Ctx.Create<Projekt>();
            prj1.Name = "Project User 1";
            prj1.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma1.ID));
            CreateTasks(id1Ctx, prj1);

            // Create TestData with Identitiy 2
            prj2 = id2Ctx.Create<Projekt>();
            prj2.Name = "Project User 2";
            prj2.Mitarbeiter.Add(id2Ctx.Find<Mitarbeiter>(ma2.ID));
            CreateTasks(id2Ctx, prj2);

            // Create TestData with Identitiy 1, common
            prjCommon = id1Ctx.Create<Projekt>();
            prjCommon.Name = "Project Common";
            prjCommon.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma1.ID));
            prjCommon.Mitarbeiter.Add(id1Ctx.Find<Mitarbeiter>(ma2.ID));
            CreateTasks(id1Ctx, prjCommon);

            id1Ctx.SubmitChanges();
            id2Ctx.SubmitChanges();

            // Fix security tables
            // Own test checks if this works during objet modifications too
            using (SqlConnection db = new SqlConnection(ApplicationContext.Current.Configuration.Server.ConnectionString))
            {
                db.Open();
                using (SqlCommand cmd = db.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "RefreshRightsOn_Projekte";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "RefreshRightsOn_Tasks";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "RefreshRightsOn_Auftraege";
                    cmd.ExecuteNonQuery();
                }
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
            using (var ctx = container.Resolve<IKistlServerContext>())
            {
                if (identity1 != null) { var id = ctx.Find<Identity>(identity1.ID); id.Groups.Clear(); ctx.Delete(id); }
                if (identity2 != null) { var id = ctx.Find<Identity>(identity2.ID); id.Groups.Clear(); ctx.Delete(id); }
                if (identity3_low != null) { var id = ctx.Find<Identity>(identity3_low.ID); id.Groups.Clear(); ctx.Delete(id); }

                ctx.GetQuery<Kunde>().ForEach(obj => ctx.Delete(obj));
                ctx.GetQuery<Auftrag>().ForEach(obj => ctx.Delete(obj));
                ctx.GetQuery<Projekt>().ForEach(obj => { obj.Mitarbeiter.Clear(); obj.Tasks.Clear(); ctx.Delete(obj); });
                ctx.GetQuery<Task>().ForEach(obj => ctx.Delete(obj));
                ctx.GetQuery<Mitarbeiter>().ForEach(obj => ctx.Delete(obj));

                ctx.SubmitChanges();
            }
        }

        [SetUp]
        public void SetUp()
        {
            container = Kistl.Server.Tests.SetUp.CreateInnerContainer();
            DeleteData();
            CreateTestData();
        }

        [TearDown]
        public void DisposeContext()
        {
            srvCtx.Dispose();
            id1Ctx.Dispose();
            id2Ctx.Dispose();
        }
    }
}
