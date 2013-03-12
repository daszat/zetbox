
namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.API.SchemaManagement;
    using Zetbox.App.Base;

    // TODO: Derive from IGlobalMigrationFragment to implement a global database migration 
    public class DemoGlobalMigrationFragment // : IGlobalMigrationFragment 
    {
        public void PreMigration(ISchemaProvider db)
        {
        }

        public void PostMigration(ISchemaProvider db)
        {
        }
    }

    // TODO: Derive from I*MigrationFragment to implement a specific (class, property, relation, ...) migration 
    public class DemoClassMigrationFragment // : IClassMigrationFragment
    {
        public ClassMigrationEventType ClassEventType
        {
            // TODO: Change Type
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
            // TODO: Add target guid here
            get { return Guid.Empty; }
        }
    }
}
