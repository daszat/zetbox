
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.API.SchemaManagement;
    using Zetbox.App.Base;

    public class DemoGlobalMigrationFragment : IGlobalMigrationFragment
    {
        public void PreMigration(ISchemaProvider db)
        {
        }

        public void PostMigration(ISchemaProvider db)
        {
        }
    }

    public class DemoClassMigrationFragment : IClassMigrationFragment
    {
        public ClassMigrationEventType ClassEventType
        {
            get { return ClassMigrationEventType.Add; }
        }

        public bool PreMigration(ISchemaProvider db, ObjectClass oldClass, ObjectClass newClass)
        {
            // the default implementation of the event should run
            return true;
        }

        public void PostMigration(ISchemaProvider db, ObjectClass oldClass, ObjectClass newClass)
        {
        }

        public Guid Target
        {
            get { return Guid.Empty; }
        }
    }
}
