
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.SchemaManagement;

    [AutoLoad]
    [Description("$safeprojectname$ migrations module")]
    public class MigrationsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterMigrationFragments(typeof(MigrationsModule).Assembly);

            // Register explicit overrides here
        }
    }
}
