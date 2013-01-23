using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.SchemaManagement;

namespace Zetbox.Server.SchemaManagement.MigrationFragments
{
    public class DemoGlobalMigrationFragment : IGlobalMigrationFragment
    {
        public void PreMigration(API.Server.ISchemaProvider db)
        {
        }

        public void PostMigration(API.Server.ISchemaProvider db)
        {
        }
    }

    public class DemoClassMigrationFragment : IClassMigrationFragment
    {
        public ClassMigrationEventType ClassEventType
        {
            get { return ClassMigrationEventType.Add; }
        }

        public bool PreMigration(API.Server.ISchemaProvider db, App.Base.ObjectClass oldClass, App.Base.ObjectClass newClass)
        {
            // the default implementation of the event should run
            return true;
        }

        public void PostMigration(API.Server.ISchemaProvider db, App.Base.ObjectClass oldClass, App.Base.ObjectClass newClass)
        {
        }

        public Guid Target
        {
            get { return Guid.Empty; }
        }
    }
}
