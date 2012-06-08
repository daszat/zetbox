namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class StagingDatabaseActions
    {
        [Invocation]
        public static void ToString(ZBox.App.SchemaMigration.StagingDatabase obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Description;
        }

    }
}
