using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API;
using Kistl.API.Server;

namespace Kistl.App.Projekte.Server
{
    public class CustomServerActionsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            // Register all non static ActionClasses
            foreach (var t in typeof(CustomServerActionsModule).Assembly.GetTypes())
            {
                if (!t.IsStatic())
                {
                    moduleBuilder.RegisterType(t)
                        .SingleInstance();
                }
            }

            // Register types explicit
            moduleBuilder.Register(c => new ZBox.App.SchemaMigraion.CustomServerActions_SchemaMigration(
                c.Resolve<SchemaProviderFactory> ("MSSQL"),
                c.Resolve<SchemaProviderFactory>("POSTGRES"),
                c.Resolve<SchemaProviderFactory>("OLEDB")
            ));
        }
    }
}
