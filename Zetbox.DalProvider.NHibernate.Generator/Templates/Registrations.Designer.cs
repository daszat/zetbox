using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst")]
    public partial class Registrations : Zetbox.Generator.Templates.Registrations
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("");
#line 25 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("\n");
#line 26 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
base.Generate(); 
#line 27 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("\n");
this.WriteObjects("            builder\n");
this.WriteObjects("                .Register<ISessionFactory>(\n");
this.WriteObjects("                    c => {\n");
this.WriteObjects("                        var zetboxConfig = c.Resolve<ZetboxConfig>();\n");
this.WriteObjects("                        var result = new Configuration();\n");
this.WriteObjects("                        var connectionString = zetboxConfig.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);\n");
this.WriteObjects("                        result.Properties[\"dialect\"] = connectionString.DatabaseProvider;\n");
this.WriteObjects("                        result.Properties[\"connection.connection_string\"] = connectionString.ConnectionString;\n");
this.WriteObjects("                        result.Properties[\"max_fetch_depth\"] = \"1\"; // keep SQL statements small\n");
this.WriteObjects("\n");
this.WriteObjects("                        return result\n");
this.WriteObjects("                            .AddAssembly(typeof(NHibernateModule).Assembly)\n");
this.WriteObjects("                            .BuildSessionFactory();\n");
this.WriteObjects("                    })\n");
this.WriteObjects("                .SingleInstance();\n");
this.WriteObjects("\n");
this.WriteObjects("            builder\n");
this.WriteObjects("                .Register<ISession>(\n");
this.WriteObjects("                    (c, p) => {\n");
this.WriteObjects("                        var result = c.Resolve<ISessionFactory>().OpenSession(c.Resolve<IInterceptor>());\n");
this.WriteObjects("                        Logging.Log.DebugFormat(\"Created ISession: {0}\", result.GetHashCode());\n");
this.WriteObjects("                        return result;\n");
this.WriteObjects("                    })\n");
this.WriteObjects("                .OnRelease(s => Logging.Log.DebugFormat(\"Disposed ISession: {0}\", s.GetHashCode()))\n");
this.WriteObjects("                // TODO: reconsider this configuration\n");
this.WriteObjects("                //       using IPD makes it safer, but requires passing the session manually\n");
this.WriteObjects("                //       on the other hand, the session should never escape the data context\n");
this.WriteObjects("                .InstancePerDependency();\n");

        }

    }
}