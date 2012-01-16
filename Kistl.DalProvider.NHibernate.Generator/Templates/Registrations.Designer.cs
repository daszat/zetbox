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
this.WriteObjects("                        var connectionString = kistlConfig.Server.GetConnectionString(Kistl.API.Helper.KistlConnectionStringKey);\r\n");
this.WriteObjects("                        result.Properties[\"dialect\"] = connectionString.DatabaseProvider;\r\n");
this.WriteObjects("                        result.Properties[\"connection.connection_string\"] = connectionString.ConnectionString;\r\n");
this.WriteObjects("                        result.Properties[\"max_fetch_depth\"] = \"1\"; // keep SQL statements small\r\n");
this.WriteObjects("#if DEBUG\r\n");
this.WriteObjects("                        // make debugging easier by removing reflection optimizations\r\n");
this.WriteObjects("                        result.Properties[\"use_reflection_optimizer\"] = \"false\";\r\n");
this.WriteObjects("                        result.Properties[\"bytecode.provider\"] = \"null\";\r\n");
this.WriteObjects("#endif\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                        return result\r\n");
this.WriteObjects("                            .AddAssembly(typeof(NHibernateModule).Assembly)\r\n");
this.WriteObjects("                            .BuildSessionFactory();\r\n");
this.WriteObjects("                    })\r\n");
this.WriteObjects("                .SingleInstance();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            builder\r\n");
this.WriteObjects("                .Register<ISession>(\r\n");
this.WriteObjects("                    (c, p) => {\r\n");
this.WriteObjects("                        var result = c.Resolve<ISessionFactory>().OpenSession(c.Resolve<IInterceptor>());\r\n");
this.WriteObjects("                        Logging.Log.InfoFormat(\"Created ISession: {0}\", result.GetHashCode());\r\n");
this.WriteObjects("                        return result;\r\n");
this.WriteObjects("                    })\r\n");
this.WriteObjects("                .OnRelease(s => Logging.Log.InfoFormat(\"Disposing of ISession: {0}\", s.GetHashCode()))\r\n");
this.WriteObjects("                // TODO: reconsider this configuration\r\n");
this.WriteObjects("                //       using IPD makes it safer, but requires passing the session manually\r\n");
this.WriteObjects("                //       on the other hand, the session should never escape the data context\r\n");
this.WriteObjects("                .InstancePerDependency();\r\n");

        }

    }
}