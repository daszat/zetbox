using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;


namespace Kistl.DalProvider.NHibernate.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Registrations.cst")]
    public partial class Registrations : Kistl.Generator.Templates.Registrations
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Registrations");
        }

        public Registrations(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 9 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("\r\n");
#line 10 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
base.Generate(); 
#line 11 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            builder\r\n");
this.WriteObjects("                .Register<ISessionFactory>(\r\n");
this.WriteObjects("                    c => {\r\n");
this.WriteObjects("                        var kistlConfig = c.Resolve<KistlConfig>();\r\n");
this.WriteObjects("                        var result = new Configuration();\r\n");
this.WriteObjects("                        result.DataBaseIntegration(db => \r\n");
this.WriteObjects("                            {\r\n");
this.WriteObjects("                                db.Dialect<global::NHibernate.Dialect.PostgreSQL82Dialect>();\r\n");
this.WriteObjects("                                db.ConnectionString = kistlConfig.Server.ConnectionString;\r\n");
this.WriteObjects("                            });\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                        return result\r\n");
this.WriteObjects("                            .AddAssembly(this.GetType().Assembly)\r\n");
this.WriteObjects("                            .Configure()\r\n");
this.WriteObjects("                            .BuildSessionFactory();\r\n");
this.WriteObjects("                    })\r\n");
this.WriteObjects("                .SingleInstance();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            builder\r\n");
this.WriteObjects("                .Register<ISession>(\r\n");
this.WriteObjects("                    c => c.Resolve<ISessionFactory>()\r\n");
this.WriteObjects("                            .OpenSession())\r\n");
this.WriteObjects("                // TODO: reconsider this configuration\r\n");
this.WriteObjects("                //       using IPD makes it safer, but requires passing the session manually\r\n");
this.WriteObjects("                //       on the other hand, the session should never escape the data context\r\n");
this.WriteObjects("                .InstancePerDependency();\r\n");

        }

    }
}