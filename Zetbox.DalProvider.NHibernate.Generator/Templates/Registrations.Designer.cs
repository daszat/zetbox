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
#line 25 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("\r\n");
#line 26 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
base.Generate(); 
#line 27 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Registrations.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            builder\r\n");
this.WriteObjects("                .Register<ISessionFactory>(\r\n");
this.WriteObjects("                    c => {\r\n");
this.WriteObjects("                        var zetboxConfig = c.Resolve<ZetboxConfig>();\r\n");
this.WriteObjects("                        using (Logging.Log.InfoTraceMethodCall(\"Init NH Session Factory\"))\r\n");
this.WriteObjects("                        {\r\n");
this.WriteObjects("                            var result = new Configuration();\r\n");
this.WriteObjects("                            var connectionString = zetboxConfig.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);\r\n");
this.WriteObjects("                            result.Properties[\"dialect\"] = connectionString.DatabaseProvider;\r\n");
this.WriteObjects("                            result.Properties[\"connection.connection_string\"] = connectionString.ConnectionString;\r\n");
this.WriteObjects("                            result.Properties[\"max_fetch_depth\"] = \"1\"; // keep SQL statements small\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                            return result\r\n");
this.WriteObjects("                                .AddAssembly(typeof(NHibernateModule).Assembly)\r\n");
this.WriteObjects("                                .BuildSessionFactory();\r\n");
this.WriteObjects("                        }\r\n");
this.WriteObjects("                    })\r\n");
this.WriteObjects("                .SingleInstance();\r\n");

        }

    }
}