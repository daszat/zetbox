using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.SchemaManagement;

namespace Zetbox.Server.SchemaManagement.MigrationFragments
{
    public class File_IsFileReadonlyMigrationFragment : IPropertyMigrationFragment
    {
        public PropertyMigrationEventType PropertyEventType
        {
            get { return PropertyMigrationEventType.Add; }
        }

        public bool PreMigration(API.Server.ISchemaProvider db, App.Base.Property oldProperty, App.Base.Property newProperty)
        {
            // the default implementation of the event should run
            return true;
        }

        public void PostMigration(API.Server.ISchemaProvider db, App.Base.Property oldProperty, App.Base.Property newProperty)
        {
            if (db == null) throw new ArgumentNullException("db");
            db.ExecuteSqlResource(typeof(File_IsFileReadonlyMigrationFragment), "Zetbox.Server.SchemaManagement.MigrationFragments.Scripts.SetIsFileReadonly.{0}.sql");
        }

        public Guid Target
        {
            get { return NamedObjects.Base.Classes.at.dasz.DocumentManagement.File_Properties.IsFileReadonly.Guid; }
        }
    }

    public class File_KeepRevisionsMigrationFragment : IPropertyMigrationFragment
    {
        public PropertyMigrationEventType PropertyEventType
        {
            get { return PropertyMigrationEventType.Add; }
        }

        public bool PreMigration(API.Server.ISchemaProvider db, App.Base.Property oldProperty, App.Base.Property newProperty)
        {
            // the default implementation of the event should run
            return true;
        }

        public void PostMigration(API.Server.ISchemaProvider db, App.Base.Property oldProperty, App.Base.Property newProperty)
        {
            if (db == null) throw new ArgumentNullException("db");
            db.ExecuteSqlResource(typeof(File_IsFileReadonlyMigrationFragment), "Zetbox.Server.SchemaManagement.MigrationFragments.Scripts.SetKeepRevisions.{0}.sql");
        }

        public Guid Target
        {
            get { return NamedObjects.Base.Classes.at.dasz.DocumentManagement.File_Properties.KeepRevisions.Guid; }
        }
    }
}
