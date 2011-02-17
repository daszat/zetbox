namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    [Implementor]
    public static class SourceTableActions
    {
        [Invocation]
        public static void ToString(ZBox.App.SchemaMigration.SourceTable obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "[" + ((obj.StagingDatabase != null ? obj.StagingDatabase.Description : null) ?? string.Empty) + "]";
            e.Result += "." + (!string.IsNullOrEmpty(obj.Name) ? obj.Name : "new Source Table");
        }

        [Invocation]
        public static void CreateObjectClass(ZBox.App.SchemaMigration.SourceTable obj)
        {
            if (obj.StagingDatabase == null) throw new InvalidOperationException("Not attached to a staging database");
            if (obj.StagingDatabase.MigrationProject == null) throw new InvalidOperationException("Not attached to a migration project");
            if (obj.StagingDatabase.MigrationProject.DestinationModule == null) throw new InvalidOperationException("No destination module provided");
            if (obj.DestinationObjectClass != null) throw new InvalidOperationException("there is already a destination object class");

            obj.DestinationObjectClass = obj.Context.Create<ObjectClass>();
            obj.DestinationObjectClass.Name = obj.Name;
            obj.DestinationObjectClass.Module = obj.StagingDatabase.MigrationProject.DestinationModule;
            obj.DestinationObjectClass.Description = obj.Description;
        }
    }
}
